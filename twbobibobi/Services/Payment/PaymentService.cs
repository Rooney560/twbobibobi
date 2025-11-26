using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Temple.Temples.templeCheck;

namespace twbobibobi.Services.Payment
{
    /// <summary>
    /// 核心服務類別：負責根據 (AdminID, kind, ChargeType) 取得對應的 TemplePaymentConfig，
    /// 並呼叫其 Invoker 產生最終支付連結。
    /// </summary>
    public static class PaymentService
    {
        /// <summary>
        /// 所有 (AdminID, kind) 的支付設定集合，
        /// Key 使用 Tuple(AdminID, kind)，Value 為 TemplePaymentConfig。
        /// </summary>
        private static readonly Dictionary<(int AdminID, int Kind), TemplePaymentConfig> _configs
            = new Dictionary<(int, int), TemplePaymentConfig>
        {
            // 範例：大甲鎮瀾宮 (AdminID=3)、點燈服務 (kind=1)
            { (3, 1), new TemplePaymentConfig {
                    ServicePrefix = "lights",
                    ChargeTypeMap = CommonChargeMaps.Default,
                    Invoker = (page, basePage, orderId, applicantId, paytype, telco, price, m_phone, returnUrl, year) =>
                        // 移植原 TWWebPay_lights_Fw 方法內容
                        page.TWWebPay_lights_da(basePage, orderId, applicantId, paytype, telco, price, m_phone, returnUrl, year)
            }},
            // …其他 (AdminID, kind) 組合…
        };

        /// <summary>
        /// 產生並回傳支付連結。
        /// 1. 解析 (adminId, kind) 找到對應設定；
        /// 2. 根據傳入的 ChargeType 取得 apiChargeCode + SubType；
        /// 3. 呼叫設定裡的 Invoker 委派執行實際 API 並回傳連結。
        /// </summary>
        /// <param name="page">當前處理器，含 TWWebPay_* 方法</param>
        /// <param name="basepage">當前頁面物件 (用於取 Request/Response/Helper)</param>
        /// <param name="adminId">廟宇 AdminID</param>
        /// <param name="kind">服務項目 (點燈、普渡、補財庫…)</param>
        /// <param name="orderId">訂單流水號</param>
        /// <param name="applicantId">申請者 ID</param>
        /// <param name="cost">金額</param>
        /// <param name="appMobile">申請者手機號碼</param>
        /// <param name="chargeType">ChargeType enum ToString()</param>
        /// <param name="year">年度 (year)</param>
        /// <returns>建構完成的支付連結 URL</returns>
        public static string GeneratePaymentLink(
            AjaxPageHandler page,
            AjaxBasePage basepage,
            int adminId,
            int kind,
            string orderId,
            int applicantId,
            int cost,
            string appMobile,
            string chargeType,
            string year)
        {
            if (!_configs.TryGetValue((adminId, kind), out var cfg))
                throw new NotSupportedException(
                    $"不支援的廟宇/服務組合：AdminID={adminId}, kind={kind}");

            // 取得或 fallback 類型參數
            var pair = cfg.ChargeTypeMap.TryGetValue(chargeType, out var p)
                       ? p
                       : (paytype: chargeType, telco: string.Empty);

            // 組出 query 字串（可依需求再擴充）
            string query = $"a={adminId}&aid={applicantId}&kind={kind}"
                         + (basepage.Request["twm"] != null ? "&twm=1" : "")
                         + (basepage.Request["bobi"] != null ? "&bobi=1" : "")
                         + (basepage.Request["type"] != null ? "&type=" + basepage.Request["type"] : "");

            // 呼叫實際的 API
            return cfg.Invoker(
                page,
                basepage,
                orderId,
                applicantId,
                pair.paytype,
                pair.telco,
                cost,
                appMobile,
                query,
                year);
        }
    }
}