using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twbobibobi.Temples
{
    public partial class newsContent_2025purdue : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        public string purl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["purl"] != null)
            {
                purl = "?purl=" + Request["purl"];
            }
        }
    }
}