using MotoSystem.Data;
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

namespace Temple.FET.Lights
{
    public partial class LightsCheck : AjaxBasePage
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
                            typeString = " 2024點燈";

                            int type = 0;
                            if (Request["type"] != null)
                            {
                                type = int.Parse(Request["type"]);
                            }

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_da_Lights(adminID, ApplicantID, Year);          //大甲鎮瀾宮資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetPurchaserlist_h_Lights(adminID, ApplicantID, Year);          //申請人資料列表
                                    Checkedtemple_h(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Lights(adminID, ApplicantID, Year);          //申請人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/02/04 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetPurchaserlist_Fu_Lights(adminID, ApplicantID, Year);          //西螺福興宮資料列表
                                    Checkedtemple_Fu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 10:
                                    //台南正宗鹿耳門聖母廟
                                    title = "台南正宗鹿耳門聖母廟";
                                    GetPurchaserlist_Luer_Lights(adminID, ApplicantID, Year);          //申請人資料列表
                                    Checkedtemple_Luer(adminID, ApplicantID, kind, Year, type);
                                    EndDate = "2024/06/30 23:59";
                                    if (type == 2)
                                    {

                                    }
                                    else if (type == 3)
                                    {
                                        ExpirationDate = false;
                                        bindPayButton(true, false, false, false, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(true, false, false, false, false, false);
                                    }
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Lights(adminID, ApplicantID, Year);          //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_Lights(adminID, ApplicantID, Year);          //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Lights(adminID, ApplicantID, Year);          //台東東海龍門天聖宮資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Lights(adminID, ApplicantID, Year);          //鹿港城隍廟資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/06/30 23:59";
                                    bindPayButton(true, true, false, true, true, false);
                                    break;
                            }
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
                                DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                {
                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                }
                                else
                                {
                                    int cost = Total;
                                    string link = string.Empty;
                                    string name = "保必保庇線上服務平台";

                                    switch (AdminID)
                                    {
                                        case 3:
                                            //大甲鎮瀾宮
                                            name = "大甲鎮瀾宮點燈服務";
                                            DataTable dtData = objLightDAC.Getlights_da_info(ApplicantID, Year);

                                            int[] count_da_lights = new int[3];
                                            string[] lightstypelist = new string[dtData.Rows.Count];
                                            bool checkednum_da = true;
                                            for (int i = 0; i < dtData.Rows.Count; i++)
                                            {
                                                lightstypelist[i] = dtData.Rows[i]["LightsType"].ToString();
                                                switch (dtData.Rows[i]["LightsType"].ToString())
                                                {
                                                    case "3":
                                                        //光明燈
                                                        count_da_lights[0]++;
                                                        break;
                                                    case "4":
                                                        //安太歲
                                                        count_da_lights[1]++;
                                                        break;
                                                    case "5":
                                                        //文昌燈
                                                        count_da_lights[2]++;
                                                        break;
                                                }
                                            }

                                            //string[] Lightstypelist = new string[] { "3", "4", "5" };
                                            string[] LightsStringlist = new string[] { "光明燈", "安太歲", "文昌燈" };
                                            for (int i = 0; i < lightstypelist.Length; i++)
                                            {
                                                if (objLightDAC.checkedLightsNum(lightstypelist[i], AdminID.ToString(), count_da_lights[i], -1, Year))
                                                {
                                                    checkednum_da = false;

                                                    basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                    basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                    if (basePage.Request["ad"] != null)
                                                    {
                                                        checkednum_da = true;
                                                    }

                                                    break;
                                                }
                                            }

                                            if (checkednum_da)
                                            {
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_lights_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }


                                                //if (ChargeType == "LinePay")
                                                //{
                                                //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                //}
                                                //else
                                                //{
                                                //    link = TWWebPay_lights_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                //}
                                            }
                                            break;
                                        case 4:
                                            //新港奉天宮
                                            name = "新港奉天宮點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 6:
                                            //北港武德宮
                                            name = "北港武德宮點燈服務";
                                            dtData = objLightDAC.Getlights_wu_info(ApplicantID, Year);

                                            int[] count_wu_lights = new int[3];
                                            lightstypelist = new string[dtData.Rows.Count];
                                            bool checkednum_wu = true;
                                            for (int i = 0; i < dtData.Rows.Count; i++)
                                            {
                                                lightstypelist[i] = dtData.Rows[i]["LightsType"].ToString();
                                                switch (dtData.Rows[i]["LightsType"].ToString())
                                                {
                                                    case "3":
                                                        //光明燈
                                                        count_wu_lights[0]++;
                                                        break;
                                                    case "4":
                                                        //安太歲
                                                        count_wu_lights[1]++;
                                                        break;
                                                    case "6":
                                                        //財神燈
                                                        count_wu_lights[2]++;
                                                        break;
                                                }
                                            }

                                            //Lightstypelist = new string[] { "3", "4", "6" };
                                            LightsStringlist = new string[] { "光明燈", "安太歲", "財神燈" };
                                            for (int i = 0; i < lightstypelist.Length; i++)
                                            {
                                                if (objLightDAC.checkedLightsNum(lightstypelist[i], AdminID.ToString(), count_wu_lights[i], -1, Year))
                                                {
                                                    checkednum_wu = false;

                                                    basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                    basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                    break;
                                                }
                                            }

                                            if (checkednum_wu)
                                            {
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }

                                                //if (ChargeType == "LinePay")
                                                //{
                                                //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                //}
                                                //else
                                                //{
                                                //    link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                //}
                                            }
                                            break;
                                        case 8:
                                            //西螺福興宮
                                            name = "西螺福興宮點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 10:
                                            //台南正統鹿耳門聖母廟
                                            name = "遠傳祈福點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 14:
                                            //桃園威天宮
                                            name = "桃園威天宮點燈服務";
                                            dtData = objLightDAC.Getlights_ty_info(ApplicantID, Year);

                                            int[] count_ty_lights = new int[5];
                                            lightstypelist = new string[dtData.Rows.Count];
                                            bool checkednum_ty = true;
                                            for (int i = 0; i < dtData.Rows.Count; i++)
                                            {
                                                lightstypelist[i] = dtData.Rows[i]["LightsType"].ToString();
                                                switch (dtData.Rows[i]["LightsType"].ToString())
                                                {
                                                    case "3":
                                                        //光明燈
                                                        count_ty_lights[0]++;
                                                        break;
                                                    case "4":
                                                        //太歲燈
                                                        count_ty_lights[1]++;
                                                        break;
                                                    case "6":
                                                        //財神燈
                                                        count_ty_lights[2]++;
                                                        break;
                                                    case "8":
                                                        //藥師燈
                                                        count_ty_lights[3]++;
                                                        break;
                                                    case "10":
                                                        //貴人燈
                                                        count_ty_lights[4]++;
                                                        break;
                                                }
                                            }

                                            //Lightstypelist = new string[] { "3", "4", "6", "8", "10" };
                                            LightsStringlist = new string[] { "光明燈", "太歲燈", "財神燈", "藥師燈", "貴人燈" };
                                            for (int i = 0; i < lightstypelist.Length; i++)
                                            {
                                                if (objLightDAC.checkedLightsNum(lightstypelist[i], AdminID.ToString(), count_ty_lights[i], 1, Year))
                                                {
                                                    checkednum_ty = false;

                                                    basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                    basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                    break;
                                                }
                                            }

                                            if (checkednum_ty)
                                            {
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }

                                                //if (ChargeType == "LinePay")
                                                //{
                                                //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                //}
                                                //else
                                                //{
                                                //    link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                //}
                                            }
                                            break;
                                        case 15:
                                            //斗六五路財神宮
                                            name = "斗六五路財神宮點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 16:
                                            //台東東海龍門天聖宮
                                            name = "台東東海龍門天聖宮點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 17:
                                            //五股賀聖宮
                                            name = "五股賀聖宮點燈服務";

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 18:
                                            //外澳接天宮
                                            name = "外澳接天宮點燈服務";

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Jt(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 19:
                                            //安平開台天后宮
                                            name = "安平開台天后宮點燈服務";

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Am(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
                                        case 21:
                                            //鹿港城隍廟
                                            name = "鹿港城隍廟點燈服務";

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "JkosPay":
                                                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                            }

                                            //if (ChargeType == "LinePay")
                                            //{
                                            //    link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                            //}
                                            //else
                                            //{
                                            //    link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                            //}
                                            break;
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
                                break;
                        }
                    }
                }
            }

            protected string TWWebPay_lights_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DaJiaLightUp";    //大甲鎮瀾宮祈福點燈 PR00004024
                string item = "大甲鎮瀾宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_da_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_da(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_h(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-HsinKangLightUp";    //新港奉天宮祈福點燈 PR00004008
                string item = "新港奉天宮祈福點燈";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_h_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_h(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_h(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeLightUp";    //北港武德宮 宮廟服務 PR00004401
                string item = "北港武德宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_wu_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_wu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Fu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-XiluoFuyu";    //西螺福興宮_宮廟服務 00004588
                string item = "西螺福興宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Fu_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Fu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Fu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Luer(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Luermen";    //台南正統鹿耳門聖母廟_宮廟服務 PR00004609
                string item = "台南正統鹿耳門聖母廟點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Luer_ReceiveURL_FET");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Luer(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Luer(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務 PR00004719
                string item = "桃園威天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_ty_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YunlinWulucaishen";    //斗六五路財神宮_宮廟服務 PR00004721
                string item = "斗六五路財神宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Fw_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Fw(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務 PR00004720
                string item = "台東東海龍門天聖宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_dh_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_dh(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Hs(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DaJiaLightUp";    //五股賀聖宮祈福點燈 PR00004024
                string item = "五股賀聖宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Hs_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Hs(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Hs(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Jt(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DaJiaLightUp";    //外澳接天宮祈福點燈 PR00004024
                string item = "外澳接天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Jt_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Jt(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Jt(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Am(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DaJiaLightUp";    //安平開台天后宮祈福點燈 PR00004024
                string item = "安平開台天后宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Am_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Am(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Am(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-LugangChenghuangmiao";    //鹿港城隍廟祈福點燈 PR00004755
                string item = "鹿港城隍廟點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://tw.mktwservice.com/atPay/pay.aspx?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Lk_ReceiveURL");
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
                long id = objdatabaseHelper.AddChargeLog_Lights_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Lk(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }
        }

        //申請人資料列表-大甲鎮瀾宮
        public void GetPurchaserlist_da_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_da_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    //lightsString = lightsType == "2" ? dtData.Rows[i]["FirstName"].ToString() + "家堂上歷代九玄七祖" : lightsString;
                    //lightsString = lightsType == "6" ? dtData.Rows[i]["FirstName"].ToString() + "家嬰靈" : lightsString;
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);


                    //switch (lightsType)
                    //{
                    //    case "3":
                    //        cost += 620;
                    //        break;
                    //    case "4":
                    //        cost += 520;
                    //        break;
                    //    case "5":
                    //        cost += 820;
                    //        break;
                    //}

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_da(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_da.aspx";
                    if (objLightDAC.checkedappcharge_Lights_da(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_da.aspx";
                    if (objLightDAC.checkedappcharge_Purdue_da(ApplicantID, AdminID))
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
            }

        }


        //申請人資料列表-新港奉天宮
        public void GetPurchaserlist_h_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_h_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_h(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_h.aspx";
                    if (objLightDAC.checkedappcharge_Lights_h(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_h.aspx";
                    if (objLightDAC.checkedappcharge_Purdue_h(ApplicantID, AdminID))
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
            }
        }


        //申請人資料列表-北港武德宮
        public void GetPurchaserlist_wu_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_wu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    string sex = "善男";
                    if (dtData.Rows[i]["Sex"].ToString() == "F")
                    {
                        sex = "信女";
                    }
                    OrderInfo += OrderData("性別", sex);
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                    OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());
                    //OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    OrderInfo += OrderData("備註", dtData.Rows[i]["Remark"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
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
                    if (objLightDAC.checkedappcharge_Lights_wu(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_Purdue_wu(ApplicantID, AdminID))
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
                    if (objLightDAC.checkedappcharge_Supplies_wu(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_Supplies_wu2(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies3.aspx";
                    if (objLightDAC.checkedappcharge_Supplies_wu3(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
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


        //申請人資料列表-西螺福興宮
        public void GetPurchaserlist_Fu_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Fu(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fu.aspx";
                    if (objLightDAC.checkedappcharge_Lights_Fu(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Fu.aspx";
                    if (objLightDAC.checkedappcharge_Purdue_Fu(ApplicantID, AdminID))
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
            }

        }


        //申請人資料列表-台南正統鹿耳門聖母廟
        public void GetPurchaserlist_Luer_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Luer_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderInfo = string.Empty;

                OrderInfo = OrderData("購買者姓名", dtData.Rows[0]["AppName"].ToString());

                string result = "<tr><td>{0} :</td>\r\n                      <td id='AppMobile'>{1}</td></tr>";
                OrderInfo += String.Format(result, "購買者電話", dtData.Rows[0]["AppMobile"].ToString());

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    OrderInfo += OrderData("服務項目", lightsString);

                    if (dtData.Rows[i]["PetName"].ToString() != "")
                    {
                        OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("飼主農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                        OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString() == "" ? "吉" : dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("飼主地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                        OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                        OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                    }
                    else
                    {
                        OrderInfo += OrderData("被祈福者姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("被祈福者電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("被祈福者農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() != "N" ? " 閏月" : ""));
                        OrderInfo += OrderData("被祈福者農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("被祈福者居住地址", dtData.Rows[i]["Address"].ToString());
                    }

                    if (dtData.Rows[i]["Msg"].ToString() != "")
                    {
                        OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    //OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    //OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Luer(int AdminID, int ApplicantID, int kind, string Year, int type)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            //點燈服務
            string reback = "https://bobibobi.tw/FET/Lights/LightsIndex_Luer.aspx";
            //if (type == 2)
            //{
            //    reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer.aspx";
            //}
            //else if (type == 3)
            //{
            //    reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer_twm.aspx?twm=1";
            //}
            if (objLightDAC.checkedappcharge_Lights_Luer(ApplicantID, AdminID, Year))
            {
                if (OrderInfo == "")
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
        }


        //申請人資料列表-桃園威天宮
        public void GetPurchaserlist_ty_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_ty_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString());
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ty(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_ty.aspx";
                    if (objLightDAC.checkedappcharge_Lights_ty(ApplicantID, AdminID, Year))
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
                    //reback = "https://bobibobi.tw/Temples/templeService_purdue_ty.aspx";
                    //if (objLightDAC.checkedappcharge_Purdue_ty(ApplicantID, AdminID))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    //}
                    break;
            }

        }


        //申請人資料列表-斗六五路財神宮
        public void GetPurchaserlist_Fw_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fw_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString());
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Fw(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fw.aspx";
                    if (objLightDAC.checkedappcharge_Lights_Fw(ApplicantID, AdminID, Year))
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
                    //reback = "https://bobibobi.tw/Temples/templeService_purdue_Fw.aspx";
                    //if (objLightDAC.checkedappcharge_Purdue_Fw(ApplicantID, AdminID))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    //}
                    break;
            }

        }


        //申請人資料列表-台東東海龍門天聖宮
        public void GetPurchaserlist_dh_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_dh_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString());
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_dh(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_dh.aspx";
                    if (objLightDAC.checkedappcharge_Lights_dh(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_dh.aspx";
                    //if (objLightDAC.checkedappcharge_Purdue_dh(ApplicantID, AdminID))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    //}
                    break;
            }

        }

        //申請人資料列表-鹿港城隍廟
        public void GetPurchaserlist_Lk_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Lk_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("申請人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                OrderPurchaser += String.Format(result, "申請人電話", dtData.Rows[0]["AppMobile"].ToString());

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    string sex = "善男";
                    if (dtData.Rows[i]["Sex"].ToString() == "F")
                    {
                        sex = "信女";
                    }
                    OrderInfo += OrderData("性別", sex);
                    OrderInfo += OrderData("農歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? "閏月" : ""));
                    OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", (dtData.Rows[i]["ZipCode"].ToString() != "" ? dtData.Rows[i]["ZipCode"].ToString() + " " : "") + dtData.Rows[i]["Address"].ToString());

                    if (dtData.Rows[i]["Sendback"].ToString() == "Y")
                    {
                        cost += 100;
                        OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
                        OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
                        OrderInfo += OrderData("收件人地址", (dtData.Rows[i]["rZipCode"].ToString() != "" ? dtData.Rows[i]["rZipCode"].ToString() + " " : "") + dtData.Rows[i]["rAddress"].ToString());
                    }

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Lk(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Lk.aspx";
                    if (objLightDAC.checkedappcharge_Lights_Lk(ApplicantID, AdminID, Year))
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
                    //reback = "https://bobibobi.tw/Temples/templeService_purdue_Lk.aspx";
                    //if (objLightDAC.checkedappcharge_Purdue_Lk(ApplicantID, AdminID))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取申請人資料錯誤。請重新申請。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此申請人付款時間已超時20分鐘，請重新申請。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此申請人已進行付款動作。請重新申請。');location='" + reback + "'</script>");
                    //}
                    break;
            }

        }


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