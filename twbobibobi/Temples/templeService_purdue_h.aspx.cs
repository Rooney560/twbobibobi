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

namespace Temple.Temples
{
    public partial class templeService_purdue_h : AjaxBasePage
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

                int adminID = a = 4;
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
                string AdminID = "4";
                string AppName = basePage.Request["Appname"];               //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];           //購買人電話
                string AppEmail = basePage.Request["AppEmail"];             //購買人信箱
                string AppBirth = basePage.Request["AppBirth"];             //購買人農曆生日
                string AppleapMonth = basePage.Request["AppleapMonth"];     //閏月 Y-是 N-否
                string Appbirthtime = basePage.Request["Appbirthtime"];     //購買人農曆時辰
                string AppsBirth = basePage.Request["AppsBirth"];           //購買人國曆生日
                string Appcounty = basePage.Request["Appcounty"];           //購買人縣市
                string Appdist = basePage.Request["Appdist"];               //購買人區域
                string Appaddr = basePage.Request["Appaddr"];               //購買人地址(部分)
                string AppzipCode = basePage.Request["AppzipCode"];         //購買人郵遞區號

                //功德主、內容
                string Merit = basePage.Request["item1"].ToString();
                string MeritText = (Merit == "1") ? basePage.Request["tp_a"].ToString() : "";

                //祝燈延壽消災
                string Life = basePage.Request["item2"].ToString();

                //四十九愿解冤釋結
                string Redress = basePage.Request["item3"].ToString();

                //冤親債主
                string Creditor = basePage.Request["item4"].ToString();

                //九玄七祖
                string Ancestor = basePage.Request["item5"].ToString();
                //超度亡者姓氏
                string AncestorLastname = (Ancestor == "1") ? basePage.Request["firstName"].ToString() : "";
                //祖先牌位縣市
                string AncestorCounty = (Ancestor == "1") ? basePage.Request["Deathcounty1"].ToString() : "";
                //祖先牌位區域
                string AncestorDist = (Ancestor == "1") ? basePage.Request["Deathdist1"].ToString() : "";
                //祖先牌位地址(部分)
                string AncestorAddr = (Ancestor == "1") ? basePage.Request["Deathaddr1"].ToString() : "";
                //祖先牌位郵遞區號
                string AncestorZipCode = (Ancestor == "1") ? basePage.Request["DeathzipCode1"].ToString() : "";
                string AncestorAddress = AncestorCounty + AncestorDist + AncestorAddr;

                //功德迴向往生者
                string Deceased = basePage.Request["item6"].ToString();
                //超度亡者姓名
                string DeceasedName = (Deceased == "1") ? basePage.Request["DeathName"].ToString() : "";
                //祖先牌位縣市
                string DeceasedCounty = (Deceased == "1") ? basePage.Request["Deathcounty2"].ToString() : "";
                //祖先牌位區域
                string DeceasedDist = (Deceased == "1") ? basePage.Request["Deathdist2"].ToString() : "";
                //祖先牌位地址(部分)
                string DeceasedAddr = (Deceased == "1") ? basePage.Request["Deathaddr2"].ToString() : "";
                //祖先牌位郵遞區號
                string DeceasedZipCode = (Deceased == "1") ? basePage.Request["DeathzipCode2"].ToString() : "";
                string DeceasedAddress = DeceasedCounty + DeceasedDist + DeceasedAddr;

                //地祇主
                string Landlord = basePage.Request["item7"].ToString();
                string LandlordNum = (Landlord == "1") ? basePage.Request["itema7"].ToString() : "";

                //嬰靈
                string Baby = basePage.Request["item8"].ToString();
                string BabyNum = (Baby == "1") ? basePage.Request["itema8"].ToString() : "";

                //動物靈
                string Animal = basePage.Request["item9"].ToString();
                string AnimalNum = (Animal == "1") ? basePage.Request["itema9"].ToString() : "";

