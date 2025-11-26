using twbobibobi.Data;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace twbobibobi.Temples
{
    /// <summary>
    /// 桃園龍德宮點燈服務頁面 (元辰光明燈/太歲燈/文昌功名燈/五路財神燈/健康延壽燈/月老姻緣燈/明心智慧燈)
    /// </summary>
    /// <remarks>
    /// 此頁面繼承自 AjaxBasePage，負責：
    /// 1. 控制祈福燈顯示狀態（元辰光明燈、太歲燈、文昌功名燈、五路財神燈、健康延壽燈、月老姻緣燈、明心智慧燈）
    /// 2. 初始化 Ajax 處理器（建立/修改報名資料）
    /// 3. 處理購買人與祈福人資料寫入流程
    /// </remarks>
    public partial class templeService_lights_ld : AjaxBasePage
    {
        /// <summary> 購買人編號 ApplicantID </summary>
        public int aid = 0;
        /// <summary> 宮廟代碼對應 AdminID </summary>
        public int a = 0;
        /// <summary> 當前年份（動態設定年度資料庫名稱用） </summary>
        protected static string Year = "2026";

        /// <summary>
        /// 初始化 Ajax Handler（綁定 gotochecked / editinfo）
        /// </summary>
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "gotochecked");
            AddAjaxHandler(typeof(AjaxPageHandler), "editinfo");
        }

        /// <summary>
        /// 頁面載入事件
        /// </summary>
        /// <param name="sender">觸發者物件</param>
        /// <param name="e">事件參數</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 設定時區為台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();

                // 取得購買人編號
                if (Request["aid"] != null)
                {
                    aid = int.Parse(Request["aid"]);
                }

                // 固定桃園龍德宮 AdminID = 32
                int adminID = a = 32;

                // 預設七種燈種已額滿字串都隱藏
                this.light1.Visible = false;
                this.light2.Visible = false;
                this.light3.Visible = false;
                this.light4.Visible = false;
                this.light5.Visible = false;
                this.light6.Visible = false;
                this.light7.Visible = false;



                LightDAC objLightDAC = new LightDAC(this);

                // 顯示 元辰光明燈
                this.light1.Visible = objLightDAC.CheckedLightsNum("3", adminID.ToString(), 1, Year, this);

                // 顯示 太歲燈
                this.light2.Visible = objLightDAC.CheckedLightsNum("4", adminID.ToString(), 1, Year, this);

                // 顯示 文昌功名燈
                this.light3.Visible = objLightDAC.CheckedLightsNum("5", adminID.ToString(), 1, Year, this);

                // 顯示 五路財神燈
                this.light4.Visible = objLightDAC.CheckedLightsNum("6", adminID.ToString(), 1, Year, this);

                // 顯示 健康延壽燈
                this.light5.Visible = objLightDAC.CheckedLightsNum("8", adminID.ToString(), 1, Year, this);

                // 顯示 月老姻緣燈
                this.light6.Visible = objLightDAC.CheckedLightsNum("20", adminID.ToString(), 1, Year, this);

                // 顯示 明心智慧燈
                this.light7.Visible = objLightDAC.CheckedLightsNum("33", adminID.ToString(), 1, Year, this);

            }
        }

        /// <summary>
        /// 處理 AJAX 要求的內部類別
        /// </summary>
        public class AjaxPageHandler
        {
            /// <summary> 購買人編號 Applicant ID </summary>
            public int ApplicantID = 0;
            /// <summary> 祈福人編號 Lights ID </summary>
            public int LightsID = 0;

            /// <summary>
            /// 建立資料（新增購買人與祈福人資料）
            /// </summary>
            /// <param name="basePage">基礎頁面物件 (繼承 BasePage)</param>
            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "32";

                // === 購買人資料 ===
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["AppEmail"];                                     //購買人信箱
                string AppSendback = basePage.Request["AppSendback"];                               //贈品處理方式
                string ReceiptName = basePage.Request["ReceiptName"];                               //收件人姓名
                string ReceiptMobile = basePage.Request["ReceiptMobile"];                           //收件人電話
                string AppZipCode = basePage.Request["AppZipCode"];                                 //收件人郵遞區號
                string AppCounty = basePage.Request["AppCounty"];                                   //收件人縣市
                string Appdist = basePage.Request["Appdist"];                                       //收件人區域
                string AppAddr = basePage.Request["AppAddr"];                                       //收件人部分地址

                // === 祈福人相關資料（多筆） ===
                string name_Tag = basePage.Request["name_Tag"];                                     //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                                 //祈福人電話
                string sex_Tag = basePage.Request["sex_Tag"];                                       //祈福人性別
                string birth_Tag = basePage.Request["birth_Tag"];                                   //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];                           //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];                           //祈福人農曆時辰
                string sbirth_Tag = basePage.Request["sbirth_Tag"];                                 //祈福人國曆生日
                string oversea_Tag = basePage.Request["oversea_Tag"];                               //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string remark_Tag = basePage.Request["remark_Tag"];                                 //備註
                string LightsString_Tag = basePage.Request["LightsString_Tag"];                     //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量

                // === 轉換為 JArray 以便後續處理 ===
                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsex = JArray.Parse(sex_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray Jsbirth = JArray.Parse(sbirth_Tag);
                JArray Joversea = JArray.Parse(oversea_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jremark = JArray.Parse(remark_Tag);
                JArray JLightsString_Tag = JArray.Parse(LightsString_Tag);

                string postURL_Init = "Lights_ld_Index";
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                string postURL = GetRequestURL(url, postURL_Init);

                // === 確認各燈種數量 ===
                int[] count_ld_lights = new int[7];
                bool checkednum_ld = true;
                for (int i = 0; i < listcount; i++)
                {
                    switch (JLightsString_Tag[i].ToString())
                    {
                        case "太歲燈": count_ld_lights[0]++; break;
                        case "元辰光明燈": count_ld_lights[1]++; break;
                        case "文昌功名燈": count_ld_lights[2]++; break;
                        case "五路財神燈": count_ld_lights[3]++; break;
                        case "健康延壽燈": count_ld_lights[4]++; break;
                        case "月老姻緣燈": count_ld_lights[5]++; break;
                        case "明心智慧燈": count_ld_lights[6]++; break;
                    }
                }

                // === 確認可否報名（名額檢查） ===
                for (int i = 0; i < JLightsString_Tag.Count; i++)
                {
                    string lightsType = GetLightsType(JLightsString_Tag[i].ToString(), "32");
                    int c = 0;

                    c += lightsType == "4" ? count_ld_lights[0] : 0;
                    c += lightsType == "3" ? count_ld_lights[1] : 0;
                    c += lightsType == "5" ? count_ld_lights[2] : 0;
                    c += lightsType == "6" ? count_ld_lights[3] : 0;
                    c += lightsType == "8" ? count_ld_lights[4] : 0;
                    c += lightsType == "20" ? count_ld_lights[5] : 0;
                    c += lightsType == "33" ? count_ld_lights[6] : 0;

                    if (objLightDAC.CheckedLightsNum(lightsType, AdminID.ToString(), c, Year, basePage))
                    {
                        checkednum_ld = false;
                        basePage.mJSonHelper.AddContent("overnumType", GetLightsType(JLightsString_Tag[i].ToString(), "32"));

                        // 若有 ad 參數允許強制通過
                        //if (basePage.Request["ad"] == "0610")
                        //{
                        //    checkednum_ld = true;
                        //}

                        break;
                    }
                }

                if (checkednum_ld)
                {
                    // 判斷贈品處理方式
                    if (AppSendback == "N")
                    {
                        AppCounty = Appdist = AppAddr = string.Empty;
                        AppZipCode = "0";
                        ReceiptName = AppName;
                        ReceiptMobile = AppMobile;
                    }

                    // === 建立購買人資料 ===
                    ApplicantID = objLightDAC.Addapplicantinfo_lights_ld(
                        Name: AppName,
                        Mobile: AppMobile,
                        Cost: "0",
                        County: AppCounty,
                        Dist: Appdist,
                        Addr: AppAddr,
                        ZipCode: AppZipCode,
                        Sendback: AppSendback,
                        ShippingFee: AppSendback == "Y" ? "60" : "0",
                        ReceiptName: ReceiptName,
                        ReceiptMobile: ReceiptMobile,
                        Email: AppEmail,
                        Status: 0,
                        AdminID: AdminID,
                        PostURL: postURL,
                        Year: Year);
                    bool lightsinfo = false;

                    // === 新增祈福人資料 ===
                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            // 設定時區為台北標準時間
                            DateTime dtNow = LightDAC.GetTaipeiNow();

                            string name = Jname[i].ToString();
                            string mobile = Jmobile[i].ToString();
                            string sex = Jsex[i].ToString();
                            string Birth = Jbirth[i].ToString();
                            string leapMonth = JleapMonth[i].ToString();
                            string birthTime = Jbirthtime[i].ToString();
                            string sBirth = Jsbirth[i].ToString();
                            string lightsString = JLightsString_Tag[i].ToString(); ;
                            string lightsType = GetLightsType(lightsString, "32");
                            string addr = Jaddr[i].ToString();
                            string county = Jcounty[i].ToString();
                            string dist = Jdist[i].ToString();
                            string zipCode = JzipCode[i].ToString();
                            string oversea = Joversea[i].ToString();
                            string remark = Jremark[i].ToString();
                            string birthMonth = "0";
                            string age = "0";
                            string Zodiac = string.Empty;
                            string year = string.Empty;
                            string month = string.Empty;
                            string day = string.Empty;
                            string syear = string.Empty;
                            string smonth = string.Empty;
                            string sday = string.Empty;

                            if (Birth != "")
                            {
                                //農曆生日!=空白
                                GetBirthDetail(Birth, ref birthMonth, ref age, ref Zodiac);

                                string birth = Birth;
                                if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                                {
                                    int birth_roc_index = birth.IndexOf("民國");
                                    int birth_year_index = birth.IndexOf("年");
                                    int birth_month_index = birth.IndexOf("月");
                                    int birth_day_index = birth.IndexOf("日");
                                    year = (int.Parse(birth.Substring(2, birth_year_index - 2)) + 1911).ToString();
                                    month = birthMonth = CheckedDateZero(birth.Substring(birth_year_index + 1, birth_month_index - birth_year_index - 1), 1);
                                    day = CheckedDateZero(birth.Substring(birth_month_index + 1, birth.Length - birth_month_index - 2), 1);

                                    Lunar lunar = new Lunar();
                                    int.TryParse(year, out lunar.lunarYear);
                                    int.TryParse(month, out lunar.lunarMonth);
                                    int.TryParse(day, out lunar.lunarDay);

                                    if (sBirth == "")
                                    {
                                        //國曆生日=空白
                                        Solar solor = new Solar();
                                        solor = LunarSolarConverter.LunarToSolar(lunar);

                                        string sROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                                        sBirth = sROC + CheckedDateZero(solor.solarMonth.ToString(), 1) + "月" + CheckedDateZero(solor.solarDay.ToString(), 1) + "日";

                                        string ROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                        Birth = ROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";
                                    }
                                    else
                                    {
                                        //國曆生日!=空白
                                        string sbirth = sBirth;
                                        if (sbirth.IndexOf("民國") >= 0 && sbirth.IndexOf("年") > 0 && sbirth.IndexOf("月") > 0 && sbirth.IndexOf("日") > 0)
                                        {
                                            int sbirth_roc_index = sbirth.IndexOf("民國");
                                            int sbirth_year_index = sbirth.IndexOf("年");
                                            int sbirth_month_index = sbirth.IndexOf("月");
                                            int sbirth_day_index = sbirth.IndexOf("日");
                                            syear = (int.Parse(sbirth.Substring(2, sbirth_year_index - 2))).ToString();
                                            smonth = CheckedDateZero(sbirth.Substring(sbirth_year_index + 1, sbirth_month_index - sbirth_year_index - 1), 1);
                                            sday = CheckedDateZero(sbirth.Substring(sbirth_month_index + 1, sbirth.Length - sbirth_month_index - 2), 1);

                                            sBirth = "民國" + syear + "年" + smonth + "月" + sday + "日";

                                            string ROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                            Birth = ROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //農曆生日=空白
                                string sbirth = sBirth;
                                if (sbirth.IndexOf("民國") >= 0 && sbirth.IndexOf("年") > 0 && sbirth.IndexOf("月") > 0 && sbirth.IndexOf("日") > 0)
                                {
                                    int sbirth_roc_index = sbirth.IndexOf("民國");
                                    int sbirth_year_index = sbirth.IndexOf("年");
                                    int sbirth_month_index = sbirth.IndexOf("月");
                                    int sbirth_day_index = sbirth.IndexOf("日");
                                    syear = (int.Parse(sbirth.Substring(2, sbirth_year_index - 2)) + 1911).ToString();
                                    smonth = CheckedDateZero(sbirth.Substring(sbirth_year_index + 1, sbirth_month_index - sbirth_year_index - 1), 1);
                                    sday = CheckedDateZero(sbirth.Substring(sbirth_month_index + 1, sbirth.Length - sbirth_month_index - 2), 1);

                                    Solar solor = new Solar();
                                    int.TryParse(syear, out solor.solarYear);
                                    int.TryParse(smonth, out solor.solarMonth);
                                    int.TryParse(sday, out solor.solarDay);

                                    Lunar lunar = new Lunar();
                                    lunar = LunarSolarConverter.SolarToLunar(solor);

                                    LunarSolarConverter.shuxiang(lunar.lunarYear, ref Zodiac);
                                    age = GetAge(lunar.lunarYear, lunar.lunarMonth, lunar.lunarDay).ToString();
                                    birthMonth = CheckedDateZero(lunar.lunarMonth.ToString(), 1);

                                    string ROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                    Birth = ROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";

                                    string sROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                                    sBirth = sROC + smonth + "月" + sday + "日";
                                }
                            }

                            birthMonth = CheckedDateZero(birthMonth, 1);

                            int cost = GetLightsCost(32, lightsType);

                            if (name != "")
                            {
                                lightsinfo = true;
                                LightsID = objLightDAC.AddLights_ld(
                                    ApplicantID: ApplicantID,
                                    Name: name,
                                    Mobile: mobile,
                                    Cost: cost,
                                    Sex: sex,
                                    LightsType: lightsType,
                                    LightsString: lightsString,
                                    Oversea: oversea,
                                    Birth: Birth,
                                    LeapMonth: leapMonth,
                                    BirthTime: birthTime,
                                    BirthMonth: birthMonth,
                                    Age: age,
                                    Zodiac: Zodiac,
                                    sBirth: sBirth,
                                    Email: "",
                                    Count: 1,
                                    Remark: remark,
                                    Addr: addr,
                                    County: county,
                                    Dist: dist,
                                    ZipCode: zipCode,
                                    Year: Year);
                            }
                        }
                    }

                    // === 成功建立資料，回傳前端 ===
                    if (ApplicantID > 0 && lightsinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);

                        string redirectUrl = BuildRedirectUrl(
                            "templeCheck.aspx",
                            1,
                            AdminID,
                            ApplicantID,
                            basePage.Request
                        );

                        // 加入 JSON 回傳內容
                        basePage.mJSonHelper.AddContent("redirect", redirectUrl);

                        basePage.Session["ApplicantID"] = ApplicantID;
                    }
                }
            }

            /// <summary>
            /// 修改資料（取得既有報名資料）
            /// </summary>
            /// <param name="basePage">基礎頁面物件</param>
            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getlights_ld_Info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
                    basePage.mJSonHelper.AddContent("AppSendback", dtData.Rows[0]["AppSendback"].ToString());
                    basePage.mJSonHelper.AddContent("ReceiptName", dtData.Rows[0]["ReceiptName"].ToString());
                    basePage.mJSonHelper.AddContent("ReceiptMobile", dtData.Rows[0]["ReceiptMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppCounty", dtData.Rows[0]["AppCounty"].ToString());
                    basePage.mJSonHelper.AddContent("Appdist", dtData.Rows[0]["Appdist"].ToString());
                    basePage.mJSonHelper.AddContent("AppAddr", dtData.Rows[0]["AppAddr"].ToString());

                    basePage.mJSonHelper.AddDataTable("DataSource", dtData);
                }
            }

            /// <summary>
            /// 檢查並安全轉換 JArray
            /// </summary>
            /// <param name="str">輸入的 JSON 字串</param>
            /// <param name="jArry">輸出的 JArray 參照</param>
            public void nullChecked(string str, ref JArray jArry)
            {
                if (str != null)
                {
                    jArry = JArray.Parse(str);
                }
            }
        }
    }
}