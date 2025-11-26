using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 前端呼叫 /api/invoice 時所需的請求 DTO
    /// </summary>
    public class CreateInvoiceDto
    {
        /// <summary>
        /// 訂單編號，跟廠商對接的參數
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
        [Required]
        public List<ProductItem> Items { get; set; }

        /// <summary>
        /// 買方統一編號，若無統編則請填 "0000000000"
        /// </summary>
        [Required]
        public string BuyerIdentifier { get; set; }

        /// <summary>
        /// 買方名稱，如公司或個人名稱
        /// </summary>
        [Required]
        public string BuyerName { get; set; }

        /// <summary>
        /// 買方地址（選填）
        /// </summary>
        public string BuyerAddress { get; set; }

        /// <summary>
        /// 買方電話（選填）
        /// </summary>
        public string BuyerTelephoneNumber { get; set; }

        /// <summary>
        /// 買方電子郵件（選填），若希望收到開立通知請填寫
        /// </summary>
        public string BuyerEmailAddress { get; set; }

        /// <summary>
        /// 總備註（選填），用於填寫額外說明
        /// </summary>
        public string MainRemark { get; set; }

        /// <summary>
        /// 載具類別（選填），如手機載具代碼
        /// </summary>
        public string CarrierType { get; set; }

        /// <summary>
        /// 載具識別碼（選填），如手機載具唯一碼
        /// </summary>
        public string CarrierId { get; set; }

        /// <summary>
        /// 捐贈碼（選填），若需捐贈請填此欄
        /// </summary>
        public string NPOBAN { get; set; }
    }
}