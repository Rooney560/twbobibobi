using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Data;

namespace Temple.Temples
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

    /// <summary>
    /// 活動最新消息頁面
    /// 用於判斷各活動是否進行中或已結束，並輸出狀態字串到前端。
    /// </summary>
    public partial class news : System.Web.UI.Page
    {
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

        /// <summary>
        /// 頁面載入事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
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

        /// <summary>
        /// 取得指定活動的狀態 HTML
        /// </summary>
        /// <param name="eventKey">活動代碼</param>
        /// <returns>對應的 HTML 狀態字串</returns>
        private string GetEventStatus(string eventKey)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();

            if (!_events.ContainsKey(eventKey))
                return string.Empty;

            EventInfo ev = _events[eventKey];

            if (ev.IsPermanent || ev.Deadline == null)
                return status_ing; // 永久活動 → 永遠進行中

            return dtNow <= ev.Deadline.Value ? status_ing : status_end;
        }
    }
}