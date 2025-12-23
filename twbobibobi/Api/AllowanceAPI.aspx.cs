/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceAPI.cs
   類別說明：提供電子發票折讓單建立 API（WebForms API）
   建立日期：2025-12-10
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
    /// WebForms API 頁面：處理 /Api/AllowanceAPI.aspx POST 請求
    /// </summary>
    public partial class AllowanceAPI : AjaxBasePage
    {
        // 假設從 query string 判斷是否為測試模式
        static string env = HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";

        // 1. 建立服務實例
        //    可改為透過 DI 容器注入
        IInvoiceService _service = AllowanceServiceFactory.Create(GetEnvironment());

        private static string GetEnvironment()
        {
            return HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";
        }

        /// <summary>
        /// Page Load 事件：僅接受 POST
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod != "POST")
                {
                    Response.StatusCode = 405; // Method Not Allowed
                    Response.End();
                }
                ProcessRequest();
            }
        }

        /// <summary>
        /// 處理 POST 請求：讀取 JSON、驗證、呼叫服務、回傳 JSON
        /// </summary>
        private void ProcessRequest()
        {
            twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();

            // 2. 讀取 request body
            string body;
            using (var reader = new StreamReader(Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            CreateAllowanceDto dto;
            try
            {
                // 3. 反序列化成 DTO
                dto = JsonConvert.DeserializeObject<CreateAllowanceDto>(body);
            }
            catch (Exception)
            {
                basePage.SaveErrorLog("AllowanceAPI.ProcessRequest(無法解析 JSON 格式");
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 4. 驗證必要欄位
            if (dto == null || dto.ProductItem == null || dto.ProductItem.Count == 0
                || string.IsNullOrWhiteSpace(dto.BuyerIdentifier)
                || string.IsNullOrWhiteSpace(dto.BuyerName))
            {
                basePage.SaveErrorLog("AllowanceAPI.ProcessRequest(缺少必要參數：Items, BuyerIdentifier, BuyerName");
                WriteJsonError(400, "缺少必要參數：Items, BuyerIdentifier, BuyerName");
                return;
            }

            try
            {
                // 5. 呼叫業務邏輯，改用強型別回傳
                var result = Task.Run(() => _service.CreateAllowanceAsync(dto)).Result;

                //JObject result = _service.CreateAllowanceAsync(dto).GetAwaiter().GetResult();

                // 6. 回傳成功 JSON
                // 包成統一格式
                var output = new
                {
                    success = result.Success,
                    code = result.Code,
                    msg = result.Success ? "折讓單開立成功" : result.ErrorMessage,
                    data = result
                };

                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (ArgumentException error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceAPI).FullName);
                basePage.SaveErrorLog("AllowanceAPI.ProcessRequest(回傳參數驗證錯誤)：\r\n" + detailedError);
                // 7. 回傳參數驗證錯誤
                WriteJsonError(400, error.Message);
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AllowanceAPI).FullName);
                basePage.SaveErrorLog("AllowanceAPI.ProcessRequest(伺服器內部錯誤)：\r\n" + detailedError);
                // 8. 回傳通用伺服器錯誤
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
        /// 將錯誤訊息以 JSON 格式回傳
        /// </summary>
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