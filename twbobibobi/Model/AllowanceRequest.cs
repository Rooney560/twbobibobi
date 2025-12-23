/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceRequest.cs
   類別說明：折讓單 API 傳送的主要請求結構，封裝了折讓單開立所需的各種資料。
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace twbobibobi.Model
{
    /// <summary>
    /// 折讓單 API 傳送的主要請求結構
    /// </summary>
    /// <remarks>
    /// 此類別用來封裝所有向折讓單 API 發送請求時所需的資料。它包含訂單編號、買方資料、商品項目、稅金等資訊。
    /// 這些資料會被傳送至折讓單 API，用於生成折讓單。
    /// </remarks>
    public class AllowanceRequest
    {
        /// <summary>
        /// 折讓單編號，不可重複，不可超過16字 (必填)
        /// </summary>
        /// <example>ORD123456789</example>
        public string AllowanceNumber { get; set; }

        /// <summary>
        /// 折讓單日期，Ymd (必填)
        /// </summary>
        /// <example>20210618</example>
        public string AllowanceDate { get; set; }

        /// <summary>
        /// 折讓單種類，1:買方開立折讓證明單 2:賣方折讓證明通知單 (必填)
        /// </summary>
        /// <example>2</example>
        public int AllowanceType { get; set; } = 2;

        /// <summary>
        /// 買方統一編號，沒有則填 "0000000000" (必填)
        /// </summary>
        /// <example>0000000000</example>
        public string BuyerIdentifier { get; set; } = "0000000000";

        /// <summary>
        /// 買方名稱 (必填)
        /// </summary>
        /// <example>消費者</example>
        public string BuyerName { get; set; }

        /// <summary>
        /// 買方地址 (選填)
        /// </summary>
        /// <example>台北市信義路五段7號</example>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerAddress { get; set; }

        /// <summary>
        /// 買方電話 (選填)
        /// </summary>
        /// <example>0987654321</example>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerTelephoneNumber { get; set; }

        /// <summary>
        /// 買方電子信箱，不想寄信留空 (選填)
        /// </summary>
        /// <example>buyer@example.com</example>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuyerEmailAddress { get; set; }

        /// <summary>
        /// 商品陣列，最多9999筆 (必填)
        /// </summary>
        /// <example>
        /// [ { "OriginalInvoiceDate": 20210520, "OriginalInvoiceNumber": "NW93016392", "OriginalDescription": "超聲波清洗機", "Quantity": 2, "UnitPrice": "2180", "Amount": "4360", "Tax": 218, "TaxType": 1 }]
        /// </example>
        public List<ProductItem_Allowance> ProductItem { get; set; }
        /// <summary>
        /// 營業稅額 (必填)
        /// </summary>
        /// <example>50</example>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 總計 (必填)
        /// </summary>
        /// <example>1050</example>
        public decimal TotalAmount { get; set; }

    }
}