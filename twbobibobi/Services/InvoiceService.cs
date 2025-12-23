/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceService.cs
   類別說明：發票服務實作：負責驗證、產生請求DTO與呼叫 API，封裝了與外部發票 API 交互的邏輯。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using twbobibobi.ApiClients;
using twbobibobi.Helpers;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票服務實作：負責驗證、產生請求DTO與呼叫 API
    /// </summary>
    /// <remarks>
    /// 這個類別是 `IInvoiceService` 介面的實作，負責根據輸入的開立發票資料創建發票，
    /// 並將結果封裝為結構化的 `InvoiceResponseDto`。它會處理手機載具驗證，生成發票請求 DTO，
    /// 並透過 `InvoiceApiClient` 進行外部 API 的呼叫。
    /// </remarks>
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceApiClient _client;
        private readonly IMobileCarrierValidator _validator;

        /// <summary>
        /// 建構子，注入 API 客戶端與載具驗證器
        /// </summary>
        /// <param name="client">發票 API 客戶端，負責與外部發票 API 進行通信</param>
        /// <param name="validator">手機載具驗證器，負責驗證手機載具資訊</param>
        /// <remarks>
        /// 建構此服務時，會將 `InvoiceApiClient` 和 `IMobileCarrierValidator` 注入，方便後續調用 API 和進行載具驗證。
        /// </remarks>
        public InvoiceService(InvoiceApiClient client, IMobileCarrierValidator validator)
        {
            _client = client;
            _validator = validator;
        }

        /// <summary>
        /// 根據輸入資料建立折讓單，並解析 API 結果為結構化物件。
        /// </summary>
        /// <param name="dto">輸入的開立折讓單資料</param>
        /// <returns>折讓單回應結果 DTO</returns>
        public Task<AllowanceResponseDto> CreateAllowanceAsync(CreateAllowanceDto dto)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 根據輸入資料建立發票，並解析 API 結果為結構化物件。
        /// </summary>
        /// <param name="dto">輸入的開立發票資料</param>
        /// <returns>發票回應結果 DTO</returns>
        /// <remarks>
        /// 這個方法會根據輸入的 `CreateInvoiceDto` 資料，進行必要的驗證，然後生成發票請求 DTO，
        /// 並呼叫外部 API。最終將 API 的回應結果轉換為 `InvoiceResponseDto` 並返回。
        /// </remarks>
        /// <example>
        /// var response = await invoiceService.CreateInvoiceAsync(createInvoiceDto);
        /// </example>
        public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            // 1. 驗證手機載具情境
            if (dto.Scenario == InvoiceIssueScenario.MobileCarrier)
                _validator.Validate(dto.CarrierType, dto.CarrierId);

            // 2. 建立發票請求 DTO
            var request = InvoiceRequestFactory.Create(
                dto.Scenario,
                orderId: dto.OrderId,    // 由 factory 或 client 自行組
                items: dto.Items,
                buyerIdentifier: dto.BuyerIdentifier,
                buyerName: dto.BuyerName,
                buyerAddress: dto.BuyerAddress,
                buyerTelephone: dto.BuyerTelephoneNumber,
                buyerEmail: dto.BuyerEmailAddress,
                mainRemark: dto.MainRemark,
                carrierType: dto.CarrierType,
                carrierId: dto.CarrierId,
                npoban: dto.NPOBAN
            );

            // 3. 呼叫外部 API
            string raw = await _client.SendInvoiceAsync(request);
            var jObj = JObject.Parse(raw);

            // 4. 成功時補上自定欄位（for 顯示或追蹤）
            if (jObj.Value<int>("code") == 0)
            {
                if (!string.IsNullOrWhiteSpace(request.CarrierType))
                    jObj["carrierType"] = request.CarrierType;
                if (!string.IsNullOrWhiteSpace(request.CarrierId1))
                    jObj["carrierId"] = request.CarrierId1;
                if (!string.IsNullOrWhiteSpace(request.BuyerIdentifier))
                    jObj["buyerIdentifier"] = request.BuyerIdentifier;
                if (!string.IsNullOrWhiteSpace(request.BuyerName))
                    jObj["buyerName"] = request.BuyerName;
                if (!string.IsNullOrWhiteSpace(request.NPOBAN))
                    jObj["npoban"] = request.NPOBAN;
            }

            // 5. 轉成結構化 DTO 並回傳
            return jObj.ToInvoiceResponseDto();
        }
    }
}