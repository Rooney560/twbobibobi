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
    public partial class templeService_supplies_sx : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2025/02/03 23:59";
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

                int adminID = a = 33;
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
                string AdminID = "33";
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話

                string name_Tag = basePage.Request["name_Tag"];                                     //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                                 //祈福人電話
                string sex_Tag = basePage.Request["sex_Tag"];                                       //祈福人性別
                string birth_Tag = basePage.Request["birth_Tag"];                                   //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];                           //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];                           //祈福人農曆時辰
                string sbirth_Tag = basePage.Request["sbirth_Tag"];                                 //祈福人國曆生日
                string email_Tag = basePage.Request["email_Tag"];                                   //祈福人信箱
                string homenum_Tag = basePage.Request["homenum_Tag"];                               //祈福人市話
                string oversea_Tag = basePage.Request["oversea_Tag"];                               //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string SuppliesType_Tag = basePage.Request["SuppliesType_Tag"];                     //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jsex = JArray.Parse(sex_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray Jsbirth = JArray.Parse(sbirth_Tag);
                JArray Jemail = JArray.Parse(email_Tag);
                JArray Joversea = JArray.Parse(oversea_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray JSuppliesType_Tag = JArray.Parse(SuppliesType_Tag);

                JArray Jhomenum = new JArray();
                nullChecked(homenum_Tag, ref Jhomenum);

                string postURL = "Supplies_sx_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["cht"] != null ? "_CHT" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                postURL += basePage.Request["fbsx"] != null ? "_FBSX" : "";

                postURL += basePage.Request["ig"] != null ? "_IG" : "";

                postURL += basePage.Request["fetsms"] != null ? "_fetSMS" : "";

                postURL += basePage.Request["jkos"] != null ? "_JKOS" : "";

                postURL += basePage.Request["gads"] != null ? "_GADS" : "";

                postURL += basePage.Request["insx"] != null ? "_INSX" : "";

                postURL += basePage.Request["elv"] != null ? "_ELV" : "";


                string AppSendback = "N";                                                           //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                string Apprname = "";                                                               //收件人姓名
                string Apprmobile = "";                                                             //收件人電話
                string ApprzipCode = "0";                                                           //收件人郵政區號
                string Apprcounty = "";                                                             //收件人縣市
                string Apprdist = "";                                                               //收件人區域
                string Appraddr = "";                                                               //收件人部分地址

                ApplicantID = objLightDAC.addapplicantinfo_Supplies_sx(AppName, AppMobile, "0", Apprcounty, Apprdist, Appraddr, ApprzipCode, AppSendback, Apprname, Apprmobile
                    , 0, AdminID, postURL, Year);
                bool suppliesinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();
                        string mobile = Jmobile[i].ToString();
                        string sex = Jsex[i].ToString();
                        string Birth = Jbirth[i].ToString();
                        string leapMonth = JleapMonth[i].ToString();
                        string birthTime = Jbirthtime[i].ToString();
                        string sBirth = Jsbirth[i].ToString();
                        string email = Jemail.Count > 0 ? Jemail[i].ToString() : "";
                        string homenum = Jhomenum.Count > 0 ? Jhomenum[i].ToString() : "";
                        string suppliesType = JSuppliesType_Tag[i].ToString();
                        string suppliesString = GetSuppliesString(suppliesType); ;
                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string oversea = Joversea[i].ToString();
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

                        if (name != "")
                        {
                            suppliesinfo = true;
                            SuppliesID = objLightDAC.addSupplies_sx(ApplicantID, name, mobile, sex, suppliesType, suppliesString, oversea, Birth, leapMonth, birthTime,
                                birthMonth, age, Zodiac, sBirth, email, homenum, 1, addr, county, dist, zipCode, Year);
                        }

                    }
                }

                if (ApplicantID > 0 && SuppliesID > 0 && suppliesinfo)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=17&a=" + AdminID + "&aid=" + ApplicantID +
                        (basePage.Request["ad"] != null ? "&ad=" + basePage.Request["ad"] : "") +
                        (basePage.Request["jkos"] != null ? "&jkos=1" : "") +
                        (basePage.Request["twm"] != null ? "&twm=1" : ""));

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

                dtData = objLightDAC.Getsupplies_sx_info(applicantID, Year);

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

            //補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-代燒金紙 7-招財補運 8-招財補運九九重陽升級版 9-補財庫 10-財神賜福-消災補庫法會 11-地母廟-赦罪解業
            //          12-地母廟-補財庫 13-地母廟-赦罪解業+補財庫 14-草屯敦和宮-赦罪解業 15-草屯敦和宮-補財庫 16-草屯敦和宮-赦罪解業+補財庫 17-紫南宮-赦罪解業 18-紫南宮-補財庫
            //          19-紫南宮-赦罪解業+補財庫
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
                    case "補財庫":
                        result = "9";
                        break;
                    case "財神賜福-消災補庫法會":
                        result = "10";
                        break;
                    case "地母廟-赦罪解業":
                        result = "11";
                        break;
                    case "地母廟-補財庫":
                        result = "12";
                        break;
                    case "地母廟-赦罪解業+補財庫":
                        result = "13";
                        break;
                    case "草屯敦和宮-赦罪解業":
                        result = "14";
                        break;
                    case "草屯敦和宮-補財庫":
                        result = "15";
                        break;
                    case "草屯敦和宮-赦罪解業+補財庫":
                        result = "16";
                        break;
                    case "紫南宮-赦罪解業":
                        result = "17";
                        break;
                    case "紫南宮-補財庫":
                        result = "18";
                        break;
                    case "紫南宮-赦罪解業+補財庫":
                        result = "19";
                        break;
                }

                return result;
            }
            public string GetSuppliesString(string SuppliesType)
            {
                string result = string.Empty;
                switch (SuppliesType)
                {
                    case "1":
                        result = "下元補庫";
                        break;
                    case "2":
                        result = "呈疏補庫";
                        break;
                    case "3":
                        result = "企業補財庫";
                        break;
                    case "4":
                        result = "天赦日補運";
                        break;
                    case "5":
                        result = "天赦日祭改";
                        break;
                    case "6":
                        result = "天貺納福添運法會";
                        break;
                    case "7":
                        result = "";
                        break;
                    case "8":
                        result = "";
                        break;
                    case "9":
                        result = "補財庫";
                        break;
                    case "10":
                        result = "財神賜福-消災補庫法會";
                        break;
                    case "11":
                        result = "地母廟-赦罪解業";
                        break;
                    case "12":
                        result = "地母廟-補財庫";
                        break;
                    case "13":
                        result = "地母廟-赦罪解業+補財庫";
                        break;
                    case "14":
                        result = "草屯敦和宮-赦罪解業";
                        break;
                    case "15":
                        result = "草屯敦和宮-補財庫";
                        break;
                    case "16":
                        result = "草屯敦和宮-赦罪解業+補財庫";
                        break;
                    case "17":
                        result = "紫南宮-赦罪解業";
                        break;
                    case "18":
                        result = "紫南宮-補財庫";
                        break;
                    case "19":
                        result = "紫南宮-赦罪解業+補財庫";
                        break;
                }

                return result;
            }
        }
    }
}