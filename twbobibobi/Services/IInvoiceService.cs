using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票服務介面：定義建立發票的方法
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// 根據輸入資料建立發票，並回傳結構化回應物件。
        /// </summary>
        /// <param name="dto">開立發票所需資訊</param>
        /// <returns>API 回應結果（封裝為 InvoiceResponseDto）</returns>
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto);
    }
}