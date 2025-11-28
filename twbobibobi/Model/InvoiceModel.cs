using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// 此檔案定義發票模型，用於封裝發票相關資料
namespace twbobibobi.Model
{
    /// <summary>
    /// 發票主模型，包含發票抬頭、編號、金額、買賣方資訊等
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>商家 Logo 圖片 URL</summary>
        public string LogoUrl { get; set; } = "https://bobibobi.tw/Temples/images/Logo_no.png";

        /// <summary>台北時間(現在)</summary>
        public DateTime dtNow { get; set; }

        /// <summary>中華民國年 (三位數字，例如 "114" )</summary>
        public string Year { get; set; }

        /// <summary>月份 (兩位數字，例如 "05" )</summary>
        public string Month { get; set; }

        /// <summary>發票號碼，不含連字符</summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// 發票號碼（在第 3 個字元後自動插入連字符），方便顯示或列印
        /// </summary>
        public string InvoiceNumberWithDash
            => !string.IsNullOrWhiteSpace(InvoiceNumber) && InvoiceNumber.Length > 2
                ? InvoiceNumber.Insert(2, "-")
                : InvoiceNumber;

        /// <summary>開立日期 (格式 yyyy-MM-dd)</summary>
        public string Date { get; set; }

        /// <summary>開立時間 (格式 HH:mm:ss)</summary>
        public string Time { get; set; }

        /// <summary>隨機碼</summary>
        public string RandomCode { get; set; }

        /// <summary>發票總金額</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>賣方統一編號</summary>
        public string SellerTaxId { get; set; }

        /// <summary>買方統一編號</summary>
        public string BuyerTaxId { get; set; } = "";

        /// <summary>賣方名稱</summary>
        public string SellerName { get; set; }

        /// <summary>賣方地址</summary>
        public string SellerAddress { get; set; }

        /// <summary>訂單編號 格式 yyyyMMddHHmmssfff，用於追蹤與廠商訂單</summary>
        public string OrderId { get; set; }

        /// <summary>訂單編號 格式 宮廟縮寫 + 數字(N位數)，用於客戶與廟方追蹤產品</summary>
        public string NumString { get; set; }

        /// <summary>Bar code 內容</summary>
        public string BarcodeStr { get; set; }

        /// <summary>QR code 左邊內容</summary>
        public string Qrcode_leftStr { get; set; }

        /// <summary>QR code 右邊內容</summary>
        public string Qrcode_rightStr { get; set; }

        /// <summary>發票明細項目清單</summary>
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }


    /// <summary>
    /// 發票明細項目模型，對應一筆商品或服務
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>商品或服務描述</summary>
        public string Description { get; set; }

        /// <summary>品名</summary>
        public string ProductName { get; set; }

        /// <summary>數量</summary>
        public int Quantity { get; set; }

        /// <summary>單價</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>是否課稅 (顯示 TX)</summary>
        public bool Taxable { get; set; }
    }
}