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

namespace twbobibobi.Temples
{
    public partial class templeService_TaoistJiaoCeremony_da : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/12/26 23:59";
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

                int adminID = a = 3;
                this.taoistJiaoCeremony1.Visible = false;
                this.taoistJiaoCeremony2.Visible = false;
                this.taoistJiaoCeremony3.Visible = false;
                this.taoistJiaoCeremony4.Visible = false;
                this.taoistJiaoCeremony5.Visible = false;
                this.taoistJiaoCeremony6.Visible = false;
                this.taoistJiaoCeremony7.Visible = false;

                DateTime endTime = new DateTime(2024, 11, 16);
                int end = DateTime.Compare(dtNow, endTime);
                if (end > 0)
                {
                    this.taoistJiaoCeremony2.Visible = true;
                    this.taoistJiaoCeremony3.Visible = true;
                }


                //if (Request["twm"] != null)
                //{
                //    Response.Write("<script>alert('訪問網址錯誤。');window.location.href='https://bobibobi.tw/Temples/templeService_TaoistJiaoCeremony_da.aspx'</script>");
                //}
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int TaoistJiaoCeremonyID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "3";
                string AppName = basePage.Request["Appname"];                                               //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];                                           //申請人電話
                string Appemail = basePage.Request["Appemail"];                                             //申請人Email
                string AppzipCode = basePage.Request["AppzipCode"];                                         //申請人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                                           //申請人縣市
                string Appdist = basePage.Request["Appdist"];                                               //申請人區域
                string Appaddr = basePage.Request["Appaddr"];                                               //申請人部分地址

                string Sendback = "N";

                string name_Tag = basePage.Request["name_Tag"];                                             //祈福人姓名
                string name2_Tag = basePage.Request["name2_Tag"];                                           //祈福人姓名2
                string name3_Tag = basePage.Request["name3_Tag"];                                           //祈福人姓名3
                string name4_Tag = basePage.Request["name4_Tag"];                                           //祈福人姓名4
                string name5_Tag = basePage.Request["name5_Tag"];                                           //祈福人姓名5
                string name6_Tag = basePage.Request["name6_Tag"];                                           //祈福人姓名6
                string mobile_Tag = basePage.Request["mobile_Tag"];                                         //祈福人電話
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                                       //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                         //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                             //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                             //祈福人部分地址
                string sendback_Tag = basePage.Request["sendback_Tag"];                                     //寄回-Y 不寄回-N
                string rname_Tag = basePage.Request["rname_Tag"];                                           //收件人姓名
                string rmobile_Tag = basePage.Request["rmobile_Tag"];                                       //收件人電話
                string rzipCode_Tag = basePage.Request["rzipCode_Tag"];                                     //收件人郵遞區號
                string rcounty_Tag = basePage.Request["rcounty_Tag"];                                       //收件人縣市
                string rdist_Tag = basePage.Request["rdist_Tag"];                                           //收件人區域
                string raddr_Tag = basePage.Request["raddr_Tag"];                                           //收件人部分地址
                string TaoistJiaoCeremonyType_Tag = basePage.Request["TaoistJiaoCeremonyType_Tag"];         //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                                   //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);

                JArray Jname2 = new JArray();
                JArray Jname3 = new JArray();
                JArray Jname4 = new JArray();
                JArray Jname5 = new JArray();
                JArray Jname6 = new JArray();

                JArray Jsendback = new JArray();
                JArray Jrname = new JArray();
                JArray Jrmobile = new JArray();
                JArray JrzipCode = new JArray();
                JArray Jrcounty = new JArray();
                JArray Jrdist = new JArray();
                JArray Jraddr = new JArray();

                nullChecked(name2_Tag, ref Jname2);
                nullChecked(name3_Tag, ref Jname3);
                nullChecked(name4_Tag, ref Jname4);
                nullChecked(name5_Tag, ref Jname5);
                nullChecked(name6_Tag, ref Jname6);

                nullChecked(sendback_Tag, ref Jsendback);
                nullChecked(rname_Tag, ref Jrname);
                nullChecked(rmobile_Tag, ref Jrmobile);
                nullChecked(rzipCode_Tag, ref JrzipCode);
                nullChecked(rcounty_Tag, ref Jrcounty);
                nullChecked(rdist_Tag, ref Jrdist);
                nullChecked(raddr_Tag, ref Jraddr);

