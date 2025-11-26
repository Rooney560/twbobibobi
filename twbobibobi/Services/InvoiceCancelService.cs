using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票作廢的實作服務。
    /// </summary>
    public class InvoiceCancelService : IInvoiceCancelService
    {
        private readonly InvoiceApiClient _apiClient;

        /// <summary>
        /// 建構子，初始化發票作廢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiClient">用於呼叫遠端 API 的發票客戶端</param>
        public InvoiceCancelService(InvoiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// 呼叫遠端 API 作廢指定的發票號碼。
        /// </summary>
        /// <param name="cancelInvoiceNumber">欲作廢的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public async Task<string> CancelInvoiceAsync(string cancelInvoiceNumber)
        {
            // 組成 POST 表單內容（單筆發票號碼）
            var payload = new[] {
                new Dictionary<string, string> {
                    { "CancelInvoiceNumber", cancelInvoiceNumber }
                }
            };

            // 呼叫發票 API 送出作廢請求
            return await _apiClient.PostAsync("/json/f0501", payload);
        }
    }
}