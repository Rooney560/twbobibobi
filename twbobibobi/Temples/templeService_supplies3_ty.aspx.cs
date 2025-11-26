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

namespace twbobibobi.Temples
{
    public partial class templeService_supplies3_ty : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2025/02/04 23:59";
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
                this.supplies3.Visible = false;
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
                string AdminID = "14";
                string AppName = basePage.Request["Appname"];                               //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                           //購買人電話
                string AppEmail = basePage.Request["Appemail"];                             //購買人Email
                string Appbirth = basePage.Request["Appbirth"];                             //購買人國曆生日
                string AppzipCode = basePage.Request["AppzipCode"];                         //購買人郵遞區號
                string Appcounty = basePage.Request["Appcounty"];                           //購買人縣市
                string Appdist = basePage.Request["Appdist"];                               //購買人區域
                string Appaddr = basePage.Request["Appaddr"];                               //購買人部分地址

                string name_Tag = basePage.Request["name_Tag"];                             //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                         //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];                           //祈福人國曆生日
                string oversea_Tag = basePage.Request["oversea_Tag"];                       //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string remark_Tag = basePage.Request["remark_Tag"];                         //備註
                string SuppliesType_Tag = basePage.Request["SuppliesType_Tag"];         //服務項目

                int listcount = int.Parse(basePage.Request["listcount"]);                   //祈福人數量

                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray Joversea = JArray.Parse(oversea_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jremark = JArray.Parse(remark_Tag);
                JArray JSuppliesType_Tag = JArray.Parse(SuppliesType_Tag);

                string postURL = "Supplies3_ty_Index";

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

                bool checkednum_ty = true;

                if (checkednum_ty)
                {
                    string AppBirth = string.Empty;
                    string AppbirthMonth = "0";
                    string Appage = "0";
                    string AppZodiac = string.Empty;
                    string AppsBirth = Appbirth;

                    string Appyear = string.Empty;
                    string Appmonth = string.Empty;
                    string Appday = string.Empty;
                    string Appsyear = string.Empty;
                    string Appsmonth = string.Empty;
                    string Appsday = string.Empty;


                    string Appsbirth = AppsBirth;
                    if (Appsbirth.IndexOf("民國") >= 0 && Appsbirth.IndexOf("年") > 0 && Appsbirth.IndexOf("月") > 0 && Appsbirth.IndexOf("日") > 0)
                    {
                        int Appsbirth_roc_index = Appsbirth.IndexOf("民國");
                        int Appsbirth_year_index = Appsbirth.IndexOf("年");
                        int Appsbirth_month_index = Appsbirth.IndexOf("月");
                        int Appsbirth_day_index = Appsbirth.IndexOf("日");
                        Appsyear = (int.Parse(Appsbirth.Substring(2, Appsbirth_year_index - 2)) + 1911).ToString();
                        Appsmonth = CheckedDateZero(Appsbirth.Substring(Appsbirth_year_index + 1, Appsbirth_month_index - Appsbirth_year_index - 1), 1);
                        Appsday = CheckedDateZero(Appsbirth.Substring(Appsbirth_month_index + 1, Appsbirth.Length - Appsbirth_month_index - 2), 1);

                        Solar solor = new Solar();
                        int.TryParse(Appsyear, out solor.solarYear);
                        int.TryParse(Appsmonth, out solor.solarMonth);
                        int.TryParse(Appsday, out solor.solarDay);

                        Lunar lunar = new Lunar();
                        lunar = LunarSolarConverter.SolarToLunar(solor);

                        LunarSolarConverter.shuxiang(lunar.lunarYear, ref AppZodiac);
                        Appage = GetAge(lunar.lunarYear, lunar.lunarMonth, lunar.lunarDay).ToString();
                        AppbirthMonth = CheckedDateZero(lunar.lunarMonth.ToString(), 1);

                        string ROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                        AppBirth = ROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";

                        string sROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                        AppsBirth = sROC + Appsmonth + "月" + Appsday + "日";
                    }

                    AppbirthMonth = CheckedDateZero(AppbirthMonth, 1);

                    ApplicantID = objLightDAC.Addapplicantinfo_supplies3_ty(
                        Name: AppName,
                        Mobile: AppMobile,
                        Birth: AppBirth,
                        LeapMonth: "N",
                        BirthTime: "吉",
                        BirthMonth: AppbirthMonth,
                        Age: Appage,
                        Zodiac: AppZodiac,
                        sBirth: AppsBirth,
                        Cost: "0",
                        Email: AppEmail,
                        ZipCode: AppzipCode,
                        County: Appcounty,
                        Dist: Appdist,
                        Addr: Appaddr,
                        Sendback: "Y",
                        ReceiptName: AppName,
                        ReceiptMobile: AppMobile,
                        Status: 0,
                        AdminID: AdminID,
                        PostURL: postURL,
                        Year: Year);

                    bool suppliesinfo = false;

                    if (ApplicantID > 0)
                    {
                        for (int i = 0; i < listcount; i++)
                        {
                            string name = Jname[i].ToString();
                            string mobile = Jmobile[i].ToString();
                            string sex = "善男";
                            string Birth = "";
                            string leapMonth = "N";
                            string birthTime = "吉";
                            string sBirth = Jbirth[i].ToString();
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

                            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

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

                            birthMonth = CheckedDateZero(birthMonth, 1);

                            int cost = GetSuppliesCost(14, suppliesType);

                            if (name != "")
                            {
                                suppliesinfo = true;
                                SuppliesID = objLightDAC.Addsupplies3_ty(
                                    ApplicantID: ApplicantID,
                                    Name: name,
                                    Mobile: mobile,
                                    Cost: cost,
                                    Sex: "善男",
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
                                    Count: 1,
                                    Remark: remark,
                                    Addr: addr,
                                    County: county,
                                    Dist: dist,
                                    ZipCode: zipCode,
                                    Year: Year);
                            }
                        }
                    }

                    if (ApplicantID > 0 && suppliesinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=18&a=" + AdminID + "&aid=" + ApplicantID +
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

                dtData = objLightDAC.Getsupplies3_ty_Info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());
                    basePage.mJSonHelper.AddContent("AppsBirth", dtData.Rows[0]["AppsBirth"].ToString());
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

            //補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版
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
                    case "招財補運":
                        result = "7";
                        break;
                    case "招財補運九九重陽升級版":
                        result = "8";
                        break;
                }

                return result;
            }

            private string GetSuppliesString(string v)
            {
                switch (v)
                {
                    case "8":
                        return "招財補運加強版";
                    default:
                        return "招財補運";
                }
            }

        }
    }
}