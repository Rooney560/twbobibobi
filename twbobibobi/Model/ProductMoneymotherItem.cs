using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 文創商品錢母擺件項目資料模型
    /// </summary>
    public class ProductMoneymotherItem
    {
        /// <summary>
        /// 商品代碼 (A~J)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 商品代碼 (1~11)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// 購買數量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 商品圖片網址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 小計金額
        /// </summary>
        public int SubTotal => Cost * Count;
    }
}