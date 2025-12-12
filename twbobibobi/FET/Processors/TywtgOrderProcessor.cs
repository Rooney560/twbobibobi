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
using twbobibobi.FET.API;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: ShoushanyanOrderProcessor
    /// 實作 ITempleOrderProcessor，桃園威天宮建單邏輯
    /// </summary>
    public class TywtgOrderProcessor : TempleOrderProcessorBase
    {
        /// <summary> 宮廟代碼：桃園威天宮 (AdminID = 14) </summary>
        private readonly int _adminID = 14;

        /// <summary> API 專用後綴來源網址，用於串接後端流程。 </summary>
        private string PostUrl = "_ty_Index_FETAPI";

        /// <summary>
        /// 建構式：建立處理器實例-桃園威天宮。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例，用於記錄與取得 HttpContext。</param>
        /// <param name="year">當前年度字串。</param>
        public TywtgOrderProcessor(
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
        /// 處理訂單的主流程-桃園威天宮。
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
            // 進入桃園威天宮的處理邏輯時，先記錄一筆日誌
            var page = HttpContext.Current.Handler as CreateOrder;
            if (page != null)
            {
                page.SaveTimingLog("進入 TywtgOrderProcessor.HandleOrderAsync", 0);
            }

            var resultOrderNumbers = new List<string>();

            // 台北時區時間
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            // 處理 Receipt 欄位預設
            string receiptName = string.IsNullOrEmpty(applicant.reName) ? applicant.appName : applicant.reName;
            string receiptMobile = string.IsNullOrEmpty(applicant.reMobile) ? applicant.appMobile : applicant.reMobile;

            // 根據 kind 選擇服務邏輯
            switch (kind)
            {
                // 點燈
                case "1":  
                    PostUrl = "Lights" + PostUrl;

                    // 新增購買人資料
                    int applicantId = _lightDAC.Addapplicantinfo_lights_ty(
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
                    PostUrl = "Purdue" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_purdue_ty(
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
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessPurdueing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 天赦日招財補運
                case "7":
                    PostUrl = "Supplies" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_supplies_ty(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Cost: totalAmount,
                        Email: applicant.appEmail,
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
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessSuppliesing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 關聖帝君聖誕
                case "9":
                    PostUrl = "EmperorGuansheng" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_emperorGuansheng_ty(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Email: applicant.appEmail,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
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
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessEmperorGuanshenging(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 天公生招財補運
                case "18":
                    PostUrl = "Supplies3" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_supplies3_ty(
                        applicant.appName,
                        applicant.appMobile,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Cost: totalAmount,
                        Email: applicant.appEmail,
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
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessSupplies3ing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 千手觀音千燈迎佛法會
                case "25":
                    PostUrl = "QnLight" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_qnlight_ty(
                        AdminID: _adminID.ToString(),
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Email: applicant.appEmail,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        Status: 2,
                        PostURL: PostUrl,
                        Year: _year
                    );

                    if (applicantId <= 0)
                        return Task.FromResult(resultOrderNumbers);

                    // 做 JSON 備份
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessQnLighting(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 新春賀歲感恩招財祿位
                case "27":
                    PostUrl = "Luckaltar" + PostUrl;

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_luckaltar_ty(
                        AdminID: _adminID.ToString(),
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        Birth: applicant.appBirth,
                        LeapMonth: applicant.appLeapMonth,
                        BirthTime: applicant.appBirthTime,
                        BirthMonth: applicant.appBirthMonth,
                        Age: applicant.appAge,
                        Zodiac: applicant.appZodiac,
                        sBirth: applicant.appsBirth,
                        Email: applicant.appEmail,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        Status: 2,
                        PostURL: PostUrl,
                        Year: _year
                    );

                    if (applicantId <= 0)
                        return Task.FromResult(resultOrderNumbers);

                    // 做 JSON 備份
                    itemsJson = JsonConvert.SerializeObject(prayedPersons);

                    amount = 0;
                    int.TryParse(totalAmount, out amount);

                    resultOrderNumbers.AddRange(ProcessLuckaltaring(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
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
                int lightsId = _lightDAC.AddLights_ty(
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
                    Email: p.AdditionalData.TryGetValue("email", out var e) ? e.ToString() : string.Empty,
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
            _lightDAC.AddChargeLog_Lights_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Lights_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int lightsType = _lightDAC.GetLightsType_ty(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var lightsArr = new string[lightsList.Length];
                _lightDAC.UpdateLights_ty_Info(
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
                if (!_lightDAC.UpdateChargeLog_Lights_ty(
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
            foreach (var p in prayedPersons)
            {
                // 取電話與性別
                string mobile = !string.IsNullOrEmpty(p.Mobile) ? p.Mobile : applicant.appMobile;
                string gender = p.Gender == "F" ? "信女" : "善男";

                // 地址
                string oversea = p.Oversea ?? "1";
                string city = oversea == "1" ? p.City : (p.City ?? string.Empty);
                string region = oversea == "1" ? p.Region : (p.Region ?? string.Empty);

                string purdueString = p.TypeString;
                string purdueItem = string.Empty;
                string firstName = string.Empty;
                int count = 0, count_3rice = 0, count_50rice = 0;

                switch (p.TypeString)
                {
                    case "O氏歷代祖先":
                        purdueString = "光明功德主";
                        purdueItem = "超薦大牌-" + p.TypeString;

                        count = p.OfferingQty ?? 1;

                        if (p.dName != "")
                        {
                            p.firstName = p.dName;
                            p.dName = "";
                        }
                        break;
                    case "累劫冤親債主":
                        purdueString = "光明功德主";
                        purdueItem = "超薦大牌-" + p.TypeString;

                        count = p.OfferingQty ?? 1;
                        break;
                    case "贊普":
                        count = p.OfferingQty ?? 1;
                        break;
                    case "白米50台斤":
                        count = p.OfferingQty ?? 1;

                        int purdueId2 = _lightDAC.Addpurdue_ty(
                            ApplicantID: applicantId,
                            Name: p.Name,
                            Mobile: mobile,
                            Cost: p.Cost,
                            Sex: gender,
                            PurdueType: "1",
                            PurdueString: "贊普",
                            Oversea: oversea,
                            Birth: p.Birth,
                            LeapMonth: p.LeapMonth,
                            BirthTime: p.BirthTime,
                            BirthMonth: p.BirthMonth,
                            Age: p.Age,
                            Zodiac: p.Zodiac,
                            sBirth: p.sBirth,
                            Count: count,
                            Count_3rice: count_3rice,
                            Count_50rice: count_50rice,
                            Remark: p.Remark,
                            Addr: p.Addr,
                            County: city,
                            Dist: region,
                            ZipCode: p.ZipCode,
                            PurdueItem: purdueItem,
                            DeathName: p.dName,
                            FirstName: p.firstName,
                            MomName: "",
                            LastName: "",
                            Reason: "",
                            LicenseNum: "",
                            DeathAddr: p.dAddr,
                            DeathCounty: p.dCity,
                            DeathDist: p.dRegion,
                            DeathZipCode: p.dZipCode,
                            PurdueItem1: "",
                            DeathName1: "",
                            FirstName1: "",
                            MomName1: "",
                            LastName1: "",
                            Reason1: "",
                            LicenseNum1: "",
                            DeathAddr1: "",
                            DeathCounty1: "",
                            DeathDist1: "",
                            DeathZipCode1: "0",
                            Year: _year);

                        count_50rice = p.OfferingQty ?? 1;
                        count = 0;
                        break;
                    case "白米3台斤":
                        count = p.OfferingQty ?? 1;

                        int purdueId3 = _lightDAC.Addpurdue_ty(
                            ApplicantID: applicantId,
                            Name: p.Name,
                            Mobile: mobile,
                            Cost: p.Cost,
                            Sex: gender,
                            PurdueType: "1",
                            PurdueString: "贊普",
                            Oversea: oversea,
                            Birth: p.Birth,
                            LeapMonth: p.LeapMonth,
                            BirthTime: p.BirthTime,
                            BirthMonth: p.BirthMonth,
                            Age: p.Age,
                            Zodiac: p.Zodiac,
                            sBirth: p.sBirth,
                            Count: count,
                            Count_3rice: count_3rice,
                            Count_50rice: count_50rice,
                            Remark: p.Remark,
                            Addr: p.Addr,
                            County: city,
                            Dist: region,
                            ZipCode: p.ZipCode,
                            PurdueItem: purdueItem,
                            DeathName: p.dName,
                            FirstName: p.firstName,
                            MomName: "",
                            LastName: "",
                            Reason: "",
                            LicenseNum: "",
                            DeathAddr: p.dAddr,
                            DeathCounty: p.dCity,
                            DeathDist: p.dRegion,
                            DeathZipCode: p.dZipCode,
                            PurdueItem1: "",
                            DeathName1: "",
                            FirstName1: "",
                            MomName1: "",
                            LastName1: "",
                            Reason1: "",
                            LicenseNum1: "",
                            DeathAddr1: "",
                            DeathCounty1: "",
                            DeathDist1: "",
                            DeathZipCode1: "0",
                            Year: _year);

                        count_3rice = p.OfferingQty ?? 1;
                        count = 0;
                        break;
                }

                // 呼叫 LightDAC
                int purdueId = _lightDAC.Addpurdue_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    PurdueType: p.TypeID,
                    PurdueString: purdueString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Count: count,
                    Count_3rice: count_3rice,
                    Count_50rice: count_50rice,
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    PurdueItem: purdueItem,
                    DeathName: p.dName,
                    FirstName: p.firstName,
                    MomName: "",
                    LastName: "",
                    Reason: "",
                    LicenseNum: "",
                    DeathAddr: p.dAddr,
                    DeathCounty: p.dCity,
                    DeathDist: p.dRegion,
                    DeathZipCode: p.dZipCode,
                    PurdueItem1: "",
                    DeathName1: "",
                    FirstName1: "",
                    MomName1: "",
                    LastName1: "",
                    Reason1: "",
                    LicenseNum1: "",
                    DeathAddr1: "",
                    DeathCounty1: "",
                    DeathDist1: "",
                    DeathZipCode1: "0",
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Purdue_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Purdue_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int purdueType = _lightDAC.GetPurdueType_ty(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var purdueArr = new string[purdueList.Length];
                _lightDAC.UpdatePurdue_ty_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref purdueList,
                    ref purdueArr);
                orderNumbers.AddRange(purdueArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Purdue_ty(
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

        // 天赦日招財補運服務
        private List<string> ProcessSuppliesing(int applicantId,
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
            string[] suppliesList = new string[0];

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
                int suppliesId = _lightDAC.Addsupplies_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    SuppliesType: p.TypeID,
                    SuppliesString: p.TypeString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Email: p.AdditionalData.TryGetValue("email", out var e) ? e.ToString() : string.Empty,
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
            _lightDAC.AddChargeLog_Supplies_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Supplies_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var suppliesArr = new string[suppliesList.Length];
                _lightDAC.UpdateSupplies_ty_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref suppliesList,
                    ref suppliesArr);
                orderNumbers.AddRange(suppliesArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Supplies_ty(
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

        // 關聖帝君聖誕千秋祝壽謝恩祈福活動
        private List<string> ProcessEmperorGuanshenging(int applicantId,
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
            string[] emperorGuanshengList = new string[0];

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
                int emperorGuanshengId = _lightDAC.Addemperorguansheng_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    EmperorGuanshengType: p.TypeID,
                    EmperorGuanshengString: p.TypeString,
                    Oversea: oversea,
                    Birth: p.Birth,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.BirthMonth,
                    Age: p.Age,
                    Zodiac: p.Zodiac,
                    sBirth: p.sBirth,
                    Count: p.OfferingQty ?? 1,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Remark: p.Remark,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_EmperorGuansheng_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_EmperorGuansheng_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var emperorGuanshengArr = new string[emperorGuanshengList.Length];
                _lightDAC.UpdateEmperorGuansheng_ty_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref emperorGuanshengList,
                    ref emperorGuanshengArr);
                orderNumbers.AddRange(emperorGuanshengArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_EmperorGuansheng_ty(
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

        // 天公生招財補運
        private List<string> ProcessSupplies3ing(int applicantId,
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
            string[] suppliesList = new string[0];

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
                int suppliesId = _lightDAC.Addsupplies3_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    SuppliesType: p.TypeID,
                    SuppliesString: p.TypeString,
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
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Supplies3_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Supplies3_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var suppliesArr = new string[suppliesList.Length];
                _lightDAC.UpdateSupplies3_ty_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref suppliesList,
                    ref suppliesArr);
                orderNumbers.AddRange(suppliesArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Supplies3_ty(
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

        // 千手觀音千燈迎佛法會服務
        private List<string> ProcessQnLighting(int applicantId,
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
            string[] qnlightList = new string[0];

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
                int qnlightId = _lightDAC.AddQnLight_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    QnLightType: p.TypeID,
                    QnLightString: p.TypeString,
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
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_QnLight_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_QnLight_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int qnlightType = _lightDAC.GetQnLightType_ty(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var qnlightArr = new string[qnlightList.Length];
                _lightDAC.UpdateQnLight_ty_Info(
                    applicantID: applicantId,
                    QnLightType: qnlightType,
                    Year: _year,
                    ref msg,
                    ref qnlightList,
                    ref qnlightArr);
                orderNumbers.AddRange(qnlightArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_QnLight_ty(
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


        // 新春賀歲感恩招財祿位服務
        private List<string> ProcessLuckaltaring(int applicantId,
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
            string[] luckaltarList = new string[0];

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
                int luckaltarId = _lightDAC.AddLuckaltar_ty(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: mobile,
                    Cost: p.Cost,
                    Sex: gender,
                    LuckaltarType: p.TypeID,
                    LuckaltarString: p.TypeString,
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
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Luckaltar_ty(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Luckaltar_ty(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int luckaltarType = _lightDAC.GetLuckaltarType_ty(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var luckaltarArr = new string[luckaltarList.Length];
                _lightDAC.UpdateLuckaltar_ty_Info(
                    applicantID: applicantId,
                    LuckaltarType: luckaltarType,
                    Year: _year,
                    ref msg,
                    ref luckaltarList,
                    ref luckaltarArr);
                orderNumbers.AddRange(luckaltarArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Luckaltar_ty(
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