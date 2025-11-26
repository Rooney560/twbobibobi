using System.Collections.Generic;
using Newtonsoft.Json;

namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// API 請求主體
    /// </summary>
    public class OrderRequestDto
    {
        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty("fetOrderNumber")]
        public string FetOrderNumber { get; set; }

        [JsonProperty("applicant")]
        public ApplicantDto Applicant { get; set; }

        [JsonProperty("items")]
        public List<ItemDto> Items { get; set; }
    }
}