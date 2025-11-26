using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twbobibobi.Temples
{
    /// <summary>
    /// ２０２６丙午馬年安奉斗燈最新消息頁面
    /// </summary>
    public partial class newsContent_2026andou : System.Web.UI.Page
    {
        /// <summary>
        /// 2026 安奉斗燈活動狀態字串 - 斗六五路財神宮
        /// </summary>
        public string Status_AnDou_Fw_2026 = string.Empty;

        /// <summary>
        /// 2026 安奉斗燈活動狀態字串 - 台灣道教總廟無極三清總道院
        /// </summary>
        public string Status_AnDou_wjsan_2026 = string.Empty;

        /// <summary>
        /// 活動清單（可擴充）
        /// Key = 活動代碼, Value = EventInfo
        /// </summary>
        private readonly Dictionary<string, EventInfo> _events =
            new Dictionary<string, EventInfo>()
            {
                {
                    "AnDou_Fw_2026",
                    new EventInfo
                    {
                        Key = "AnDou_Fw_2026",
                        TempleCode = "Fw",
                        TempleName = "斗六五路財神宮",
                        Name = "2026 安奉斗燈",
                        Deadline = new DateTime(2026, 10, 31, 23, 59, 59),
                        IsPermanent = false
                    }
                },
                {
                    "AnDou_wjsan_2026",
                    new EventInfo
                    {
                        Key = "AnDou_wjsan_2026",
                        TempleCode = "wjsan",
                        TempleName = "台灣道教總廟無極三清總道院",
                        Name = "2026 安奉斗燈",
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
            Status_AnDou_Fw_2026 = GetEventStatus("AnDou_Fw_2026");
            Status_AnDou_wjsan_2026 = GetEventStatus("AnDou_wjsan_2026");
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
                a_URL = $"<a href=\"templeService_andou_{ev.TempleCode}.aspx\" target=\"_blank\" title=\"{ev.TempleName}\">{ev.TempleName}</a>";
            }

            return DateTime.Now <= ev.Deadline.Value ? a_URL : string.Empty;
        }
    }
}