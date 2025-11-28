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
    public partial class templeService_supplies_dh : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
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

                int adminID = a = 16;
                this.supplies1.Visible = false;
                this.supplies2.Visible = false;
                this.supplies3.Visible = false;
                this.supplies4.Visible = false;
                this.supplies5.Visible = false;
                this.supplies6.Visible = false;
                this.supplies7.Visible = false;
                this.supplies8.Visible = false;
                this.supplies9.Visible = false;
                this.supplies10.Visible = false;
                this.supplies11.Visible = false;
                this.supplies12.Visible = false;
                this.supplies13.Visible = false;
                this.supplies14.Visible = false;
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
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["AppEmail"];                                     //購買人信箱
                string AppzipCode = basePage.Request["AppzipCode"];                                 //購買人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                                   //購買人縣市
                string Appdist = basePage.Request["Appdist"];                                       //購買人區域
                string Appaddr = basePage.Request["Appaddr"];                                       //購買人部分地址

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

                string count1_Tag = basePage.Request["count1_Tag"];                                 //水晶蓮花燈
                string count2_Tag = basePage.Request["count2_Tag"];                                 //財神財寶箱
                string count3_Tag = basePage.Request["count3_Tag"];                                 //虎爺財寶箱
                string count4_Tag = basePage.Request["count4_Tag"];                                 //旺龍紫氣寶燈
                string count5_Tag = basePage.Request["count5_Tag"];                                 //玉皇宥罪錫福七星燈
                string count6_Tag = basePage.Request["count6_Tag"];                                 //通天點金大龍香
                string count7_Tag = basePage.Request["count7_Tag"];                                 //五路財神香
                string count8_Tag = basePage.Request["count8_Tag"];                                 //開恩赦罪科儀
                string count9_Tag = basePage.Request["count9_Tag"];                                 //消災解厄科儀
                string count10_Tag = basePage.Request["count10_Tag"];                               //補運科儀
                string count11_Tag = basePage.Request["count11_Tag"];                               //身體康健科儀
                string count12_Tag = basePage.Request["count12_Tag"];                               //補財庫科儀
                string count13_Tag = basePage.Request["count13_Tag"];                               //補文昌科儀
                string count14_Tag = basePage.Request["count14_Tag"];                               //招貴人科儀


                //string count1_Tag = basePage.Request["count1_Tag"];                                 //三十二天帝燈
                //string count2_Tag = basePage.Request["count2_Tag"];                                 //黑虎將軍補財庫
                //string count3_Tag = basePage.Request["count3_Tag"];                                 //消災解厄科儀
                //string count4_Tag = basePage.Request["count4_Tag"];                                 //身體康健科儀
                //string count5_Tag = basePage.Request["count5_Tag"];                                 //補運科儀
                //string count6_Tag = basePage.Request["count6_Tag"];                                 //補財庫科儀
                //string count7_Tag = basePage.Request["count7_Tag"];                                 //補文昌科儀
                //string count8_Tag = basePage.Request["count8_Tag"];                                 //招貴人科儀

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量


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

                JArray Jcount1 = JArray.Parse(count1_Tag);
                JArray Jcount2 = JArray.Parse(count2_Tag);
                JArray Jcount3 = JArray.Parse(count3_Tag);
                JArray Jcount4 = JArray.Parse(count4_Tag);
                JArray Jcount5 = JArray.Parse(count5_Tag);
                JArray Jcount6 = JArray.Parse(count6_Tag);
                JArray Jcount7 = JArray.Parse(count7_Tag);
                JArray Jcount8 = JArray.Parse(count8_Tag);
                JArray Jcount9 = JArray.Parse(count9_Tag);
                JArray Jcount10 = JArray.Parse(count10_Tag);
                JArray Jcount11 = JArray.Parse(count11_Tag);
                JArray Jcount12 = JArray.Parse(count12_Tag);
                JArray Jcount13 = JArray.Parse(count13_Tag);
                JArray Jcount14 = JArray.Parse(count14_Tag);

                string postURL_Init = "Supplies_dh_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                ApplicantID = objLightDAC.Addapplicantinfo_supplies_dh(
                    Name: AppName, 
                    Mobile: AppMobile, 
                    Cost: "0", 
                    ZipCode: AppzipCode, 
                    County: Appcounty, 
                    Dist: Appdist, 
                    Addr: Appaddr, 
                    Sendback: "Y", 
                    ReceiptName: AppName, 
                    ReceiptMobile: AppMobile, 
                    Email: AppEmail, 
                    Status: 0, 
                    AdminID: AdminID, 
                    PostURL: postURL, 
                    Year: Year);

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                        string name = Jname[i].ToString();
                        string mobile = Jmobile[i].ToString();
                        string sex = Jsex[i].ToString();
                        string Birth = Jbirth[i].ToString();
                        string leapMonth = JleapMonth[i].ToString();
                        string birthTime = Jbirthtime[i].ToString();
                        string sBirth = Jsbirth[i].ToString();
                        string suppliesString = "天貺納福添運法會";
                        string suppliesType = GetSuppliesType(suppliesString);
                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string oversea = Joversea[i].ToString();
                        string remark = Jremark.Count > 0 ? Jremark[i].ToString() : "";
                        //string oversea = "1";
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

                        int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0, count7 = 0, count8 = 0, count9 = 0, count10 = 0;
                        int count11 = 0, count12 = 0, count13 = 0, count14 = 0;
                        int.TryParse(Jcount1[i].ToString(), out count1);
                        int.TryParse(Jcount2[i].ToString(), out count2);
                        int.TryParse(Jcount3[i].ToString(), out count3);
                        int.TryParse(Jcount4[i].ToString(), out count4);
                        int.TryParse(Jcount5[i].ToString(), out count5);
                        int.TryParse(Jcount6[i].ToString(), out count6);
                        int.TryParse(Jcount7[i].ToString(), out count7);
                        int.TryParse(Jcount8[i].ToString(), out count8);
                        int.TryParse(Jcount9[i].ToString(), out count9);
                        int.TryParse(Jcount10[i].ToString(), out count10);
                        int.TryParse(Jcount11[i].ToString(), out count11);
                        int.TryParse(Jcount12[i].ToString(), out count12);
                        int.TryParse(Jcount13[i].ToString(), out count13);
                        int.TryParse(Jcount14[i].ToString(), out count14);

                        int cost = 0;
                        cost += count1 > 0 ? count1 * 2388 : 0;
                        cost += count2 > 0 ? count2 * 800 : 0;
                        cost += count3 > 0 ? count3 * 800 : 0;
                        cost += count4 > 0 ? count4 * 700 : 0;
                        cost += count5 > 0 ? count5 * 1000 : 0;
                        cost += count6 > 0 ? count6 * 1000 : 0;
                        cost += count7 > 0 ? count7 * 200 : 0;
                        cost += count8 > 0 ? count8 * 600 : 0;
                        cost += count9 > 0 ? count9 * 600 : 0;
                        cost += count10 > 0 ? count10 * 600 : 0;
                        cost += count11 > 0 ? count11 * 600 : 0;
                        cost += count12 > 0 ? count12 * 600 : 0;
                        cost += count13 > 0 ? count13 * 600 : 0;
                        cost += count14 > 0 ? count14 * 600 : 0;

                        if (name != "")
                        {
                            SuppliesID = objLightDAC.Addsupplies_dh(
                                ApplicantID: ApplicantID, 
                                Name: name, 
                                Mobile: mobile,
                                Cost: cost,
                                Sex: sex, 
                                SuppliesType: suppliesType, 
                                SuppliesString: suppliesString,
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
                                Count1: count1,
                                Count2: count2,
                                Count3: count3,
                                Count4: count4,
                                Count5: count5,
                                Count6: count6,
                                Count7: count7,
                                Count8: count8,
                                Count9: count9,
                                Count10: count10,
                                Count11: count11,
                                Count12: count12,
                                Count13: count13,
                                Count14: count14,
                                Remark: remark, 
                                Addr: addr, 
                                County: county, 
                                Dist: dist, 
                                ZipCode: zipCode, 
                                Year: Year);
                        }

                    }
                }

                if (ApplicantID > 0 && SuppliesID > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    string redirectUrl = BuildRedirectUrl(
                        "templeCheck.aspx",
                        11,
                        AdminID,
                        ApplicantID,
                        basePage.Request
                    );

                    // 加入 JSON 回傳內容
                    basePage.mJSonHelper.AddContent("redirect", redirectUrl);

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

                dtData = objLightDAC.Getsupplies_dh_Info(applicantID, Year);

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