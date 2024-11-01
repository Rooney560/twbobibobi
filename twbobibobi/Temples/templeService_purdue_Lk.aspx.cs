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
    public partial class templeService_purdue_Lk : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/07/31 23:59";

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

                int adminID = a = 21;
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int PurdueID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                LightDAC objLightDAC = new LightDAC(basePage);
                string Year = dtNow.Year.ToString();
                string AdminID = "21";
                string AppName = basePage.Request["Appname"];               //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];           //申請人電話

                string name_Tag = basePage.Request["name_Tag"];             //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];         //祈福人電話
                string sex_Tag = basePage.Request["sex_Tag"];               //祈福人性別
                string birth_Tag = basePage.Request["birth_Tag"];           //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];   //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];   //祈福人農曆時辰
                string homenum_Tag = basePage.Request["homenum_Tag"];       //祈福人市話
                string zipCode_Tag = basePage.Request["zipCode_Tag"];       //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];         //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];             //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];             //祈福人部分地址
                string purduetype_Tag = basePage.Request["purduetype_Tag"]; //普度項目

                int listcount = int.Parse(basePage.Request["listcount"]);   //祈福人數量

                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsex = JArray.Parse(sex_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jpurduetype = JArray.Parse(purduetype_Tag);

                JArray Jhomenum = new JArray();
                nullChecked(homenum_Tag, ref Jhomenum);

                string postURL = "Purdue_Lk_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                ApplicantID = objLightDAC.addapplicantinfo_purdue_Lk(AppName, AppMobile, "0", "", "", "", "0", "N", "", "", 0, AdminID, postURL, Year);
                bool purdueinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();
                        string mobile = Jmobile[i].ToString();
                        string birth = Jbirth[i].ToString();
                        string leapMonth = JleapMonth[i].ToString();
                        string birthtime = Jbirthtime[i].ToString();
                        string sex = Jsex[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string addr = Jaddr[i].ToString();

                        string homenum = Jhomenum.Count > 0 ? Jhomenum[i].ToString() : "";

                        string purdueType = "1";
                        string purdueString = "贊普";
                        string birthMonth = string.Empty;
                        string age = string.Empty;
                        string Zodiac = string.Empty;

                        //string year = string.Empty;
                        //string month = string.Empty;
                        //string day = string.Empty;

                        //string birth = Jbirth[i].ToString();
                        //int s1 = birth.IndexOf("民國");
                        //int s2 = birth.IndexOf("年");
                        //int s3 = birth.IndexOf("月");
                        //int s4 = birth.IndexOf("日");
                        //if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                        //{
                        //    int year_index = birth.IndexOf("年");
                        //    int month_index = birth.IndexOf("月");
                        //    year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                        //    month = birthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                        //    day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                        //    Birth = year + "-" + month + "-" + day;
                        //    LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                        //    age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                        //}

                        //birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;

                        GetBirthDetail(birth, ref birthMonth, ref age, ref Zodiac);

                        if (name != "")
                        {
                            purdueinfo = true;
                            PurdueID = objLightDAC.addpurdue_Lk(ApplicantID, name, mobile, sex, purdueType, purdueString, "1", birth, leapMonth, birthtime, birthMonth, age,
                                Zodiac, homenum, 1, addr, county, dist, zipCode, Year);
                        }
                    }
                }

                if (ApplicantID > 0 && purdueinfo)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=2&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));

                    basePage.Session["ApplicantID"] = ApplicantID;
                }
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                string Year = dtNow.Year.ToString();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getpurdue_Lk_info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());

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