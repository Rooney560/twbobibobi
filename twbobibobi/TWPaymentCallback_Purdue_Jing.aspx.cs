using BCFBaseLibrary.Security;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Temple.data;
using TempleAdmin.Helper;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;

namespace Temple
{
    public partial class TWPaymentCallback_Purdue_Jing : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            //DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            //if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            //{
            //    string uid = "Temple";
            //    string tid = Request["tid"];
            //    string oid = Request["oid"];
            //    string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
            //    string Timestamp = dt.ToString("yyyyMMddHHmmssfff");


            //    string m1 = Request["m1"];
            //    string m2 = Request["m2"];

            //    string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

            //    string[] purduelist = new string[0];

            //    string url = "https://paygate.tw/xpay/committrans?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

            //    string resp = "";
            //    if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
            //    {
            //        resp = "交易網址連結失敗";
            //    }
            //    //resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
            //    string orderId = oid;
            //    string CallbackLog = tid + "," + resp;
            //    DatabaseHelper objDatabaseHelper = new DatabaseHelper(this);

            //    DataTable dtCharge = objDatabaseHelper.GetChargeLog_Purdue_Jing(orderId);

            //    if (dtCharge.Rows.Count > 0)
            //    {
            //        if ((int)dtCharge.Rows[0]["Status"] == 0)
            //        {
            //            string[] result = resp.Split("|".ToCharArray());
            //            if (result[0] == "1" || result[0] == "2")
            //            {
            //                string mobile = result[5];

            //                int aid = int.Parse(m1);

            //                int adminID = 9;

            //                //更新普渡資料表並取得訂單編號
            //                objDatabaseHelper.UpdatePurdue_Jing_Info(aid, ref purduelist);
            //                //取得申請人資料表
            //                DataTable dtapplicantinfo = objDatabaseHelper.Getapplicantinfo_Purdue_Jing(aid, adminID);
            //                //更新購買表內購買人狀態為已付款(Status=2)
            //                objDatabaseHelper.Updateapplicantinfo_Purdue_Jing(aid); 

            //                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + dtapplicantinfo.Rows[0]["Cost"].ToString() + "元，您的訂單編號 ";

            //                for (int i = 0; i < purduelist.Length; i++)
            //                {
            //                    msg += purduelist[i];
            //                    if (i < purduelist.Length - 1)
            //                    {
            //                        msg += ",";
            //                    }
            //                }

            //                msg += "。客服電話：04-36092299。";


            //                //msg = "感謝大德參與線上點燈,茲收您1960元功德金,訂單編號 光明燈:T2204, 安太歲:25351, 文昌燈:六1214。";
            //                //mobile = "0903002568";

            //                SMSHepler objSMSHepler = new SMSHepler();

            //                //更新流水付費表資訊(付費成功)
            //                if (objDatabaseHelper.UpdateChargeLog_Purdue_Jing(orderId, tid, msg, Request.UserHostAddress, CallbackLog))
            //                {
            //                    if (objSMSHepler.SendMsg_SL(mobile, msg))
            //                    {

            //                        //m2 = m2.IndexOf("aid=") > 0 ? m2 : (m2.IndexOf("?") > 0 ? m2 + "&aid=" + m1 : m2 + "?aid=" + m1 + "&a=" + adminID);
            //                        m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?a=" + adminID + "&aid=" + aid;
            //                        Response.Redirect(m2, true);
            //                    }
            //                    else
            //                    {
            //                        Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeService_purdue.aspx?a=9'</script>");
            //                    }
            //                }
            //                else
            //                {
            //                    Response.Write("<script>alert('付款過程失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeService_purdue.aspx?a=9'</script>");
            //                }
            //            }
            //            else if (result[0] == "4")
            //            {
            //                if (objDatabaseHelper.UpdateChargeStatus_Purdue_Jing(orderId, -2))
            //                {
            //                    Response.Write("<script>alert('此用戶已退款。');window.location.href='https://bobibobi.tw/Temples/templeService_purdue.aspx?a=9'</script>");
            //                }
            //            }
            //            else
            //            {
            //                objDatabaseHelper.UpdateChargeErrLog_Purdue_Jing(orderId, "", Request.UserHostAddress, CallbackLog);

            //                if (m2.IndexOf("APPPaymentResult") > 0)
            //                {
            //                    Response.Redirect(m2, true);
            //                }
            //                else
            //                {
            //                    Response.Write("<script>alert('付款失敗，錯誤代碼：" + result[0] + "。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeService_purdue.aspx?a=9'</script>");
            //                }
            //            }
            //        }

            //    }
            //    else
            //    {
            //        resp = "invalid_orderid";
            //    }

            //    Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeService_purdue.aspx?a=9'</script>");
            //}
            //else
            //{
            //    Response.Write("網頁參數錯誤");
            //    Response.End();
            //}
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
    }
}