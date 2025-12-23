/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceQueryResponseDto.cs
   類別說明：發票查詢結果 DTO，對應查詢 API 的回應內容。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace twbobibobi.Model
{
    /// <summary>
    /// 發票查詢結果 DTO，對應查詢 API 的回應內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝從發票查詢 API 回傳的結果，包含發票號碼、發票狀態、錯誤訊息等資訊。
    /// </remarks>
    public class InvoiceQueryResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤訊息（若失敗）
        /// </summary>
        /// <example>"Invoice not found"</example>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 發票內容（若正確）
        /// </summary>
        public List<InvoiceQueryDataResponseDto> Data { get; set; }

        /// <summary>
        /// 原始 JSON 回應
        /// </summary>
        /// <example>
        /// "{\"code\": 0, \"msg\": \"Success\", \"data\": {\"InvoiceNumber\": \"INV123456789\", \"Type\": \"C0401\"}}"
        /// </example>
        public string RawJson { get; set; }
    }

    /// <summary>
    /// 發票內容。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝發票內容，
    /// 包含查詢的發票號碼、發票類型、發票狀態、發票日期等信息。
    /// </remarks>
    public class InvoiceQueryDataResponseDto
    {
        /// <summary>
        /// 查詢的發票號碼
        /// </summary>
        /// <example>"INV123456789"</example>
        public string Invoice_number { get; set; }

        /// <summary>
        /// 發票類型
        /// C0401：B2C 存證發票開立
        /// C0501：B2C 存證發票作廢
        /// C0701：B2C 存證發票註銷
        /// A0401：B2B 存證發票開立
        /// A0501：B2B 存證發票作廢
        /// A0101：B2B 交換發票開立
        /// A0102：B2B 交換發票開立(接收確認)
        /// A0201：B2B 交換發票作廢
        /// A0202：B2B 交換發票作廢(接收確認)
        /// A0301：B2B 交換發票退回
        /// A0302：B2B 交換發票退回(接收確認)
        /// 詳情請參考財政部的電子發票資料交換標準訊息建置指引(MIG)文件
        /// </summary>
        /// <example>"C0401"</example>
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
        /// 發票日期
        /// 格式：YYYYMMDD
        /// </summary>
        /// <example>"20161028"</example>
        public string Invoice_date { get; set; }

        /// <summary>
        /// 發票時間
        /// 格式：HH:mm:ss
        /// </summary>
        /// <example>"21:21:21"</example>
        public string Invoice_time { get; set; }

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
        /// 應稅銷售額合計
        /// </summary>
        /// <example>"100"</example>
        public string Sales_amount { get; set; }

        /// <summary>
        /// 免稅銷售額合計
        /// </summary>
        /// <example>"0"</example>
        public string Free_tax_sales_amount { get; set; }

        /// <summary>
        /// 零稅率銷售額合計
        /// </summary>
        /// <example>"0"</example>
        public string Zero_tax_sales_amount { get; set; }

        /// <summary>
        /// 課稅別　
        /// 1：應稅　2：零稅率　3：免稅　4：應稅(特種稅率)　9：混合應稅與免稅或零稅率(限訊息C0401使用)
        /// </summary>
        /// <example>"1"</example>
        public string Tax_type { get; set; } = "1";

        /// <summary>
        /// 稅率，為5%時本欄位值為0.05　
        /// </summary>
        /// <example>"0.05"</example>
        public string Tax_rate { get; set; }

        /// <summary>
        /// 營業稅額
        /// </summary>
        /// <example>"0"</example>
        public string Tax_amount { get; set; }

        /// <summary>
        /// 總計
        /// </summary>
        /// <example>"100"</example>
        public string Total_amount { get; set; }

        /// <summary>
        /// 列印註記
        /// </summary>
        /// <example>"Y"</example>
        public string Print_mark { get; set; }

        /// <summary>
        /// 隨機碼
        /// </summary>
        /// <example>"2729"</example>
        public string Random_number { get; set; }

        /// <summary>
        /// 總備註
        /// </summary>
        public string Main_remark { get; set; }

        /// <summary>
        /// 通關方式註記，若為非零稅率發票，則此欄位為 0 。
        /// 1:非經海關出口 2:經海關出口
        /// </summary>
        /// <example>"0"</example>
        public string Customs_clearance_mark { get; set; }

        /// <summary>
        /// 載具類別，手機條碼 3J0002，自然人憑證條碼 CQ0001
        /// </summary>
        public string Carrier_type { get; set; }

        /// <summary>
        /// 載具明碼
        /// </summary>
        public string Carrier_id1 { get; set; }

        /// <summary>
        /// 載具隱碼
        /// </summary>
        public string Carrier_id2 { get; set; }

        /// <summary>
        /// 捐贈碼
        /// </summary>
        public string Npoban { get; set; }

        /// <summary>
        /// 作廢時間，Unix 時間戳記
        /// </summary>
        /// <example>"0"</example>
        public string Cancel_date { get; set; }

        /// <summary>
        /// 獎項代碼，請參考 [獎項定義] API
        /// </summary>
        /// <example>"0"</example>
        public string Invoice_lottery { get; set; }

        /// <summary>
        /// 訂單編號
        /// </summary>
        /// <example>"202512121212"</example>
        public string Order_id { get; set; }

        /// <summary>
        /// 明細的單價及小計為 0:未稅價 1:含稅價
        /// </summary>
        /// <example>"1"</example>
        public string Detail_vat { get; set; }

        /// <summary>
        /// 明細的小計 0:直接加總 1:先四捨五入再加總
        /// </summary>
        /// <example>"0"</example>
        public string Detail_amount_round { get; set; }

        /// <summary>
        /// 建立時間，Unix 時間戳記
        /// </summary>
        /// <example>"1669859798"</example>
        public string Create_date { get; set; }

        /// <summary>
        /// 商品陣列，最多 9999 筆
        /// </summary>
        public List<InvoiceProductItem> Product_item { get; set; }

        /// <summary>
        /// 未處理的排程陣列，例如：等待改成「發票作廢」
        /// </summary>
        public List<InvoiceWait> Wait { get; set; }
    }

    /// <summary>
    /// 發票內容商品項目。
    /// </summary>
    /// <remarks>
    /// 這個 DTO 類別用來封裝發票的商品項目，包含商品的描述、單價、數量等。
    /// </remarks>
    public class InvoiceProductItem
    {
        /// <summary>
        /// 課稅別　
        /// 1：應稅　2：零稅率　3：免稅　4：應稅(特種稅率)　9：混合應稅與免稅或零稅率(限訊息C0401使用)
        /// </summary>
        /// <example>"1"</example>
        public string Tax_type { get; set; } = "1";

        /// <summary>
        /// 品名，不可超過256字　
        /// </summary>
        /// <example>"紅利儲值"</example>
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
        /// 備註，不可超過40字
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 發票未處理的排程資訊。
    /// </summary>
    /// <remarks>
    /// 這個類別用來封裝發票處理中的狀態與時間，包含發票類型、創建時間等資訊。
    /// </remarks>
    public class InvoiceWait
    {
        /// <summary>
        /// 改成發票類型
        /// C0401：B2C 存證發票開立
        /// C0501：B2C 存證發票作廢
        /// C0701：B2C 存證發票註銷
        /// A0401：B2B 存證發票開立
        /// A0501：B2B 存證發票作廢
        /// A0101：B2B 交換發票開立
        /// A0102：B2B 交換發票開立(接收確認)
        /// A0201：B2B 交換發票作廢
        /// A0202：B2B 交換發票作廢(接收確認)
        /// A0301：B2B 交換發票退回
        /// A0302：B2B 交換發票退回(接收確認)
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