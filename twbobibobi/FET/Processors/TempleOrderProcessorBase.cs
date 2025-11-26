using twbobibobi.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Temple.data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: TempleOrderProcessorBase
    /// Namespace: twbobibobi.FET.Processors
    /// 
    /// 宮廟訂單處理的抽象基底類別，定義所有 Processor 共用的欄位與方法，
    /// 包含：
    /// - 購買人與祈福人生辰解析（ParseBirth）
    /// - 地址資料補全邏輯
    /// - 燈種額滿檢查（ValidateLightingAvailability）
    /// - 子類別實作的下單流程（HandleOrderAsync）
    /// </summary>
    public abstract class TempleOrderProcessorBase : ITempleOrderProcessor
    {
        #region Protected Fields

        /// <summary> 當前頁面實例，用於取得 HttpContext 或記錄日誌。 </summary>
        protected readonly BasePage _page;
        /// <summary> 操作燈種與祈福資料的資料存取層物件。 </summary>
        protected readonly LightDAC _lightDAC;
        /// <summary> 操作商品資料的資料存取層物件。 </summary>
        protected readonly ProductDAC _productDAC;
        /// <summary> 當前處理的資料年度（例如 "2025"、"2026"）。 </summary>
        protected readonly string _year;

        #endregion

        #region Constructor

        /// <summary>
        /// 建構式：初始化共用的頁面、資料庫輔助類別與年度。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例。</param>
        /// <param name="dbHelper">資料庫輔助類別。</param>
        /// <param name="year">處理的資料年度字串。</param>
        protected TempleOrderProcessorBase(
            BasePage page,
            string year)
        {
            _page = page;
            _year = year;
            _lightDAC = new LightDAC(page);
            _productDAC = new ProductDAC(page);
        }

        #endregion

        #region Main Process Flow

        /// <summary>
        /// 通用的訂單處理流程。
        /// 1. 解析購買人生辰。
        /// 2. 解析祈福人生辰。
        /// 3. 檢查燈種是否額滿。
        /// 4. 委派至子類別執行宮廟專屬的建單邏輯。
        /// </summary>
        /// <param name="applicant">購買人資料。</param>
        /// <param name="prayedPersons">祈福人清單。</param>
        /// <param name="tid">Temple 系統交易代碼。</param>
        /// <param name="fetOrderNumber">FET 訂單編號。</param>
        /// <param name="kind">服務種類代碼（例如 1=點燈）。</param>
        /// <param name="totalAmount">訂單總金額。</param>
        /// <param name="itemsInfo">祈福項目 JSON 字串。</param>
        /// <param name="OrderId">Temple 訂單編號。</param>
        /// <returns>回傳 Task，包含該宮廟所有子訂單編號清單。</returns>
        public async Task<List<string>> ProcessAsync(
            ApplicantDto applicant,
            List<PrayedPersonDto> prayedPersons,
            string tid,
            string fetOrderNumber,
            string kind,
            string totalAmount,
            string itemsInfo,
            string OrderId)
        {
            // 0. 如果購買人生辰存在，解析（共用）
            string appBirth, appBirthMonth, appAge, appZodiac, appsBirth;

            ParseBirth(
                "1",
                applicant.appLunarBirthday,
                applicant.appSolarBirthday,
                out appBirth,
                out appBirthMonth,
                out appAge,
                out appZodiac,
                out appsBirth);

            applicant.appBirth = appBirth;
            applicant.appBirthMonth = appBirthMonth;
            applicant.appAge = appAge;
            applicant.appZodiac = appZodiac;
            applicant.appsBirth = appsBirth;

            // 1. 若購買人地址缺漏，從祈福人補全
            string appAddr, appCounty, appdist, appZipcode;

            appCounty = applicant.appCity;
            appdist = applicant.appRegion;
            appAddr = applicant.appAddr;
            appZipcode = applicant.appzipCode;

            // 2. 先解析祈福人生辰（共用）
            foreach (var p in prayedPersons)
            {
                // 祈福人生辰解析
                string Birth, birthMonth, age, zodiac, sBirth;

                ParseBirth(
                    "1",
                    p.LunarBirthday,
                    p.SolarBirthday,
                    out Birth,
                    out birthMonth,
                    out age,
                    out zodiac,
                    out sBirth);

                p.Birth = Birth;
                p.BirthMonth = birthMonth;
                p.Age = age;
                p.Zodiac = zodiac;
                p.sBirth = sBirth;

                if (string.IsNullOrEmpty(appCounty) && p.Oversea != "2") appCounty = p.City;
                if (string.IsNullOrEmpty(appdist) && p.Oversea != "2") appdist = p.Region;
                if (string.IsNullOrEmpty(appAddr) && p.Oversea != "2") appAddr = p.Addr;
                if (string.IsNullOrEmpty(appZipcode) && p.Oversea != "2") appZipcode = p.ZipCode;
            }

            applicant.appCity = appCounty;
            applicant.appRegion = appdist;
            applicant.appAddr = appAddr;
            applicant.appzipCode = appZipcode;

            //if (applicant.sendback == "N")
            //{
            //    applicant.appCity = applicant.appRegion = applicant.appAddr = "";
            //    applicant.appzipCode = "0";
            //}

            // 3. 共用：檢查所有燈種庫存
            ValidateLightingAvailability(prayedPersons, GetAdminId(), kind);

            // 4. 將剩下的宮廟專屬流程委派給子類
            return await HandleOrderAsync(
                applicant,
                prayedPersons,
                tid,
                fetOrderNumber,
                kind,
                totalAmount,
                itemsInfo,
                OrderId);
        }

        #endregion

        #region Abstract Method

        /// <summary>
        /// 抽象方法：由各宮廟子類別實作實際的建單邏輯。
        /// </summary>
        /// <param name="applicant">購買人資料。</param>
        /// <param name="prayedPersons">祈福人清單。</param>
        /// <param name="tid">Temple 系統交易代碼。</param>
        /// <param name="fetOrderNumber">FET 訂單編號。</param>
        /// <param name="kind">服務種類代碼。</param>
        /// <param name="totalAmount">訂單總金額。</param>
        /// <param name="itemsInfo">祈福項目 JSON 字串。</param>
        /// <param name="OrderId">Temple 訂單編號。</param>
        /// <returns>回傳 partnerOrderNumbers 清單。</returns>
        protected abstract Task<List<string>> HandleOrderAsync(
            ApplicantDto applicant,
            List<PrayedPersonDto> prayedPersons,
            string tid,
            string fetOrderNumber,
            string kind,
            string totalAmount,
            string itemsInfo,
            string OrderId);

        #endregion

        #region Helper: ParseBirth

        /// <summary>
        /// 共用：解析農曆與國曆生日，互相轉換並計算年齡與生肖。
        /// 參數格式為民國年7碼字串（yyyMMdd）。
        /// </summary>
        /// <param name="dBirthType">生辰類型（1=民國，2=民國前）。</param>
        /// <param name="lunarBirthday">農曆生日（yyyMMdd）。</param>
        /// <param name="solarBirthday">國曆生日（yyyMMdd）。</param>
        /// <param name="Birth">輸出：農曆日期文字格式。</param>
        /// <param name="birthMonth">輸出：月份（兩位數字）。</param>
        /// <param name="age">輸出：計算後年齡。</param>
        /// <param name="zodiac">輸出：生肖。</param>
        /// <param name="sBirth">輸出：國曆日期文字格式。</param>
        public void ParseBirth(
            string dBirthType,
            string lunarBirthday,
            string solarBirthday,
            out string Birth,
            out string birthMonth,
            out string age,
            out string zodiac,
            out string sBirth)
        {
            // 初始值
            Birth = lunarBirthday;
            sBirth = solarBirthday;
            birthMonth = age = zodiac = string.Empty;

            try
            {
                bool hasLunar = !string.IsNullOrEmpty(lunarBirthday) && lunarBirthday.Length == 7;
                bool hasSolar = !string.IsNullOrEmpty(solarBirthday) && solarBirthday.Length == 7;

                // 1) 同時提供農曆 & 國曆
                if (hasLunar && hasSolar)
                {
                    // ---- 農曆部分（用於年齡與生肖） ----
                    int rocLy = int.Parse(lunarBirthday.Substring(0, 3));
                    int yearLyAD = rocLy + 1911;
                    int monthLy = int.Parse(lunarBirthday.Substring(3, 2));
                    int dayLy = int.Parse(lunarBirthday.Substring(5, 2));

                    // 計算生肖、年齡
                    LunarSolarConverter.shuxiang(yearLyAD, ref zodiac);
                    age = AjaxBasePage.GetAge(yearLyAD, monthLy, dayLy).ToString();
                    birthMonth = monthLy.ToString("00");

                    // 國曆日期
                    int rocSy = int.Parse(solarBirthday.Substring(0, 3));
                    int monthSy = int.Parse(solarBirthday.Substring(3, 2));
                    int daySy = int.Parse(solarBirthday.Substring(5, 2));

                    // 如果紀元為民國前
                    string lyear = dBirthType == "1" ? rocLy.ToString() : "前" + rocLy.ToString();
                    string syear = dBirthType == "1" ? rocSy.ToString() : "前" + rocSy.ToString();

                    // 格式化輸出
                    Birth = $"民國{lyear}年{monthLy:00}月{dayLy:00}日";
                    sBirth = $"民國{syear}年{monthSy:00}月{daySy:00}日";
                }
                // 2) 只提供農曆，需轉國曆
                else if (hasLunar)
                {
                    int rocLy = int.Parse(lunarBirthday.Substring(0, 3));
                    int yearLyAD = rocLy + 1911;
                    int monthLy = int.Parse(lunarBirthday.Substring(3, 2));
                    int dayLy = int.Parse(lunarBirthday.Substring(5, 2));

                    // 計算生肖、年齡
                    LunarSolarConverter.shuxiang(yearLyAD, ref zodiac);
                    age = AjaxBasePage.GetAge(yearLyAD, monthLy, dayLy).ToString();
                    birthMonth = monthLy.ToString("00");

                    // 如果紀元為民國前
                    string lyear = dBirthType == "1" ? rocLy.ToString() : "前" + rocLy.ToString();

                    // 農曆格式
                    Birth = $"民國{lyear}年{monthLy:00}月{dayLy:00}日";

                    // 農轉國
                    Lunar lunar = new Lunar
                    {
                        lunarYear = yearLyAD,
                        lunarMonth = monthLy,
                        lunarDay = dayLy
                    };
                    Solar sol = LunarSolarConverter.LunarToSolar(lunar);
                    int rocSy = sol.solarYear - 1911;
                    string syear = dBirthType == "1" ? rocSy.ToString() : "前" + rocSy.ToString();
                    sBirth = $"民國{syear}年{sol.solarMonth:00}月{sol.solarDay:00}日";
                }
                // 3) 只提供國曆，需轉農曆
                else if (hasSolar)
                {
                    int rocSy = int.Parse(solarBirthday.Substring(0, 3));
                    int yearSyAD = rocSy + 1911;
                    int monthSy = int.Parse(solarBirthday.Substring(3, 2));
                    int daySy = int.Parse(solarBirthday.Substring(5, 2));

                    // 如果紀元為民國前
                    string syear = dBirthType == "1" ? rocSy.ToString() : "前" + rocSy.ToString();

                    // 國曆格式
                    sBirth = $"民國{syear}年{monthSy:00}月{daySy:00}日";

                    // 國轉農
                    Solar solar = new Solar
                    {
                        solarYear = yearSyAD,
                        solarMonth = monthSy,
                        solarDay = daySy
                    };
                    Lunar lunar = LunarSolarConverter.SolarToLunar(solar);

                    // 計算生肖、年齡
                    LunarSolarConverter.shuxiang(lunar.lunarYear, ref zodiac);
                    age = AjaxBasePage.GetAge(lunar.lunarYear, lunar.lunarMonth, lunar.lunarDay).ToString();
                    birthMonth = lunar.lunarMonth.ToString("00");

                    int rocLy = lunar.lunarYear - 1911;

                    // 如果紀元為民國前
                    string lyear = dBirthType == "1" ? rocLy.ToString() : "前" + rocLy.ToString();

                    Birth = $"民國{lyear}年{lunar.lunarMonth:00}月{lunar.lunarDay:00}日";
                }
                // 4) 都沒提供或格式錯誤，保留原字串
                else
                {
                    Birth = !string.IsNullOrEmpty(lunarBirthday) ? lunarBirthday : "";
                    birthMonth = age = zodiac = string.Empty;
                    sBirth = !string.IsNullOrEmpty(solarBirthday) ? solarBirthday : "";
                }
            }
            catch
            {
                // 若解析失敗，保留原始字串
                Birth = !string.IsNullOrEmpty(lunarBirthday) ? lunarBirthday : "";
                birthMonth = age = zodiac = string.Empty;
                sBirth = !string.IsNullOrEmpty(solarBirthday) ? solarBirthday : "";
            }
        }

        #endregion

        #region Helper: ValidateLightingAvailability

        /// <summary>
        /// 共用：依照 TypeID 分組後檢查各燈種是否額滿。
        /// 若額滿則拋出 InvalidOperationException。
        /// </summary>
        /// <param name="prayedPersons">祈福人清單。</param>
        /// <param name="adminId">宮廟代碼。</param>
        /// <param name="kind">服務種類代碼。</param>
        protected void ValidateLightingAvailability(
            List<PrayedPersonDto> prayedPersons,
            int adminId,
            string kind)
        {
            var groups = prayedPersons.GroupBy(p => p.TypeID);
            foreach (var g in groups)
            {
                int neededQty = g.Sum(p => p.OfferingQty ?? 1);
                switch (kind)
                {
                    case "1":
                        // 點燈
                        bool isFull = _lightDAC.CheckedLightsNum(
                            LightsType: g.Key,
                            AdminID: adminId.ToString(),
                            Count: neededQty,
                            Year: _year, 
                            _page);
                        if (isFull)
                            throw new InvalidOperationException(
                                $"宮廟 {adminId} 的 燈種 {g.Key} 已額滿 ({neededQty} 盞)");
                        break;
                }
            }
        }

        #endregion

        #region Virtual: GetAdminId

        /// <summary>
        /// 取得目前處理器對應的宮廟代碼。
        /// 預設為未實作，必須由子類別覆寫。
        /// </summary>
        /// <returns>宮廟 AdminID。</returns>
        protected virtual int GetAdminId() => throw new NotImplementedException();

        #endregion
    }
}