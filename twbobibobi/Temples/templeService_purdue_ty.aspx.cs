using Microsoft.Win32;
using MotoSystem.Data;
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

                int adminID = a = 14;
                this.purdue1.Visible = false;
                this.purdue2.Visible = false;
                this.purdue3.Visible = false;

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
                string AdminID = "14";
                string AppName = basePage.Request["Appname"];                               //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];                           //申請人電話

                string name_Tag = basePage.Request["name_Tag"];                             //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];                         //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];                           //祈福人農歷生日
                string leapMonth_Tag = basePage.Request["leapMonth_Tag"];                   //閏月 Y-是 N-否
                string birthtime_Tag = basePage.Request["birthtime_Tag"];                   //祈福人農曆時辰
                string zipCode_Tag = basePage.Request["zipCode_Tag"];                       //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];                         //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                             //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                             //祈福人部分地址
                string remark_Tag = basePage.Request["remark_Tag"];                         //備註
                string purduetype_Tag = basePage.Request["purduetype_Tag"];                 //普度項目
                string count_Tag = basePage.Request["count_Tag"];                           //普品、白米50台斤、白米3台斤 數量

                string purdueA_Tag = basePage.Request["purdueA_Tag"];                       //孝道功德主
                string purdueB_Tag = basePage.Request["purdueB_Tag"];                       //光明功德主
                string purdueC_Tag = basePage.Request["purdueC_Tag"];                       //發心功德主

                string purdue_deathname_Tag = basePage.Request["purdue_deathname_Tag"];     //亡者姓名
                string purdue_firstname_Tag = basePage.Request["purdue_firstname_Tag"];     //顯考(姓氏)公
                string purdue_momname_Tag = basePage.Request["purdue_momname_Tag"];         //(夫姓) 母
                string purdue_lastname_Tag = basePage.Request["purdue_lastname_Tag"];       //(名字)府君
                string purdue_reason_Tag = basePage.Request["purdue_reason_Tag"];           //超薦事由
                string purdue_licenseNum_Tag = basePage.Request["purdue_licenseNum_Tag"];   //車牌(車牌數字)
                string purdue_zipCode_Tag = basePage.Request["purdue_zipCode_Tag"];         //被超薦者郵遞區號
                string purdue_county_Tag = basePage.Request["purdue_county_Tag"];           //被超薦者縣市
                string purdue_dist_Tag = basePage.Request["purdue_dist_Tag"];               //被超薦者區域
                string purdue_addr_Tag = basePage.Request["purdue_addr_Tag"];               //被超薦者部分地址

                string purdue2_deathname_Tag = basePage.Request["purdue2_deathname_Tag"];   //亡者姓名
                string purdue2_firstname_Tag = basePage.Request["purdue2_firstname_Tag"];   //顯考(姓氏)公
                string purdue2_momname_Tag = basePage.Request["purdue2_momname_Tag"];       //(夫姓) 母
                string purdue2_lastname_Tag = basePage.Request["purdue2_lastname_Tag"];     //(名字)府君
                string purdue2_reason_Tag = basePage.Request["purdue2_reason_Tag"];         //超薦事由
                string purdue2_licenseNum_Tag = basePage.Request["purdue2_licenseNum_Tag"]; //車牌(車牌數字)
                string purdue2_zipCode_Tag = basePage.Request["purdue2_zipCode_Tag"];       //被超薦者郵遞區號
                string purdue2_county_Tag = basePage.Request["purdue2_county_Tag"];         //被超薦者縣市
                string purdue2_dist_Tag = basePage.Request["purdue2_dist_Tag"];             //被超薦者區域
                string purdue2_addr_Tag = basePage.Request["purdue2_addr_Tag"];             //被超薦者部分地址

                //string purdueB_deathname_Tag = basePage.Request["purdueB_deathname_Tag"];   //亡者姓名
                //string purdueB_firstname_Tag = basePage.Request["purdueB_firstname_Tag"];   //顯考(姓氏)公
                //string purdueB_momname_Tag = basePage.Request["purdueB_momname_Tag"];       //(夫姓) 母
                //string purdueB_lastname_Tag = basePage.Request["purdueB_lastname_Tag"];     //(名字)府君
                //string purdueB_reason_Tag = basePage.Request["purdueB_reason_Tag"];         //超薦事由
                //string purdueB_licenseNum_Tag = basePage.Request["purdueB_licenseNum_Tag"]; //車牌(車牌數字)
                //string purdueB_zipCode_Tag = basePage.Request["purdueB_zipCode_Tag"];       //被超薦者郵遞區號
                //string purdueB_county_Tag = basePage.Request["purdueB_county_Tag"];         //被超薦者縣市
                //string purdueB_dist_Tag = basePage.Request["purdueB_dist_Tag"];             //被超薦者區域
                //string purdueB_addr_Tag = basePage.Request["purdueB_addr_Tag"];             //被超薦者部分地址

                //string purdueC_deathname_Tag = basePage.Request["purdueC_deathname_Tag"];   //亡者姓名
                //string purdueC_firstname_Tag = basePage.Request["purdueC_firstname_Tag"];   //顯考(姓氏)公
                //string purdueC_momname_Tag = basePage.Request["purdueC_momname_Tag"];       //(夫姓) 母
                //string purdueC_lastname_Tag = basePage.Request["purdueC_lastname_Tag"];     //(名字)府君
                //string purdueC_reason_Tag = basePage.Request["purdueC_reason_Tag"];         //超薦事由
                //string purdueC_licenseNum_Tag = basePage.Request["purdueC_licenseNum_Tag"]; //車牌(車牌數字)
                //string purdueC_zipCode_Tag = basePage.Request["purdueC_zipCode_Tag"];       //被超薦者郵遞區號
                //string purdueC_county_Tag = basePage.Request["purdueC_county_Tag"];         //被超薦者縣市
                //string purdueC_dist_Tag = basePage.Request["purdueC_dist_Tag"];             //被超薦者區域
                //string purdueC_addr_Tag = basePage.Request["purdueC_addr_Tag"];             //被超薦者部分地址

                int listcount = int.Parse(basePage.Request["listcount"]);                   //祈福人數量


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
                JArray Jcount_Tag = JArray.Parse(count_Tag);

                JArray Jremark_Tag = new JArray();
                nullChecked(remark_Tag, ref Jremark_Tag);

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

                string postURL = "Purdue_ty_Index";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                ApplicantID = objLightDAC.addapplicantinfo_purdue_ty(AppName, AppMobile, "0", "", "", "", "0", "N", "", "", 0, AdminID, postURL, Year);
                bool purdueinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();
                        string mobile = Jmobile[i].ToString();
                        string birth = Jbirth[i].ToString();
                        string leapMonth = JleapMonth[i].ToString();
                        string birthTime = Jbirthtime[i].ToString();
                        string addr = Jaddr[i].ToString();
                        string county = Jcounty[i].ToString();
                        string dist = Jdist[i].ToString();
                        string zipCode = JzipCode[i].ToString();
                        string remark = Jremark_Tag.Count > 0 ? Jremark_Tag[i].ToString() : "";
                        string birthMonth = "0";
                        string age = "0";
                        string Zodiac = string.Empty;
                        int count = 0;
                        int count_3rice = 0;
                        int count_50rice = 0;

                        GetBirthDetail(birth, ref birthMonth, ref age, ref Zodiac);

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

                        switch (purduetype)
                        {
                            case "1":
                                purdueString = "贊普";
                                int.TryParse(Jcount_Tag[i].ToString(), out count);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth,
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, "", "", "", "", "", "", "", "", "", "", "0", "", "",
                                        "", "", "", "", "", "", "", "", "0", Year);
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
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth,
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, purdueItem, purdueA_deathname, purdueA_firstname, 
                                        purdueA_momname, purdueA_lastname, purdueA_reason, purdueA_licenseNum, purdueA_addr, purdueA_county, purdueA_dist, purdueA_zipCode, 
                                        purdueItem1, purdueC_deathname, purdueC_firstname, purdueC_momname, purdueC_lastname, purdueC_reason, purdueC_licenseNum, purdueC_addr, 
                                        purdueC_county, purdueC_dist, purdueC_zipCode, Year);
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
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth,
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, purdueItem, purdueB_deathname, purdueB_firstname, 
                                        purdueB_momname, purdueB_lastname, purdueB_reason, purdueB_licenseNum, purdueB_addr, purdueB_county, purdueB_dist, purdueB_zipCode, "", "",
                                        "", "", "", "", "", "", "", "", "0", Year);
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
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth, 
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, purdueItem, purdueC_deathname, purdueC_firstname, 
                                        purdueC_momname, purdueC_lastname, purdueC_reason, purdueC_licenseNum, purdueC_addr, purdueC_county, purdueC_dist, purdueC_zipCode, "", "",
                                        "", "", "", "", "", "", "", "", "0", Year);
                                }
                                break;
                            case "18":
                                purdueString = "白米50台斤";
                                int.TryParse(Jcount_Tag[i].ToString(), out count_50rice);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth,
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, "", "", "", "", "", "", "", "", "", "", "0", "", "",
                                        "", "", "", "", "", "", "", "", "0", Year);
                                }
                                break;
                            case "19":
                                purdueString = "白米3台斤";
                                int.TryParse(Jcount_Tag[i].ToString(), out count_3rice);

                                if (name != "")
                                {
                                    purdueinfo = true;
                                    PurdueID = objLightDAC.addpurdue_ty(ApplicantID, name, mobile, "善男", purduetype, purdueString, "1", birth, leapMonth, birthTime, birthMonth,
                                        age, Zodiac, count, count_3rice, count_50rice, addr, county, dist, zipCode, remark, "", "", "", "", "", "", "", "", "", "", "0", "", "",
                                        "", "", "", "", "", "", "", "", "0", Year);
                                }
                                break;
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

                dtData = objLightDAC.Getpurdue_ty_info(applicantID, Year);

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