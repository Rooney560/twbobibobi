using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Model.Common;
using twbobibobi.Services;

namespace twbobibobi.Controllers
{
    /// <summary>
    /// 發票 API Controller：處理 /api/invoice 請求（開立、查詢、作廢）
    /// </summary>
    [RoutePrefix("api/invoice")]
    public class InvoiceController : ApiController
    {
        private readonly IInvoiceService _service;
        private readonly IInvoiceStatusService _statusService;
        private readonly IInvoiceCancelService _cancelService;

        /// <summary>
        /// 建構子，注入所有發票服務
        /// </summary>
        public InvoiceController(
            IInvoiceService service,
            IInvoiceStatusService statusService,
            IInvoiceCancelService cancelService)
        {
            _service = service;
            _statusService = statusService;
            _cancelService = cancelService;
        }

        /// <summary>
        /// POST api/invoice
        /// 建立發票並回傳結果
        /// </summary>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CreateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<string>.Fail("40001", "輸入格式錯誤"));
            }

            try
            {
                var result = await _service.CreateInvoiceAsync(dto);
                if (!result.Success)
                    return Ok(ApiResponse<InvoiceResponseDto>.Fail("40002", result.ErrorMessage));
                return Ok(ApiResponse<object>.Ok(result, "發票建立成功"));
            }
            catch (ArgumentException ex)
            {
                return Ok(ApiResponse<string>.Fail("40002", ex.Message));
            }
            catch
            {
                return Ok(ApiResponse<string>.Fail("50000", "伺服器錯誤"));
            }
        }

        /// <summary>
        /// 查詢發票狀態
        /// GET /api/invoice/status/{invoiceNumber}
        /// </summary>
        [HttpGet, Route("status/{invoiceNumber}")]
        public async Task<IHttpActionResult> GetInvoiceStatus(string invoiceNumber)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
                return Ok(ApiResponse<string>.Fail("40003", "請提供發票號碼"));

            try
            {
                var raw = await _statusService.QueryStatusAsync(invoiceNumber);
                var dto = JObject.Parse(raw).ToInvoiceStatusResponseDto();

                if (!dto.Success)
                    return Ok(ApiResponse<InvoiceStatusResponseDto>.Fail("40005", dto.ErrorMessage));

                return Ok(ApiResponse<InvoiceStatusResponseDto>.Ok(dto, "查詢成功"));
            }
            catch
            {
                return Ok(ApiResponse<string>.Fail("50001", "查詢失敗"));
            }
        }

        /// <summary>
        /// 作廢發票
        /// POST /api/invoice/cancel
        /// </summary>
        [HttpPost, Route("cancel")]
        public async Task<IHttpActionResult> CancelInvoice([FromBody] CancelInvoiceModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.CancelInvoiceNumber))
                return Ok(ApiResponse<string>.Fail("40004", "請提供 CancelInvoiceNumber"));

            try
            {
                var raw = await _cancelService.CancelInvoiceAsync(model.CancelInvoiceNumber);
                var dto = JObject.Parse(raw).ToCancelInvoiceResponseDto(model.CancelInvoiceNumber);

                if (!dto.Success)
                    return Ok(ApiResponse<CancelInvoiceResponseDto>.Fail("40007", dto.ErrorMessage));

                return Ok(ApiResponse<CancelInvoiceResponseDto>.Ok(dto, "發票作廢成功"));
            }
            catch
            {
                return Ok(ApiResponse<string>.Fail("50002", "作廢失敗"));
            }
        }
    }

    /// <summary>
    /// 作廢發票用的輸入模型。
    /// </summary>
    public class CancelInvoiceModel
    {
        /// <summary>
        /// 欲作廢的發票號碼。
        /// </summary>
        public string CancelInvoiceNumber { get; set; }
    }
}