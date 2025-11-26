using twbobibobi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace twbobibobi.FET.API
{
    public partial class CreateOrderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // 1. 讀取前端各個欄位
            int totalAmount = int.Parse(txtTotalAmount.Text);
            string fetOrderNumber = txtFetOrderNumber.Text.Trim();
            var applicant = new
            {
                name = txtAppName.Text.Trim(),
                mobile = txtAppMobile.Text.Trim(),
                receipt = txtAppReceipt.Text.Trim(),
                zipCode = txtAppZip.Text.Trim(),
                city = txtAppCity.Text.Trim(),
                region = txtAppRegion.Text.Trim(),
                address = txtAppAddress.Text.Trim(),
                send = txtAppSend.Text.Trim(),
                email = txtAppEmail.Text.Trim(),
                receiptMobile = txtAppReceiptMobile.Text.Trim(),
                birthdayType = txtAppBirthdayType.Text.Trim(),
                birthday = txtAppBirthday.Text.Trim(),
                lunarBirthday = txtAppLunarBirthday.Text.Trim(),
                lunarBirthTime = txtAppLunarTime.Text.Trim(),
                lunarLeap = txtAppLunarLeap.Text.Trim()
            };
            var item = new
            {
                productCode = txtProductCode.Text.Trim(),
                qty = int.Parse(txtQty.Text.Trim()),
                unitPrice = int.Parse(txtUnitPrice.Text.Trim()),
                prayedPerson = new[] {
                    new {
                        prayedPersonSeq  = int.Parse(txtPrayedSeq.Text.Trim()),
                        name             = txtPrayedName.Text.Trim(),
                        birthday         = txtPrayedBirthday.Text.Trim(),
                        lunarBirthday    = txtPrayedLunarBirthday.Text.Trim(),
                        leapMonth        = txtPrayedLeap.Text.Trim(),
                        oversea          = txtPrayedOversea.Text.Trim(),
                        zipCode          = txtPrayedZip.Text.Trim(),
                        city             = txtPrayedCity.Text.Trim(),
                        region           = txtPrayedRegion.Text.Trim(),
                        address          = txtPrayedAddress.Text.Trim(),
                        lunarBirthTime   = txtPrayedTime.Text.Trim(),
                        msisdn           = txtPrayedMsisdn.Text.Trim(),
                        offeringQty      = int.Parse(txtPrayedOfferingQty.Text.Trim())
                    }
                }
            };

            var requestData = new
            {
                totalAmount = totalAmount,
                fetOrderNumber = fetOrderNumber,
                applicant = applicant,
                items = new[] { item }
            };
            string rawJson = JsonConvert.SerializeObject(requestData);

            // 2. 環境與其他基本
            string env = rblEnv.SelectedValue;
            string channel = "FETnet";
            string clientOrderNumber = "CMPO20250518024922";
            //string transmitTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string transmitTime = "20250520000000";
            string Fkey = ConfigurationManager.AppSettings[$"EncryptionKey_F{env}"];
            string key = ConfigurationManager.AppSettings[$"EncryptionKey_{env}"];

            // 3. 加密
            JSonHelper mJSonHelper = new JSonHelper();
            string paramContent = AESHelper.AesEncrypt(rawJson, key);
            string fetValue = mJSonHelper.Sha256(channel + Fkey + clientOrderNumber);

            var payload = new
            {
                channel,
                clientOrderNumber,
                transmitTime,
                paramContent,
                FETVALUE = fetValue
            };
            string jsonPayload = JsonConvert.SerializeObject(payload);

            // 4. 呼叫 API 並顯示回應
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(txtApiUrl.Text.Trim());
                req.Method = "POST";
                req.ContentType = "application/json; charset=utf-8";
                using (var sw = new StreamWriter(req.GetRequestStream())) sw.Write(jsonPayload);
                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var sr = new StreamReader(resp.GetResponseStream()))
                    txtResult.Text = sr.ReadToEnd();
            }
            catch (Exception ex) { txtResult.Text = "Error: " + ex.Message; }
        }
    }
}