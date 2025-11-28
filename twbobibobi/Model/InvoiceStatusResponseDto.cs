using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 發票狀態查詢結果 DTO，對應查詢 API 的回應內容。
    /// </summary>
    public class InvoiceStatusResponseDto
    {
        /// <summary>是否成功</summary>
        public bool Success { get; set; }

        /// <summary>錯誤訊息（若失敗）</summary>
        public string ErrorMessage { get; set; }

        /// <summary>查詢的發票號碼</summary>
        public string InvoiceNumber { get; set; }

        /// <summary>發票類型
        /// NOT_FOUND：查無發票
        /// C0401：發票開立
        /// C0501：發票作廢
        /// C0701：發票註銷
        /// TYPE_ERROR：類型錯誤
        /// </summary>
        public string Type { get; set; }

        /// <summary>發票狀態
        /// 1：待處理
        /// 2：上傳中
        /// 3：已上傳
        /// 31：處理中
        /// 32：處理完成／待確認
        /// 91：錯誤
        /// 99：完成
        /// </summary>
        public string Status { get; set; }

        /// <summary>開立日期（若有）</summary>
        public string InvoiceDate { get; set; } = "";

        /// <summary>原始 JSON</summary>
        public string RawJson { get; set; }
    }

}