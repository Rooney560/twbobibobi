/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceStatusResponseDto.cs
   類別說明：折讓單狀態查詢結果 DTO，對應查詢 API 的回應內容。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System.Collections.Generic;

namespace twbobibobi.Model
{
    /// <summary>
    /// 折讓單狀態查詢結果 DTO，對應查詢 API 的回應內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝折讓單狀態查詢 API 的回應結果，
    /// 包含查詢的折讓單編號、折讓單狀態、錯誤訊息等信息。
    /// </remarks>
    public class AllowanceStatusResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤代碼
        /// </summary>
        /// <example>"Allowance not found"</example>
        public string Code { get; set; }

        /// <summary>
        /// 錯誤訊息（若失敗）
        /// </summary>
        /// <example>"Allowance not found"</example>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 折讓單狀態資料集合，對應多筆折讓單資料
        /// </summary>
        public List<AllowanceStatusDataResponseDto> Data { get; set; }

        /// <summary>
        /// 原始 JSON
        /// </summary>
        public string RawJson { get; set; }
    }


    /// <summary>
    /// 折讓單狀態中的明細項目，對應一筆商品或服務。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝折讓單狀態的明細項目，
    /// 包含查詢的折讓單編號、折讓單類型、折讓單狀態、營業稅額、未稅總計等信息。
    /// </remarks>
    public class AllowanceStatusDataResponseDto
    {
        /// <summary>
        /// 查詢的折讓單編號
        /// </summary>
        /// <example>"123456789"</example>
        public string AllowanceNumber { get; set; }

        /// <summary>
        /// 折讓單類型
        /// NOT_FOUND：查無折讓單
        /// D0401：折讓單開立
        /// D0501：折讓單作廢
        /// TYPE_ERROR：類型錯誤
        /// </summary>
        /// <example>"D0401"</example>
        public string Type { get; set; }

        /// <summary>
        /// 折讓單狀態
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
        /// 營業稅額
        /// </summary>
        /// <example>"54"</example>
        public string Tax_amount { get; set; }

        /// <summary>
        /// 未稅總計
        /// </summary>
        /// <example>"1000"</example>
        public string Total_amount { get; set; }
    }
}