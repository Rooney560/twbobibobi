using twbobibobi.Data;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.FET.Lights
{
    public partial class LightsIndex_Luer : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2023/11/21 23:59";
        public static string Add_year = "2024";

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

                int adminID = a = 10;

                Add_year = dtNow.Year.ToString();

                //if (dtNow >= DateTime.Parse(EndDate))
                //{
                //    Response.Write("<script>alert('親愛的大德您好\\n 北港武德宮 2023下元補庫活動已截止！！\\n感謝您的支持, 謝謝!');</script>");
                //}
            }
        }

        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int lightsID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "10";
                string AppName = basePage.Request["Appname"];                   //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];               //申請人電話

                string name_Tag = basePage.Request["name_Tag"];                 //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];             //祈福人電話
                string sex_Tag = basePage.Request["sex_Tag"];                   //祈福人性別
                string birth_Tag = basePage.Request["birth_Tag"];               //祈福人農歷生日
                string birthTime_Tag = basePage.Request["birthTime_Tag"];       //祈福人農曆時辰
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];       //閏月 Y-是 N-否
                string zipCode_Tag = basePage.Request["zipCode_Tag"];           //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];             //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                 //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                 //祈福人部分地址
                string lightstype_Tag = basePage.Request["lightstype_Tag"];     //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);       //祈福人數量

                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsex = JArray.Parse(sex_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JbirthTime = JArray.Parse(birthTime_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jlightstype = JArray.Parse(lightstype_Tag);

                string postURL = "Lights_Luer_Index_FETAPI";

                //ApplicantID = objLightDAC.addapplicantinfo_lights_Luer(AppName, AppMobile, "0", "", "", "", "0", "N", "", "", "", 0, AdminID, postURL, Add_year);
                //bool lightsinfo = false;

                //if (ApplicantID > 0)
                //{
                //    string name = Jname[0].ToString();

                //    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                //    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                //    string lightsString = "財利燈";
                //    string Birth = string.Empty;
                //    string birthMonth = string.Empty;
                //    string age = string.Empty;
                //    string Zodiac = string.Empty;

                //    string year = string.Empty;
                //    string month = string.Empty;
                //    string day = string.Empty;

                //    string birth = Jbirth[0].ToString();
                //    int s1 = birth.IndexOf("民國");
                //    int s2 = birth.IndexOf("年");
                //    int s3 = birth.IndexOf("月");
                //    int s4 = birth.IndexOf("日");
                //    if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                //    {
                //        int year_index = birth.IndexOf("年");
                //        int month_index = birth.IndexOf("月");
                //        year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                //        month = birthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                //        day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                //        Birth = year + "-" + month + "-" + day;
                //        LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                //        //age = GetAge(DateTime.Parse(Birth), dtNow);
                //        age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                //    }

                //    if (name != "")
                //    {
                //        lightsinfo = true;
                //        lightsID = objLightDAC.addLights_Luer(ApplicantID, name, Jmobile[0].ToString(), Jsex[0].ToString(), Jlightstype[0].ToString(), lightsString, "1", 
                //            Jbirth[0].ToString(), JbirthTime[0].ToString(), birthMonth, JleapMonth[0].ToString(), age, Zodiac, "", "", 1, "", Jaddr[0].ToString(), 
                //            Jcounty[0].ToString(), Jdist[0].ToString(), JzipCode[0].ToString(), "", "", "無", Add_year);
                //    }
                //}

                //if (ApplicantID > 0 && lightsinfo)
                //{
                //    basePage.mJSonHelper.AddContent("StatusCode", 1);
                //    basePage.mJSonHelper.AddContent("redirect", "LightsCheck.aspx?kind=1&a=" + AdminID + "&aid=" + ApplicantID);

                //    basePage.Session["ApplicantID"] = ApplicantID;
                //}
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getlights_Luer_Info(applicantID, Add_year);

                if (dtData.Rows.Count > 0)
                {
                    string year = string.Empty;
                    string month = string.Empty;
                    string day = string.Empty;

                    string birth = dtData.Rows[0]["Birth"].ToString();
                    int s1 = birth.IndexOf("民國");
                    int s2 = birth.IndexOf("年");
                    int s3 = birth.IndexOf("月");
                    int s4 = birth.IndexOf("日");
                    if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                    {
                        int year_index = birth.IndexOf("年");
                        int month_index = birth.IndexOf("月");
                        year = (int.Parse(birth.Substring(2, year_index - 2))).ToString();
                        month = birth.Substring(year_index + 1, month_index - year_index - 1);
                        day = birth.Substring(month_index + 1, birth.Length - month_index - 2);
                    }


                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("year", year);
                    basePage.mJSonHelper.AddContent("month", month);
                    basePage.mJSonHelper.AddContent("day", day);

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