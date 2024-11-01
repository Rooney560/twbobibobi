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
    public partial class templeService_marriagelights_Luer : AjaxBasePage
    {
        public int aid = 0;
        public int a = 0;
        public string EndDate = "2023/11/30 23:59";

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

                int adminID = a = 10;

                //if (dtNow >= DateTime.Parse(EndDate))
                //{
                //    Response.Write("<script>alert('親愛的大德您好\\n台南正統鹿耳門聖母廟 2023姻緣燈活動已截止！！\\n感謝您的支持, 謝謝!');</script>");
                //}
            }
        }
        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int marriagelightsID = 0;

            public void gotochecked(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                string AdminID = "10";
                string AppName = basePage.Request["Appname"];                   //申請人姓名
                string AppMobile = basePage.Request["Appmobile"];               //申請人電話

                string name_Tag = basePage.Request["name_Tag"];                 //祈福人姓名
                string mobile_Tag = basePage.Request["mobile_Tag"];             //祈福人電話
                string birth_Tag = basePage.Request["birth_Tag"];               //祈福人農歷生日
                string birthtime_Tag = basePage.Request["birthtime_Tag"];       //祈福人農曆時辰
                string zipCode_Tag = basePage.Request["zipCode_Tag"];           //祈福人郵遞區號
                string county_Tag = basePage.Request["county_Tag"];             //祈福人縣市
                string dist_Tag = basePage.Request["dist_Tag"];                 //祈福人區域
                string addr_Tag = basePage.Request["addr_Tag"];                 //祈福人部分地址
                string msg_Tag = basePage.Request["msg_Tag"];                   //祈福小語

                int listcount = int.Parse(basePage.Request["listcount"]);       //祈福人數量


                JArray Jname = JArray.Parse(name_Tag);
                JArray Jmobile = JArray.Parse(mobile_Tag);
                JArray Jbirth = JArray.Parse(birth_Tag);
                JArray Jbirthtime = JArray.Parse(birthtime_Tag);
                JArray JzipCode = JArray.Parse(zipCode_Tag);
                JArray Jcounty = JArray.Parse(county_Tag);
                JArray Jdist = JArray.Parse(dist_Tag);
                JArray Jaddr = JArray.Parse(addr_Tag);
                JArray Jmsg = JArray.Parse(msg_Tag);

                string postURL = "marriagelightsIndex";

                //ApplicantID = objLightDAC.addapplicantinfo_lights_Luer(AppName, AppMobile, AdminID, postURL);
                bool marriagelightsinfo = false;

                if (ApplicantID > 0)
                {
                    for (int i = 0; i < listcount; i++)
                    {
                        string name = Jname[i].ToString();

                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                        string lightsType = "7";
                        string lightsString = "姻緣燈";

                        if (name != "")
                        {
                            marriagelightsinfo = true;
                            //marriagelightsID = objLightDAC.addLights_Luer(ApplicantID, name, Jmobile[i].ToString(), lightsType, lightsString, Jbirth[i].ToString(), Jbirthtime[i].ToString(), Jcounty[i].ToString() + Jdist[i] + Jaddr[i].ToString(), Jaddr[i].ToString(), Jcounty[i].ToString(), Jdist[i].ToString(), JzipCode[i].ToString(), Jmsg[i].ToString());
                        }

                    }
                }

                if (ApplicantID > 0 && marriagelightsinfo)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                    basePage.mJSonHelper.AddContent("redirect", "templeCheck.aspx?kind=1&type=2&a=" + AdminID + "&aid=" + ApplicantID);

                    basePage.Session["ApplicantID"] = ApplicantID;
                }
            }

            public void editinfo(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtData = new DataTable();

                int applicantID = int.Parse(basePage.Request["aid"]);

                string AdminID = basePage.Request["a"];

                dtData = objLightDAC.Getlights_Luer_info(applicantID);

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