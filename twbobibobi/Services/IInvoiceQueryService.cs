/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：IInvoiceQueryService.cs
   類別說明：提供發票查詢服務介面，定義了查詢發票狀態的方法。
   建立日期：2025-11-28
   建立人員：Rooney

 * 修改記錄：2025-12-17 提供折讓單查詢服務介面，定義了查詢折讓單狀態的方法。
   目前維護人員：Rooney
   =================================================================================================== */

using System.Threading.Tasks;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票、折讓單查詢服務介面。
    /// </summary>
    /// <remarks>
    /// 此介面定義了查詢發票、折讓單狀態的功能，允許根據訂單編號或發票號碼查詢發票的詳細狀態；允許根據折讓單編號查詢折讓單的詳細狀態。
    /// 發票查詢可以通過訂單編號或發票號碼進行，並回傳相應的 API 查詢結果。
    /// 折讓單查詢可以通過折讓單編號進行，並回傳相應的 API 查詢結果。
    /// </remarks>
    public interface IInvoiceQueryService
    {
        /// <summary>
        /// 查詢指定訂單編號的發票狀態。
        /// </summary>
        /// <param name="orderID">要查詢的訂單編號</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法根據提供的訂單編號查詢對應的發票狀態，並返回 API 的查詢結果。
        /// </remarks>
        /// <example>
        /// var result = await invoiceQueryService.QueryOrderIdAsync("ORD123456789");
        /// </example>
        Task<string> QueryOrderIdAsync(string orderID);

        /// <summary>
        /// 查詢指定發票號碼的狀態。
        /// </summary>
        /// <param name="invoiceNumber">要查詢的發票號碼</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法根據提供的發票號碼查詢對應的發票狀態，並返回 API 的查詢結果。
        /// </remarks>
        /// <example>
        /// var result = await invoiceQueryService.QueryInvoiceNumberAsync("INV123456789");
        /// </example>
        Task<string> QueryInvoiceNumberAsync(string invoiceNumber);

        /// <summary>
        /// 查詢指定折讓單號碼的狀態。
        /// </summary>
        /// <param name="allowanceNumber">要查詢的折讓單號碼</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法根據提供的折讓單號碼查詢對應的折讓單狀態，並返回 API 的查詢結果。
        /// </remarks>
        /// <example>
        /// var result = await invoiceQueryService.QueryAllowanceNumberAsync("INV123456789");
        /// </example>
        Task<string> QueryAllowanceNumberAsync(string allowanceNumber);
    }
}