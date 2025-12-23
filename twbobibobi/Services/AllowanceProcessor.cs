/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceProcessor.cs
   類別說明：折讓單共用流程處理器，提供標準化開立折讓單的介面。
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 折讓單共用流程處理器，提供標準化開立折讓單的介面
    /// </summary>
    /// <remarks>
    /// 這個靜態類別封裝了開立折讓單的標準流程。它提供了兩個主要功能：
    /// 1. 將折讓單輸入資料封裝成 `CreateAllowanceDto` 並呼叫折讓單 API 來創建折讓單。
    /// </remarks>
    public static class AllowanceProcessor
    {
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

    }
}