using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 發票 API 傳送的主要請求結構
    /// </summary>
    public class InvoiceRequest
    {
        /// <summary>訂單編號，不可重複，不可超過40字 (必填)</summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 指定字軌開立 API 代碼(選填)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TrackApiCode { get; set; }

        /// <summary>買方統一編號，沒有則填 0000000000 (必填)</summary>
        public string BuyerIdentifier { get; set; }

        /// <summary>買方名稱 (必填)</summary>
        public string BuyerName { get; set; }

        /// <summary>買方地址 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerAddress { get; set; }

        /// <summary>買方電話 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerTelephoneNumber { get; set; }

        /// <summary>買方電子信箱，不想寄信留空 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerEmailAddress { get; set; }

        /// <summary>總備註，不可超過200字 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MainRemark { get; set; }

        /// <summary>載具類別 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierType { get; set; }

        /// <summary>載具明碼 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierId1 { get; set; }

        /// <summary>載具隱碼 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CarrierId2 { get; set; }

        /// <summary>捐贈碼 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NPOBAN { get; set; }

        /// <summary>商品陣列，最多9999筆 (必填)</summary>
        public List<ProductItem> ProductItem { get; set; }

        /// <summary>應稅銷售額合計 (必填)</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>免稅銷售額合計 (必填)</summary>
        public decimal FreeTaxSalesAmount { get; set; }

        /// <summary>零稅率銷售額合計 (必填)</summary>
        public decimal ZeroTaxSalesAmount { get; set; }

        /// <summary>
        /// 課稅別 1:應稅 2:零稅率 3:免稅 4:特種 9:混合(限C0401)
        /// (必填)
        /// </summary>
        public int TaxType { get; set; }

        /// <summary>稅率，5% → "0.05" (必填)</summary>
        public string TaxRate { get; set; }

        /// <summary>營業稅額 (必填)</summary>
        public decimal TaxAmount { get; set; }

        /// <summary>總計 (必填)</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 通關方式註記 (選填)
        /// 1:非經海關出口 2:經海關出口
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CustomsClearanceMark { get; set; }

        /// <summary>
        /// 零稅率原因 (選填)
        /// 71～79
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ZeroTaxRateReason { get; set; }

        /// <summary>品牌名稱 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BrandName { get; set; }

        /// <summary>
        /// 明細單價及小計是否含稅(選填)
        /// 0:未稅 1:含稅
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? DetailVat { get; set; }

        /// <summary>
        /// 明細小計是否四捨五入(選填)
        /// 0:小數精度7位 1:四捨五入整數
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? DetailAmountRound { get; set; }

        /// <summary>熱感應機型號代碼 (選填)</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PrinterType { get; set; }

        /// <summary>
        /// 熱感應機編碼
        /// 1:BIG5 2:GBK 3:UTF-8
        /// (選填)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PrinterLang { get; set; }

        /// <summary>
        /// 熱感應機是否列印明細
        /// 1:列印(預設) 0:不列印
        /// (選填)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PrintDetail { get; set; }
    }
}