/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：ThankYouEmailService.cs
 *  類別說明：感謝狀寄送服務，支援自訂感謝內容模板
 *
 *  建立日期：2025-11-10
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版，支援感謝狀寄送流程
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
    /// 感謝狀寄送服務
    /// </summary>
    public class ThankYouEmailService
    {
        private readonly EmailHelper _emailHelper = new EmailHelper();

        /// <summary>
        /// 寄送感謝狀信件
        /// </summary>
        /// <param name="recipientEmail">收件人信箱</param>
        /// <param name="userName">收件人名稱</param>
        /// <param name="message">感謝內容文字</param>
        /// <returns>是否寄送成功</returns>
        public bool SendThankYou(string recipientEmail, string userName, string message)
        {
            try
            {
                var model = new ThankYouModel
                {
                    UserName = userName,
                    Message = message
                };

                string htmlBody = EmailTemplateService.GetTemplate(EmailTemplateType.ThankYou, model);
                var view = _emailHelper.CreateHtmlView(htmlBody);

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("service@appssp.com", "保必保庇線上宮廟平台");
                    mail.To.Add(recipientEmail);
                    mail.Subject = "【保必保庇】感謝狀通知";
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(view);

                    _emailHelper.Send(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog($"寄送感謝狀郵件失敗：{ex.Message}\r\n{ex.StackTrace}");
                return false;
            }
        }
    }
}
