using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 四種發票開立情境
    /// </summary>
    public enum InvoiceIssueScenario
    {
        /// <summary>
        /// 一般開立, 需列印
        /// </summary>
        StandardPrint,

        /// <summary>
        /// 手機載具
        /// </summary>
        MobileCarrier,

        /// <summary>
        /// 捐贈
        /// </summary>
        Donation,

        /// <summary>
        /// 打統編, 需列印 (未稅價)
        /// </summary>
        TaxIdPrint         
    }
}