                JArray JTaoistJiaoCeremonyTypw_Tag = JArray.Parse(TaoistJiaoCeremonyType_Tag);

                string postURL = "TaoistJiaoCeremony_da_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["cht"] != null ? "_CHT" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                postURL += basePage.Request["ig"] != null ? "_IG" : "";

                postURL += basePage.Request["fbda"] != null ? "_FBDA" : "";

                postURL += basePage.Request["fetsms"] != null ? "_fetSMS" : "";

                postURL += basePage.Request["jkos"] != null ? "_JKOS" : "";

                postURL += basePage.Request["gads"] != null ? "_GADS" : "";

                bool checkednum_da = true;

                if (checkednum_da)
                {
                    ApplicantID = objLightDAC.addapplicantinfo_TaoistJiaoCeremony_da(AppName, AppMobile, "", "N", "吉", "0", "0", "", Appemail, "0", AppzipCode, Appcounty, Appdist,
                        Appaddr, Sendback, AppName, AppMobile, 0, AdminID, postURL, Year);
                    bool taoistJiaoCeremonyinfo = false;

                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            string name = Jname[i].ToString();
                            string name2 = Jname2[i].ToString();
                            string name3 = Jname3[i].ToString();
                            string name4 = Jname4[i].ToString();
                            string name5 = Jname5[i].ToString();
                            string name6 = Jname6[i].ToString();
                            string mobile = Jmobile[i].ToString();
                            string sex = "善男";
                            string Birth = "";
                            string leapMonth = "N";
                            string birthTime = "吉";
                            string sBirth = "";
                            string taoistJiaoCeremonyType = JTaoistJiaoCeremonyTypw_Tag[i].ToString();
                            string taoistJiaoCeremonyString = GetTaoistJiaoCeremonyType2String(taoistJiaoCeremonyType, "3");
                            string addr = Jaddr[i].ToString();
                            string county = Jcounty[i].ToString();
                            string dist = Jdist[i].ToString();
                            string zipCode = JzipCode[i].ToString();
                            string sendback = Jsendback[i].ToString();
                            string rname = Jrname[i].ToString();
                            string rmobile = Jrmobile[i].ToString();
                            string raddr = Jraddr[i].ToString();
                            string rcounty = Jrcounty[i].ToString();
                            string rdist = Jrdist[i].ToString();
                            string rzipCode = JrzipCode[i].ToString();
                            string birthMonth = "0";
                            string age = "0";
                            string Zodiac = string.Empty;
                            string year = string.Empty;
                            string month = string.Empty;
                            string day = string.Empty;
                            string syear = string.Empty;
                            string smonth = string.Empty;
                            string sday = string.Empty;

                            switch (taoistJiaoCeremonyType)
                            {
                                case "1":
                                    name3 = name4 = name5 = name6 = "";
                                    break;
                                case "2":
                                    name2 = name3 = name4 = name5 = name6 = "";
                                    break;
                                case "3":
                                    name2 = name3 = name4 = name5 = name6 = "";
                                    break;
                                case "4":
                                    break;
                                case "5":
                                    name3 = name4 = name5 = name6 = "";
                                    break;
                                case "6":
                                    name3 = name4 = name5 = name6 = "";
                                    break;
                                case "7":
                                    name3 = name4 = name5 = name6 = "";
                                    break;
                            }

                            if (name != "")
                            {
                                taoistJiaoCeremonyinfo = true;
                                TaoistJiaoCeremonyID = objLightDAC.addTaoistJiaoCeremony_da(ApplicantID, name, name2, name3, name4, name5, name6, mobile, sex, taoistJiaoCeremonyType, 
                                    taoistJiaoCeremonyString, "1", Birth, leapMonth, birthTime, birthMonth, age, Zodiac, sBirth, 1, addr, county, dist, zipCode, sendback, rname, 
                                    rmobile, raddr, rcounty, rdist, rzipCode,Year);
                            }

                        }
                    }

                    if (ApplicantID > 0 && taoistJiaoCeremonyinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=13&a=" + AdminID + "&aid=" + ApplicantID + 
                            (basePage.Request["ad"] != null ? "&ad=1" : "") +
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

                dtData = objLightDAC.GettaoistJiaoCeremony_da_info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
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