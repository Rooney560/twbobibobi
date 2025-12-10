/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：OrderService.cs
 * 類別說明：訂單建立服務，負責整合批量讀取、組合商品拆分、資料展平、分組、並交由各宮廟 Processor 執行
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 新增資料驅動版組合商品展開（雙重驗證：ServiceID=26 AND ComboRules > 0）；加入 TempleCode 反查 ProductCode，移除硬編碼 ProductCode；
 * 
 * 目前維護人員：Rooney
 **************************************************************************************************/

using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Data;
using twbobibobi.FET.API;
using twbobibobi.FET.Data;
using twbobibobi.FET.Dto;
using twbobibobi.FET.Processors;

namespace twbobibobi.FET.Services
{
    /// <summary>
    /// 訂單服務，實作 <see cref="IOrderService"/>。
    /// 負責：
    /// 1. 組合商品展開（若有）
    /// 2. ProductCode → TempleCodeInfo 批量查詢
    /// 3. 展平 PrayedPersonDto
    /// 4. 依 AdminID + ServiceID 進行分組
    /// 5. 呼叫各宮廟 Processor 執行 ProcessAsync
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ITempleCodeRepository _templeCodeRepo;
        private readonly ITempleOrderProcessorFactory _processorFactory;
        private readonly IComboProductRuleService _comboRuleService;

        /// <summary>
        /// 建構子。
        /// </summary>
        /// <param name="templeCodeRepo">提供 ProductCode → TempleCodeInfo 查詢之 Repository。</param>
        /// <param name="processorFactory">宮廟 Processor 工廠，用來取得正確的處理器。</param>
        /// <param name="comboRuleService">取得組合商品的展開規則。</param>
        public OrderService(
            ITempleCodeRepository templeCodeRepo,
            ITempleOrderProcessorFactory processorFactory,
            IComboProductRuleService comboRuleService)
        {
            _templeCodeRepo = templeCodeRepo;
            _processorFactory = processorFactory;
            _comboRuleService = comboRuleService;
        }

        /// <summary>
        /// 處理訂單主流程：
        /// 1. 若有組合商品，先展開
        /// 2. 批量查詢 TempleCodeInfo
        /// 3. 填入每個 PrayedPersonDto 所需的 AdminID / ServiceID / 類別資料
        /// 4. 依宮廟與服務種類分組並呼叫 Processor 執行
        /// </summary>
        /// <param name="clientOrderNumber">客戶端訂單編號。</param>
        /// <param name="request">完整訂單請求內容（Applicant + Items）。</param>
        /// <param name="fetOrderNumber">遠傳金流訂單編號。</param>
        /// <param name="TotalAmount">訂單總金額。</param>
        /// <param name="itemsInfo">原始 items 的 JSON 字串（紀錄用）。</param>
        /// <param name="OrderId">資料庫建立的訂單編號。</param>
        /// <returns>回傳 PartnerOrderNumbers 等資訊。</returns>
        /// <exception cref="KeyNotFoundException">當 productCode 無法對應任何 TempleCodeInfo 時擲出。</exception>
        public async Task<OrderResultDto> ProcessOrderAsync(
            string clientOrderNumber,
            OrderRequestDto request,
            string fetOrderNumber,
            string TotalAmount,
            string itemsInfo,
            string OrderId)
        {
            // 1. 批量取得所有 productCode 對應的 TempleCodeInfo（展開前）
            var productCodes = request.Items.Select(i => i.ProductCode).Distinct().ToList();

            // 2. 批次查 TempleCode（會回傳 AdminID/ServiceID/TypeID/TypeString）等資訊（展開前）
            var codeInfos = await _templeCodeRepo.GetByProductCodesAsync(productCodes);

            // 3. 展開組合商品（資料驅動 + 雙重驗證）
            request.Items = ExpandComboItemsIfNeeded(request.Items, codeInfos, TotalAmount);

            // 4. 批量取得所有 productCode 對應的 TempleCodeInfo（展開後）（避免 KeyNotFoundException）
            var productCodes2 = request.Items.Select(i => i.ProductCode).Distinct().ToList();

            // 5. 批次查 TempleCode（會回傳 AdminID/ServiceID/TypeID/TypeString）等資訊（展開後）（避免 KeyNotFoundException）
            codeInfos = await _templeCodeRepo.GetByProductCodesAsync(productCodes2);

            // 6. 將每筆 ItemDto.PrayedPerson 都打上對應的 codeInfo
            var flatList = new List<PrayedPersonDto>();

            // 宣告一個金額去加總單樣價格後 與總金額 TotalAmount 作比對驗證是否相同金額
            int cost = 0;

            foreach (var item in request.Items)
            {
                string productCode = item.ProductCode;

                // 用 TryGetValue 保证不会 KeyNotFound
                if (!codeInfos.TryGetValue(productCode, out var info))
                {
                    // 找不到就跳错或紀錄 log
                    throw new KeyNotFoundException($"找不到廟宇代碼 {productCode} 的對應資料");
                }

                // 1. 如果 null，就先 new 一個 List
                if (item.PrayedPerson == null)
                    item.PrayedPerson = new List<PrayedPersonDto>();

                // 2. 如果是空集合，就至少新增一筆空白的 PrayedPersonDto
                if (!item.PrayedPerson.Any())
                    item.PrayedPerson.Add(new PrayedPersonDto());

                foreach (var pp in item.PrayedPerson)
                {
                    pp.AdminID = info.AdminID;
                    pp.ServiceID = info.ServiceID;
                    pp.TypeID = info.TypeID;
                    pp.TypeString = info.TypeString;
                    pp.Cost = (int)item.UnitPrice;         // ← 取這筆 item 的 UnitPrice

                    // 文創小販部
                    if (info.ServiceID == 3)
                    {
                        pp.Qty = item.Qty;                    // ← 取這筆 item 的 Qty
                        pp.ItemTypeID = info.ItemTypeID;
                    }

                    flatList.Add(pp);
                }
            }

            // 7. 根據 AdminID 和 ServiceID 分組，每組交給對應宮廟的 Processor
            var groups = flatList
                .GroupBy(p => new { p.ServiceID, p.AdminID });

            var tasks = new List<Task<List<string>>>();
            foreach (var g in groups)
            {
                // 拿出這一組的 key
                int serviceId = g.Key.ServiceID;    // kind
                int adminId = g.Key.AdminID;        // Temple

                // 根據 serviceId 自動切換年度
                _processorFactory.UpdateYearByService(serviceId);

                // Factory 會回傳大甲/桃園/… 的 Processor
                var processor = _processorFactory.GetProcessor(adminId);

                // **在此處記錄「即將呼叫哪一個 Processor」**
                var page = HttpContext.Current.Handler as CreateOrder;
                if (page != null)
                {
                    // GetType().Name 會取得類別名稱，例如 "TywtgOrderProcessor" 或 "DaOrderProcessor"
                    string procName = processor.GetType().Name;
                    page.SaveTimingLog($"將呼叫 Processor 類別：{procName}", 0);
                }

                // 呼 ProcessAsync，並把 clientOrderNumber、Tid、fetOrderNumber、kind 一併帶入
                tasks.Add(processor.ProcessAsync(
                        request.Applicant,
                        g.ToList(),
                        clientOrderNumber,
                        fetOrderNumber,
                        serviceId.ToString(),
                        TotalAmount,
                        itemsInfo,
                        OrderId
                    ));
            }

            // 8. 等待所有宮廟處理完成
            var results = await Task.WhenAll(tasks);  // List<string>[]

            // 合併成一條清單
            var collectedList = results.SelectMany(x => x).ToList();

            // 假设你已经把所有 partnerOrderNumbers 收集到 collectedList
            return new OrderResultDto
            {
                ClientOrderNumber = clientOrderNumber,
                PartnerOrderNumbers = collectedList
            };
        }

