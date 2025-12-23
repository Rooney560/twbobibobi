/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceResponseDto.cs
   類別說明：對應發票 API 回傳的 JSON 結構，封裝 API 回應結果。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

namespace twbobibobi.Model
{
    /// <summary>
    /// 對應發票 API 回傳的 JSON 結構
    /// 來源：/Api/AllowanceAPI.aspx 回傳格式
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝從發票 API 回傳的資料結構，這些資料包括發票是否成功、錯誤訊息、發票號碼等。
    /// 回應資料還包含條碼、QR 代碼、列印資料等，根據 API 回應的具體情況而有所不同。
    /// </remarks>
    public class AllowanceResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤代碼
        /// </summary>
        /// <example>"0"</example>
        public string Code { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        /// <example>"Invalid invoice number"</example>
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 原始 JSON 回傳
        /// </summary>
        /// <example>{"Success": true, "InvoiceNumber": "1234567890", "InvoiceTime": 1609459200}</example>
        public string RawJson { get; set; }
    }
}