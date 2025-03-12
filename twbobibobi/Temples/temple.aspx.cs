using MotoSystem.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.Temples
{
    public partial class temple : AjaxBasePage
    {
        public string TempleList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                AdminDAC objAdminDAC = new AdminDAC(this);
                DataTable dtAdmin = objAdminDAC.GetAdminList(9);

                if (dtAdmin.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAdmin.Rows.Count; i++)
                    {
                        string adminID = dtAdmin.Rows[i]["AdminID"].ToString();
                        DataTable dtTempleInfo = objAdminDAC.GetTempleInfo(adminID);
                        if (dtTempleInfo.Rows.Count > 0)
                        {
                            string title = dtTempleInfo.Rows[0]["Name"].ToString();
                            string img = dtTempleInfo.Rows[0]["OriginalImageAddress"].ToString();
                            string lightsService = dtTempleInfo.Rows[0]["LightsService"].ToString();
                            string purdueService = dtTempleInfo.Rows[0]["PurdueService"].ToString();
                            string suppliesService = dtTempleInfo.Rows[0]["SuppliesService"].ToString();
                            string supplies2Service = dtTempleInfo.Rows[0]["Supplies2Service"].ToString();
                            string supplies3Service = dtTempleInfo.Rows[0]["Supplies3Service"].ToString();
                            string Supplies4Service = dtTempleInfo.Rows[0]["Supplies4Service"].ToString();
                            string lights2Service = dtTempleInfo.Rows[0]["Lights2Service"].ToString();
                            if(adminID != "30")
                                TempleList += InitTemplelist(adminID, title, img, lightsService, purdueService, suppliesService, supplies2Service, lights2Service, supplies3Service,
                                    Supplies4Service);
                        }
                    }
                }
            }
        }

        //protected string InitTemplelist(string adminID, string title, string img, string lightsService, string purdueService, string suppliesService, string supplies2Service, 
        //    string lights2Service, string supplies3Service)
        //{
        //    string result = string.Empty;

        //    result = "<li>";
        //    result += "<a href=\"templeInfo.aspx?a=" + adminID + "\" title=\"" + title + "\">";
        //    result += "<div class=\"IndexTempleImg\">";
        //    result += "<img src=\"" + img + "\" width=\"600\" height=\"400\" alt=\"\" /></div>";
        //    result += "<div class=\"IndexTempleName\">" + title + "</div>";
        //    result += "<div class=\"IndexTempleTag\">";
        //    result += "<ul>";

        //    if (lightsService == "1")
        //    {
        //        result += "<li class=\"Tag_01\">祈福點燈</li>";
        //    }

        //    if (purdueService == "1")
        //    {
        //        result += "<li class=\"Tag_03\">中元普渡</li>";
        //    }

        //    if (suppliesService == "1")
        //    {
        //        result += "<li class=\"Tag_04\">下元補庫</li>";
        //    }

        //    if (supplies2Service == "1")
        //    {
        //        result += "<li class=\"Tag_02\">財神聖誕補庫</li>";
        //    }

        //    if (supplies3Service == "1")
        //    {
        //        result += "<li class=\"Tag_05\">企業補財庫補庫</li>";
        //    }

        //    if (lights2Service == "1")
        //    {
        //        result += "<li class=\"Tag_01\">月老姻緣燈</li>";
        //    }

        //    result += "</ul></div></a></li>";
        //    return result; 
        //}

        protected string InitTemplelist(string adminID, string title, string img, string lightsService, string purdueService, string suppliesService, string supplies2Service,
            string lights2Service, string supplies3Service, string Supplies4Service)
        {
            string result = string.Empty;

            result = "<li>";
            result += "<a href=\"templeInfo.aspx?a=" + adminID + "\" title=\"" + title + "\">";
            result += "<div class=\"IndexTempleImg\">";
            result += "<img src=\"" + img + "\" width=\"600\" height=\"400\" alt=\"\" /></div>";
            result += "<div class=\"IndexTempleName\">" + title + "</div>";
            result += "<div class=\"IndexTempleTag\">";
            result += "<ul>";

            if (lightsService == "1")
            {
                result += "<li class=\"Tag_01\">祈福點燈</li>";
            }

            if (purdueService == "1")
            {
                result += "<li class=\"Tag_03\">中元普渡</li>";
            }

            if (suppliesService == "1")
            {
                result += "<li class=\"Tag_04\">下元補庫</li>";
            }

            if (supplies2Service == "1")
            {
                result += "<li class=\"Tag_02\">財神聖誕補庫</li>";
            }

            if (supplies3Service == "1")
            {
                result += "<li class=\"Tag_05\">企業補財庫</li>";
            }

            if (Supplies4Service == "1")
            {
                result += "<li class=\"Tag_05\">補財庫</li>";
            }

            if (lights2Service == "1")
            {
                result += "<li class=\"Tag_06\">月老姻緣燈</li>";
            }

            if (adminID == "3")
            {
                result += "<li class=\"Tag_07\">重修慶成祈安七朝清醮</li>";
            }

            result += "</ul></div></a></li>";
            return result;
        }
    }
}