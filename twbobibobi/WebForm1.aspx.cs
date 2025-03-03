using MotoSystem.Data;
using Org.BouncyCastle.Asn1.Ocsp;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace twbobibobi
{
    public partial class WebForm1 : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["cost"] != null && Request["ad"] != null)
            {
                if (Request["ad"] == "55688")
                {
                    string link = TWWebPay_taoistJiaoCeremony_da("20241029124447217", 459, "LINEPAY", "", 10, "0934315020", "a=3&aid=459&kind=13", "2024");
                    Response.Redirect(link);
                }
            }

            if (Request["msg"] != null && Request["mobile"] != null)
            {
                string msg = Request["msg"];
                string mobile = Request["mobile"];

                SMSHepler objSMSHepler = new SMSHepler();
                if (objSMSHepler.SendMsg_SL(mobile, msg))
                {

                }
            }
        }


        protected string TWWebPay_taoistJiaoCeremony_da(string orderid, int applicantID, string paytype, string telco, int price, string m_phone,
            string returnUrl, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
            string oid = orderid;
            string uid = "Temple";
            string Sid = "Temple-DajiaCeremony";    //大甲鎮瀾宮普渡法會(CSENT64199)
            string item = "大甲鎮瀾宮重修慶成祈安七朝清醮活動";
            string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
            string link = "https://paygate.tw/xpay/pay?uid=";
            string PaymentReceiveURL = "https://bobibobi.tw/TWPaymentCallback_TaoistJiaoCeremony_da.aspx";
            string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");
            string msisdn = m_phone;
            string chrgtype = "1";
            string m1 = applicantID.ToString();
            string m2 = returnUrl;
            //string mac = MD5.Encode(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
            //                      + telco + chrgtype + Timestamp + ValidationKey).Replace("-", "").ToLower();
            string mac = MD5.MD5Encrypt(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
                                  + telco + chrgtype + msisdn + Timestamp + ValidationKey).Replace("-", "").ToLower();

            string paymentChannelLog = returnUrl;
            DatabaseHelper objdatabaseHelper = new DatabaseHelper(basePage);
            string ChargeType = paytype;
            if (ChargeType == "TELEPAY")
            {
                if (telco == "twm")
                {
                    ChargeType = "Twm";
                }
                else
                {
                    ChargeType = "Cht";
                }
            }

            link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

            return link;
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