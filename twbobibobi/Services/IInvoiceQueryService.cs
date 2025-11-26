using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供發票查詢服務介面。
    /// </summary>
    public interface IInvoiceQueryService
    {
        /// <summary>
        /// 查詢指定發票號碼的狀態。
        /// </summary>
        /// <param name="orderID">要查詢的訂單編號</param>
        /// <returns>API 回應結果</returns>
        Task<string> QueryOrderIdAsync(string orderID);

        /// <summary>
        /// 查詢指定發票號碼的狀態。
        /// </summary>
        /// <param name="invoiceNumber">要查詢的發票號碼</param>
        /// <returns>API 回應結果</returns>
        Task<string> QueryInvoiceNumberAsync(string invoiceNumber);
    }
}