                string Name = basePage.Request["Name"].ToString();          //祈福人姓名
                string Mobile = basePage.Request["Mobile"].ToString();      //祈福人電話
                string Sex = basePage.Request["Sex"].ToString();            //祈福人性別
                string Birth = basePage.Request["Birth"];                   //祈福人農曆生日
                string leapMonth = basePage.Request["leapMonth"];           //閏月 Y-是 N-否
                string birthtime = basePage.Request["birthtime"];           //祈福人農曆時辰
                string sBirth = basePage.Request["sBirth"];                 //祈福人國曆生日
                string oversea = basePage.Request["oversea"];               //國內-1 國外-2
                string county = basePage.Request["county"].ToString();
                string dist = basePage.Request["dist"].ToString();
                string addr = basePage.Request["addr"].ToString();
                string zipCode = basePage.Request["zipCode"].ToString();
                string Address = county + dist + addr;
                string Remark = basePage.Request["Remark"];                 //備註

                int PurdueNum = int.Parse(basePage.Request["PurdueNum"].ToString());
                int RiceNum = int.Parse(basePage.Request["RiceNum"].ToString());
                int mMoneyNum = int.Parse(basePage.Request["mMoneyNum"].ToString());

                bool item = Merit == "1" || Life == "1" || Redress == "1" || Creditor == "1" || Ancestor == "1" || Deceased == "1" || Landlord == "1" || Baby == "1" || Animal == "1" || PurdueNum > 0 || RiceNum > 0 || mMoneyNum > 0 ? true : false;

                string postURL_Init = "Purdue_h_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                string AppSendback = "N";                                                           //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                string Apprname = AppName;                                                          //收件人姓名
                string Apprmobile = AppMobile;                                                      //收件人電話

