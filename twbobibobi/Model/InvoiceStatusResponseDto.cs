/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceStatusResponseDto.cs
   類別說明：發票狀態查詢結果 DTO，對應查詢 API 的回應內容。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System;
using System.Collections.Generic;

namespace twbobibobi.Model
{
    /// <summary>
    /// 發票狀態查詢結果 DTO，對應查詢 API 的回應內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝發票狀態查詢 API 的回應結果，
    /// 包含查詢的發票號碼、發票狀態、錯誤訊息等信息。
    /// </remarks>
    public class InvoiceStatusResponseDto
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
        /// 錯誤訊息（若失敗）
        /// </summary>
        /// <example>"Invoice not found"</example>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 發票狀態資料集合，對應多筆發票資料
        /// </summary>
        public List<InvoiceStatusDataResponseDto> Data { get; set; }

        /// <summary>
        /// 原始 JSON
        /// </summary>
        public string RawJson { get; set; }
    }

    /// <summary>
    /// 發票狀態中的明細項目，對應一筆商品或服務。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝發票狀態的明細項目，
    /// 包含查詢的發票號碼、發票類型、發票狀態、發票金額等信息。
    /// </remarks>
    public class InvoiceStatusDataResponseDto
    {
        /// <summary>
        /// 查詢的發票號碼
        /// </summary>
        /// <example>"INV123456789"</example>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// 發票類型
        /// NOT_FOUND：查無發票
        /// C0401：發票開立
        /// C0501：發票作廢
        /// C0701：發票註銷
        /// TYPE_ERROR：類型錯誤
        /// </summary>
        /// <example>"C0401"</example>
        public string Type { get; set; }

        /// <summary>
        /// 發票狀態
        /// 1：待處理
        /// 2：上傳中
        /// 3：已上傳
        /// 31：處理中
        /// 32：處理完成／待確認
        /// 91：錯誤
        /// 99：完成
        /// </summary>
        /// <example>"99"</example>
        public string Status { get; set; }

        /// <summary>
        /// 發票金額
        /// </summary>
        /// <example>"1000"</example>
        public string Total_amount { get; set; }
    }
}