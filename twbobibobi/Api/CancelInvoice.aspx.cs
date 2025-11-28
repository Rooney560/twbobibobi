using twbobibobi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Helpers;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// WebForms API 頁面：處理 /Api/CancelInvoice.aspx?invoiceNumber=XXX&env=YYY
    /// </summary>
    public partial class CancelInvoice : AjaxBasePage
    {
        // 根據 query string 決定要用哪一組環境設定
        static string envSuffix = HttpContext.Current.Request["env"] == "uat" ? "_UAT" : "_Prod";

        // 透過 Factory 建立 Service 實例
        IInvoiceStatusService _service_status = InvoiceServiceFactory.CreateStatusService(GetEnvironment());
        IInvoiceCancelService _service_cancel = InvoiceServiceFactory.CreateCancelService(GetEnvironment());

        private static string GetEnvironment()
        {
            return HttpContext.Current?.Request["env"] == "uat" ? "_UAT" : "_Prod";
        }
        /// <summary>
        /// Page_Load 只接受 GET 請求
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
                string env = Request["env"] == "uat" ? "uat" : "prod";
                string ts = Request["ts"];
                string sig = Request["sig"];

                // 1. 基本參數檢查
                if (string.IsNullOrWhiteSpace(invoiceNo) ||
                    string.IsNullOrWhiteSpace(ts) ||
                    string.IsNullOrWhiteSpace(sig))
                {
                    WriteJsonError(400, "缺少必要參數");
                    return;
                }

                // 2. 驗證簽章
                if (!ValidateSignature(invoiceNo, env, ts, sig))
                {
                    WriteJsonError(401, "簽章驗證失敗");
                    return;
                }

                // 3. 防止重放攻擊：檢查時間戳僅允許 ±5 分鐘內
                if (!ValidateTimestamp(ts, 300))
                {
                    WriteJsonError(408, "時間戳已過期");
                    return;
                }

                // 4. 進入原本流程
                ProcessRequest();
            }
        }

        /// <summary>
        /// 驗證簽章：HMAC-SHA256(payload) == sig
        /// payload = "{invoiceNo}|{env}|{ts}"
        /// </summary>
        private bool ValidateSignature(string invoiceNo, string env, string ts, string sig)
        {
            // 由 env 決定要用哪組密鑰
            string keyName = "ApiAuthSecret_" + (env == "uat" ? "UAT" : "Prod");
            string secret = ConfigurationManager.AppSettings[keyName];
            if (string.IsNullOrEmpty(secret))
                return false;

            string payload = $"{invoiceNo}|{env}|{ts}";
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] dataBytes = Encoding.UTF8.GetBytes(payload);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hash = hmac.ComputeHash(dataBytes);
                string expected = BitConverter
                    .ToString(hash)
                    .Replace("-", "")
                    .ToLowerInvariant();
                return expected == sig;
            }
        }

        /// <summary>
        /// 檢查時間戳是否在允許範圍內（秒）。
        /// </summary>
        private bool ValidateTimestamp(string ts, int allowedWindowSeconds)
        {
            if (!long.TryParse(ts, out long unixSec))
                return false;
            var reqTime = DateTimeOffset.FromUnixTimeSeconds(unixSec);
            var now = DateTimeOffset.UtcNow;
            return Math.Abs((now - reqTime).TotalSeconds) <= allowedWindowSeconds;
        }

        /// <summary>
        /// 主流程：查詢 → 驗證 type → 作廢 → 回傳 JSON
        /// </summary>
        private void ProcessRequest()
        {
            string invoiceNo = Request["invoiceNumber"];
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                WriteJsonError(400, "缺少必要參數：invoiceNumber");
                return;
            }

            try
            {
                // 1. 查詢發票狀態
                var statusJson = Task.Run(() =>
                    _service_status.QueryStatusAsync(invoiceNo)).Result;
                var statusDto = JObject.Parse(statusJson).ToInvoiceStatusResponseDto();

                if (!statusDto.Success)
                {
                    WriteJsonError(400, "查詢失敗：" + statusDto.ErrorMessage);
                    return;
                }
                // 2. 驗證型態
                if (statusDto.Type != "C0401" || statusDto.Status != "99")
                {
                    WriteJsonError(400, $"發票類型（{statusDto.Type}）及發票狀態（{statusDto.Status}）非正常參數。");
                    return;
                }

                // 3. 呼叫作廢 API
                var cancelJson = Task.Run(() =>
                    _service_cancel.CancelInvoiceAsync(invoiceNo)).Result;
                var cancelDto = JObject.Parse(cancelJson).ToCancelInvoiceResponseDto(invoiceNo);

                if (!cancelDto.Success)
                {
                    WriteJsonError(400, "作廢失敗：" + cancelDto.ErrorMessage);
                    return;
                }

                // 4. 回傳成功結果
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