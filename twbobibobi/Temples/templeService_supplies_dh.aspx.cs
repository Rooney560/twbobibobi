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
    public partial class templeService_supplies_dh : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/07/11 23:59";
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

                int adminID = a = 16;
                this.supplies1.Visible = false;
                this.supplies2.Visible = false;
                this.supplies3.Visible = false;
                this.supplies4.Visible = false;
                this.supplies5.Visible = false;
                this.supplies6.Visible = false;
                this.supplies7.Visible = false;
                this.supplies8.Visible = false;
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int SuppliesID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "16";
                string AppName = basePage.Request["Appname"];                   //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];               //申請人電話

                string name_Tag = basePage.Request["name_Tag"];                 //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];             //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];               //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];       //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];       //祈福人農曆時辰
                string sbirth_Tag = basePage.Request["sbirth_Tag"];             //祈福人國歷生日
                string zipCode_Tag = basePage.Request["zipCode_Tag"];           //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];             //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                 //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                 //祈福人部分地址
                string count1_Tag = basePage.Request["count1_Tag"];             //三十二天帝燈
                string count2_Tag = basePage.Request["count2_Tag"];             //黑虎將軍補財庫
                string count3_Tag = basePage.Request["count3_Tag"];             //消災解厄科儀
                string count4_Tag = basePage.Request["count4_Tag"];             //身體康健科儀
                string count5_Tag = basePage.Request["count5_Tag"];             //補運科儀
                string count6_Tag = basePage.Request["count6_Tag"];             //補財庫科儀
                string count7_Tag = basePage.Request["count7_Tag"];             //補文昌科儀
                string count8_Tag = basePage.Request["count8_Tag"];             //招貴人科儀

                int listcount = int.Parse(basePage.Request["listcount"]);       //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray Jsbirth = JArray.Parse(sbirth_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);

                JArray Jcount1 = JArray.Parse(count1_Tag);
                JArray Jcount2 = JArray.Parse(count2_Tag);
                JArray Jcount3 = JArray.Parse(count3_Tag);
                JArray Jcount4 = JArray.Parse(count4_Tag);
                JArray Jcount5 = JArray.Parse(count5_Tag);
                JArray Jcount6 = JArray.Parse(count6_Tag);
                JArray Jcount7 = JArray.Parse(count7_Tag);
                JArray Jcount8 = JArray.Parse(count8_Tag);

                string postURL = "Supplies_dh_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                ApplicantID = objLightDAC.addapplicantinfo_Supplies_dh(AppName, AppMobile, "0", "0", "", "", "", "Y", AppName, AppMobile, 0, AdminID, postURL, Year);

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();
                        string mobile = Jmobile[i].ToString();
                        string leapMonth = JleapMonth[i].ToString();
                        string birthTime = Jbirthtime[i].ToString();

                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();

                        int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0, count7 = 0, count8 = 0;
                        int.TryParse(Jcount1[i].ToString(), out count1);
                        int.TryParse(Jcount2[i].ToString(), out count2);
                        int.TryParse(Jcount3[i].ToString(), out count3);
                        int.TryParse(Jcount4[i].ToString(), out count4);
                        int.TryParse(Jcount5[i].ToString(), out count5);
                        int.TryParse(Jcount6[i].ToString(), out count6);
                        int.TryParse(Jcount7[i].ToString(), out count7);
                        int.TryParse(Jcount8[i].ToString(), out count8);

                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                        string suppliesString = "天貺納福添運法會";
                        string suppliesType = GetSuppliesType(suppliesString);
                        string Birth = string.Empty;
                        string birthMonth = "0";
                        string age = "0";
                        string Zodiac = string.Empty;
                        string birth = Jbirth[i].ToString();
                        string sbirth = Jsbirth[i].ToString();

                        if (Jbirth[i].ToString() != "")
                        {
                            GetBirthDetail(birth, ref birthMonth, ref age, ref Zodiac);
                        }
                        else if (Jsbirth[i].ToString() != "")
                        {
                            GetBirthDetail(sbirth, ref birthMonth, ref age, ref Zodiac);
                        }

                        if (name != "")
                        {
                            SuppliesID = objLightDAC.addSupplies_dh(ApplicantID, name, mobile, "善男", suppliesType, suppliesString, "1", birth, leapMonth, birthTime, sbirth, 
                                birthMonth, age, Zodiac, count1, count2, count3, count4, count5, count6, count7, count8, addr, county, dist, zipCode, Year);
                        }

                    }
                }

                if (ApplicantID > 0 && SuppliesID > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=11&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));

                    basePage.Session["ApplicantID"] = ApplicantID;
                }
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getsupplies_dh_info(applicantID, Year);

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

            //補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會
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
                    case "天赦日祭改":
                        result = "5";
                        break;
                    case "天貺納福添運法會":
                        result = "6";
                        break;
                }

                return result;
            }
        }
    }
}