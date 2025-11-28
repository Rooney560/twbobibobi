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

namespace twbobibobi
{
    public partial class PaymentCallback_Lights : BasePage
    {
        private static object _thisLock = new object();
        protected void Page_Load(object sender, EventArgs e)
        {
            lock (_thisLock)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                if (Request["aid"] != null && Request["oid"] != null && Request["tid"] != null && Request["resp"] != null && Request["msg"] != null)
                {
                    string Year = dtNow.Year.ToString();
                    int aid = 0;
                    int.TryParse(Request["aid"], out aid);
                    string tid = Request["tid"];
                    string oid = Request["oid"];
                    string resp = Request["resp"];
                    string msg = Request["msg"];

                    string[] lightslist = new string[0];
                    string[] Lightslist = new string[0];

                    if (Request["ad"] != null)
                    {
                        resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                    }
                    string orderId = oid;
                    string CallbackLog = resp;
                    LightDAC objLightDAC = new LightDAC(this);
                    int status = 999;

                    DataTable dtCharge = objLightDAC.GetChargeLog_Lights_da(orderId, Year);

                    if (dtCharge.Rows.Count > 0)
                    {
                        if (aid == 0)
                        {
                            int.TryParse(dtCharge.Rows[0]["ApplicantID"].ToString(), out aid);
                        }

                        int cost = 0;
                        int.TryParse(dtCharge.Rows[0]["Amount"].ToString(), out cost);
                        int.TryParse(dtCharge.Rows[0]["Status"].ToString(), out status);
                        if (status == 0)
                        {
                            int lightstype = objLightDAC.GetLightsType_da(aid, Year);

                            string rebackURL = "https://bobibobi.tw/Temples/templeService_lights_da.aspx";

                            if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm")
                            {
                                rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&twm=1" : rebackURL + "?twm=1";
                            }

                            //string mobile = objLightDAC.GetMobile_da(aid, Year);

                            //int adminID = 3;

                            ////更新普渡資料表並取得訂單編號
                            //objLightDAC.UpdateLights_da_Info(aid, lightstype, Year, ref msg, ref lightslist, ref Lightslist);

                        }
                        else if (status == 1)
                        {
                            //已經付費成功。
                            //m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=1&a=3&aid=" + aid + (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" ? "&twm=1" : "");
                            //Response.Redirect(m2, true);
                        }
                        else
                        {
                            SaveErrorLog(resp + ", 此訂單已交易失敗!");
                            Response.Write("<script>alert('此訂單已交易失敗，交易代碼：" + resp + "如有疑問。請洽客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=3'</script>");
                        }
                    }
                    else
                    {
                        //resp = "invalid_orderid";
                        SaveErrorLog(resp + ", 取得付款資料失敗!");
                        Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=3'</script>");
                    }

                }
                else
                {
                    Response.Write("網頁參數錯誤");
                    Response.End();
                }
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