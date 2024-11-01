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
    public partial class templeService_purdue_dh : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/08/21 23:59";

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

                //switch (adminID)
                //{
                //    case 3:
                //        //大甲鎮瀾宮
                //        EndDate = "2023/08/21 23:59";
                //        break;
                //    case 4:
                //        //新港奉天宮
                //        EndDate = "2023/08/14 23:59";
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
                //    Response.Write("<script>alert('親愛的大德您好\\n大甲鎮瀾宮 2023普度活動已截止！！\\n感謝您的支持, 謝謝!');</script>");
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
                string AdminID = "16";
                string AppName = basePage.Request["Appname"];                           //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];                       //申請人電話

                string name_Tag = basePage.Request["name_Tag"];                         //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                     //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];                       //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];               //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];               //祈福人農曆時辰
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                   //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                     //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                         //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                         //祈福人部分地址
                string purduetype_Tag = basePage.Request["purduetype_Tag"];             //普度項目

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
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray JleapMonth = JArray.Parse(leapMonth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
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

                string postURL = "Purdue_dh_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                ApplicantID = objLightDAC.addapplicantinfo_purdue_dh(AppName, AppMobile, "0", "", "", "", "0", "N", "", "", 0, AdminID, postURL, Year);
                bool purdueinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();

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
                        //string deathCounty = Jdeathcounty.Count > 0 && Jdeathcounty[i].ToString() != "縣市" ? Jdeathcounty[i].ToString() : "";
                        //string deathDist = Jdeathdist.Count > 0 && Jdeathdist[i].ToString() != "鄉鎮市區" ? Jdeathdist[i].ToString() : "";

                        string purdueString = PurdueType2String(Jpurduetype[i].ToString(), "16");

                        string Birth = string.Empty;
                        string birthMonth = string.Empty;
                        string age = string.Empty;
                        string Zodiac = string.Empty;
                        string deathBirth = string.Empty;
                        string deathbirthMonth = string.Empty;
                        string deathage = string.Empty;
                        string deathZodiac = string.Empty;

                        string year = string.Empty;
                        string month = string.Empty;
                        string day = string.Empty;
                        string deathyear = string.Empty;
                        string deathmonth = string.Empty;
                        string dday = string.Empty;

                        string birth = Jbirth[i].ToString();
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

                            Birth = year + "-" + month + "-" + day;
                            LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                            age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                        }

                        birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;

                        if (deathbirth != "")
                        {
                            string dbirth = deathbirth;
                            int ds1 = dbirth.IndexOf("民國");
                            int ds2 = dbirth.IndexOf("年");
                            int ds3 = dbirth.IndexOf("月");
                            int ds4 = dbirth.IndexOf("日");
                            if (dbirth.IndexOf("民國") >= 0 && dbirth.IndexOf("年") > 0 && dbirth.IndexOf("月") > 0 && dbirth.IndexOf("日") > 0)
                            {
                                int year_index = dbirth.IndexOf("年");
                                int month_index = dbirth.IndexOf("月");
                                deathyear = (int.Parse(dbirth.Substring(2, year_index - 2)) + 1911).ToString();
                                deathmonth = deathbirthMonth = dbirth.Substring(year_index + 1, month_index - year_index - 1);
                                dday = dbirth.Substring(month_index + 1, dbirth.Length - month_index - 2);

                                Birth = deathyear + "-" + deathmonth + "-" + dday;
                                LunarSolarConverter.shuxiang(int.Parse(deathyear), ref deathZodiac);
                                deathage = GetAge(int.Parse(deathyear), int.Parse(deathmonth), int.Parse(dday)).ToString();
                            }
                        }

                        deathbirthMonth = deathbirthMonth.Length < 2 ? "0" + deathbirthMonth : deathbirthMonth;

                        if (name != "")
                        {
                            purdueinfo = true;
                            PurdueID = objLightDAC.addpurdue_dh(ApplicantID, name, Jmobile[i].ToString(), "善男", Jpurduetype[i].ToString(), purdueString, "1", birth, JleapMonth[i].ToString(), 
                                Jbirthtime[i].ToString(), birthMonth, age, Zodiac, 1, Jaddr[i].ToString(), Jcounty[i].ToString(), Jdist[i].ToString(), JzipCode[i].ToString(), deathname, 
                                deathday, deathbirth, deathLeapMonth, deathBirthTime, deathbirthMonth, deathage, deathZodiac, firstname, deathAddr, deathCounty, deathDist, deathZipCode, Year);

                        }
                    }
                }

                if (ApplicantID > 0 && purdueinfo)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=2&a=" + AdminID + "&aid=" + ApplicantID + (basePage.Request["ad"] != null ? "&ad=1" : "") + (basePage.Request["twm"] != null ? "&twm=1" : ""));

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

                dtData = objLightDAC.Getpurdue_dh_info(applicantID, Year);

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