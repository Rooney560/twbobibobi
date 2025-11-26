/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：EmailTemplateType.cs
 *  類別說明：定義不同用途的 Email 模板種類
 *
 *  建立日期：2025-11-10
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

namespace twbobibobi.Services
{
    /// <summary>
    /// Email 模板類型列舉
    /// </summary>
    public enum EmailTemplateType
    {
        /// <summary>發票通知信</summary>
        Invoice,

        /// <summary>驗證碼通知信</summary>
        Captcha,

        /// <summary>感謝狀通知信</summary>
        ThankYou
    }
}
