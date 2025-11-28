using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.ApiClients;
using twbobibobi.Helpers;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票服務實作：負責驗證、產生請求DTO與呼叫 API
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceApiClient _client;
        private readonly IMobileCarrierValidator _validator;

        /// <summary>
        /// 建構子，注入 API 客戶端與載具驗證器
        /// </summary>
        public InvoiceService(InvoiceApiClient client, IMobileCarrierValidator validator)
        {
            _client = client;
            _validator = validator;
        }

        /// <summary>
        /// 根據輸入資料建立發票，並解析 API 結果為結構化物件。
        /// </summary>
        /// <param name="dto">輸入的開立發票資料</param>
        /// <returns>發票回應結果 DTO</returns>
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