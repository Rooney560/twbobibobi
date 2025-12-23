/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceAPI.cs
   類別說明：提供電子發票建立 API（WebForms API），處理 /Api/InvoiceAPI.aspx POST 請求，實現開立發票功能
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/InvoiceAPI.aspx POST 請求，提供開立發票功能。
    /// </summary>
    /// <remarks>
    /// 此頁面負責處理來自 API 的發票作廢請求。它會接收請求，解析 JSON 內容，並調用開立發票服務。
    /// 若開立成功，會返回成功的結果；若有錯誤，則返回相應的錯誤訊息。
    /// </remarks>
    public partial class InvoiceAPI : AjaxBasePage
    {
        // 假設從 query string 判斷是否為測試模式
        static string env = HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";

        /// <summary>開立發票服務</summary>
        IInvoiceService _service = InvoiceServiceFactory.Create(GetEnvironment());

        /// <summary>
        /// 根據請求中的 env 參數，決定當前環境是 UAT 還是 Prod。
        /// </summary>
        /// <returns>返回 "_UAT" 或 "_Prod" 以設定服務環境</returns>
        private static string GetEnvironment()
        {
            return HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";
        }

        /// <summary>
        /// Page_Load 事件處理：此頁面僅接受 POST 請求，其他請求會回應 405 錯誤。
        /// </summary>
        /// <param name="sender">事件發送者</param>
        /// <param name="e">事件參數</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod != "POST")
                {
                    Response.StatusCode = 405; // Method Not Allowed
                    Response.End();
                }
                ProcessRequest(); // 處理請求
            }
        }

        /// <summary>
        /// 處理開立發票請求：
        /// 1. 解析 JSON 並檢查必要參數。
        /// 2. 呼叫開立發票服務。
        /// 3. 回傳結果或錯誤訊息。
        /// </summary>
        private void ProcessRequest()
        {
            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();

            string body;
            using (var reader = new StreamReader(Request.InputStream))
            {
                body = reader.ReadToEnd(); // 讀取請求內容
            }

            CreateInvoiceDto dto;
            try
            {
                // 解析請求中的 JSON
                dto = JsonConvert.DeserializeObject<CreateInvoiceDto>(body);
            }
            catch (Exception)
            {
                // 解析 JSON 失敗，記錄錯誤
                basePage.SaveErrorLog("InvoiceAPI.ProcessRequest(無法解析 JSON 格式)");
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 驗證必要欄位
            if (dto == null || dto.Items == null || dto.Items.Count == 0
                || string.IsNullOrWhiteSpace(dto.BuyerIdentifier)
                || string.IsNullOrWhiteSpace(dto.BuyerName))
            {
                basePage.SaveErrorLog("InvoiceAPI.ProcessRequest(缺少必要參數：Scenario, Items, BuyerIdentifier, BuyerName");
                WriteJsonError(400, "缺少必要參數：Scenario, Items, BuyerIdentifier, BuyerName");
                return;
            }

            try
            {
                // 呼叫開立發票服務
                var result = Task.Run(() => _service.CreateInvoiceAsync(dto)).Result;

                // 構造回應
                var output = new
                {
                    success = result.Success,
                    code = result.Code,
                    msg = result.Success ? "發票開立成功" : result.ErrorMessage,
                    data = result
                };

                // 回傳 JSON 結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (ArgumentException error)
            {
                // 回傳參數驗證錯誤
                string detailedError = ErrorLogger.FormatError(error, typeof(InvoiceAPI).FullName);
                basePage.SaveErrorLog("InvoiceAPI.ProcessRequest(回傳參數驗證錯誤)：\r\n" + detailedError);
                WriteJsonError(400, error.Message);
            }
            catch (Exception error)
            {
                // 回傳通用伺服器錯誤
                string detailedError = ErrorLogger.FormatError(error, typeof(InvoiceAPI).FullName);
                basePage.SaveErrorLog("InvoiceAPI.ProcessRequest(伺服器內部錯誤)：\r\n" + detailedError);

                WriteJsonError(500, $"伺服器內部錯誤");
                // 回傳具體例外訊息（僅建議在開發或測試環境使用）
                //Exception inner = ex.InnerException ?? ex;

                //// 取出內部最底層例外訊息
                //while (inner.InnerException != null)
                //    inner = inner.InnerException;

                //WriteJsonError(500, $"內部錯誤：{inner.Message}");
            }
        }

        /// <summary>
        /// 回傳錯誤的 JSON 格式：包含錯誤代碼與訊息。
        /// </summary>
        /// <param name="statusCode">HTTP 狀態碼</param>
        /// <param name="message">錯誤訊息</param>
        private void WriteJsonError(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";
            var errObj = new { code = statusCode, msg = message };
            // Debug 模式下可印出 Exception stack trace
            //var errObj = new
            //{
            //    success = false,
            //    code = statusCode.ToString(),
            //    msg = message,
            //    data = (object)null
            //};
            Response.Write(JsonConvert.SerializeObject(errObj, Formatting.None));
        }
    }
}
