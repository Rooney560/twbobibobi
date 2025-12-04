using twbobibobi.Data;
using Newtonsoft.Json;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Temple.data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: ShoutianOrderProcessor
    /// 實作 ITempleOrderProcessor，松柏嶺受天宮點燈建單邏輯
    /// </summary>
    public class ShoutianOrderProcessor : TempleOrderProcessorBase
    {
        /// <summary> 宮廟代碼：松柏嶺受天宮 (AdminID = 35) </summary>
        private readonly int _adminID = 35;

        /// <summary> API 專用後綴來源網址，用於串接後端流程。 </summary>
        private string PostUrl = "_st_Index_FETAPI";

        /// <summary>
        /// 建構式：建立處理器實例-松柏嶺受天宮。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例，用於記錄與取得 HttpContext。</param>
        /// <param name="year">當前年度字串。</param>
        public ShoutianOrderProcessor(
            BasePage page,
            string year)
            : base(page, year)
        {
        }

        /// <summary>
        /// 取得宮廟編號
        /// </summary>
        /// <returns></returns>
        protected override int GetAdminId() => _adminID;

        /// <summary>
        /// 處理訂單的主流程-松柏嶺受天宮。
        /// 依據服務種類（kind）分派至不同處理方法。
        /// </summary>
        /// <param name="applicant">購買人資料</param>
        /// <param name="prayedPersons">祈福人列表</param>
        /// <param name="Tid">交易代碼</param>
        /// <param name="fetOrderNumber">合作方訂單號</param>
        /// <param name="kind">服務種類 (e.g. "1"=點燈)</param>
        /// <param name="totalAmount">取得總金額</param>
        /// <param name="itemsInfo">取得祈福人資料</param>
        /// <param name="OrderId">取得訂單序號</param>
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
            var resultOrderNumbers = new List<string>();

            // 台北時區時間
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            // 處理 Receipt 欄位預設
            string receiptName = string.IsNullOrEmpty(applicant.reName) ? applicant.appName : applicant.reName;
            string receiptMobile = string.IsNullOrEmpty(applicant.reMobile) ? applicant.appMobile : applicant.reMobile;

            // 購買人農曆/國曆生日解析 (暫不寫入 DB，只做預處理)
            string purBirth = applicant.appBirth;
            string purBirthday = applicant.appsBirth;
            string purBirthMonth = string.Empty;
            string purAge = string.Empty;
            string purZodiac = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(purBirth) && purBirth.Length == 7)
                {
                    int lyear = int.Parse(purBirth.Substring(0, 3));
                    int yearAD = lyear + 1911;
                    purBirthMonth = purBirth.Substring(3, 2);
                    string day = purBirth.Substring(5, 2);
                    string dateStr = $"{yearAD}-{purBirthMonth}-{day}";
                    LunarSolarConverter.shuxiang(yearAD, ref purZodiac);
                    if (DateTime.TryParse(dateStr, out var tmpDt))
                        purAge = AjaxBasePage.GetAge(yearAD, int.Parse(purBirthMonth), int.Parse(day)).ToString();
                }
                else if (!string.IsNullOrEmpty(purBirthday) && purBirthday.Length == 7)
                {
                    int syearInt = int.Parse(purBirthday.Substring(0, 3));
                    int yearAD = syearInt + 1911;
                    purBirthMonth = purBirthday.Substring(3, 2);
                    string day = purBirthday.Substring(5, 2);
                    string dateStr = $"{yearAD}-{purBirthMonth}-{day}";
                    LunarSolarConverter.shuxiang(yearAD, ref purZodiac);
                    if (DateTime.TryParse(dateStr, out var tmpDt))
                        purAge = AjaxBasePage.GetAge(yearAD, int.Parse(purBirthMonth), int.Parse(day)).ToString();
                }
            }
            catch
            {
                purAge = purZodiac = purBirthMonth = string.Empty;
            }

            // 根據 kind 選擇服務邏輯
            switch (kind)
            {
                // 點燈
                case "1":
                    PostUrl = "Lights" + PostUrl;

                    // 新增購買人資料
                    int applicantId = _lightDAC.Addapplicantinfo_lights_st(
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
                    string itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    int amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessLighting(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 普度
                case "2":
                    //PostUrl = "Purdue" + PostUrl;

                    //// 新增購買人資料
                    //applicantId = _lightDAC.Addapplicantinfo_purdue_st(
                    //    Name: applicant.appName,
                    //    Mobile: applicant.appMobile,
                    //    Cost: totalAmount,
                    //    County: applicant.appCity,
                    //    Dist: applicant.appRegion,
                    //    Addr: applicant.appAddr,
                    //    ZipCode: applicant.appzipCode,
                    //    Sendback: applicant.sendback,
                    //    ReceiptName: receiptName,
                    //    ReceiptMobile: receiptMobile,
                    //    Status: 2,
                    //    AdminID: _adminID.ToString(),
                    //    PostURL: PostUrl,
                    //    Year: _year
                    //);

                    //if (applicantId <= 0)
                    //    return Task.FromResult(resultOrderNumbers);

                    //// 做 JSON 備份
                    //itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    //amount = 0;
                    //int.TryParse(totalAmount, out amount);

                    //resultOrderNumbers.AddRange(ProcessPurdueing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 祈安植福
                case "23":
                    PostUrl = "Blessing" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_blessing_st(
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
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessBlessinging(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
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
                int lightsId = _lightDAC.AddLights_st(
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
            _lightDAC.AddChargeLog_Lights_st(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Lights_st(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int lightsType = _lightDAC.GetLightsType_st(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var lightsArr = new string[lightsList.Length];
                _lightDAC.UpdateLights_st_Info(
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
                if (!_lightDAC.UpdateChargeLog_Lights_st(
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

            // 逐筆插入燈明細
            //foreach (var p in prayedPersons)
            //{
            //    // 取電話與性別
            //    string mobile = !string.IsNullOrEmpty(p.Mobile) ? p.Mobile : applicant.appMobile;
            //    string gender = p.Gender == "F" ? "信女" : "善男";

            //    // 地址
            //    string oversea = p.Oversea ?? "1";
            //    string city = oversea == "1" ? p.City : (p.City ?? string.Empty);
            //    string region = oversea == "1" ? p.Region : (p.Region ?? string.Empty);

            //    // 祖先/亡者 農曆生日與國曆生日，並互相轉換、計算年齡與生肖
            //    string dBirth, dBirthMonth, dage, dZodiac, dsBirth;

            //    ParseBirth(p.dBirthType, p.dLunarBirthday, "", out dBirth, out dBirthMonth, out dage, out dZodiac, out dsBirth);

            //    p.dBirth = dBirth;
            //    p.dBirthMonth = dBirthMonth;
            //    p.dAge = dage;
            //    p.dZodiac = dZodiac;
            //    p.dsBirth = dsBirth;

            //    int count = 0;

            //    string PurdueType = p.TypeID;
            //    switch (PurdueType)
            //    {
            //        case "1":
            //            //贊普
            //            //int.TryParse(item["qty"].ToString(), out count);                                                                                        //數量/普品數量
            //            count = 1;
            //            break;
            //    }

            //    // 呼叫 LightDAC
            //    int purdueId = _lightDAC.Addpurdue_st(
            //        ApplicantID: applicantId,
            //        Name: p.Name,
            //        Mobile: mobile,
            //        Cost: p.Cost,
            //        Sex: gender,
            //        PurdueType: p.TypeID,
            //        PurdueString: p.TypeString,
            //        Oversea: oversea,
            //        Birth: p.Birth,
            //        LeapMonth: p.LeapMonth,
            //        BirthTime: p.BirthTime,
            //        BirthMonth: p.BirthMonth,
            //        Age: p.Age,
            //        Zodiac: p.Zodiac,
            //        sBirth: p.sBirth,
            //        Count: p.OfferingQty ?? count,
            //        Email: p.Email,
            //        Addr: p.Addr,
            //        County: city,
            //        Dist: region,
            //        ZipCode: p.ZipCode,
            //        FirstName: p.firstName,
            //        Remark: p.Remark,
            //        Year: _year);
            //}

            //// 建立交易流水
            //string callbackLog = $"{Tid},{fetOrderNumber}";
            //_lightDAC.AddChargeLog_Purdue_st(
            //    OrderID: OrderId,
            //    ApplicantID: applicantId,
            //    Amount: totalAmount,
            //    ChargeType: "FETAPI",
            //    Status: 0,
            //    Description: itemsJson,
            //    Comment: string.Empty,
            //    PayChannelLog: fetOrderNumber,
            //    IP: _page.Request.UserHostAddress,
            //    Year: _year);

            //// 更新並獲取燈號
            //DataTable dtCharge = _lightDAC.GetChargeLog_Purdue_st(OrderId, _year);
            //if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            //{
            //    int purdueType = _lightDAC.GetPurdueType_st(applicantId, _year);

            //    string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

            //    var purdueArr = new string[purdueList.Length];
            //    _lightDAC.UpdatePurdue_st_Info(
            //        applicantID: applicantId,
            //        PurdueType: purdueType,
            //        Year: _year,
            //        ref msg,
            //        ref purdueList,
            //        ref purdueArr);
            //    orderNumbers.AddRange(purdueArr);

            //    string ChargeType = string.Empty;
            //    //更新流水付費表資訊(付費成功)
            //    if (!_lightDAC.UpdateChargeLog_Purdue_st(
            //        OrderId,
            //        Tid,
            //        msg,
            //        _page.Request.UserHostAddress,
            //        callbackLog, _year, ref ChargeType, ref uStatus))
            //    {
            //        // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
            //        throw new InvalidOperationException("更新交易流水或狀態異常");
            //    }
            //}
            //else
            //{
            //    // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
            //    throw new InvalidOperationException("查無交易流水或狀態異常");
            //}

            return orderNumbers;
        }

        // 祈安植福服務
        private List<string> ProcessBlessinging(int applicantId,
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
            string[] blessingList = new string[0];

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
                int blessingId = _lightDAC.AddBlessing_st(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    BlessingType: p.TypeID,
                    BlessingString: p.TypeString,
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
            _lightDAC.AddChargeLog_Blessing_st(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Blessing_st(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int blessingType = _lightDAC.GetBlessingType_st(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var blessingArr = new string[blessingList.Length];
                _lightDAC.UpdateBlessing_st_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref blessingList,
                    ref blessingArr);
                orderNumbers.AddRange(blessingArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Blessing_st(
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