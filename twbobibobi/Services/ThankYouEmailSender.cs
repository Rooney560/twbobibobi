/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：ThankYouEmailSender.cs
 *  類別說明：感謝狀 Email 寄送入口類別，負責呼叫 ThankYouEmailService 寄送郵件
 *
 *  建立日期：2025-11-11
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版，支援統一寄信入口設計與錯誤記錄
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-11
 **************************************************************************/

using System;
using twbobibobi.Helpers;


namespace twbobibobi.Services
{
    /// <summary>
    /// 感謝狀寄送入口服務（封裝 ThankYouEmailService）
    /// </summary>
    public class ThankYouEmailSender
    {
        /// <summary>
        /// 寄送感謝狀通知信（統一入口）
        /// </summary>
        /// <param name="recipientEmail">收件人 Email</param>
        /// <param name="userName">收件人姓名</param>
        /// <param name="message">感謝內容文字</param>
        /// <returns>是否寄送成功（true 成功；false 失敗）</returns>
        public static bool Send(string recipientEmail, string userName, string message)
        {
            try
            {
                var service = new ThankYouEmailService();
                return service.SendThankYou(recipientEmail, userName, message);
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(ThankYouEmailSender).FullName);

                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("寄送感謝狀郵件失敗：\r\n" + detailedError);
                return false;
            }
        }

        /// <summary>
        /// 寄送感謝狀通知信（支援自訂寄件信箱與寄件人名稱）
        /// </summary>
        /// <param name="recipientEmail">收件人 Email</param>
        /// <param name="userName">收件人姓名</param>
        /// <param name="message">感謝內容文字</param>
        /// <param name="sendEmail">寄件人信箱（預設為 service@appssp.com）</param>
        /// <param name="displayName">寄件人顯示名稱（預設為 保庇保庇線上宮廟平台）</param>
        /// <returns>是否寄送成功（true 成功；false 失敗）</returns>
        public static bool Send(string recipientEmail, string userName, string message, string sendEmail, string displayName)
        {
            try
            {
                var service = new ThankYouEmailService();
                bool result = service.SendThankYou(recipientEmail, userName, message);

                // 保留參數以支援未來擴充自訂寄件人邏輯
                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(ThankYouEmailSender).FullName);

                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("寄送感謝狀郵件失敗（自訂寄件人）：\r\n" + detailedError);
                return false;
            }
        }
    }
}