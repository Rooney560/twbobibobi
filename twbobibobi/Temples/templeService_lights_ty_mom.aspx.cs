using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Encoders;
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
using twbobibobi.Data;

namespace Temple.Temples
{
    /// <summary>
    /// 桃園威天宮 孝親祈福燈 服務頁面
    /// </summary>
    /// <remarks>
    /// 此頁面繼承自 AjaxBasePage，負責：
    /// 1. 控制顯示狀態
    /// 2. 初始化 Ajax 處理器（建立/修改報名資料）
    /// 3. 處理購買人與祈福人資料寫入流程
    /// </remarks>
    public partial class templeService_lights_ty_mom : AjaxBasePage
    {
        /// <summary> 購買人編號 ApplicantID </summary>
        public int aid = 0;
        /// <summary> 宮廟代碼對應 AdminID </summary>
        public int a = 0;
        /// <summary> 當前年份（動態設定年度資料庫名稱用） </summary>
        protected static string Year = "2025";

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

                // 固定桃園威天宮 AdminID = 14
                int adminID = a = 14;

                // 預設兩種燈種已額滿字串都隱藏
                this.light1.Visible = false;

                LightDAC objLightDAC = new LightDAC(this);
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
                string AdminID = "14";

                // === 購買人資料 ===
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["Appemail"];                                     //購買人信箱
                string appsBirth = basePage.Request["Appsbirth"];                                   //購買人國曆生日
                string AppzipCode = basePage.Request["AppzipCode"];                                 //購買人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                                   //購買人縣市
                string Appdist = basePage.Request["Appdist"];                                       //購買人區域
                string Appaddr = basePage.Request["Appaddr"];                                       //購買人部分地址

                // === 祈福人相關資料（多筆） ===
                string name_Tag = basePage.Request["name_Tag"];                                     //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                                 //祈福人電話
                string sbirth_Tag = basePage.Request["sbirth_Tag"];                                 //祈福人國曆生日
                string oversea_Tag = basePage.Request["oversea_Tag"];                               //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string remark_Tag = basePage.Request["remark_Tag"];                                 //備註

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量

                // === 轉換為 JArray 以便後續處理 ===
                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsbirth = JArray.Parse(sbirth_Tag);
                JArray Joversea = JArray.Parse(oversea_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jremark = JArray.Parse(remark_Tag);

                string postURL_Init = "Lights_ty_mom_Index";
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                string postURL = GetRequestURL(url, postURL_Init);

                int[] count_ty_mom_lights = new int[1];
                bool checkednum_ty = true;
                //for (int i = 0; i < listcount; i++)
                //{
                //    //孝親祈福燈
                //    count_ty_mom_lights[0]++;
                //}

                //string[] Lightstypelist = new string[] { "21"};
                //for (int i = 0; i < 6; i++)
                //{
                //    if (objLightDAC.checkedLightsNum(Lightstypelist[i], AdminID.ToString(), count_ty_mom_lights[i], 2, Year))
                //    {
                //        checkednum_ty = false;

                //        basePage.mJSonHelper.AddContent("overnumType", Lightstypelist[i]);

                //        break;
                //    }
                //}

                if (checkednum_ty)
                {
                    string AppBirth = string.Empty;
                    string AppsBirth = string.Empty;
                    string AppbirthMonth = "0";
                    string Appage = "0";
                    string AppZodiac = string.Empty;

                    string Appyear = string.Empty;
                    string Appmonth = string.Empty;
                    string Appday = string.Empty;

                    //農曆生日=空白
                    string appsbirth = appsBirth;
                    if (appsbirth.IndexOf("民國") >= 0 && appsbirth.IndexOf("年") > 0 && appsbirth.IndexOf("月") > 0 && appsbirth.IndexOf("日") > 0)
                    {
                        int appsbirth_roc_index = appsbirth.IndexOf("民國");
                        int appsbirth_year_index = appsbirth.IndexOf("年");
                        int appsbirth_month_index = appsbirth.IndexOf("月");
                        int appsbirth_day_index = appsbirth.IndexOf("日");
                        Appyear = (int.Parse(appsbirth.Substring(2, appsbirth_year_index - 2)) + 1911).ToString();
                        Appmonth = CheckedDateZero(appsbirth.Substring(appsbirth_year_index + 1, appsbirth_month_index - appsbirth_year_index - 1), 1);
                        Appday = CheckedDateZero(appsbirth.Substring(appsbirth_month_index + 1, appsbirth.Length - appsbirth_month_index - 2), 1);

                        Solar solor = new Solar();
                        int.TryParse(Appyear, out solor.solarYear);
                        int.TryParse(Appmonth, out solor.solarMonth);
                        int.TryParse(Appday, out solor.solarDay);

                        Lunar lunar = new Lunar();
                        lunar = LunarSolarConverter.SolarToLunar(solor);

                        LunarSolarConverter.shuxiang(lunar.lunarYear, ref AppZodiac);
                        Appage = GetAge(lunar.lunarYear, lunar.lunarMonth, lunar.lunarDay).ToString();
                        AppbirthMonth = CheckedDateZero(lunar.lunarMonth.ToString(), 1);

                        string ROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                        AppBirth = ROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";

                        string sROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                        AppsBirth = sROC + Appmonth + "月" + Appday + "日";
                    }

                    string AppSendback = "N";                                                           // 寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                    string Apprname = AppName;                                                          // 收件人姓名
                    string Apprmobile = AppMobile;                                                      // 收件人電話

                    // === 建立購買人資料 ===
                    ApplicantID = objLightDAC.Addapplicantinfo_lights_ty_mom(
                        Name: AppName,
                        Mobile: AppMobile,
                        Birth: AppBirth,
                        LeapMonth: "N",
                        BirthTime: "吉",
                        BirthMonth: AppbirthMonth,
                        Age: Appage,
                        Zodiac: AppZodiac,
                        sBirth: AppsBirth,
                        Cost: "0",
                        Email: AppEmail,
                        ZipCode: AppzipCode,
                        County: Appcounty,
                        Dist: Appdist,
                        Addr: Appaddr,
                        Sendback: AppSendback,
                        ReceiptName: Apprname,
                        ReceiptMobile: Apprmobile,
                        Status: 0,
                        AdminID: AdminID,
                        PostURL: postURL,
                        Year: Year);
                    bool lightsinfo = false;

                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            string name = Jname[i].ToString();
                            string mobile = Jmobile[i].ToString();
                            string sex = "善男";
                            string Birth = string.Empty;
                            string leapMonth = "N";
                            string birthTime = "吉";
                            string sBirth = Jsbirth[i].ToString();
                            string lightsString = "孝親祈福燈";
                            string lightsType = "21";
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

                            int cost = GetLightsCost(14, lightsType);

                            if (name != "")
                            {
                                lightsinfo = true;
                                LightsID = objLightDAC.AddLights_ty_mom(
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
                            22,
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

                dtData = objLightDAC.Getlights_ty_mom_Info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppsBirth", dtData.Rows[0]["AppsBirth"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
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