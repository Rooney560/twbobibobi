using Newtonsoft.Json;
using System.Collections.Generic;

namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// 訂單明細: 商品與數量
    /// </summary>
    public class ItemDto
    {
        [JsonProperty("productCode")] public string ProductCode { get; set; }
        [JsonProperty("qty")] public int Qty { get; set; }
        [JsonProperty("unitPrice")] public decimal UnitPrice { get; set; }

        [JsonProperty("prayedPerson")]
        public List<PrayedPersonDto> PrayedPerson { get; set; }
                = new List<PrayedPersonDto>();   // ← 這裡預設賦值
    }

}