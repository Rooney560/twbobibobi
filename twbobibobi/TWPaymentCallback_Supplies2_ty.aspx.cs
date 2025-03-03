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
using Temple.data;

namespace twbobibobi
{
    public partial class TWPaymentCallback_Supplies2_ty : BasePage
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

                string m1 = Request["m1"];
                string m2 = Request["m2"];

                string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                string[] Supplieslist = new string[0];

                //mac = MD5.Encode(uid + tid + Timestamp + ValidationKey).Replace("-", "");
                string url = "https://paygate.tw/xpay/committrans?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

                string resp = "";
                if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
                {
                    //resp = "交易網址連結失敗";
                    SaveErrorLog(resp + ", 取得API錯誤。");
                }

                if (Request["ad"] != null)
                {
                    resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                }
                //resp = "1|18062216041218500003|100|TELEPAY|twm|0918101710|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                string orderId = oid;
                string CallbackLog = tid + "," + resp;
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(this);
                int aid = int.Parse(m1);
                int status = 999;
                int adminID = objDatabaseHelper.GetAdminID_Supplies2_ty(aid, Year);

                DataTable dtCharge = objDatabaseHelper.GetChargeLog_Supplies2_ty(orderId, Year);

                if (dtCharge.Rows.Count > 0)
                {
                    string rebackURL = "https://bobibobi.tw/Temples/templeService_Supplies2_ty.aspx";

                    if (dtCharge.Rows[0]["PayChannelLog"].ToString().IndexOf("bobi") > 0)
                    {
                        rebackURL = "https://bobibobi.tw/Temples/templeService_Supplies2_bobi_ty.aspx";
                    }

                    if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm")
                    {
                        rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&twm=1" : rebackURL + "?twm=1";
                    }

                    int cost = 0;
                    int.TryParse(dtCharge.Rows[0]["Amount"].ToString(), out cost);
                    int.TryParse(dtCharge.Rows[0]["Status"].ToString(), out status);
                    if (status == 0)
                    {
                        string[] result = resp.Split("|".ToCharArray());
                        if (result[0] == "1" || result[0] == "2")
                        {
                            string mobile = result[5];

                            objDatabaseHelper.UpdateSupplies2_ty_Info(aid, Year, ref Supplieslist);
                            objDatabaseHelper.Updateapplicantinfo_Supplies2_ty(aid, cost, 2, Year); //更新購買表內購買人狀態為已付款(Status=2)

                            string msg = "感謝購買,已成功付款" + cost + "元,您的訂單編號 ";

                            for (int i = 0; i < Supplieslist.Length; i++)
                            {
                                msg += Supplieslist[i];
                                if (i < Supplieslist.Length - 1)
                                {
                                    msg += ",";
                                }
                            }

                            msg += "。客服電話：04-36092299。";


                            //msg = "感謝大德參與線上點燈,茲收您1960元功德金,訂單編號 光明燈:T2204, 安太歲:25351, 文昌燈:六1214。";
                            //mobile = "0903002568";

                            SMSHepler objSMSHepler = new SMSHepler();
                            string ChargeType = string.Empty;

                            //更新流水付費表資訊(付費成功)
                            if (objDatabaseHelper.UpdateChargeLog_Supplies2_ty(orderId, tid, msg, Request.UserHostAddress, CallbackLog, Year, ref ChargeType))
                            {
                                if (objSMSHepler.SendMsg_SL(mobile, msg))
                                {
                                    m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?a=" + adminID + "&aid=" + aid + "&kind=14" + 
                                        (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" ? "&twm=1" : "");
                                    Response.Redirect(m2, true);
                                }
                                else
                                {
                                    Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('付款過程失敗。請聯繫管理員。錯誤代碼：109');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else if (result[0] == "4")
                        {
                            if (objDatabaseHelper.UpdateChargeStatus_Supplies2_ty(orderId, -2, Request.UserHostAddress, CallbackLog, Year))
                            {
                                Response.Write("<script>alert('此用戶已退款。');window.location.href='" + rebackURL + "'</script>");
                            }
                        }
                        else
                        {
                            objDatabaseHelper.UpdateChargeStatus_Supplies2_ty(orderId, -1, Request.UserHostAddress, CallbackLog, Year);

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
                    else if (status == 1)
                    {
                        //已經付費成功。
                        m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=14&a=" + adminID + "&aid=" + aid + 
                            (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" ? "&twm=1" : "");
                        Response.Redirect(m2, true);
                    }
                    else
                    {
                        Response.Write("<script>alert('此訂單已交易失敗，交易代碼：" + resp + "如有疑問。請洽客服電話：04-36092299。');" +
                            "window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=" + adminID + "'</script>");
                    }
                }
                else
                {
                    //resp = "invalid_orderid";
                    Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');" +
                        "window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=" + adminID + "'</script>");
                }

                //Response.Write(resp);
            }
            else
            {
                Response.Write("網頁參數錯誤");
                Response.End();
            }
        }
    }
}