using MotoSystem.Data;
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

namespace Temple.Temples
{
    public partial class templeService_EmperorGuansheng_ty : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/07/26 23:59";
        protected static string Year = "2024";

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
                this.emperorGuansheng1.Visible = true;
                this.emperorGuansheng2.Visible = false;
                this.emperorGuansheng3.Visible = false;

                this.luck1.Visible = false;
                this.luck2.Visible = false;


                if (Request["twm"] != null)
                {
                    Response.Write("<script>alert('訪問網址錯誤。');window.location.href='https://bobibobi.tw/Temples/templeService_EmperorGuansheng_ty.aspx'</script>");
                }
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int EmperorGuanshengID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "14";
                string AppName = basePage.Request["Appname"];                                       //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //申請人電話
                string Appbirth = basePage.Request["Appbirth"];                                     //申請人國曆生日
                string Appemail = basePage.Request["Appemail"];                                     //申請人Email
                string AppzipCode = basePage.Request["AppzipCode"];                                 //申請人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                                   //申請人縣市
                string Appdist = basePage.Request["Appdist"];                                       //申請人區域
                string Appaddr = basePage.Request["Appaddr"];                                       //申請人部分地址

                string name_Tag = basePage.Request["name_Tag"];                                     //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                                 //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];                                   //祈福人國曆生日
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string EmperorGuanshengString_Tag = basePage.Request["EmperorGuanshengString_Tag"]; //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray JEmperorGuanshengString_Tag = JArray.Parse(EmperorGuanshengString_Tag);

                string postURL = "EmperorGuansheng_ty_Index_FET";

                bool checkednum_ty = true;

                if (checkednum_ty)
                {
                    string AppBirth = string.Empty;
                    string AppbirthMonth = "0";
                    string Appage = "0";
                    string AppZodiac = string.Empty;

                    string Appyear = string.Empty;
                    string Appmonth = string.Empty;
                    string Appday = string.Empty;

                    string appbirth = Appbirth;
                    int Apps1 = appbirth.IndexOf("民國");
                    int Apps2 = appbirth.IndexOf("年");
                    int Apps3 = appbirth.IndexOf("月");
                    int Apps4 = appbirth.IndexOf("日");
                    if (appbirth.IndexOf("民國") >= 0 && appbirth.IndexOf("年") > 0 && appbirth.IndexOf("月") > 0 && appbirth.IndexOf("日") > 0)
                    {
                        int year_index = appbirth.IndexOf("年");
                        int month_index = appbirth.IndexOf("月");
                        Appyear = (int.Parse(appbirth.Substring(2, year_index - 2)) + 1911).ToString();
                        Appmonth = AppbirthMonth = appbirth.Substring(year_index + 1, month_index - year_index - 1);
                        Appday = appbirth.Substring(month_index + 1, appbirth.Length - month_index - 2);

                        Solar solor = new Solar();
                        int.TryParse(Appyear, out solor.solarYear);
                        int.TryParse(Appmonth, out solor.solarMonth);
                        int.TryParse(Appday, out solor.solarDay);

                        Lunar lunar = new Lunar();
                        lunar = LunarSolarConverter.SolarToLunar(solor);

                        AppBirth = lunar.lunarYear + "-" + lunar.lunarMonth + "-" + lunar.lunarDay;
                        LunarSolarConverter.shuxiang(lunar.lunarYear, ref AppZodiac);

                        Appage = GetAge(int.Parse(Appyear), int.Parse(Appmonth), int.Parse(Appday)).ToString();
                    }

                    AppbirthMonth = AppbirthMonth.Length < 2 ? "0" + AppbirthMonth : AppbirthMonth;

                    ApplicantID = objLightDAC.addapplicantinfo_EmperorGuansheng_ty(AppName, AppMobile, Appbirth, "N", "吉", AppbirthMonth, Appage, AppZodiac, Appemail, "0", AppzipCode, Appcounty, Appdist, Appaddr, "Y", AppName, AppMobile, 0, AdminID, postURL, Year);
                    bool marriageemperorGuanshenginfo = false;

                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            string name = Jname[i].ToString();

                            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                            string emperorGuanshengString = JEmperorGuanshengString_Tag[i].ToString(); ;
                            string emperorGuanshengType = GetEmperorGuanshengType(emperorGuanshengString, "14"); ;
                            string Birth = string.Empty;
                            string birthMonth = "0";
                            string age = "0";
                            string Zodiac = string.Empty;

                            string year = string.Empty;
                            string month = string.Empty;
                            string day = string.Empty;

                            string birth = Jbirth[i].ToString();
                            int s1 = birth.IndexOf("民國");
                            int s2 = birth.IndexOf("年");
                            int s3 = birth.IndexOf("月");
                            int s4 = birth.IndexOf("日");
                            if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                            {
                                int year_index = birth.IndexOf("年");
                                int month_index = birth.IndexOf("月");
                                year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                                month = birthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                                day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                                Solar solor = new Solar();
                                int.TryParse(Appyear, out solor.solarYear);
                                int.TryParse(Appmonth, out solor.solarMonth);
                                int.TryParse(Appday, out solor.solarDay);

                                Lunar lunar = new Lunar();
                                lunar = LunarSolarConverter.SolarToLunar(solor);

                                Birth = lunar.lunarYear + "-" + lunar.lunarMonth + "-" + lunar.lunarDay;
                                LunarSolarConverter.shuxiang(lunar.lunarYear, ref Zodiac);
                                //age = GetAge(DateTime.Parse(Birth_ad), dtNow);
                                age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                            }

                            birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;

                            if (name != "")
                            {
                                marriageemperorGuanshenginfo = true;
                                EmperorGuanshengID = objLightDAC.addEmperorGuansheng_ty(ApplicantID, name, Jmobile[i].ToString(), "善男", emperorGuanshengType, emperorGuanshengString, 
                                    "1", birth, "N", "吉", birthMonth, age, Zodiac, 1, Jaddr[i].ToString(), Jcounty[i].ToString(), Jdist[i].ToString(), JzipCode[i].ToString(), Year);
                            }

                        }
                    }

                    if (ApplicantID > 0 && marriageemperorGuanshenginfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=9&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : ""));

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

                dtData = objLightDAC.GetemperorGuansheng_ty_info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
                    basePage.mJSonHelper.AddContent("AppBirth", dtData.Rows[0]["AppBirth"].ToString());
                    basePage.mJSonHelper.AddContent("AppLeapMonth", dtData.Rows[0]["AppLeapMonth"].ToString());
                    basePage.mJSonHelper.AddContent("AppBirthTime", dtData.Rows[0]["AppBirthTime"].ToString());
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

        }
    }
}