        /// <summary>
        /// 偵測組合商品並展開為多筆 item。（需同時符合：ServiceID=26 AND ComboRules > 0）
        /// 以「奉天宮安太歲 + 文創商品」為例：
        /// 1. 安太歲固定 620 元
        /// 2. 文創商品金額 = TotalAmount - 620
        /// 3. 原始祈福人資料保留給安太歲使用
        /// </summary>
        /// <param name="items">原始 ItemDto 清單。</param>
        /// <param name="codeInfos">TempleCode 資訊。</param>
        /// <param name="totalAmountString">訂單總金額字串。</param>
        /// <returns>展開後的新 ItemDto 清單。</returns>
        private List<ItemDto> ExpandComboItemsIfNeeded(
            List<ItemDto> items,
            Dictionary<string, TempleCodeInfoDto> codeInfos,
            string totalAmountString)
        {
            const int comboKind = 26;      // 組合商品用的 ServiceID (Kind)

            int totalAmount = int.Parse(totalAmountString);

            var result = new List<ItemDto>();

            foreach (var item in items)
            {
                // 先取得此商品資訊
                if (!codeInfos.TryGetValue(item.ProductCode, out var info))
                {
                    // 找不到就直接原樣加入
                    result.Add(item);
                    continue;
                }

                // 取得此商品是否有 Combo 規則
                var rules = _comboRuleService.GetRules(info.CodeID);

                // ★ 雙重驗證 ★
                bool isCombo =
                    info.ServiceID == comboKind   // 舊邏輯
                    && rules != null
                    && rules.Count > 0;          // 新資料規則

                if (!isCombo)
                {
                    result.Add(item);
                    continue;
                }

                // 開始展開 Combo 子商品
                int usedAmount = 0;

                foreach (var rule in rules.OrderBy(r => r.SortOrder))
                {
                    // 反查 ProductCode（不寫死）
                    var code = _templeCodeRepo.GetCode(rule.AdminID, rule.ServiceID, rule.TypeID);
                    if (code == null)
                        throw new Exception($"TempleCode 查無資料 Admin={rule.AdminID}, Service={rule.ServiceID}, Type={rule.TypeID}");

                    // 計算子商品價格
                    int price = 0;

                    switch (rule.PriceSource)
                    {
                        case "Fixed":
                            price = rule.FixedPrice.Value;
                            break;

                        case "LightsCost":
                            price = AjaxBasePage.GetLightsCost(rule.AdminID, rule.TypeID.ToString());
                            break;

                        case "Formula":
                            price = totalAmount - usedAmount;
                            break;
                    }

                    usedAmount += price;

                    // 建立子商品
                    result.Add(new ItemDto
                    {
                        ProductCode = code.ProductCode,
                        Qty = 1,
                        UnitPrice = price,
                        PrayedPerson = (rule.ServiceID == 3
                            ? new List<PrayedPersonDto>()
                            : item.PrayedPerson)
                    });
                }
            }

            return result;
        }
    }
}