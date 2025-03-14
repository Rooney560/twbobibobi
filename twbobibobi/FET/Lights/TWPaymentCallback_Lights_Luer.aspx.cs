﻿using System;
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
using Temple.data;

namespace Temple.FET.Lights
{
    public partial class TWPaymentCallback_Lights_Luer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            {
                string Year = dtNow.Year.ToString();
                string uid = "Temple";
                //string Sid = "Temple-LightUp";
                string tid = Request["tid"];
                string oid = Request["oid"];
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");
                string rebackURL = "https://bobibobi.tw/FET/Lights/LightsIndex_Luer.aspx";
                //string rebackURL = "https://localhost:44329/FET/Lights/LightsIndex_Luer.aspx";

                string m1 = Request["m1"];
                string m2 = Request["m2"];

                string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                string[] lightslist = new string[0];
                string[] Lightslist = new string[0];

                //mac = MD5.Encode(uid + tid + Timestamp + ValidationKey).Replace("-", "");
                string url = "https://paygate.tw/xpay/committrans?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

                string resp = "";
                if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
                {
                    //resp = "交易網址連結失敗";
                    SaveErrorLog(resp);
                }
                if (Request["ad"] != null)
                {
                    resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                }
                //resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                string orderId = oid;
                string CallbackLog = tid + "," + resp;
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(this);

                DataTable dtCharge = objDatabaseHelper.GetChargeLog_Lights_Luer(orderId, Year);

                int cost = 0;
                int.TryParse(dtCharge.Rows[0]["Amount"].ToString(), out cost);
                //int.TryParse(dtCharge.Rows[0]["Status"].ToString(), out status);
                if (dtCharge.Rows.Count > 0)
                {
                    if ((int)dtCharge.Rows[0]["Status"] == 0)
                    {
                        string[] result = resp.Split("|".ToCharArray());
                        if (result[0] == "1" || result[0] == "2")
                        {
                            string mobile = result[5];

                            int aid = int.Parse(m1);

                            int type = objDatabaseHelper.GetLightsType_Luer(aid, Year);

                            int adminID = 10;

                            string msg = "感謝購買,已成功付款" + cost + "元,您的訂單編號 ";

                            objDatabaseHelper.UpdateLights_Luer_Info(aid, type, Year, ref msg, ref lightslist, ref Lightslist);
                            //DataTable dtapplicantinfo = objDatabaseHelper.Getapplicantinfo_Lights_Luer(aid, adminID, Year);
                            //int cost = int.Parse(dtapplicantinfo.Rows[0]["Cost"].ToString());
                            objDatabaseHelper.Updateapplicantinfo_Lights_Luer(aid, cost, 2, Year); //更新購買表內購買人狀態為已付款(Status=2)

                            //msg = "感謝大德參與線上點燈,茲收您1960元功德金,訂單編號 光明燈:T2204, 安太歲:25351, 文昌燈:六1214。";
                            //mobile = "0903002568";

                            SMSHepler objSMSHepler = new SMSHepler();
                            string ChargeType = string.Empty;

                            //更新流水付費表資訊(付費成功)
                            if (objDatabaseHelper.UpdateChargeLog_Lights_Luer(orderId, tid, msg, Request.UserHostAddress, CallbackLog, Year, ref ChargeType))
                            {
                                m2 = "https://bobibobi.tw/FET/Lights/LightsComplete.aspx?a=" + adminID + "&aid=" + aid + "&kind=1";
                                //m2 = "https://localhost:44329/FET/Lights/LightsComplete.aspx?a=" + adminID + "&aid=" + aid + "&kind=1";
                                Response.Redirect(m2, true);
                                //if (objSMSHepler.SendMsg_SL(mobile, msg))
                                //{
                                //    //m2 = m2.IndexOf("aid=") > 0 ? m2 : (m2.IndexOf("?") > 0 ? m2 + "&aid=" + m1 : m2 + "?aid=" + m1 + "&a=" + adminID);
                                //}
                                //else
                                //{
                                //    Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                //}
                            }
                            else
                            {
                                Response.Write("<script>alert('付款過程失敗。請聯繫管理員。錯誤代碼：109');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else if (result[0] == "4")
                        {
                            if (objDatabaseHelper.UpdateChargeStatus_Lights_Luer(orderId, -2, Request.UserHostAddress, CallbackLog, Year))
                            {
                                Response.Write("<script>alert('此用戶已退款。');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else
                        {
                            objDatabaseHelper.UpdateChargeStatus_Lights_Luer(orderId, -1, Request.UserHostAddress, CallbackLog, Year);
                            //objDatabaseHelper.UpdateChargeErrLog_Lights_Luer(orderId, "", Request.UserHostAddress, CallbackLog, Year);

                            if (m2.IndexOf("APPPaymentResult") > 0)
                            {
                                Response.Redirect(m2, true);
                            }
                            else
                            {
                                Response.Write("<script>alert('付款失敗。錯誤代碼：" + result[0] + "，請聯繫管理員。');window.location.href='" + rebackURL + "'</script>");
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