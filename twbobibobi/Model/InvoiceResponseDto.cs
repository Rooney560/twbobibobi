/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceResponseDto.cs
   類別說明：對應發票 API 回傳的 JSON 結構，封裝 API 回應結果。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

namespace twbobibobi.Model
{
    /// <summary>
    /// 對應發票 API 回傳的 JSON 結構
    /// 來源：/Api/InvoiceAPI.aspx 回傳格式
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝從發票 API 回傳的資料結構，這些資料包括發票是否成功、錯誤訊息、發票號碼等。
    /// 回應資料還包含條碼、QR 代碼、列印資料等，根據 API 回應的具體情況而有所不同。
    /// </remarks>
    public class InvoiceResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤代碼
        /// </summary>
        /// <example>"Invalid invoice number"</example>
        public string Code { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        /// <example>"Invalid invoice number"</example>
        public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// 發票號碼
        /// </summary>
        /// <example>"1234567890"</example>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// 發票開立時間，Unix 時間戳記，正確才會回傳
        /// </summary>
        /// <example>1609459200</example>
        public long InvoiceTime { get; set; }

        /// <summary>
        /// 發票隨機碼
        /// </summary>
        /// <example>"abc123"</example>
        public string RandomNumber { get; set; }

        /// <summary>
        /// 條碼內容
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// QRCode 左側
        /// </summary>
        public string QrCodeLeft { get; set; }

        /// <summary>
        /// QRCode 右側
        /// </summary>
        public string QrCodeRight { get; set; }

        /// <summary>列印資料（Base64 字串）PrinterType = 1，base64編碼的 XML 列印格式字串(mC-Print3 熱感應機專用)，正確且需要列印才會回傳。
        ///                                 PrinterType >= 2，base64編碼的 ESC/POS 列印格式字串，正確且需要列印才會回傳。
        ///                                 如何設定熱感應機及印出發票，請洽客服。
        ///                                 0元發票不會回傳此欄位</summary>
        /// <example>"base64stringdata"</example>
        public string Base64Data { get; set; } = "";

        /// <summary>
        /// 原始 JSON 回傳
        /// </summary>
        /// <example>{"Success": true, "InvoiceNumber": "1234567890", "InvoiceTime": 1609459200}</example>
        public string RawJson { get; set; }
    }
}