using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Temple.Temples.templeCheck;

namespace twbobibobi.Services.Payment
{
    /// <summary>
    /// 代表「某廟宇 (AdminID) + 某服務項目 (kind)」的支付設定。
    /// 將原本 templeCheck.aspx.cs 裡面多層 switch(case) 的設定，
    /// 全部集中在這個字典裡，方便維護與擴充。
    /// </summary>
    public class TemplePaymentConfig
    {
        /// <summary>
        /// 服務項目前綴，用來呼叫對應的 TWWebPay 方法，
        /// 例如 "lights" → TWWebPay_lights_Fw(...)
        /// </summary>
        public string ServicePrefix { get; set; }

        /// <summary>
        /// 支付方式字典：Key 為 ChargeType enum 的字串 (或其他識別)，
        /// Value 為傳給底層 API 的 (ChargeCode, SubType) 參數對。
        /// </summary>
        public Dictionary<string, (string paytype, string telco)> ChargeTypeMap { get; set; }

        /// <summary>
        /// 封裝了對應 TWWebPay_* 方法的呼叫委派，
        /// 統一傳入 AjaxBasePage、orderId、applicantId、ChargeCode、SubType、金額等，
        /// 回傳支付連結字串。
        /// </summary>
        public Func<
            AjaxPageHandler, // 當前處理器，含 TWWebPay_* 方法
            AjaxBasePage,    // 當前頁面物件 (用於取 Request/Response/Helper)
            string,          // orderId / 購買單流水號
            int,             // applicantId / 購買者 ID
            string,          // paytype /
            string,          // telco /
            int,             // price / 金額
            string,          // m_phone / 購買者手機 (AppMobile)
            string,          // returnUrl / query 參數 (原本要拼的 URL 查詢字串)
            string,          // 年度參數 (year)
            string           // 回傳：最終要導向的支付連結
        > Invoker
        { get; set; }
    }
}