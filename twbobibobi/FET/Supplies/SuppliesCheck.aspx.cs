using twbobibobi.Data;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.FET.Supplies
{
    public partial class SuppliesCheck : AjaxBasePage
    {
        public string ogurl = string.Empty;
        public string title = string.Empty;
        public string typeString = string.Empty;
        public string servicelist = string.Empty;
        public string OrderPurchaser = string.Empty;
        public string OrderInfo = string.Empty;
        public string EndDate = "2023/07/09 23:59";
        public static string Year = "2024";

        public int Total = 0;

        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "gotopay");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null && Request["aid"] != null)
                {
                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                    ogurl = Request.RawUrl.ToString();

                    int adminID = int.Parse(Request["a"]);
                    int ApplicantID = int.Parse(Request["aid"]);
                    int kind = int.Parse(Request["kind"]);
                    bool ExpirationDate = true;

                    switch (kind)
                    {
                        case 1:
                            //點燈服務
                            break;
                        case 2:
                            //普度服務
                            break;
                        case 3:
                            //商品販賣服務
                            //typeString = "商品販賣小舖";
                            ExpirationDate = false;

                            //switch (adminID)
                            //{
                            //    case 5:
                            //        //商品小舖
                            //        title = "文創商品販賣小舖";
                            //        GetPurchaserlist_da(adminID, ApplicantID, Year);          //大甲鎮瀾宮資料列表
                            //        Checkedtemple_da(adminID, ApplicantID, kind, Year);
                            //        EndDate = "2024/08/21 23:59";
                            //        break;
                            //}
                            break;
                        case 4:
                            //補財庫-下元補庫
                            break;
                        case 5:
                            //補財庫-呈疏補庫
                            break;
                        case 6:
                            //補財庫-企業補財庫
                            typeString = " " + dtNow.Year.ToString() + "企業補財庫";
                            ExpirationDate = false;
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies3(adminID, ApplicantID, Year);          //申請人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/22 23:59";
                                    bindPayButton(true, false, false, false, false, false);
                                    break;
                            }
                            break;
                    }

                    if ((dtNow >= DateTime.Parse(EndDate)) && ExpirationDate)
                    {
                        Response.Write("<script>alert('親愛的大德您好\\n " + title + typeString + "活動已截止！！\\n感謝您的支持, 謝謝!');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                    }

                    //switch (adminID)
                    //{
                    //    case 3:
                    //        //大甲鎮瀾宮
                    //        title = "大甲鎮瀾宮";
                    //        EndDate = "2023/08/21 23:59";
                    //        break;
                    //    case 4:
                    //        //新港奉天宮
                    //        title = "新港奉天宮";
                    //        EndDate = "2023/08/14 23:59";
                    //        break;
                    //    case 6:
                    //        //北港武德宮
                    //        title = "北港武德宮";
                    //        EndDate = "2023/08/22 23:59";
                    //        break;
                    //    case 8:
                    //        //西螺福興宮
                    //        title = "西螺福興宮";
                    //        EndDate = "2023/08/31 23:59";
                    //        break;
                    //    case 9:
                    //        //桃園大廟景福宮
                    //        title = "桃園大廟景福宮";
                    //        EndDate = "2023/08/25 23:59";
                    //        break;
                    //    case 10:
                    //        //台南正宗鹿耳門聖母廟
                    //        title = "台南正宗鹿耳門聖母廟";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 14:
                    //        //桃園威天宮
                    //        title = "桃園威天宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 15:
                    //        //斗六五路財神宮
                    //        title = "斗六五路財神宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 16:
                    //        //台東東海龍門天聖宮
                    //        title = "台東東海龍門天聖宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 17:
                    //        //五股賀聖宮
                    //        title = "五股賀聖宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 18:
                    //        //外澳接天宮
                    //        title = "外澳接天宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 19:
                    //        //安平開台天后宮
                    //        title = "安平開台天后宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 21:
                    //        //鹿港城隍廟
                    //        title = "鹿港城隍廟";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //}

                    //switch (kind)
                    //{
                    //    case 1:
                    //        //點燈服務
                    //        break;
                    //    case 2:
                    //        //普度服務
                    //        break;
                    //    case 3:
                    //        //商品販賣服務
                    //        break;
                    //    case 4:
                    //        //補財庫
                    //        break;
                    //    case 5:
                    //        //補財庫-呈疏補庫
                    //        break;
                    //    case 6:
                    //        //補財庫-企業補財庫
                    //        break;
                    //}


                    //if (dtNow >= DateTime.Parse(EndDate))
                    //{
                    //    Response.Write("<script>alert('親愛的大德您好\\n " + title + " 2023普度活動已截止！！\\n感謝您的支持, 謝謝!');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                    //}
                    //else
                    //{
                    //}
                }
                else
                {
                    Response.Write("<script>alert('訪問網址錯誤，請重新進入。');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                }
            }
        }

        public class AjaxPageHandler
        {
            public int ApplicantID = 0;
            public int AdminID = 0;
            public int kind = 0;
            public int Total = 0;

            public void gotopay(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                string ChargeType = basePage.Request["ChargeType"];
                string AppMobile = basePage.Request["AppMobile"];
                Total = int.Parse(basePage.Request["Total"]);
                //string discountCode = basePage.Request["discountCode"];

                ApplicantID = int.Parse(basePage.Request["aid"]);

                if (basePage.Request["a"] != null)
                {
                    AdminID = int.Parse(basePage.Request["a"].ToString());
                }

                if (basePage.Request["kind"] != null)
                {
                    kind = int.Parse(basePage.Request["kind"].ToString());
                }

                if (ApplicantID > 0 && AdminID > 0 && kind > 0)
                {
                    LightDAC objLightDAC = new LightDAC(basePage);

                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                    string orderId = dtNow.ToString("yyyyMMddHHmmssfff");
                    string Year = "2024";

                    //GetInfo(basePage);
                    //switch (AdminID)
                    //{
                    //    case 3:
                    //        //大甲鎮瀾宮
                    //        break;
                    //    case 4:
                    //        //新港奉天宮
                    //        break;
                    //    case 6:
                    //        //北港武德宮
                    //        break;
                    //    case 8:
                    //        //西螺福興宮
                    //        break;
                    //    case 9:
                    //        //桃園大廟景福宮
                    //        break;
                    //    case 10:
                    //        //台南正宗鹿耳門聖母廟
                    //        break;
                    //    case 10:
                    //        //桃園威天宮
                    //        break;
                    //    case 10:
                    //        //斗六五路財神宮
                    //        break;
                    //    case 10:
                    //        //台東東海龍門天聖宮
                    //        break;
                    //    case 10:
                    //        //五股賀聖宮
                    //        break;
                    //    case 10:
                    //        //外澳接天宮
                    //        break;
                    //    case 10:
                    //        //安平開台天后宮
                    //        break;
                    //}


                    if (Total > 0)
                    {
                        switch (kind)
                        {
                            case 1:
                                //點燈服務
                                break;
                            case 2:
                                //普度服務
                                break;
                            case 3:
                                //商品販賣服務
                                //typeString = "商品販賣小舖";

                                //switch (adminID)
                                //{
                                //    case 5:
                                //        //商品小舖
                                //        title = "文創商品販賣小舖";
                                //        GetPurchaserlist_da(adminID, ApplicantID, Year);          //大甲鎮瀾宮資料列表
                                //        Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                //        EndDate = "2024/08/21 23:59";
                                //        break;
                                //}
                                break;
                            case 4:
                                //補財庫-下元補庫
                                break;
                            case 5:
                                //補財庫-呈疏補庫
                                break;
                            case 6:
                                //補財庫-企業補財庫
                                Year = dtNow.Year.ToString();

                                DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 6, -1, Year);
                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                {
                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                }
                                else
                                {
                                    int cost = Total;
                                    string link = string.Empty;

                                    if (ChargeType == "LinePay")
                                    {
                                        link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮企業補財庫&orderId=" + orderId;
                                    }
                                    else
                                    {
                                        link = TWWebPay_supplies_wu3(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                    }

                                    if (link != "")
                                    {
                                        basePage.SavePayLog(link);

                                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                                        basePage.mJSonHelper.AddContent("redirect", link);

                                        basePage.Session["ApplicantID"] = ApplicantID;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            protected string TWWebPay_supplies_wu3(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeLightUp";    //北港武德宮宮廟服務 PR00004401
                string item = "北港武德宮企業補財庫";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_wu_ReceiveURL_FET");
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
                LightDAC objLightDAC = new LightDAC(basePage);
                long id = objLightDAC.AddChargeLog_Supplies_wu3(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu3(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

        }


        //申請人資料列表-北港武德宮
        public void GetPurchaserlist_wu_Supplies3(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getsupplies_wu_Info3(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderInfo = string.Empty;

                OrderInfo = OrderData("購買者姓名", dtData.Rows[0]["AppName"].ToString());

                string result = "<tr><td>{0} :</td>\r\n                      <td id='AppMobile'>{1}</td></tr>";
                OrderInfo += String.Format(result, "購買者電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderInfo += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += OrderData("服務項目", "企業補財庫");
                    OrderInfo += OrderData("被祈福者姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("被祈福者電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("被祈福者農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() != "N" ? " 閏月" : ""));
                    OrderInfo += OrderData("被祈福者農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("被祈福者市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("被祈福者Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("被祈福者居住地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 1300;
                    //OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    //OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_wu(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_wu.aspx";
                    if (objLightDAC.Checkedappcharge_Lights_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_wu.aspx";
                    if (objLightDAC.Checkedappcharge_Purdue_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    }
                    break;
                case 4:
                    //補財庫-下元補庫
                    reback = "https://bobibobi.tw/Temples/templeService_supplies.aspx";
                    if (objLightDAC.Checkedappcharge_Supplies_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 4, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    }
                    break;
                case 5:
                    //補財庫-呈疏補庫
                    reback = "https://bobibobi.tw/Temples/templeService_supplies2.aspx";
                    if (objLightDAC.Checkedappcharge_Supplies_wu2(ApplicantID, AdminID, Year))
                    {
                        if (OrderInfo == "")
                        {
                            Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 5, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    }
                    break;
                case 6:
                    //補財庫-企業補財庫
                    Year = dtNow.Year.ToString();
                    reback = "https://bobibobi.tw/FET/Supplies/SuppliesIndex.aspx";
                    if (objLightDAC.Checkedappcharge_Supplies_wu3(ApplicantID, AdminID, Year))
                    {
                        if (OrderInfo == "")
                        {
                            Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 6, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    }
                    break;
            }
        }


        //點燈燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈
        //protected int GetLightsCost(int AdminID, string LightsType)
        //{
        //    int result = 0;

        //    switch (AdminID)
        //    {
        //        case 3:
        //            //大甲鎮瀾宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 620;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 520;
        //                    break;
        //                case "5":
        //                    //文昌燈
        //                    result = 820;
        //                    break;
        //            }
        //            break;
        //        case 4:
        //            //新港奉天宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 620;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 620;
        //                    break;
        //            }
        //            break;
        //        case 6:
        //            //北港武德宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 600;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 600;
        //                    break;
        //                case "6":
        //                    //財神燈
        //                    result = 600;
        //                    break;
        //            }
        //            break;
        //        case 8:
        //            //西螺福興宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 600;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 300;
        //                    break;
        //                case "6":
        //                    //財神燈
        //                    result = 600;
        //                    break;
        //                case "8":
        //                    //藥師燈
        //                    result = 600;
        //                    break;
        //            }
        //            break;
        //        case 10:
        //            //台南正宗鹿耳門聖母廟
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 500;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 500;
        //                    break;
        //                case "5":
        //                    //文昌燈
        //                    result = 500;
        //                    break;
        //                case "7":
        //                    //姻緣燈
        //                    result = 999;
        //                    break;
        //                case "9":
        //                    //財利燈
        //                    result = 600;
        //                    break;
        //                case "11":
        //                    //福壽燈
        //                    result = 999;
        //                    break;
        //                case "12":
        //                    //寵物平安燈
        //                    result = 500;
        //                    break;
        //                case "20":
        //                    //月老姻緣燈
        //                    result = 999;
        //                    break;
        //            }
        //            break;
        //        case 14:
        //            //桃園威天宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 300;
        //                    break;
        //                case "4":
        //                    //太歲燈
        //                    result = 300;
        //                    break;
        //                case "6":
        //                    //財神燈
        //                    result = 600;
        //                    break;
        //                case "8":
        //                    //藥師燈
        //                    result = 600;
        //                    break;
        //                case "10":
        //                    //貴人燈
        //                    result = 600;
        //                    break;
        //                case "11":
        //                    //福祿燈
        //                    result = 600;
        //                    break;
        //            }
        //            break;
        //        case 15:
        //            //斗六五路財神宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //貴人燈(光明燈)
        //                    result = 500;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 500;
        //                    break;
        //                case "6":
        //                    //發財燈
        //                    result = 500;
        //                    break;
        //                case "7":
        //                    //桃花燈
        //                    result = 500;
        //                    break;
        //                case "19":
        //                    //財庫燈
        //                    result = 500;
        //                    break;
        //            }
        //            break;
        //        case 16:
        //            //台東東海龍門天聖宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 500;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 500;
        //                    break;
        //                case "5":
        //                    //文昌燈
        //                    result = 500;
        //                    break;
        //                case "9":
        //                    //財利燈
        //                    result = 500;
        //                    break;
        //                case "13":
        //                    //龍王燈
        //                    result = 800;
        //                    break;
        //                case "14":
        //                    //虎爺燈
        //                    result = 500;
        //                    break;
        //            }
        //            break;
        //        case 17:
        //            //五股賀聖宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 600;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 600;
        //                    break;
        //                case "5":
        //                    //文昌燈
        //                    result = 600;
        //                    break;
        //                case "8":
        //                    //藥師燈
        //                    result = 600;
        //                    break;
        //                case "9":
        //                    //財利燈
        //                    result = 600;
        //                    break;
        //            }
        //            break;
        //        case 18:
        //            //外澳接天宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 300;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 600;
        //                    break;
        //                case "6":
        //                    //財神燈
        //                    result = 600;
        //                    break;
        //            }
        //            break;
        //        case 19:
        //            //安平開台天后宮
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //光明燈
        //                    result = 620;
        //                    break;
        //                case "4":
        //                    //安太歲
        //                    result = 620;
        //                    break;
        //                case "5":
        //                    //文昌燈
        //                    result = 620;
        //                    break;
        //            }
        //            break;
        //        case 21:
        //            //鹿港城隍廟
        //            switch (LightsType)
        //            {
        //                case "3":
        //                    //元神光明燈
        //                    result = 500;
        //                    break;
        //                case "4":
        //                    //太歲平安符
        //                    result = 500;
        //                    break;
        //                case "5":
        //                    //文魁智慧燈
        //                    result = 500;
        //                    break;
        //                case "6":
        //                    //正財福報燈
        //                    result = 500;
        //                    break;
        //                case "15":
        //                    //轉運納福燈
        //                    result = 1000;
        //                    break;
        //                case "16":
        //                    //光明燈上層
        //                    result = 1000;
        //                    break;
        //                case "17":
        //                    //偏財旺旺燈
        //                    result = 500;
        //                    break;
        //                case "18":
        //                    //廣進安財庫
        //                    result = 300;
        //                    break;
        //            }
        //            break;
        //    }

        //    return result;
        //}

        protected string OrderData(string label, string text)
        {
            string result = "<tr><td>{0} :</td>\r\n                      <td>{1}</td></tr>";

            result = String.Format(result, label, text);

            return result;
        }

        protected string Ordertextarea(string text)
        {
            string result = "<p style='width: 100%; margin: 0 3px;'>{0}</p>";

            result = String.Format(result, text);

            return result;
        }

        protected void bindPayButton(bool fetCSP, bool card, bool linepay, bool jkospay, bool chtCSP, bool twmCSP)
        {
            if (Request["twm"] != null)
            {
                fetCSP = card = linepay = jkospay = chtCSP = false;
                twmCSP = true;
            }

            //this.fetPay.Visible = fetCSP;

            //this.cardPay.Visible = card;

            //this.LinePay.Visible = linepay;

            //this.JkosPay.Visible = jkospay;

            //this.chtPay.Visible = chtCSP;

            //this.twmPay.Visible = twmCSP;
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