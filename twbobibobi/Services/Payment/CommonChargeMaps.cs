using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Services.Payment
{
    /// <summary>
    /// 放所有支付方式對應的共用參數設定 (ChargeType → (ChargeCode, SubType))
    /// </summary>
    public static class CommonChargeMaps
    {
        /// <summary>
        /// 預設映射表:
        ///   - APPLEPAY    → ("APPLEPAY", "")
        ///   - LINEPAY    → ("LINEPAY", "")
        ///   - JKOPAY    → ("JKOPAY", "")
        ///   - PXPAY  → ("PXPAY",   "")
        ///   - CHT        → ("TELEPAY", "")
        ///   - TWM        → ("TELEPAY", "twm")
        ///   - CreditCard → ("CreditCard", "")
        ///   - UNIONPAY   → ("CreditCard", "UNIONPAY")
        /// </summary>
        public static readonly Dictionary<string, (string ChargeCode, string SubType)> Default
            = new Dictionary<string, (string ChargeCode, string SubType)>
        {
            { "APPLEPAY",    ("APPLEPAY",    "") },
            { "LINEPAY",    ("LINEPAY",    "") },
            { "JKOPAY",    ("JKOPAY",    "") },
            { "PXPAY",  ("PXPAY",      "") },
            { "CHT",        ("TELEPAY",    "") },
            { "TWM",        ("TELEPAY",    "twm") },
            { "CreditCard", ("CreditCard", "") },
            { "UNIONPAY",   ("CreditCard", "UNIONPAY") }
        };
    }
}