                if (IsMoblie(AppMobile))
                {
                    if (item)
                    {
                        string appbirthMonth = "0";
                        string appage = "0";
                        string appZodiac = string.Empty;
                        string appyear = string.Empty;
                        string appmonth = string.Empty;
                        string appday = string.Empty;
                        string appsyear = string.Empty;
                        string appsmonth = string.Empty;
                        string appsday = string.Empty;

                        if (AppBirth != "")
                        {
                            //農曆生日!=空白
                            GetBirthDetail(Birth, ref appbirthMonth, ref appage, ref appZodiac);

                            string appbirth = AppBirth;
                            if (appbirth.IndexOf("民國") >= 0 && appbirth.IndexOf("年") > 0 && appbirth.IndexOf("月") > 0 && appbirth.IndexOf("日") > 0)
                            {
                                int birth_roc_index = appbirth.IndexOf("民國");
                                int birth_year_index = appbirth.IndexOf("年");
                                int birth_month_index = appbirth.IndexOf("月");
                                int birth_day_index = appbirth.IndexOf("日");
                                appyear = (int.Parse(appbirth.Substring(2, birth_year_index - 2)) + 1911).ToString();
                                appmonth = appbirthMonth = CheckedDateZero(appbirth.Substring(birth_year_index + 1, birth_month_index - birth_year_index - 1), 1);
                                appday = CheckedDateZero(appbirth.Substring(birth_month_index + 1, appbirth.Length - birth_month_index - 2), 1);

                                Lunar lunar = new Lunar();
                                int.TryParse(appyear, out lunar.lunarYear);
                                int.TryParse(appmonth, out lunar.lunarMonth);
                                int.TryParse(appday, out lunar.lunarDay);

                                if (AppsBirth == "")
                                {
                                    //國曆生日=空白
                                    Solar solor = new Solar();
                                    solor = LunarSolarConverter.LunarToSolar(lunar);

                                    string appsROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                                    AppsBirth = appsROC + CheckedDateZero(solor.solarMonth.ToString(), 1) + "月" + CheckedDateZero(solor.solarDay.ToString(), 1) + "日";

                                    string appROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                    AppsBirth = appROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";
                                }
                                else
                                {
                                    //國曆生日!=空白
                                    string appsbirth = AppsBirth;
                                    if (appsbirth.IndexOf("民國") >= 0 && appsbirth.IndexOf("年") > 0 && appsbirth.IndexOf("月") > 0 && appsbirth.IndexOf("日") > 0)
                                    {
                                        int appsbirth_roc_index = appsbirth.IndexOf("民國");
                                        int appsbirth_year_index = appsbirth.IndexOf("年");
                                        int appsbirth_month_index = appsbirth.IndexOf("月");
                                        int appsbirth_day_index = appsbirth.IndexOf("日");
                                        appsyear = (int.Parse(appsbirth.Substring(2, appsbirth_year_index - 2))).ToString();
                                        appsmonth = CheckedDateZero(appsbirth.Substring(appsbirth_year_index + 1, appsbirth_month_index - appsbirth_year_index - 1), 1);
                                        appsday = CheckedDateZero(appsbirth.Substring(appsbirth_month_index + 1, appsbirth.Length - appsbirth_month_index - 2), 1);

                                        AppsBirth = "民國" + appsyear + "年" + appsmonth + "月" + appsday + "日";

                                        string appROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                        AppBirth = appROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";
                                    }
                                }
                            }
                        }
                        else
                        {
                            //農曆生日=空白
                            string appsbirth = AppsBirth;
                            if (appsbirth.IndexOf("民國") >= 0 && appsbirth.IndexOf("年") > 0 && appsbirth.IndexOf("月") > 0 && appsbirth.IndexOf("日") > 0)
                            {
                                int appsbirth_roc_index = appsbirth.IndexOf("民國");
                                int appsbirth_year_index = appsbirth.IndexOf("年");
                                int appsbirth_month_index = appsbirth.IndexOf("月");
                                int appsbirth_day_index = appsbirth.IndexOf("日");
                                appsyear = (int.Parse(appsbirth.Substring(2, appsbirth_year_index - 2)) + 1911).ToString();
                                appsmonth = CheckedDateZero(appsbirth.Substring(appsbirth_year_index + 1, appsbirth_month_index - appsbirth_year_index - 1), 1);
                                appsday = CheckedDateZero(appsbirth.Substring(appsbirth_month_index + 1, appsbirth.Length - appsbirth_month_index - 2), 1);

                                Solar solor = new Solar();
                                int.TryParse(appsyear, out solor.solarYear);
                                int.TryParse(appsmonth, out solor.solarMonth);
                                int.TryParse(appsday, out solor.solarDay);

                                Lunar lunar = new Lunar();
                                lunar = LunarSolarConverter.SolarToLunar(solor);

                                LunarSolarConverter.shuxiang(lunar.lunarYear, ref appZodiac);
                                appage = GetAge(lunar.lunarYear, lunar.lunarMonth, lunar.lunarDay).ToString();
                                appbirthMonth = CheckedDateZero(lunar.lunarMonth.ToString(), 1);

                                string appROC = lunar.lunarYear > 1911 ? "民國" + (lunar.lunarYear - 1911) + "年" : "民國" + (lunar.lunarYear) + "年";
                                AppBirth = appROC + CheckedDateZero(lunar.lunarMonth.ToString(), 1) + "月" + CheckedDateZero(lunar.lunarDay.ToString(), 1) + "日";

                                string appsROC = solor.solarYear > 1911 ? "民國" + (solor.solarYear - 1911) + "年" : "民國" + (solor.solarYear) + "年";
                                AppsBirth = appsROC + appsmonth + "月" + appsday + "日";
                            }
                        }

                        appbirthMonth = CheckedDateZero(appbirthMonth, 1);

                        ApplicantID = objLightDAC.Addapplicantinfo_purdue_h(
                            Name: AppName,
                            Mobile: AppMobile,
                            Cost: "0",
                            County: Appcounty,
                            Dist: Appdist,
                            Addr: Appaddr,
                            ZipCode: AppzipCode,
                            Birth: AppBirth,
                            LeapMonth: AppleapMonth,
                            BirthTime: Appbirthtime,
                            BirthMonth: appbirthMonth,
                            Age: appage,
                            Zodiac: appZodiac,
                            sBirth: AppsBirth,
                            Email: AppEmail,
                            Sendback: AppSendback,
                            ReceiptName: Apprname,
                            ReceiptMobile: Apprmobile,
                            Status: 0,
                            AdminID: AdminID,
                            PostURL: postURL,
                            Year: Year);

                        if (ApplicantID > 0)
                        {
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

                            int cost = objLightDAC.GetTotal_h(
                                            Merit: Merit,
                                            Life: Life,
                                            Redress: Redress,
                                            Creditor: Creditor,
                                            Ancestor: Ancestor,
                                            Deceased: Deceased,
                                            Landlord: Landlord,
                                            LandlordNum: LandlordNum,
                                            Baby: Baby,
                                            BabyNum: BabyNum,
                                            Animal: Animal,
                                            AnimalNum: AnimalNum,
                                            PurdueNum: PurdueNum.ToString(),
                                            RiceNum: RiceNum.ToString(),
                                            mMoneyNum: mMoneyNum.ToString());

                            PurdueID = objLightDAC.Addpurdue_h(
                                ApplicantID: ApplicantID, 
                                AdminID: "4", 
                                Merit: Merit, 
                                MeritText: MeritText, 
                                Life: Life, 
                                Redress: Redress, 
                                Creditor: Creditor, 
                                Ancestor: Ancestor, 
                                AncestorLastname: AncestorLastname,
                                AncestorAddress: AncestorAddress, 
                                AncestorCounty: AncestorCounty,
                                AncestorDist: AncestorDist, 
                                AncestorAddr: AncestorAddr, 
                                AncestorZipCode: AncestorZipCode, 
                                Deceased: Deceased,
                                DeceasedName: DeceasedName, 
                                DeceasedAddress: DeceasedAddress, 
                                DeceasedCounty: DeceasedCounty, 
                                DeceasedDist: DeceasedDist, 
                                DeceasedAddr: DeceasedAddr, 
                                DeceasedZipCode: DeceasedZipCode, 
                                Landlord: Landlord, 
                                LandlordNum: LandlordNum, 
                                Baby: Baby, 
                                BabyNum: BabyNum, 
                                Animal: Animal, 
                                AnimalNum: AnimalNum, 
                                PurdueType: "1", 
                                PurdueString: "贊普", 
                                Name: Name, 
                                Mobile: Mobile, 
                                Cost: cost,
                                Sex: Sex, 
                                Birth: Birth, 
                                LeapMonth: leapMonth, 
                                BirthTime: birthtime, 
                                BirthMonth: birthMonth, 
                                Age: age, 
                                Zodiac: Zodiac, 
                                sBirth: sBirth, 
                                Oversea: "1", 
                                County: county, 
                                Dist: dist, 
                                Addr: addr, 
                                ZipCode: zipCode, 
                                PurdueNum: PurdueNum, 
                                RiceNum: RiceNum, 
                                mMoneyNum: mMoneyNum, 
                                Remark: Remark, 
                                Year: Year);

                            if (ApplicantID > 0 && PurdueID > 0)
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
                    }
                    else
                    {
                        basePage.mJSonHelper.AddContent("elementId", "item1");
                    }
                }
                else
                {
                    basePage.mJSonHelper.AddContent("elementId", "m_phone");
                    basePage.mJSonHelper.AddContent("elementtype", "手機格式錯誤");
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

                string AdminID = "4";

                dtData = objLightDAC.Getpurdue_h_Info(applicantID, Year);

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

        }
    }
}