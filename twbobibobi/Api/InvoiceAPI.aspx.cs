using twbobibobi.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.ApiClients;
using twbobibobi.Model;
using twbobibobi.Services;
using System.Configuration;
using System.Threading.Tasks;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/InvoiceAPI.aspx POST 請求
    /// </summary>
    public partial class InvoiceAPI : AjaxBasePage
    {
        // 假設從 query string 判斷是否為測試模式
        static string env = HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";

        // 1. 建立服務實例
        //    可改為透過 DI 容器注入
        IInvoiceService _service = InvoiceServiceFactory.Create(GetEnvironment());

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
            // 2. 讀取 request body
            string body;
            using (var reader = new StreamReader(Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            CreateInvoiceDto dto;
            try
            {
                // 3. 反序列化成 DTO
                dto = JsonConvert.DeserializeObject<CreateInvoiceDto>(body);
            }
            catch (Exception)
            {
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 4. 驗證必要欄位
            if (dto == null || dto.Items == null || dto.Items.Count == 0
                || string.IsNullOrWhiteSpace(dto.BuyerIdentifier)
                || string.IsNullOrWhiteSpace(dto.BuyerName))
            {
                WriteJsonError(400, "缺少必要參數：Scenario, Items, BuyerIdentifier, BuyerName");
                return;
            }

            try
            {
                // 5. 呼叫業務邏輯，改用強型別回傳
                var result = Task.Run(() => _service.CreateInvoiceAsync(dto)).Result;

                //JObject result = _service.CreateInvoiceAsync(dto).GetAwaiter().GetResult();

                // 6. 回傳成功 JSON
                // 包成統一格式
                var output = new
                {
                    success = result.Success,
                    code = result.Success ? "00000" : "40002",
                    msg = result.Success ? "發票開立成功" : result.ErrorMessage,
                    data = result
                };

                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (ArgumentException ex)
            {
                // 7. 回傳參數驗證錯誤
                WriteJsonError(400, ex.Message);
            }
            catch (Exception ex)
            {
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
