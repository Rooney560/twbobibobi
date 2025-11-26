/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：CaptchaEmailService.cs
 *  類別說明：驗證碼寄送服務，使用 EmailTemplateService 組合郵件內容後寄出
 *
 *  建立日期：2025-11-10
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版，支援 OTP 驗證碼寄送流程
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using System;
using System.Net.Mail;
using twbobibobi.Helpers;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 驗證碼寄送服務（OTP / Email 驗證用途）
    /// </summary>
    public class CaptchaEmailService
    {
        private readonly EmailHelper _emailHelper = new EmailHelper();

        /// <summary>
        /// 寄送驗證碼郵件
        /// </summary>
        /// <param name="recipientEmail">收件人信箱</param>
        /// <param name="appName">收件人名稱</param>
        /// <param name="code">驗證碼</param>
        /// <returns>是否寄送成功</returns>
        public bool SendCaptcha(string recipientEmail, string appName, string code)
        {
            try
            {
                var model = new CaptchaModel
                {
                    AppName = appName,
                    Code = code
                };

                string htmlBody = EmailTemplateService.GetTemplate(EmailTemplateType.Captcha, model);
                var view = _emailHelper.CreateHtmlView(htmlBody);

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("service@appssp.com", "保必保庇線上宮廟平台");
                    mail.To.Add(recipientEmail);
                    mail.Subject = "【保必保庇】OTP 驗證碼通知";
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(view);

                    _emailHelper.Send(mail);
                }
                return true;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(InvoiceEmailService).FullName);

                twbobibobi.Data.AjaxBasePage _ajaxBasePage = new twbobibobi.Data.AjaxBasePage();
                _ajaxBasePage.SaveErrorLog($"寄送驗證碼郵件失敗：\r\n{detailedError}");
                return false;
            }
        }
    }
}
