/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceService.cs
   類別說明：折讓單服務實作：負責驗證、產生請求DTO與呼叫 API，封裝了與外部折讓單 API 交互的邏輯。
   建立日期：2025-12-12
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using twbobibobi.ApiClients;
using twbobibobi.Helpers;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 折讓單服務實作：負責驗證、產生請求DTO與呼叫 API
    /// </summary>
    /// <remarks>
    /// 這個類別是 `IAllowanceService` 介面的實作，負責根據輸入的開立折讓單資料創建折讓單，
    /// 並將結果封裝為結構化的 `AllowanceResponseDto`。它會處理手機載具驗證，生成折讓單請求 DTO，
    /// 並透過 `InvoiceApiClient` 進行外部 API 的呼叫。
    /// </remarks>
    public class AllowanceService : IInvoiceService
    {
        private readonly InvoiceApiClient _client;
        private readonly IMobileCarrierValidator _validator;

        /// <summary>
        /// 建構子，注入 API 客戶端與載具驗證器
        /// </summary>
        /// <param name="client">折讓單 API 客戶端，負責與外部折讓單 API 進行通信</param>
        /// <remarks>
        /// 建構此服務時，會將 `InvoiceApiClient` 和 `IMobileCarrierValidator` 注入，方便後續調用 API 和進行載具驗證。
        /// </remarks>
        public AllowanceService(InvoiceApiClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 根據輸入資料建立發票，並解析 API 結果為結構化物件。
        /// </summary>
        /// <param name="dto">輸入的開立發票資料</param>
        /// <returns>發票回應結果 DTO</returns>
        public Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 根據輸入資料建立折讓單，並解析 API 結果為結構化物件。
        /// </summary>
        /// <param name="dto">輸入的開立折讓單資料</param>
        /// <returns>折讓單回應結果 DTO</returns>
        /// <remarks>
        /// 這個方法會根據輸入的 `CreateAllowanceDto` 資料，進行必要的驗證，然後生成折讓單請求 DTO，
        /// 並呼叫外部 API。最終將 API 的回應結果轉換為 `AllowanceResponseDto` 並返回。
        /// </remarks>
        /// <example>
        /// var response = await allowanceService.CreateAllowanceAsync(createAllowanceDto);
        /// </example>
        public async Task<AllowanceResponseDto> CreateAllowanceAsync(CreateAllowanceDto dto)
        {
            // 2. 建立折讓單請求 DTO
            var request = AllowanceRequestFactory.Create(
                allowanceNumber: dto.AllowanceNumber,    // 由 factory 或 client 自行組
                date: dto.AllowanceDate,
                items: dto.ProductItem,
                taxAmount: dto.TaxAmount,
                totalAmount: dto.TotalAmount,
                buyerIdentifier: dto.BuyerIdentifier,
                buyerName: dto.BuyerName,
                buyerAddress: dto.BuyerAddress,
                buyerTelephone: dto.BuyerTelephoneNumber,
                buyerEmail: dto.BuyerEmailAddress
            );

            // 3. 呼叫外部 API
            string raw = await _client.SendAllowanceAsync(request);
            var jObj = JObject.Parse(raw);

            // 4. 成功時補上自定欄位（for 顯示或追蹤）
            if (jObj.Value<int>("code") == 0)
            {
                if (!string.IsNullOrWhiteSpace(request.BuyerIdentifier))
                    jObj["buyerIdentifier"] = request.BuyerIdentifier;
                if (!string.IsNullOrWhiteSpace(request.BuyerName))
                    jObj["buyerName"] = request.BuyerName;
            }

            // 5. 轉成結構化 DTO 並回傳
            return jObj.ToAllowanceResponseDto();
        }
    }
}