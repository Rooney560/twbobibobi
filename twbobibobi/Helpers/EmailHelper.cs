/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：EmailHelper.cs
 *  類別說明：封裝 SMTP 設定與 Email 寄送功能（支援 HTML、多帳號寄信、自動判斷主機與錯誤記錄）
 *
 *  建立日期：2025-11-01
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-01　Rooney　建立初版
 *  2025-11-10　Rooney　新增多帳號寄信設定、自動判斷 SMTP 主機（Dictionary 寫法）、整合 AjaxBasePage 錯誤記錄
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 封裝 SMTP 設定及 Email 寄送功能（支援 HTML、OTP 驗證、多帳號寄信、自動判斷主機與錯誤記錄）
    /// </summary>
    public class EmailHelper
    {
        /// <summary> 內部共用的 SmtpClient 物件 </summary>
        private readonly SmtpClient _smtp;

        /// <summary>
        /// SMTP 主機對應表（依信箱網域自動匹配）
        /// </summary>
        private static readonly Dictionary<string, SmtpSetting> SmtpMap = new Dictionary<string, SmtpSetting>
        {
            { "gmail.com", new SmtpSetting("smtp.gmail.com", 587) },
            { "outlook.com", new SmtpSetting("smtp.office365.com", 587) },
            { "hotmail.com", new SmtpSetting("smtp.office365.com", 587) },
            { "yahoo.com.tw", new SmtpSetting("smtp.mail.yahoo.com", 587) },
            { "yahoo.com", new SmtpSetting("smtp.mail.yahoo.com", 587) },
            { "bobibobi.tw", new SmtpSetting("mail.bobibobi.tw", 587) }
        };


        /// <summary>
        /// 建構子：初始化 SMTP 設定（支援多帳號與自動主機判斷）
        /// </summary>
        /// <param name="userName">SMTP 帳號（可為 null 或空字串，預設為 service@appssp.com）</param>
        /// <param name="password">SMTP 密碼（可為 null 或空字串，預設為 txju nnco hwxe lyvy）</param>
        public EmailHelper(string userName = null, string password = null)
        {
            // 若未輸入則使用預設帳號密碼
            string smtpUser = string.IsNullOrWhiteSpace(userName) ? "service@appssp.com" : userName;
            string smtpPass = string.IsNullOrWhiteSpace(password) ? "txju nnco hwxe lyvy" : password;

            // 解析網域
            string domain = "gmail.com";
            int atIndex = smtpUser.IndexOf('@');
            if (atIndex > -1 && atIndex < smtpUser.Length - 1)
                domain = smtpUser.Substring(atIndex + 1).ToLower();

            // 自動比對主機設定
            SmtpSetting smtpSetting;
            if (SmtpMap.ContainsKey(domain))
                smtpSetting = SmtpMap[domain];
            else
                smtpSetting = new SmtpSetting("smtp.gmail.com", 587); // fallback

            _smtp = new SmtpClient(smtpSetting.Host, smtpSetting.Port)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };
        }

        /// <summary>
        /// 建立 HTML 格式的 AlternateView
        /// </summary>
        /// <param name="html">HTML 內容字串</param>
        /// <returns>HTML AlternateView 物件</returns>
        public AlternateView CreateHtmlView(string html)
        {
            return AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
        }

        /// <summary>
        /// 寄出指定的 MailMessage 物件
        /// </summary>
        /// <param name="mail">要寄出的 MailMessage 物件</param>
        public void Send(MailMessage mail)
        {
            _smtp.Send(mail);
            mail.Dispose();
        }

        /// <summary>
        /// SMTP 主機設定結構（.NET Framework 相容版）
        /// </summary>
        private class SmtpSetting
        {
            /// <summary> 主機名稱 </summary>
            public string Host { get; set; }

            /// <summary> 連接埠 </summary>
            public int Port { get; set; }

            /// <summary>
            /// 初始化設定
            /// </summary>
            public SmtpSetting(string host, int port)
            {
                Host = host;
                Port = port;
            }
        }
    }
}