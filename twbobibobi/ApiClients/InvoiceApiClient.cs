using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using twbobibobi.Model;

namespace twbobibobi.ApiClients
{
    /// <summary>
    /// 發票 API 客戶端：封裝簽章與 Http 呼叫邏輯
    /// </summary>
    public class InvoiceApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _invoiceNumber;
        private readonly string _apiUrl;
        private readonly string _secretKey;

        /// <summary>
        /// 建構子：設定 API URL、密鑰與 HttpClient timeout
        /// </summary>
        /// <param name="apiUrl">廠商發票 API 位址</param>
        /// <param name="invoiceNumber">公司統編</param>
        /// <param name="secretKey">簽章用的密鑰</param>
        /// <param name="timeout">HttpClient 請求逾時時間（預設 30 秒）</param>
        public InvoiceApiClient(string apiUrl, string invoiceNumber, string secretKey, TimeSpan? timeout = null)
        {
            _apiUrl = apiUrl;
            _invoiceNumber = invoiceNumber;
            _secretKey = secretKey;
            _httpClient = new HttpClient
            {
                Timeout = timeout ?? TimeSpan.FromSeconds(60)
            };
        }

        /// <summary>
        /// 傳送發票請求並回傳原始回應（已做 Regex.Unescape）
        /// 只有在 <see cref="InvoiceRequest"/> 中有值的欄位才會被序列化進 JSON。
        /// </summary>
        /// <param name="request">發票請求資料模型</param>
        /// <returns>API 回傳字串</returns>
        public async Task<string> SendInvoiceAsync(InvoiceRequest request)
        {
            // 1. 序列化時忽略所有 null 屬性
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string dataJson = JsonConvert.SerializeObject(request, settings);

            // 2. 取得 Unix 時間戳
            string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            // 3. 計算 MD5 簽章（小寫 hex）
            string signature = GenerateSignature(dataJson, time, _secretKey);

            // 4. 組成表單
            var form = new Dictionary<string, string>
            {
                ["invoice"] = _invoiceNumber,
                ["data"] = dataJson,
                ["time"] = time,
                ["sign"] = signature
            };

            using (var content = new FormUrlEncodedContent(form))
            {
                // 5. 發送 POST 請求
                HttpResponseMessage response = await _httpClient
                    .PostAsync(_apiUrl, content)
                    .ConfigureAwait(false);

                // 6. 確保回應成功
                response.EnsureSuccessStatusCode();

                // 7. 讀取並 Unescape 回傳字串
                string raw = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Regex.Unescape(raw);
            }
        }

        /// <summary>
        /// 傳送發票請求並回傳原始回應（已做 Regex.Unescape）
        /// 只有在 <see cref="InvoiceRequest"/> 中有值的欄位才會被序列化進 JSON。
        /// </summary>
        /// <param name="invoiceNumber">發票號碼</param>
        /// <param name="request">發票請求資料模型</param>
        /// <returns>API 回傳字串</returns>
        public async Task<string> SendInvoiceAsync(string invoiceNumber, Dictionary<string, string> request)
        {
            // 1. 序列化
            string dataJson = JsonConvert.SerializeObject(request);

            // 2. 取得 Unix 時間戳
            string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            // 3. 計算 MD5 簽章（小寫 hex）
            string signature = GenerateSignature(dataJson, time, _secretKey);

            // 4. 組成表單
            var form = new Dictionary<string, string>
            {
                ["invoice"] = invoiceNumber,
                ["data"] = dataJson,
                ["time"] = time,
                ["sign"] = signature
            };

            using (var content = new FormUrlEncodedContent(form))
            {
                // 5. 發送 POST 請求
                HttpResponseMessage response = await _httpClient
                    .PostAsync(_apiUrl, content)
                    .ConfigureAwait(false);

                // 6. 確保回應成功
                response.EnsureSuccessStatusCode();

                // 7. 讀取並 Unescape 回傳字串
                string raw = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Regex.Unescape(raw);
            }
        }

        /// <summary>
        /// 計算簽章：MD5(data + time + secretKey)，並輸出小寫 hex 字串
        /// </summary>
        private static string GenerateSignature(string data, string time, string secret)
        {
            string toSign = data + time + secret;
            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(toSign));
                var sb = new StringBuilder();
                foreach (var b in hash)
                {
                    // "x2" 產生小寫 2 位元 hex
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 發送帶有簽章與時間戳的 POST 請求至指定 API 路徑。
        /// </summary>
        /// <param name="endpoint">API 相對路徑（例：/json/f0501）</param>
        /// <param name="data">要送出的發票欄位資料字典</param>
        /// <returns>API 回應內容（已 Unescape）</returns>
        public async Task<string> PostAsync(string endpoint, object data)
        {
            // 1. 將資料序列化為 JSON 格式字串
            string jsonData = JsonConvert.SerializeObject(data);

            // 2. 建立 Unix 時間戳（秒）
            string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            // 3. 根據 data + time + secretKey 產生 MD5 簽章
            string signature = GenerateSignature(jsonData, time, _secretKey);

            // 4. 組成 POST 表單內容
            var form = new Dictionary<string, string>
            {
                ["invoice"] = _invoiceNumber,
                ["data"] = jsonData,
                ["time"] = time,
                ["sign"] = signature
            };

            using (var content = new FormUrlEncodedContent(form))
            {
                // 5. 合成完整 URL，送出 POST 請求
                var fullUrl = string.Concat(_apiUrl.TrimEnd('/'), endpoint);
                HttpResponseMessage response = await _httpClient
                    .PostAsync(fullUrl, content)
                    .ConfigureAwait(false);

                // 6. 確保 HTTP 狀態為 2xx，否則拋例外
                response.EnsureSuccessStatusCode();

                // 7. 讀取回應字串並執行反斜線還原
                string raw = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Regex.Unescape(raw);
            }
        }

        /// <summary>
        /// 釋放 HttpClient
        /// </summary>
        public void Dispose() => _httpClient?.Dispose();
    }
}