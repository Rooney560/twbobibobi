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
using twbobibobi.Data;
using Org.BouncyCastle.Asn1.Ocsp;

namespace twbobibobi
{
    /// <summary>
    /// 活動資訊類別
    /// 用來描述單一活動的基本資料與狀態設定
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        /// 活動代碼（唯一識別用，例如 "Lights_2025"）
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 活動名稱（可選，主要顯示用）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活動截止時間（可為 null，如果為 null 表示永久活動）
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// 是否為永久活動（長期活動）
        /// </summary>
        public bool IsPermanent { get; set; }
    }

    public partial class index : AjaxBasePage
    {
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

        // 狀態顯示字串
        private readonly string status_ing = "<div class=\"event-ongoing fs-5\">⚡ 活動進行中 ⚡</div>";
        private readonly string status_end = "<div class=\"event-ongoend fs-5\">活動已結束</div>";

        /// <summary>
        /// 2026 點燈活動狀態字串
        /// </summary>
        public string Status_Lights_2026 = string.Empty;

        /// <summary>
        /// 2026 安斗活動狀態字串
        /// </summary>
        public string Status_AnDou_2026 = string.Empty;

        /// <summary>
        /// 2025 點燈活動狀態字串
        /// </summary>
        public string Status_Lights_2025 = string.Empty;

        /// <summary>
        /// 2025 安斗活動狀態字串
        /// </summary>
        public string Status_AnDou_2025 = string.Empty;

        /// <summary>
        /// 2025 普度活動狀態字串
        /// </summary>
        public string Status_Purdue_2025 = string.Empty;

        /// <summary>
        /// 2025 斗六五路財神宮 補財庫 活動狀態字串
        /// </summary>
        public string Status_Supplies_Fw_2025 = string.Empty;

        /// <summary>
        /// 2025 天赦日招財補運活動狀態字串
        /// </summary>
        public string Status_SuppliesBF_2025 = string.Empty;

        /// <summary>
        /// 2025 神霄玉府財神會館 赦罪補庫 活動狀態字串
        /// </summary>
        public string Status_Supplies_sx_2025 = string.Empty;

        /// <summary>
        /// 2025 神霄玉府財神會館 供香轉運 活動狀態字串
        /// </summary>
        public string Status_Supplies2_sx_2025 = string.Empty;

        /// <summary>
        /// 2025 台灣道教總廟無極三清總道院 供花供果 活動狀態字串
        /// </summary>
        public string Status_Huaguo_wjsan_2025 = string.Empty;

        /// <summary>
        /// 2025 松柏嶺受天宮 祈安植福 活動狀態字串
        /// </summary>
        public string Status_Blessing_st_2025 = string.Empty;

        /// <summary>
        /// 2025 玉敕大樹朝天宮 靈寶禮斗 活動狀態字串
        /// </summary>
        public string Status_Lingbaolidou_2025 = string.Empty;

        /// <summary>
        /// 2025 台東東海龍門天聖宮 護國息災梁皇大法會 活動狀態字串
        /// </summary>
        public string Status_Lybc_dh_2025 = string.Empty;

        /// <summary>
        /// 2025 桃園威天宮 千手觀音千燈迎佛法會 活動狀態字串
        /// </summary>
        public string Status_QnLight_ty_2025 = string.Empty;

        /// <summary>
        /// 2025 北港武德宮 下元補庫 活動狀態字串
        /// </summary>
        public string Status_Supplies_wu_2025 = string.Empty;

        /// <summary>
        /// 活動清單（可擴充）
        /// Key = 活動代碼, Value = EventInfo
        /// </summary>
        private readonly Dictionary<string, EventInfo> _events =
            new Dictionary<string, EventInfo>()
            {
                {
                    "Lights_2025",
                    new EventInfo
                    {
                        Key = "Lights_2025",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "AnDou_2025",
                    new EventInfo
                    {
                        Key = "AnDou_2025",
                        Name = "2025 安奉斗燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Purdue_2025",
                    new EventInfo
                    {
                        Key = "Purdue_2025",
                        Name = "2025 普度",
                        Deadline = new DateTime(2025, 9, 19, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Supplies_Fw_2025",
                    new EventInfo
                    {
                        Key = "Supplies_Fw_2025",
                        Name = "斗六五路財神宮 補財庫",
                        Deadline = null, // 無截止日
                        IsPermanent = true
                    }
                },
                {
                    "SuppliesBF_2025",
                    new EventInfo
                    {
                        Key = "SuppliesBF_2025",
                        Name = "2025 天赦日招財補運",
                        Deadline = new DateTime(2025, 12, 20, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Supplies2_sx_2025",
                    new EventInfo
                    {
                        Key = "Supplies2_sx_2025",
                        Name = "神霄玉府財神會館 供香轉運",
                        Deadline = null, // 無截止日
                        IsPermanent = true
                    }
                },
                {
                    "Huaguo_wjsan_2025",
                    new EventInfo
                    {
                        Key = "Huaguo_wjsan_2025",
                        Name = "台灣道教總廟無極三清總道院 供花供果",
                        Deadline = null, // 無截止日
                        IsPermanent = true
                    }
                },
                {
                    "Blessing_st_2025",
                    new EventInfo
                    {
                        Key = "Blessing_st_2025",
                        Name = "松柏嶺受天宮 祈安植福",
                        Deadline = null, // 無截止日
                        IsPermanent = true
                    }
                },
                {
                    "LongTermBlessing",
                    new EventInfo
                    {
                        Key = "LongTermBlessing",
                        Name = "長期祈福",
                        Deadline = null, // 無截止日
                        IsPermanent = true
                    }
                },
                {
                    "Lingbaolidou_2025",
                    new EventInfo
                    {
                        Key = "Lingbaolidou_2025",
                        Name = "2025 靈寶禮斗",
                        Deadline = new DateTime(2025, 11, 06, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lybc_2025",
                    new EventInfo
                    {
                        Key = "Lybc_2025",
                        Name = "2025 護國息災梁皇大法會",
                        Deadline = new DateTime(2025, 11, 10, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "QnLight_2025",
                    new EventInfo
                    {
                        Key = "QnLight_2025",
                        Name = "2025 千手觀音千燈迎佛法會",
                        Deadline = new DateTime(2025, 11, 04, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Supplies_wu_2025",
                    new EventInfo
                    {
                        Key = "Supplies_wu_2025",
                        Name = "2025 下元補庫",
                        Deadline = new DateTime(2025, 11, 26, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_2026",
                    new EventInfo
                    {
                        Key = "Lights_2026",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "AnDou_2026",
                    new EventInfo
                    {
                        Key = "AnDou_2026",
                        Name = "2026 安奉斗燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                }
            };

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
                DataTable dtTempleInfo = objAdminDAC.GetTempleInfo();
                for (int i = 0; i < dtTempleInfo.Rows.Count; i++)
                {
                    string adminID = dtTempleInfo.Rows[i]["AdminID"].ToString();
                    string title = dtTempleInfo.Rows[i]["Name"].ToString();
                    string img = dtTempleInfo.Rows[i]["OriginalImageAddress"].ToString();
                    string lightsService = dtTempleInfo.Rows[i]["LightsService"].ToString();
                    string purdueService = dtTempleInfo.Rows[i]["PurdueService"].ToString();
                    string suppliesService = dtTempleInfo.Rows[i]["SuppliesService"].ToString();
                    string supplies2Service = dtTempleInfo.Rows[i]["Supplies2Service"].ToString();
                    string supplies3Service = dtTempleInfo.Rows[i]["Supplies3Service"].ToString();
                    string Supplies4Service = dtTempleInfo.Rows[i]["Supplies4Service"].ToString();
                    string lights2Service = dtTempleInfo.Rows[i]["Lights2Service"].ToString();
                    string BlessingService = dtTempleInfo.Rows[i]["BlessingService"].ToString();
                    TempleList += InitTemplelist(adminID, title, img, lightsService, purdueService, suppliesService, supplies2Service, lights2Service, supplies3Service,
                        Supplies4Service, BlessingService);
                }


                Status_Lights_2025 = GetEventStatus("Lights_2025");
                Status_AnDou_2025 = GetEventStatus("AnDou_2025");
                Status_Purdue_2025 = GetEventStatus("Purdue_2025");
                Status_Supplies_Fw_2025 = GetEventStatus("Supplies_Fw_2025");
                Status_SuppliesBF_2025 = GetEventStatus("SuppliesBF_2025");
                Status_Supplies2_sx_2025 = GetEventStatus("Supplies2_sx_2025");
                Status_Huaguo_wjsan_2025 = GetEventStatus("Huaguo_wjsan_2025");
                Status_Blessing_st_2025 = GetEventStatus("Blessing_st_2025");
                Status_Lingbaolidou_2025 = GetEventStatus("Lingbaolidou_2025");
                Status_Lybc_dh_2025 = GetEventStatus("Lybc_2025");
                Status_QnLight_ty_2025 = GetEventStatus("QnLight_2025");
                Status_Supplies_wu_2025 = GetEventStatus("Supplies_wu_2025");
                Status_Lights_2026 = GetEventStatus("Lights_2026");
                Status_AnDou_2026 = GetEventStatus("AnDou_2026");
            }
        }

        protected string InitTemplelist(string adminID, string title, string img, string lightsService, string purdueService, string suppliesService, string supplies2Service,
            string lights2Service, string supplies3Service, string Supplies4Service, string BlessingService)
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

            if (BlessingService == "1")
            {
                result += "<li class=\"Tag_09\">祈安植福</li>";
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
        /// 取得指定活動的狀態 HTML
        /// </summary>
        /// <param name="eventKey">活動代碼</param>
        /// <returns>對應的 HTML 狀態字串</returns>
        private string GetEventStatus(string eventKey)
        {
            if (!_events.ContainsKey(eventKey))
                return string.Empty;

            EventInfo ev = _events[eventKey];

            if (ev.IsPermanent || ev.Deadline == null)
                return status_ing; // 永久活動 → 永遠進行中

            return DateTime.Now <= ev.Deadline.Value ? status_ing : status_end;
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