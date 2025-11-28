using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.FET.API;
using twbobibobi.FET.Data;
using twbobibobi.FET.Dto;
using twbobibobi.FET.Processors;
using ZXing;

namespace twbobibobi.FET.Services
{
    /// <summary>
    /// Class: OrderService
    /// 實作 IOrderService，負責整合批量讀取、展平資料、分組與交由處理器執行
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly ITempleCodeRepository _templeCodeRepo;
        private readonly ITempleOrderProcessorFactory _processorFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templeCodeRepo"></param>
        /// <param name="processorFactory"></param>
        public OrderService(
            ITempleCodeRepository templeCodeRepo,
            ITempleOrderProcessorFactory processorFactory)
        {
            _templeCodeRepo = templeCodeRepo;
            _processorFactory = processorFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientOrderNumber"></param>
        /// <param name="request"></param>
        /// <param name="fetOrderNumber"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="itemsInfo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<OrderResultDto> ProcessOrderAsync(
            string clientOrderNumber,
            OrderRequestDto request,
            string fetOrderNumber,
            string TotalAmount,
            string itemsInfo,
            string OrderId)
        {
            // 1. 批量取得所有 productCode 對應的 TempleCodeInfo
            var productCodes = request.Items.Select(i => i.ProductCode).Distinct().ToList();

            // 2. 批次查 TempleCode（會回傳 AdminID/ServiceID/TypeID/TypeString）
            var codeInfos = await _templeCodeRepo.GetByProductCodesAsync(productCodes);

            // 3. 將每筆 ItemDto.PrayedPerson 都打上對應的 codeInfo
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

            // 4. 根據 AdminID 和 ServiceID 分組，每組交給對應宮廟的 Processor
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

            // 5. 等待所有宮廟處理完成
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
    }
}