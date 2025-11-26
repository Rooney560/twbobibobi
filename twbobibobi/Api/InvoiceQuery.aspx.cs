using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Services;
using twbobibobi.Data;
using twbobibobi.Model;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/InvoiceQuery.aspx POST 請求
    /// </summary>
    public partial class InvoiceQuery : AjaxBasePage
    {
        InvoiceQueryService _service = InvoiceServiceFactory.CreateQueryService(GetEnvironment());

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
                    Response.StatusCode = 405;
                    Response.End();
                }
                ProcessRequest();
            }
        }

        private void ProcessRequest()
        {
            string body;
            using (var reader = new StreamReader(Request.InputStream))
                body = reader.ReadToEnd();

            string orderID;
            try
            {
                var obj = JsonConvert.DeserializeObject<JObject>(body);
                orderID = obj?.Value<string>("orderID");
            }
            catch
            {
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            if (string.IsNullOrWhiteSpace(orderID))
            {
                WriteJsonError(400, "請提供 orderID");
                return;
            }

            try
            {
                var raw = Task.Run(() => _service.QueryOrderIdAsync(orderID)).Result;
                var dto = JObject.Parse(raw);
                // 直接把 data 當成 JObject
                var data = dto["data"] as JObject;
                if (data == null)
                {
                    // (萬一有時候也是陣列) 再做備援
                    var dataArr = dto["data"] as JArray;
                    data = dataArr?.First as JObject;
                }

                var output = new
                {
                    success = true,
                    code = "00000",
                    msg = "查詢成功",
                    data = new
                    {
                        Success = true,
                        ErrorMessage = "",
                        InvoiceNumber = data["invoice_number"],
                        InvoiceTime = data["create_date"],
                        RandomNumber = data["random_number"],
                        Barcode = data["random_number"]
                    }
                };

                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(output, Formatting.None));
            }
            catch (Exception ex)
            {
                WriteJsonError(500, "伺服器內部錯誤");
                //Exception inner = ex.InnerException ?? ex;

                //// 取出內部最底層例外訊息
                //while (inner.InnerException != null)
                //    inner = inner.InnerException;

                //WriteJsonError(500, $"內部錯誤：{inner.Message}");
            }
        }

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