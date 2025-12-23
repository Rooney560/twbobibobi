/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceWrapperInput.cs
   類別說明：電子發票折讓單的輸入資料封裝，包含買方資訊、商品項目、金額等。
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 用來封裝電子發票折讓單的輸入資料
    /// </summary>
    /// <remarks>
    /// 這個類別將用來將折讓單所需的所有資料封裝起來，包括買方資料、商品項目、折讓金額、稅金等。
    /// 此資料將會傳遞給 API 用來創建折讓單。
    /// </remarks>
    public class AllowanceWrapperInput
    {
        /// <summary>
        /// 折讓單編號，不可重複，不可超過16字
        /// </summary>
        /// <example>3821061800001</example>
        public string AllowanceNumber { get; set; }

        /// <summary>四種發票開立情境</summary>
        public int Scenario { get; set; }

        /// <summary>
        /// 折讓單日期，Ymd 格式（例如：20210618）
        /// </summary>
        /// <example>20210618</example>
        public string AllowanceDate { get; set; }

        /// <summary>
        /// 折讓單種類，1:買方開立折讓證明單 2:賣方折讓證明通知單
        /// </summary>
        /// <example>2</example>
        public int AllowanceType { get; set; }

        /// <summary>
        /// 買方統一編號，若沒有則填入 "0000000000"
        /// </summary>
        /// <example>0000000000</example>
        public string BuyerIdentifier { get; set; } = "0000000000";

        /// <summary>
        /// 買方名稱
        /// </summary>
        /// <example>蕭XX</example>
        public string BuyerName { get; set; }

        /// <summary>
        /// 買方地址
        /// </summary>
        /// <example>台北市信義路五段7號</example>
        public string BuyerAddress { get; set; } = "";

        /// <summary>
        /// 買方電話
        /// </summary>
        /// <example>0987654321</example>
        public string BuyerTelephoneNumber { get; set; } = "";

        /// <summary>
        /// 買方電子信箱
        /// </summary>
        /// <example>buyer@example.com</example>
        public string BuyerEmailAddress { get; set; } = "";

        /// <summary>
        /// 商品項目列表
        /// </summary>
        public List<ProductItem_Allowance> Items { get; set; }

        /// <summary>
        /// 營業稅額
        /// </summary>
        /// <example>218</example>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 金額合計（不含稅）
        /// </summary>
        /// <example>4360</example>
        public decimal TotalAmount { get; set; }
    }
}