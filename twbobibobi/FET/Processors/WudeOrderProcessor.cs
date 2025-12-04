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
    /// Class: ShoushanyanOrderProcessor
    /// 實作 ITempleOrderProcessor，北港武德宮建單邏輯
    /// </summary>
    public class WudeOrderProcessor : TempleOrderProcessorBase
    {
        /// <summary> 宮廟代碼：北港武德宮 (AdminID = 6) </summary>
        private readonly int _adminID = 6;

        /// <summary> API 專用後綴來源網址，用於串接後端流程。 </summary>
        private string PostUrl = "_wu_Index_FETAPI";

        /// <summary>
        /// 建構式：建立處理器實例-北港武德宮。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例，用於記錄與取得 HttpContext。</param>
        /// <param name="year">當前年度字串。</param>
        public WudeOrderProcessor(
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
        /// 處理訂單的主流程-北港武德宮。
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

            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();

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
                    int applicantId = _lightDAC.Addapplicantinfo_lights_wu(
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
                    applicantId = _lightDAC.Addapplicantinfo_purdue_wu(
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

                    resultOrderNumbers.AddRange(ProcessPurdueing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                //下元補庫
                case "4":
                    PostUrl = "Supplies_wu_Index_FETAPI";

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_supplies_wu(
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

                    resultOrderNumbers.AddRange(ProcessSuppliesing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                //呈疏補庫(天官武財神聖誕補財庫)
                case "5":
                    PostUrl = "Supplies_wu2_Index_FETAPI";

                    // 新增購買人資料
                    applicantId = _lightDAC.Addapplicantinfo_supplies_wu2(
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

                    resultOrderNumbers.AddRange(ProcessSupplies2ing(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));
                    break;
                // 企業補財庫
                case "6":
                    PostUrl = "Supplies_wu3_Index_FETAPI";

                    int newUser = 1;
                    string addDate = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                    string addDateString = dtNow.ToString("yyyy-MM-dd");

                    // 新增購買人資料
                    applicantId = _lightDAC.CheckedSupplies_wu_Info3(
                        Name: applicant.appName,
                        Mobile: applicant.appMobile,
                        Cost: totalAmount,
                        Email: applicant.appEmail,
                        County: applicant.appCity,
                        Dist: applicant.appRegion,
                        Addr: applicant.appAddr,
                        ZipCode: applicant.appzipCode,
                        Sendback: applicant.sendback,
                        ReceiptName: receiptName,
                        ReceiptMobile: receiptMobile,
                        NewUser: newUser,
                        AddDate: addDate,
                        AddDateString: addDateString,
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
                int lightsId = _lightDAC.AddLights_wu(
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
                    HomeNum: p.HomeNum,
                    Count: ResolveOfferingQty(p.OfferingQty),
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Remark: p.Remark,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Lights_wu(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Lights_wu(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int lightsType = _lightDAC.GetLightsType_wu(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var lightsArr = new string[lightsList.Length];
                _lightDAC.UpdateLights_wu_Info(
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
                if (!_lightDAC.UpdateChargeLog_Lights_wu(
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

                // 呼叫 LightDAC
                int purdueId = _lightDAC.Addpurdue_wu(
                    ApplicantID: applicantId,
                    Name: p.Name,
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
                    HomeNum: p.HomeNum,
                    Email: p.Email,
                    Count: ResolveOfferingQty(p.OfferingQty),
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Purdue_wu(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Purdue_wu(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                int purdueType = _lightDAC.GetPurdueType_wu(applicantId, _year);

                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var purdueArr = new string[purdueList.Length];
                _lightDAC.UpdatePurdue_wu_Info(
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
                if (!_lightDAC.UpdateChargeLog_Purdue_wu(
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

        // 下元補庫
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
                int suppliesId = _lightDAC.Addsupplies_wu(
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
                    Email: p.Email,
                    HomeNum: p.HomeNum,
                    Count: ResolveOfferingQty(p.OfferingQty),
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Supplies_wu(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Supplies_wu(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var suppliesArr = new string[suppliesList.Length];
                _lightDAC.UpdateSupplies_wu_Info(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref suppliesList,
                    ref suppliesArr);
                orderNumbers.AddRange(suppliesArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Supplies_wu(
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

        // 呈疏補庫(天官武財神聖誕補財庫)
        private List<string> ProcessSupplies2ing(int applicantId,
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
                int suppliesId = _lightDAC.Addsupplies_wu2(
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
                    Email: p.Email,
                    HomeNum: p.HomeNum,
                    Count: ResolveOfferingQty(p.OfferingQty),
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Supplies_wu2(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Supplies_wu2(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var suppliesArr = new string[suppliesList.Length];
                _lightDAC.UpdateSupplies_wu_Info2(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref suppliesList,
                    ref suppliesArr);
                orderNumbers.AddRange(suppliesArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Supplies_wu2(
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

        // 企業補財庫服務
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
                int suppliesId = _lightDAC.Addsupplies_wu3(
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
                    Email: p.Email,
                    HomeNum: p.HomeNum,
                    Count: ResolveOfferingQty(p.OfferingQty),
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: city,
                    Dist: region,
                    ZipCode: p.ZipCode,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Supplies_wu3(
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
            DataTable dtCharge = _lightDAC.GetChargeLog_Supplies_wu3(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var suppliesArr = new string[suppliesList.Length];
                _lightDAC.UpdateSupplies_wu_Info3(
                    applicantID: applicantId,
                    Year: _year,
                    ref msg,
                    ref suppliesList,
                    ref suppliesArr);
                orderNumbers.AddRange(suppliesArr);

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Supplies_wu3(
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