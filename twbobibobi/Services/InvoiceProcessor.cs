/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceProcessor.cs
   類別說明：發票共用流程處理器，提供標準化開立發票的介面。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票共用流程處理器，提供標準化開立發票的介面
    /// </summary>
    /// <remarks>
    /// 這個靜態類別封裝了開立發票的標準流程。它提供了兩個主要功能：
    /// 1. 將發票輸入資料封裝成 `CreateInvoiceDto` 並呼叫發票 API 來創建發票。
    /// 2. 根據發票類型代碼轉換為對應的發票開立情境（Scenario）。
    /// </remarks>
    public static class InvoiceProcessor
    {
        /// <summary>
        /// 組裝 CreateInvoiceDto 並呼叫廠商發票 API，回傳處理結果
        /// </summary>
        /// <param name="input">發票輸入參數封裝</param>
        /// <returns>發票處理結果資訊</returns>
        /// <remarks>
        /// 這個方法接收一個 `InvoiceWrapperInput` 物件，將其轉換為 `CreateInvoiceDto`，
        /// 然後透過 `InvoiceFactory.CreateInvoice` 呼叫發票 API 並回傳發票處理結果。
        /// </remarks>
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
        /// 組裝 CreateAllowanceDto 並呼叫廠商折讓單 API，回傳處理結果
        /// </summary>
        /// <param name="input">折讓單輸入參數封裝</param>
        /// <returns>折讓單處理結果資訊</returns>
        /// <remarks>
        /// 這個方法接收一個 `AllowanceWrapperInput` 物件，將其轉換為 `CreateAllowanceDto`，
        /// 然後透過 `AllowanceFactory.CreateAllowance` 呼叫折讓單 API 並回傳折讓單處理結果。
        /// </remarks>
        public static AllowanceResponseDto ProcessAllowance(AllowanceWrapperInput input)
        {
            var dto = new CreateAllowanceDto
            {
                AllowanceNumber = input.AllowanceNumber,
                AllowanceDate = input.AllowanceDate,
                AllowanceType = input.AllowanceType,
                BuyerIdentifier = string.IsNullOrEmpty(input.BuyerIdentifier) ? "0000000000" : input.BuyerIdentifier,
                BuyerName = string.IsNullOrEmpty(input.BuyerName) ? "消費者" : input.BuyerName,
                BuyerAddress = input.BuyerAddress ?? "",
                BuyerTelephoneNumber = input.BuyerTelephoneNumber ?? "",
                BuyerEmailAddress = input.BuyerEmailAddress ?? "",
                ProductItem = input.Items,
                TaxAmount = input.TaxAmount,
                TotalAmount = input.TotalAmount
            };

            return AllowanceFactory.CreateAllowance(dto);
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
        /// <remarks>
        /// 這個方法將發票類型代碼轉換為對應的發票開立情境（`InvoiceIssueScenario`），
        /// 以便於後續處理發票請求的邏輯。
        /// </remarks>
        /// <example>
        /// var scenario = InvoiceProcessor.GetScenario("1"); // StandardPrint
        /// </example>
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