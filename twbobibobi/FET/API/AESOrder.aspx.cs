using MotoSystem.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using LitJson;
using System.Data.SqlTypes;
using System.Security.Policy;
using Read.data;
using System.EnterpriseServices;
using System.Reflection;

namespace Temple.FET.API
{
    public partial class AESOrder : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           string  encryptText2 = string.Empty;
            string checkedkey = "lvrd5bidxr^dqlwX";

            encryptText2 = this.TextBox1.Text.Trim();

            if (encryptText2 != string.Empty)
            {
                string decrypt2 = AESHelper.AesDecrypt(encryptText2, checkedkey);

                this.Label2.Text = decrypt2;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string encryptText2 = string.Empty;
            string checkedkey = "shh#upsu6lyoeBkx";

            encryptText2 = this.TextBox2.Text.Trim();

            if (encryptText2 != string.Empty)
            {
                string decrypt2 = AESHelper.AesDecrypt(encryptText2, checkedkey);

                this.Label4.Text = decrypt2;
            }
        }
    }
}