/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AddAllowance.cs
   類別說明：提供開立折讓單 API（WebForms API）
   建立日期：2025-12-18
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Model.Allowance;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// /Api/AddAllowance.aspx?env=YYY API 頁面  
    /// 功能：依據 allowanceNumber 開立折讓單，並支援 UAT / Prod 環境切換、簽章驗證、防重放攻擊
    /// </summary>
    /// <remarks>
    /// 這個頁面處理折讓單開立請求，包含驗證參數、簽章、時間戳以及折讓單狀態檢查，最後呼叫折讓單開立 API 並回傳結果。
    /// </remarks>
    public partial class AddAllowance : AjaxBasePage
    {
        /// <summary>依據 QueryString env 決定環境設定後綴（_UAT / _Prod）</summary>
        static string EnvSuffix = HttpContext.Current.Request["env"] == "uat" ? "_UAT" : "_Prod";

        /// <summary>發票狀態服務</summary>
        IInvoiceStatusService _service_invoice_status = InvoiceServiceFactory.CreateStatusService(GetEnvironment());
        /// <summary>折讓單狀態服務</summary>
        IInvoiceStatusService _service_allowance_status = AllowanceServiceFactory.CreateStatusService(GetEnvironment());
        /// <summary>折讓單狀態服務</summary>
        IInvoiceQueryService _service_allowance_query = AllowanceServiceFactory.CreateQueryService(GetEnvironment());
        /// <summary>折讓單開立服務</summary>
        IInvoiceService _service_create = AllowanceServiceFactory.Create(GetEnvironment());

        private static readonly object _globalAllowanceLock = new object();
        private static readonly HashSet<string> _allowanceProcessing = new HashSet<string>();

        /// <summary>
        /// 取得目前 API 呼叫使用的環境（Prod / UAT）
        /// </summary>
        private static string GetEnvironment()
        {
            return HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";
        }

        /// <summary>
        /// Page_Load 入口：
        /// 1. 接受 GET  
        /// 2. 驗證必要參數  
        /// 3. 驗證簽章  
        /// 4. 驗證時間戳（防重放攻擊）  
        /// 5. 進入主流程 ProcessRequest()
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod != "GET")
                {
                    Response.StatusCode = 405; // Method Not Allowed
                    return;
                }

                // 讀參數
                string invoiceNo = Request["invoiceNumber"];
                string orderID = Request["oid"];
                string adminID = Request["a"];
                string applicantID = Request["aid"];
                string kind = Request["kind"];
                string Year = Request["y"];
                string env = Request["env"] == "uat" ? "uat" : "prod";
                string ts = Request["ts"];
                string sig = Request["sig"];

                // 1. 基本參數檢查
                if (string.IsNullOrWhiteSpace(invoiceNo) || 
                    string.IsNullOrWhiteSpace(orderID) ||
                    string.IsNullOrWhiteSpace(adminID) ||
                    string.IsNullOrWhiteSpace(applicantID) ||
                    string.IsNullOrWhiteSpace(kind) ||
                    string.IsNullOrWhiteSpace(Year) ||
                    string.IsNullOrWhiteSpace(ts) ||
                    string.IsNullOrWhiteSpace(sig))
                {
                    WriteJsonError(400, "缺少必要參數");
                    return;
                }

                // 2. 驗證簽章
                if (!JSonHelper.ValidateSignature(env, ts, sig, invoiceNo, orderID, adminID, applicantID, kind))
                {
                    WriteJsonError(401, "簽章驗證失敗");
                    return;
                }

                // 3. 防止重放攻擊：檢查時間戳僅允許 ±5 分鐘內
                if (!JSonHelper.ValidateTimestamp(ts, 300))
                {
                    WriteJsonError(408, "時間戳已過期");
                    return;
                }

                // 4. 進入原本流程
                    ProcessRequest();
            }
        }

        /// <summary>
        /// 主流程：
        /// 1. 取得相對應的購買人資料
        /// 2. 設定折讓單編號
        /// 3. 查詢折讓單狀態  
        /// 4. 組合折讓單資料  
        /// 5. 驗證「累積 + 本次」不超過原發票
        /// 6. 呼叫共用發票處理器
        /// 7. 回傳 JSON  
        /// </summary>
        private void ProcessRequest()
        {
            bool lockTaken = false;

            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
            string invoiceNo = Request["invoiceNumber"];
            string orderID = Request["oid"];
            string adminID = Request["a"];
            string applicantID = Request["aid"];
            string kind = Request["kind"];
            string Year = Request["y"];
            string msg = string.Empty;

            // 取得 ServiceIDlist 參數，假設它是以逗號分隔的字串
            string serviceIDListStr = Request["ServiceIDlist"];

            // 轉換 adminID、applicantID 和 kind 為 int 並驗證是否大於 0
            int adminIDInt, applicantIDInt, kindInt;
            if (!int.TryParse(adminID, out adminIDInt) || adminIDInt <= 0 ||
                !int.TryParse(applicantID, out applicantIDInt) || applicantIDInt <= 0 ||
                !int.TryParse(kind, out kindInt) || kindInt <= 0)
            {
                msg = $"缺少或無效的參數：a:{adminID}, aid:{applicantID}, kind:{kind}";
                basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // Year 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(Year))
            {
                msg = $"缺少必要參數：y:{Year}";
                basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // invoiceNo 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                msg = $"缺少必要參數：invoiceNumber:{invoiceNo}";
                basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // orderID 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(orderID))
            {
                msg = $"缺少必要參數：OrderID:{orderID}";
                basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            List<int> serviceIDList = new List<int>();
            if (!string.IsNullOrWhiteSpace(serviceIDListStr))
            {
                // 拆解 ServiceIDlist 並轉換為整數列表
                serviceIDList = serviceIDListStr.Split(',').Select(id => int.TryParse(id, out int result) ? result : 0).Where(id => id > 0).ToList();
            }

            try
            {
                lock (_globalAllowanceLock)
                {
                    if (_allowanceProcessing.Contains(orderID))
                    {
                        WriteJsonError(409, "此訂單折讓單正在處理中，請稍後再試");
                        return;
                    }

                    _allowanceProcessing.Add(orderID);
                    lockTaken = true;
                }

                LightDAC objLightDAC = new LightDAC(this);
                DateTime dtNow = LightDAC.GetTaipeiNow();

                // 1. 取得相對應的購買人資料
                DataTable dtData = objLightDAC.GetApplicantInfo(applicantIDInt, adminIDInt, kindInt, Year);

                if (dtData.Rows.Count == 0)
                {
                    msg = $"查詢購買人資料失敗。";
                    basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                DataRow allowanceRow = dtData.Rows[0]; // 只取代表開發票的那一筆（通常是第一筆）

                if (allowanceRow["InvoiceNumber"].ToString() != invoiceNo)
                {
                    msg = $"參數與訂單內的發票號碼不匹配。";
                    basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                if (allowanceRow["OrderID"].ToString() != orderID)
                {
                    msg = $"參數與訂單內的訂單編號不匹配。";
                    basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 初始化金額合計
                decimal totalAmount = 0;

                // 取得訂單總金額
                decimal.TryParse(allowanceRow["Amount"].ToString(), out totalAmount);

                // 計算營業稅額
                Decimal invoiceSales = Math.Round(totalAmount / 1.05m, 0);       // 未稅
                Decimal invoiceTax = Math.Round(totalAmount - invoiceSales, 0);   // 稅額

                var amountContext = new AllowanceAmountContext(
                    invoiceSales,
                    invoiceTax
                );

                var usageContext = new AllowanceUsageContext(
                    totalItemCount: dtData.Rows.Count
                );

                // 2. 設定折讓單編號 allowanceNo=OrderID前12碼+n (n=第幾筆折讓單。預設為1)。 ex:202512281448 + 1
                if (!ResolveAllowanceSequence(orderID, amountContext, usageContext, out string allowanceNo, out msg))
                {
                    basePage.SaveErrorLog(msg);
                    WriteJsonError(400, msg);
                    return;
                }

                bool isLastAllowance = usageContext.IsLastAllowanceAfter(serviceIDList.Count);

                // 3. 查詢發票狀態
                if (!CheckedInvoiceStatus(invoiceNo, out msg))
                {
                    basePage.SaveErrorLog(msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 4. 組合折讓單資料
                if (!BuildAllowanceItems(this, adminIDInt, applicantIDInt, kindInt, serviceIDList, allowanceNo, allowanceRow, dtData, isLastAllowance, amountContext, out var input, out msg))
                {
                    basePage.SaveErrorLog(msg);
                    WriteJsonError(400, msg);
                    return;
                }

                decimal currentSales = input.Items.Sum(x => x.UnitPrice);
                decimal currentTax = input.Items.Sum(x => x.Tax);

                // 5. 驗證「累積 + 本次」不超過原發票
                if (!amountContext.TryApplyAllowance(currentSales, currentTax, out string err))
                {
                    msg = $"折讓累積金額已超過原發票金額：" + err;
                    basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 6. 呼叫共用發票處理器
                var createDto = InvoiceProcessor.ProcessAllowance(input);

                if (!createDto.Success)
                {
                    msg = $"開立失敗：" + createDto.ErrorMessage;
                    basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 7. 回傳成功結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(new
                {
                    success = true,
                    allowanceNumber = allowanceNo,
                    data = createDto
                }, Formatting.None));
            }
            catch (Exception ex)
            {
                //WriteJsonError(500, "伺服器內部錯誤：" + ex.Message);
                Exception inner = ex.InnerException ?? ex;

                // 取出內部最底層例外訊息
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                string detailedError = ErrorLogger.FormatError(ex, typeof(AddAllowance).FullName);
                basePage.SaveErrorLog("AddAllowance.ProcessRequest：\r\n" + detailedError);

                WriteJsonError(500, $"內部錯誤：{inner.Message}");
            }
            finally
            {
                if (lockTaken)
                {
                    lock (_globalAllowanceLock)
                    {
                        _allowanceProcessing.Remove(orderID);
                    }
                }
            }
        }

        /// <summary>
        /// 解析本次折讓序列，
        /// 沿著折讓單編號序列（OrderID 前 12 碼 + n）
        /// 依序檢查折讓單狀態，
        /// 同步累積歷史折讓金額，
        /// 回傳下一個可用的折讓單編號，
        /// 並判斷是否為最後一次折讓。
        /// </summary>
        /// <param name="orderID"> 訂單編號（使用前 12 碼作為折讓單編號前綴）</param>
        /// <param name="amountContext"> 折讓金額累積內容，會在本方法中依序加入「歷史折讓金額」，以便後續驗證本次折讓是否超過原發票 </param>
        /// <param name="usageContext"> 折讓訂單數量累積內容，會在本方法中依序加入「歷史折讓數量」，以便後續驗證是不是最後一筆折讓單資料 </param>
        /// <param name="allowanceNo">輸出：可用的折讓單編號</param>
        /// <param name="msg">輸出：錯誤訊息；成功時為空字串</param>
        /// <returns>
        /// true：
        ///   成功找到可用的折讓單編號，
        ///   且歷史折讓金額累積正常  
        /// false：
        ///   發生錯誤、折讓金額異常，
        ///   或已超出可折讓次數
        /// </returns>
        /// <remarks>
        /// 本方法會：
        /// 1. 依序嘗試折讓單編號（OrderID + 1..n）
        /// 2. 若折讓單不存在（NOT_FOUND）→ 視為可用
        /// 3. 若折讓單已存在且成功（D0401 / 99）→
        ///    查詢其金額並累積至 <paramref name="amountContext"/>
        /// 4. 若折讓單狀態不合法 → 立即回傳錯誤
        ///
        /// 注意：
        /// 本方法同時負責「編號解析」與「歷史折讓金額累積」，
        /// 這是因應廠商 API 僅能以折讓單編號查詢金額的限制。
        /// </remarks>
        private bool ResolveAllowanceSequence(string orderID, AllowanceAmountContext amountContext, AllowanceUsageContext usageContext, out string allowanceNo, out string msg)
        {
            allowanceNo = string.Empty;
            msg = string.Empty;
            string allowanceType = string.Empty;

            string prefix = orderID.Substring(0, 12);

            // 🔒 安全上限，避免無限 loop（可記錄 log）
            const int MAX_TRY = 9999;

            for (int index = 1; index <= MAX_TRY; index++)
            {
                string candidateNo = prefix + index;

                // 1️. 檢查折讓單狀態
                if (CheckedAllowanceStatus(candidateNo, out msg, out allowanceType))
                {
                    // NOT_FOUND → 此編號可使用
                    allowanceNo = candidateNo;
                    return true;
                }

                // 2️. msg 為空代表「已存在且可嘗試下一筆」
                if (string.IsNullOrWhiteSpace(msg))
                {
                    if (allowanceType == "D0401")
                    {
                        // 讀取歷史折讓，累積進 Context
                        var queryDto_allowance = GetAllowanceOrder(candidateNo, out msg);

                        if (!queryDto_allowance.Success)
                        {
                            msg = "查詢歷史折讓單失敗";
                            return false;
                        }

                        foreach (var his in queryDto_allowance.Data)
                        {
                            decimal hisSales = decimal.Parse(his.Total_amount);
                            decimal hisTax = decimal.Parse(his.Tax_amount);

                            // 金額累積
                            if (!amountContext.TryApplyAllowance(hisSales, hisTax, out string err))
                            {
                                msg = $"歷史折讓金額異常，已超過原發票金額{err}";
                                return false;
                            }

                            // 新增歷史資料項目數累積
                            usageContext.ApplyHistoricalItems(his.Product_item.Count);
                        }
                    }

                    // 繼續嘗試下一個編號
                    continue;
                }

                // 3. 有錯誤訊息 → 直接中止
                msg = !string.IsNullOrWhiteSpace(msg) ? msg + $" OrderID: {orderID}, 折讓單編號: {candidateNo}" : msg;
                return false;
            }

            msg = "折讓次數已達上限，無可用折讓單編號";
            return false;
        }

        /// <summary>
        /// 計算本次折讓「實際應使用」的未稅與稅額。
        /// 若為最後一筆折讓，則自動改用剩餘可折讓金額補齊，
        /// 避免因四捨五入導致總額或稅額超過原發票。
        /// </summary>
        /// <param name="requestedSalesAmount"> 本次原本希望折讓的未稅金額（依商品金額比例計算） </param>
        /// <param name="requestedTaxAmount"> 本次原本希望折讓的營業稅額（依商品金額比例計算） </param>
        /// <param name="isLastItem"> 是否為本次折讓的最後一筆明細 </param>
        /// <param name="amountContext"> 折讓金額累積 Context，內含原始與剩餘金額 </param>
        /// <param name="finalSalesAmount">
        /// 輸出：本次實際應折讓的未稅金額
        /// </param>
        /// <param name="finalTaxAmount">
        /// 輸出：本次實際應折讓的營業稅額
        /// </param>
        public static void ResolveAllowanceRequestAmount(decimal requestedSalesAmount, decimal requestedTaxAmount, bool isLastItem, AllowanceAmountContext amountContext, out decimal finalSalesAmount, out decimal finalTaxAmount)
        {
            if (isLastItem)
            {
                // 🔑 最後一筆：直接補齊剩餘金額
                finalSalesAmount = amountContext.RemainingSalesAmount;
                finalTaxAmount = amountContext.RemainingTaxAmount;
                return;
            }

            // 前面筆數：依比例計算
            finalSalesAmount = requestedSalesAmount;
            finalTaxAmount = requestedTaxAmount;
        }

        /// <summary>
        /// 組合折讓單資料，依據訂單內容產生折讓單明細與輸入資料。
        /// </summary>
        /// <param name="basePage">BasePage，用於取得環境與共用方法</param>
        /// <param name="adminIDInt">宮廟編號</param>
        /// <param name="applicantIDInt">購買人編號</param>
        /// <param name="kindInt">服務類型（點燈、普渡、法會等）</param>
        /// <param name="serviceIDList">服務類型編號陣列(用來放部分折讓的編號)</param>
        /// <param name="allowanceNo">折讓單編號</param>
        /// <param name="allowanceRow">代表發票的訂單資料列</param>
        /// <param name="dtData">該訂單的完整資料表</param>
        /// <param name="isLastAllowance">是否為最後一次折讓</param>
        /// <param name="amountContext">
        /// 折讓金額累積內容，
        /// 會在本方法中驗證本次折讓是否超過原發票
        /// </param>
        /// <param name="input">
        /// 輸出折讓單開立所需的 <see cref="AllowanceWrapperInput"/>；
        /// 成功時回傳完整資料，失敗時為空物件
        /// </param>
        /// <param name="msg">錯誤訊息；成功時為空字串</param>
        /// <returns>
        /// true 表示組合成功；false 表示失敗，並於 <paramref name="msg"/> 提供原因
        /// </returns>
        /// <remarks>
        /// 本方法支援：
        /// 1. 單項商品折讓（依據服務項目逐筆組合）
        /// 2. 全額折讓（僅產生一筆折讓明細）
        ///
        /// 本方法僅負責資料組合與驗證，不進行 API 呼叫。
        /// </remarks>
        private bool BuildAllowanceItems(BasePage basePage, int adminIDInt, int applicantIDInt, int kindInt, List<int> serviceIDList, string allowanceNo, DataRow allowanceRow, DataTable dtData, bool isLastAllowance, AllowanceAmountContext amountContext, out AllowanceWrapperInput input, out string msg)
        {
            msg = string.Empty;
            input = new AllowanceWrapperInput { };
            DateTime dtNow = LightDAC.GetTaipeiNow();

            string templeName = TempleHelper.GetTempleName(adminIDInt, basePage);

            // 組發票商品項目（你可以根據多筆資料彙總計算）
            List<ProductItem_Allowance> items_allowance = new List<ProductItem_Allowance>();

            foreach (DataRow row in dtData.Rows)
            {
                // 根據 Kind 判斷對應的 ServiceID 和 ServiceString
                string serviceID = string.Empty;
                string serviceString = string.Empty;
                switch (kindInt)
                {
                    case 1:  // 點燈
                        serviceID = row["LightsID"].ToString();
                        serviceString = row["LightsString"].ToString();
                        break;
                    case 2:  // 普渡
                        serviceID = row["PurdueID"].ToString();
                        serviceString = row["PurdueString"].ToString();
                        break;
                    case 3:  // 文創商品
                        serviceID = row["BuyID"].ToString();
                        serviceString = row["Num2String"].ToString();
                        break;
                    case 4:  // 下元補庫
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 5:  // 呈疏補庫
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 6:  // 企業補財庫
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 7:  // 天赦日補運
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 8:  // 天赦日祭改
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 9:  // 關聖帝君聖誕
                        serviceID = row["EmperorGuanshengID"].ToString();
                        serviceString = row["EmperorGuanshengString"].ToString();
                        break;
                    case 10: // 代燒金紙
                        serviceID = row["BPOID"].ToString();
                        serviceString = row["BPOString"].ToString();
                        break;
                    case 11: // 天貺納福添運法會
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 12: // 靈寶禮斗
                        serviceID = row["LingbaolidouID"].ToString();
                        serviceString = row["LingbaolidouString"].ToString();
                        break;
                    case 13: // 七朝清醮
                        serviceID = row["TaoistJiaoCeremonyID"].ToString();
                        serviceString = row["TaoistJiaoCeremonyString"].ToString();
                        break;
                    case 14: // 九九重陽天赦日補運
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 15: // 護國息災梁皇大法會
                        serviceID = row["LybcID"].ToString();
                        serviceString = row["LybcString"].ToString();
                        break;
                    case 16: // 補財庫
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 17: // 赦罪補庫
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 18: // 天公生招財補運
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 19: // 供香轉運
                        serviceID = row["SuppliesID"].ToString();
                        serviceString = row["SuppliesString"].ToString();
                        break;
                    case 20: // 安斗
                        serviceID = row["AnDouID"].ToString();
                        serviceString = row["AnDouString"].ToString();
                        break;
                    case 21: // 供花供果
                        serviceID = row["HuaguoID"].ToString();
                        serviceString = row["HuaguoString"].ToString();
                        break;
                    case 22: // 孝親祈福燈
                        serviceID = row["LightsID"].ToString();
                        serviceString = row["LightsString"].ToString();
                        break;
                    case 23: // 祈安植福
                        serviceID = row["BlessingID"].ToString();
                        serviceString = row["BlessingString"].ToString();
                        break;
                    case 24: // 祈安禮斗
                    case 25: // 千手觀音千燈迎佛法會
                        serviceID = row["QnLightID"].ToString();
                        serviceString = row["QnLightString"].ToString();
                        break;
                    case 26: // 組合商品
                    case 27: // 新春賀歲感恩招財祿位
                        serviceID = row["LuckaltarID"].ToString();
                        serviceString = row["LuckaltarString"].ToString();
                        break;
                }

                int.TryParse(row["Num"].ToString(), out int num);
                string numString = row["Num2String"].ToString();

                // 檢查發票更新日期是否存在
                if (DateTime.TryParse(row["UpdateDate"].ToString(), out DateTime updateDate))
                {
                    // 成功解析日期，將其轉換為 yyyyMMdd 格式
                    int originalInvoiceDate = int.Parse(updateDate.ToString("yyyyMMdd"));

                    // 根據 服務類型編號陣列 是否存在，判斷是否為單項商品折讓或全額折讓
                    if (serviceIDList.Count > 0)
                    {
                        // 檢查 ServiceID 是否有在 服務類型編號陣列 內
                        if (int.TryParse(serviceID, out int sid) && serviceIDList.Contains(sid) && !string.IsNullOrWhiteSpace(numString) && num > 0)
                        {
                            // 取得單項商品金額及數量
                            decimal item_UnitPrice = decimal.TryParse(row["Cost"].ToString(), out decimal item_cost) ? item_cost : 0;
                            decimal item_Quantity = decimal.TryParse(row["Count"].ToString(), out decimal item_count) ? item_count : 1;

                            // 當處理商品折讓時，檢查單項金額是否有效
                            if (item_UnitPrice <= 0)
                            {
                                msg = $"AddAllowance.BuildAllowanceItems：\r\n單項商品金額無效：{item_UnitPrice}";
                                return false;
                            }

                            // 計算單項商品營業稅額
                            Decimal item_SalesAmount = Math.Round(item_UnitPrice / 1.05m, 0);            // 未稅
                            Decimal item_taxAmount = Math.Round(item_UnitPrice - item_SalesAmount, 0);   // 稅額

                            if (item_SalesAmount <= 0 && item_taxAmount <= 0)
                            {
                                msg = $"AddAllowance.BuildAllowanceItems：\r\n計算單項商品營業稅額失敗或，單項商品的稅額無效，SalesAmount:（{item_SalesAmount}），TaxAmount:（{item_taxAmount}）。";
                                return false;
                            }

                            ResolveAllowanceRequestAmount(
                                item_SalesAmount,
                                item_taxAmount,
                                isLastAllowance,
                                amountContext,
                                out var reqSales,
                                out var reqTax
                            );

                            if (!amountContext.TryResolveNextAllowanceAmount(
                                reqSales,
                                reqTax,
                                out var finalSales,
                                out var finalTax,
                                out msg))
                            {
                                return false;
                            }

                            // 生成單項商品折讓
                            items_allowance.Add(new ProductItem_Allowance
                            {
                                OriginalInvoiceNumber = row["InvoiceNumber"].ToString(),
                                OriginalInvoiceDate = originalInvoiceDate,
                                OriginalDescription = serviceString,
                                Quantity = 1, // Quantity 固定為 1：目前折讓邏輯以「金額」為單位，不處理數量拆分
                                UnitPrice = finalSales,
                                Amount = finalSales,
                                Tax = finalTax,
                                TaxType = 1
                            });

                            if (items_allowance.Count == serviceIDList.Count) break;
                        }
                        else
                        {
                            string serviceIdListText = serviceIDList == null
                                ? "null"
                                : string.Join(",", serviceIDList);

                            if (string.IsNullOrWhiteSpace(msg))
                            {
                                msg = $"AddAllowance.BuildAllowanceItems 服務類型編號陣列比對：\r\n［serviceIDList:({serviceIdListText}), ServiceID: {serviceID}］";
                            }
                            else
                            {
                                string error = num == 0 && string.IsNullOrWhiteSpace(numString) ? "(已退款)" : "";
                                msg += $"\r\n［serviceIDList:({serviceIdListText}), ServiceID: {serviceID}{error}］";
                            }
                        }
                    }
                    else
                    {
                        if (!amountContext.TryResolveNextAllowanceAmount(
                            amountContext.RemainingSalesAmount,
                            amountContext.RemainingTaxAmount,
                            out var finalSales,
                            out var finalTax,
                            out msg))
                        {
                            return false;
                        }

                        // 沒有匹配的 ServiceID，生成全額折讓
                        items_allowance.Add(new ProductItem_Allowance
                        {
                            OriginalInvoiceNumber = row["InvoiceNumber"].ToString(),
                            OriginalInvoiceDate = originalInvoiceDate,
                            OriginalDescription = "線上服務費折讓",
                            Quantity = 1, // Quantity 固定為 1：目前折讓邏輯以「金額」為單位，不處理數量拆分
                            UnitPrice = finalSales,
                            Amount = finalSales,
                            Tax = finalTax,
                            TaxType = 1
                        });
                        break;
                    }
                }
                else
                {
                    // 無法解析有效的日期，這裡可以處理錯誤或跳過
                    msg = $"AddAllowance.BuildAllowanceItems：\r\n發票 UpdateDate 格式無效。";
                    return false;
                }
            }

            // 處理購買人聯絡資訊 fallback
            string buyerName = !string.IsNullOrEmpty(allowanceRow["AppName"]?.ToString())
                ? allowanceRow["AppName"].ToString()
                : (!string.IsNullOrEmpty(allowanceRow["Name"]?.ToString())
                    ? allowanceRow["Name"].ToString()
                    : "");

            string buyerMobile = !string.IsNullOrEmpty(allowanceRow["AppMobile"]?.ToString())
                ? allowanceRow["AppMobile"].ToString()
                : (!string.IsNullOrEmpty(allowanceRow["Mobile"]?.ToString())
                    ? allowanceRow["Mobile"].ToString()
                    : "");

            string buyerEmail = !string.IsNullOrEmpty(allowanceRow["AppEmail"]?.ToString())
                ? allowanceRow["AppEmail"].ToString()
                : (!string.IsNullOrEmpty(allowanceRow["Email"]?.ToString())
                    ? allowanceRow["Email"].ToString()
                    : "");

            if (items_allowance.Count == 0)
            {
                if(string.IsNullOrWhiteSpace(msg))
                    msg = $"AddAllowance.BuildAllowanceItems：\r\n組合商品明細失敗。";
                return false;
            }

            decimal currentSales = items_allowance.Sum(x => x.Amount);
            decimal currentTax = items_allowance.Sum(x => x.Tax);

            // 組折讓單輸入資料
            input = new AllowanceWrapperInput
            {
                AllowanceNumber = allowanceNo,
                AllowanceDate = dtNow.ToString("yyyyMMdd"),
                AllowanceType = 2,
                BuyerIdentifier = allowanceRow["BuyerIdentifier"]?.ToString() ?? "0000000000",
                BuyerName = string.IsNullOrEmpty(allowanceRow["BuyerName"]?.ToString()) ? buyerName : allowanceRow["BuyerName"].ToString(),
                BuyerAddress = "",
                BuyerTelephoneNumber = buyerMobile,
                BuyerEmailAddress = buyerEmail,
                Items = items_allowance,
                TotalAmount = currentSales,
                TaxAmount = currentTax
            };

            return true;
        }

        /// <summary>
        /// 檢查發票狀態是否允許開立折讓單。
        /// </summary>
        /// <param name="invoiceNo">發票號碼</param>
        /// <param name="msg">
        /// 回傳錯誤訊息；
        /// 若檢查通過則為空字串
        /// </param>
        /// <returns>
        /// true：發票已開立 (C0401) 且狀態成功 (99)，可進行折讓  
        /// false：發票狀態不符或查詢失敗
        /// </returns>
        private bool CheckedInvoiceStatus(string invoiceNo, out string msg)
        {
            msg = string.Empty;

            var statusJson = Task.Run(() =>
                _service_invoice_status.QueryStatusAsync(invoiceNo)).Result;

            var statusDto = JObject.Parse(statusJson).ToInvoiceStatusResponseDto();


            if (!statusDto.Success)
            {
                msg = $"AddAllowance.ProcessRequest：\r\n查詢失敗：{statusDto.ErrorMessage}";
                return false;
            }

            // 必須是發票已開立 (C0401) 且發票狀態已完成 (99)
            if (statusDto.Data[0].Type != "C0401" || statusDto.Data[0].Status != "99")
            {
                msg = $"AddAllowance.ProcessRequest：\r\n發票類型（{statusDto.Data[0].Type}）及發票狀態（{statusDto.Data[0].Status}）不允許作廢。";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 檢查指定折讓單編號是否可用。
        /// </summary>
        /// <param name="allowanceNo">欲檢查的折讓單編號</param>
        /// <param name="msg">
        /// 錯誤訊息；
        /// 若為空字串表示該折讓單編號「已存在但可嘗試下一筆」
        /// </param>
        /// <returns>
        /// true：查無折讓單 (NOT_FOUND / -1)，可使用此編號  
        /// false：
        /// - 折讓單已存在且不可使用  
        /// - 查詢失敗
        /// </returns>
        private bool CheckedAllowanceStatus(string allowanceNo, out string msg, out string allowanceType)
        {
            msg = string.Empty;
            allowanceType = string.Empty;

            var statusJson_allowance = Task.Run(() =>
                _service_allowance_status.QueryAllowanceStatusAsync(allowanceNo)).Result;

            var statusDto_allowance = JObject.Parse(statusJson_allowance).ToAllowanceStatusResponseDto();

            if (!statusDto_allowance.Success)
            {
                msg = $"AddAllowance.CheckedAllowanceStatus：\r\n查詢失敗：" + statusDto_allowance.ErrorMessage;
                return false;
            }

            // 可使用：查無折讓單
            if (statusDto_allowance.Data[0].Type == "NOT_FOUND" && statusDto_allowance.Data[0].Status == "-1")
            {
                return true;
            }

            // 已存在且成功 → 嘗試下一筆（不視為錯誤）
            if (statusDto_allowance.Data[0].Type == "D0401" && statusDto_allowance.Data[0].Status == "99")
            {
                allowanceType = statusDto_allowance.Data[0].Type;
                return false;
            }
            else if (statusDto_allowance.Data[0].Type == "D0501" && statusDto_allowance.Data[0].Status == "99")
            {
                // 已作廢且成功 → 嘗試下一筆（不視為錯誤）
                allowanceType = statusDto_allowance.Data[0].Type;
                return false;
            }

            msg = $"AddAllowance.CheckedAllowanceStatus：\r\n折讓單類型（{statusDto_allowance.Data[0].Type}）及折讓單狀態（{statusDto_allowance.Data[0].Status}）不允許開立。";
            return false;
        }

        /// <summary>
        /// 取得指定折讓單編號資訊。
        /// </summary>
        /// <param name="allowanceNo">欲查詢的折讓單編號</param>
        /// <param name="msg">
        /// 錯誤訊息；
        /// </param>
        /// <returns>
        /// 返回折讓單查詢結果
        /// </returns>
        private AllowanceQueryResponseDto GetAllowanceOrder(string allowanceNo, out string msg)
        {
            msg = string.Empty;

            var queryJson_allowance = Task.Run(() =>
                _service_allowance_query.QueryAllowanceNumberAsync(allowanceNo)).Result;

            var queryDto_allowance = JObject.Parse(queryJson_allowance).ToAllowanceQueryResponseDto();

            return queryDto_allowance;
        }

        /// <summary>
        /// 回傳錯誤 JSON（格式：{ code:xxx, msg:yyy }）並設定 HTTP StatusCode
        /// </summary>
        private void WriteJsonError(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";
            //var err = new { code = statusCode, msg = message };
            // Debug 模式下可印出 Exception stack trace
            var err = new
            {
                success = false,
                code = statusCode.ToString(),
                msg = message
            };
            Response.Write(JsonConvert.SerializeObject(err, Formatting.None));
        }
    }
}