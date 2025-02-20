using MotoSystem.Data;
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

namespace Temple.Temples
{
    public partial class templeService_supplies_ty : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2025/03/06 23:59";
        protected static string Year = "2025";

        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "gotochecked");
            AddAjaxHandler(typeof(AjaxPageHandler), "editinfo");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                if (Request["aid"] != null)
                {
                    aid = int.Parse(Request["aid"]);
                }

                int adminID = a = 14;
                this.supplies1.Visible = false;

                LightDAC objLightDAC = new LightDAC(this);

                //if (objLightDAC.checkedSuppliesNum("3", adminID.ToString(), 1, Year))
                //{
                //    this.supplies1.Visible = true;
                //}
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int SuppliesID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "14";
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["Appemail"];                                     //購買人Email
                string appsBirth = basePage.Request["Appsbirth"];                                   //購買人國曆生日
                string AppzipCode = basePage.Request["AppzipCode"];                                 //購買人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                                   //購買人縣市
                string Appdist = basePage.Request["Appdist"];                                       //購買人區域
                string Appaddr = basePage.Request["Appaddr"];                                       //購買人部分地址

                string name_Tag = basePage.Request["name_Tag"];                                     //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                                 //祈福人電話
                string sbirth_Tag = basePage.Request["sbirth_Tag"];                                 //祈福人國曆生日
                string oversea_Tag = basePage.Request["oversea_Tag"];                               //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string remark_Tag = basePage.Request["remark_Tag"];                                 //備註
                string SuppliesType_Tag = basePage.Request["SuppliesType_Tag"];                     //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsbirth = JArray.Parse(sbirth_Tag);
                JArray Joversea = JArray.Parse(oversea_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jremark = JArray.Parse(remark_Tag);
                JArray JSuppliesType_Tag = JArray.Parse(SuppliesType_Tag);

                string postURL = "Supplies_ty_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["cht"] != null ? "_CHT" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                postURL += basePage.Request["fbty"] != null ? "_FBTY" : "";

                postURL += basePage.Request["ig"] != null ? "_IG" : "";

                postURL += basePage.Request["fetsms"] != null ? "_fetSMS" : "";

                postURL += basePage.Request["jkos"] != null ? "_JKOS" : "";

                postURL += basePage.Request["gads"] != null ? "_GADS" : "";

                postURL += basePage.Request["inty"] != null ? "_INTY" : "";

                postURL += basePage.Request["elv"] != null ? "_ELV" : "";

                //int[] count_ty_supplies = new int[1];
                bool checkednum_ty = true;
                //for (int i = 0; i < listcount; i++)
                //{
                //    //孝親祈福燈
                //    count_ty_mom_supplies[0]++;
                //}

                //string[] Suppliestypelist = new string[] { "21"};
                //for (int i = 0; i < 6; i++)
                //{
                //    if (objLightDAC.checkedSuppliesNum(Suppliestypelist[i], AdminID.ToString(), count_ty_mom_supplies[i], 2, Year))
                //    {
                //        checkednum_ty = false;

                //        basePage.mJSonHelper.AddContent("overnumType", Suppliestypelist[i]);

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

                    ApplicantID = objLightDAC.addapplicantinfo_Supplies_ty(AppName, AppMobile, AppBirth, "N", "吉", AppbirthMonth, Appage, AppZodiac, AppsBirth, "0", AppEmail, 
                        AppzipCode, Appcounty, Appdist, Appaddr, "Y", AppName, AppMobile, 0, AdminID, postURL, Year);
                    bool suppliesinfo = false;

                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            string name = Jname[i].ToString();
                            string mobile = Jmobile[i].ToString();
                            string Birth = string.Empty;
                            string leapMonth = "N";
                            string birthTime = "吉";
                            string sBirth = Jsbirth[i].ToString();
                            string suppliesString = GetSuppliesString(JSuppliesType_Tag[i].ToString());
                            string suppliesType = JSuppliesType_Tag[i].ToString();
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

                            if (name != "")
                            {
                                suppliesinfo = true;
                                SuppliesID = objLightDAC.addSupplies_ty(ApplicantID, name, mobile, "善男", suppliesType, suppliesString, oversea, Birth, leapMonth, birthTime,
                                    birthMonth, age, Zodiac, sBirth, "", 1, remark, addr, county, dist, zipCode, Year);
                            }
                        }
                    }

                    if (ApplicantID > 0 && suppliesinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=7&a=" + AdminID + "&aid=" + ApplicantID +
                        (basePage.Request["ad"] != null ? "&ad=" + basePage.Request["ad"] : "") +
                        (basePage.Request["jkos"] != null ? "&jkos=1" : "") +
                        (basePage.Request["twm"] != null ? "&twm=1" : ""));

                        basePage.Session["ApplicantID"] = ApplicantID;
                    }
                }
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getsupplies_ty_info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppsBirth", dtData.Rows[0]["AppsBirth"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
                    //basePage.mJSonHelper.AddContent("AppLeapMonth", dtData.Rows[0]["AppLeapMonth"].ToString());
                    //basePage.mJSonHelper.AddContent("AppBirthTime", dtData.Rows[0]["AppBirthTime"].ToString());
                    basePage.mJSonHelper.AddContent("AppCounty", dtData.Rows[0]["AppCounty"].ToString());
                    basePage.mJSonHelper.AddContent("Appdist", dtData.Rows[0]["Appdist"].ToString());
                    basePage.mJSonHelper.AddContent("AppAddr", dtData.Rows[0]["AppAddr"].ToString());

                    basePage.mJSonHelper.AddDataTable("DataSource", dtData);
                }
            }

            public void nullChecked(string str, ref JArray jArry)
            {
                if (str != null)
                {
                    jArry = JArray.Parse(str);
                }
            }

            //補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 6-天貺納福添運法會 
            public string GetSuppliesType(string SuppliesString)
            {
                string result = "-1";
                switch (SuppliesString)
                {
                    case "下元補庫":
                        result = "1";
                        break;
                    case "呈疏補庫":
                        result = "2";
                        break;
                    case "企業補財庫":
                        result = "3";
                        break;
                    case "天赦日補運":
                        result = "4";
                        break;
                    case "天赦日招財補運":
                        result = "4";
                        break;
                }

                return result;
            }
            private string GetSuppliesString(string v)
            {
                switch (v)
                {
                    case "8":
                        return "加強運勢升級版";
                    default:
                        return "天赦日招財補運";
                }
            }
        }
    }
}