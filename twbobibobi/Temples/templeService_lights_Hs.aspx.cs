﻿using MotoSystem.Data;
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
    public partial class templeService_lights_Hs : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2025/05/08 23:59";
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

                int adminID = a = 17;

                LightDAC objLightDAC = new LightDAC(this);

                //if (objLightDAC.checkedLightsNum("3", adminID.ToString(), 1, Year))
                //{
                //    this.light1.Visible = true;
                //}
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int LightsID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "17";
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
                string oversea_Tag = basePage.Request["oversea_Tag"];                               //國內-1 國外-2
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                               //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                                 //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                                     //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                                     //祈福人部分地址
                string LightsString_Tag = basePage.Request["LightsString_Tag"];                     //服務項目

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
                JArray JLightsString_Tag = JArray.Parse(LightsString_Tag);

                JArray Jemail = new JArray();
                nullChecked(email_Tag, ref Jemail);

                string postURL_Init = "Lights_Hs_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                bool checkednum_Hs = true;

                if (checkednum_Hs)
                {
                    ApplicantID = objLightDAC.addapplicantinfo_lights_Hs(AppName, AppMobile, "0", "", "", "", "0", "N", "", "", 0, AdminID, postURL, Year);
                    bool lightsinfo = false;

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
                            string email = Jemail[i].ToString();
                            string lightsString = JLightsString_Tag[i].ToString(); ;
                            string lightsType = GetLightsType(lightsString, "17");
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
                                lightsinfo = true;
                                LightsID = objLightDAC.addLights_Hs(ApplicantID, name, mobile, sex, lightsType, lightsString,
                                    oversea, Birth, leapMonth, birthTime, birthMonth, age, Zodiac, sBirth, email, 1, addr, county, dist, zipCode, Year);
                            }
                        }
                    }

                    if (ApplicantID > 0 && lightsinfo)
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);

                        string rawQuery = basePage.Request.Url.Query.TrimStart('?');

                        // 將 query 拆成每個 key=value
                        string[] queryParts = rawQuery.Split('&');
                        List<string> filteredParts = new List<string>();

                        foreach (string part in queryParts)
                        {
                            if (part.StartsWith("RequestPageType=", StringComparison.OrdinalIgnoreCase))
                            {
                                break; // 一旦遇到 RequestPageType 就停止擷取
                            }

                            // 忽略 key 為 kind, a, aid 的參數
                            if (part.StartsWith("kind=", StringComparison.OrdinalIgnoreCase) ||
                                part.StartsWith("a=", StringComparison.OrdinalIgnoreCase) ||
                                part.StartsWith("aid=", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            if (!string.IsNullOrWhiteSpace(part))
                            {
                                filteredParts.Add(part);
                            }
                        }

                        // 組合處理後的 query 字串
                        string filteredQuery = string.Join("&", filteredParts);

                        basePage.mJSonHelper.AddContent("redirect",
                            "templeCheck.aspx?" +
                            (string.IsNullOrEmpty(filteredQuery) ? "" : filteredQuery + "&") +
                            "kind=1&a=" + AdminID + "&aid=" + ApplicantID);

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

                dtData = objLightDAC.Getlights_Hs_info(applicantID, Year);

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