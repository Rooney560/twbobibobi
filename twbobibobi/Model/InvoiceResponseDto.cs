using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 對應發票 API 回傳的 JSON 結構
    /// 來源：/Api/InvoiceAPI.aspx 回傳格式
    /// </summary>
    public class InvoiceResponseDto
    {
        /// <summary>是否成功</summary>
        public bool Success { get; set; }

        /// <summary>錯誤訊息</summary>
        public string ErrorMessage { get; set; } = "";

        /// <summary>發票號碼</summary>
        public string InvoiceNumber { get; set; }

        /// <summary>發票開立時間，Unix 時間戳記，正確才會回傳</summary>
        public long InvoiceTime { get; set; }

        /// <summary>發票隨機碼</summary>
        public string RandomNumber { get; set; }

        /// <summary>條碼內容</summary>
        public string Barcode { get; set; }

        /// <summary>QRCode 左側</summary>
        public string QrCodeLeft { get; set; }

        /// <summary>QRCode 右側</summary>
        public string QrCodeRight { get; set; }

        /// <summary>列印資料（Base64 字串）PrinterType = 1，base64編碼的 XML 列印格式字串(mC-Print3 熱感應機專用)，正確且需要列印才會回傳。
        ///                                 PrinterType >= 2，base64編碼的 ESC/POS 列印格式字串，正確且需要列印才會回傳。
        ///                                 如何設定熱感應機及印出發票，請洽客服。
        ///                                 0元發票不會回傳此欄位</summary>
        public string Base64Data { get; set; } = "";

        /// <summary>原始 JSON 回傳</summary>
        public string RawJson { get; set; }
    }
}