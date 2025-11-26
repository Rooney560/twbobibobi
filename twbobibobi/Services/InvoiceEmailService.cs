/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：InvoiceEmailService.cs
 *  類別說明：發票 Email 寄送服務，負責組合發票資料與模板內容，並透過 EmailHelper 寄出
 *
 *  建立日期：2025-11-01
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-01  Rooney  建立初版
 *  2025-11-10　Rooney　改用 EmailTemplateService 統一管理模板；保留 QR 與 Barcode 圖像處理邏輯
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using BCFBaseLibrary.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using twbobibobi.Helpers;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票 Email 寄送服務，負責組合 Model、影像及 Email 內容，並執行寄送
    /// </summary>
    public class InvoiceEmailService
    {
        private readonly EmailHelper _emailHelper = new EmailHelper();

        /// <summary>
        /// 寄送發票 Email 至指定收件者
        /// </summary>
        /// <param name="model">發票所有欄位的資料模型</param>
        /// <param name="sendEmail">寄件人信箱</param>
        /// <param name="recipientEmail">收件人信箱</param>
        /// <param name="displayName">寄件人顯示名稱（在對方郵件裡看到的名稱）</param>
        /// <param name="subject">Email 主旨</param>
        /// <returns>是否寄送成功</returns>
        public bool SendInvoice(
            InvoiceModel model,
            string sendEmail,
            string recipientEmail,
            string displayName,
            string subject)
        {
            // 設定時區為台北標準時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            try
            {
                // 防呆檢查：確保必要欄位都有值
                if (model == null)
                    throw new ArgumentNullException(nameof(model));
                if (string.IsNullOrWhiteSpace(model.CustomerName))
                    throw new ArgumentException("CustomerName 必須提供", nameof(model.CustomerName));
                if (string.IsNullOrWhiteSpace(model.LogoUrl))
                    throw new ArgumentException("LogoUrl 必須提供", nameof(model.LogoUrl));
                if (string.IsNullOrWhiteSpace(model.Year))
                    throw new ArgumentException("Year 必須提供", nameof(model.Year));
                if (string.IsNullOrWhiteSpace(model.Month))
                    throw new ArgumentException("Month 必須提供", nameof(model.Month));
                if (string.IsNullOrWhiteSpace(model.InvoiceNumber))
                    throw new ArgumentException("InvoiceNumber 必須提供", nameof(model.InvoiceNumber));
                if (string.IsNullOrWhiteSpace(model.Date) || string.IsNullOrWhiteSpace(model.Time))
                    throw new ArgumentException("Date/Time 必須提供", nameof(model.Date));
                if (string.IsNullOrWhiteSpace(model.RandomCode))
                    throw new ArgumentException("RandomCode 必須提供", nameof(model.RandomCode));
                if (model.TotalAmount <= 0)
                    throw new ArgumentException("TotalAmount 必須大於 0", nameof(model.TotalAmount));
                if (string.IsNullOrWhiteSpace(model.SellerTaxId))
                    throw new ArgumentException("SellerTaxId 必須提供", nameof(model.SellerTaxId));
                if (string.IsNullOrWhiteSpace(model.BuyerTaxId))
                    throw new ArgumentException("BuyerTaxId 必須提供", nameof(model.BuyerTaxId));
                if (string.IsNullOrWhiteSpace(model.SellerName))
                    throw new ArgumentException("SellerName 必須提供", nameof(model.SellerName));
                if (string.IsNullOrWhiteSpace(model.SellerAddress))
                    throw new ArgumentException("SellerAddress 必須提供", nameof(model.SellerAddress));
                if (string.IsNullOrWhiteSpace(model.NumString))
                    throw new ArgumentException("NumString 必須提供", nameof(model.NumString));
                if (string.IsNullOrWhiteSpace(model.BarcodeStr))
                    throw new ArgumentException("BarcodeStr 必須提供", nameof(model.BarcodeStr));
                if (string.IsNullOrWhiteSpace(model.Qrcode_leftStr))
                    throw new ArgumentException("Qrcode_leftStr 必須提供", nameof(model.Qrcode_leftStr));
                if (string.IsNullOrWhiteSpace(model.Qrcode_rightStr))
                    throw new ArgumentException("Qrcode_rightStr 必須提供", nameof(model.Qrcode_rightStr));
                if (model.Items == null || !model.Items.Any())
                    throw new ArgumentException("Items 必須至少包含一筆項目", nameof(model.Items));

                // 組出 Code39 條碼及 QR code 的原始字串
                //var yearPeriod = model.Year.PadLeft(3, '0') + model.Month.PadLeft(2, '0');
                //var normalizedInvoice = model.InvoiceNumber.Replace("-", string.Empty);
                //var code39 = $"{yearPeriod}{normalizedInvoice}{model.RandomCode}";

                // 產生條碼及 QR 圖片 bytes
                var barcodeBytes = EmailImageHelper.GetBarcode(model.BarcodeStr, 300, 80);
                var qrBytes_left = EmailImageHelper.GetQRcode(model.Qrcode_leftStr, 100, 100);
                var qrBytes_right = EmailImageHelper.GetQRcode(model.Qrcode_rightStr, 100, 100);

                // 轉為 Data URI
                var barDataUri = EmailImageHelper.ToBase64DataUri(barcodeBytes, "image/png");
                var qrLeftDataUri = EmailImageHelper.ToBase64DataUri(qrBytes_left, "image/png");
                var qrRightDataUri = EmailImageHelper.ToBase64DataUri(qrBytes_right, "image/png");

                // 🔹 取得 HTML 樣板內容（新版整合）
                string htmlBody = EmailTemplateService.GetTemplate(EmailTemplateType.Invoice, model);
                var view = _emailHelper.CreateHtmlView(htmlBody);

                var logoRes = EmailImageHelper.DownloadImageAsLinkedResource(
                    model.LogoUrl,
                    "logoCid",
                    "image/png");

                var barcodeRes = EmailImageHelper.Base64ImageAsLinkedResource(
                    barDataUri,
                    "barcodeCid",
                    "image/png"
                    );

                var qrleftRes = EmailImageHelper.QRCodeImageAsLinkedResource(
                    qrLeftDataUri,
                    "qrLeftDataUri",
                    "image/png"
                    );

                var unnamedCidRes = EmailImageHelper.DownloadImageAsLinkedResource(
                    "https://bobibobi.tw/Temples/images/unnamed.gif",
                    "unnamedCid",
                    "image/png");

                var qrrightRes = EmailImageHelper.QRCodeImageAsLinkedResource(
                    qrRightDataUri,
                    "qrRightDataUri",
                    "image/png"
                    );

                // 加入 LinkedResources
                view.LinkedResources.Add(logoRes);
                view.LinkedResources.Add(barcodeRes);
                view.LinkedResources.Add(qrleftRes);
                view.LinkedResources.Add(unnamedCidRes);
                view.LinkedResources.Add(qrrightRes);

                // 組裝並寄送 MailMessage
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(sendEmail, displayName);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject + "#" + model.NumString;
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(view);

                    _emailHelper.Send(mail);
                }

                return true;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(InvoiceEmailService).FullName);

                AjaxBasePage _ajaxBasePage = new AjaxBasePage();
                _ajaxBasePage.SaveErrorLog($"寄送發票 Email 失敗：\r\n{detailedError}");
                return false;
            }
        }
    }
}
