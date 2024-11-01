using MotoSystem.Data;
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
        public string EndDate = "2023/07/09 23:59";

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

                //switch (adminID)
                //{
                //    case 3:
                //        //大甲鎮瀾宮
                //        EndDate = "2023/08/20 23:59";
                //        break;
                //    case 4:
                //        //新港奉天宮
                //        EndDate = "2024/08/14 23:59";
                //        break;
                //    case 6:
                //        //北港武德宮
                //        EndDate = "2023/08/22 23:59";
                //        break;
                //    case 8:
                //        //西螺福興宮
                //        EndDate = "2023/08/31 23:59";
                //        break;
                //    case 9:
                //        //桃園大廟景福宮
                //        EndDate = "2023/08/25 23:59";
                //        break;
                //    case 10:
                //        //台南正宗鹿耳門聖母廟
                //        EndDate = "2023/08/20 23:59";
                //        break;
                //}

                //if (dtNow >= DateTime.Parse(EndDate))
                //{
                //    Response.Write("<script>alert('親愛的大德您好\\n新港奉天宮 2024普度活動已截止！！\\n感謝您的支持, 謝謝!');</script>");
                //}
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
                string AppName = basePage.Request["Appname"];               //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];           //申請人電話
                string AppEmail = basePage.Request["AppEmail"];             //申請人信箱
                string AppBirth = basePage.Request["AppBirth"];             //申請人農歷生日
                string AppleapMonth = basePage.Request["AppleapMonth"];     //閏月 Y-是 N-否
                string Appbirthtime = basePage.Request["Appbirthtime"];     //申請人農曆時辰
                string Appcounty = basePage.Request["Appcounty"];           //申請人縣市
                string Appdist = basePage.Request["Appdist"];               //申請人區域
                string Appaddr = basePage.Request["Appaddr"];               //申請人地址(部分)
                string AppzipCode = basePage.Request["AppzipCode"];         //申請人郵遞區號

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
                string Birth = basePage.Request["Birth"];                   //農歷生日
                string leapMonth = basePage.Request["leapMonth"];           //閏月 Y-是 N-否
                string birthtime = basePage.Request["birthtime"];           //祈福人農曆時辰
                string county = basePage.Request["county"].ToString();
                string dist = basePage.Request["dist"].ToString();
                string addr = basePage.Request["addr"].ToString();
                string zipCode = basePage.Request["zipCode"].ToString();
                string Address = county + dist + addr;

                int PurdueNum = int.Parse(basePage.Request["PurdueNum"].ToString());
                int RiceNum = int.Parse(basePage.Request["RiceNum"].ToString());
                int mMoneyNum = int.Parse(basePage.Request["mMoneyNum"].ToString());

                string Remark = basePage.Request["m_btxt"].ToString();

                bool item = Merit == "1" || Life == "1" || Redress == "1" || Creditor == "1" || Ancestor == "1" || Deceased == "1" || Landlord == "1" || Baby == "1" || Animal == "1" || PurdueNum > 0 || RiceNum > 0 || mMoneyNum > 0 ? true : false;

                string postURL = "Purdue_h_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                if (IsMoblie(AppMobile))
                {
                    if (item)
                    {
                        string appBirth = string.Empty;
                        string appbirthMonth = string.Empty;
                        string appage = string.Empty;
                        string appZodiac = string.Empty;

                        string appyear = string.Empty;
                        string appmonth = string.Empty;
                        string appday = string.Empty;

                        string appbirth = AppBirth;
                        int apps1 = appbirth.IndexOf("民國");
                        int apps2 = appbirth.IndexOf("年");
                        int apps3 = appbirth.IndexOf("月");
                        int apps4 = appbirth.IndexOf("日");
                        if (appbirth.IndexOf("民國") >= 0 && appbirth.IndexOf("年") > 0 && appbirth.IndexOf("月") > 0 && appbirth.IndexOf("日") > 0)
                        {
                            int year_index = appbirth.IndexOf("年");
                            int month_index = appbirth.IndexOf("月");
                            appyear = (int.Parse(appbirth.Substring(2, year_index - 2)) + 1911).ToString();
                            appmonth = appbirthMonth = appbirth.Substring(year_index + 1, month_index - year_index - 1);
                            appday = appbirth.Substring(month_index + 1, appbirth.Length - month_index - 2);

                            appBirth = appyear + "-" + appmonth + "-" + appday;
                            LunarSolarConverter.shuxiang(int.Parse(appyear), ref appZodiac);
                            appage = GetAge(int.Parse(appyear), int.Parse(appmonth), int.Parse(appday)).ToString();
                        }

                        appbirthMonth = appbirthMonth.Length < 2 ? "0" + appbirthMonth : appbirthMonth;

                        //ApplicantID = objLightDAC.addapplicantinfo_Purdue_h(AppName, AppMobile, Birth, AppEmail, "4", Appcounty, Appdist, Appaddr, AppzipCode, postURL);
                        ApplicantID = objLightDAC.addapplicantinfo_purdue_h(AppName, AppMobile, "0", Appcounty, Appdist, Appaddr, AppzipCode, AppBirth, AppleapMonth, Appbirthtime, 
                            appbirthMonth, appage, appZodiac, AppEmail, "N", "", "", 0, AdminID, postURL, Year);

                        if (ApplicantID > 0)
                        {
                            //PurdueID = objLightDAC.addpurdue_h(ApplicantID, "4", Merit, MeritText, Life, Redress, Creditor, Ancestor, AncestorLastname, AncestorAddress, AncestorCounty, AncestorDist, AncestorAddr, AncestorZipCode, Deceased, DeceasedName, DeceasedAddress, DeceasedCounty, DeceasedDist, DeceasedAddr, DeceasedZipCode, Landlord, LandlordNum,
                            //    Baby, BabyNum, Animal, AnimalNum, Name, "1", "贊普", Address, county, dist, addr, zipCode, PurdueNum, RiceNum, mMoneyNum, Remark);

                            string cBirth = string.Empty;
                            string birthMonth = string.Empty;
                            string age = string.Empty;
                            string Zodiac = string.Empty;

                            string year = string.Empty;
                            string month = string.Empty;
                            string day = string.Empty;

                            string birth = Birth;
                            int s1 = birth.IndexOf("民國");
                            int s2 = birth.IndexOf("年");
                            int s3 = birth.IndexOf("月");
                            int s4 = birth.IndexOf("日");
                            if (birth.IndexOf("民國") >= 0 && birth.IndexOf("年") > 0 && birth.IndexOf("月") > 0 && birth.IndexOf("日") > 0)
                            {
                                int year_index = birth.IndexOf("年");
                                int month_index = birth.IndexOf("月");
                                year = (int.Parse(birth.Substring(2, year_index - 2)) + 1911).ToString();
                                month = birthMonth = birth.Substring(year_index + 1, month_index - year_index - 1);
                                day = birth.Substring(month_index + 1, birth.Length - month_index - 2);

                                cBirth = year + "-" + month + "-" + day;
                                LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                                age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                            }

                            birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;
                            PurdueID = objLightDAC.addpurdue_h(ApplicantID, "4", Merit, MeritText, Life, Redress, Creditor, Ancestor, AncestorLastname, AncestorAddress, AncestorCounty, 
                                AncestorDist, AncestorAddr, AncestorZipCode, Deceased, DeceasedName, DeceasedAddress, DeceasedCounty, DeceasedDist, DeceasedAddr, DeceasedZipCode, Landlord, 
                                LandlordNum, Baby, BabyNum, Animal, AnimalNum, "1", "贊普", Name, birth, leapMonth, birthtime, birthMonth, age, Zodiac, "1", county, dist, addr, zipCode, 
                                PurdueNum, RiceNum, mMoneyNum, Remark, Year);

                            if (ApplicantID > 0 && PurdueID > 0)
                            {
                                basePage.mJSonHelper.AddContent("StatusCode", 1);
                                basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=2&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));
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

                dtData = objLightDAC.Getpurdue_h_info(applicantID, Year);

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