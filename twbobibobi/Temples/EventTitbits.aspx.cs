using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using twbobibobi.Data;

namespace twbobibobi.Temples
{
    public partial class EventTitbits : System.Web.UI.Page
    {
        public string mainList = string.Empty;
        public string navList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack) InitList();
        }
        private void InitList()
        {
            mainList = string.Empty;
            navList = string.Empty;
            if (Request["eventId"] == null || !int.TryParse(Request["eventId"], out int eventId)) return;
            

            var list = CacheEventTitbits.GetListByEveintId(eventId);
            string tmp;
            foreach (var item in list)
            {
                if (item.Type == 2)
                    tmp = item.ResourceUri;
                else
                    tmp = $"<img src=\"SiteFile/EventTitbits/{item.EventId}/{item.Id}.{item.ImageFileType}\" />";

                mainList += $"                            <div class=\"swiper-slide swiper-slide-container\">\r\n                                {tmp}\r\n                            </div>\r\n";
                navList += $"                            <div class=\"swiper-slide\">\r\n                                <img src=\"SiteFile/EventTitbits/{item.EventId}/{item.Id}.{item.ImageFileType}\" />\r\n                            </div>\r\n";
            }
        }
    }
}