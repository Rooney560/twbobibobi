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
    public partial class templeService_purdue_Fu : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2024/07/09 23:59";

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

                //switch (adminID)
                //{
                //    case 3:
                //        //大甲鎮瀾宮
                //        EndDate = "2023/08/20 23:59";
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
                //    Response.Write("<script>alert('親愛的大德您好\\n 西螺福興宮 2024普度活動已截止！！\\n感謝您的支持, 謝謝!');</script>");
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
                string AdminID = "8";
                string AppName = basePage.Request["Appname"];                           //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];                       //申請人電話
                string Appcounty = basePage.Request["Appcounty"];                       //申請人縣市
                string Appdist = basePage.Request["Appdist"];                           //申請人區域
                string Appaddr = basePage.Request["Appaddr"];                           //申請人地址(部分)
                string AppzipCode = basePage.Request["AppzipCode"];                     //申請人郵遞區號
                string AppSendback = "N";
                AppSendback = Appcounty != "" && Appdist != "" && Appaddr != "" ? "Y" : "N";

                string zampname_Tag = basePage.Request["zampname_Tag"];                 //祈福人姓名
                string zampname2_Tag = basePage.Request["zampname2_Tag"];               //祈福人姓名2
                string zampmobile_Tag = basePage.Request["zampmobile_Tag"];             //祈福人電話
                string birth_Tag = basePage.Request["zampbirth_Tag"];                   //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["zampleapMonth_Tag"];           //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["zampbirthtime_Tag"];           //祈福人農曆時辰
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                   //郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                     //縣市
                string dist_Tag = basePage.Request["dist_Tag"];                         //區域
                string addr_Tag = basePage.Request["addr_Tag"];                         //部分地址
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
                JArray Jbirth = new JArray();
                JArray JleapMonth = new JArray();
                JArray Jbirthtime = new JArray();
                JArray JzipCode = new JArray();
                JArray Jcounty = new JArray();
                JArray Jdist = new JArray(); ;
                JArray Jaddr = new JArray();
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
                nullChecked(birth_Tag, ref Jbirth);
                nullChecked(leapMonth_Tag, ref JleapMonth);
                nullChecked(birthtime_Tag, ref Jbirthtime);
                nullChecked(zipCode_Tag, ref JzipCode);
                nullChecked(county_Tag, ref Jcounty);
                nullChecked(dist_Tag, ref Jdist);
                nullChecked(addr_Tag, ref Jaddr);
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

                string postURL = "Purdue_Fu_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                ApplicantID = objLightDAC.addapplicantinfo_purdue_Fu(AppName, AppMobile, "0", Appcounty, Appdist, Appaddr, AppzipCode, AppSendback, "", "", 0, AdminID, postURL, Year);
                bool purdueinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string purdueString = PurdueType2String(Jpurduetype[i].ToString(), "8");

                        string name = JZname.Count > 0 ? JZname[i].ToString() : "";
                        string name2 = JZname2.Count > 0 ? JZname2[i].ToString() : "";
                        string zampmobile = JZmobile.Count > 0 ? JZmobile[i].ToString() : "";
                        string Birth = Jbirth.Count > 0 ? Jbirth[i].ToString() : "";
                        string LeapMonth = JleapMonth.Count > 0 ? JleapMonth[i].ToString() : "N";
                        string BirthTime = Jbirthtime.Count > 0 ? Jbirthtime[i].ToString() : "吉";
                        string zipCode = JzipCode.Count > 0 ? JzipCode[i].ToString() : "0";
                        string county = Jcounty.Count > 0 ? Jcounty[i].ToString() : "";
                        string dist = Jdist.Count > 0 ? Jdist[i].ToString() : "";
                        string addr = Jaddr.Count > 0 ? Jaddr[i].ToString() : "";
                        string address = county + dist + addr;
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

                        string birthMonth = string.Empty;
                        string age = "0";
                        string Zodiac = string.Empty;

                        string year = string.Empty;
                        string month = string.Empty;
                        string day = string.Empty;

                        if (Birth != "")
                        {
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

                                string Birth_ad = year + "-" + month + "-" + day;
                                LunarSolarConverter.shuxiang(int.Parse(year), ref Zodiac);
                                //age = GetAge(DateTime.Parse(Birth_ad), dtNow);
                                age = GetAge(int.Parse(year), int.Parse(month), int.Parse(day)).ToString();
                            }
                        }

                        birthMonth = birthMonth.Length < 2 ? "0" + birthMonth : birthMonth;

                        if (purdueString != "")
                        {
                            purdueinfo = true;
                            PurdueID = objLightDAC.addpurdue_Fu(ApplicantID, name, name2, zampmobile, "善男", Jpurduetype[i].ToString(), purdueString, "1", Birth, LeapMonth, BirthTime, 
                                birthMonth, age, Zodiac, count, addGoldPaperCount, addr, county, dist, zipCode, firstname, deathname, Year);
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

                dtData = objLightDAC.Getpurdue_Fu_info(applicantID, Year);

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