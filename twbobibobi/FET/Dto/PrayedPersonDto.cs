using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// 被祈福者資料 (通用)
    /// 含所有宮廟與服務可能用到的欄位，其中未使用的可為 null
    /// </summary>
    public class PrayedPersonDto
    {
        [JsonProperty("prayedPersonSeq")] public int PrayedPersonSeq { get; set; }

        /// <summary>
        /// 數量 / 服務數量
        /// </summary>
        [JsonProperty(
            "offeringQty",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue(1)]
        public int? OfferingQty { get; set; } = 1;

        /// <summary>
        /// 數量 / 普品數量
        /// </summary>
        [JsonProperty("qty")]
        public int Qty { get; set; }

        /// <summary>
        /// 單價 / 普品單價
        /// </summary>
        [JsonProperty("unitPrice")]
        public int Cost { get; set; }

        /// <summary>
        /// 被祈福者姓名 / 飼主姓名
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = "";

        /// <summary>
        /// 祈福者電話 (若 msisdn 不存在，使用 appMobile)
        /// </summary>
        [JsonProperty("msisdn", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        /// <summary>
        /// 性別：M => 善男, F => 信女
        /// </summary>
        [JsonProperty("gender")] 
        public string Gender { get; set; } = "善男";

        [JsonProperty("serviceType")] public string ServiceType { get; set; }
        [JsonProperty("serviceString")] public string ServiceString { get; set; }


        /// <summary>
        /// 國曆生日 (YYYMMDD，民國年 + 月 + 日)
        /// </summary>
        [JsonProperty("birthday")] 
        public string SolarBirthday { get; set; } = "";
        [JsonIgnore]
        public string sBirth { get; set; } = "";

        /// <summary>
        /// 農曆生日 (YYYMMDD，民國年 + 月 + 日)
        /// </summary>
        [JsonProperty("lunarBirthday")] 
        public string LunarBirthday { get; set; } = "";
        [JsonIgnore]
        public string Birth { get; set; } = "";

        /// <summary>
        /// 是否閏月 (Y/N)，无论 JSON 传来了 null 还是根本没带，最终都默认为 "N"
        /// 如果客户端传了非空字符串，就用客户端的值
        /// </summary>
        [JsonProperty(
            "leapMonth",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue("N")]
        public string LeapMonth { get; set; } = "N";

        /// <summary>
        /// 農曆時辰 (子丑寅…)，同理默认 "吉"
        /// </summary>
        [JsonProperty(
            "lunarBirthTime",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("吉")]
        public string BirthTime { get; set; } = "吉";

        /// <summary>
        /// 農曆生辰月份
        /// </summary>
        [JsonProperty("birthMonth")] 
        public string BirthMonth { get; set; } = "";

        /// <summary>
        /// 年齡
        /// </summary>
        [JsonProperty("age")] 
        public string Age { get; set; } = "0";

        /// <summary>
        /// 生肖
        /// </summary>
        [JsonProperty("zodiac")] 
        public string Zodiac { get; set; } = "";

        /// <summary>
        /// 市話
        /// </summary>
        [JsonProperty(
            "phone",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("")]
        public string HomeNum { get; set; } = "";

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty(
            "email",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("")]
        public string Email { get; set; } = "";

        /// <summary>
        /// 備註
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; } = "";

        /// <summary>
        /// 國內／國外：1 => 國內, 2 => 國外
        /// </summary>
        [JsonProperty("oversea")] 
        public string Oversea { get; set; } = "1";

        /// <summary>
        /// 郵遞區號 (預設 "0")
        /// </summary>
        [JsonProperty("zipCode")] 
        public string ZipCode { get; set; } = "0";

        /// <summary>
        /// 縣/市 (國內地址必填)
        /// </summary>
        [JsonProperty("city")] 
        public string City { get; set; } = "";

        /// <summary>
        /// 地區 (國內地址必填)
        /// </summary>
        [JsonProperty("region")] 
        public string Region { get; set; } = "";

        /// <summary>
        /// 部分地址
        /// </summary>
        [JsonProperty("address")] 
        public string Addr { get; set; } = "";

        /// <summary>
        /// 附加祈福人姓名
        /// </summary>
        [JsonProperty("additionalName")]
        public string AdditionalName { get; set; } = "";

        /// <summary>
        /// 公司行號名稱
        /// </summary>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; } = "";

        /// <summary>
        /// 寵物姓名
        /// </summary>
        [JsonProperty("petName")]
        public string PetName { get; set; } = "";

        /// <summary>
        /// 寵物品種，同理默认 "無"
        /// </summary>
        [JsonProperty(
            "petBreed",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("無")]
        public string PetType { get; set; } = "無";

        /// <summary>
        /// 寵物性別
        /// </summary>
        [JsonProperty("petGender")]
        public string PetGender { get; set; } = "";

        /// <summary>
        /// 寵物生日 (YYYMMDD，民國年 + 月 + 日)
        /// </summary>
        [JsonProperty("petDate")]
        public string petDate { get; set; } = "";
        /// <summary>
        /// 寵物死亡日 (民國yyy年MM月DD日)
        /// </summary>
        [JsonIgnore]
        public string PetsBirth { get; set; } = "";

        /// <summary>
        /// 寵物死亡日 (YYYMMDD，民國年 + 月 + 日)
        /// </summary>
        [JsonProperty("petDeath")]
        public string petDeath { get; set; } = "";
        /// <summary>
        /// 寵物死亡日 (民國yyy年MM月DD日)
        /// </summary>
        [JsonIgnore]
        public string petDeathday { get; set; } = "";


        #region — 祖先/亡者資訊（Deceased） —

        /// <summary>
        /// 祖先/亡者姓名 (超拔法會必填)
        /// </summary>
        [JsonProperty("deceasedName")]
        public string dName { get; set; } = "";

        /// <summary>
        /// 祖先/亡者姓氏 (超拔法會必填)
        /// </summary>
        [JsonProperty("firstName")]
        public string firstName { get; set; } = "";

        /// <summary>
        /// 祖先/亡者民國生日紀元：0 => 民國前, 1 => 民國
        /// </summary>
        [JsonProperty(
            "deceasedBirthRocEra",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue("1")]
        public string dBirthType { get; set; } = "1";

        /// <summary>
        /// 祖先/亡者國曆生日 (YYYMMDD)
        /// </summary>
        [JsonProperty("deceasedBirthday")]
        public string dBirthday { get; set; } = "";
        [JsonIgnore]
        public string dsBirth { get; set; } = "";

        /// <summary>
        /// 祖先/亡者農曆生日 (YYYMMDD)
        /// </summary>
        [JsonProperty("deceasedLunarBirthday")]
        public string dLunarBirthday { get; set; } = "";
        [JsonIgnore]
        public string dBirth { get; set; } = "";

        /// <summary>
        /// 祖先/亡者農曆生日是否為閏月：Y => 是, N => 否
        /// </summary>
        [JsonProperty(
            "deceasedBirthdayLeapMonth",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue("N")]
        public string dLeapMonth { get; set; } = "N";

        /// <summary>
        /// 祖先/亡者農曆時辰
        /// </summary>
        [JsonProperty(
            "deceasedLunarBirthTime",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate
        )]
        [DefaultValue("吉")]
        public string dBirthTime { get; set; } = "吉";

        /// <summary>
        /// 祖先/亡者生肖
        /// </summary>
        [JsonProperty("dzodiac")]
        public string dZodiac { get; set; } = "";

        /// <summary>
        /// 祖先/亡者死亡日期紀元：0 => 民國前, 1 => 民國
        /// </summary>
        [JsonProperty("deceasedDoDRocEra")]
        public string dDoDType { get; set; } = "1";

        /// <summary>
        /// 祖先/亡者死亡日期 (YYYMMDD)
        /// </summary>
        [JsonProperty("deceasedDoD")]
        public string deceasedDoD { get; set; } = "";
        /// <summary>
        /// 祖先/亡者死亡日期 (民國yyy年MM月DD日)
        /// </summary>
        [JsonIgnore]
        public string dDoD { get; set; } = "";
        [JsonIgnore]
        public string dBirthMonth { get; set; } = "";

        /// <summary>
        /// 祖先/亡者農曆死亡日期 (YYYMMDD)
        /// </summary>
        [JsonProperty("deceasedLunarDoD")]
        public string deceasedLunarDoD { get; set; } = "";
        /// <summary>
        /// 祖先/亡者農曆死亡日期 (民國yyy年MM月DD日)
        /// </summary>
        [JsonIgnore]
        public string dLunarDoD { get; set; } = "";

        /// <summary>
        /// 祖先/亡者農曆死亡日期是否為閏月：Y => 是, N => 否
        /// </summary>
        [JsonProperty(
            "deceasedDoDLeepMonth",
            NullValueHandling = NullValueHandling.Ignore,   // 忽略明写的 null
            DefaultValueHandling = DefaultValueHandling.Populate // 缺失时填入 DefaultValue
        )]
        [DefaultValue("N")]
        public string dDoDLeapMonth { get; set; } = "N";

        /// <summary>
        /// 祖先/亡者年紀
        /// </summary>
        [JsonIgnore]
        public string dAge { get; set; } = "0";

        /// <summary>
        /// 祖先/亡者 牌位郵遞區號
        /// </summary>
        [JsonProperty("deceasedZipCode")]
        public string dZipCode { get; set; } = "0";

        /// <summary>
        /// 祖先/亡者 牌位縣/市
        /// </summary>
        [JsonProperty("deceasedCity")]
        public string dCity { get; set; } = "";

        /// <summary>
        /// 祖先/亡者 牌位地區
        /// </summary>
        [JsonProperty("deceasedRegion")]
        public string dRegion { get; set; } = "";

        /// <summary>
        /// 祖先/亡者 牌位部分地址
        /// </summary>
        [JsonProperty("deceasedAddress")]
        public string dAddr { get; set; } = "";

        #endregion

        #region — 加購項目（Additional Offerings） —

        /// <summary>
        /// 是否加購普品：Y => 是, N => 否
        /// </summary>
        [JsonProperty("additionalOffering")]
        public string AdditionalOffering { get; set; } = "N";

        /// <summary>
        /// 加購普品數量
        /// </summary>
        [JsonProperty("additionalOfferingQty")]
        public string AdditionalOfferingQty { get; set; } = "0";

        /// <summary>
        /// 加購普品單價
        /// </summary>
        [JsonProperty("additionalOfferingPrice")]
        public string AdditionalOfferingPrice { get; set; } = "0";

        /// <summary>
        /// 加購金紙數量
        /// </summary>
        [JsonProperty("jossMoneyQty")]
        public string JossMoneyQty { get; set; } = "0";

        /// <summary>
        /// 加購金紙單價
        /// </summary>
        [JsonProperty("jossMoneyPrice")]
        public string JossMoneyPrice { get; set; } = "0";

        #endregion


        #region — 產品資訊（TempleCode） —

        // 由 TempleCodeRepository 附加的欄位
        [JsonIgnore] public int AdminID { get; set; }
        [JsonIgnore] public int ServiceID { get; set; }
        [JsonIgnore] public string TypeID { get; set; }
        [JsonIgnore] public string ItemTypeID { get; set; }
        [JsonIgnore] public string TypeString { get; set; }

        #endregion


        // 收集所有未映射欄位，供擴充用
        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; }
        = new Dictionary<string, object>();  // 一出來就初始化
    }
}