/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：CancelInvoiceResponseDto.cs
   類別說明：發票作廢結果 DTO，對應作廢 API 的回應內容。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

namespace twbobibobi.Model
{
    /// <summary>
    /// 發票作廢結果 DTO，對應作廢 API 的回應內容。
    /// </summary>
    public class CancelInvoiceResponseDto
    {
        /// <summary>是否成功</summary>
        public bool Success { get; set; }

        /// <summary>錯誤代碼</summary>
        public string Code { get; set; }

        /// <summary>錯誤訊息（若失敗）</summary>
        public string ErrorMessage { get; set; }

        /// <summary>被作廢的發票號碼</summary>
        public string CancelInvoiceNumber { get; set; }

        /// <summary>作廢時間（若有）</summary>
        public string CancelTime { get; set; }

        /// <summary>原始 JSON</summary>
        public string RawJson { get; set; }
    }

}