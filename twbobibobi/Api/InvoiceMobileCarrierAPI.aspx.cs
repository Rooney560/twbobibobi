using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.ApiClients;
using twbobibobi.Model;
using twbobibobi.Services;
using twbobibobi.Data;
using System.Net.Http;
using System.Text;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/InvoiceMobileCarrierAPI.aspx POST 請求
    /// 僅做手機載具合法性驗證
    /// </summary>
    public partial class InvoiceMobileCarrierAPI : AjaxBasePage
    {
        // 驗證器：檢查 carrierType 與 carrierId 格式合法性
        private readonly MobileCarrierValidator _validator = new MobileCarrierValidator();

        /// <summary>
        /// 測試環境 _Prod 正式環境 _UAT
        /// </summary>
        public static string env = "_Prod";

        /// <summary>
        /// 取得公司統一編號
        /// </summary>
        public static string CONST_CONFIG_INVOICE_Name = "INVOICE_Name" + env;

        /// <summary>
        /// 取得APP KEY
        /// </summary>
        public static string CONST_CONFIG_INVOICE_AppKey = "INVOICE_AppKey" + env;

        public static string invoiceNumber = ConfigurationManager.AppSettings[CONST_CONFIG_INVOICE_Name];
        public static string secretKey = ConfigurationManager.AppSettings[CONST_CONFIG_INVOICE_AppKey];

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
        /// 處理 POST 請求：讀取 JSON、驗證載具，回傳合法性結果
        /// </summary>
        private void ProcessRequest()
        {
            // 1. 讀取 request body
            string body;
            using (var reader = new StreamReader(Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            // 2. 解析 JSON
            JObject input;
            try
            {
                input = JObject.Parse(body);
            }
            catch (Exception)
            {
                WriteJsonError(400, "無法解析 JSON 格式");
                return;
            }

            // 3. 取參數
            string carrierType = input.Value<string>("carrierType");
            string carrierId = input.Value<string>("carrierId");
            if (string.IsNullOrWhiteSpace(carrierType) || string.IsNullOrWhiteSpace(carrierId))
            {
                WriteJsonError(400, "缺少必要參數：carrierType, carrierId");
                return;
            }

            // 4. 驗證：呼叫廠商提供的手機條碼查詢 API，需要帶 invoice, data, time, sign
            try
            {
                // 準備基本參數
                // invoice 使用公司統編
                string invoice = invoiceNumber;
                // data 包含 barCode 的 JSON 字串
                var dataObj = new JObject { ["barCode"] = carrierId };
                string dataJson = dataObj.ToString(Formatting.None);
                // 時間戳
                string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                // 簽章 = md5(dataJson + time + secretKey).ToLower()
                string toSign = dataJson + time + secretKey;
                using (var md5 = System.Security.Cryptography.MD5.Create())
                {
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(toSign));
                    var sb = new StringBuilder();
                    foreach (var b in hash) sb.Append(b.ToString("x2"));
                    string sign = sb.ToString();

                    // form urlencoded 參數
                    var form = new Dictionary<string, string>
                    {
                        ["invoice"] = invoice,
                        ["data"] = dataJson,
                        ["time"] = time,
                        ["sign"] = sign
                    };
                    using (var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                    using (var content = new FormUrlEncodedContent(form))
                    {
                        // header 預設 application/x-www-form-urlencoded
                        var resp = http.PostAsync("https://invoice-api.amego.tw/json/barcode", content).Result;
                        var respBody = resp.Content.ReadAsStringAsync().Result;
                        if (!resp.IsSuccessStatusCode)
                        {
                            WriteJsonError(502, "廠商驗證服務回應錯誤: " + resp.StatusCode);
                            return;
                        }

                        var vendorJson = JObject.Parse(respBody);
                        int code = vendorJson.Value<int>("code");
                        string msg = vendorJson.Value<string>("msg");

                        if (code == 0)
                            WriteJsonSuccess();
                        else
                            WriteJsonError(400, "廠商驗證失敗: " + msg);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteJsonError(500, "驗證過程發生例外: " + ex.Message);
            }
        }


        /// <summary>
        /// 回傳成功 JSON：valid = true
        /// </summary>
        private void WriteJsonSuccess()
        {
            Response.StatusCode = 200;
            Response.ContentType = "application/json";
            var obj = new JObject { ["valid"] = true };
            Response.Write(obj.ToString(Formatting.None));
        }

        /// <summary>
        /// 回傳錯誤 JSON：valid = false, error = message
        /// </summary>
        private void WriteJsonError(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";
            var obj = new JObject
            {
                ["valid"] = false,
                ["error"] = message
            };
            Response.Write(obj.ToString(Formatting.None));
        }
    }
}
