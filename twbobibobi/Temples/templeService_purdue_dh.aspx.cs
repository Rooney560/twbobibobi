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
    public partial class templeService_purdue_dh : AjaxBasePage
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
                string AdminID = "16";
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["AppEmail"];                                     //購買人信箱

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
                string purduetype_Tag = basePage.Request["purduetype_Tag"];                         //普度項目

                string deathname_Tag = basePage.Request["deathname_Tag"];               //往生者姓名
                string deathday_Tag = basePage.Request["deathday_Tag"];                 //往生日期
                string deathbirth_Tag = basePage.Request["deathbirth_Tag"];             //農歷生日
                string deathleapMonth_Tag = basePage.Request["deathleapMonth_Tag"];     //閏月 Y-是 N-否
                string deathbirthtime_Tag = basePage.Request["deathbirthtime_Tag"];     //農曆時辰
                string firstname_Tag = basePage.Request["firstname_Tag"];               //姓氏
                string deathzipCode_Tag = basePage.Request["deathzipCode_Tag"];         //郵遞區號
                string deathcounty_Tag = basePage.Request["deathcounty_Tag"];           //縣市
                string deathdist_Tag = basePage.Request["deathdist_Tag"];               //區域
                string deathaddr_Tag = basePage.Request["deathaddr_Tag"];               //部分地址
                int listcount = int.Parse(basePage.Request["listcount"]);               //祈福人數量


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
                JArray Jpurduetype = JArray.Parse(purduetype_Tag);

                JArray Jdeathname = new JArray();
                JArray Jdeathday = new JArray();
                JArray Jdeathbirth = new JArray();
                JArray Jfirstname = new JArray();
                JArray JdeathleapMonth = JArray.Parse(deathleapMonth_Tag);
                JArray Jdeathbirthtime = JArray.Parse(deathbirthtime_Tag);
                JArray JdeathzipCode = JArray.Parse(deathzipCode_Tag);
                JArray Jdeathcounty = JArray.Parse(deathcounty_Tag);
                JArray Jdeathdist = JArray.Parse(deathdist_Tag);
                JArray Jdeathaddr = JArray.Parse(deathaddr_Tag);

                nullChecked(deathname_Tag, ref Jdeathname);
                nullChecked(deathday_Tag, ref Jdeathday);
                nullChecked(deathbirth_Tag, ref Jdeathbirth);
                nullChecked(firstname_Tag, ref Jfirstname);
                nullChecked(deathleapMonth_Tag, ref JdeathleapMonth);
                nullChecked(deathbirthtime_Tag, ref Jdeathbirthtime);
                nullChecked(deathzipCode_Tag, ref JdeathzipCode);
                nullChecked(deathcounty_Tag, ref Jdeathcounty);
                nullChecked(deathdist_Tag, ref Jdeathdist);
                nullChecked(deathaddr_Tag, ref Jdeathaddr);

                string postURL_Init = "Purdue_dh_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                string AppSendback = "N";                                                           //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                string Apprname = AppName;                                                          //收件人姓名
                string Apprmobile = AppMobile;                                                      //收件人電話
                string Appcounty = "";                                                              //購買人地址縣市
                string Appdist = "";                                                                //購買人地址區域
                string Appaddr = "";                                                                //購買人地址部分地址
                string AppzipCode = "";                                                             //購買人地址郵遞區號

                ApplicantID = objLightDAC.Addapplicantinfo_purdue_dh(
                    Name: AppName,
                    Mobile: AppMobile,
                    Cost: "0",
                    County: Appcounty,
                    Dist: Appdist,
                    Addr: Appaddr,
                    ZipCode: AppzipCode,
                    Sendback: AppSendback,
                    ReceiptName: Apprname,
                    ReceiptMobile: Apprmobile,
                    Email: AppEmail,
                    Status: 0,
                    AdminID: AdminID,
                    PostURL: postURL,
                    Year: Year);
                bool purdueinfo = false;

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
                        string purdueType = Jpurduetype[i].ToString();
                        string purdueString = PurdueType2String(purdueType, "16");
                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string oversea = Joversea[i].ToString();
                        string remark = Jremark[i].ToString();

                        string deathname = Jdeathname.Count > 0 ? Jdeathname[i].ToString() : "";
                        string deathday = Jdeathday.Count > 0 ? Jdeathday[i].ToString() : "";
                        string deathbirth = Jdeathbirth.Count > 0 ? Jdeathbirth[i].ToString() : "";
                        string firstname = Jfirstname.Count > 0 ? Jfirstname[i].ToString() : "";
                        string deathLeapMonth = JdeathleapMonth.Count > 0 ? JdeathleapMonth[i].ToString() : "N";
                        string deathBirthTime = Jdeathbirthtime.Count > 0 ? Jdeathbirthtime[i].ToString() : "吉";

                        string deathAddr = Jdeathaddr.Count > 0 ? Jdeathaddr[i].ToString() : "";
                        string deathCounty = Jdeathcounty.Count > 0 ? Jdeathcounty[i].ToString() : "";
                        string deathDist = Jdeathdist.Count > 0 ? Jdeathdist[i].ToString() : "";
                        string deathZipCode = JdeathzipCode.Count > 0 ? JdeathzipCode[i].ToString() : "0";

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

                        string deathbirthMonth = string.Empty;
                        string deathage = string.Empty;
                        string deathZodiac = string.Empty;
                        string deathyear = string.Empty;
                        string deathmonth = string.Empty;
                        string dday = string.Empty;

                        GetDeathBirthDetail(deathbirth, deathday, ref deathbirthMonth, ref deathage, ref deathZodiac);

                        int cost = GetPurdueCost(16, purdueType);

                        if (name != "")
                        {
                            purdueinfo = true;
                            PurdueID = objLightDAC.Addpurdue_dh(
                                ApplicantID: ApplicantID,
                                Name: name,
                                Mobile: mobile,
                                Cost: cost,
                                Sex: sex,
                                PurdueType: purdueType,
                                PurdueString: purdueString,
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
                                DeathName: deathname,
                                Deathday: deathday,
                                DeathBirth: deathbirth,
                                DeathLeapMonth: deathLeapMonth,
                                DeathBirthTime: deathBirthTime,
                                DeathBirthMonth: deathbirthMonth,
                                DeathAge: deathage,
                                DeathZodiac: deathZodiac,
                                FirstName: firstname,
                                DeathAddr: deathAddr,
                                DeathCounty: deathCounty,
                                DeathDist: deathDist,
                                DeathZipCode: deathZipCode,
                                Year: Year);

                        }
                    }
                }

                if (ApplicantID > 0 && purdueinfo)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    string redirectUrl = BuildRedirectUrl(
                        "templeCheck.aspx",
                        2,
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

                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                string Year = dtNow.Year.ToString();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getpurdue_dh_Info(applicantID, Year);

                if (dtData.Rows.Count > 0)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                    basePage.mJSonHelper.AddContent("listcount", dtData.Rows.Count);
                    basePage.mJSonHelper.AddContent("a", AdminID);
                    basePage.mJSonHelper.AddContent("AppName", dtData.Rows[0]["AppName"].ToString());
                    basePage.mJSonHelper.AddContent("AppMobile", dtData.Rows[0]["AppMobile"].ToString());
                    basePage.mJSonHelper.AddContent("AppEmail", dtData.Rows[0]["AppEmail"].ToString());

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

            //public string Purdue2String(string purdueType)
            //{
            //    string result = string.Empty;

            //    switch (purdueType)
            //    {
            //        case "1":
            //            //贊普
            //            result = "贊普";
            //            break;
            //        case "2":
            //            //超薦『歷代祖先』
            //            result = "歷代祖先";
            //            break;
            //        case "3":
            //            //超薦『往生親友』
            //            result = "往生親友";
            //            break;
            //        case "5":
            //            //超薦『冤親債主』
            //            result = "冤親債主";
            //            break;
            //        case "6":
            //            //超薦『嬰靈(無緣子女)』
            //            result = "嬰靈(無緣子女)";
            //            break;
            //    }

            //    return result;
            //}
        }
    }
}