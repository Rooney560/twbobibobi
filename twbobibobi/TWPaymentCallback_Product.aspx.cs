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
    public partial class TWPaymentCallback_Product : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            {
                string uid = "Temple";
                string tid = Request["tid"];
                string oid = Request["oid"];
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string Timestamp = dt.ToString("yyyyMMddHHmmssfff");

                string Year = "2024";

                string m1 = Request["m1"];
                string m2 = Request["m2"];

                string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                string[] productlist = new string[0];
                string[] Productlist = new string[0];

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
                LightDAC objLightDAC = new LightDAC(this);
                int aid = int.Parse(m1);
                int status = 999;
                int AdminID = 0;

                DataTable dtCharge = objLightDAC.GetChargeLog_Product(orderId, Year);

                if (dtCharge.Rows.Count > 0)
                {
                    int adminID = objLightDAC.GetAdminID_Product(aid, Year);
                    int cost = 0;
                    int.TryParse(dtCharge.Rows[0]["Amount"].ToString(), out cost);
                    int.TryParse(dtCharge.Rows[0]["Status"].ToString(), out status);
                    if (status == 0)
                    {
                        string rebackURL = "https://bobibobi.tw/Templeproduct.aspx?a=" + adminID;

                        if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm")
                        {
                            rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&twm=1" : rebackURL + "?twm=1";
                        }

                        string[] result = resp.Split("|".ToCharArray());
                        if (result[0] == "1" || result[0] == "2")
                        {
                            if (result.Length > 1)
                            {
                                string mobile = result[5];

                                //int adminID = 5;

                                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + cost + "元，您的訂單編號 ";

                                //更新普渡資料表並取得訂單編號
                                objLightDAC.UpdateProductInfo(aid, adminID, Year, ref msg, ref productlist, ref Productlist);
                                //取得申請人資料表
                                //DataTable dtapplicantinfo = objLightDAC.Getapplicantinfo_Product(aid, adminID, Year);
                                ////更新購買表內購買人狀態為已付款(Status=2)
                                //int cost = dtapplicantinfo.Rows.Count > 0 ? int.Parse(dtapplicantinfo.Rows[0]["Cost"].ToString()) : 0;
                                objLightDAC.Updateapplicantinfo_Product(aid, cost, 2, Year);

                                objLightDAC.UpdateCount2Product(aid, dt.Year.ToString()); //更新購買數量至商品表or商品類別表

                                //for (int i = 0; i < productlist.Length; i++)
                                //{
                                //    msg += productlist[i];
                                //    if (i < productlist.Length - 1)
                                //    {
                                //        msg += ",";
                                //    }
                                //}

                                //msg += "。客服電話：04-36092299。";


                                //msg = "感謝大德參與線上點燈,茲收您1960元功德金,訂單編號 光明燈:T2204, 安太歲:25351, 文昌燈:六1214。";
                                //mobile = "0903002568";

                                SMSHepler objSMSHepler = new SMSHepler();
                                string ChargeType = string.Empty;
                                int uStatus = 0;
                                //更新流水付費表資訊(付費成功)
                                if (objLightDAC.UpdateChargeLog_Product(orderId, tid, msg, Request.UserHostAddress, CallbackLog, Year, ref ChargeType, ref uStatus))
                                {
                                    if (objSMSHepler.SendMsg_SL(mobile, msg))
                                    {

                                        m2 = m2.IndexOf("aid=") > 0 ? m2 : (m2.IndexOf("?") > 0 ? m2 + "&aid=" + m1 : m2 + "?aid=" + m1 + "&a=" + adminID);
                                        //m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=3&a=" + adminID + "&aid=" + aid + (ChargeType == "Twm" ? "&twm=1" : "");
                                        Response.Redirect(m2, true);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('付款過程失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                            else
                            {
                                SaveErrorLog(resp + ", 回傳值 錯誤!");
                                Response.Write("<script>window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else if (result[0] == "4")
                        {
                            if (objLightDAC.UpdateChargeStatus_Product(orderId, -2, Request.UserHostAddress, CallbackLog, Year))
                            {
                                Response.Write("<script>alert('此用戶已退款。');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else
                        {
                            objLightDAC.UpdateChargeStatus_Product(orderId, -1, Request.UserHostAddress, CallbackLog, Year);

                            if (m2.IndexOf("APPPaymentResult") > 0)
                            {
                                Response.Redirect(m2, true);
                            }
                            else
                            {
                                Response.Write("<script>alert('付款失敗，錯誤代碼：" + result[0] + "。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                    }
                    else if (status == 1)
                    {
                        //已經付費成功。
                        m2 = m2.IndexOf("aid=") > 0 ? m2 : (m2.IndexOf("?") > 0 ? m2 + "&aid=" + m1 : m2 + "?aid=" + m1 + "&a=" + adminID);
                        //m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=3&a=" + adminID + "&aid=" + aid + (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" ? "&twm=1" : "");
                        Response.Redirect(m2, true);
                    }
                    else
                    {
                        Response.Write("<script>alert('此訂單已交易失敗，交易代碼：" + resp + "如有疑問。請洽客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Templeproduct.aspx?a=5'</script>");
                    }
                }
                else
                {
                    //resp = "invalid_orderid";
                    Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Templeproduct.aspx?a=5'</script>");
                }

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