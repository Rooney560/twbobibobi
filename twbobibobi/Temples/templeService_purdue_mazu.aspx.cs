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


namespace Temple.Temples
{
    public partial class templeService_purdue_mazu : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/08/15 23:59";

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

                int adminID = a = 30;
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
                string AdminID = "30";
                string AppName = basePage.Request["Appname"];               //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];           //申請人電話
                string Appcounty = basePage.Request["Appcounty"];           //申請人縣市
                string Appdist = basePage.Request["Appdist"];               //申請人區域
                string Appaddr = basePage.Request["Appaddr"];               //申請人地址(部分)
                string AppzipCode = basePage.Request["AppzipCode"];         //申請人郵遞區號

                string name_Tag = basePage.Request["name_Tag"];             //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];         //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];           //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];   //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];   //祈福人農曆時辰
                string purduetype_Tag = basePage.Request["purduetype_Tag"]; //普度項目

                int listcount = int.Parse(basePage.Request["listcount"]);   //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray Jpurduetype = JArray.Parse(purduetype_Tag);

                string postURL = "Purdue_mazu_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                //ApplicantID = objLightDAC.addapplicantinfo_purdue_mazu(AppName, AppMobile, "0", Appcounty, Appdist, Appaddr, AppzipCode, "Y", AppName, AppMobile, 0, AdminID, postURL, Year);
                //bool purdueinfo = false;

                //if (ApplicantID > 0)
                //{
                //    for (int i = 0; i < listcount; i++)
                //    {
                //        string name = Jname[i].ToString();
                //        string mobile = Jmobile[i].ToString();
                //        string leapMonth = JleapMonth[i].ToString();
                //        string birthTime = Jbirthtime[i].ToString();
                //        string purdueType = Jpurduetype[i].ToString();

                //        string purdueString = PurdueType2String(Jpurduetype[i].ToString(), "30");

                //        string Birth = string.Empty;
                //        string birthMonth = string.Empty;
                //        string age = string.Empty;
                //        string Zodiac = string.Empty;

                //        string year = string.Empty;
                //        string month = string.Empty;
                //        string day = string.Empty;

                //        string birth = Jbirth[i].ToString();
                //        int s1 = birth.IndexOf("民國");
                //        int s2 = birth.IndexOf("年");
                //        int s3 = birth.IndexOf("月");
                //        int s4 = birth.IndexOf("日");
                //        if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                //        {
                //            int year_index = birth.IndexOf("年");
                //            int month_index = birth.IndexOf("月");
                //            year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                //            month = birthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                //            day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                //            Birth = year + "-" + month + "-" + day;
                //            LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                //            age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                //        }

                //        birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;

                //        if (name != "")
                //        {
                //            purdueinfo = true;
                //            PurdueID = objLightDAC.addpurdue_mazu(ApplicantID, name, mobile, "善男", purdueType, purdueString, "1", birth , leapMonth, birthTime, birthMonth, age, 
                //                Zodiac, 1, "", "", "", "0", Year);

                //        }
                //    }
                //}

                //if (ApplicantID > 0 && purdueinfo)
                //{
                //    basePage.mJSonHelper.AddContent("StatusCode", 1);
                //    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=2&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));

                //    basePage.Session["ApplicantID"] = ApplicantID;
                //}
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

                //dtData = objLightDAC.Getpurdue_mazu_info(applicantID, Year);

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