using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Encoders;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using twbobibobi.Data;


namespace Temple.Temples
{
    public partial class templeService_purdue_Fu : AjaxBasePage
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

                int adminID = a = 8;
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
                string AdminID = "8";
                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["AppEmail"];                                     //購買人信箱
                string Appcounty = basePage.Request["Appcounty"];                       //購買人縣市
                string Appdist = basePage.Request["Appdist"];                           //購買人區域
                string Appaddr = basePage.Request["Appaddr"];                           //購買人地址(部分)
                string AppzipCode = basePage.Request["AppzipCode"];                     //購買人郵遞區號

                string zampname_Tag = basePage.Request["zampname_Tag"];                 //祈福人姓名
                string zampname2_Tag = basePage.Request["zampname2_Tag"];               //祈福人姓名2
                string zampmobile_Tag = basePage.Request["zampmobile_Tag"];             //祈福人電話
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
                string purduetype_Tag = basePage.Request["purduetype_Tag"];             //普度項目
                string count_Tag = basePage.Request["count_Tag"];                       //普品數量


                string salvationname_Tag = basePage.Request["salvationname_Tag"];       //祈福人姓名
                string firstname_Tag = basePage.Request["firstname_Tag"];               //姓氏
                string deathname_Tag = basePage.Request["deathname_Tag"];               //超度姓名
                string dzipCode_Tag = basePage.Request["dzipCode_Tag"];                 //超度(牌位)郵遞區號
                string dcounty_Tag = basePage.Request["dcounty_Tag"];                   //超度(牌位)縣市
                string ddist_Tag = basePage.Request["ddist_Tag"];                       //超度(牌位)區域
                string daddr_Tag = basePage.Request["daddr_Tag"];                       //超度(牌位)部分地址
                string addpurdue_Tag = basePage.Request["addpurdue_Tag"];               //加購普品
                string GoldPaperCount_Tag = basePage.Request["GoldPaperCount_Tag"];     //金紙數量

                int listcount = int.Parse(basePage.Request["listcount"]);               //祈福人數量


                JArray Jpurduetype = JArray.Parse(purduetype_Tag);

                JArray JZname = new JArray();
                JArray JZname2 = new JArray();
                JArray JZmobile = new JArray();
                JArray Jsex = new JArray();
                JArray Jbirth = new JArray();
                JArray JleapMonth = new JArray();
                JArray Jbirthtime = new JArray();
                JArray Jsbirth = new JArray();
                JArray Joversea = new JArray();
                JArray JzipCode = new JArray();
                JArray Jcounty = new JArray();
                JArray Jdist = new JArray(); ;
                JArray Jaddr = new JArray();
                JArray Jremark = new JArray();
                JArray Jcount = new JArray();

                JArray JSname = new JArray();
                JArray Jfirstname = new JArray();
                JArray Jdeathname = new JArray();
                JArray JdzipCode = new JArray();
                JArray Jdcounty = new JArray();
                JArray Jddist = new JArray(); ;
                JArray Jdaddr = new JArray();
                JArray Jaddpurdue = new JArray(); ;
                JArray JaddGoldPaper = new JArray();

                nullChecked(zampname_Tag, ref JZname);
                nullChecked(zampname2_Tag, ref JZname2);
                nullChecked(zampmobile_Tag, ref JZmobile);
                nullChecked(sex_Tag, ref Jsex);
                nullChecked(birth_Tag, ref Jbirth);
                nullChecked(leapMonth_Tag, ref JleapMonth);
                nullChecked(birthtime_Tag, ref Jbirthtime);
                nullChecked(sbirth_Tag, ref Jsbirth);
                nullChecked(oversea_Tag, ref Joversea);
                nullChecked(zipCode_Tag, ref JzipCode);
                nullChecked(county_Tag, ref Jcounty);
                nullChecked(dist_Tag, ref Jdist);
                nullChecked(addr_Tag, ref Jaddr);
                nullChecked(remark_Tag, ref Jremark);
                nullChecked(count_Tag, ref Jcount);

                nullChecked(salvationname_Tag, ref JSname);
                nullChecked(firstname_Tag, ref Jfirstname);
                nullChecked(deathname_Tag, ref Jdeathname);
                nullChecked(dzipCode_Tag, ref JdzipCode);
                nullChecked(dcounty_Tag, ref Jdcounty);
                nullChecked(ddist_Tag, ref Jddist);
                nullChecked(daddr_Tag, ref Jdaddr);
                nullChecked(addpurdue_Tag, ref Jaddpurdue);
                nullChecked(GoldPaperCount_Tag, ref JaddGoldPaper);

