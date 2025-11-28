using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twbobibobi.Temples
{
    /// <summary>
    /// 2026 所有宮廟點燈前置頁面
    /// </summary>
    public partial class newsContent_2026lights : System.Web.UI.Page
    {
        /// <summary> 2026 點燈活動狀態字串 - 大甲鎮瀾宮 </summary>
        public string Status_Lights_da_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 台南正統鹿耳門聖母廟 </summary>
        public string Status_Lights_Luer_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 新港奉天宮 </summary>
        public string Status_Lights_h_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 北港武德宮 </summary>
        public string Status_Lights_wu_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 西螺福興宮 </summary>
        public string Status_Lights_Fu_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 桃園威天宮 </summary>
        public string Status_Lights_ty_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 斗六五路財神宮 </summary>
        public string Status_Lights_Fw_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 台東東海龍門天聖宮 </summary>
        public string Status_Lights_dh_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 鹿港城隍廟 </summary>
        public string Status_Lights_Lk_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 玉敕大樹朝天宮 </summary>
        public string Status_Lights_ma_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 台灣道教總廟無極三清總道院 </summary>
        public string Status_Lights_wjsan_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 桃園龍德宮 </summary>
        public string Status_Lights_ld_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 松柏嶺受天宮 </summary>
        public string Status_Lights_st_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 池上北極玄天宮 </summary>
        public string Status_Lights_bj_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 花蓮慈惠石壁部堂 </summary>
        public string Status_Lights_sbbt_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 新北真武山受玄宮 </summary>
        public string Status_Lights_bpy_2026 = string.Empty;

        /// <summary> 2026 點燈活動狀態字串 - 桃園壽山巖觀音寺 </summary>
        public string Status_Lights_ssy_2026 = string.Empty;

        /// <summary>
        /// 活動清單（可擴充）
        /// Key = 活動代碼, Value = EventInfo
        /// </summary>
        private readonly Dictionary<string, EventInfo> _events =
            new Dictionary<string, EventInfo>()
            {
                {
                    "Lights_da_2026",
                    new EventInfo
                    {
                        Key = "Lights_da_2026",
                        TempleCode = "da",
                        TempleName = "大甲鎮瀾宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 02, 08, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Luer_2026",
                    new EventInfo
                    {
                        Key = "Lights_Luer_2026",
                        TempleCode = "Luer",
                        TempleName = "台南正統鹿耳門聖母廟",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_h_2026",
                    new EventInfo
                    {
                        Key = "Lights_h_2026",
                        TempleCode = "h",
                        TempleName = "新港奉天宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_wu_2026",
                    new EventInfo
                    {
                        Key = "Lights_wu_2026",
                        TempleCode = "wu",
                        TempleName = "北港武德宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 01, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Fu_2026",
                    new EventInfo
                    {
                        Key = "Lights_Fu_2026",
                        TempleCode = "Fu",
                        TempleName = "西螺福興宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ty_2026",
                    new EventInfo
                    {
                        Key = "Lights_ty_2026",
                        TempleCode = "ty",
                        TempleName = "桃園威天宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 09, 15, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Fw_2026",
                    new EventInfo
                    {
                        Key = "Lights_Fw_2026",
                        TempleCode = "Fw",
                        TempleName = "斗六五路財神宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_dh_2026",
                    new EventInfo
                    {
                        Key = "Lights_dh_2026",
                        TempleCode = "dh",
                        TempleName = "台東東海龍門天聖宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_Lk_2026",
                    new EventInfo
                    {
                        Key = "Lights_Lk_2026",
                        TempleCode = "Lk",
                        TempleName = "鹿港城隍廟",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ma_2026",
                    new EventInfo
                    {
                        Key = "Lights_ma_2026",
                        TempleCode = "ma",
                        TempleName = "玉敕大樹朝天宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_wjsan_2026",
                    new EventInfo
                    {
                        Key = "Lights_wjsan_2026",
                        TempleCode = "wjsan",
                        TempleName = "台灣道教總廟無極三清總道院",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ld_2026",
                    new EventInfo
                    {
                        Key = "Lights_ld_2026",
                        TempleCode = "ld",
                        TempleName = "桃園龍德宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_st_2026",
                    new EventInfo
                    {
                        Key = "Lights_st_2026",
                        TempleCode = "st",
                        TempleName = "松柏嶺受天宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_bj_2026",
                    new EventInfo
                    {
                        Key = "Lights_bj_2026",
                        TempleCode = "bj",
                        TempleName = "池上北極玄天宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_sbbt_2026",
                    new EventInfo
                    {
                        Key = "Lights_sbbt_2026",
                        TempleCode = "sbbt",
                        TempleName = "花蓮慈惠石壁部堂",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_bpy_2026",
                    new EventInfo
                    {
                        Key = "Lights_bpy_2026",
                        TempleCode = "bpy",
                        TempleName = "新北真武山受玄宮",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "Lights_ssy_2026",
                    new EventInfo
                    {
                        Key = "Lights_ssy_2026",
                        TempleCode = "ssy",
                        TempleName = "桃園壽山巖觀音寺",
                        Name = "2026 點燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
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
            Status_Lights_da_2026 = GetEventStatus("Lights_da_2026");
            Status_Lights_Luer_2026 = GetEventStatus("Lights_Luer_2026");
            Status_Lights_h_2026 = GetEventStatus("Lights_h_2026");
            Status_Lights_wu_2026 = GetEventStatus("Lights_wu_2026");
            Status_Lights_Fu_2026 = GetEventStatus("Lights_Fu_2026");
            Status_Lights_ty_2026 = GetEventStatus("Lights_ty_2026");
            Status_Lights_Fw_2026 = GetEventStatus("Lights_Fw_2026");
            Status_Lights_dh_2026 = GetEventStatus("Lights_dh_2026");
            Status_Lights_Lk_2026 = GetEventStatus("Lights_Lk_2026");
            Status_Lights_ma_2026 = GetEventStatus("Lights_ma_2026");
            Status_Lights_wjsan_2026 = GetEventStatus("Lights_wjsan_2026");
            Status_Lights_ld_2026 = GetEventStatus("Lights_ld_2026");
            Status_Lights_st_2026 = GetEventStatus("Lights_st_2026");
            Status_Lights_bj_2026 = GetEventStatus("Lights_bj_2026");
            Status_Lights_sbbt_2026 = GetEventStatus("Lights_sbbt_2026");
            Status_Lights_bpy_2026 = GetEventStatus("Lights_bpy_2026");
            Status_Lights_ssy_2026 = GetEventStatus("Lights_ssy_2026");
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