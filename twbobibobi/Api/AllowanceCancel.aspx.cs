/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceCancel.aspx.cs
   類別說明：WebForms API 頁面，處理 /Api/AllowanceCancel.aspx POST 請求，實現折讓單作廢功能
   建立日期：2025-12-18
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/AllowanceCancel.aspx POST 請求，提供折讓單作廢功能。
    /// </summary>
    /// <remarks>
    /// 此頁面負責處理來自 API 的折讓單作廢請求。它會接收請求，解析 JSON 內容，並調用折讓單作廢服務。
    /// 若作廢成功，會返回成功的結果；若有錯誤，則返回相應的錯誤訊息。
    /// </remarks>
    public partial class AllowanceCancel : AjaxBasePage
    {
        /// <summary>折讓單作廢服務</summary>
        IInvoiceCancelService _service = AllowanceServiceFactory.CreateCancelService(GetEnvironment());

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
        /// 處理折讓單作廢請求：
        /// 1. 解析 JSON 並檢查必要參數。
        /// 2. 呼叫折讓單作廢服務。
        /// 3. 回傳結果或錯誤訊息。
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
                // 解析請求中的 JSON
                var obj = JsonConvert.DeserializeObject<JObject>(body);
                allowanceNumber = obj?.Value<string>("CancelAllowanceNumber");
            }
            catch
            {
                // 解析 JSON 失敗，記錄錯誤
                basePage.SaveErrorLog("AllowanceCancel.ProcessRequest(無法解析 JSON 格式)");
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 檢查折讓單號碼是否存在
            if (string.IsNullOrWhiteSpace(allowanceNumber))
            {
                basePage.SaveErrorLog("AllowanceCancel.ProcessRequest(請提供 CancelAllowanceNumber)");
                WriteJsonError(400, "請提供 CancelAllowanceNumber");
                return;
            }

            try
            {
                // 呼叫折讓單作廢服務
                var raw = Task.Run(() => _service.CancelAllowanceAsync(allowanceNumber)).Result;
                var dto = JObject.Parse(raw).ToCancelAllowanceResponseDto(allowanceNumber);

                // 構造回應
                var output = new
                {
                    success = dto.Success,
                    code = dto.Code,
                    msg = dto.Success ? "作廢成功" : dto.ErrorMessage,
                    data = dto
                };

                // 回傳 JSON 結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (Exception error)
            {
                // 捕捉並記錄錯誤
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceAPI).FullName);
                basePage.SaveErrorLog("AllowanceCancel.ProcessRequest(伺服器內部錯誤)：\r\n" + detailedError);
                WriteJsonError(500, "伺服器內部錯誤");
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