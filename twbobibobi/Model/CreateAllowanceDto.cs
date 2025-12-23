/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：CreateAllowanceDto.cs
   類別說明：前端呼叫 /api/invoice 時所需的請求 DTO，用來封裝發票的輸入資料。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace twbobibobi.Model
{
    /// <summary>
    /// 前端呼叫 /Api/AllowanceAPI.aspx 時所需的請求 DTO
    /// </summary>
    /// <remarks>
    /// 這個類別是用來封裝前端請求折讓單的資料。
    /// 並且會被用來發送至後端以開立折讓單。
    /// </remarks>
    public class CreateAllowanceDto
    {
        /// <summary>
        /// 折讓單編號，不可重複，不可超過16字
        /// </summary>
        /// <example>3821061800001</example>
        [Required]
        public string AllowanceNumber { get; set; }

        /// <summary>
        /// 折讓單日期，Ymd 格式（例如：20210618）
        /// </summary>
        /// <example>20210618</example>
        [Required]
        public string AllowanceDate { get; set; }

        /// <summary>
        /// 折讓單種類，1:買方開立折讓證明單 2:賣方折讓證明通知單
        /// </summary>
        /// <example>2</example>
        [Required]
        public int AllowanceType { get; set; } = 2;

        /// <summary>
        /// 買方統一編號，若沒有則填入 "0000000000"
        /// </summary>
        /// <example>0000000000</example>
        [Required]
        public string BuyerIdentifier { get; set; } = "0000000000";

        /// <summary>
        /// 買方名稱
        /// </summary>
        /// <example>蕭XX</example>
        [Required]
        public string BuyerName { get; set; }

        /// <summary>
        /// 買方地址
        /// </summary>
        /// <example>台北市信義路五段7號</example>
        [Required]
        public string BuyerAddress { get; set; } = "";

        /// <summary>
        /// 買方電話
        /// </summary>
        /// <example>0987654321</example>
        [Required]
        public string BuyerTelephoneNumber { get; set; } = "";

        /// <summary>
        /// 買方電子信箱
        /// </summary>
        /// <example>buyer@example.com</example>
        [Required]
        public string BuyerEmailAddress { get; set; } = "";

        /// <summary>
        /// 商品項目列表
        /// </summary>
        /// <example>
        /// [ { "OriginalInvoiceDate": 20210520, "OriginalInvoiceNumber": "NW93016392", "OriginalDescription": "超聲波清洗機", "Quantity": 2, "UnitPrice": "2180", "Amount": "4360", "Tax": 218, "TaxType": 1 } ]
        /// </example>
        [Required]
        public List<ProductItem_Allowance> ProductItem { get; set; }

        /// <summary>
        /// 營業稅額
        /// </summary>
        /// <example>218</example>
        [Required]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 金額合計（不含稅）
        /// </summary>
        /// <example>4360</example>
        [Required]
        public decimal TotalAmount { get; set; }
    }
}