using LitJson;
using twbobibobi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.FET.API
{
    public partial class AESOrder : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sr = new StreamReader(Request.InputStream);//读取流
            string URL = Request.Url.Authority;
            string checkedkey = "lvrd5bidxr^dqlwX";

            var stream = sr.ReadToEnd();//读取所有数据
            if (stream != "")
            {
                string decrypt2 = AESHelper.AesEncrypt(stream, checkedkey);
                Response.Write(decrypt2);
            }
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            string encryptText2 = string.Empty;
            string checkedkey = "shh#upsu6lyoeBkx";

            encryptText2 = this.TextBox3.Text.Trim();

            if (encryptText2 != string.Empty)
            {
                string decrypt2 = AESHelper.AesEncrypt(encryptText2, checkedkey);

                this.Label6.Text = decrypt2;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string channel = "FETnet";
            string clientOrderNumber = string.Empty;

            string env = "UAT";
            string FkeyName = $"EncryptionKey_F{env}";
            string Fkey = ConfigurationManager.AppSettings[FkeyName];

            clientOrderNumber = this.TextBox4.Text.Trim();

            if (clientOrderNumber != string.Empty)
            {
                string decrypt2 = AESHelper.Sha256(channel + Fkey + clientOrderNumber);

                this.Label8.Text = decrypt2;
            }
        }
    }
}