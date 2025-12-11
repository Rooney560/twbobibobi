// ===================================================================================================
// 專案名稱：twbobibobi
// 檔案名稱：AllowanceInvoice.aspx.cs
// 類別說明：提供電子發票折讓單建立 API（WebForms API）
// 建立日期：2025-12-10
// 建立人員：Rooney

// 目前維護人員：Rooney
// ===================================================================================================

using twbobibobi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Helpers;
using twbobibobi.Services;

namespace twbobibobi.Api
{
    /// <summary>
    /// /Api/AllowanceInvoice.aspx?invoiceNumber=XXX&env=YYY API 頁面  
    /// 功能：依據 invoiceNumber 建立折讓單，並支援 UAT / Prod 環境切換、簽章驗證、防重放攻擊
    /// </summary>
    public partial class AllowanceInvoice : AjaxBasePage
    {
        /// <summary>依據 QueryString env 決定環境設定後綴（_UAT / _Prod）</summary>
        private static readonly string EnvSuffix =
            HttpContext.Current.Request["env"] == "uat" ? "_UAT" : "_Prod";

        /// <summary>發票查詢服務</summary>
        private readonly IInvoiceStatusService _serviceStatus =
            InvoiceServiceFactory.CreateStatusService(GetEnvironment());

        /// <summary>折讓單建立服務</summary>
        //private readonly IInvoiceAllowanceService _serviceAllowance =
        //    InvoiceServiceFactory.CreateAllowanceService(GetEnvironment());

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
                    Response.StatusCode = 405;
                    return;
                }

                string invoiceNo = Request["invoiceNumber"];
                string env = Request["env"] == "uat" ? "uat" : "prod";
                string ts = Request["ts"];
                string sig = Request["sig"];

                if (string.IsNullOrWhiteSpace(invoiceNo) ||
                    string.IsNullOrWhiteSpace(ts) ||
                    string.IsNullOrWhiteSpace(sig))
                {
                    WriteJsonError(400, "缺少必要參數");
                    return;
                }

                if (!ValidateSignature(invoiceNo, env, ts, sig))
                {
                    WriteJsonError(401, "簽章驗證失敗");
                    return;
                }

                if (!ValidateTimestamp(ts, 300))
                {
                    WriteJsonError(408, "時間戳已過期");
                    return;
                }

                ProcessRequest();
            }
        }

        /// <summary>
        /// 驗證 HMAC-SHA256 簽章  
        /// payload: {invoiceNo}|{env}|{ts}
        /// </summary>
        private bool ValidateSignature(string invoiceNo, string env, string ts, string sig)
        {
            string keyName = "ApiAuthSecret_" + (env == "uat" ? "UAT" : "Prod");
            string secret = ConfigurationManager.AppSettings[keyName];
            if (string.IsNullOrEmpty(secret))
                return false;

            string payload = $"{invoiceNo}|{env}|{ts}";
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] dataBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                string expected = BitConverter
                    .ToString(hmac.ComputeHash(dataBytes))
                    .Replace("-", "")
                    .ToLowerInvariant();

                return expected == sig;
            }
        }

        /// <summary>
        /// 檢查 UNIX Timestamp 是否落在允許的 ±N 秒內（預設 300 秒）
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
        /// 主流程：  
        /// 1. 查詢發票狀態  
        /// 2. 驗證狀態是否允許折讓  
        /// 3. 呼叫折讓單 API  
        /// 4. 回傳 JSON  
        /// </summary>
        private void ProcessRequest()
        {
            string invoiceNo = Request["invoiceNumber"];

            try
            {
                // 1. 查詢發票狀態
                var statusJson = _serviceStatus.QueryStatusAsync(invoiceNo).Result;
                var statusDto = JObject.Parse(statusJson).ToInvoiceStatusResponseDto();

                if (!statusDto.Success)
                {
                    WriteJsonError(400, "查詢失敗：" + statusDto.ErrorMessage);
                    return;
                }

                // 必須是正常發票 (C0401) 且可折讓 (99)
                if (statusDto.Type != "C0401" || statusDto.Status != "99")
                {
                    WriteJsonError(400,
                        $"發票類型({statusDto.Type}) 或狀態({statusDto.Status}) 不允許折讓");
                    return;
                }

                // 2. 呼叫折讓 API
                //var allowanceJson = _serviceAllowance.CreateAllowanceAsync(invoiceNo).Result;
                //var allowanceDto = JObject.Parse(allowanceJson).ToAllowanceInvoiceResponseDto(invoiceNo);

                //if (!allowanceDto.Success)
                //{
                //    WriteJsonError(400, "建立折讓單失敗：" + allowanceDto.ErrorMessage);
                //    return;
                //}

                // 3. 回傳成功
                //Response.ContentType = "application/json";
                //Response.Write(JsonConvert.SerializeObject(new
                //{
                //    success = true,
                //    invoiceNumber = invoiceNo,
                //    data = allowanceDto
                //}));
            }
            catch (Exception ex)
            {
                Exception inner = ex.InnerException ?? ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;

                WriteJsonError(500, "內部錯誤：" + inner.Message);
            }
        }

        /// <summary>
        /// 回傳統一格式錯誤 JSON，並設定 HTTP Status Code
        /// </summary>
        private void WriteJsonError(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";

            var err = new
            {
                success = false,
                code = statusCode.ToString(),
                msg = message
            };

            Response.Write(JsonConvert.SerializeObject(err));
        }
    }
}