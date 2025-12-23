/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：ProductItem.cs
   類別說明：單一商品項目的資料結構，用於電子發票與折讓單的明細內容。
   建立日期：2025-11-28
   建立人員：Rooney

 * 修改記錄：2025-12-12 新增折讓單明細內容
   目前維護人員：Rooney
   =================================================================================================== */
using Newtonsoft.Json;

namespace twbobibobi.Model
{
    /// <summary>
    /// 電子發票單一商品項目的資料結構
    /// </summary>
    public class ProductItem
    {
        /// <summary>品名，不可超過256字 (必填)</summary>
        public string Description { get; set; }

        /// <summary>數量，小數精度7位 (必填)</summary>
        public decimal Quantity { get; set; }

        /// <summary>單位，不可超過6字 (必填)</summary>
        public string Unit { get; set; }

        /// <summary>單價，小數精度7位 (必填)</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>小計，小數精度7位 (必填)</summary>
        public decimal Amount { get; set; }

        /// <summary>備註，不可超過40字 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Remark { get; set; }

        /// <summary>課稅別 1:應稅 2:零稅率 3:免稅</summary>
        public int TaxType { get; set; }
    }

    /// <summary>
    /// 折讓單明細商品項目
    /// </summary>
    public class ProductItem_Allowance
    {
        /// <summary>
        /// 原發票號碼
        /// </summary>
        /// <example>NW93016392</example>
        public string OriginalInvoiceNumber { get; set; }

        /// <summary>
        /// 原發票日期（Ymd 格式）
        /// </summary>
        /// <example>20210520</example>
        public int OriginalInvoiceDate { get; set; }

        /// <summary>
        /// 原品名
        /// </summary>
        /// <example>超聲波清洗機</example>
        public string OriginalDescription { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        /// <example>2</example>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 單價（不含稅）
        /// </summary>
        /// <example>2180</example>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 小計（不含稅）
        /// </summary>
        /// <example>4360</example>
        public decimal Amount { get; set; }

        /// <summary>
        /// 稅金
        /// </summary>
        /// <example>218</example>
        public decimal Tax { get; set; }

        /// <summary>
        /// 課稅別：1:應稅 2:零稅率 3:免稅
        /// </summary>
        /// <example>1</example>
        public int TaxType { get; set; }
    }
}