/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceCancelService.cs
   類別說明：電子發票折讓單作廢建立服務，負責呼叫廠商 API 建立折讓單。
   建立日期：2025-12-17
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System.Collections.Generic;
using System.Threading.Tasks;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 電子發票折讓單作廢服務。
    /// 此服務負責組裝作廢資料並呼叫電子發票廠商 API。
    /// </summary>
    /// <remarks>
    /// 這個服務負責處理折讓單的作廢流程，接收要作廢折讓單編號，並將其提交給廠商 API。
    /// 在作廢完成後，會返回 API 的回應結果，通常為 JSON 格式，包含是否成功等信息。
    /// </remarks>
    public class AllowanceCancelService : IInvoiceCancelService
    {
        /// <summary>
        /// API 端點 URL（由外部 Factory 注入）
        /// </summary>
        private readonly InvoiceApiClient _apiUrl;

        /// <summary>
        /// 建構子，初始化折讓單作廢服務，並注入 API 客戶端。
        /// </summary>
        /// <param name="apiUrl">用於呼叫電子發票廠商的折讓單作廢 API</param>
        /// <remarks>
        /// 透過建構子注入 `InvoiceApiClient` 實例，這樣服務可以使用該客戶端與發票廠商進行通信。
        /// </remarks>
        public AllowanceCancelService(InvoiceApiClient apiUrl)
        {
            _apiUrl = apiUrl;
        }

        /// <summary>
        /// 呼叫遠端 API 作廢指定發票號碼。
        /// </summary>
        /// <param name="cancelInvoiceNumber">欲作廢的發票號碼</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        public Task<string> CancelInvoiceAsync(string cancelInvoiceNumber)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 呼叫遠端 API 作廢指定的折讓單編號。
        /// </summary>
        /// <param name="cancelAllowanceNumber">欲作廢的折讓單編號</param>
        /// <returns>API 回應字串（通常為 JSON 格式）</returns>
        /// <remarks>
        /// 這個方法會根據提供的折讓單編號，構建請求並將其發送到折讓單 API。API 回應會被返回，通常為 JSON 格式，包含是否成功的結果。
        /// </remarks>
        /// <example>
        /// var result = await invoiceCancelService.CancelAllowanceAsync("123456789");
        /// </example>
        public async Task<string> CancelAllowanceAsync(string cancelAllowanceNumber)
        {
            // 組成 POST 表單內容（單筆發票號碼）
            var payload = new[] {
                new Dictionary<string, string> {
                    { "CancelAllowanceNumber", cancelAllowanceNumber }
                }
            };

            // 呼叫發票 API 送出作廢請求
            return await _apiUrl.PostAsync("/json/g0501", payload);
        }

    }
}