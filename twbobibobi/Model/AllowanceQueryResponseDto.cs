/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceQueryResponseDto.cs
   類別說明：折讓單查詢結果 DTO，對應查詢 API 的回應內容。
   建立日期：2025-12-17
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */


using System.Collections.Generic;

namespace twbobibobi.Model
{
    /// <summary>
    /// 折讓單查詢結果 DTO，對應查詢 API 的回應內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝從折讓單查詢 API 回傳的結果，包含折讓單編號、折讓單狀態、錯誤訊息等資訊。
    /// </remarks>
    public class AllowanceQueryResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤訊息（若失敗）
        /// </summary>
        /// <example>"Allowance not found"</example>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 折讓單內容（若正確）
        /// </summary>
        public List<AllowanceQueryDataResponseDto> Data { get; set; }

        /// <summary>
        /// 原始 JSON 回應
        /// </summary>
        /// <example>
        /// "{\"code\": 0, \"msg\": \"Success\", \"data\": {\"AllowanceNumber\": \"INV123456789\", \"Type\": \"C0401\"}}"
        /// </example>
        public string RawJson { get; set; }
    }

    /// <summary>
    /// 折讓單內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝折讓單內容，
    /// 包含查詢的折讓單編號、折讓單類型、折讓單狀態、折讓單日期等信息。
    /// </remarks>
    public class AllowanceQueryDataResponseDto
    {
        /// <summary>
        /// 查詢的折讓單編號
        /// </summary>
        /// <example>"123456789"</example>
        public string Allowance_number { get; set; }

        /// <summary>
        /// 折讓單類型
        /// D0401：B2C 存證折讓單開立
        /// D0501：B2C 存證折讓單作廢
        /// D0701：B2C 存證折讓單註銷
        /// B0401：B2B 存證折讓單開立
        /// B0501：B2B 存證折讓單作廢
        /// B0101：B2B 交換折讓單開立
        /// B0102：B2B 交換折讓單開立(接收確認)
        /// B0201：B2B 交換折讓單作廢
        /// B0202：B2B 交換折讓單作廢(接收確認)
        /// 詳情請參考財政部的電子發票資料交換標準訊息建置指引(MIG)文件
        /// </summary>
        /// <example>"D0401"</example>
        public string Invoice_type { get; set; }

        /// <summary>
        /// 上傳至財政部的狀態
        /// 1：待處理
        /// 2：上傳中
        /// 3：已上傳
        /// 31：處理中
        /// 32：處理完成／待確認
        /// 91：錯誤
        /// 99：完成
        /// </summary>
        /// <example>"99"</example>
        public string Invoice_status { get; set; }

        /// <summary>
        /// 折讓單日期
        /// 格式：YYYYMMDD
        /// </summary>
        /// <example>"20161028"</example>
        public string Allowance_date { get; set; }

        /// <summary>
        /// 折讓單種類，1:買方開立折讓證明單 2:賣方折讓證明通知單
        /// </summary>
        /// <example>"2"</example>
        public string Allowance_type { get; set; }

        /// <summary>
        /// 買方統一編號
        /// 沒有統一編號會回傳 0000000000
        /// </summary>
        /// <example>"0000000000"</example>
        public string Buyer_identifier { get; set; }

        /// <summary>
        /// 買方名稱
        /// </summary>
        /// <example>蕭XX</example>
        public string Buyer_name { get; set; }

        /// <summary>
        /// 買方郵遞區號
        /// </summary>
        /// <example>911</example>
        public string Buyer_zip { get; set; } = "0";

        /// <summary>
        /// 買方地址
        /// </summary>
        /// <example>台北市信義路五段7號</example>
        public string Buyer_address { get; set; } = "";

        /// <summary>
        /// 買方電話
        /// </summary>
        /// <example>0987654321</example>
        public string Buyer_telephone_number { get; set; } = "";

        /// <summary>
        /// 買方電子信箱
        /// </summary>
        /// <example>buyer@example.com</example>
        public string Buyer_email_address { get; set; } = "";

        /// <summary>
        /// 營業稅額
        /// </summary>
        /// <example>"0"</example>
        public string Tax_amount { get; set; }

        /// <summary>
        /// 金額合計(不含稅)
        /// </summary>
        /// <example>"100"</example>
        public string Total_amount { get; set; }

        /// <summary>
        /// 作廢時間，Unix 時間戳記
        /// </summary>
        /// <example>"0"</example>
        public string Cancel_date { get; set; }

        /// <summary>
        /// 明細的單價及小計為 0:未稅價 1:含稅價
        /// </summary>
        /// <example>"1"</example>
        public string Detail_vat { get; set; }

        /// <summary>
        /// 建立時間，Unix 時間戳記
        /// </summary>
        /// <example>"1669859798"</example>
        public string Create_date { get; set; }

        /// <summary>
        /// 商品陣列，最多 9999 筆
        /// </summary>
        public List<AllowanceProductItem> Product_item { get; set; }

        /// <summary>
        /// 未處理的排程陣列，例如：等待改成「折讓單作廢」
        /// </summary>
        public List<AllowanceWait> Wait { get; set; }
    }

    /// <summary>
    /// 折讓單內容商品項目。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝折讓單的商品項目，包含商品的描述、單價、數量等。
    /// </remarks>
    public class AllowanceProductItem
    {
        /// <summary>
        /// 原發票號碼　
        /// </summary>
        /// <example>"AA11110053"</example>
        public string Original_invoice_number { get; set; }

        /// <summary>
        /// 原發票日期 格式：YYYYMMDD　
        /// </summary>
        /// <example>"20250901"</example>
        public string Original_invoice_date { get; set; }

        /// <summary>
        /// 課稅別　
        /// 1：應稅　2：零稅率　3：免稅　4：應稅(特種稅率)　9：混合應稅與免稅或零稅率(限訊息C0401使用)
        /// </summary>
        /// <example>"1"</example>
        public string Tax_type { get; set; } = "1";

        /// <summary>
        /// 品名，不可超過256字　
        /// </summary>
        /// <example>"南非砂糖橘小可愛A_#12"</example>
        public string Description { get; set; }

        /// <summary>
        /// 單價，可到小數7位數　
        /// </summary>
        /// <example>"100"</example>
        public string Unit_price { get; set; }

        /// <summary>
        /// 數量，可到小數7位數
        /// </summary>
        /// <example>"1"</example>
        public string Quantity { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        /// <example>"盞"</example>
        public string Unit { get; set; }

        /// <summary>
        /// 小計，可到小數7位數
        /// </summary>
        /// <example>"100"</example>
        public string Amount { get; set; }

        /// <summary>
        /// 稅金，整數
        /// </summary>
        /// <example>"7"</example>
        public string Tax { get; set; }
    }

    /// <summary>
    /// 折讓單未處理的排程資訊。
    /// </summary>
    /// <remarks>
    /// 這個類別用來封裝折讓單處理中的狀態與時間，包含折讓單類型、創建時間等資訊。
    /// </remarks>
    public class AllowanceWait
    {
        /// <summary>
        /// 改成折讓單類型
        /// D0401：B2C 存證折讓單開立
        /// D0501：B2C 存證折讓單作廢
        /// D0701：B2C 存證折讓單註銷
        /// B0401：B2B 存證折讓單開立
        /// B0501：B2B 存證折讓單作廢
        /// B0101：B2B 交換折讓單開立
        /// B0102：B2B 交換折讓單開立(接收確認)
        /// B0201：B2B 交換折讓單作廢
        /// B0202：B2B 交換折讓單作廢(接收確認)
        /// 詳情請參考財政部的電子發票資料交換標準訊息建置指引(MIG)文件
        /// </summary>
        /// <example>"C0501"</example>
        public string Invoice_type { get; set; }

        /// <summary>
        /// 建立時間，Unix 時間戳記
        /// </summary>
        /// <example>"1669859798"</example>
        public string Create_date { get; set; }
    }
}