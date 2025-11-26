using Newtonsoft.Json;
using System.ComponentModel;

namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// 購買人資料
    /// </summary>
    public class ApplicantDto
    {
        /// <summary>
        /// 購買人姓名
        /// </summary>
        [JsonProperty("name")]
        public string appName { get; set; }

        /// <summary>
        /// 購買人電話
        /// </summary>
        [JsonProperty("mobile")]
        public string appMobile { get; set; } = "";

        /// <summary>
        /// 購買人電子信箱
        /// </summary>
        [JsonProperty("email")]
        public string appEmail { get; set; } = "";

        /// <summary>
        /// 購買人國曆生日，格式 YYYYMMDD（民國年/國曆月/日）
        /// </summary>
        [JsonProperty("birthday")]
        public string appSolarBirthday { get; set; }
        [JsonIgnore]
        public string appsBirth { get; set; }

        /// <summary>
        /// 購買人農曆生日，格式 YYYYMMDD（民國年/農曆月/日）
        /// </summary>
        [JsonProperty("lunarBirthday")]
        public string appLunarBirthday { get; set; }
        [JsonIgnore]
        public string appBirth { get; set; }

        /// <summary>
        /// 是否閏月 (Y/N)，无论 JSON 传来了 null 还是根本没带，最终都默认为 "N"
        /// 如果客户端传了非空字符串，就用客户端的值
        /// </summary>
        [JsonProperty(
            "lunarLeap",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue("N")]
        public string appLeapMonth { get; set; } = "N";

        /// <summary>
        /// 農曆時辰 (子丑寅…)，同理默认 "吉"
        /// </summary>
        [JsonProperty(
            "lunarBirthTime",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("吉")]
        public string appBirthTime { get; set; } = "吉";

        /// <summary>
        /// 農曆生辰月份
        /// </summary>
        [JsonProperty("birthMonth")]
        public string appBirthMonth { get; set; } = "";

        /// <summary>
        /// 年齡
        /// </summary>
        [JsonProperty("age")]
        public string appAge { get; set; } = "0";

        /// <summary>
        /// 生肖
        /// </summary>
        [JsonProperty("zodiac")]
        public string appZodiac { get; set; } = "";

        /// <summary>
        /// 購買人備註
        /// </summary>
        [JsonProperty("remark")]
        public string appRemark { get; set; } = "";

        /// <summary>
        /// 收件人姓名（若與購買人不同）
        /// </summary>
        [JsonProperty("receipt")]
        public string reName { get; set; } = "";

        /// <summary>
        /// 收件人行動電話（若與購買人不同）
        /// </summary>
        [JsonProperty("receiptMobile")]
        public string reMobile { get; set; } = "";

        /// <summary>
        /// 縣/市
        /// </summary>
        [JsonProperty("city")]
        public string appCity { get; set; } = "";

        /// <summary>
        /// 區/鄉/鎮
        /// </summary>
        [JsonProperty("region")]
        public string appRegion { get; set; } = "";

        /// <summary>
        /// 地址（街道門牌）
        /// </summary>
        [JsonProperty("address")]
        public string appAddr { get; set; } = "";

        /// <summary>
        /// 郵遞區號
        /// </summary>
        [JsonProperty("zipCode")]
        public string appzipCode { get; set; } = "0";

        /// <summary>
        /// 是否寄送通知單及祈福品(開運商品固定 defult: Y) Y-是 N-否
        /// </summary>
        [JsonProperty("send")]
        public string sendback { get; set; } = "N";

        /// <summary>
        /// 寄送註記：奉食/自用… 當 send = Y 時，本欄位註記寄送模式
        /// </summary>
        [JsonProperty("sendRemark")]
        public string sendRemark { get; set; } = "";

        /// <summary>
        /// 運費金額
        /// </summary>
        [JsonProperty("shippingFee")]
        public string appshippingFee { get; set; } = "0";
    }
}