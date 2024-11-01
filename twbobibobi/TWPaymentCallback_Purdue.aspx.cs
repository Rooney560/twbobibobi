using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BCFBaseLibrary.Web;
using Read.data;
using BCFBaseLibrary.Security;
using System.Collections;

namespace Temple
{
    public partial class TWPaymentCallback_Purdue : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            {
                string uid = "Temple";
                //string Sid = "Temple-LightUp";
                string tid = Request["tid"];
                string oid = Request["oid"];
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string Timestamp = dt.ToString("yyyyMMddHHmmssfff");


                string m1 = Request["m1"];
                string m2 = Request["m2"];

                string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                string[] zamplist = new string[0];
                string[] salvationlist = new string[0];

                //mac = MD5.Encode(uid + tid + Timestamp + ValidationKey).Replace("-", "");
                string url = "https://tw.mktwservice.com/atPay/CommitTrans.aspx?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

                string resp = "";
                if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
                {
                    resp = "交易網址連結失敗";
                }
                //resp = "1|18062216041218500003|100|TELEPAY|twm|0918101710|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                string orderId = oid;
                string CallbackLog = tid + "," + resp;
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(this);

                DataTable dtCharge = objDatabaseHelper.GetChargeLog_Purdue(orderId);

                if (dtCharge.Rows.Count > 0)
                {
                    if ((int)dtCharge.Rows[0]["Status"] == 0)
                    {
                        string[] result = resp.Split("|".ToCharArray());
                        if (result[0] == "1" || result[0] == "2")
                        {
                            string mobile = result[5];

                            int aid = int.Parse(m1);
                            //m2 = System.Web.HttpUtility.UrlDecode(m2);
                            //string b = "https://localhost:44329/confirmlights.aspx?aid=31&a=3&type=3&c=0";
                            //string[] c = m1.Split('&');
                            //int adminID = int.Parse(c[1].Substring(c[1].IndexOf("=") + 1, c[1].Length - 2));
                            //int type = int.Parse(c[2].Substring(c[2].IndexOf("=") + 1, c[2].Length - 5));

                            int adminID = objDatabaseHelper.GetAdminID_Purdue(aid);


                            objDatabaseHelper.UpdateZampNum2(aid, ref zamplist);
                            objDatabaseHelper.UpdateSalvationNum2(aid, ref salvationlist);
                            DataTable dtapplicantinfo = objDatabaseHelper.Getapplicantinfo_Purdue(aid, adminID);
                            objDatabaseHelper.Updateapplicantinfo_Purdue(aid); //更新購買表內購買人狀態為已付款(Status=2)

                            string msg = "感謝購買,已成功付款" + dtapplicantinfo.Rows[0]["Cost"].ToString() + "元,您的訂單編號 ";

                            for (int i = 0; i < zamplist.Length; i++)
                            {
                                msg += zamplist[i];
                                if (i < zamplist.Length - 1)
                                {
                                    msg += ",";
                                }
                            }
                            if (salvationlist.Length > 0)
                            {
                                msg += ",";
                            }
                            for (int i = 0; i < salvationlist.Length; i++)
                            {
                                msg += salvationlist[i];
                                if (i < salvationlist.Length - 1)
                                {
                                    msg += ",";
                                }
                            }

                            msg += "。於農曆7/10(國曆8/7) 進行法會科儀。客服電話：04-23582760。";


                            //msg = "感謝大德參與線上點燈,茲收您1960元功德金,訂單編號 光明燈:T2204, 安太歲:25351, 文昌燈:六1214。";
                            //mobile = "0903002568";

                            ArrayList myArrayList = new ArrayList();
                            msgSubstring(msg, ref myArrayList);

                            //string messageslink = "https://tw.mktwservice.com/smsgw/default.aspx?username=temple&password=tpl511&telco=fet&shortcode=55828927&msisdn={0}&action=mt&format=950&billing=0&data={1}";
                            //string data = HttpUtility.UrlEncode(msg, System.Text.Encoding.GetEncoding("big5"));
                            //messageslink = string.Format(messageslink, mobile, data);
                            if (objDatabaseHelper.UpdateChargeLog_Purdue(orderId, tid, msg, Request.UserHostAddress, CallbackLog))
                            {
                                if (myArrayList.Count > 0)
                                {
                                    for (int i = 0; i < myArrayList.Count; i++)
                                    {
                                        string data = HttpUtility.UrlEncode(myArrayList[i].ToString(), System.Text.Encoding.GetEncoding("big5"));

                                        //Response.Write("<script>alert('成功');window.location.href='https://bobibobi.tw/'</script>");
                                        string messageslink = "https://tw.mktwservice.com/smsgw/default.aspx?username=temple&password=tpl511&telco=fet&shortcode=55828927&msisdn={0}&action=mt&format=950&billing=0&data={1}";
                                        messageslink = string.Format(messageslink, mobile, data);
                                        string messages_resp = "";
                                        if (!BCFBaseLibrary.Net.HTTPClient.Get(messageslink, string.Empty, ref messages_resp))
                                        {
                                            messages_resp = "交易網址連結失敗";
                                        }
                                    }
                                }
                                else
                                {
                                    string data = HttpUtility.UrlEncode(msg, System.Text.Encoding.GetEncoding("big5"));

                                    //Response.Write("<script>alert('成功');window.location.href='https://bobibobi.tw/'</script>");
                                    string messageslink = "https://tw.mktwservice.com/smsgw/default.aspx?username=temple&password=tpl511&telco=fet&shortcode=55828927&msisdn={0}&action=mt&format=950&billing=0&data={1}";
                                    messageslink = string.Format(messageslink, mobile, data);
                                    string messages_resp = "";
                                    if (!BCFBaseLibrary.Net.HTTPClient.Get(messageslink, string.Empty, ref messages_resp))
                                    {
                                        messages_resp = "交易網址連結失敗";
                                    }
                                }


                                //string b = "aHR0cHM6Ly9ib2JpYm9iaS50dy9MaWdodHNMb2cuYXNweD9haW";
                                //m2 = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(m2));
                                Response.Redirect(m2, true);
                            }
                            else
                            {
                                Response.Write("<script>alert('付款過程失敗。請聯繫管理員。錯誤代碼：109');window.location.href='https://bobibobi.tw/'</script>");
                            }
                        }
                        else if (result[0] == "4")
                        {
                            if (objDatabaseHelper.UpdateChargeStatus_Purdue(orderId, -2))
                            {
                                Response.Write("<script>alert('此用戶已退款。');window.location.href='https://bobibobi.tw/'</script>");
                            }
                        }
                        else
                        {
                            objDatabaseHelper.UpdateChargeErrLog_Purdue(orderId, "", Request.UserHostAddress, CallbackLog);

                            if (m2.IndexOf("APPPaymentResult") > 0)
                            {
                                Response.Redirect(m2, true);
                            }
                            else
                            {
                                Response.Write("<script>alert('付款失敗。錯誤代碼：" + result[0] + "，請聯繫管理員。');window.location.href='https://bobibobi.tw/'</script>");
                            }

                            //Response.Redirect(m2, true);
                        }
                    }

                }
                else
                {
                    resp = "invalid_orderid";
                }

                Response.Write(resp);
            }
            else
            {
                Response.Write("網頁參數錯誤");
                Response.End();
            }
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