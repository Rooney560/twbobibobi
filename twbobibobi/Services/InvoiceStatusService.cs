/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceStatusService.cs
   類別說明：提供發票狀態查詢的實作服務，負責根據發票號碼查詢該發票的處理狀態。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票狀態查詢的實作服務。
    /// </summary>
    /// <remarks>
    /// 這個類別實作了 `IInvoiceStatusService` 介面，負責查詢指定發票號碼的發票狀態。
    /// 透過 `InvoiceApiClient` 呼叫發票 API 查詢發票的處理狀態並返回回應結果（通常為 JSON 格式）。
    /// </remarks>
    public class InvoiceStatusService : IInvoiceStatusService
    {
        private readonly InvoiceApiClient _apiClient;

        /// <summary>
        /// 建構子，初始化發票查詢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiClient">用於呼叫遠端 API 的發票客戶端</param>
        /// <remarks>
        /// 這個類別實作了 `IInvoiceStatusService` 介面，負責查詢指定發票號碼的發票狀態。
        /// 透過 `InvoiceApiClient` 呼叫發票 API 查詢發票的處理狀態並返回回應結果（通常為 JSON 格式）。
        /// </remarks>
        public InvoiceStatusService(InvoiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// 查詢指定發票號碼的處理狀態。
        /// </summary>
        /// <param name="invoiceNumber">欲查詢狀態的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        /// <remarks>
        /// 此方法會根據提供的發票號碼，組成查詢請求並發送至發票 API，然後返回 API 的回應結果。
        /// 回應結果通常為 JSON 格式，包含發票的詳細狀態。
        /// </remarks>
        /// <example>
        /// var result = await invoiceStatusService.QueryStatusAsync("INV123456789");
        /// </example>
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

        /// <summary>
        /// 查詢指定折讓單編號的處理狀態。
        /// </summary>
        /// <param name="allowanceNumber">欲查詢狀態的折讓單編號</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public Task<string> QueryAllowanceStatusAsync(string allowanceNumber)
        {
            throw new NotImplementedException();
        }

    }
}