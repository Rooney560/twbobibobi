using MotoSystem.Data;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;


namespace Temple.Temples
{
    public partial class templeService_supplies3 : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2023/11/21 23:59";
        public static string Add_year = "2025";

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

                int adminID = a = 6;

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
            public int suppliesID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtlist = new DataTable();
                string AdminID = "6";
                string AppName = basePage.Request["Appname"];                   //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];               //購買人電話
                string Appemail = basePage.Request["Appemail"];                 //購買人電話

                string name_Tag = basePage.Request["name_Tag"];                 //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];             //祈福人電話
                string sex_Tag = basePage.Request["sex_Tag"];                   //祈福人性別
                string birth_Tag = basePage.Request["birth_Tag"];               //祈福人農歷生日
                string birthTime_Tag = basePage.Request["birthTime_Tag"];       //祈福人農曆時辰
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];       //閏月 Y-是 N-否
                string homenum_Tag = basePage.Request["homenum_Tag"];           //祈福人市話
                string email_Tag = basePage.Request["email_Tag"];               //祈福人Email
                string zipCode_Tag = basePage.Request["zipCode_Tag"];           //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];             //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                 //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                 //祈福人部分地址
                string suppliestype_Tag = basePage.Request["suppliestype_Tag"]; //服務項目
                string remark_Tag = basePage.Request["remark_Tag"];             //備註

                int listcount = int.Parse(basePage.Request["listcount"]);       //祈福人數量

                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsex = JArray.Parse(sex_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JbirthTime = JArray.Parse(birthTime_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jemail = JArray.Parse(email_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jsuppliestype = JArray.Parse(suppliestype_Tag);

                JArray Jhomenum = new JArray();
                JArray Jremark = new JArray();

                nullChecked(homenum_Tag, ref Jhomenum);
                nullChecked(remark_Tag, ref Jremark);

                string postURL = "Supplies_wu3_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["cht"] != null ? "_CHT" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                postURL += basePage.Request["fbwu"] != null ? "_FBWU" : "";

                postURL += basePage.Request["ig"] != null ? "_IG" : "";

                postURL += basePage.Request["fetssms"] != null ? "_fetSMS" : "";

                postURL += basePage.Request["jkos"] != null ? "_JKOS" : "";

                postURL += basePage.Request["gads"] != null ? "_GADS" : "";

                postURL += basePage.Request["inwu"] != null ? "_INWU" : "";

                postURL += basePage.Request["elv"] != null ? "_ELV" : "";

                //檢查此購買人電話是否上個月已註冊
                if (objLightDAC.CheckedSupplies_wu_info3(AdminID, AppMobile, Add_year))
                {
                    ApplicantID = objLightDAC.addapplicantinfo_Supplies_wu3(AppName, AppMobile, "0", Appemail, "", "", "", "0", "N", "", "", 0, AdminID, postURL, Add_year);
                    bool suppliesinfo = false;

                    if (ApplicantID > 0)
                    {
                        string name = Jname[0].ToString();
                        string homenum = Jhomenum.Count > 0 ? Jhomenum[0].ToString() : "";

                        string remark = Jremark.Count > 0 ? Jremark[0].ToString() : "";

                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                        string suppliesString = "企業補財庫";
                        string Birth = string.Empty;
                        string birthMonth = string.Empty;
                        string age = string.Empty;
                        string Zodiac = string.Empty;

                        string year = string.Empty;
                        string month = string.Empty;
                        string day = string.Empty;

                        string birth = Jbirth[0].ToString();
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

                            Birth = year + "-" + month + "-" + day;
                            LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                            //age = GetAge(DateTime.Parse(Birth_ad), dtNow);
                            age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                        }

                        if (name != "")
                        {
                            suppliesinfo = true;
                            suppliesID = objLightDAC.addsupplies_wu3(ApplicantID, name, Jmobile[0].ToString(), Jsuppliestype[0].ToString(), suppliesString, Jsex[0].ToString(), "1", Jbirth[0].ToString(), JleapMonth[0].ToString(), JbirthTime[0].ToString(), birthMonth, age, Zodiac, homenum, Jemail[0].ToString(), Jcounty[0].ToString() + Jdist[0] + Jaddr[0].ToString(), Jaddr[0].ToString(), Jcounty[0].ToString(), Jdist[0].ToString(), JzipCode[0].ToString(), remark, "1", Add_year);
                        }
                    }

                    if (ApplicantID > 0 && suppliesinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=6&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));

                        basePage.Session["ApplicantID"] = ApplicantID;
                    }
                }
                else
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                    basePage.mJSonHelper.AddContent("OldUser", 1);
                }
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getsupplies_wu_info3(applicantID, Add_year);

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