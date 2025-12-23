/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceQueryService.cs
   類別說明：提供折讓單狀態查詢的實作服務，負責根據折讓單編號查詢折讓單的處理狀態。
   建立日期：2025-12-17
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
    /// 提供折讓單狀態查詢的實作服務。
    /// </summary>
    /// <remarks>
    /// 這個類別實作了 `IInvoiceQueryService` 介面，負責查詢指定折讓單編號的折讓單處理狀態。
    /// 這些查詢請求將通過 `InvoiceApiClient` 呼叫折讓單 API，並返回 API 回應的字串結果（通常為 JSON 格式）。
    /// </remarks>
    public class AllowanceQueryService : IInvoiceQueryService
    {
        private readonly InvoiceApiClient _apiClient;

        /// <summary>
        /// 建構子，初始化折讓單查詢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiClient">用於呼叫遠端 API 的折讓單客戶端</param>
        /// <remarks>
        /// 這個建構子會注入 `InvoiceApiClient` 實例，這樣就能通過它與折讓單 API 進行交互。
        /// </remarks>
        public AllowanceQueryService(InvoiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// 查詢指定發票號碼的處理狀態。
        /// </summary>
        /// <param name="orderID">欲查詢狀態的訂單編號</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public Task<string> QueryOrderIdAsync(string orderID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查詢指定發票號碼的處理狀態。
        /// </summary>
        /// <param name="invoice_number">欲查詢狀態的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public Task<string> QueryInvoiceNumberAsync(string invoice_number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查詢指定折讓單編號的處理狀態。
        /// </summary>
        /// <param name="allowanceNumber">欲查詢狀態的折讓單編號</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        /// <remarks>
        /// 這個方法將會根據提供的折讓單編號，構建請求並傳送給折讓單 API 查詢該折讓單的狀態。
        /// 返回的結果是 JSON 格式的字串，包含了折讓單的詳細狀態。
        /// </remarks>
        /// <example>
        /// var result = await invoiceQueryService.QueryAllowanceNumberAsync("123456789");
        /// </example>
        public async Task<string> QueryAllowanceNumberAsync(string allowanceNumber)
        {
            // 組成 POST 表單內容（單筆折讓單編號）
            var payload = 
                new Dictionary<string, string> {
                    { "allowance_number", allowanceNumber}
                };

            // 呼叫折讓單 API 查詢狀態
            return await _apiClient.PostAsync("/json/allowance_query", payload);
        }

    }
}