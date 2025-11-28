using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.ApiClients
{
    /// <summary>
    /// 呼叫 WebForms InvoiceAPI.aspx 端點的客戶端；同步方法，適用於 .NET Framework 4.8
    /// </summary>
    public class InvoiceCaller
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 建構子：傳入完整的 InvoiceAPI.aspx 網址
        /// </summary>
        public InvoiceCaller(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));

            _endpoint = endpoint;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        /// <summary>
        /// 同步呼叫 InvoiceAPI.aspx，傳入 DTO，回傳合併自定欄位後的 JSON 物件
        /// </summary>
        /// <exception cref="HttpRequestException">HTTP 狀態非 2xx 時拋出</exception>
        public JObject CallInvoiceApi(CreateInvoiceDto dto)
        {
            // 1. 序列化 DTO 成 JSON
            string jsonPayload = JsonConvert.SerializeObject(dto);

            // 2. 建立 StringContent (application/json)
            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // 3. 同步 POST
            HttpResponseMessage response = _httpClient.PostAsync(_endpoint, content).Result;

            // 4. 讀取回應字串
            string responseBody = response.Content.ReadAsStringAsync().Result;

            // 5. 錯誤處理
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    string.Format("呼叫 InvoiceAPI 失敗: HTTP {0} {1}，回應: {2}",
                        (int)response.StatusCode,
                        response.ReasonPhrase,
                        responseBody));
            }

            // 6. 解析並回傳 JObject
            return JObject.Parse(responseBody);
        }
    }
}