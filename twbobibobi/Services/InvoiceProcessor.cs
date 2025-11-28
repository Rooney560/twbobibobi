using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票共用流程處理器，提供標準化開立發票的介面
    /// </summary>
    public static class InvoiceProcessor
    {
        /// <summary>
        /// 組裝 CreateInvoiceDto 並呼叫廠商發票 API，回傳處理結果
        /// </summary>
        /// <param name="input">發票輸入參數封裝</param>
        /// <returns>發票處理結果資訊</returns>
        public static InvoiceResponseDto ProcessInvoice(InvoiceWrapperInput input)
        {
            var dto = new CreateInvoiceDto
            {
                OrderId = input.OrderId,
                Scenario = input.Scenario,
                Items = input.Items,
                BuyerIdentifier = string.IsNullOrEmpty(input.BuyerIdentifier) ? "0000000000" : input.BuyerIdentifier,
                BuyerName = string.IsNullOrEmpty(input.BuyerName) ? "消費者" : input.BuyerName,
                BuyerAddress = input.BuyerAddress ?? "",
                BuyerTelephoneNumber = input.BuyerTelephoneNumber ?? "",
                BuyerEmailAddress = input.BuyerEmailAddress ?? "",
                MainRemark = input.MainRemark ?? "",
                CarrierType = input.CarrierType ?? "",
                CarrierId = input.CarrierId ?? "",
                NPOBAN = input.NPOBAN ?? ""
            };

            return InvoiceFactory.CreateInvoice(dto);
        }
        /// <summary>
        /// 將發票類型代碼轉換為發票開立情境（Scenario）
        /// </summary>
        /// <param name="invoiceType">
        /// 字串型別的發票類型代碼：
        /// <list type="bullet">
        /// <item><term>"1"</term><description>對應：一般電子發票（需列印）</description></item>
        /// <item><term>"2"</term><description>對應：手機載具發票</description></item>
        /// <item><term>"3"</term><description>對應：捐款發票</description></item>
        /// <item><term>"4"</term><description>對應：公司發票（打統編、需列印）</description></item>
        /// </list>
        /// </param>
        /// <returns>對應的 <see cref="InvoiceIssueScenario"/> 列舉值</returns>
        public static InvoiceIssueScenario GetScenario(string invoiceType)
        {
            switch (invoiceType)
            {
                case "1": return InvoiceIssueScenario.StandardPrint;
                case "2": return InvoiceIssueScenario.MobileCarrier;
                case "3": return InvoiceIssueScenario.Donation;
                case "4": return InvoiceIssueScenario.TaxIdPrint;
                default: return InvoiceIssueScenario.StandardPrint;
            }
        }
    }
}