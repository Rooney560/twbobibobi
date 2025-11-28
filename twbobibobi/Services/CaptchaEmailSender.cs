/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：CaptchaEmailSender.cs
 *  類別說明：驗證碼 Email 寄送入口類別，負責呼叫 CaptchaEmailService 寄送郵件
 *
 *  建立日期：2025-11-11
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-11　Rooney　建立初版，支援統一寄信入口設計
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-11
 **************************************************************************/

using System;
using twbobibobi.Helpers;

namespace twbobibobi.Services
{
    /// <summary>
    /// 驗證碼寄送入口服務（封裝 CaptchaEmailService）
    /// </summary>
    public class CaptchaEmailSender
    {
        /// <summary>
        /// 寄送驗證碼通知信
        /// </summary>
        /// <param name="buyerEmail">收件人 Email</param>
        /// <param name="buyerName">收件人姓名</param>
        /// <param name="code">驗證碼內容</param>
        /// <returns>是否寄送成功</returns>
        public static bool Send(string buyerEmail, string buyerName, string code)
        {
            try
            {
                var service = new CaptchaEmailService();
                return service.SendCaptcha(buyerEmail, buyerName, code);
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(CaptchaEmailSender).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("寄送驗證碼通知失敗：\r\n" + detailedError);
                return false;
            }
        }

        /// <summary>
        /// 寄送驗證碼通知信（支援自訂寄件信箱與寄件人名稱）
        /// </summary>
        /// <param name="buyerEmail">收件人 Email</param>
        /// <param name="buyerName">收件人姓名</param>
        /// <param name="code">驗證碼內容</param>
        /// <param name="sendEmail">寄件人信箱（預設為 service@appssp.com）</param>
        /// <param name="displayName">寄件人顯示名稱（預設為 保庇保庇線上宮廟平台）</param>
        /// <returns>是否寄送成功（true 成功；false 失敗）</returns>
        public static bool Send(string buyerEmail, string buyerName, string code, string sendEmail, string displayName)
        {
            try
            {
                var service = new CaptchaEmailService();
                bool result = service.SendCaptcha(buyerEmail, buyerName, code);

                // 此版本保留參數以後可擴充（例如自訂寄件人信箱）
                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(CaptchaEmailSender).FullName);

                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("寄送驗證碼通知失敗（自訂寄件人）：\r\n" + detailedError);
                return false;
            }
        }
    }
}
