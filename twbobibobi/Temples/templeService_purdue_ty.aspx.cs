using Microsoft.Win32;
using twbobibobi.Data;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.Temples
{
    public partial class templeService_purdue_ty : AjaxBasePage
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

                int adminID = a = 14;
                this.purdue1.Visible = false;
                this.purdue2.Visible = false;
                this.purdue3.Visible = false;
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int PurdueID = 0;

            /// <summary>
            /// AjaxPageHandler：處理天赦日招財補運表單資料 - 桃園威天宮
            /// 特色：
            /// 1. 先驗證所有陣列長度是否符合 listcount，否則不建立 Applicant。
            /// 2. SafeGetValue 確保任何 JArray 超界時不會拋出例外。
            /// </summary>
            /// <param name="basePage">繼承 BasePage 的頁面</param>
            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                // 取得台北時間
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                LightDAC objLightDAC = new LightDAC(basePage);
                string Year = dtNow.Year.ToString();
                string AdminID = "14";

                string AppName = basePage.Request["Appname"];                                       //購買人姓名
                string AppMobile = basePage.Request["Appmobile"];                                   //購買人電話
                string AppEmail = basePage.Request["AppEmail"];                                     //購買人信箱

                int listcount = int.Parse(basePage.Request["listcount"]);                           //祈福人數量

                string name_Tag = basePage.Request["name_Tag"];                                     
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
                string count_Tag = basePage.Request["count_Tag"];                                   //普品、白米50台斤、白米3台斤 數量

                string purdueA_Tag = basePage.Request["purdueA_Tag"];                               //孝道功德主
                string purdueB_Tag = basePage.Request["purdueB_Tag"];                               //光明功德主
                string purdueC_Tag = basePage.Request["purdueC_Tag"];                               //發心功德主

                string purdue_deathname_Tag = basePage.Request["purdue_deathname_Tag"];             //亡者姓名
                string purdue_firstname_Tag = basePage.Request["purdue_firstname_Tag"];             //顯考(姓氏)公
                string purdue_momname_Tag = basePage.Request["purdue_momname_Tag"];                 //(夫姓) 母
                string purdue_lastname_Tag = basePage.Request["purdue_lastname_Tag"];               //(名字)府君
                string purdue_reason_Tag = basePage.Request["purdue_reason_Tag"];                   //超薦事由
                string purdue_licenseNum_Tag = basePage.Request["purdue_licenseNum_Tag"];           //車牌(車牌數字)
                string purdue_zipCode_Tag = basePage.Request["purdue_zipCode_Tag"];                 //被超薦者郵遞區號
                string purdue_county_Tag = basePage.Request["purdue_county_Tag"];                   //被超薦者縣市
                string purdue_dist_Tag = basePage.Request["purdue_dist_Tag"];                       //被超薦者區域
                string purdue_addr_Tag = basePage.Request["purdue_addr_Tag"];                       //被超薦者部分地址

                string purdue2_deathname_Tag = basePage.Request["purdue2_deathname_Tag"];           //亡者姓名
                string purdue2_firstname_Tag = basePage.Request["purdue2_firstname_Tag"];           //顯考(姓氏)公
                string purdue2_momname_Tag = basePage.Request["purdue2_momname_Tag"];               //(夫姓) 母
                string purdue2_lastname_Tag = basePage.Request["purdue2_lastname_Tag"];             //(名字)府君
                string purdue2_reason_Tag = basePage.Request["purdue2_reason_Tag"];                 //超薦事由
                string purdue2_licenseNum_Tag = basePage.Request["purdue2_licenseNum_Tag"];         //車牌(車牌數字)
                string purdue2_zipCode_Tag = basePage.Request["purdue2_zipCode_Tag"];               //被超薦者郵遞區號
                string purdue2_county_Tag = basePage.Request["purdue2_county_Tag"];                 //被超薦者縣市
                string purdue2_dist_Tag = basePage.Request["purdue2_dist_Tag"];                     //被超薦者區域
                string purdue2_addr_Tag = basePage.Request["purdue2_addr_Tag"];                     //被超薦者部分地址

                //string purdueB_deathname_Tag = basePage.Request["purdueB_deathname_Tag"];         //亡者姓名
                //string purdueB_firstname_Tag = basePage.Request["purdueB_firstname_Tag"];         //顯考(姓氏)公
                //string purdueB_momname_Tag = basePage.Request["purdueB_momname_Tag"];             //(夫姓) 母
                //string purdueB_lastname_Tag = basePage.Request["purdueB_lastname_Tag"];           //(名字)府君
                //string purdueB_reason_Tag = basePage.Request["purdueB_reason_Tag"];               //超薦事由
                //string purdueB_licenseNum_Tag = basePage.Request["purdueB_licenseNum_Tag"];       //車牌(車牌數字)
                //string purdueB_zipCode_Tag = basePage.Request["purdueB_zipCode_Tag"];             //被超薦者郵遞區號
                //string purdueB_county_Tag = basePage.Request["purdueB_county_Tag"];               //被超薦者縣市
                //string purdueB_dist_Tag = basePage.Request["purdueB_dist_Tag"];                   //被超薦者區域
                //string purdueB_addr_Tag = basePage.Request["purdueB_addr_Tag"];                   //被超薦者部分地址

                //string purdueC_deathname_Tag = basePage.Request["purdueC_deathname_Tag"];         //亡者姓名
                //string purdueC_firstname_Tag = basePage.Request["purdueC_firstname_Tag"];         //顯考(姓氏)公
                //string purdueC_momname_Tag = basePage.Request["purdueC_momname_Tag"];             //(夫姓) 母
                //string purdueC_lastname_Tag = basePage.Request["purdueC_lastname_Tag"];           //(名字)府君
                //string purdueC_reason_Tag = basePage.Request["purdueC_reason_Tag"];               //超薦事由
                //string purdueC_licenseNum_Tag = basePage.Request["purdueC_licenseNum_Tag"];       //車牌(車牌數字)
                //string purdueC_zipCode_Tag = basePage.Request["purdueC_zipCode_Tag"];             //被超薦者郵遞區號
                //string purdueC_county_Tag = basePage.Request["purdueC_county_Tag"];               //被超薦者縣市
                //string purdueC_dist_Tag = basePage.Request["purdueC_dist_Tag"];                   //被超薦者區域
                //string purdueC_addr_Tag = basePage.Request["purdueC_addr_Tag"];                   //被超薦者部分地址


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
                JArray Jcount_Tag = JArray.Parse(count_Tag);

                JArray JpurdueA_Tag = new JArray();
                JArray JpurdueB_Tag = new JArray();
                JArray JpurdueC_Tag = new JArray();

                JArray Jpurdue_deathname = new JArray();
                JArray Jpurdue_firstname = new JArray();
                JArray Jpurdue_momname = new JArray();
                JArray Jpurdue_lastname = new JArray();
                JArray Jpurdue_reason = new JArray();
                JArray Jpurdue_licenseNum = new JArray();
                JArray Jpurdue_zipCode = new JArray();
                JArray Jpurdue_county = new JArray();
                JArray Jpurdue_dist = new JArray();
                JArray Jpurdue_addr = new JArray();

                JArray Jpurdue2_deathname = new JArray();
                JArray Jpurdue2_firstname = new JArray();
                JArray Jpurdue2_momname = new JArray();
                JArray Jpurdue2_lastname = new JArray();
                JArray Jpurdue2_reason = new JArray();
                JArray Jpurdue2_licenseNum = new JArray();
                JArray Jpurdue2_zipCode = new JArray();
                JArray Jpurdue2_county = new JArray();
                JArray Jpurdue2_dist = new JArray();
                JArray Jpurdue2_addr = new JArray();

                nullChecked(purdueA_Tag, ref JpurdueA_Tag);
                nullChecked(purdueB_Tag, ref JpurdueB_Tag);
                nullChecked(purdueC_Tag, ref JpurdueC_Tag);
                nullChecked(purdue_deathname_Tag, ref Jpurdue_deathname);
                nullChecked(purdue_firstname_Tag, ref Jpurdue_firstname);
                nullChecked(purdue_momname_Tag, ref Jpurdue_momname);
                nullChecked(purdue_lastname_Tag, ref Jpurdue_lastname);
                nullChecked(purdue_reason_Tag, ref Jpurdue_reason);
                nullChecked(purdue_licenseNum_Tag, ref Jpurdue_licenseNum);
                nullChecked(purdue_zipCode_Tag, ref Jpurdue_zipCode);
                nullChecked(purdue_county_Tag, ref Jpurdue_county);
                nullChecked(purdue_dist_Tag, ref Jpurdue_dist);
                nullChecked(purdue_addr_Tag, ref Jpurdue_addr);
                nullChecked(purdue2_deathname_Tag, ref Jpurdue2_deathname);
                nullChecked(purdue2_firstname_Tag, ref Jpurdue2_firstname);
                nullChecked(purdue2_momname_Tag, ref Jpurdue2_momname);
                nullChecked(purdue2_lastname_Tag, ref Jpurdue2_lastname);
                nullChecked(purdue2_reason_Tag, ref Jpurdue2_reason);
                nullChecked(purdue2_licenseNum_Tag, ref Jpurdue2_licenseNum);
                nullChecked(purdue2_zipCode_Tag, ref Jpurdue2_zipCode);
                nullChecked(purdue2_county_Tag, ref Jpurdue2_county);
                nullChecked(purdue2_dist_Tag, ref Jpurdue2_dist);
                nullChecked(purdue2_addr_Tag, ref Jpurdue2_addr);

                // ==================== listcount 與各 JSON 陣列安全檢查 ====================
                // 目的：避免因為前端傳入的 listcount 與陣列實際長度不一致，導致後續存取 JArray[i] 時拋出 IndexOutOfRangeException
                // 作法：逐一檢查所有必填欄位的 JArray 長度是否小於 listcount
                if (listcount > Jname.Count ||    // 祈福人姓名
                    listcount > Jmobile.Count ||    // 祈福人電話
                    listcount > Jsex.Count ||    // 祈福人性別
                    listcount > Jbirth.Count ||    // 祈福人農曆生日
                    listcount > JleapMonth.Count ||    // 閏月 Y-是 N-否
                    listcount > Jbirthtime.Count ||    // 祈福人農曆時辰
                    listcount > Jsbirth.Count ||    // 祈福人國曆生日
                    listcount > Joversea.Count ||    // 國內-1 國外-2
                    listcount > JzipCode.Count ||    // 祈福人郵遞區號
                    listcount > Jcounty.Count ||    // 祈福人縣市
                    listcount > Jdist.Count ||    // 祈福人區域
                    listcount > Jaddr.Count ||    // 祈福人部分地址
                    listcount > Jremark.Count ||    // 備註
                    listcount > Jpurduetype.Count ||    // 普度項目
                    listcount > Jcount_Tag.Count)       // 數量
                {
                    basePage.mJSonHelper.AddContent("StatusCode", -1);                  // 狀態碼：失敗
                    basePage.mJSonHelper.AddContent("Message", "祈福人數量與傳入資料不一致"); // 錯誤訊息
                    return;                                                             // 中止執行，避免後續取值拋出例外
                }

                string postURL_Init = "Purdue_ty_Index";

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string postURL = GetRequestURL(url, postURL_Init);

                string AppSendback = "N";                                                           //寄送方式 N-不寄回(會轉送給弱勢團體) Y-寄回(加收運費120元)
                string Apprname = AppName;                                                          //收件人姓名
                string Apprmobile = AppMobile;                                                      //收件人電話
                string Appcounty = "";                                                              //購買人地址縣市
                string Appdist = "";                                                                //購買人地址區域
                string Appaddr = "";                                                                //購買人地址部分地址
                string AppzipCode = "";                                                             //購買人地址郵遞區號

                ApplicantID = objLightDAC.Addapplicantinfo_purdue_ty(
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
                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string oversea = Joversea[i].ToString();
                        string remark = Jremark.Count > 0 ? Jremark[i].ToString() : "";
                        int count = 0;
                        int count_3rice = 0;
                        int count_50rice = 0;

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

                        string purduetype = Jpurduetype[i].ToString();
                        string purdueString = "贊普";
                        string purdueItem = string.Empty;

                        string purdueA_deathname = string.Empty;
                        string purdueA_firstname = string.Empty;
                        string purdueA_momname = string.Empty;
                        string purdueA_lastname = string.Empty;
                        string purdueA_reason = string.Empty;
                        string purdueA_licenseNum = string.Empty;
                        string purdueA_addr = string.Empty;
                        string purdueA_county = string.Empty;
                        string purdueA_dist = string.Empty;
                        string purdueA_zipCode = string.Empty;
                        string purdueA_address = string.Empty;

                        string purdueB_deathname = string.Empty;
                        string purdueB_firstname = string.Empty;
                        string purdueB_momname = string.Empty;
                        string purdueB_lastname = string.Empty;
                        string purdueB_reason = string.Empty;
                        string purdueB_licenseNum = string.Empty;
                        string purdueB_addr = string.Empty;
                        string purdueB_county = string.Empty;
                        string purdueB_dist = string.Empty;
                        string purdueB_zipCode = string.Empty;
                        string purdueB_address = string.Empty;

                        string purdueC_deathname = string.Empty;
                        string purdueC_firstname = string.Empty;
                        string purdueC_momname = string.Empty;
                        string purdueC_lastname = string.Empty;
                        string purdueC_reason = string.Empty;
                        string purdueC_licenseNum = string.Empty;
                        string purdueC_addr = string.Empty;
                        string purdueC_county = string.Empty;
                        string purdueC_dist = string.Empty;
                        string purdueC_zipCode = string.Empty;
                        string purdueC_address = string.Empty;

                        int cost = GetPurdueCost(14, purduetype);

                        switch (purduetype)
                        {
                            case "1":
                                purdueString = "贊普";
                                int.TryParse(Jcount_Tag[i].ToString(), out count);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: "",
                                        DeathName: "",
                                        FirstName: "",
                                        MomName: "",
                                        LastName: "",
                                        Reason: "",
                                        LicenseNum: "",
                                        DeathAddr: "",
                                        DeathCounty: "",
                                        DeathDist: "",
                                        DeathZipCode: "0",
                                        PurdueItem1: "",
                                        DeathName1: "",
                                        FirstName1: "",
                                        MomName1: "",
                                        LastName1: "",
                                        Reason1: "",
                                        LicenseNum1: "",
                                        DeathAddr1: "",
                                        DeathCounty1: "",
                                        DeathDist1: "",
                                        DeathZipCode1: "0",
                                        Year: Year);
                                }
                                break;
                            case "14":
                                //孝道功德主
                                count = 1;
                                purdueString = "孝道功德主";
                                purdueItem = "花雕木牌-" + JpurdueA_Tag[i].ToString();

                                purdueA_deathname = Jpurdue_deathname.Count > 0 ? Jpurdue_deathname[i].ToString() : "";
                                purdueA_firstname = Jpurdue_firstname.Count > 0 ? Jpurdue_firstname[i].ToString() : "";
                                purdueA_momname = Jpurdue_momname.Count > 0 ? Jpurdue_momname[i].ToString() : "";
                                purdueA_lastname = Jpurdue_lastname.Count > 0 ? Jpurdue_lastname[i].ToString() : "";
                                purdueA_reason = Jpurdue_reason.Count > 0 ? Jpurdue_reason[i].ToString() : "";
                                purdueA_licenseNum = Jpurdue_licenseNum.Count > 0 ? Jpurdue_licenseNum[i].ToString() : "";

                                purdueA_addr = Jpurdue_addr.Count > 0 ? Jpurdue_addr[i].ToString() : "";
                                purdueA_county = Jpurdue_county.Count > 0 ? Jpurdue_county[i].ToString() : "";
                                purdueA_dist = Jpurdue_dist.Count > 0 ? Jpurdue_dist[i].ToString() : "";
                                purdueA_zipCode = Jpurdue_zipCode.Count > 0 ? Jpurdue_zipCode[i].ToString() : "0";
                                purdueA_address = purdueA_county != "" ? (purdueA_county + purdueA_dist + purdueA_addr) : "";
                                if (purdueA_addr == "")
                                {
                                    purdueA_address = purdueA_county = purdueA_dist = purdueA_addr;
                                    purdueA_zipCode = "0";
                                }

                                string purdueItem1 = "超薦中牌-" + JpurdueC_Tag[i].ToString();

                                purdueC_deathname = Jpurdue2_deathname.Count > 0 ? Jpurdue2_deathname[i].ToString() : "";
                                purdueC_firstname = Jpurdue2_firstname.Count > 0 ? Jpurdue2_firstname[i].ToString() : "";
                                purdueC_momname = Jpurdue2_momname.Count > 0 ? Jpurdue2_momname[i].ToString() : "";
                                purdueC_lastname = Jpurdue2_lastname.Count > 0 ? Jpurdue2_lastname[i].ToString() : "";
                                purdueC_reason = Jpurdue2_reason.Count > 0 ? Jpurdue2_reason[i].ToString() : "";
                                purdueC_licenseNum = Jpurdue2_licenseNum.Count > 0 ? Jpurdue2_licenseNum[i].ToString() : "";

                                purdueC_addr = Jpurdue2_addr.Count > 0 ? Jpurdue2_addr[i].ToString() : "";
                                purdueC_county = Jpurdue2_county.Count > 0 ? Jpurdue2_county[i].ToString() : "";
                                purdueC_dist = Jpurdue2_dist.Count > 0 ? Jpurdue2_dist[i].ToString() : "";
                                purdueC_zipCode = Jpurdue2_zipCode.Count > 0 ? Jpurdue2_zipCode[i].ToString() : "0";
                                purdueC_address = purdueC_county != "" ? (purdueC_county + purdueC_dist + purdueC_addr) : "";
                                if (purdueC_addr == "")
                                {
                                    purdueC_address = purdueC_county = purdueC_dist = purdueC_addr;
                                    purdueC_zipCode = "0";
                                }

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: purdueItem,
                                        DeathName: purdueA_deathname,
                                        FirstName: purdueA_firstname,
                                        MomName: purdueA_momname,
                                        LastName: purdueA_lastname,
                                        Reason: purdueA_reason,
                                        LicenseNum: purdueA_licenseNum,
                                        DeathAddr: purdueA_addr,
                                        DeathCounty: purdueA_county,
                                        DeathDist: purdueA_dist,
                                        DeathZipCode: purdueA_zipCode,
                                        PurdueItem1: purdueItem1,
                                        DeathName1: purdueC_deathname,
                                        FirstName1: purdueC_firstname,
                                        MomName1: purdueC_momname,
                                        LastName1: purdueC_lastname,
                                        Reason1: purdueC_reason,
                                        LicenseNum1: purdueC_licenseNum,
                                        DeathAddr1: purdueC_addr,
                                        DeathCounty1: purdueC_county,
                                        DeathDist1: purdueC_dist,
                                        DeathZipCode1: purdueC_zipCode,
                                        Year: Year); 
                                }
                                break;
                            case "15":
                                //光明功德主
                                count = 1;
                                purdueString = "光明功德主";
                                purdueItem = "超薦大牌-" + JpurdueB_Tag[i].ToString();

                                purdueB_deathname = Jpurdue_deathname.Count > 0 ? Jpurdue_deathname[i].ToString() : "";
                                purdueB_firstname = Jpurdue_firstname.Count > 0 ? Jpurdue_firstname[i].ToString() : "";
                                purdueB_momname = Jpurdue_momname.Count > 0 ? Jpurdue_momname[i].ToString() : "";
                                purdueB_lastname = Jpurdue_lastname.Count > 0 ? Jpurdue_lastname[i].ToString() : "";
                                purdueB_reason = Jpurdue_reason.Count > 0 ? Jpurdue_reason[i].ToString() : "";
                                purdueB_licenseNum = Jpurdue_licenseNum.Count > 0 ? Jpurdue_licenseNum[i].ToString() : "";

                                purdueB_addr = Jpurdue_addr.Count > 0 ? Jpurdue_addr[i].ToString() : "";
                                purdueB_county = Jpurdue_county.Count > 0 ? Jpurdue_county[i].ToString() : "";
                                purdueB_dist = Jpurdue_dist.Count > 0 ? Jpurdue_dist[i].ToString() : "";
                                purdueB_zipCode = Jpurdue_zipCode.Count > 0 ? Jpurdue_zipCode[i].ToString() : "0";
                                purdueB_address = purdueB_county != "" ? (purdueB_county + purdueB_dist + purdueB_addr) : "";
                                if (purdueB_addr == "")
                                {
                                    purdueB_address = purdueB_county = purdueB_dist = purdueB_addr;
                                    purdueB_zipCode = "0";
                                }

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: purdueItem,
                                        DeathName: purdueB_deathname,
                                        FirstName: purdueB_firstname,
                                        MomName: purdueB_momname,
                                        LastName: purdueB_lastname,
                                        Reason: purdueB_reason,
                                        LicenseNum: purdueB_licenseNum,
                                        DeathAddr: purdueB_addr,
                                        DeathCounty: purdueB_county,
                                        DeathDist: purdueB_dist,
                                        DeathZipCode: purdueB_zipCode,
                                        PurdueItem1: "",
                                        DeathName1: "",
                                        FirstName1: "",
                                        MomName1: "",
                                        LastName1: "",
                                        Reason1: "",
                                        LicenseNum1: "",
                                        DeathAddr1: "",
                                        DeathCounty1: "",
                                        DeathDist1: "",
                                        DeathZipCode1: "0",
                                        Year: Year);
                                }
                                break;
                            case "16":
                                //發心功德主
                                count = 1;
                                purdueString = "發心功德主";
                                purdueItem = "超薦中牌-" + JpurdueC_Tag[i].ToString();

                                purdueC_deathname = Jpurdue_deathname.Count > 0 ? Jpurdue_deathname[i].ToString() : "";
                                purdueC_firstname = Jpurdue_firstname.Count > 0 ? Jpurdue_firstname[i].ToString() : "";
                                purdueC_momname = Jpurdue_momname.Count > 0 ? Jpurdue_momname[i].ToString() : "";
                                purdueC_lastname = Jpurdue_lastname.Count > 0 ? Jpurdue_lastname[i].ToString() : "";
                                purdueC_reason = Jpurdue_reason.Count > 0 ? Jpurdue_reason[i].ToString() : "";
                                purdueC_licenseNum = Jpurdue_licenseNum.Count > 0 ? Jpurdue_licenseNum[i].ToString() : "";

                                purdueC_addr = Jpurdue_addr.Count > 0 ? Jpurdue_addr[i].ToString() : "";
                                purdueC_county = Jpurdue_county.Count > 0 ? Jpurdue_county[i].ToString() : "";
                                purdueC_dist = Jpurdue_dist.Count > 0 ? Jpurdue_dist[i].ToString() : "";
                                purdueC_zipCode = Jpurdue_zipCode.Count > 0 ? Jpurdue_zipCode[i].ToString() : "0";
                                purdueC_address = purdueC_county != "" ? (purdueC_county + purdueC_dist + purdueC_addr) : "";
                                if (purdueC_addr == "")
                                {
                                    purdueC_address = purdueC_county = purdueC_dist = purdueC_addr;
                                    purdueC_zipCode = "0";
                                }

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: purdueItem,
                                        DeathName: purdueC_deathname,
                                        FirstName: purdueC_firstname,
                                        MomName: purdueC_momname,
                                        LastName: purdueC_lastname,
                                        Reason: purdueC_reason,
                                        LicenseNum: purdueC_licenseNum,
                                        DeathAddr: purdueC_addr,
                                        DeathCounty: purdueC_county,
                                        DeathDist: purdueC_dist,
                                        DeathZipCode: purdueC_zipCode,
                                        PurdueItem1: "",
                                        DeathName1: "",
                                        FirstName1: "",
                                        MomName1: "",
                                        LastName1: "",
                                        Reason1: "",
                                        LicenseNum1: "",
                                        DeathAddr1: "",
                                        DeathCounty1: "",
                                        DeathDist1: "",
                                        DeathZipCode1: "0",
                                        Year: Year);
                                }
                                break;
                            case "18":
                                purdueString = "白米50台斤";
                                int.TryParse(Jcount_Tag[i].ToString(), out count_50rice);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: "",
                                        DeathName: "",
                                        FirstName: "",
                                        MomName: "",
                                        LastName: "",
                                        Reason: "",
                                        LicenseNum: "",
                                        DeathAddr: "",
                                        DeathCounty: "",
                                        DeathDist: "",
                                        DeathZipCode: "0",
                                        PurdueItem1: "",
                                        DeathName1: "",
                                        FirstName1: "",
                                        MomName1: "",
                                        LastName1: "",
                                        Reason1: "",
                                        LicenseNum1: "",
                                        DeathAddr1: "",
                                        DeathCounty1: "",
                                        DeathDist1: "",
                                        DeathZipCode1: "0",
                                        Year: Year);
                                }
                                break;
                            case "19":
                                purdueString = "白米3台斤";
                                int.TryParse(Jcount_Tag[i].ToString(), out count_3rice);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.Addpurdue_ty(
                                        ApplicantID: ApplicantID,
                                        Name: name,
                                        Mobile: mobile,
                                        Cost: cost,
                                        Sex: sex,
                                        PurdueType: purduetype,
                                        PurdueString: purdueString,
                                        Oversea: oversea,
                                        Birth: Birth,
                                        LeapMonth: leapMonth,
                                        BirthTime: birthTime,
                                        BirthMonth: birthMonth,
                                        Age: age,
                                        Zodiac: Zodiac,
                                        sBirth: sBirth,
                                        Count: count,
                                        Count_3rice: count_3rice,
                                        Count_50rice: count_50rice,
                                        Remark: remark,
                                        Addr: addr,
                                        County: county,
                                        Dist: dist,
                                        ZipCode: zipCode,
                                        PurdueItem: "",
                                        DeathName: "",
                                        FirstName: "",
                                        MomName: "",
                                        LastName: "",
                                        Reason: "",
                                        LicenseNum: "",
                                        DeathAddr: "",
                                        DeathCounty: "",
                                        DeathDist: "",
                                        DeathZipCode: "0",
                                        PurdueItem1: "",
                                        DeathName1: "",
                                        FirstName1: "",
                                        MomName1: "",
                                        LastName1: "",
                                        Reason1: "",
                                        LicenseNum1: "",
                                        DeathAddr1: "",
                                        DeathCounty1: "",
                                        DeathDist1: "",
                                        DeathZipCode1: "0",
                                        Year: Year);
                                }
                                break;
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

                dtData = objLightDAC.Getpurdue_ty_Info(applicantID, Year);

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