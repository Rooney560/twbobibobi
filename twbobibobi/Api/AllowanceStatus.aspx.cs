/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceStatus.aspx.cs
   類別說明：WebForms API 頁面，處理 /Api/AllowanceStatus.aspx POST 請求，查詢折讓單狀態。
   建立日期：2025-12-17
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Services;
using twbobibobi.Data;
using twbobibobi.Helpers;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面，處理 /Api/AllowanceStatus.aspx POST 請求。
    /// 這個頁面負責接收查詢折讓單狀態的請求，並調用 `IAllowanceStatusService` 服務來查詢折讓單狀態。
    /// </summary>
    /// <remarks>
    /// 這個頁面只接受 POST 請求，當收到請求時，會從請求的 JSON 內容中提取折讓單編號，
    /// 然後調用相應的服務來查詢折讓單狀態，並返回查詢結果。如果出現錯誤，會返回對應的錯誤訊息。
    /// </remarks>
    public partial class AllowanceStatus : AjaxBasePage
    {
        // 注入折讓單查詢服務，依據環境設定選擇 UAT 或 Prod
        IInvoiceStatusService _service = AllowanceServiceFactory.CreateStatusService(GetEnvironment());

        /// <summary>
        /// 根據環境參數決定要使用的 API 環境。
        /// </summary>
        /// <returns>回傳 "_UAT" 或 "_Prod"</returns>
        private static string GetEnvironment()
        {
            return HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";
        }

        /// <summary>
        /// Page Load 事件，僅接受 POST 請求。
        /// 若不是 POST 請求，回傳 405 錯誤並結束處理。
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod != "POST")
                {
                    Response.StatusCode = 405; // Method Not Allowed
                    Response.End(); // 結束處理
                }
                ProcessRequest(); // 處理請求
            }
        }

        /// <summary>
        /// 處理查詢折讓單狀態的請求。
        /// </summary>
        private void ProcessRequest()
        {
            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
            string body;
            using (var reader = new StreamReader(Request.InputStream))
                body = reader.ReadToEnd(); // 讀取請求內容

            string allowanceNumber;
            try
            {
                // 解析 JSON 內容
                var obj = JsonConvert.DeserializeObject<JObject>(body);
                allowanceNumber = obj?.Value<string>("AllowanceNumber");
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceStatus).FullName);
                basePage.SaveErrorLog("無法解析 JSON 格式：\r\n" + detailedError);
                WriteJsonError(400, "無法解析 JSON 格式"); // 錯誤的 JSON 格式
                return;
            }

            // 未提供折讓單編號
            if (string.IsNullOrWhiteSpace(allowanceNumber))
            {
                basePage.SaveErrorLog("AllowanceStatus：\r\n" + "請提供 AllowanceNumber");

                WriteJsonError(400, "請提供 AllowanceNumber"); // 錯誤的 JSON 格式
                return;
            }

            try
            {
                var raw = Task.Run(() => _service.QueryAllowanceStatusAsync(allowanceNumber)).Result;
                var dto = JObject.Parse(raw).ToAllowanceStatusResponseDto();

                // 返回結果
                var output = new
                {
                    success = dto.Success,
                    code = dto.Code,
                    msg = dto.Success ? "查詢成功" : dto.ErrorMessage,
                    data = dto
                };

                // 回傳 JSON 格式結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceStatus).FullName);
                basePage.SaveErrorLog("AllowanceStatus：\r\n" + detailedError);

                WriteJsonError(500, "伺服器內部錯誤"); // 伺服器錯誤
            }
        }

        /// <summary>
        /// 輸出 JSON 格式的錯誤訊息。
        /// </summary>
        /// <param name="statusCode">HTTP 狀態碼</param>
        /// <param name="message">錯誤訊息</param>
        private void WriteJsonError(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";
            var err = new { success = false, code = statusCode.ToString(), msg = message, data = (object)null };
            // Debug 模式下可印出 Exception stack trace
            //var err = new
            //{
            //    success = false,
            //    code = statusCode.ToString(),
            //    msg = message,
            //    data = (object)null
            //};
            Response.Write(JsonConvert.SerializeObject(err, Formatting.None));
        }
    }
}