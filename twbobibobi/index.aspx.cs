using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Data;
using twbobibobi.Entities;
using MotoSystem.Data;
using Org.BouncyCastle.Asn1.Ocsp;

namespace twbobibobi
{
    public partial class index : AjaxBasePage
    {
        public string Templelist = string.Empty;
        public string TempleList = string.Empty;
        public string SEO_Title = "";
        public string SEO_Description = "";
        public List<string> seo_Tags = new List<string>();
        public string lightList = string.Empty;
        public string carousel_indicators = string.Empty;
        public string carousel_inners = string.Empty;
        public string ceremonyList = string.Empty;
        public List<SysSetting> sysSettings = new List<SysSetting>();
        public string eventTitbitsList = string.Empty; //活動花絮
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                //SEO設定，數據從 database 拿 /////////////////////////////////////
                //SEO_Title = "SEO【保必保庇】線上宮廟服務平台";
                //SEO_Description = "SEO - 線上光明燈,線上點燈,線上宮廟服務,線上即可點燈,多家知名宮廟可選,光明燈,補財庫,普渡,點燈服務等,忙碌之餘也能進行跨縣市宮廟點燈";
                seo_Tags = CacheSeoTag.GetList("HOME");
                //////////////////////////////////////////////////
                sysSettings = CacheSysSetting.GetList("home");
                InitLightList();
                InitCarouselList();
                InitCeremonyList();
                InitEventTitbits();

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
                            string supplies4Service = dtTempleInfo.Rows[0]["Supplies4Service"].ToString();
                            string lights2Service = dtTempleInfo.Rows[0]["Lights2Service"].ToString();
                            if (adminID != "30")
                            {
                                Templelist += InitTemplelist(adminID, title, img, lightsService, purdueService, suppliesService, supplies2Service, lights2Service, supplies3Service,
                                     supplies4Service);
                            }
                        }
                    }
                }
            }
        }

        protected string InitTemplelist(string adminID, string title, string img, string lightsService, string purdueService, string suppliesService, string supplies2Service, 
            string lights2Service, string supplies3Service, string supplies4Service)
        {
            string result = string.Empty;

            result = "<li>";
            result += "<a href=\"https://bobibobi.tw/Temples/templeInfo.aspx?a=" + adminID + "\" title=\"" + title + "\">";
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
                result += "<li class=\"Tag_02\">呈疏補庫</li>";
            }

            if (supplies3Service == "1")
            {
                result += "<li class=\"Tag_05\">企業補財庫</li>";
            }

            if (supplies4Service == "1")
            {
                result += "<li class=\"Tag_05\">補財庫</li>";
            }

            if (lights2Service == "1")
            {
                result += "<li class=\"Tag_01\">月老姻緣燈</li>";
            }

            if (adminID == "3")
            {
                result += "<li class=\"Tag_07\">重修慶成祈安七朝清醮</li>";
            }

            result += "</ul></div></a></li>";
            return result;
        }

        protected void InitLightList()
        {
            var list = CacheLight.GetList();

            lightList = "";
            foreach (var item in list)
            {
                if (item.ButtonVisible == 1 && item.ButtonText.Length > 0 && item.ButtonLink.Length > 0)
                    lightList += $"                        <div class=\"LightList-Item\">\r\n                            <a href=\"{item.ButtonLink}\">\r\n                                <div class=\"card shadow h-100 LightCard\">\r\n                                    <img src=\"./Temples/SiteFile/light/{item.Id}.{item.ImageFileType}\" style=\"height: 150px;\" class=\"card-img-to\" alt=\"\" />\r\n                                    <div class=\"card-body pb-0\">\r\n                                        <div class=\"fs-3\">{item.Title}</div>\r\n                                        <div class=\"fs-5\">{item.Description}</div>\r\n                                    </div>\r\n                                    <div class=\"card-footer pt-0 pb-3\" style=\"border-top: none; background-color: transparent;\">\r\n                                        <div class=\"ReadMoreBtn\"><span>&nbsp;{item.ButtonText}&nbsp;</span></div>\r\n                                    </div>\r\n                                </div>\r\n                            </a>\r\n                        </div>\r\n";
                else
                    lightList += $"                        <div class=\"LightList-Item\">\r\n                            <div class=\"card shadow h-100 LightCard\">\r\n                                    <img src=\"./Temples/SiteFile/light/{item.Id}.{item.ImageFileType}\" style=\"height: 150px;\" class=\"card-img-to\" alt=\"\" />\r\n                                    <div class=\"card-body pb-0\">\r\n                                        <div class=\"fs-3\">{item.Title}</div>\r\n                                        <div class=\"fs-5\">{item.Description}</div>\r\n                                    </div>\r\n                            </div>\r\n                        </div>\r\n";
            }
        }
        protected void InitCarouselList()
        {
            var list = CacheCarousel.GetList("HOME");

            carousel_indicators = "";
            carousel_inners = "";
            string activeTag, carousel_caption, title, description, button;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                activeTag = i > 0 ? "" : $"                        class=\"active\"\r\n                        aria-current=\"true\"\r\n";
                carousel_indicators += $"                    <button\r\n                        type=\"button\"\r\n                        data-bs-target=\"#carouselExampleCaptions\"\r\n                        data-bs-slide-to=\"{i}\"\r\n{activeTag}                        aria-label=\"Slide {i + 1}\">\r\n                    </button>\r\n";

                activeTag = i > 0 ? "" : $" active";
                if (item.Title.Length == 0 && item.Description.Length == 0 && item.ButtonText.Length == 0)
                    carousel_caption = "";
                else
                {
                    title = item.Title.Length == 0 ? "" : $"                            <h1>{item.Title}</h1>\r\n";
                    description = item.Description.Length == 0 ? "" : $"                            <h5>{item.Description}</h5>\r\n";
                    button = item.ButtonVisible != 1 || item.ButtonText.Length == 0 || item.ButtonLink.Length == 0 ? "" : $"                            <a href=\"{item.ButtonLink}\">\r\n                                <div class=\"btn btn-warning carousel-button\">{item.ButtonText}</div>\r\n                            </a>\r\n";
                    carousel_caption = $"                        <div class=\"carousel-caption\">\r\n{title}{description}{button}                        </div>\r\n";
                }

                carousel_inners += $"                    <div class=\"carousel-item{activeTag}\">\r\n                        <img src=\"./Temples/SiteFile/carousel/{item.Id}.{item.ImageFileType}\" class=\"d-none d-sm-block w-100\" alt=\"...\" />\r\n                        <img src=\"./Temples/SiteFile/carousel/{item.Id}s.{item.ImageFileType}\" class=\"d-block d-sm-none w-100\" alt=\"...\" />\r\n{carousel_caption}                    </div>\r\n";
            }
        }
        //protected void InitCeremonyList()
        //{
        //    ceremonyList = "";

        //    if (FindSysSetting("home_show_ceremony") == "false") return;

        //    string sectionPrefix = $"            <hr />\r\n            <div class=\"IndexCeremony\">\r\n                <div class=\"row justify-content-center py-5\">\r\n                    <img src=\"./Temples/images/roof.png\" class=\"pb-1\" style=\"width: 200px;\" alt=\"\" />\r\n                    <div class=\"CategoryTitle\">-&nbsp;新聞報導&nbsp;-</div>\r\n                    <hr class=\"Category\" />\r\n                </div>\r\n                <div class=\"row justify-content-center\">\r\n                    <div class=\"accordion CeremonyList\" id=\"accordionExample\">\r\n";
        //    string sectionSuffix = $"                    </div>\r\n                </div>\r\n            </div>\r\n";

        //    var list = CacheCeremony.GetList();

        //    string expanded, show, button;

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var item = list[i];

        //        expanded = i > 0 ? "false" : "true";
        //        show = i > 0 ? "" : " show";
        //        button = item.ButtonVisible != 1 || item.ButtonText.Length == 0 || item.ButtonLink.Length == 0 ? "" : $"                                    <a href=\"{item.ButtonLink}\">\r\n                                        <div class=\"btn btn-warning btn-lg\">{item.ButtonText}</div>\r\n                                    </a>\r\n";

        //        ceremonyList += $"                        <div class=\"accordion-item\" style=\"background-color: papayawhip;\">\r\n                            <div class=\"accordion-button\" style=\"background-color: wheat;\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse{i}\" aria-expanded=\"{expanded}\" aria-controls=\"collapse{i}\">\r\n                                <h2 class=\"accordion-header\">{item.Title}\r\n                                </h2>\r\n                            </div>\r\n                            <div id=\"collapse{i}\" class=\"accordion-collapse collapse{show}\" data-bs-parent=\"#accordionExample\">\r\n                                <div class=\"accordion-body\">\r\n                                    <div class=\"row justify-content-center pt-3 pb-5\">\r\n                                        <img src=\"./Temples/SiteFile/ceremony/{item.Id}.{item.ImageFileType}\" class=\"w-50\" alt=\"\" />\r\n                                    </div>\r\n                                    <div class=\"fs-5 fw-lighter lh-lg\">\r\n{item.Description.Replace("\n", "<br />")}\r\n                                    </div>\r\n{button}                                </div>\r\n                            </div>\r\n                        </div>\r\n";
        //    }

        //    if (ceremonyList.Length > 0)
        //        ceremonyList = sectionPrefix + ceremonyList + sectionSuffix;
        //}
        protected void InitCeremonyList()
        {
            ceremonyList = "";

            if (FindSysSetting("home_show_ceremony") == "false") return;

            string sectionPrefix = $"            <hr />\r\n            <div class=\"IndexCeremony\">\r\n                <div class=\"row justify-content-center py-5\">\r\n                    <img src=\"./Temples/images/roof.png\" class=\"pb-1\" style=\"width: 200px;\" alt=\"\" />\r\n                    <div class=\"CategoryTitle\">-&nbsp;新聞報導&nbsp;-</div>\r\n                    <hr class=\"Category\" />\r\n                </div>\r\n                <div class=\"row justify-content-center\">\r\n                    <div class=\"accordion CeremonyList\" id=\"accordionExample\">\r\n";
            string sectionSuffix = $"                    </div>\r\n                </div>\r\n            </div>\r\n";

            var list = CacheCeremony.GetList();

            string expanded, show, button, img;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                expanded = i > 0 ? "false" : "true";
                show = i > 0 ? "" : " show";
                button = item.ButtonVisible != 1 || item.ButtonText.Length == 0 || item.ButtonLink.Length == 0 ? "" : $"                                    <a href=\"{item.ButtonLink}\">\r\n                                        <div class=\"btn btn-warning btn-lg\">{item.ButtonText}</div>\r\n                                    </a>\r\n";
                img = item.ImageFileType.Length == 0 ? "" : $"                                    <div class=\"row justify-content-center pt-3\">\r\n                                        <div class=\"col-lg-7 col-md-12\">\r\n                                            <img src=\"./Temples/SiteFile/ceremony/{item.Id}.{item.ImageFileType}\" class=\"w-100\" alt=\"\" />\r\n                                        </div>\r\n                                    </div>\r\n";

                ceremonyList += $"                        <div class=\"accordion-item\" style=\"background-color: papayawhip;\">\r\n                            <div class=\"accordion-button\" style=\"background-color: wheat;\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse{i}\" aria-expanded=\"{expanded}\" aria-controls=\"collapse{i}\">\r\n                                <h2 class=\"accordion-header\">{item.Title}\r\n                                </h2>\r\n                            </div>\r\n                            <div id=\"collapse{i}\" class=\"accordion-collapse collapse{show}\" data-bs-parent=\"#accordionExample\">\r\n                                <div class=\"accordion-body\">\r\n{img}                                    <div class=\"fs-5 fw-lighter lh-lg\">\r\n{item.Description.Replace("\n", "<br />")}\r\n                                    </div>\r\n{button}                                </div>\r\n                            </div>\r\n                        </div>\r\n";
            }

            if (ceremonyList.Length > 0)
                ceremonyList = sectionPrefix + ceremonyList + sectionSuffix;
        }


        /// <summary>
        /// 初始化活動花絮
        /// </summary>
        protected void InitEventTitbits()
        {
            eventTitbitsList = "";
            string val = CacheSysSetting.GetValue("event_titbits_top_rows"); //首頁活動花絮顯示最新的前 xx 筆記錄
            if (!int.TryParse(val, out int topRows)) return;

            var list = CacheEventTitbits.GetList(topRows);
            if (list == null || list.Count == 0) return;

            eventTitbitsList = "             <div class=\"EventTitbitsList\">\r\n";
            foreach (var item in list)
            {
                eventTitbitsList += $"                 <div>\r\n                     <a href=\"Temples/EventTitbits.aspx?eventId={item.EventId}\">\r\n                         <div class=\"IndexEventTitbitsImg\">\r\n                             <img src=\"./Temples/SiteFile/EventTitbits/{item.EventId}/{item.Id}.{item.ImageFileType}\" alt=\"\" />\r\n                         </div>\r\n                     </a>\r\n                 </div>\r\n";
            }
            eventTitbitsList += "             </div>\r\n             <br />\r\n";
            eventTitbitsList = $"         <div class=\"IndexEventTitbits\">\r\n             <div class=\"row justify-content-center py-5\">\r\n                 <img src=\"Temples/images/roof.png\" class=\"pb-1\" style=\"width: 200px;\" alt=\"\" />\r\n                 <div class=\"CategoryTitle\">-&nbsp;活動花絮&nbsp;-</div>\r\n                 <hr class=\"Category\" />\r\n             </div>\r\n{eventTitbitsList}         </div>\r\n";
        }
        protected string FindSysSetting(string item)
        {
            foreach (var it in sysSettings)
            {
                if (it.Item.Equals(item, StringComparison.OrdinalIgnoreCase))
                {
                    if (it.Type == "text")
                        return it.Value.Replace("\n", "<br />");
                    else
                        return it.Value;
                }
            }
            return "";
        }

        //protected void InitCeremonyList()
        //{
        //    ceremonyList = "";

        //    if (FindSysSetting("home_show_ceremony") == "false") return;

        //    string sectionPrefix = $"            <hr />\r\n            <div class=\"IndexCeremony\">\r\n                <div class=\"row justify-content-center py-5\">\r\n                    <img src=\"./Temples/images/roof.png\" class=\"pb-1\" style=\"width: 200px;\" alt=\"\" />\r\n                    <div class=\"CategoryTitle\">-&nbsp;法會介紹&nbsp;-</div>\r\n                    <hr class=\"Category\" />\r\n                </div>\r\n                <div class=\"row justify-content-center\">\r\n                    <div class=\"accordion CeremonyList\" id=\"accordionExample\">\r\n";
        //    string sectionSuffix = $"                    </div>\r\n                </div>\r\n            </div>\r\n";

        //    var list = CacheCeremony.GetList();

        //    string expanded, show;

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var item = list[i];

        //        expanded = i > 0 ? "false" : "true";
        //        show = i > 0 ? "" : " show";

        //        ceremonyList += $"                        <div class=\"accordion-item\" style=\"background-color: papayawhip;\">\r\n                            <div class=\"accordion-button\" style=\"background-color: wheat;\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse{i}\" aria-expanded=\"{expanded}\" aria-controls=\"collapse{i}\">\r\n                                <h2 class=\"accordion-header\">{item.Title}\r\n                                </h2>\r\n                            </div>\r\n                            <div id=\"collapse{i}\" class=\"accordion-collapse collapse{show}\" data-bs-parent=\"#accordionExample\">\r\n                                <div class=\"accordion-body\">\r\n                                    <div class=\"row justify-content-center pt-3 pb-5\">\r\n                                        <img src=\"./Temples/SiteFile/ceremony/{item.Id}.{item.ImageFileType}\" /*class=\"w-50\"*/ alt=\"\" />\r\n                                    </div>\r\n                                    <div class=\"fs-5 fw-lighter lh-lg\">\r\n{item.Description.Replace("\n", "<br />")}\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n";
        //    }

        //    if (ceremonyList.Length > 0)
        //        ceremonyList = sectionPrefix + ceremonyList + sectionSuffix;
        //}
        //protected void InitCeremonyList()
        //{
        //    ceremonyList = "";

        //    if (FindSysSetting("home_show_ceremony") == "false") return;

        //    string sectionPrefix = $"            <hr />\r\n            <div class=\"IndexCeremony\">\r\n                <div class=\"row justify-content-center py-5\">\r\n                    <img src=\"./Temples/images/roof.png\" class=\"pb-1\" style=\"width: 200px;\" alt=\"\" />\r\n                    <div class=\"CategoryTitle\">-&nbsp;法會介紹&nbsp;-</div>\r\n                    <hr class=\"Category\" />\r\n                </div>\r\n                <div class=\"row justify-content-center\">\r\n                    <div class=\"accordion CeremonyList\" id=\"accordionExample\">\r\n";
        //    string sectionSuffix = $"                    </div>\r\n                </div>\r\n            </div>\r\n";

        //    var list = CacheCeremony.GetList();

        //    string expanded, show, button;

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        var item = list[i];

        //        expanded = i > 0 ? "false" : "true";
        //        show = i > 0 ? "" : " show";
        //        button = item.ButtonVisible == 0 || item.ButtonText.Length == 0 ? "" : $"                                    <a href=\"{item.ButtonLink}\">\r\n                                        <div class=\"btn btn-warning btn-lg\">{item.ButtonText}</div>\r\n                                    </a>\r\n";

        //        ceremonyList += $"                        <div class=\"accordion-item\" style=\"background-color: papayawhip;\">\r\n                            <div class=\"accordion-button\" style=\"background-color: wheat;\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse{i}\" aria-expanded=\"{expanded}\" aria-controls=\"collapse{i}\">\r\n                                <h2 class=\"accordion-header\">{item.Title}\r\n                                </h2>\r\n                            </div>\r\n                            <div id=\"collapse{i}\" class=\"accordion-collapse collapse{show}\" data-bs-parent=\"#accordionExample\">\r\n                                <div class=\"accordion-body\">\r\n                                    <div class=\"row justify-content-center pt-3 pb-5\">\r\n                                        <img src=\"./Temples/SiteFile/ceremony/{item.Id}.{item.ImageFileType}\" class=\"w-50\" alt=\"\" />\r\n                                    </div>\r\n                                    <div class=\"fs-5 fw-lighter lh-lg\">\r\n{item.Description.Replace("\n", "<br />")}\r\n                                    </div>\r\n{button}                                </div>\r\n                            </div>\r\n                        </div>\r\n";
        //    }

        //    if (ceremonyList.Length > 0)
        //        ceremonyList = sectionPrefix + ceremonyList + sectionSuffix;
        //}
        //protected string FindSysSetting(string item)
        //{
        //    foreach (var it in sysSettings)
        //    {
        //        if (it.Item.Equals(item, StringComparison.OrdinalIgnoreCase))
        //        {
        //            if (it.Type == "text")
        //                return it.Value.Replace("\n", "<br />");
        //            else
        //                return it.Value;
        //        }
        //    }
        //    return "";
        //}
    }
}