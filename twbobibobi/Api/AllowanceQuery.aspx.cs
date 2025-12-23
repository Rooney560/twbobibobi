/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceQuery.aspx.cs
   類別說明：WebForms API 頁面，處理 /Api/AllowanceQuery.aspx POST 請求，查詢折讓單資訊
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
    /// WebForms API 頁面：處理 /Api/AllowanceQuery.aspx POST 請求，提供查詢折讓單資訊的功能。
    /// </summary>
    /// <remarks>
    /// 這個頁面接收 POST 請求並根據提供的 allowanceNumber 查詢折讓單資訊。
    /// 當查詢成功時，會返回包含折讓單編號、創建時間、隨機碼等資料的結果。
    /// 若請求或資料格式不正確，會返回相應的錯誤訊息。
    /// </remarks>
    public partial class AllowanceQuery : AjaxBasePage
    {
        /// <summary>折讓單查詢服務</summary>
        AllowanceQueryService _service = AllowanceServiceFactory.CreateQueryService(GetEnvironment());

        /// <summary>
        /// 根據請求中的 env 參數決定當前環境是 UAT 還是 Prod。
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
        /// 處理查詢折讓單資訊的請求：
        /// 1. 解析 JSON 並檢查是否有提供必要的參數（orderID 或 allowanceNumber）。
        /// 2. 呼叫折讓單查詢服務。
        /// 3. 回傳查詢結果或錯誤訊息。
        /// </summary>
        private void ProcessRequest()
        {
            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
            string body;
            using (var reader = new StreamReader(Request.InputStream))
                body = reader.ReadToEnd();

            string allowanceNumber = null;

            try
            {
                // 解析請求中的 JSON
                var obj = JsonConvert.DeserializeObject<JObject>(body);
                allowanceNumber = obj?.Value<string>("AllowanceNumber");
            }
            catch
            {
                // 解析 JSON 失敗，記錄錯誤
                basePage.SaveErrorLog("AllowanceQuery.ProcessRequest(無法解析 JSON 格式)");
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 如果參數都沒有，返回錯誤
            if (string.IsNullOrWhiteSpace(allowanceNumber))
            {
                basePage.SaveErrorLog("AllowanceQuery.ProcessRequest(請提供AllowanceNumber)");
                WriteJsonError(400, "請提供AllowanceNumber");
                return;
            }

            try
            {
                // 使用折讓單編號查詢
                string raw = Task.Run(() => _service.QueryAllowanceNumberAsync(allowanceNumber)).Result;
                var dto = JObject.Parse(raw);

                // 取得查詢結果中的 data 部分
                var data = dto["data"] as JObject;
                if (data == null)
                {
                    // 若 data 為 null，表示廠商回應中有錯誤，使用廠商回傳的錯誤訊息
                    var errorCode = dto["code"]?.ToString();
                    var errorMsg = dto["msg"]?.ToString();

                    // 返回廠商的錯誤訊息
                    WriteJsonError(400, $"錯誤代碼：{errorCode}，錯誤訊息：{errorMsg}");
                    return;
                }

                // 構造回應
                var output = new
                {
                    success = true,
                    code = "00000",
                    msg = "查詢成功",
                    data = new
                    {
                        Success = true,
                        ErrorMessage = "",
                        AllowanceNumber = data["allowance_number"],
                        AllowanceDate = data["create_date"],
                        DataJSON = data
                    }
                };

                // 回傳 JSON 結果
                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (Exception error)
            {
                // 回傳通用伺服器錯誤
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceAPI).FullName);
                basePage.SaveErrorLog("AllowanceAPI.ProcessRequest(伺服器內部錯誤)：\r\n" + detailedError);
                WriteJsonError(500, detailedError);
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