                string postURL_Init = "Purdue_Fu_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                string AppSendback = "N";                                                           //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                AppSendback = Appcounty != "" && Appdist != "" && Appaddr != "" ? "Y" : "N";
                string Apprname = AppName;                                                          //收件人姓名
                string Apprmobile = AppMobile;                                                      //收件人電話

                ApplicantID = objLightDAC.Addapplicantinfo_purdue_Fu(
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
                        string purdueType = Jpurduetype[i].ToString();
                        string purdueString = PurdueType2String(purdueType, "8");

                        string name = JZname.Count > 0 ? JZname[i].ToString() : "";
                        string name2 = JZname2.Count > 0 ? JZname2[i].ToString() : "";
                        string zampmobile = JZmobile.Count > 0 ? JZmobile[i].ToString() : "";
                        string sex = Jsex.Count > 0 ? Jsex[i].ToString() : "善男";
                        string Birth = Jbirth.Count > 0 ? Jbirth[i].ToString() : "";
                        string LeapMonth = JleapMonth.Count > 0 ? JleapMonth[i].ToString() : "N";
                        string BirthTime = Jbirthtime.Count > 0 ? Jbirthtime[i].ToString() : "吉";
                        string sBirth = Jsbirth.Count > 0 ? Jsbirth[i].ToString() : "";
                        string oversea = Joversea.Count > 0 ? Joversea[i].ToString() : "1";
                        string zipCode = JzipCode.Count > 0 ? JzipCode[i].ToString() : "0";
                        string county = Jcounty.Count > 0 ? Jcounty[i].ToString() : "";
                        string dist = Jdist.Count > 0 ? Jdist[i].ToString() : "";
                        string addr = Jaddr.Count > 0 ? Jaddr[i].ToString() : "";
                        string address = county + dist + addr;
                        string remark = Jremark.Count > 0 ? Jremark[i].ToString() : "";
                        string count = Jcount.Count > 0 ? Jcount[i].ToString() : "0";

                        string firstname = Jfirstname.Count > 0 ? Jfirstname[i].ToString() : "";
                        string deathname = Jdeathname.Count > 0 ? Jdeathname[i].ToString() : "";
                        if (Jpurduetype[i].ToString() != "1")
                        {
                            name = JSname.Count > 0 ? JSname[i].ToString() : "";
                            zipCode = JdzipCode.Count > 0 ? JdzipCode[i].ToString() : "0";
                            county = Jdcounty.Count > 0 ? Jdcounty[i].ToString() : "";
                            dist = Jddist.Count > 0 ? Jddist[i].ToString() : "";
                            addr = Jdaddr.Count > 0 ? Jdaddr[i].ToString() : "";
                            address = county + dist + addr;
                            count = "0";
                        }

                        string addpurdue = Jaddpurdue.Count > 0 ? Jaddpurdue[i].ToString() : "0";
                        if(addpurdue == "1")
                        {
                            count = "1";
                        }
                        string addGoldPaperCount = JaddGoldPaper.Count > 0 ? JaddGoldPaper[i].ToString() : "0";

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

                        int cost = GetPurdueCost(8, purdueType);
                        int goldcount = int.TryParse(addGoldPaperCount, out goldcount) ? goldcount : 0;

                        if (purdueString != "")
                        {
                            purdueinfo = true;
                            PurdueID = objLightDAC.Addpurdue_Fu(
                                ApplicantID: ApplicantID,
                                Name: name,
                                Name2: name2,
                                Mobile: zampmobile,
                                Cost: cost,
                                Sex: sex,
                                PurdueType: purdueType,
                                PurdueString: purdueString,
                                Oversea: oversea,
                                Birth: Birth,
                                LeapMonth: LeapMonth,
                                BirthTime: BirthTime,
                                BirthMonth: birthMonth,
                                Age: age,
                                Zodiac: Zodiac,
                                sBirth: sBirth,
                                Count: 1,
                                GoldPaperCount: goldcount,
                                Remark: remark,
                                Addr: addr,
                                County: county,
                                Dist: dist,
                                ZipCode: zipCode,
                                FirstName: firstname,
                                DeathName: deathname,
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

                dtData = objLightDAC.Getpurdue_Fu_Info(applicantID, Year);

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
            //            //九玄七祖
            //            result = "超度九玄七祖";
            //            break;
            //        case "3":
            //            //亡者
            //            result = "超度指名亡者";
            //            break;
            //        case "4":
            //            //地基主
            //            result = "超度地基主";
            //            break;
            //        case "5":
            //            //冤親債主
            //            result = "解冤親債主";
            //            break;
            //        case "6":
            //            //嬰靈
            //            result = "超度嬰靈";
            //            break;
            //        case "11":
            //            //動物靈
            //            result = "超度動物靈";
            //            break;
            //    }

            //    return result;
            //}
        }
    }
}