/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：CreateInvoiceDto.cs
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
    /// 前端呼叫 /Api/InvoiceAPI.aspx 時所需的請求 DTO
    /// </summary>
    /// <remarks>
    /// 這個類別是用來封裝前端請求發票的資料。它包含訂單編號、發票開立情境、商品清單、買方資訊等，
    /// 並且會被用來發送至後端以開立發票。
    /// </remarks>
    public class CreateInvoiceDto
    {
        /// <summary>
        /// 訂單編號，跟廠商對接的參數，不可重複，不可超過40字
        /// </summary>
        [Required]
        public string OrderId { get; set; }

        /// <summary>
        /// 發票開立情境：StandardPrint、MobileCarrier、Donation 或 TaxIdPrint
        /// </summary>
        [Required]
        public InvoiceIssueScenario Scenario { get; set; }

        /// <summary>
        /// 商品清單，至少一筆商品
        /// </summary>
        /// <example>
        /// [ { "Description": "商品A", "Quantity": 1, "Unit": "個", "UnitPrice": 100, "Amount": 100, "TaxType": 1 } ]
        /// </example>
        [Required]
        public List<ProductItem> Items { get; set; }

        /// <summary>
        /// 買方統一編號，若無統編則請填 "0000000000"
        /// </summary>
        /// <example>0000000000</example>
        [Required]
        public string BuyerIdentifier { get; set; } = "0000000000";

        /// <summary>
        /// 買方名稱，如公司或個人名稱
        /// </summary>
        /// <example>蕭XX</example>
        [Required]
        public string BuyerName { get; set; }

        /// <summary>
        /// 買方地址（選填）
        /// </summary>
        /// <example>台北市信義路五段7號</example>
        public string BuyerAddress { get; set; } = "";

        /// <summary>
        /// 買方電話（選填）
        /// </summary>
        /// <example>0987654321</example>
        public string BuyerTelephoneNumber { get; set; } = "";

        /// <summary>
        /// 買方電子郵件（選填），若希望收到開立通知請填寫
        /// </summary>
        /// <example>buyer@example.com</example>
        public string BuyerEmailAddress { get; set; } = "";

        /// <summary>
        /// 總備註（選填），用於填寫額外說明
        /// </summary>
        /// <example>感謝您的支持，期待再次合作。</example>
        public string MainRemark { get; set; }

        /// <summary>
        /// 載具類別（選填），如手機載具代碼
        /// </summary>
        /// <example>3J0002</example>
        public string CarrierType { get; set; }

        /// <summary>
        /// 載具識別碼（選填），如手機載具唯一碼
        /// </summary>
        /// <example>1234567890</example>
        public string CarrierId { get; set; }

        /// <summary>
        /// 捐贈碼（選填），若需捐贈請填此欄
        /// </summary>
        /// <example>987654321</example>
        public string NPOBAN { get; set; }
    }
}