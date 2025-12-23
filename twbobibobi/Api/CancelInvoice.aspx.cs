/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：CancelInvoice.cs
   類別說明：提供電子發票作廢 API（WebForms API）
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using Temple.FET.APITEST;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// /Api/CancelInvoice.aspx?invoiceNumber=XXX&env=YYY API 頁面  
    /// 功能：依據 invoiceNumber 作廢發票，並支援 UAT / Prod 環境切換、簽章驗證、防重放攻擊
    /// </summary>
    /// <remarks>
    /// 這個頁面處理發票作廢請求，包含驗證參數、簽章、時間戳以及發票狀態檢查，最後呼叫作廢 API 並回傳結果。
    /// </remarks>
    public partial class CancelInvoice : AjaxBasePage
    {
        /// <summary>依據 QueryString env 決定環境設定後綴（_UAT / _Prod）</summary>
        static string EnvSuffix = HttpContext.Current.Request["env"] == "uat" ? "_UAT" : "_Prod";

        /// <summary>發票查詢服務</summary>
        IInvoiceStatusService _service_status = InvoiceServiceFactory.CreateStatusService(GetEnvironment());
        /// <summary>發票作廢建立服務</summary>
        IInvoiceCancelService _service_cancel = InvoiceServiceFactory.CreateCancelService(GetEnvironment());

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

                WriteJsonError(409, "此服務已暫停使用！");
                // 4. 進入原本流程
                //ProcessRequest();
            }
        }

        /// <summary>
        /// 主流程：  
        /// 1. 查詢發票狀態  
        /// 2. 驗證發票類型-C0401(發票開立)及發票狀態-99(已完成)
        /// 3. 呼叫廠商作廢 API  
        /// 4. 回傳 JSON  
        /// </summary>
        private void ProcessRequest()
        {
            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
            string invoiceNo = Request["invoiceNumber"];
            string orderID = Request["oid"];
            string adminID = Request["a"];
            string applicantID = Request["aid"];
            string kind = Request["kind"];
            string Year = Request["y"];

            // 轉換 adminID、applicantID 和 kind 為 int 並驗證是否大於 0
            int adminIDInt, applicantIDInt, kindInt;
            if (!int.TryParse(adminID, out adminIDInt) || adminIDInt <= 0 ||
                !int.TryParse(applicantID, out applicantIDInt) || applicantIDInt <= 0 ||
                !int.TryParse(kind, out kindInt) || kindInt <= 0)
            {
                string msg = $"缺少或無效的參數：a:{adminID}, aid:{applicantID}, kind:{kind}";
                basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // Year 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(Year))
            {
                string msg = $"缺少必要參數：y:{Year}";
                basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // invoiceNo 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                string msg = $"缺少必要參數：invoiceNumber:{invoiceNo}";
                basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            // orderID 仍然保留為 string，但可以在這裡對其進行額外的處理或驗證
            if (string.IsNullOrWhiteSpace(orderID))
            {
                string msg = $"缺少必要參數：OrderID:{orderID}";
                basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                WriteJsonError(400, msg);
                return;
            }

            try
            {
                LightDAC objLightDAC = new LightDAC(this);
                DateTime dtNow = LightDAC.GetTaipeiNow();

                // 1. 取得相對應的購買人資料
                DataTable dtData = objLightDAC.GetApplicantInfo(applicantIDInt, adminIDInt, kindInt, Year);

                if (dtData.Rows.Count == 0)
                {
                    string msg = $"查詢購買人資料失敗。";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                DataRow invoiceRow = dtData.Rows[0]; // 只取代表開發票的那一筆（通常是第一筆）

                if (invoiceRow["InvoiceNumber"].ToString() != invoiceNo)
                {
                    string msg = $"參數與訂單內的發票號碼不匹配。";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                if (invoiceRow["OrderID"].ToString() != orderID)
                {
                    string msg = $"參數與訂單內的訂單編號不匹配。";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 2. 查詢發票狀態
                var statusJson = Task.Run(() =>
                    _service_status.QueryStatusAsync(invoiceNo)).Result;
                var statusDto = JObject.Parse(statusJson).ToInvoiceStatusResponseDto();

                if (!statusDto.Success)
                {
                    string msg = $"查詢失敗：{statusDto.ErrorMessage}";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 3. 必須是發票已開立 (C0401) 且發票狀態已完成 (99)
                if (statusDto.Data[0].Type != "C0401" || statusDto.Data[0].Status != "99")
                {
                    string msg = $"發票類型（{statusDto.Data[0].Type}）及發票狀態（{statusDto.Data[0].Status}）不允許作廢。";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 4. 呼叫作廢 API
                var cancelJson = Task.Run(() =>
                    _service_cancel.CancelInvoiceAsync(invoiceNo)).Result;
                var cancelDto = JObject.Parse(cancelJson).ToCancelInvoiceResponseDto(invoiceNo);

                if (!cancelDto.Success)
                {
                    string msg = $"作廢失敗：{cancelDto.ErrorMessage}";
                    basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + msg);
                    WriteJsonError(400, msg);
                    return;
                }

                // 5. 回傳成功結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(new
                {
                    success = true,
                    invoiceNumber = invoiceNo,
                    data = cancelDto
                }, Formatting.None));
            }
            catch (Exception ex)
            {
                //WriteJsonError(500, "伺服器內部錯誤：" + ex.Message);
                Exception inner = ex.InnerException ?? ex;

                // 取出內部最底層例外訊息
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                string detailedError = ErrorLogger.FormatError(ex, typeof(CancelInvoice).FullName);
                basePage.SaveErrorLog("CancelInvoice.ProcessRequest：\r\n" + detailedError);

                WriteJsonError(500, $"內部錯誤：{inner.Message}");
            }
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