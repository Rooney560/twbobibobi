/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceStatusService.cs
   類別說明：提供折讓單狀態查詢的實作服務，負責根據折讓單編號查詢該折讓單的處理狀態。
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
    /// 這個類別實作了 `IInvoiceStatusService` 介面，負責查詢指定折讓單編號的折讓單狀態。
    /// 透過 `InvoiceApiClient` 呼叫折讓單 API 查詢折讓單的處理狀態並返回回應結果（通常為 JSON 格式）。
    /// </remarks>
    public class AllowanceStatusService : IInvoiceStatusService
    {
        private readonly InvoiceApiClient _apiClient;

        /// <summary>
        /// 建構子，初始化折讓單查詢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiClient">用於呼叫遠端 API 的折讓單客戶端</param>
        /// <remarks>
        /// 這個類別實作了 `IInvoiceStatusService` 介面，負責查詢指定折讓單編號的折讓單狀態。
        /// 透過 `InvoiceApiClient` 呼叫折讓單 API 查詢折讓單的處理狀態並返回回應結果（通常為 JSON 格式）。
        /// </remarks>
        public AllowanceStatusService(InvoiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// 查詢指定發票號碼的處理狀態。
        /// </summary>
        /// <param name="invoiceNumber">欲查詢狀態的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public Task<string> QueryStatusAsync(string invoiceNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查詢指定折讓單編號的處理狀態。
        /// </summary>
        /// <param name="allowanceNumber">欲查詢狀態的折讓單編號</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        /// <remarks>
        /// 此方法會根據提供的折讓單編號，組成查詢請求並發送至折讓單 API，然後返回 API 的回應結果。
        /// 回應結果通常為 JSON 格式，包含折讓單的詳細狀態。
        /// </remarks>
        /// <example>
        /// var result = await invoiceStatusService.QueryAllowanceStatusAsync("123456789");
        /// </example>
        public async Task<string> QueryAllowanceStatusAsync(string allowanceNumber)
        {
            // 組成 POST 表單內容（單筆折讓單號碼）
            var payload = new[] {
                new Dictionary<string, string> {
                    { "AllowanceNumber", allowanceNumber }
                }
            };

            // 呼叫折讓單 API 查詢狀態
            return await _apiClient.PostAsync("/json/allowance_status", payload);
        }

    }
}