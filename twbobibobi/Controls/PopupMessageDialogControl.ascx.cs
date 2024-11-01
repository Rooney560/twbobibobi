using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EChatAdmin.Controls
{
    public partial class PopupMessageDialogControl : System.Web.UI.UserControl
    {
        public string PopupMessage = "";
        public string RedirectUrl = "";

        public void ShowPopupMessage(string message, string url)
        {
            PopupMessage = message;
            RedirectUrl = url;
            Page.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}