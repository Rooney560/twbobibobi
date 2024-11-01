using MotoSystem.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AL.Common;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Temple.data
{
    public class SMSHepler
    {

        public bool SendMsg_SL(string mobile, string msg)
        {
            bool result = true;

            JSonHelper objJsonHelper = new JSonHelper();

            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string Url = "https://www.sand-la.com/api/mt";

            string userid = "mazu-payment";
            string senderid = "";
            string isflash = "0";
            string country = "886";
            string msisdn = CheckedMobileTW(mobile);
            string msgid = "111";
            string msgtype = "0";
            string data = msg;
            string timestamp = dt.ToString("yyyyMMddHHmmssfff");
            string ValidationKey = "dsp25901";
            string mac = MD5.MD5Encrypt(userid + senderid + isflash + country + msisdn + data + msgtype + timestamp + ValidationKey).Replace("-", "").ToLower();

            objJsonHelper.AddContent("userid", userid);
            objJsonHelper.AddContent("senderid", senderid);
            objJsonHelper.AddContent("isflash", isflash);
            objJsonHelper.AddContent("country", country);
            objJsonHelper.AddContent("msisdn", msisdn);
            objJsonHelper.AddContent("msgid", msgid);
            objJsonHelper.AddContent("msgtype", msgtype);
            objJsonHelper.AddContent("data", data);
            objJsonHelper.AddContent("timestamp", timestamp);
            objJsonHelper.AddContent("mac", mac);


            string jsonString = objJsonHelper.ToJSonString();
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            jsonString = reg.Replace(jsonString, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

            //string messages_resp = "";
            //if (!BCFBaseLibrary.Net.HTTPClient.PostSSL(Url, jsonString, ref messages_resp))
            //{
            //    messages_resp = "交易網址連結失敗";
            //    result = false;
            //}

            string messages_resp = "";
            if (!HttpPost(Url, jsonString, ref messages_resp, "application/json;charset=utf-8"))
            {
                result = false;
            }

            return result;
        }

        public bool HttpPost(string url, string strPostData, ref string resp, string contentType = "application/x-www-form-urlencoded", Dictionary<string, string> headers = null)
        {
            bool result = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 1000;
            try
            {
                Encoding encoding = ((!Tools.containUnicodeChars(strPostData)) ? Encoding.ASCII : Encoding.UTF8);
                byte[] bytes = encoding.GetBytes(strPostData);
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.ContentLength = bytes.Length;
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        httpWebRequest.Headers.Add(header.Key, header.Value);
                    }
                }

                string log = "Header: " + httpWebRequest.Headers.ToString() + ", JsonData: " + strPostData;

                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    Stream stream2 = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream2);
                    resp = streamReader.ReadToEnd();
                }

                httpWebRequest.Abort();
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                resp = ex.Message;
                return result;
            }
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public bool SendMsg_MK(string mobile, string msg)
        {
            bool result = true;

            string data = HttpUtility.UrlEncode(msg, System.Text.Encoding.GetEncoding("big5"));

            //Response.Write("<script>alert('成功');window.location.href='https://bobibobi.tw/'</script>");
            string messageslink = "https://tw.mktwservice.com/smsgw/default.aspx?username=temple&password=tpl511&telco=fet&shortcode=55828927&msisdn={0}&action=mt&format=950&billing=0&data={1}";
            messageslink = string.Format(messageslink, mobile, data);
            string messages_resp = "";
            if (!BCFBaseLibrary.Net.HTTPClient.Get(messageslink, string.Empty, ref messages_resp))
            {
                messages_resp = "交易網址連結失敗";
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 簡訊內容分割，69字數為一則
        /// </summary>
        /// <param name="inputString">參數字符串</param>
        /// <param name="msg">回傳內容陣列</param>
        /// <returns></returns>
        public void msgSubstring(string inputString, ref ArrayList msg)
        {
            int tempLen = inputString.Length;
            int num = tempLen / 69 + 1;
            if (num > 1)
            {
                int str = 0;
                for (int i = 0; i < num; i++)
                {
                    int strLen = 69;
                    if ((inputString.Length - str) / 69 == 0)
                    {
                        strLen = inputString.Length - str;
                    }
                    msg.Add(inputString.Substring(str, strLen));
                    str += 69;
                }
            }
        }

        /// <summary>
        /// 得到字符串長度，一個漢字長度為2
        /// </summary>
        /// <param name="inputString">參數字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString, ref ArrayList msg)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            bool first = true;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                if (tempLen >= 69 && tempLen < 138 && first)
                {
                    msg.Add(inputString.Substring(0, i));
                    first = false;
                }
            }
            return tempLen;
        }

        public string CheckedMobileTW(string mobile)
        {
            string result = string.Empty;

            if (mobile.Length == 10)
            {
                result = "886" + mobile.Substring(1, 9);
            }
            else
            {
                result = mobile;
            }

            return result;
        }

        public class MD5
        {
            public static string MD5Encrypt(string str)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            public static string Encode(string text)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(text)));
            }
        }
    }
}