using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票狀態查詢的實作服務。
    /// </summary>
    public class InvoiceStatusService : IInvoiceStatusService
    {
        private readonly InvoiceApiClient _apiClient;

        /// <summary>
        /// 建構子，初始化發票查詢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiClient">用於呼叫遠端 API 的發票客戶端</param>
        public InvoiceStatusService(InvoiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// 查詢指定發票號碼的處理狀態。
        /// </summary>
        /// <param name="invoiceNumber">欲查詢狀態的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public async Task<string> QueryStatusAsync(string invoiceNumber)
        {
            // 組成 POST 表單內容（單筆發票號碼）
            var payload = new[] {
                new Dictionary<string, string> {
                    { "InvoiceNumber", invoiceNumber }
                }
            };

            // 呼叫發票 API 查詢狀態
            return await _apiClient.PostAsync("/json/invoice_status", payload);
        }
    }
}