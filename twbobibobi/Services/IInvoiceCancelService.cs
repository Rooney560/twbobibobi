using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票作廢服務介面。
    /// </summary>
    public interface IInvoiceCancelService
    {
        /// <summary>
        /// 作廢指定發票號碼。
        /// </summary>
        /// <param name="cancelInvoiceNumber">要作廢的發票號碼</param>
        /// <returns>API 回應結果</returns>
        Task<string> CancelInvoiceAsync(string cancelInvoiceNumber);
    }
}