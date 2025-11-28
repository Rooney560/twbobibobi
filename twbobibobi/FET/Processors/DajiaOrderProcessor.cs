using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using Temple.data;
using twbobibobi.Data;
using twbobibobi.FET.API;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: DajiaOrderProcessor
    /// 實作 <see cref="ITempleOrderProcessor"/>，
    /// 負責處理「大甲鎮瀾宮」的訂單邏輯（點燈 / 普度 / 七朝清醮）。
    /// </summary>
    public class DajiaOrderProcessor : TempleOrderProcessorBase
    {
        /// <summary> 宮廟代碼：大甲鎮瀾宮 (AdminID = 3) </summary>
        private readonly int _adminID = 3;

        /// <summary> API 專用後綴來源網址，用於串接後端流程。 </summary>
        private string PostUrl = "_da_Index_FETAPI";

        /// <summary>
        /// 建構式：建立大甲鎮瀾宮處理器實例。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例，用於記錄與取得 HttpContext。</param>
        /// <param name="dbHelper">資料庫協助物件，用於 DB 操作。</param>
        /// <param name="year">當前年度字串。</param>
        public DajiaOrderProcessor(
            BasePage page,
            string year)
            : base(page, year)
        {
        }

        /// <summary>
        /// 取得宮廟編號（大甲鎮瀾宮）。
        /// </summary>
        /// <returns>AdminID 整數值。</returns>
        protected override int GetAdminId() => _adminID;

        /// <summary>
        /// 處理大甲鎮瀾宮訂單的主流程。
        /// 依據服務種類（kind）分派至不同處理方法。
        /// </summary>
        /// <param name="applicant">購買人資料。</param>
        /// <param name="prayedPersons">祈福人資料清單。</param>
        /// <param name="Tid">交易識別碼。</param>
        /// <param name="fetOrderNumber">合作方訂單編號 (FET)。</param>
        /// <param name="kind">服務種類代碼 (1=點燈, 2=普度, 13=七朝清醮)。</param>
        /// <param name="totalAmount">訂單總金額。</param>
        /// <param name="itemsInfo">祈福項目 JSON 字串。</param>
        /// <param name="OrderId">Temple 系統訂單編號。</param>
        /// <returns>回傳 Task，包含該筆訂單所有產生的子訂單號清單。</returns>
        protected override Task<List<string>> HandleOrderAsync(
            ApplicantDto applicant,
            List<PrayedPersonDto> prayedPersons,
            string Tid,
            string fetOrderNumber,
            string kind,
            string totalAmount,
            string itemsInfo,
            string OrderId)
        {
            // 進大甲鎮瀾宮的處理邏輯時，先記錄一筆日誌
            var page = HttpContext.Current.Handler as CreateOrder;
            if (page != null)
            {
                page.SaveTimingLog("進入 DajiaOrderProcessor.HandleOrderAsync", 0);
            }

            var resultOrderNumbers = new List<string>();

            // 取得台北時區時間
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            // 處理 收件人姓名、收件人電話 欄位預設
            string receiptName = string.IsNullOrEmpty(applicant.reName) ? applicant.appName : applicant.reName;
            string receiptMobile = string.IsNullOrEmpty(applicant.reMobile) ? applicant.appMobile : applicant.reMobile;

            // 根據 kind 選擇服務邏輯
            switch (kind)
            {
                // 點燈
                case "1":  
                    PostUrl = "Lights" + PostUrl;

                    // 新增購買人資料
                    int applicantId = _lightDAC.Addapplicantinfo_lights_da(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        Email: applicant.appEmail,
                        Status: 2,
                        AdminID: _adminID.ToString(),
                        PostURL: PostUrl,
                        Year: _year
                    );

                    if (applicantId <= 0)
                        return Task.FromResult(resultOrderNumbers);

                    // 做 JSON 備份
                    //string itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    int amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessLighting(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 普度
                case "2":
                    PostUrl = "Purdue" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_purdue_da(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        Email: applicant?.appEmail,
                        Status: 2,
                        AdminID: _adminID.ToString(),
                        PostURL: PostUrl,
                        Year: _year
                    );

                    if (applicantId <= 0)
                        return Task.FromResult(resultOrderNumbers);

                    // 做 JSON 備份
                    //itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessPurdueing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 七朝清醮
                case "13":
                    PostUrl = "TaoistJiaoCeremony" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_TaoistJiaoCeremony_da(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Email: applicant.appEmail,
                        Cost: totalAmount,
                        ZipCode: applicant.appzipCode,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        Status: 2,
                        AdminID: _adminID.ToString(),
                        PostURL: PostUrl,
                        Year: _year
                    );

                    if (applicantId <= 0)
                        return Task.FromResult(resultOrderNumbers);

                    // 做 JSON 備份
                    //itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessTaoistJiaoCeremonying(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;

                default:
                    throw new NotSupportedException($"不支援的服務種類 kind={kind}");
            }

            return Task.FromResult(resultOrderNumbers);
        }


        // 點燈服務
        private List<string> ProcessLighting(int applicantId,
                                             ApplicantDto applicant,
                                             List<PrayedPersonDto> prayedPersons,
                                             string Tid,
                                             string fetOrderNumber,
                                             int totalAmount,
                                             string itemsJson,
                                             string OrderId,
                                             DateTime dtNow)
        {
            var orderNumbers = new List<string>();
            string[] lightsList = new string[0];

            // 逐筆插入燈明細
            foreach (var p in prayedPersons)
            {
                // 取電話與性別
                string mobile = !string.IsNullOrEmpty(p.Mobile) ? p.Mobile : applicant.appMobile;
                string gender = p.Gender == "F" ? "信女" : "善男";

                // 地址
                string oversea = p.Oversea ?? "1";
                string city = oversea == "1" ? p.City : (p.City ?? string.Empty);
                string region = oversea == "1" ? p.Region : (p.Region ?? string.Empty);

                // 呼叫 LightDAC
                int lightsId = _lightDAC.AddLights_da(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    LightsType: p.TypeID,
                    LightsString: p.TypeString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Email: p.Email,
                    Count: p.OfferingQty ?? 1,
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Lights_da(
                OrderID: OrderId,
                ApplicantID: applicantId,
                Amount: totalAmount,
                ChargeType: "FETAPI",
                Status: 0,
                Description: itemsJson,
                Comment: string.Empty,
                PayChannelLog: fetOrderNumber,
                IP: _page.Request.UserHostAddress,
                Year: _year);

            // 更新並獲取燈號
            DataTable dtCharge = _lightDAC.GetChargeLog_Lights_da(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int lightsType = _lightDAC.GetLightsType_da(applicantId, _year);

                string msg = "感謝購買,已成功付款" + totalAmount + "元,您的訂單編號 ";

                var lightsArr = new string[lightsList.Length];
                _lightDAC.UpdateLights_da_Info(
                    applicantID: applicantId,
                    LightsType: lightsType,
                    Year: _year,
                    ref msg,
                    ref lightsList,
                    ref lightsArr);
                orderNumbers.AddRange(lightsArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Lights_da(
                    OrderId, 
                    Tid, 
                    msg,
                    _page.Request.UserHostAddress,
                    callbackLog, _year, ref ChargeType, ref uStatus))
                {
                    // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                    throw new InvalidOperationException("更新交易流水或狀態異常");
                }
            }
            else
            {
                // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                throw new InvalidOperationException("查無交易流水或狀態異常");
            }

            return orderNumbers;
        }

        // 普度服務
        private List<string> ProcessPurdueing(int applicantId,
                                             ApplicantDto applicant,
                                             List<PrayedPersonDto> prayedPersons,
                                             string Tid,
                                             string fetOrderNumber,
                                             int totalAmount,
                                             string itemsJson,
                                             string OrderId,
                                             DateTime dtNow)
        {
            var orderNumbers = new List<string>();
            string[] purdueList = new string[0];

            // 逐筆插入明細
            foreach (var p in prayedPersons)
            {
                // 取電話與性別
                string mobile = !string.IsNullOrEmpty(p.Mobile) ? p.Mobile : applicant.appMobile;
                string gender = p.Gender == "F" ? "信女" : "善男";

                // 地址
                string oversea = p.Oversea ?? "1";
                string city = oversea == "1" ? p.City : (p.City ?? string.Empty);
                string region = oversea == "1" ? p.Region : (p.Region ?? string.Empty);

                // 祖先/亡者 農曆生日與國曆生日，並互相轉換、計算年齡與生肖
                string dBirth, dBirthMonth, dage, dZodiac, dsBirth;

                ParseBirth(p.dBirthType, p.dLunarBirthday, "", out dBirth, out dBirthMonth, out dage, out dZodiac, out dsBirth);

                p.dBirth = dBirth;
                p.dsBirth = dsBirth;

                // 祖先/亡者 國曆死亡日期，並計算死亡時年齡
                string dDoD = string.Empty;
                string ddod = p.deceasedDoD;
                string dbirth = p.dLunarBirthday;
                if (ddod.Length == 7 && dbirth.Length == 7)
                {
                    int rocSy = int.Parse(dbirth.Substring(0, 3));
                    int yearSyAD = rocSy + 1911;
                    int monthSy = int.Parse(dbirth.Substring(3, 2));
                    int daySy = int.Parse(dbirth.Substring(5, 2));

                    int drocSy = int.Parse(ddod.Substring(0, 3));
                    int dyearSyAD = drocSy + 1911;
                    int dmonthSy = int.Parse(ddod.Substring(3, 2));
                    int ddaySy = int.Parse(ddod.Substring(5, 2));

                    // 如果紀元為民國前
                    string syear = p.dDoDType == "1" ? drocSy.ToString() : "前" + drocSy.ToString();

                    // 國曆格式
                    dDoD = $"民國{drocSy}年{dmonthSy:00}月{ddaySy:00}日";

                    string DDod = dyearSyAD + "-" + dmonthSy + "-" + ddaySy;

                    if (p.dBirth != "" && DDod != "")
                    {
                        dage = AjaxBasePage.GetAge(yearSyAD, monthSy, daySy, dyearSyAD, dmonthSy, ddaySy).ToString();
                    }

                    p.dDoD = dDoD;
                    p.dBirthMonth = "{dmonthSy: 00}";
                    p.dAge = dage;
                }

                string PurdueType = p.TypeID;
                switch (PurdueType)
                {
                    case "1":
                        //贊普
                        break;
                    case "2":
                        //九玄七祖
                        p.firstName = p.dName;
                        p.dName = "";
                        break;
                    case "3":
                        //亡者
                        break;
                    case "4":
                        //地基主
                        break;
                    case "5":
                        //冤親債主
                        p.AdditionalName = "";
                        break;
                    case "6":
                        //嬰靈
                        p.firstName = p.dName;
                        p.dName = "";
                        break;
                }

                if (PurdueType != "1")
                {
                    p.OfferingQty = 1;
                }

                // 呼叫 LightDAC
                int purdueId = _lightDAC.Addpurdue_da(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Name2: p.AdditionalName,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    PurdueType: p.TypeID,
                    PurdueString: p.TypeString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Count: p.OfferingQty ?? 1,
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Sendback: applicant.sendback == "Y" ? "1" : "0",
                    rName: applicant.reName,
                    rMobile: applicant.reMobile,
                    rAddr: applicant.appAddr,
                    rCounty: applicant.appCity,
                    rDist: applicant.appRegion,
                    rZipCode: applicant.appzipCode,
                    DeathName: p.dName,
                    Birthday: p.dBirth,
                    DeathLeapMonth: p.dLeapMonth,
                    DeathBirthTime: p.dBirthTime,
                    Deathday: p.dDoD,
                    DeathAge: p.dAge,
                    FirstName: p.firstName,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Purdue_da(
                OrderID: OrderId,
                ApplicantID: applicantId,
                Amount: totalAmount,
                ChargeType: "FETAPI",
                Status: 0,
                Description: itemsJson,
                Comment: string.Empty,
                PayChannelLog: fetOrderNumber,
                IP: _page.Request.UserHostAddress,
                Year: _year);

            // 更新並獲取燈號
            DataTable dtCharge = _lightDAC.GetChargeLog_Purdue_da(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int purdueType = _lightDAC.GetPurdueType_da(applicantId, _year);

                string msg = "感謝購買,已成功付款" + totalAmount + "元,您的訂單編號 ";

                var purdueArr = new string[purdueList.Length];
                _lightDAC.UpdatePurdue_da_Info(
                    applicantID: applicantId,
                    PurdueType: purdueType,
                    Year: _year,
                    ref msg,
                    ref purdueList,
                    ref purdueArr);
                orderNumbers.AddRange(purdueArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Purdue_da(
                    OrderId,
                    Tid,
                    msg,
                    _page.Request.UserHostAddress,
                    callbackLog, _year, ref ChargeType, ref uStatus))
                {
                    // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                    throw new InvalidOperationException("更新交易流水或狀態異常");
                }
            }
            else
            {
                // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                throw new InvalidOperationException("查無交易流水或狀態異常");
            }

            return orderNumbers;
        }

        // 七朝清醮服務
        private List<string> ProcessTaoistJiaoCeremonying(int applicantId,
                                             ApplicantDto applicant,
                                             List<PrayedPersonDto> prayedPersons,
                                             string Tid,
                                             string fetOrderNumber,
                                             int totalAmount,
                                             string itemsJson,
                                             string OrderId,
                                             DateTime dtNow)
        {
            var orderNumbers = new List<string>();
            string[] TaoistJiaoCeremonyList = new string[0];

            // 逐筆插入燈明細
            foreach (var p in prayedPersons)
            {
                // 取電話與性別
                string mobile = !string.IsNullOrEmpty(p.Mobile) ? p.Mobile : applicant.appMobile;
                string gender = p.Gender == "F" ? "信女" : "善男";

                // 地址
                string oversea = p.Oversea ?? "1";
                string city = oversea == "1" ? p.City : (p.City ?? string.Empty);
                string region = oversea == "1" ? p.Region : (p.Region ?? string.Empty);

                // 呼叫 LightDAC
                int TaoistJiaoCeremonyId = _lightDAC.AddTaoistJiaoCeremony_da(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Name2: p.AdditionalName,
                    Name3: "",
                    Name4: "",
                    Name5: "",
                    Name6: "",
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    TaoistJiaoCeremonyType: p.TypeID,
                    TaoistJiaoCeremonyString: p.TypeString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Count: p.OfferingQty ?? 1,
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Sendback: applicant.sendback,
                    rName: applicant.reName,
                    rMobile: applicant.reMobile,
                    rAddr: applicant.appAddr,
                    rCounty: applicant.appCity,
                    rDist: applicant.appRegion,
                    rZipCode: applicant.appzipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_TaoistJiaoCeremony_da(
                OrderID: OrderId,
                ApplicantID: applicantId,
                Amount: totalAmount,
                ChargeType: "FETAPI",
                Status: 0,
                Description: itemsJson,
                Comment: string.Empty,
                PayChannelLog: fetOrderNumber,
                IP: _page.Request.UserHostAddress,
                Year: _year);

            // 更新並獲取燈號
            DataTable dtCharge = _lightDAC.GetChargeLog_TaoistJiaoCeremony_da(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "感謝購買,已成功付款" + totalAmount + "元,您的訂單編號 ";

                var TaoistJiaoCeremonyArr = new string[TaoistJiaoCeremonyList.Length];
                _lightDAC.UpdateTaoistJiaoCeremony_da_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref TaoistJiaoCeremonyList,
                    ref TaoistJiaoCeremonyArr);
                orderNumbers.AddRange(TaoistJiaoCeremonyArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_TaoistJiaoCeremony_da(
                    OrderId,
                    Tid,
                    msg,
                    _page.Request.UserHostAddress,
                    callbackLog, _year, ref ChargeType, ref uStatus))
                {
                    // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                    throw new InvalidOperationException("更新交易流水或狀態異常");
                }
            }
            else
            {
                // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                throw new InvalidOperationException("查無交易流水或狀態異常");
            }

            return orderNumbers;
        }
    }
}