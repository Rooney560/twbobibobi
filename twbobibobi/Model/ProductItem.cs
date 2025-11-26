using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 單一商品項目的資料結構
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
}