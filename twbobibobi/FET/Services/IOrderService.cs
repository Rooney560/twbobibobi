using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.FET.Dto;
using twbobibobi.FET.Data;
using twbobibobi.FET.Processors;    

namespace twbobibobi.FET.Services
{
    // 新增一个 Result DTO
    public class OrderResultDto
    {
        public string ClientOrderNumber { get; set; }
        public List<string> PartnerOrderNumbers { get; set; }
        public string OrderId { get; set; }   
    }

    /// <summary>
    /// Class: IOrderService
    /// 訂單處理服務介面，定義建立訂單的核心方法
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// 建立並處理訂單
        /// </summary>
        /// <param name="clientOrderNumber"></param>
        /// <param name="request">來自 API 的訂單資料</param>
        /// <param name="fetOrderNumber"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="itemsInfo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        Task<OrderResultDto> ProcessOrderAsync(
            string clientOrderNumber,
            OrderRequestDto request,
            string fetOrderNumber,
            string TotalAmount,
            string itemsInfo,
            string OrderId);
    }
}