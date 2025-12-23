/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：IInvoiceService.cs
   類別說明：發票服務介面：定義建立發票的方法，提供開立發票的契約。
   建立日期：2025-11-28
   建立人員：Rooney

 * 修改記錄：2025-12-16 新增建立折讓單的方法，提供開立折讓單的契約。
   目前維護人員：Rooney
   =================================================================================================== */

using System.Threading.Tasks;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票服務介面：定義建立發票的方法、定義建立折讓單的方法
    /// </summary>
    /// <remarks>
    /// 這個介面定義了發票、折讓單服務的主要功能，包括根據傳入的資料創建發票、折讓單。
    /// 我們可以依照這個介面來實作不同的發票、折讓單服務。
    /// </remarks>
    public interface IInvoiceService
    {
        /// <summary>
        /// 根據輸入資料建立發票，並回傳結構化回應物件。
        /// </summary>
        /// <param name="dto">開立發票所需資訊</param>
        /// <returns>API 回應結果（封裝為 InvoiceResponseDto）</returns>
        /// <remarks>
        /// 此方法將接收一個 `CreateInvoiceDto` 物件，並通過調用外部發票 API 來開立發票。
        /// 最終會返回一個封裝好的 `InvoiceResponseDto` 物件，包含 API 回應結果。
        /// </remarks>
        /// <example>
        /// // 呼叫範例
        /// var response = await invoiceService.CreateInvoiceAsync(createInvoiceDto);
        /// </example>
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto);

        /// <summary>
        /// 根據輸入資料建立折讓單，並回傳結構化回應物件。
        /// </summary>
        /// <param name="dto">開立折讓單所需資訊</param>
        /// <returns>API 回應結果（封裝為 AllowanceResponseDto）</returns>
        /// <remarks>
        /// 此方法將接收一個 `CreateAllowanceDto` 物件，並通過調用外部折讓單 API 來開立折讓單。
        /// 最終會返回一個封裝好的 `AllowanceResponseDto` 物件，包含 API 回應結果。
        /// </remarks>
        /// <example>
        /// // 呼叫範例
        /// var response = await allowanceService.CreateAllowanceAsync(createAllowanceDto);
        /// </example>
        Task<AllowanceResponseDto> CreateAllowanceAsync(CreateAllowanceDto dto);
    }
}