using System;
using System.Web.UI;
using twbobibobi.Data;

namespace twbobibobi.Temples
{
    public partial class EventTitbitsCategory : System.Web.UI.Page
    {
        public string eventList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) InitList();
        }
        private void InitList()
        {
            eventList = string.Empty;

            var list = CacheEvent.GetList();
            foreach (var item in list)
            {
                if (item.ResourceCount > 0 && item.FirstResourceId > 0 && !string.IsNullOrWhiteSpace(item.FirstResourceImageType))
                {
                    eventList += $"                        <div class=\"col p-4\">\r\n                            <h1 class=\"display-6 text-info\">{item.EventName} ({item.ResourceCount}筆)</h1>\r\n                            <a href=\"EventTitbits.aspx?eventId={item.Id}\" title=\"{item.EventName}\"><img src=\"SiteFile/EventTitbits/{item.Id}/{item.FirstResourceId}.{item.FirstResourceImageType}\" class=\"img-thumbnail\" alt=\"{item.EventName}\"></a>\r\n                        </div>\r\n";
                }
            }

            if (eventList.Length == 0)
                eventList = "<h1>（暫無記錄）</h1>";
        }
    }
}