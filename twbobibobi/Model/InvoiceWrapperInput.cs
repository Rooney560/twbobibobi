using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 給共用發票處理器使用的輸入參數封裝類別
    /// </summary>
    public class InvoiceWrapperInput
    {
        /// <summary>訂單編號，跟廠商對接的參數</summary>
        public string OrderId { get; set; }

        /// <summary>四種發票開立情境</summary>
        public InvoiceIssueScenario Scenario { get; set; }

        /// <summary>商品項目列表</summary>
        public List<ProductItem> Items { get; set; }

        /// <summary>統一編號（選填，預設為 "0000000000"）</summary>
        public string BuyerIdentifier { get; set; }

        /// <summary>公司或個人名稱</summary>
        public string BuyerName { get; set; }

        /// <summary>地址（選填）</summary>
        public string BuyerAddress { get; set; }

        /// <summary>電話（選填）</summary>
        public string BuyerTelephoneNumber { get; set; }

        /// <summary>Email（選填）</summary>
        public string BuyerEmailAddress { get; set; }

        /// <summary>備註說明（選填）</summary>
        public string MainRemark { get; set; }

        /// <summary>載具類型（選填）</summary>
        public string CarrierType { get; set; }

        /// <summary>載具代碼（選填）</summary>
        public string CarrierId { get; set; }

        /// <summary>捐贈碼（選填）</summary>
        public string NPOBAN { get; set; }
    }
}