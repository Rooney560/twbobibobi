/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：IInvoiceStatusService.cs
   類別說明：提供發票查詢服務介面，定義了查詢發票狀態的方法。
   建立日期：2025-11-28
   建立人員：Rooney

 * 修改記錄：2025-12-16 提供折讓單查詢服務介面，定義了查詢折讓單狀態的方法。
   目前維護人員：Rooney
   =================================================================================================== */


using System.Threading.Tasks;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票、折讓單查詢服務介面。
    /// </summary>
    /// <remarks>
    /// 此介面定義了發票、折讓單查詢服務，允許根據發票號碼查詢該發票的狀態；允許根據折讓單編號查詢該折讓單的狀態。
    /// </remarks>
    public interface IInvoiceStatusService
    {
        /// <summary>
        /// 查詢指定發票號碼的狀態。
        /// </summary>
        /// <param name="invoiceNumber">要查詢的發票號碼</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法根據提供的發票號碼查詢該發票的狀態。返回的結果包含該發票的最新狀態，
        /// 例如是否開立成功、是否作廢、是否已報稅等。
        /// </remarks>
        Task<string> QueryStatusAsync(string invoiceNumber);

        /// <summary>
        /// 查詢指定折讓單編號的狀態。
        /// </summary>
        /// <param name="allowanceNumber">要查詢的折讓單編號</param>
        /// <returns>API 回應結果</returns>
        /// <remarks>
        /// 這個方法根據提供的折讓單編號查詢該折讓單的狀態。返回的結果包含該折讓單的最新狀態，
        /// 例如是否開立成功、是否作廢、是否已報稅等。
        /// </remarks>
        Task<string> QueryAllowanceStatusAsync(string allowanceNumber);
    }
}