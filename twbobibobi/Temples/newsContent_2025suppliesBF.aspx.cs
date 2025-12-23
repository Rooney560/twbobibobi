using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Entities;

namespace twbobibobi.Temples
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
        /// 宮廟編號（可選，主要顯示用）
        /// </summary>
        public string TempleCode { get; set; }

        /// <summary>
        /// 宮廟名稱（可選，主要顯示用）
        /// </summary>
        public string TempleName { get; set; }

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

    public partial class newsContent_2025suppliesBF : System.Web.UI.Page
    {
        /// <summary>
        /// 2025 天赦日招財補運活動狀態字串 - 桃園威天宮
        /// </summary>
        public string Status_Supplies_ty_2025 = string.Empty;

        /// <summary>
        /// 2025 天赦日招財補運活動狀態字串 - 玉敕大樹朝天宮
        /// </summary>
        public string Status_Supplies_ma_2025 = string.Empty;

        /// <summary>
        /// 活動清單（可擴充）
        /// Key = 活動代碼, Value = EventInfo
        /// </summary>
        private readonly Dictionary<string, EventInfo> _events =
            new Dictionary<string, EventInfo>()
            {
                {
                    "Supplies_ty_2025",
                    new EventInfo
                    {
                        Key = "Supplies_ty_2025",
                        TempleCode = "ty",
                        TempleName = "桃園威天宮",
                        Name = "2025 天赦日招財補運",
                        Deadline = new DateTime(2025, 12, 18, 11, 00, 00),
                        IsPermanent = false
                    }
                },
                {
                    "Supplies_ma_2025",
                    new EventInfo
                    {
                        Key = "Supplies_ma_2025",
                        TempleCode = "ma",
                        TempleName = "玉敕大樹朝天宮",
                        Name = "2025 天赦日招財補運",
                        Deadline = new DateTime(2025, 12, 20, 23, 59, 59),
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
            Status_Supplies_ty_2025 = GetEventStatus("Supplies_ty_2025");
            Status_Supplies_ma_2025 = GetEventStatus("Supplies_ma_2025");
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
                a_URL = $"<a href=\"templeService_supplies_{ev.TempleCode}.aspx\" target=\"_blank\" title=\"{ev.TempleName}\">{ev.TempleName}</a>";
            }

            return DateTime.Now <= ev.Deadline.Value ? a_URL : string.Empty;
        }
    }
}