/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：IInvoiceCancelService.cs
   類別說明：提供發票作廢服務介面，定義了作廢發票的方法。
   建立日期：2025-11-28
   建立人員：Rooney

 * 修改記錄：2025-12-16 新增提供折讓單作廢服務介面，定義了折讓單的方法。
   目前維護人員：Rooney
   =================================================================================================== */

using System.Threading.Tasks;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票、折讓單作廢服務介面。
    /// </summary>
    /// <summary>
    /// <remarks>
    /// 這個介面定義了發票、折讓單作廢服務，允許根據發票號碼、折讓單編號進行作廢操作。
    /// </remarks>
    public interface IInvoiceCancelService
    {
        /// <summary>
        /// 作廢指定發票號碼。
        /// </summary>
        /// <param name="cancelInvoiceNumber">要作廢的發票號碼</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法會接收一個發票號碼，並將該發票標記為作廢。返回 API 的回應結果，
        /// 包含是否成功、錯誤訊息等。
        /// </remarks>
        /// <example>
        /// var result = await invoiceCancelService.CancelInvoiceAsync("INV123456789");
        /// </example>
        Task<string> CancelInvoiceAsync(string cancelInvoiceNumber);

        /// <summary>
        /// 作廢指定折讓單編號。
        /// </summary>
        /// <param name="cancelAllowanceNumber">要作廢的折讓單編號</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法會接收一個折讓單編號，並將該折讓單標記為作廢。返回 API 的回應結果，
        /// 包含是否成功、錯誤訊息等。
        /// </remarks>
        /// <example>
        /// var result = await invoiceCancelService.CancelAllowanceAsync("123456789");
        /// </example>
        Task<string> CancelAllowanceAsync(string cancelAllowanceNumber);
    }
}