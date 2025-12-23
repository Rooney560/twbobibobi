/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：InvoiceEmailSender.cs
 *  類別說明：共用發票 Email 寄送服務，轉接發票資料模型並呼叫 Email 寄送流程
 *
 *  建立日期：2025-05-01
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　改用新版 InvoiceEmailService 與 EmailTemplateService 整合版本
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 共用發票 Email 寄送服務，負責轉接發票資料到 Email 寄送流程
    /// </summary>
    public static class InvoiceEmailSender
    {
        /// <summary>
        /// 寄送發票成功通知信至買方 Email
        /// </summary>
        /// <param name="dto">發票 API 回傳的資料，含發票號碼、隨機碼、條碼、QR Code 等</param>
        /// <param name="items">商品清單，包含品名、數量、單價、稅別等</param>
        /// <param name="buyerEmail">買方 Email（收件人信箱）</param>
        /// <param name="buyerName">買方姓名（信件稱呼）</param>
        /// <param name="buyerTaxId">買方統一編號，呈現於發票下方（格式：買方：統編）</param>
        /// <param name="NumString">訂單編號，會顯示於服務明細區塊中</param>
        /// <param name="TotalAmount">總金額</param>
        /// <param name="dtNow">台北時間(現在)</param>
        /// <param name="Year">中華民國年 (三位數字，例如 "114" )</param>
        /// <param name="Month">月份 (兩位數字，例如 "05" )</param>
        /// <param name="Date">開立日期 (格式 yyyy-MM-dd)</param>
        /// <param name="Time">開立時間 (格式 HH:mm:ss)</param>
        /// <returns>是否寄送成功</returns>
        public static bool Send(
            InvoiceResponseDto dto,
            List<InvoiceItem> items,
            string buyerEmail,
            string buyerName,
            string buyerTaxId,
            string NumString,
            int TotalAmount,
            DateTime dtNow,
            string Year,
            string Month,
            string Date,
            string Time)
        {
            var debug =
                "[InvoiceEmail Debug] " +
                "dto=" + (dto == null ? "null" : "ok") + ", " +
                "items=" + (items == null ? "null" : items.Count.ToString()) + ", " +
                "buyerEmail=" + (buyerEmail ?? "null") + ", " +
                "buyerName=" + (buyerName ?? "null") + ", " +
                "buyerTaxId=" + (buyerTaxId ?? "null") + ", " +
                "NumString=" + (NumString ?? "null");
            try
            {
                // 🔹 統一稅號格式
                string BuyerTaxId = "0000000000";
                if (buyerTaxId != "")
                {
                    BuyerTaxId = buyerTaxId;
                }

                // 組模型
                var model = new InvoiceModel
                {
                    CustomerName = buyerName,
                    LogoUrl = "https://bobibobi.tw/Temples/images/Logo_no.png",
                    dtNow = dtNow,
                    Year = Year,
                    Month = Month,
                    InvoiceNumber = dto.InvoiceNumber,
                    Date = Date,
                    Time = Time,
                    RandomCode = dto.RandomNumber,
                    TotalAmount = TotalAmount,
                    SellerName = "九九商通科技有限公司",
                    SellerAddress = "台中市西屯區台灣大道四段925號5樓之9",
                    SellerTaxId = "54605358",
                    BuyerTaxId = BuyerTaxId,
                    NumString = NumString,
                    BarcodeStr = dto.Barcode,
                    Qrcode_leftStr = dto.QrCodeLeft,
                    Qrcode_rightStr = dto.QrCodeRight,
                    Items = items.Select(p => new InvoiceItem
                    {
                        Description = p.Description,
                        ProductName = p.ProductName, // 若你有額外品名可以分開帶
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice,
                        Taxable = true
                    }).ToList()
                };

                // 寄送
                var service = new InvoiceEmailService();
                return service.SendInvoice(model, "service@appssp.com", buyerEmail, "保必保庇線上宮廟平台", "【保必保庇線上宮廟平台】您的電子發票");
            }
            catch (Exception error)
            {
                var msg = error.InnerException != null
                ? error.InnerException.Message
                : error.Message;

                // 取得最內層的例外（真實的 NullReferenceException 通常在這裡）
                Exception inner = error.InnerException ?? error;

                // 組合詳細錯誤訊息
                string detailedError = string.Format(
                    "==== InvoiceEmailSender Error ====\r\n" +
                    "Time: {0}\r\n" +
                    "Request URL: {1}\r\n" +
                    "Class: {2}\r\n" +
                    "Error Type: {3}\r\n" +
                    "Message: {4}\r\n" +
                    "Inner Message: {5}\r\n" +
                    "StackTrace:\r\n{6}\r\n\r\n",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    HttpContext.Current?.Request?.Url?.ToString() ?? "N/A",
                    typeof(InvoiceEmailSender).FullName,
                    error.GetType().FullName,
                    error.Message,
                    error.InnerException != null ? error.InnerException.Message : "N/A",
                    inner.StackTrace ?? "(no stack trace)"
                );

                AjaxBasePage _ajaxBasePage = new AjaxBasePage();
                _ajaxBasePage.SaveErrorLog($"寄送發票通知失敗：\r\n{detailedError}\r\n{debug}");
                return false;
            }
        }
    }
}