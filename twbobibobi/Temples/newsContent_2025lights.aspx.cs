using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twbobibobi.Temples
{
    /// <summary>
    /// 2025 所有宮廟點燈前置頁面
    /// </summary>
    public partial class newsContent_2025lights : System.Web.UI.Page
    {
        /// <summary> 2025 點燈活動狀態字串 - 大甲鎮瀾宮 </summary>
        public string Status_Lights_da_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 台南正統鹿耳門聖母廟 </summary>
        public string Status_Lights_Luer_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 新港奉天宮 </summary>
        public string Status_Lights_h_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 北港武德宮 </summary>
        public string Status_Lights_wu_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 西螺福興宮 </summary>
        public string Status_Lights_Fu_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 桃園威天宮 </summary>
        public string Status_Lights_ty_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 斗六五路財神宮 </summary>
        public string Status_Lights_Fw_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 台東東海龍門天聖宮 </summary>
        public string Status_Lights_dh_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 鹿港城隍廟 </summary>
        public string Status_Lights_Lk_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 玉敕大樹朝天宮 </summary>
        public string Status_Lights_ma_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 台灣道教總廟無極三清總道院 </summary>
        public string Status_Lights_wjsan_2025 = string.Empty;

        /// <summary> 2025 點燈活動狀態字串 - 松柏嶺受天宮 </summary>
        public string Status_Lights_st_2025 = string.Empty;

        /// <summary>
        /// 活動清單（可擴充）
        /// Key = 活動代碼, Value = EventInfo
        /// </summary>
        private readonly Dictionary<string, EventInfo> _events =
            new Dictionary<string, EventInfo>()
            {
                {
                    "Lights_da_2025",
                    new EventInfo
                    {
                        Key = "Lights_da_2025",
                        TempleCode = "da",
                        TempleName = "大甲鎮瀾宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 06, 30, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Luer_2025",
                    new EventInfo
                    {
                        Key = "Lights_Luer_2025",
                        TempleCode = "Luer",
                        TempleName = "台南正統鹿耳門聖母廟",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_h_2025",
                    new EventInfo
                    {
                        Key = "Lights_h_2025",
                        TempleCode = "h",
                        TempleName = "新港奉天宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_wu_2025",
                    new EventInfo
                    {
                        Key = "Lights_wu_2025",
                        TempleCode = "wu",
                        TempleName = "北港武德宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 01, 19, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Fu_2025",
                    new EventInfo
                    {
                        Key = "Lights_Fu_2025",
                        TempleCode = "Fu",
                        TempleName = "西螺福興宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ty_2025",
                    new EventInfo
                    {
                        Key = "Lights_ty_2025",
                        TempleCode = "ty",
                        TempleName = "桃園威天宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 09, 20, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Fw_2025",
                    new EventInfo
                    {
                        Key = "Lights_Fw_2025",
                        TempleCode = "Fw",
                        TempleName = "斗六五路財神宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_dh_2025",
                    new EventInfo
                    {
                        Key = "Lights_dh_2025",
                        TempleCode = "dh",
                        TempleName = "台東東海龍門天聖宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Lk_2025",
                    new EventInfo
                    {
                        Key = "Lights_Lk_2025",
                        TempleCode = "Lk",
                        TempleName = "鹿港城隍廟",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ma_2025",
                    new EventInfo
                    {
                        Key = "Lights_ma_2025",
                        TempleCode = "ma",
                        TempleName = "玉敕大樹朝天宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_wjsan_2025",
                    new EventInfo
                    {
                        Key = "Lights_wjsan_2025",
                        TempleCode = "wjsan",
                        TempleName = "台灣道教總廟無極三清總道院",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_st_2025",
                    new EventInfo
                    {
                        Key = "Lights_st_2025",
                        TempleCode = "st",
                        TempleName = "松柏嶺受天宮",
                        Name = "2025 點燈",
                        Deadline = new DateTime(2025, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                }
            };

        /// <summary>
        /// 頁面載入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Status_Lights_da_2025 = GetEventStatus("Lights_da_2025");
            Status_Lights_Luer_2025 = GetEventStatus("Lights_Luer_2025");
            Status_Lights_h_2025 = GetEventStatus("Lights_h_2025");
            Status_Lights_wu_2025 = GetEventStatus("Lights_wu_2025");
            Status_Lights_Fu_2025 = GetEventStatus("Lights_Fu_2025");
            Status_Lights_ty_2025 = GetEventStatus("Lights_ty_2025");
            Status_Lights_Fw_2025 = GetEventStatus("Lights_Fw_2025");
            Status_Lights_dh_2025 = GetEventStatus("Lights_dh_2025");
            Status_Lights_Lk_2025 = GetEventStatus("Lights_Lk_2025");
            Status_Lights_ma_2025 = GetEventStatus("Lights_ma_2025");
            Status_Lights_wjsan_2025 = GetEventStatus("Lights_wjsan_2025");
            Status_Lights_st_2025 = GetEventStatus("Lights_st_2025");
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

            string a_URL = string.Empty;

            if (!string.IsNullOrWhiteSpace(ev.TempleCode) && !string.IsNullOrWhiteSpace(ev.TempleName))
            {
                a_URL = $"<a href=\"templeService_lights_{ev.TempleCode}.aspx\" target=\"_blank\" title=\"{ev.TempleName}\">{ev.TempleName}</a>";
            }

            return DateTime.Now <= ev.Deadline.Value ? a_URL : string.Empty;
        }
    }
}