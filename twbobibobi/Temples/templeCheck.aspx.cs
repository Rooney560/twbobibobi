using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Temple.data;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Services;
using ZXing.QrCode.Internal;
using static System.Net.Mime.MediaTypeNames;
using static ZXing.QrCode.Internal.Mode;

namespace Temple.Temples
{
    public partial class templeCheck : AjaxBasePage
    {
        private static readonly object _thisLock = new object();

        // API 的相對路徑（放在 /Api 資料夾底下）
        private const string MobileCarrierApiUrl = "/Api/InvoiceMobileCarrierAPI.aspx";
        public string AppMobile = string.Empty;
        public string AppEmail = string.Empty;
        public string ogurl = string.Empty;
        public string title = string.Empty;
        public string typeString = string.Empty;
        public string servicelist = string.Empty;
        public string OrderPurchaser = string.Empty;
        public string OrderInfo = string.Empty;
        public string EndDate = "2023/07/09 23:59";
        public static string Year = "2025";

        public int Total = 0;

        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "gotopay");
            AddAjaxHandler(typeof(AjaxPageHandler), "sendsms");
            AddAjaxHandler(typeof(AjaxPageHandler), "verifyOTP");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null && Request["aid"] != null)
                {
                    // 取得台北標準時間
                    DateTime dtNow = LightDAC.GetTaipeiNow();

                    ogurl = Request.RawUrl.ToString() + "?a=" + Request["a"] + "&aid=" + Request["aid"] + "&kind=" + Request["kind"]; ;

                    int adminID = int.Parse(Request["a"]);
                    int ApplicantID = int.Parse(Request["aid"]);
                    int kind = int.Parse(Request["kind"]);
                    bool ExpirationDate = true;
                    bool payStatus = false;

                    switch (kind)
                    {
                        //點燈服務
                        case 1:
                            typeString = " 2025點燈";
                            string startDate = "2025/11/01 00:00:00";
                            int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || Request["ad"] == "2")
                            {
                                typeString = " 2026點燈";
                                Year = "2026";
                            }

                            int type = 1;
                            if (Request["type"] != null)
                            {
                                type = int.Parse(Request["type"]);
                            }

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_da_Lights(adminID, ApplicantID, Year, ref payStatus);          //大甲鎮瀾宮資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/02/08 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetPurchaserlist_h_Lights(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_h(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Lights(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/01/31 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetPurchaserlist_Fu_Lights(adminID, ApplicantID, Year, ref payStatus);          //西螺福興宮資料列表
                                    Checkedtemple_Fu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetPurchaserlist_Luer_Lights(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_Luer(adminID, ApplicantID, kind, Year, type);
                                    EndDate = "2026/10/31 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (type == 2)
                                            {
                                                //月老姻緣燈
                                            }
                                            else if (type == 3)
                                            {
                                                //月老姻緣燈-台哥大專屬
                                                ExpirationDate = false;
                                                bindPayButton(false, false, false, false, false, true, false, false, false);
                                            }
                                            else
                                            {
                                                bindPayButton(true, false, false, false, true, true, false, false, false);
                                                //一般點燈
                                                //if (payStatus)
                                                //{
                                                //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                                //}
                                                //else
                                                //{
                                                //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                                //}
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Lights(adminID, ApplicantID, 1, Year, ref payStatus);          //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, type, Year);
                                    EndDate = "2026/09/15 23:59";

                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_Lights(adminID, ApplicantID, Year, ref payStatus);          //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getlights_Fw_Info(ApplicantID, Year);

                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Lights(adminID, ApplicantID, Year, ref payStatus);          //台東東海龍門天聖宮資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    // 先把字串解析成 DateTime
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 17:
                                    //五股賀聖宮
                                    title = "五股賀聖宮";
                                    GetPurchaserlist_Hs_Lights(adminID, ApplicantID, Year, ref payStatus);          //五股賀聖宮資料列表
                                    Checkedtemple_Hs(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/06/30 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Lights(adminID, ApplicantID, Year, ref payStatus);          //鹿港城隍廟資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(true, false, false, false, true, true, false, false, false);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Lights(adminID, ApplicantID, Year, ref payStatus);          //玉敕大樹朝天宮資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 29:
                                    //進寶財神廟
                                    title = "進寶財神廟";
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_Lights(adminID, ApplicantID, Year, ref payStatus);          //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 32:
                                    //桃園龍德宮
                                    title = "桃園龍德宮";
                                    GetPurchaserlist_ld_Lights(adminID, ApplicantID, Year, ref payStatus);          //桃園龍德宮資料列表
                                    Checkedtemple_ld(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(fetCSP: true, card: true, linepay: true, jkospay: true, chtCSP: false, twmCSP: true, union: false, pxpaypluspay: true, applepay: true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 35:
                                    //松柏嶺受天宮
                                    title = "松柏嶺受天宮";
                                    GetPurchaserlist_st_Lights(adminID, ApplicantID, Year, ref payStatus);          //松柏嶺受天宮資料列表
                                    Checkedtemple_st(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(true, true, true, false, true, true, false, true, true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 38:
                                    //池上北極玄天宮
                                    title = "池上北極玄天宮";
                                    GetPurchaserlist_bj_Lights(adminID, ApplicantID, Year, ref payStatus);          //池上北極玄天宮資料列表
                                    Checkedtemple_bj(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(fetCSP: true, card: true, linepay: true, jkospay: true, chtCSP: false, twmCSP: true, union: false, pxpaypluspay: true, applepay: true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 39:
                                    //花蓮慈惠石壁部堂
                                    title = "花蓮慈惠石壁部堂";
                                    GetPurchaserlist_sbbt_Lights(adminID, ApplicantID, Year, ref payStatus);          //花蓮慈惠石壁部堂資料列表
                                    Checkedtemple_sbbt(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(fetCSP: true, card: true, linepay: true, jkospay: true, chtCSP: false, twmCSP: true, union: false, pxpaypluspay: true, applepay: true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 40:
                                    //新北真武山受玄宮
                                    title = "新北真武山受玄宮";
                                    GetPurchaserlist_bpy_Lights(adminID, ApplicantID, Year, ref payStatus);          //新北真武山受玄宮資料列表
                                    Checkedtemple_bpy(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(fetCSP: true, card: true, linepay: true, jkospay: true, chtCSP: false, twmCSP: true, union: false, pxpaypluspay: true, applepay: true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 41:
                                    //桃園壽山巖觀音寺
                                    title = "桃園壽山巖觀音寺";
                                    GetPurchaserlist_ssy_Lights(adminID, ApplicantID, Year, ref payStatus);          //桃園壽山巖觀音寺資料列表
                                    Checkedtemple_ssy(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(fetCSP: true, card: true, linepay: true, jkospay: true, chtCSP: false, twmCSP: true, union: false, pxpaypluspay: true, applepay: true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //普度服務
                        case 2:
                            typeString = " 2025普度";

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_da_Purdue(adminID, ApplicantID, Year, ref payStatus);             //大甲鎮瀾宮資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/27 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetPurchaserlist_h_Purdue(adminID, ApplicantID, Year, ref payStatus);              //新港奉天宮資料列表
                                    Checkedtemple_h(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/15 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Purdue(adminID, ApplicantID, Year, ref payStatus);             //北港武德宮資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/23 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetPurchaserlist_Fu_Purdue(adminID, ApplicantID, Year, ref payStatus);                    //西螺福興宮人資料列表
                                    Checkedtemple_Fu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/09/09 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetPurchaserlist_Luer_Purdue(adminID, ApplicantID, Year, ref payStatus);           //台南正統鹿耳門聖母廟資料列表
                                    Checkedtemple_Luer(adminID, ApplicantID, kind, Year, 0);
                                    EndDate = "2025/08/28 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(true, false, false, false, true, true, false, false, false);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Purdue(adminID, ApplicantID, Year, ref payStatus);             //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/09/09 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_Purdue(adminID, ApplicantID, Year, ref payStatus);             //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/09/14 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Purdue(adminID, ApplicantID, Year, ref payStatus);             //台東東海龍門天聖宮資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/09/19 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Purdue(adminID, ApplicantID, Year, ref payStatus);             //鹿港城隍廟資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/21 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(true, false, false, false, true, true, false, false, false);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Purdue(adminID, ApplicantID, Year, ref payStatus);             //玉敕大樹朝天宮資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/25 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_Purdue(adminID, ApplicantID, Year, ref payStatus);           //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/09/17 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //商品販賣服務
                        case 3:
                            //typeString = "商品販賣小舖";
                            Year = dtNow.Year.ToString();
                            ExpirationDate = false;
                            break;
                        //下元補庫
                        case 4:
                            typeString = " 2025下元補庫";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/11/26 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //呈疏補庫
                        case 5:
                            typeString = " 2025天官武財神聖誕補財庫";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies2(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/03/24 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //企業補財庫
                        case 6:
                            typeString = " " + dtNow.Year.ToString() + "企業補財庫";
                            //ExpirationDate = false;
                            //Year = dtNow.Year.ToString();

                            //switch (adminID)
                            //{
                            //    case 6:
                            //        //北港武德宮
                            //        title = "北港武德宮";
                            //        GetPurchaserlist_wu_Supplies3(adminID, ApplicantID, Year);          //購買人資料列表
                            //        Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                            //        EndDate = "2024/08/22 23:59";
                            //        bindPayButton(false, false, false, false, false, false, false, false, false);
                            //        break;
                            //}
                            break;
                        //天赦日招財補運
                        case 7:
                            typeString = " 2025天赦日招財補運";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/12/18 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Supplies(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/20 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //天赦日祭改
                        case 8:
                            typeString = " 2024天赦日祭改";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 29:
                                    //進寶財神廟
                                    title = "進寶財神廟";
                                    //GetPurchaserlist_ty_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    //Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    //EndDate = "2024/05/28 23:59";
                                    HidePayButton();
                                    break;
                            }
                            break;
                        //關聖帝君聖誕
                        case 9:
                            typeString = " 2025關聖帝君聖誕千秋祝壽謝恩祈福活動";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_EmperorGuansheng(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/08/12 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //代燒金紙
                        case 10:
                            break;
                        //天貺納福添運法會
                        case 11:
                            typeString = " 2025天貺納福添運法會";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Supplies(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //靈寶禮斗
                        case 12:
                            typeString = " 2025靈寶禮斗";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Lingbaolidou(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/11/06 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //七朝清醮
                        case 13:
                            typeString = " 七朝清醮";
                            Year = "2024";

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_da_TaoistJiaoCeremony(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/12/01 23:59";
                                    HidePayButton();
                                    break;
                            }
                            break;
                        //九九重陽天赦日補運
                        case 14:
                            typeString = " 2024九九重陽天赦日雙重加持招財補運";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies2(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2024/10/09 16:00";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getsupplies2_ty_Info(ApplicantID, Year);
                                    bool checkedPayStatus = true;

                                    foreach (DataRow dr in dtData.Rows)
                                    {
                                        if (dr["SuppliesType"].ToString() == "8")
                                        {
                                            checkedPayStatus = false;
                                            break;
                                        }
                                    }

                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (checkedPayStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //護國息災梁皇大法會
                        case 15:
                            typeString = " 護國息災梁皇大法會";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Lybc(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/11/10 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //補財庫
                        case 16:
                            typeString = " 補財庫";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 15:
                                    //斗六五路財神宮
                                    ExpirationDate = false;
                                    GetPurchaserlist_Fw_Supplies(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (Request["fetsms"] != null || Request["purl"] == "fetsms")
                                            {
                                                bindPayButton(true, false, false, false, false, false, false, false, false);
                                            }
                                            else
                                            {
                                                if (payStatus)
                                                {
                                                    bindPayButton(false, true, true, true, false, false, false, true, true);
                                                }
                                                else
                                                {
                                                    bindPayButton(true, true, true, true, true, true, false, true, true);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Supplies(adminID, ApplicantID, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/25 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out  endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (Request["fetsms"] != null || Request["purl"] == "fetsms")
                                            {
                                                bindPayButton(true, false, false, false, false, false, false, false, false);
                                            }
                                            else
                                            {
                                                bindPayButton(true, false, false, false, true, true, false, false, false);
                                                //if (payStatus)
                                                //{
                                                //    bindPayButton(false, true, true, true, false, false, false, true, true);
                                                //}
                                                //else
                                                //{
                                                //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                                //}
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //赦罪補庫
                        case 17:
                            typeString = " 赦罪補庫";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 33:
                                    //神霄玉府財神會館
                                    title = "神霄玉府財神會館";
                                    GetPurchaserlist_sx_Supplies(adminID, ApplicantID, kind, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_sx(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(false, true, true, false, false, false, false, true, false);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, false, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //天公生招財補運
                        case 18:
                            typeString = " 2025天公生招財補運";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies3(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/02/03 23:59";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getsupplies3_ty_Info(ApplicantID, Year);
                                    bool checkedPayStatus = true;

                                    foreach (DataRow dr in dtData.Rows)
                                    {
                                        if (dr["SuppliesType"].ToString() == "8")
                                        {
                                            checkedPayStatus = false;
                                            break;
                                        }
                                    }
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (checkedPayStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //供香轉運
                        case 19:
                            typeString = " 供香轉運";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 33:
                                    //神霄玉府財神會館
                                    ExpirationDate = false;
                                    title = "神霄玉府財神會館";
                                    GetPurchaserlist_sx_Supplies(adminID, ApplicantID, kind, Year, ref payStatus);          //購買人資料列表
                                    Checkedtemple_sx(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(false, true, true, false, false, false, false, true, false);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, false, false, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, false, true, true, false, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //安斗服務
                        case 20:
                            typeString = " 2026安斗";
                            Year = "2026";

                            switch (adminID)
                            {
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_AnDou(adminID, ApplicantID, Year, ref payStatus);          //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_AnDou(adminID, ApplicantID, Year, ref payStatus);          //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/10/31 23:59";
                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //供花供果服務
                        case 21:
                            typeString = " 2025供花供果";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_Huaguo(adminID, ApplicantID, Year, ref payStatus);          //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";

                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //孝親祈福燈服務
                        case 22:
                            typeString = " 2025孝親祈福燈";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Lights(adminID, ApplicantID, 2, Year, ref payStatus);          //桃園威天宮資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/05/08 23:59";

                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //祈安植福
                        case 23:
                            typeString = " 2025祈安植福";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 35:
                                    //松柏嶺受天宮
                                    title = "松柏嶺受天宮";
                                    GetPurchaserlist_st_Blessing(adminID, ApplicantID, Year, ref payStatus);          //松柏嶺受天宮資料列表
                                    Checkedtemple_st(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";

                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            bindPayButton(true, true, true, false, true, false, false, true, true);
                                            //if (payStatus)
                                            //{
                                            //    bindPayButton(false, true, true, true, false, false, true, true);
                                            //}
                                            //else
                                            //{
                                            //    bindPayButton(true, true, true, true, true, true, true, true);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                        //祈安禮斗
                        case 24:
                            typeString = " 2025祈安禮斗";
                            Year = dtNow.Year.ToString();
                            break;
                        //千手觀音千燈迎佛法會
                        case 25:
                            typeString = " 2025千手觀音千燈迎佛法會";
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_QnLight(adminID, ApplicantID, Year, ref payStatus);          //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/11/04 23:59";

                                    if (DateTime.TryParseExact(
                                            EndDate,
                                            "yyyy/MM/dd HH:mm",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out DateTime endTime))
                                    {
                                        // 如果已過期，就隱藏按鈕；否則照原來綁定
                                        if (dtNow > endTime)
                                        {
                                            HidePayButton();
                                        }
                                        else
                                        {
                                            if (payStatus)
                                            {
                                                bindPayButton(false, true, true, true, false, false, false, true, true);
                                            }
                                            else
                                            {
                                                bindPayButton(true, true, true, true, true, true, false, true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 解析失敗時，為保險起見先隱藏按鈕
                                        HidePayButton();
                                    }
                                    break;
                            }
                            break;
                    }

                    if (Request["ad"] != null)
                    {
                        ExpirationDate = false;
                    }

                    if ((dtNow >= DateTime.Parse(EndDate)) && ExpirationDate)
                    {
                        Response.Write("<script>alert('親愛的大德您好\\n " + title + typeString + "活動已截止！！\\n感謝您的支持, 謝謝!');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('訪問網址錯誤，請重新進入。');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class AjaxPageHandler
        {
            /// <summary>
            /// 服務項目 (kind)。呼叫時 IntelliSense 會列出下列所有值：
            /// </summary>
            public enum ServiceKind
            {
                /// <summary>1 – 點燈</summary>
                點燈 = 1,
                /// <summary>2 – 普度</summary>
                普度 = 2,
                /// <summary>4 – 下元補庫</summary>
                下元補庫 = 4,
                /// <summary>5 – 呈疏補庫（天官武財神聖誕補財庫）</summary>
                呈疏補庫 = 5,
                /// <summary>6 – 企業補財庫</summary>
                企業補財庫 = 6,
                /// <summary>7 – 天赦日補運</summary>
                天赦日補運 = 7,
                /// <summary>8 – 天赦日祭改</summary>
                天赦日祭改 = 8,
                /// <summary>9 – 關聖帝君聖誕</summary>
                關聖帝君聖誕 = 9,
                /// <summary>10 – 代燒金紙</summary>
                代燒金紙 = 10,
                /// <summary>11 – 天貺納福添運法會</summary>
                天貺納福添運法會 = 11,
                /// <summary>12 – 靈寶禮斗</summary>
                靈寶禮斗 = 12,
                /// <summary>13 – 七朝清醮</summary>
                七朝清醮 = 13,
                /// <summary>14 – 九九重陽天赦日補運</summary>
                九九重陽天赦日補運 = 14,
                /// <summary>15 – 護國息災梁皇大法會</summary>
                護國息災梁皇大法會 = 15,
                /// <summary>16 – 補財庫</summary>
                補財庫 = 16,
                /// <summary>17 – 赦罪補庫</summary>
                赦罪補庫 = 17,
                /// <summary>18 – 天公生招財補運</summary>
                天公生招財補運 = 18,
                /// <summary>19 – 供香轉運</summary>
                供香轉運 = 19,
                /// <summary>20 – 安斗</summary>
                安斗 = 20,
                /// <summary>21 – 供花供果</summary>
                供花供果 = 21,
                /// <summary>22 – 孝親祈福燈</summary>
                孝親祈福燈 = 22,
                /// <summary>23 – 祈安植福</summary>
                祈安植福 = 23,
                /// <summary>24 – 祈安禮斗</summary>
                祈安禮斗 = 24,
                /// <summary>25 – 千手觀音千燈迎佛法會</summary>
                千手觀音千燈迎佛法會 = 25
            }

            /// <summary> 購買人編號 </summary>
            public int ApplicantID = 0;
            /// <summary> 宮廟編號 </summary>
            public int AdminID = 0;
            /// <summary> 服務項目 </summary>
            public int kind = 0;
            /// <summary> 總金額 </summary>
            public int Total = 0;
            /// <summary>  </summary>
            public int type = 0;

            /// <summary>
            /// 按下「前往支付」按鈕時觸發，
            /// 只負責解析參數並呼叫 PaymentService，完全不含 switch/case 邏輯。
            /// </summary>
            public void gotopay(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();

                AdminID = int.Parse(basePage.Request["a"] ?? throw new ArgumentException("缺少廟宇參數"));
                kind = int.Parse(basePage.Request["kind"] ?? throw new ArgumentException("缺少服務項目參數"));
                //string AppMobile = basePage.Request["AppMobile"] ?? throw new ArgumentException("缺少購買人電話參數");
                string AppMobile = basePage.Request["AppMobile"];
                string AppEmail = basePage.Request["AppEmail"];
                ApplicantID = int.Parse(basePage.Request["aid"] ?? throw new ArgumentException("缺少購買人編號參數"));
                string ChargeType = basePage.Request["ChargeType"] ?? throw new ArgumentException("缺少支付方式參數");
                string Code = basePage.Request["Code"] ?? throw new ArgumentException("缺少驗證碼參數");
                Total = int.Parse(basePage.Request["Total"] ?? throw new ArgumentException("缺少支付金額"));

                //string discountCode = basePage.Request["discountCode"];
                string orderId = dtNow.ToString("yyyyMMddHHmmssfff");

                // 假設 Request["kind"] = "5"
                if (!Enum.TryParse(basePage.Request["kind"], out ServiceKind kindEnum))
                    throw new ArgumentException("kind 參數錯誤");

                int InvoiceType = 0;
                int.TryParse(basePage.Request["InvoiceType"], out InvoiceType);
                string CarrierCode = basePage.Request["CarrierCode"];
                string InvoiceCode = basePage.Request["InvoiceCode"];
                string InvoiceName = basePage.Request["InvoiceName"];

                if (ChargeType.IndexOf("CSP", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    CarrierCode = InvoiceCode = InvoiceName = "";
                }

                if (basePage.Request["type"] != null)
                {
                    type = int.Parse(basePage.Request["type"].ToString());
                }

                if (ApplicantID > 0 && AdminID > 0 && kind > 0)
                {
                    LightDAC objLightDAC = new LightDAC(basePage);
                    string Year = dtNow.Year.ToString();
                    bool isValid = false;
                    bool isValid2 = false;
                    string CodeError = "0";
                    string MobileCarrierError;

                    if (kind == 1)
                    {
                        string startDate = "2025/11/01 00:00:00";
                        int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                        if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0)
                        {
                            Year = "2026";
                        }
                    }

                    // 實際金額（DB 計算）
                    int dbTotal = objLightDAC.GetApplicantTotal(ApplicantID, AdminID, kind, Year);

                    if (dbTotal != Total)
                    {
                        // ⚠️ 金額驗證失敗 → 寫入 Log 並回傳錯誤
                        string logMsg = $"[金額驗證失敗] ApplicantID={ApplicantID}, AdminID={AdminID}, Kind={kind}, " +
                                        $"RequestTotal={Total}, DbTotal={dbTotal}, Year={Year}, OrderId={orderId}";
                        basePage.SaveErrorLog(logMsg);

                        basePage.mJSonHelper.AddContent("StatusCode", -1);
                        basePage.mJSonHelper.AddContent("ErrorMessage", "金額驗證失敗，請重新確認訂單");
                        basePage.ResponseJSonString();
                        return;
                    }

                    try
                    {
                        string token = OtpTokenHelper.GenerateToken(ApplicantID, kind, AdminID, Code, "true");
                        bool isValid3 = OtpTokenCache.Validate(token);
                        if (!isValid3)
                        {
                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                            basePage.mJSonHelper.AddContent("ErrorMessage", "驗證碼無效或已過期。");
                            isValid = false;
                        }
                        else
                        {
                            // 檢查發票類型是否為手機載具且非小額付款支付
                            if (InvoiceType == 2 && ChargeType.IndexOf("CSP", StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                // 驗證手機載舉正確性
                                if (!ValidateMobileCarrier(CarrierCode, out MobileCarrierError))
                                {
                                    // 顯示錯誤給使用者
                                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                                    basePage.mJSonHelper.AddContent("CodeError", "-6");
                                }
                                else
                                {
                                    isValid2 = true;
                                }
                            }
                            else
                            {
                                isValid2 = true;
                            }

                            // 先檢查是否已有發票紀錄（不論是否已開立發票）
                            DataTable invoiceInfo = objLightDAC.GetInvoiceDetail(ApplicantID, AdminID, kind, Year);

                            if (invoiceInfo.Rows.Count > 0)
                            {
                                // 情境 A：已開立發票（已有 InvoiceNumber）
                                if (!string.IsNullOrEmpty(invoiceInfo.Rows[0]["InvoiceNumber"].ToString()))
                                {
                                    isValid2 = false;
                                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                                    basePage.mJSonHelper.AddContent("CodeError", "-8"); // 已開立發票
                                }
                                else
                                {
                                    // 情境 B：已有紀錄但未開立發票，直接沿用既有資料 ID
                                    isValid = true; // 視你的流程需求，直接當作通過最終驗證
                                }
                            }
                            else
                            {
                                // 情境 C：完全沒有資料 → 正常新增
                                if (isValid2)
                                {
                                    // 小額支付以外，開立發票
                                    int id = objLightDAC.AddInvoiceDetail(ApplicantID, AdminID, kind, InvoiceType, CarrierCode, InvoiceCode, InvoiceName, Year);

                                    if (id > 0)
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        basePage.mJSonHelper.AddContent("StatusCode", 0);
                                        basePage.mJSonHelper.AddContent("CodeError", "-7");
                                    }
                                }
                            }

                        }


                        if (isValid)
                        {
                            if (Total > 0)
                            {
                                switch (kind)
                                {
                                    case 1:
                                        //點燈服務
                                        string startDate = "2025/11/01 00:00:00";
                                        int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                                        if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || basePage.Request["ad"] == "2")
                                        {
                                            Year = "2026";
                                        }

                                        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, type, Year);
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
                                                    DataTable dtData = objLightDAC.Getlights_da_Info(ApplicantID, Year);

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
                                                        string lightsType = lightstypelist[i];
                                                        int c = 0;

                                                        c += lightsType == "3" ? count_da_lights[0] : 0;
                                                        c += lightsType == "4" ? count_da_lights[1] : 0;
                                                        c += lightsType == "5" ? count_da_lights[2] : 0;

                                                        if (objLightDAC.CheckedLightsNum(lightsType, AdminID.ToString(), c, Year, basePage))
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
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    //新港奉天宮
                                                    name = "新港奉天宮點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 6:
                                                    //北港武德宮
                                                    name = "北港武德宮點燈服務";
                                                    dtData = objLightDAC.Getlights_wu_Info(ApplicantID, Year);

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
                                                        string lightsType = lightstypelist[i];
                                                        int c = 0;

                                                        c += lightsType == "3" ? count_wu_lights[0] : 0;
                                                        c += lightsType == "4" ? count_wu_lights[1] : 0;
                                                        c += lightsType == "6" ? count_wu_lights[2] : 0;

                                                        if (objLightDAC.CheckedLightsNum(lightsType, AdminID.ToString(), c, Year, basePage))
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
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 8:
                                                    //西螺福興宮
                                                    name = "西螺福興宮點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 10:
                                                    //台南正統鹿耳門聖母廟
                                                    name = "台南正統鹿耳門聖母廟點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 14:
                                                    //桃園威天宮
                                                    name = "桃園威天宮點燈服務";
                                                    dtData = objLightDAC.Getlights_ty_Info(ApplicantID, Year);

                                                    int[] count_ty_lights = new int[7];
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
                                                            case "11":
                                                                //福祿燈
                                                                count_ty_lights[5]++;
                                                                break;
                                                            case "21":
                                                                //孝親祈福燈
                                                                count_ty_lights[6]++;
                                                                break;
                                                        }
                                                    }

                                                    //Lightstypelist = new string[] { "3", "4", "6", "8", "10" };
                                                    LightsStringlist = new string[] { "光明燈", "太歲燈", "財神燈", "藥師燈", "貴人燈", "福祿燈", "孝親祈福燈" };
                                                    for (int i = 0; i < lightstypelist.Length; i++)
                                                    {
                                                        string lightsType = lightstypelist[i];
                                                        int c = 0;

                                                        c += lightsType == "3" ? count_ty_lights[0] : 0;
                                                        c += lightsType == "4" ? count_ty_lights[1] : 0;
                                                        c += lightsType == "6" ? count_ty_lights[2] : 0;
                                                        c += lightsType == "8" ? count_ty_lights[3] : 0;
                                                        c += lightsType == "10" ? count_ty_lights[4] : 0;
                                                        c += lightsType == "11" ? count_ty_lights[5] : 0;

                                                        if (objLightDAC.CheckedLightsNum(lightsType, AdminID.ToString(), c, Year, basePage))
                                                        {
                                                            checkednum_ty = false;

                                                            basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                            basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                            break;
                                                        }
                                                    }

                                                    int type = 1;
                                                    if (checkednum_ty)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 15:
                                                    //斗六五路財神宮
                                                    name = "斗六五路財神宮點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 16:
                                                    //台東東海龍門天聖宮
                                                    name = "台東東海龍門天聖宮點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 17:
                                                    //五股賀聖宮
                                                    name = "五股賀聖宮";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 21:
                                                    //鹿港城隍廟
                                                    name = "鹿港城隍廟點燈服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 23:
                                                    //玉敕大樹朝天宮
                                                    name = "玉敕大樹朝天宮點燈服務";
                                                    dtData = objLightDAC.Getlights_ma_Info(ApplicantID, Year);

                                                    int[] count_ma_lights = new int[4];
                                                    lightstypelist = new string[dtData.Rows.Count];
                                                    bool checkednum_ma = true;
                                                    for (int i = 0; i < dtData.Rows.Count; i++)
                                                    {
                                                        lightstypelist[i] = dtData.Rows[i]["LightsType"].ToString();
                                                        switch (dtData.Rows[i]["LightsType"].ToString())
                                                        {
                                                            case "3":
                                                                //光明燈
                                                                count_ma_lights[0]++;
                                                                break;
                                                            case "4":
                                                                //太歲燈
                                                                count_ma_lights[1]++;
                                                                break;
                                                            case "5":
                                                                //五文昌燈
                                                                count_ma_lights[2]++;
                                                                break;
                                                            case "6":
                                                                //福財燈
                                                                count_ma_lights[3]++;
                                                                break;
                                                        }
                                                    }

                                                    //string[] Lightstypelist = new string[] { "3", "4", "5" };
                                                    LightsStringlist = new string[] { "光明燈", "太歲燈", "五文昌燈", "福財燈" };
                                                    for (int i = 0; i < lightstypelist.Length; i++)
                                                    {
                                                        string lightsType = lightstypelist[i];
                                                        int c = 0;

                                                        c += lightsType == "3" ? count_ma_lights[0] : 0;
                                                        c += lightsType == "4" ? count_ma_lights[1] : 0;
                                                        c += lightsType == "5" ? count_ma_lights[2] : 0;
                                                        c += lightsType == "6" ? count_ma_lights[3] : 0;

                                                        if (objLightDAC.CheckedLightsNum(lightsType, AdminID.ToString(), c, Year, basePage))
                                                        {
                                                            checkednum_ma = false;

                                                            basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                            basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                            if (basePage.Request["ad"] != null)
                                                            {
                                                                checkednum_ma = true;
                                                            }

                                                            break;
                                                        }
                                                    }

                                                    if (checkednum_ma)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 31:
                                                    //台灣道教總廟無極三清總道院
                                                    name = "台灣道教總廟無極三清總道院點燈服務";
                                                    dtData = objLightDAC.Getlights_wjsan_Info(ApplicantID, Year);

                                                    bool checkednum_wjsan = true;
                                                    if (checkednum_wjsan)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 32:
                                                    //桃園龍德宮
                                                    name = "桃園龍德宮點燈服務";
                                                    dtData = objLightDAC.Getlights_ld_Info(ApplicantID, Year);

                                                    bool checkednum_ld = true;
                                                    if (checkednum_ld)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_ld(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 35:
                                                    //松柏嶺受天宮
                                                    name = "松柏嶺受天宮點燈服務";
                                                    dtData = objLightDAC.Getlights_st_Info(ApplicantID, Year);

                                                    bool checkednum_st = true;
                                                    if (checkednum_st)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_st(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 38:
                                                    //池上北極玄天宮
                                                    name = "池上北極玄天宮點燈服務";
                                                    dtData = objLightDAC.Getlights_bj_Info(ApplicantID, Year);

                                                    bool checkednum_bj = true;
                                                    if (checkednum_bj)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_bj(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 39:
                                                    //花蓮慈惠石壁部堂
                                                    name = "花蓮慈惠石壁部堂點燈服務";
                                                    dtData = objLightDAC.Getlights_sbbt_Info(ApplicantID, Year);

                                                    bool checkednum_sbbt = true;
                                                    if (checkednum_sbbt)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_sbbt(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 40:
                                                    //新北真武山受玄宮
                                                    name = "新北真武山受玄宮點燈服務";
                                                    dtData = objLightDAC.Getlights_bpy_Info(ApplicantID, Year);

                                                    bool checkednum_bpy = true;
                                                    if (checkednum_bpy)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_bpy(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 41:
                                                    //桃園壽山巖觀音寺
                                                    name = "桃園壽山巖觀音寺點燈服務";
                                                    dtData = objLightDAC.Getlights_ssy_Info(ApplicantID, Year);

                                                    bool checkednum_ssy = true;
                                                    if (checkednum_ssy)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_ssy(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
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

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;
                                            switch (AdminID)
                                            {
                                                case 3:
                                                    //大甲鎮瀾宮
                                                    string name = "大甲鎮瀾宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 4:
                                                    //新港奉天宮
                                                    name = "新港奉天宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 6:
                                                    //北港武德宮
                                                    name = "北港武德宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 8:
                                                    //西螺福興宮
                                                    name = "西螺福興宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 10:
                                                    //台南正統鹿耳門聖母廟
                                                    name = "台南正統鹿耳門聖母廟普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 14:
                                                    //桃園威天宮
                                                    name = "桃園威天宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 15:
                                                    //斗六五路財神宮
                                                    name = "斗六五路財神宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 16:
                                                    //台東東海龍門天聖宮
                                                    name = "台東東海龍門天聖宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 21:
                                                    //鹿港城隍廟
                                                    name = "鹿港城隍廟普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 23:
                                                    //玉敕大樹朝天宮
                                                    name = "玉敕大樹朝天宮普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 31:
                                                    //台灣道教總廟無極三清總道院
                                                    name = "台灣道教總廟無極三清總道院普度服務";
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_purdue_wjsan(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
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
                                        //下元補庫

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 4, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                    case 5:
                                        //呈疏補庫

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 5, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                    case 6:
                                        //企業補財庫
                                        break;
                                    case 7:
                                        //桃園威天宮天赦日招財補運

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 7, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (AdminID)
                                            {
                                                case 14:
                                                    //桃園威天宮
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 23:
                                                    //玉敕大樹朝天宮
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }

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
                                    case 8:
                                        //進寶財神廟天赦日祭改
                                        break;
                                    case 9:
                                        //桃園威天宮關聖帝君聖誕

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 9, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                    case 10:
                                        break;
                                    case 11:
                                        //台東東海龍門天聖宮天貺納福添運法會

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 11, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                    case 12:
                                        //玉敕大樹朝天宮靈寶禮斗

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 12, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : "") + (basePage.Request["fet"] != null ? "&fet=1" : ""), Year);
                                                    break;
                                            }

                                            if (link != "")
                                            {
                                                if (link.IndexOf("錯誤") > 0)
                                                {
                                                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                                                    basePage.mJSonHelper.AddContent("Getlisterr", 1);
                                                    basePage.mJSonHelper.AddContent("msg", link);

                                                    basePage.Session["ApplicantID"] = ApplicantID;
                                                }
                                                else
                                                {
                                                    basePage.SavePayLog(link);

                                                    basePage.mJSonHelper.AddContent("StatusCode", 1);
                                                    basePage.mJSonHelper.AddContent("redirect", link);

                                                    basePage.Session["ApplicantID"] = ApplicantID;
                                                }
                                            }
                                        }
                                        break;
                                    case 13:
                                        //大甲鎮瀾宮七朝清醮

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 13, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                    case 14:
                                        //桃園威天宮九九重陽天赦日補運

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 14, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                    case 15:
                                        //台東東海龍門天聖宮護國息災梁皇大法會

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 15, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                        ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                        (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                    case 16:
                                        //補財庫
                                        switch (AdminID)
                                        {
                                            case 15:
                                                Year = "2025";
                                                dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 16, type, Year);
                                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                                {
                                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                                }
                                                else
                                                {
                                                    int cost = Total;
                                                    string link = string.Empty;

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                            case 21:
                                                Year = "2025";
                                                dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 16, type, Year);
                                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                                {
                                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                                }
                                                else
                                                {
                                                    int cost = Total;
                                                    string link = string.Empty;

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                        }
                                        break;
                                    case 17:
                                        //赦罪補庫-神霄玉府財神會館
                                        switch (AdminID)
                                        {
                                            case 33:
                                                Year = "2025";
                                                dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 17, type, Year);
                                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                                {
                                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                                }
                                                else
                                                {
                                                    int cost = Total;
                                                    string link = string.Empty;

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                        }
                                        break;
                                    case 18:
                                        //桃園威天宮天公生招財補運

                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 18, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;

                                            switch (ChargeType)
                                            {
                                                case "LinePay":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "JkosPay":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "PXPayPlus":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ChtCSP":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "TwmCSP":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                    break;
                                                case "UnionPay":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                case "ApplePay":
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                    break;
                                                default:
                                                    link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                        "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                    case 19:
                                        //供香轉運-神霄玉府財神會館
                                        switch (AdminID)
                                        {
                                            case 33:
                                                Year = "2025";
                                                dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 19, type, Year);
                                                if (dtLASTTIME.AddMinutes(20) < dtNow)
                                                {
                                                    basePage.mJSonHelper.AddContent("Timeover", 1);
                                                }
                                                else
                                                {
                                                    int cost = Total;
                                                    string link = string.Empty;

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                                (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                        }
                                        break;
                                    case 20:
                                        //安斗服務
                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 20, type, Year);
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
                                                case 15:
                                                    //斗六五路財神宮
                                                    name = "斗六五路財神宮安斗服務";

                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                    }
                                                    break;
                                                case 31:
                                                    //台灣道教總廟無極三清總道院
                                                    name = "台灣道教總廟無極三清總道院安斗服務";
                                                    bool checkednum_wjsan = true;
                                                    if (checkednum_wjsan)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
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
                                    case 21:
                                        //供花供果服務
                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 21, type, Year);
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
                                                case 31:
                                                    //台灣道教總廟無極三清總道院
                                                    name = "台灣道教總廟無極三清總道院安斗服務";
                                                    bool checkednum_wjsan = true;
                                                    if (checkednum_wjsan)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_huaguo_wjsan(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                        }
                                                    }
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
                                    case 22:
                                        //孝親祈福燈服務
                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 22, type, Year);
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
                                                case 14:
                                                    //桃園威天宮
                                                    name = "桃園威天宮孝親祈福燈服務";
                                                    DataTable dtData = objLightDAC.Getlights_ty_Info(ApplicantID, Year);

                                                    int[] count_ty_lights = new int[7];
                                                    string[] lightstypelist = new string[dtData.Rows.Count];
                                                    bool checkednum_ty = true;

                                                    int type = 1;
                                                    int.TryParse(basePage.Request["type"], out type);
                                                    if (checkednum_ty)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_lights_ty_mom(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                                break;
                                                        }
                                                    }
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
                                    case 23:
                                        //祈安植福
                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 23, type, Year);
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
                                                case 35:
                                                    //松柏嶺受天宮
                                                    name = "松柏嶺受天宮祈安植福服務";
                                                    DataTable dtData = objLightDAC.Getblessing_st_Info(ApplicantID, Year);

                                                    bool checkednum_st = true;
                                                    if (checkednum_st)
                                                    {
                                                        switch (ChargeType)
                                                        {
                                                            case "LinePay":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "JkosPay":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "PXPayPlus":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ChtCSP":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "TwmCSP":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "UnionPay":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            case "ApplePay":
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                                break;
                                                            default:
                                                                link = TWWebPay_blessing_st(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                            }
                                        }
                                        break;
                                    case 24:
                                        // 祈安禮斗
                                        break;
                                    case 25:
                                        // 千手觀音千燈迎佛法會
                                        dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 25, type, Year);
                                        if (dtLASTTIME.AddMinutes(20) < dtNow)
                                        {
                                            basePage.mJSonHelper.AddContent("Timeover", 1);
                                        }
                                        else
                                        {
                                            int cost = Total;
                                            string link = string.Empty;
                                            switch (AdminID)
                                            {
                                                case 14:
                                                    //桃園威天宮
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        case "JkosPay":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "JKOPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "TELEPAY", "cht", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        case "ApplePay":
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, "APPLEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_qnlight_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                            break;
                                                    }
                                                    break;
                                            }


                                            if (link != "")
                                            {
                                                basePage.SavePayLog(link);

                                                basePage.mJSonHelper.AddContent("StatusCode", 1);
                                                basePage.mJSonHelper.AddContent("redirect", link);

                                                basePage.Session["ApplicantID"] = ApplicantID;
                                            }
                                            else
                                            {
                                                string logMsg = $"[支付例外] OrderId={orderId}, ApplicantID={ApplicantID}, Error=付款API取得失敗，請重新嘗試!";
                                                basePage.SaveErrorLog(logMsg);

                                                basePage.mJSonHelper.AddContent("StatusCode", -98);
                                                basePage.mJSonHelper.AddContent("ErrorMessage", "付款API取得失敗，請重新嘗試!");
                                                basePage.ResponseJSonString();
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string logMsg = $"[支付例外] OrderId={orderId}, ApplicantID={ApplicantID}, Error={ex}";
                        basePage.SaveErrorLog(logMsg);

                        basePage.mJSonHelper.AddContent("StatusCode", -99);
                        basePage.mJSonHelper.AddContent("ErrorMessage", "支付過程發生錯誤，請稍後再試");
                        basePage.ResponseJSonString();
                    }
                }
            }

            /// <summary>
            /// 驗證手機載具：先做本地格式檢查，再呼叫遠端驗證 API
            /// </summary>
            /// <param name="carrierCode">載具條碼</param>
            /// <param name="errorMsg">失敗時帶回錯誤訊息</param>
            /// <returns>合法回 true，否則 false</returns>
            private bool ValidateMobileCarrier(string carrierCode, out string errorMsg)
            {
                errorMsg = null;

                // 1) 本地格式檢查
                try
                {
                    new CarrierChecker(new MobileCarrierValidator())
                        .Validate("3J0002", carrierCode);
                }
                catch (ArgumentException ex)
                {
                    errorMsg = "格式驗證失敗：" + ex.Message;
                    return false;
                }

                // 2) 呼叫你自己寫的 WebForms 驗證 API
                string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                string apiUrl = host + "/Api/InvoiceMobileCarrierAPI.aspx";

                // 如果是在非 Page context，也可以用：
                // string apiUrl = HttpContext.Current.Request.ApplicationPath.TrimEnd('/') 
                //               + "/Api/InvoiceMobileCarrierAPI.aspx";

                var payload = new { carrierType = "3J0002", carrierId = carrierCode };
                string json = JsonConvert.SerializeObject(payload);

                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    HttpResponseMessage resp;
                    try
                    {
                        resp = client.PostAsync(apiUrl,
                            new StringContent(json, Encoding.UTF8, "application/json")
                        ).Result;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = "遠端呼叫失敗：" + ex.Message;
                        return false;
                    }

                    if (resp.StatusCode != HttpStatusCode.OK)
                    {
                        errorMsg = $"遠端驗證 HTTP 錯誤：{(int)resp.StatusCode}";
                        return false;
                    }

                    string body = resp.Content.ReadAsStringAsync().Result;
                    JObject obj;
                    try
                    {
                        obj = JObject.Parse(body);
                    }
                    catch
                    {
                        errorMsg = "遠端回傳格式錯誤";
                        return false;
                    }

                    if (!obj.Value<bool>("valid"))
                    {
                        errorMsg = obj.Value<string>("error") ?? "遠端驗證未通過";
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// AJAX：傳送OTP驗證碼（支援手機簡訊與Email）
            /// </summary>
            /// <param name="basePage">目前的頁面基底物件</param>
            public void sendsms(BasePage basePage)
            {
                lock (_thisLock)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 0);

                    // 取得台北標準時間
                    DateTime dtNow = LightDAC.GetTaipeiNow();

                    AdminID = int.Parse(basePage.Request["a"] ?? throw new ArgumentException("缺少廟宇參數"));
                    kind = int.Parse(basePage.Request["kind"] ?? throw new ArgumentException("缺少服務項目參數"));
                    string AppName = basePage.Request["AppName"] ?? throw new ArgumentException("缺少購買人姓名參數");
                    string AppMobile = basePage.Request["AppMobile"] ?? throw new ArgumentException("缺少購買人電話參數");
                    string AppEmail = basePage.Request["AppEmail"] ?? throw new ArgumentException("缺少購買人信箱參數");
                    string VerifyType = basePage.Request["VerifyType"] ?? throw new ArgumentException("缺少取得驗證碼方式參數");
                    ApplicantID = int.Parse(basePage.Request["aid"] ?? throw new ArgumentException("缺少購買人編號參數"));

                    // 假設 Request["kind"] = "5"
                    if (!Enum.TryParse(basePage.Request["kind"], out ServiceKind kindEnum))
                        throw new ArgumentException("kind 參數錯誤");

                    if (ApplicantID > 0 && AdminID > 0 && kind > 0)
                    {
                        LightDAC objLightDAC = new LightDAC(basePage);
                        SMSHepler objSMSHepler = new SMSHepler();

                        string orderId = dtNow.ToString("yyyyMMddHHmmssfff");
                        string Year = dtNow.Year.ToString();
                        string Codeerror = "0";

                        string code = CreateRandomWord(6);

                        string msg = "【保必保庇】線上宮廟服務平臺，購買人電話OTP認證，【" + code + "】簡訊密碼180秒有效，驗證碼請勿提供他人，以防詐騙";

                        if (kind == 1)
                        {
                            string startDate = "2025/11/01 00:00:00";
                            int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0)
                            {
                                Year = "2026";
                            }
                        }

                        string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}&VerifyType{5}", ApplicantID, AdminID, kind, Year, code, VerifyType);

                        int codeType = VerifyType == "email" ? 1 : 0;

                        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, codeType, AppMobile, Year, ref Codeerror))
                        {
                            if (objLightDAC.AddCAPTCHACode(ApplicantID, AdminID, kind, codeType, code, AppMobile, Year))
                            {
                                switch (VerifyType)
                                {
                                    case "email":
                                        if (CaptchaEmailSender.Send(
                                            buyerEmail: AppEmail,
                                            buyerName: AppName,
                                            code: code))
                                        {
                                            basePage.SaveCAPTCHACodeLog(log);

                                            basePage.mJSonHelper.AddContent("StatusCode", 1);

                                            basePage.Session["ApplicantID"] = ApplicantID;
                                        }
                                        else
                                        {
                                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                                            basePage.mJSonHelper.AddContent("CodeError", "-11");
                                        }
                                        break;
                                    default:
                                        if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                                        {
                                            basePage.SaveCAPTCHACodeLog(log);

                                            basePage.mJSonHelper.AddContent("StatusCode", 1);

                                            basePage.Session["ApplicantID"] = ApplicantID;
                                        }
                                        else
                                        {
                                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                                            basePage.mJSonHelper.AddContent("CodeError", "-10");
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                basePage.mJSonHelper.AddContent("StatusCode", 0);
                                basePage.mJSonHelper.AddContent("CodeError", "-9");
                            }
                        }
                        else
                        {
                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("缺少必要參數");
                    }
                }
            }

            /// <summary>
            /// 產生隨機驗證碼（純數字）
            /// </summary>
            /// <param name="length">驗證碼長度</param>
            /// <returns>隨機數字字串</returns>
            private string CreateRandomWord(int length = 6)
            {
                string chars = "0123456789";
                //string chars = "ABCDEFGHJKMPQRSTUVWXYZ23456789abcdefghjkmpqrstuvwxyz".ToArray();
                Random random = new Random();
                char[] buffer = new char[length];
                for (int i = 0; i < length; i++)
                {
                    buffer[i] = chars[random.Next(chars.Length)];
                }
                return new string(buffer);
            }

            /// <summary>
            /// AJAX：驗證OTP驗證碼（支援手機簡訊與Email）
            /// </summary>
            /// <param name="basePage">目前的頁面基底物件</param>
            public void verifyOTP(BasePage basePage)
            {
                lock (_thisLock)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 0);

                    // 取得台北標準時間
                    DateTime dtNow = LightDAC.GetTaipeiNow();

                    AdminID = int.Parse(basePage.Request["a"] ?? throw new ArgumentException("缺少廟宇參數"));
                    kind = int.Parse(basePage.Request["kind"] ?? throw new ArgumentException("缺少服務項目參數"));
                    string Code = basePage.Request["Code"] ?? throw new ArgumentException("缺少驗證碼參數");
                    string VerifyType = basePage.Request["VerifyType"] ?? throw new ArgumentException("缺少取得驗證碼方式參數");
                    ApplicantID = int.Parse(basePage.Request["aid"] ?? throw new ArgumentException("缺少購買人編號參數"));

                    if (ApplicantID > 0 && AdminID > 0 && kind > 0)
                    {
                        LightDAC objLightDAC = new LightDAC(basePage);
                        SMSHepler objSMSHepler = new SMSHepler();

                        string orderId = dtNow.ToString("yyyyMMddHHmmssfff");
                        string Year = dtNow.Year.ToString();
                        string Codeerror = "0";

                        if (kind == 1)
                        {
                            string startDate = "2025/11/01 00:00:00";
                            int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0)
                            {
                                Year = "2026";
                            }
                        }

                        int codeType = VerifyType == "email" ? 1 : 0;

                        // 檢查驗證碼是否有效且正確。
                        if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, codeType, Year, ref Codeerror))
                        {
                            // 更新驗證碼狀態，標記已使用。
                            if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, codeType, Year))
                            {
                                string otpToken = OtpTokenHelper.GenerateToken(ApplicantID, kind, AdminID, Code, "true");
                                OtpTokenCache.Save(otpToken, dtNow.AddMinutes(20));

                                basePage.mJSonHelper.AddContent("StatusCode", 1);
                            }
                            else
                            {
                                basePage.mJSonHelper.AddContent("StatusCode", 0);
                                basePage.mJSonHelper.AddContent("CodeError", "-4");
                            }
                        }
                        else
                        {
                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        }
                    }
                    else
                    {

                    }
                }
            }

            public void checkedDiscountCode(BasePage basePage)
            {

            }

            public string TWWebPay_lights_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DaJiaLightUp";    //大甲鎮瀾宮祈福點燈 PR00004024
                string item = "大甲鎮瀾宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if(ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 3, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "大甲鎮瀾宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_da(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "大甲鎮瀾宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_h(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-HsinKangLightUp";    //新港奉天宮祈福點燈 PR00004008
                string item = "新港奉天宮祈福點燈";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 4, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "新港奉天宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_h(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_h(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "新港奉天宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeLightUp";    //北港武德宮 宮廟服務 PR00004401
                string item = "北港武德宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 6, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "北港武德宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_wu(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "北港武德宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_Fu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-XiluoFuyu";    //西螺福興宮_宮廟服務 00004588
                string item = "西螺福興宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 8, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "西螺福興宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_Fu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Fu(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "西螺福興宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_Luer(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Luermen";    //台南正統鹿耳門聖母廟_宮廟服務 PR00004609
                string item = "台南正統鹿耳門聖母廟點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_Luer_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 10, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台南正統鹿耳門聖母廟 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_Luer(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Luer(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台南正統鹿耳門聖母廟 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                int type, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務 PR00004719
                string item = "桃園威天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = type == 2 ? basePag.GetConfigValue("PaymentLights_ty_mom_ReceiveURL") : basePag.GetConfigValue("PaymentLights_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    //int.TryParse(basePag.Request["type"].ToString(), out type);
                    long id = objLightDAC.AddChargeLog_Lights_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_ty_mom(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                int type, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務 PR00004719
                string item = "桃園威天宮孝親祈福燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_ty_mom_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 22, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 孝親祈福燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    //int.TryParse(basePag.Request["type"].ToString(), out type);
                    //活動-孝親祈福燈
                    long id = objLightDAC.AddChargeLog_Lights_ty_mom(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ty_mom(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 孝親祈福燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            /// <summary>
            /// CALL 付款API - 千手觀音千燈迎佛法會 - 桃園威天宮
            /// </summary>
            /// <param name="basePag"></param>
            /// <param name="orderid"></param>
            /// <param name="applicantID"></param>
            /// <param name="paytype"></param>
            /// <param name="telco"></param>
            /// <param name="price"></param>
            /// <param name="m_phone"></param>
            /// <param name="returnUrl"></param>
            /// <param name="type"></param>
            /// <param name="Year"></param>
            /// <returns></returns>
            protected string TWWebPay_qnlight_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                int type, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務 PR00004719
                string item = "桃園威天宮千手觀音千燈迎佛法會服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentQnLight_ty_ReceiveURL");
                string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");
                string msisdn = m_phone;
                string chrgtype = "1";
                string m1 = applicantID.ToString();
                string m2 = returnUrl;

                string mac = MD5.MD5Encrypt(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
                                      + telco + chrgtype + msisdn + Timestamp + ValidationKey).Replace("-", "").ToLower();

                string paymentChannelLog = returnUrl;
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 25, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 千手觀音千燈迎佛法會 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_QnLight_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_QnLight_ty(applicantID, AdminID, price, Year))
                    {
                        link = "https://paygate.tw/xpay/pay?uid=" + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 千手觀音千燈迎佛法會 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YunlinWulucaishen";    //斗六五路財神宮_宮廟服務 PR00004721
                string item = "斗六五路財神宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 15, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "斗六五路財神宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Fw(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "斗六五路財神宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_andou_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YunlinWulucaishen";    //斗六五路財神宮_宮廟服務 PR00004721
                string item = "斗六五路財神宮安斗服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentAnDou_Fw_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 15, 20, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "斗六五路財神宮 安斗 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_AnDou_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_AnDou_Fw(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "斗六五路財神宮 安斗 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務 PR00004720
                string item = "台東東海龍門天聖宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 16, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_dh(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_Hs(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務 PR00004720
                string item = "五股賀聖宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 17, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "五股賀聖宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_Hs(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Hs(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "五股賀聖宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-LugangChenghuangmiao";    //鹿港城隍廟祈福點燈 PR00004755
                string item = "鹿港城隍廟點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
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
                LightDAC objLightDAC = new LightDAC(basePage);
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 21, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "鹿港城隍廟 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_Lk(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "鹿港城隍廟 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YuchiDashu";    //玉敕大樹朝天宮(PR00004866)
                string item = "玉敕大樹朝天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_ma_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 23, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ma(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Wjsan";    //台灣道教總廟無極三清總道院(PR00009720)
                string item = "台灣道教總廟無極三清總道院點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_wjsan_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 31, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_wjsan(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_andou_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Wjsan";    //台灣道教總廟無極三清總道院(PR00009720)
                string item = "台灣道教總廟無極三清總道院安斗服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentAnDou_wjsan_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 31, 20, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 安斗 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_AnDou_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_AnDou_wjsan(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 安斗 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_huaguo_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Wjsan";    //台灣道教總廟無極三清總道院(PR00009720)
                string item = "台灣道教總廟無極三清總道院供花供果服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentHuaguo_wjsan_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 31, 21, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 供花供果 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Huaguo_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Huaguo_wjsan(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 供花供果 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_ld(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanLD";    //桃園龍德宮(PR00009720)
                string item = "桃園龍德宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_ld_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 32, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園龍德宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_ld(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ld(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園龍德宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_st(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Shoutian";    //松柏嶺受天宮(PR00009720)
                string item = "松柏嶺受天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_st_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 35, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "松柏嶺受天宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_st(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_st(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "松柏嶺受天宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_nt(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Nantian";    //台中南天宮 宮廟服務
                string item = "台中南天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_nt_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                //if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 37, 1, Year, ref oid))
                //{
                //    if (oid != "")
                //    {
                //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //    }
                //    else
                //    {
                //        link = "台中南天宮 點燈 AppChargeLog ID 錯誤！";
                //        basePag.SaveErrorLog(link);
                //    }
                //}
                //else
                //{
                //    long id = objLightDAC.AddChargeLog_Lights_nt(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //    //long id = 6;

                //    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_nt(applicantID, AdminID, price, Year))
                //    {
                //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //    }
                //    else
                //    {
                //        link = "台中南天宮 點燈 更新購買人資料 錯誤！";
                //        basePag.SaveErrorLog(link);
                //    }
                //}

                return link;
            }

            protected string TWWebPay_lights_bj(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Beijitaitung";    //池上北極玄天宮(PR00009720)
                string item = "池上北極玄天宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_bj_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 38, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "池上北極玄天宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_bj(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_bj(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "池上北極玄天宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_sbbt(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Shibibutang";    //花蓮慈惠石壁部堂(PR00009720)
                string item = "花蓮慈惠石壁部堂點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_sbbt_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 39, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "慈惠石壁部堂 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_sbbt(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_sbbt(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "慈惠石壁部堂 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_bpy(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Bopeeyou";    //新北真武山受玄宮(PR00009720)
                string item = "新北真武山受玄宮點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_bpy_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 40, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "真武山受玄宮 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_bpy(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_bpy(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "真武山受玄宮 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lights_ssy(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Shoushanyan";    //桃園壽山巖觀音寺(PR00009720)
                string item = "桃園壽山巖觀音寺點燈服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLights_ssy_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 41, 1, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "壽山巖觀音寺 點燈 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lights_ssy(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ssy(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "壽山巖觀音寺 點燈 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DajiaCeremony";    //大甲鎮瀾宮普渡法會(CSENT64199)
                string item = "大甲鎮瀾宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_da_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 3, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "大甲鎮瀾宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_da(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "大甲鎮瀾宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_h(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-HsinKangCeremony";    //新港奉天宮普度法會 PR00004271
                string item = "新港奉天宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_h_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 4, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "新港奉天宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_h(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_h(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "新港奉天宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeLightUp";    //北港武德宮_宮廟服務(PR00004401)
                string item = "北港武德宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_wu_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 6, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "北港武德宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_wu(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "北港武德宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_Fu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-XiluoFuyu";    //西螺福興宮普度法會 PR00004588
                string item = "西螺福興宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_Fu_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 8, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "西螺福興宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_Fu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Fu(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "西螺福興宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_Jing(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanJingfu";    //桃園大廟景福宮普度法會 PR00004589
                string item = "桃園大廟景福宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_Jing_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                //long id = objLightDAC.AddChargeLog_Purdue_Jing(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                ////long id = 6;

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Jing(applicantID, AdminID, price, Year))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //}
                //long id = objLightDAC.AddChargeLog_Purdue_Jing(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Jing(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //}

                return link;
            }

            protected string TWWebPay_purdue_Luer(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Luermen";    //台南正統鹿耳門聖母廟普度法會 PR00004609
                string item = "台南正統鹿耳門聖母廟普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_Luer_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 10, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台南正統鹿耳門聖母廟 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_Luer(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Luer(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台南正統鹿耳門聖母廟 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "桃園威天宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YunlinWulucaishen";    //斗六五路財神宮_宮廟服務(PR00004721)
                string item = "斗六五路財神宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_Fw_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 15, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "斗六五路財神宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Fw(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "斗六五路財神宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務(PR00004720)
                string item = "台東東海龍門天聖宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_dh_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 16, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_dh(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-LugangChenghuangmiao";    //鹿港城隍廟(PR00004755)
                string item = "鹿港城隍廟普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_Lk_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 21, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "鹿港城隍廟 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Lk(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "鹿港城隍廟 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_purdue_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YuchiDashu";    //玉敕大樹朝天宮(PR00004866)
                string item = "玉敕大樹朝天宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_ma_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 23, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_ma(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            //protected string TWWebPay_purdue_mazu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
            //    string Year)
            //{
            //    // 取得台北標準時間
            //    DateTime dtNow = LightDAC.GetTaipeiNow();
            //    BasePage basePage = new BasePage();
            //    string oid = orderid;
            //    string uid = "Temple";
            //    string Sid = "Temple-DajiaCeremony";    //大甲鎮瀾宮普渡法會(CSENT64199)
            //    string item = "大甲鎮瀾宮普度法會";
            //    string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
            //    string link = "https://paygate.tw/xpay/pay?uid=";
            //    string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_mazu_ReceiveURL");
            //    string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");
            //    string msisdn = m_phone;
            //    string chrgtype = "1";
            //    string m1 = applicantID.ToString();
            //    string m2 = returnUrl;
            //    //string mac = MD5.Encode(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
            //    //                      + telco + chrgtype + Timestamp + ValidationKey).Replace("-", "").ToLower();
            //    string mac = MD5.MD5Encrypt(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
            //                          + telco + chrgtype + msisdn + Timestamp + ValidationKey).Replace("-", "").ToLower();

            //    string paymentChannelLog = returnUrl;
            //    LightDAC objLightDAC = new LightDAC(basePage);
            //    string ChargeType = paytype;
            //    if (ChargeType == "TELEPAY")
            //    {
            //        if (telco == "twm")
            //        {
            //            ChargeType = "Twm";
            //        }
            //        else
            //        {
            //            ChargeType = "Cht";
            //        }
            //    }

            //    if (ChargeType == "CreditCard")
            //    {
            //        if (telco == "UNIONPAY")
            //        {
            //            ChargeType = "UNIONPAY";
            //        }
            //    }

            //    if (ChargeType == "JKOPAY")
            //    {
            //        ChargeType = "JKOSPAY";
            //    }

            //    long id = objLightDAC.AddChargeLog_Purdue_mazu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
            //    //long id = 6;

            //    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_mazu(applicantID, AdminID, price, Year))
            //    {
            //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
            //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
            //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

            //    }

            //    return link;
            //}

            protected string TWWebPay_purdue_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Wjsan";    //台灣道教總廟無極三清總道院(PR00009720)
                string item = "台灣道教總廟無極三清總道院普度服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_wjsan_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 31, 2, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 普度 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Purdue_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_wjsan(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台灣道教總廟無極三清總道院 普度 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_emperorGuansheng_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, 
                string returnUrl, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "2025關聖帝君聖誕千秋祝壽謝恩祈福活動";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentEmperorGuansheng_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 9, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 關聖帝君聖誕 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_EmperorGuansheng_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_EmperorGuansheng_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 關聖帝君聖誕 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lingbaolidou_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YuchiDashu";    //玉敕大樹朝天宮(PR00004866)
                string item = "玉敕大樹朝天宮靈寶禮斗";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLingbaolidou_ma_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                //檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 12, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 靈寶禮斗 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lingbaolidou_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lingbaolidou_ma(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 靈寶禮斗 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_taoistJiaoCeremony_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, 
                string returnUrl, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DajiaCeremony";    //大甲鎮瀾宮普渡法會(CSENT64199)
                string item = "大甲鎮瀾宮重修慶成祈安七朝清醮活動";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentTaoistJiaoCeremony_da_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 3, 13, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "大甲鎮瀾宮 七朝清醮 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_TaoistJiaoCeremony_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_TaoistJiaoCeremony_da(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "大甲鎮瀾宮 七朝清醮 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lybc_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone,
                string returnUrl, string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務(PR00004720)
                string item = "台東東海龍門天聖宮護國息災梁皇大法會活動";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentLybc_dh_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 16, 15, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 護國息災梁皇大法會 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Lybc_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lybc_dh(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 護國息災梁皇大法會 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_sx(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Thewealthgod";    //神霄玉府財神會 PR00009720
                string item = "神霄玉府財神會館赦罪補庫服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_sx_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 33, 17, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "神霄玉府財神會館 赦罪補庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_sx(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_sx(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "神霄玉府財神會館 赦罪補庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies2_sx(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Thewealthgod";    //神霄玉府財神會 PR00009720
                string item = "神霄玉府財神會館供香轉運服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies2_sx_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 33, 19, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "神霄玉府財神會館 供香轉運 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies2_sx(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies2_sx(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "神霄玉府財神會館 供香轉運 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-LugangChenghuangmiao";    //鹿港城隍廟祈福點燈 PR00004755
                string item = "鹿港城隍廟補財庫服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_Lk_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 21, 16, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "鹿港城隍廟 補財庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_Lk(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "鹿港城隍廟 補財庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YunlinWulucaishen";    //斗六五路財神宮_宮廟服務 PR00004721
                string item = "斗六五路財神宮補財庫服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_Fw_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 15, 16, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "斗六五路財神宮 補財庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_Fw(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "斗六五路財神宮 補財庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaidongHailongmen";    //台東東海龍門天聖宮_宮廟服務(PR00004720)
                string item = "台東東海龍門天聖宮-天貺納福添運法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_dh_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 16, 11, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 天貺納福添運法會 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_dh(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "台東東海龍門天聖宮 天貺納福添運法會 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-YuchiDashu";    //玉敕大樹朝天宮(PR00004866)
                string item = "玉敕大樹朝天宮天赦日招財補運";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_ma_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 23, 7, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 天赦日招財補運 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_ma(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "玉敕大樹朝天宮 天赦日招財補運 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "桃園威天宮天赦日招財補運";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 7, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 天赦日招財補運 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 天赦日招財補運 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies2_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "桃園威天宮九九重陽天赦日雙重加持招財補運";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies2_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 14, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 九九重陽天赦日雙重加持招財補運 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies2_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies2_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 九九重陽天赦日雙重加持招財補運 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies3_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "桃園威天宮天公生招財補運";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies3_ty_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 14, 18, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "桃園威天宮 天公生招財補運 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies3_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies3_ty(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "桃園威天宮 天公生招財補運 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeCSBK";    //北港武德宮宮廟服務 PR00004401
                string item = "北港武德宮下元補庫";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_wu_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 6, 4, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "北港武德宮 下元補庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "北港武德宮 下元補庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_wu2(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeCSBK";    //北港武德宮宮廟服務 PR00004401
                string item = "北港武德宮天官武財神聖誕補財庫";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_wu_ReceiveURL2");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 6, 5, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "北港武德宮 天官武財神聖誕補財庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_wu2(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu2(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "北港武德宮 天官武財神聖誕補財庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_wu3(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-WudeLightUp";    //北港武德宮宮廟服務 PR00004401
                string item = "北港武德宮企業補財庫";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentSupplies_wu_ReceiveURL3");
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

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 6, 6, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "北港武德宮 企業補財庫 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Supplies_wu3(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu3(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "北港武德宮 企業補財庫 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_blessing_st(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                // 取得台北標準時間
                DateTime dtNow = LightDAC.GetTaipeiNow();
                BasePage basePage = new BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Shoutian";    //松柏嶺受天宮(PR00009720)
                string item = "松柏嶺受天宮祈安植福服務";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentBlessing_st_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                if (ChargeType == "JKOPAY")
                {
                    ChargeType = "JKOSPAY";
                }

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 35, 23, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "松柏嶺受天宮 祈安植福 AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Blessing_st(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Blessing_st(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "松柏嶺受天宮 祈安植福 更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

        }

        /// <summary>
        /// 如果 <paramref name="columnName"/> 存在、且值不是 null/空白，就呼叫 <see cref="OrderData(string, string)"/> 加入文字。
        /// </summary>
        /// <param name="sb">用來累加字串的 StringBuilder。</param>
        /// <param name="label">欄位對應顯示的 Label。</param>
        /// <param name="row">來源 DataRow。</param>
        /// <param name="columnName">要檢查的欄位名稱。</param>
        /// <param name="isHtml">若為 true，則不做 HTML encode（例如備註可能已含 HTML）。</param>
        private void AddField(StringBuilder sb, string label, DataRow row, string columnName, bool isHtml = false)
        {
            if (!row.Table.Columns.Contains(columnName))
                return;

            string text = row.Field<string>(columnName);
            if (string.IsNullOrWhiteSpace(text))
                return;

            sb.Append(isHtml
                ? OrderData(label, text)
                : OrderData(label, System.Net.WebUtility.HtmlEncode(text)));
        }

        //購買人資料列表-大甲鎮瀾宮
        public void GetPurchaserlist_da_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_da_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

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

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_da_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_da_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();
                    string sendback = dtData.Rows[i]["Sendback"].ToString();

                    ////服務項目
                    //purdueString = purdueType == "2" ? dtData.Rows[i]["FirstName"].ToString() + "家堂上歷代九玄七祖" : purdueString;
                    //purdueString = purdueType == "6" ? dtData.Rows[i]["FirstName"].ToString() + "家嬰靈" : purdueString;
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());                    //OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    //OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());

                    //if (dtData.Columns.Contains("sBirth"))
                    //{
                    //    var rawsBirth = dtData.Rows[i]["sBirth"];
                    //    if (rawsBirth != DBNull.Value)
                    //    {
                    //        var sBirthText = rawsBirth.ToString();
                    //        OrderInfo += OrderData("國曆生日", sBirthText);
                    //    }
                    //}


                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    if (sendback == "1")
                    {
                        //贊普-寄回+$250
                        OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
                        OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
                        OrderInfo += OrderData("收件人地址", dtData.Rows[i]["rAddress"].ToString());
                    }

                    if (purdueType == "3")
                    {
                        //指名亡者
                        OrderInfo += OrderData("亡者姓名", dtData.Rows[i]["DeathName"].ToString());
                        OrderInfo += OrderData("亡者農曆生日", dtData.Rows[i]["Birthday"].ToString() + (dtData.Rows[i]["DeathLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("亡者農曆時辰", dtData.Rows[i]["DeathBirthTime"].ToString());
                        OrderInfo += OrderData("亡者死亡日期", dtData.Rows[i]["Deathday"].ToString());
                    }

                    if (purdueType == "2" || purdueType == "6")
                    {
                        //九玄七祖、嬰靈
                        OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = 0;

                    cost = GetPurdueCost(AdminID, purdueType);

                    if (purdueType == "1")
                    {
                        //贊普
                        //cost = 1000;

                        cost = sendback == "1" ? cost += 250 : cost;
                    }
                    else
                    {
                        //超拔
                        //cost = 620;
                    }
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_da_TaoistJiaoCeremony(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.GettaoistJiaoCeremony_da_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string taoistJiaoCeremonyString = dtData.Rows[i]["TaoistJiaoCeremonyString"].ToString();
                    string taoistJiaoCeremonyType = dtData.Rows[i]["TaoistJiaoCeremonyType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", taoistJiaoCeremonyString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    string Name = dtData.Rows[i]["Name"].ToString();
                    string Name2 = dtData.Rows[i]["Name2"].ToString();
                    string Name3 = dtData.Rows[i]["Name3"].ToString();
                    string Name4 = dtData.Rows[i]["Name4"].ToString();
                    string Name5 = dtData.Rows[i]["Name5"].ToString();
                    string Name6 = dtData.Rows[i]["Name6"].ToString();
                    string Mobile = dtData.Rows[i]["Mobile"].ToString();
                    string Address = dtData.Rows[i]["Address"].ToString();
                    string Sendback = dtData.Rows[i]["Sendback"].ToString();
                    string rName = dtData.Rows[i]["rName"].ToString();
                    string rMobile = dtData.Rows[i]["rMobile"].ToString();
                    string rAddress = dtData.Rows[i]["rAddress"].ToString();

                    OrderInfo += OrderData("祈福人姓名", Name);

                    switch (taoistJiaoCeremonyType)
                    {
                        case "1":
                            OrderInfo += OrderData("祈福人姓名2", Name2);
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);

                            OrderInfo += OrderData("寄送方式", Sendback == "N" ? "不寄回(會轉送給弱勢團體)" : "寄回(加收運費250元)");
                            if (Sendback == "Y")
                            {
                                OrderInfo += OrderData("收件人姓名", rName);
                                OrderInfo += OrderData("收件人電話", rMobile);
                                OrderInfo += OrderData("收件人地址", rAddress);
                            }
                            break;
                        case "2":
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                        case "3":
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                        case "4":
                            OrderInfo += OrderData("祈福人姓名2", Name2);
                            OrderInfo += OrderData("祈福人姓名3", Name3);
                            OrderInfo += OrderData("祈福人姓名4", Name4);
                            OrderInfo += OrderData("祈福人姓名5", Name5);
                            OrderInfo += OrderData("祈福人姓名6", Name6);
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                        case "5":
                            OrderInfo += OrderData("祈福人姓名2", Name2);
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                        case "6":
                            OrderInfo += OrderData("祈福人姓名2", Name2);
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                        case "7":
                            OrderInfo += OrderData("祈福人姓名2", Name2);
                            OrderInfo += OrderData("祈福人電話", Mobile);
                            OrderInfo += OrderData("祈福人地址", Address);
                            break;
                    }

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetTaoistJiaoCeremonyCost(AdminID, taoistJiaoCeremonyType);
                    cost += Sendback == "Y" ? 250 : 0;

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_da(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_da.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_da(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_da.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_da(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 13:
                    //七朝清醮服務
                    reback = "https://bobibobi.tw/Temples/templeService_TaoistJiaoCeremony_da.aspx";

                    reback = CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_TaoistJiaoCeremony_da(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 13, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-新港奉天宮
        public void GetPurchaserlist_h_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_h_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_h_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_h_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderPurchaser += OrderData("購買人農曆生日", dtData.Rows[0]["AppBirth"].ToString() + (dtData.Rows[0]["AppLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                OrderPurchaser += OrderData("購買人農曆時辰", dtData.Rows[0]["AppBirthTime"].ToString());
                if (dtData.Columns.Contains("AppsBirth"))
                {
                    var rawsBirth = dtData.Rows[0]["AppsBirth"];
                    if (rawsBirth != DBNull.Value)
                    {
                        var sBirthText = rawsBirth.ToString();
                        OrderPurchaser += OrderData("購買人國曆生日", sBirthText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //服務項目金額
                    int cost = 0;

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";


                    if (dtData.Rows[0]["Merit"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "功德主($3000)");
                        if (dtData.Rows[0]["MeritText"].ToString() != "")
                        {
                            OrderInfo += Ordertextarea(dtData.Rows[0]["MeritText"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                        }
                    }

                    if (dtData.Rows[0]["Life"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "祝燈延壽消災($600)");
                    }

                    if (dtData.Rows[0]["Redress"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "四十九愿解冤釋結($600)");
                    }

                    if (dtData.Rows[0]["Creditor"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "冤親債主($500)");
                    }

                    if (dtData.Rows[0]["Ancestor"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "九玄七祖 (歷代祖先，迴向功德)($500)");
                        OrderInfo += OrderData("超度亡者姓氏", dtData.Rows[i]["AncestorLastname"].ToString());
                        OrderInfo += OrderData("祖先牌位地址", dtData.Rows[i]["AncestorAddress"].ToString());
                    }

                    if (dtData.Rows[0]["Deceased"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "功德迴向往生者($500)");
                        OrderInfo += OrderData("超度亡者姓名", dtData.Rows[i]["DeceasedName"].ToString());
                        OrderInfo += OrderData("祖先牌位地址", dtData.Rows[i]["DeceasedAddress"].ToString());
                    }

                    if (dtData.Rows[0]["Landlord"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "地祇主($500)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["LandlordNum"].ToString());
                    }

                    if (dtData.Rows[0]["Baby"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "嬰靈($500)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["BabyNum"].ToString());
                    }

                    if (dtData.Rows[0]["Animal"].ToString() == "1")
                    {
                        OrderInfo += OrderData("購買項目", "動物靈($500)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["AnimalNum"].ToString());
                    }

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    if (dtData.Columns.Contains("Mobile"))
                    {
                        var rawMobile = dtData.Rows[0]["Mobile"];
                        if (rawMobile != DBNull.Value)
                        {
                            var MobileText = rawMobile.ToString();
                            OrderInfo += OrderData("祈福人電話", MobileText);
                        }
                    }
                    if (dtData.Columns.Contains("Sex"))
                    {
                        var rawSex = dtData.Rows[0]["Sex"];
                        if (rawSex != DBNull.Value)
                        {
                            var SexText = rawSex.ToString();
                            OrderInfo += OrderData("祈福人性別", SexText);
                        }
                    }
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[0]["Birth"].ToString() + (dtData.Rows[0]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[0]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[0]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Rows[0]["PurdueNum"].ToString() != "0")
                    {
                        OrderInfo += OrderData("購買項目", "普 度 品($1000)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["PurdueNum"].ToString());
                    }

                    if (dtData.Rows[0]["RiceNum"].ToString() != "0")
                    {
                        OrderInfo += OrderData("購買項目", "白米認捐($200)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["RiceNum"].ToString());
                    }

                    if (dtData.Rows[0]["mMoneyNum"].ToString() != "0")
                    {
                        OrderInfo += OrderData("購買項目", "金紙部分($200)");
                        OrderInfo += OrderData("數量", dtData.Rows[i]["mMoneyNum"].ToString());
                    }

                    if (dtData.Rows[0]["Remark"].ToString() != "")
                    {
                        OrderInfo += OrderData("備註說明", "");

                        if (dtData.Columns.Contains("Remark"))
                        {
                            var rawRemark = dtData.Rows[0]["Remark"];
                            if (rawRemark != DBNull.Value)
                            {
                                var remarkText = rawRemark.ToString();
                                OrderInfo += Ordertextarea(TextToHtml(remarkText));
                            }
                        }
                        //OrderInfo += Ordertextarea(dtData.Rows[0]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                    }

                    OrderInfo += "</div></div>";

                    cost = getTotal(dtData);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_h(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_h.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_h(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_h.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_h(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }
        }
        public int getTotal(DataTable dtdata)
        {
            int result = 0;

            //功德主 $3000
            if (dtdata.Rows[0]["Merit"].ToString() == "1")
            {
                result += 3000;
            }

            //祝燈延壽消災 $600
            if (dtdata.Rows[0]["Life"].ToString() == "1")
            {
                result += 600;
            }

            //四十九愿解冤釋結 $600
            if (dtdata.Rows[0]["Redress"].ToString() == "1")
            {
                result += 600;
            }

            //冤親債主 $500
            if (dtdata.Rows[0]["Creditor"].ToString() == "1")
            {
                result += 500;
            }

            //九玄七祖 $500
            if (dtdata.Rows[0]["Ancestor"].ToString() == "1")
            {
                result += 500;
            }

            //功德迴向往生者 $500
            if (dtdata.Rows[0]["Deceased"].ToString() == "1")
            {
                result += 500;
            }

            //地祇主 $500
            if (dtdata.Rows[0]["Landlord"].ToString() == "1")
            {
                result += (500 * int.Parse(dtdata.Rows[0]["LandlordNum"].ToString()));
            }

            //嬰靈 $500
            if (dtdata.Rows[0]["Baby"].ToString() == "1")
            {
                result += (500 * int.Parse(dtdata.Rows[0]["BabyNum"].ToString()));
            }

            //動物靈 $500
            if (dtdata.Rows[0]["Animal"].ToString() == "1")
            {
                result += (500 * int.Parse(dtdata.Rows[0]["AnimalNum"].ToString()));
            }

            //普頓品 $1000
            result += (1000 * int.Parse(dtdata.Rows[0]["PurdueNum"].ToString()));

            //白米 $200
            result += (200 * int.Parse(dtdata.Rows[0]["RiceNum"].ToString()));

            //金紙 $200
            result += (200 * int.Parse(dtdata.Rows[0]["mMoneyNum"].ToString()));

            return result;
        }


        //購買人資料列表-北港武德宮
        public void GetPurchaserlist_wu_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_wu_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    if (dtData.Columns.Contains("HomeNum"))
                    {
                        var rawHomeNum = dtData.Rows[i]["HomeNum"];
                        if (rawHomeNum != DBNull.Value)
                        {
                            var HomeNumText = rawHomeNum.ToString();
                            OrderInfo += OrderData("祈福人市話", HomeNumText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wu_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_wu_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    if (dtData.Columns.Contains("HomeNum"))
                    {
                        var rawHomeNum = dtData.Rows[i]["HomeNum"];
                        if (rawHomeNum != DBNull.Value)
                        {
                            var HomeNumText = rawHomeNum.ToString();
                            OrderInfo += OrderData("祈福人市話", HomeNumText);
                        }
                    }
                    if (dtData.Columns.Contains("Email"))
                    {
                        var rawEmail = dtData.Rows[i]["Email"];
                        if (rawEmail != DBNull.Value)
                        {
                            var EmailText = rawEmail.ToString();
                            OrderInfo += OrderData("祈福人信箱", EmailText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wu_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getsupplies_wu_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    ////服務項目
                    OrderInfo += "<div class=\"ProductsName\">下元補庫</div>";

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 650;
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wu_Supplies2(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getsupplies_wu_Info2(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    ////服務項目
                    OrderInfo += "<div class=\"ProductsName\">天官武財神聖誕補財庫</div>";

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    //int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 650;
                    int cost = 650;
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wu_Supplies3(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getsupplies_wu_Info3(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    ////服務項目
                    OrderInfo += "<div class=\"ProductsName\">企業補財庫</div>";

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    //OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 1300;
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_wu(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_wu.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_wu.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 4:
                    //補財庫-下元補庫
                    reback = "https://bobibobi.tw/Temples/templeService_supplies.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_wu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 4, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 5:
                    //補財庫-呈疏補庫
                    reback = "https://bobibobi.tw/Temples/templeService_supplies2.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_wu2(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 5, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 6:
                    //補財庫-企業補財庫
                    Year = dtNow.Year.ToString();
                    reback = "https://bobibobi.tw/Temples/templeService_supplies3.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_wu3(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 6, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }
        }


        //購買人資料列表-西螺福興宮
        public void GetPurchaserlist_Fu_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fu_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fu_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Fu_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    //purdueString = purdueType == "2" ? dtData.Rows[i]["FirstName"].ToString() + "家堂上歷代九玄七祖" : purdueString;
                    //purdueString = purdueType == "6" ? dtData.Rows[i]["FirstName"].ToString() + "家嬰靈" : purdueString;
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //服務項目金額
                    int cost = 0;

                    cost = GetPurdueCost(AdminID, purdueType);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    switch (purdueType)
                    {
                        case "1":
                            //贊普
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawsBirth = dtData.Rows[i]["sBirth"];
                                if (rawsBirth != DBNull.Value)
                                {
                                    var sBirthText = rawsBirth.ToString();
                                    OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                                }
                            }
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            OrderInfo += OrderData("普品數量", dtData.Rows[i]["Count"].ToString());

                            cost = cost * int.Parse(dtData.Rows[i]["Count"].ToString());
                            break;
                        case "2":
                            //九玄七祖
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祖先姓氏", dtData.Rows[i]["FirstName"].ToString());
                            OrderInfo += OrderData("牌位所在地址", dtData.Rows[i]["Address"].ToString());

                            int Count = int.Parse(dtData.Rows[i]["Count"].ToString());
                            string addpurdue = Count == 1 ? "加購" : "不加購";
                            OrderInfo += OrderData("加購普品", addpurdue);
                            cost += Count == 1 ? 1500 : 0;

                            int GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                        case "3":
                            //亡者
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                            OrderInfo += OrderData("牌位所在地址", dtData.Rows[i]["Address"].ToString());
                            
                            Count = int.Parse(dtData.Rows[i]["Count"].ToString());
                            addpurdue = Count == 1 ? "加購" : "不加購";
                            OrderInfo += OrderData("加購普品", addpurdue);
                            cost += Count == 1 ? 1500 : 0;

                            GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                        case "4":
                            //地基主
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("超度地址", dtData.Rows[i]["Address"].ToString());

                            GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                        case "5":
                            //冤親債主-超度指定對象
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                            OrderInfo += OrderData("超度對象地址", dtData.Rows[i]["Address"].ToString());

                            GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                        case "6":
                            //嬰靈-超度指定對象
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                            OrderInfo += OrderData("超度對象地址", dtData.Rows[i]["Address"].ToString());

                            GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                        case "11":
                            //動物靈-超度指定對象
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                            OrderInfo += OrderData("超度對象地址", dtData.Rows[i]["Address"].ToString());

                            GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                            OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                            cost += GoldPaperCount * 300;
                            break;
                    }

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }


                    OrderInfo += "</div></div>";

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Fu(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fu.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_Fu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Fu.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_Fu(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-台南正統鹿耳門聖母廟
        public void GetPurchaserlist_Luer_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Luer_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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

                    if (dtData.Rows[i]["PetName"].ToString() != "")
                    {
                        OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("飼主Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                        OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                        OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                    }
                    else
                    {
                        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    }

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    if (dtData.Rows[i]["Msg"].ToString() != "")
                    {
                        OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Luer_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Luer_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    //purdueString = purdueType == "2" ? dtData.Rows[i]["FirstName"].ToString() + "家堂上歷代九玄七祖" : purdueString;
                    //purdueString = purdueType == "6" ? dtData.Rows[i]["FirstName"].ToString() + "家嬰靈" : purdueString;
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += OrderData("普度組數", dtData.Rows[i]["Count"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Luer(int AdminID, int ApplicantID, int kind, string Year, int type)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Luer.aspx";
                    if (type == 2)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer.aspx";
                    }
                    else if (type == 3)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer_twm.aspx";
                    }

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_Luer(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Luer.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_Luer(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }
        }


        //購買人資料列表-桃園威天宮
        public void GetPurchaserlist_ty_Lights(int AdminID, int ApplicantID, int type, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            switch (type)
            {
                case 1:
                    //一般點燈
                    DataTable dtData = objLightDAC.Getlights_ty_Info(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                        string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                        string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                        OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                AppEmail = appEmailText;
                                OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                            }
                        }

                        if (dtData.Columns.Contains("AppAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            string appAddressText = firstRow.Field<string>("AppAddress");
                            if (!string.IsNullOrWhiteSpace(appAddressText))
                            {
                                OrderPurchaser += OrderData("購買人地址", appAddressText);
                            }
                        }

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
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                            if (dtData.Columns.Contains("Remark"))
                            {
                                var rawRemark = dtData.Rows[i]["Remark"];
                                if (rawRemark != DBNull.Value)
                                {
                                    var remarkText = rawRemark.ToString();
                                    OrderInfo += OrderData("備註", TextToHtml(remarkText));
                                }
                            }

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            payStatus = Total> 3000 ? true : false;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //活動-孝親祈福燈
                    dtData = objLightDAC.Getlights_ty_mom_Info(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                        string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                        string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                        OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                AppEmail = appEmailText;
                                OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                            }
                        }

                        if (dtData.Columns.Contains("AppAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            string appAddressText = firstRow.Field<string>("AppAddress");
                            if (!string.IsNullOrWhiteSpace(appAddressText))
                            {
                                OrderPurchaser += OrderData("購買人地址", appAddressText);
                            }
                        }

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
                            OrderInfo += OrderData("國歷生日", dtData.Rows[i]["sBirth"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                            if (dtData.Columns.Contains("Remark"))
                            {
                                var rawRemark = dtData.Rows[i]["Remark"];
                                if (rawRemark != DBNull.Value)
                                {
                                    var remarkText = rawRemark.ToString();
                                    OrderInfo += OrderData("備註", TextToHtml(remarkText));
                                }
                            }

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            cost = GetLightsCost(AdminID, lightsType);

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            payStatus = Total> 3000 ? true : false;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }
        public void GetPurchaserlist_ty_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_ty_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    int count = 0;
                    string purdueItem = dtData.Rows[i]["PurdueItem"].ToString();
                    string deathName = dtData.Rows[i]["DeathName"].ToString();
                    string firstName = dtData.Rows[i]["FirstName"].ToString();
                    string momName = dtData.Rows[i]["MomName"].ToString();
                    string lastName = dtData.Rows[i]["LastName"].ToString();
                    string reason = dtData.Rows[i]["Reason"].ToString();
                    string licenseNum = dtData.Rows[i]["LicenseNum"].ToString();
                    string deathAddress = dtData.Rows[i]["DeathZipCode"].ToString() + " " + dtData.Rows[i]["DeathAddress"].ToString();

                    string purdueItem1 = dtData.Rows[i]["PurdueItem1"].ToString();
                    string deathName1 = dtData.Rows[i]["DeathName1"].ToString();
                    string firstName1 = dtData.Rows[i]["FirstName1"].ToString();
                    string momName1 = dtData.Rows[i]["MomName1"].ToString();
                    string lastName1 = dtData.Rows[i]["LastName1"].ToString();
                    string reason1 = dtData.Rows[i]["Reason1"].ToString();
                    string licenseNum1 = dtData.Rows[i]["LicenseNum1"].ToString();
                    string deathAddress1 = dtData.Rows[i]["DeathZipCode1"].ToString() + " " + dtData.Rows[i]["DeathAddress1"].ToString();

                    switch (purdueType)
                    {
                        case "1":
                            //贊普
                            int.TryParse(dtData.Rows[i]["Count"].ToString(), out count);
                            OrderInfo += OrderData("普度數量", dtData.Rows[i]["Count"].ToString());
                            break;
                        case "14":
                            //孝道功德主
                            count = 1;
                            TabletChecked(purdueItem, deathName, firstName, momName, lastName, reason, licenseNum, deathAddress, purdueItem1, deathName1, firstName1, momName1, 
                                lastName1, reason1, licenseNum1, deathAddress1);
                            break;
                        case "15":
                            //光明功德主
                            count = 1;
                            TabletChecked(purdueItem, deathName, firstName, momName, lastName, reason, licenseNum, deathAddress, purdueItem1, deathName1, firstName1, momName1,
                                lastName1, reason1, licenseNum1, deathAddress1);
                            break;
                        case "16":
                            //發心功德主
                            count = 1;
                            TabletChecked(purdueItem, deathName, firstName, momName, lastName, reason, licenseNum, deathAddress, purdueItem1, deathName1, firstName1, momName1,
                                lastName1, reason1, licenseNum1, deathAddress1);
                            break;
                        case "18":
                            //白米50台斤
                            int.TryParse(dtData.Rows[i]["Count_50rice"].ToString(), out count);
                            OrderInfo += OrderData("普度白米50台斤數量", dtData.Rows[i]["Count_50rice"].ToString());
                            break;
                        case "19":
                            //白米3台斤
                            int.TryParse(dtData.Rows[i]["Count_3rice"].ToString(), out count);
                            OrderInfo += OrderData("普度白米3台斤數量", dtData.Rows[i]["Count_3rice"].ToString());
                            break;
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = count * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_Supplies(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_ty_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("國歷生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, suppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_EmperorGuansheng(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.GetemperorGuansheng_ty_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string emperorGuanshengString = dtData.Rows[i]["EmperorGuanshengString"].ToString();
                    string emperorGuanshengType = dtData.Rows[i]["EmperorGuanshengType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", emperorGuanshengString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetEmperorGuanshengCost(AdminID, emperorGuanshengType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_Supplies2(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies2_ty_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string SuppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string SuppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", SuppliesString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("國歷生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, SuppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_Supplies3(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies3_ty_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string SuppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string SuppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", SuppliesString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("國歷生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, SuppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_QnLight(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getqnlight_ty_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string qnlightString = dtData.Rows[i]["QnLightString"].ToString();
                    string qnlightType = dtData.Rows[i]["QnLightType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", qnlightString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetQnLightCost(AdminID, qnlightType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ty(int AdminID, int ApplicantID, int kind, int type, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);
            string reback = string.Empty;

            switch (kind)
            {
                case 1:
                    //點燈服務
                    switch (type)
                    {
                        case 1:
                            //一般點燈
                            reback = "https://bobibobi.tw/Temples/templeService_lights_ty.aspx";

                            CheckedURL(reback);

                            if (objLightDAC.Checkedappcharge_Lights_ty(ApplicantID, AdminID, Year))
                            {
                                if (OrderPurchaser == "")
                                {
                                    Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                                }
                                else
                                {
                                    DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, 1, Year);
                                    if (dtLASTTIME.AddMinutes(20) < dtNow)
                                    {
                                        Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                            }
                            break;
                        case 2:
                            //活動-孝親祈福燈
                            reback = "https://bobibobi.tw/Temples/templeService_lights_ty_mom.aspx";

                            CheckedURL(reback);

                            if (objLightDAC.Checkedappcharge_Lights_ty_mom(ApplicantID, AdminID, Year))
                            {
                                if (OrderPurchaser == "")
                                {
                                    Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                                }
                                else
                                {
                                    DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, 2, Year);
                                    if (dtLASTTIME.AddMinutes(20) < dtNow)
                                    {
                                        Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                            }
                            break;
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_ty.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 7:
                    //天赦日招財補運
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_ty.aspx" +

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 7, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 9:
                    //關聖帝君聖誕
                    reback = "https://bobibobi.tw/Temples/templeService_EmperorGuansheng_ty.aspx";
                    if (Request["bobi"] != null)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_EmperorGuansheng_bobi_ty.aspx";
                    }

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_EmperorGuansheng_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 9, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 14:
                    //九九重陽天赦日
                    reback = "https://bobibobi.tw/Temples/templeService_Supplies2_ty.aspx";
                    if (Request["bobi"] != null)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_Supplies2_bobi_ty.aspx";
                    }

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies2_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 14, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 18:
                    //天公生招財補運
                    reback = "https://bobibobi.tw/Temples/templeService_Supplies3_ty.aspx";
                    if (Request["bobi"] != null)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_Supplies3_bobi_ty.aspx";
                    }

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies3_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 18, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 25:
                    //千手觀音千燈迎佛法會服務
                    reback = "https://bobibobi.tw/Temples/templeService_qnlight_ty.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_QnLight_ty(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 25, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }
        public void TabletChecked(string purdueItem, string deathName, string firstName, string momName, string lastName, string reason, string licenseNum, string deathaddress, 
            string purdueItem1, string deathName1, string firstName1, string momName1, string lastName1, string reason1, string licenseNum1, string deathaddress1)
        {
            OrderInfo += OrderData("超薦項目", purdueItem);
            purdueItem = purdueItem.IndexOf("超薦") >= 0 || purdueItem.IndexOf("花雕") >= 0 ? purdueItem.Substring(5, purdueItem.Length - 5) : purdueItem;
            switch (purdueItem)
            {
                case "顯考O公O府君":
                    OrderInfo += OrderData("顯考(姓氏)公", firstName);
                    OrderInfo += OrderData("(名字)府君", lastName);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "顯妣O母 氏OO夫人":
                    OrderInfo += OrderData("(夫姓)母", momName);
                    OrderInfo += OrderData("(本姓)氏", firstName);
                    OrderInfo += OrderData("(名字)夫人", lastName);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "O氏歷代祖先":
                    OrderInfo += OrderData("(姓氏)氏", firstName);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "空白牌":
                    OrderInfo += OrderData("超薦事由", reason);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "車號OOO車輛誤傷之生靈":
                    OrderInfo += OrderData("車牌(車牌數字)", licenseNum);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "無緣子女":
                    OrderInfo += OrderData("無緣子女(姓名)", deathName);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "累劫冤親債主":
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "地基主":
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "過去飼養一切動物之靈":
                    OrderInfo += OrderData("寵物(姓名)", deathName);
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
                case "十方法界一切有情眾生":
                    OrderInfo += OrderData("被超薦者地址", deathaddress);
                    break;
            }

            if (purdueItem1 != "")
            {
                OrderInfo += OrderData("超薦項目", purdueItem1);
                purdueItem1 = purdueItem1.IndexOf("超薦") >= 0 ? purdueItem1.Substring(5, purdueItem1.Length - 5) : purdueItem1;
                switch (purdueItem1)
                {
                    case "顯考O公O府君":
                        OrderInfo += OrderData("顯考(姓氏)公", firstName1);
                        OrderInfo += OrderData("(名字)府君", lastName1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "顯妣O母 氏OO夫人":
                        OrderInfo += OrderData("(夫姓)母", momName1);
                        OrderInfo += OrderData("(本姓)氏", firstName1);
                        OrderInfo += OrderData("(名字)夫人", lastName1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "O氏歷代祖先":
                        OrderInfo += OrderData("(姓氏)氏", firstName1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "空白牌":
                        OrderInfo += OrderData("超薦事由", reason1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "車號OOO車輛誤傷之生靈":
                        OrderInfo += OrderData("車牌(車牌數字)", licenseNum1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "無緣子女":
                        OrderInfo += OrderData("無緣子女(姓名)", deathName1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "累劫冤親債主":
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "地基主":
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "過去飼養一切動物之靈":
                        OrderInfo += OrderData("寵物(姓名)", deathName1);
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                    case "十方法界一切有情眾生":
                        OrderInfo += OrderData("被超薦者地址", deathaddress1);
                        break;
                }
            }
        }

        //購買人資料列表-斗六五路財神宮
        public void GetPurchaserlist_Fw_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fw_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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

                    if (dtData.Rows[i]["PetName"].ToString() != "")
                    {
                        OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("飼主Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                        OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                        OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                    }
                    else
                    {
                        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    }

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Fw_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();
                    int count_rice = 0;
                    int.TryParse(dtData.Rows[i]["Count_rice"].ToString(), out count_rice);

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    if (count_rice != 0)
                    {
                        OrderInfo += OrderData("捐獻白米", dtData.Rows[i]["Count_rice"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    cost += 200 * count_rice;
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_Supplies(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_Fw_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_AnDou(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getandou_Fw_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string andouString = dtData.Rows[i]["AnDouString"].ToString();
                    string andouType = dtData.Rows[i]["AnDouType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", andouString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    if (dtData.Rows[i]["PetName"].ToString() != "")
                    {
                        OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("飼主Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                        OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                        OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                    }
                    else
                    {
                        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                        OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                        OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                        OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    }

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Fw(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fw.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_Fw(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Fw.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_Fw(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 16:
                    //補財庫服務
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_Fw.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_Fw(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 16, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 20:
                    //安斗服務
                    reback = "https://bobibobi.tw/Temples/templeService_andou_Fw.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_AnDou_Fw(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 20, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-台東東海龍門天聖宮
        public void GetPurchaserlist_dh_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_dh_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_dh_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    switch (purdueType)
                    {
                        case "2":
                            OrderInfo += OrderData("祖先姓氏", dtData.Rows[i]["FirstName"].ToString());
                            OrderInfo += OrderData("牌位地址", dtData.Rows[i]["DeathAddress"].ToString());
                            break;
                        case "3":
                            OrderInfo += OrderData("往生親友姓名", dtData.Rows[i]["DeathName"].ToString());
                            OrderInfo += OrderData("往生日期", dtData.Rows[i]["Deathday"].ToString());
                            OrderInfo += OrderData("牌位地址", dtData.Rows[i]["DeathAddress"].ToString());
                            break;
                        case "5":
                            //OrderInfo += OrderData("超薦者姓名", dtData.Rows[i]["DeathName"].ToString());
                            //OrderInfo += OrderData("農曆生日", dtData.Rows[i]["DeathBirth"].ToString() + (dtData.Rows[i]["DeathLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            //OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["DeathBirthTime"].ToString());
                            //OrderInfo += OrderData("牌位地址", dtData.Rows[i]["DeathAddress"].ToString());
                            break;
                        case "6":
                            OrderInfo += OrderData("往生日期", dtData.Rows[i]["Deathday"].ToString());
                            break;
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Supplies(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_dh_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    cost = 0;
                    int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0, Count5 = 0, Count6 = 0, Count7 = 0, Count8 = 0, Count9 = 0;
                    int Count10 = 0, Count11 = 0, Count12 = 0, Count13 = 0, Count14 = 0;

                    int.TryParse(dtData.Rows[i]["Count1"].ToString(), out Count1);
                    int.TryParse(dtData.Rows[i]["Count2"].ToString(), out Count2);
                    int.TryParse(dtData.Rows[i]["Count3"].ToString(), out Count3);
                    int.TryParse(dtData.Rows[i]["Count4"].ToString(), out Count4);
                    int.TryParse(dtData.Rows[i]["Count5"].ToString(), out Count5);
                    int.TryParse(dtData.Rows[i]["Count6"].ToString(), out Count6);
                    int.TryParse(dtData.Rows[i]["Count7"].ToString(), out Count7);
                    int.TryParse(dtData.Rows[i]["Count8"].ToString(), out Count8);
                    int.TryParse(dtData.Rows[i]["Count9"].ToString(), out Count9);
                    int.TryParse(dtData.Rows[i]["Count10"].ToString(), out Count10);
                    int.TryParse(dtData.Rows[i]["Count11"].ToString(), out Count11);
                    int.TryParse(dtData.Rows[i]["Count12"].ToString(), out Count12);
                    int.TryParse(dtData.Rows[i]["Count13"].ToString(), out Count13);
                    int.TryParse(dtData.Rows[i]["Count14"].ToString(), out Count14);


                    // 1. 水晶蓮花燈 – 單價 2388
                    if (Count1 > 0)
                    {
                        cost += Count1 > 0 ? Count1 * 2388 : 0;
                        OrderInfo += OrderData("水晶蓮花燈 數量", Count1.ToString());
                    }

                    // 2. 財神財寶箱 – 單價 800
                    if (Count2 > 0)
                    {
                        cost += Count2 > 0 ? Count2 * 800 : 0;
                        OrderInfo += OrderData("財神財寶箱 數量", Count2.ToString());
                    }

                    // 3. 虎爺財寶箱 – 單價 800
                    if (Count3 > 0)
                    {
                        cost += Count3 > 0 ? Count3 * 800 : 0;
                        OrderInfo += OrderData("虎爺財寶箱 數量", Count3.ToString());
                    }

                    // 4. 旺龍紫氣寶燈 – 單價 700
                    if (Count4 > 0)
                    {
                        cost += Count4 > 0 ? Count4 * 700 : 0;
                        OrderInfo += OrderData("旺龍紫氣寶燈 數量", Count4.ToString());
                    }

                    // 5. 玉皇宥罪錫福七星燈 – 單價 1000
                    if (Count5 > 0)
                    {
                        cost += Count5 > 0 ? Count5 * 1000 : 0;
                        OrderInfo += OrderData("玉皇宥罪錫福七星燈 數量", Count5.ToString());
                    }

                    // 6. 通天點金大龍香 – 單價 1000
                    if (Count6 > 0)
                    {
                        cost += Count6 > 0 ? Count6 * 1000 : 0;
                        OrderInfo += OrderData("通天點金大龍香 數量", Count6.ToString());
                    }

                    // 7. 五路財神香 – 單價 200
                    if (Count7 > 0)
                    {
                        cost += Count7 > 0 ? Count7 * 200 : 0;
                        OrderInfo += OrderData("五路財神香 數量", Count7.ToString());
                    }

                    // 8. 開恩赦罪 科儀 – 單價 600
                    if (Count8 > 0)
                    {
                        cost += Count8 > 0 ? Count8 * 600 : 0;
                        OrderInfo += OrderData("開恩赦罪 科儀 數量", Count8.ToString());
                    }

                    // 9. 消災解厄 科儀 – 單價 600
                    if (Count9 > 0)
                    {
                        cost += Count9 > 0 ? Count9 * 600 : 0;
                        OrderInfo += OrderData("消災解厄 科儀 數量", Count9.ToString());
                    }

                    // 10. 補運 科儀 – 單價 600
                    if (Count10 > 0)
                    {
                        cost += Count10 > 0 ? Count10 * 600 : 0;
                        OrderInfo += OrderData("補運 科儀 數量", Count10.ToString());
                    }

                    // 11. 身體康健 科儀 – 單價 600
                    if (Count11 > 0)
                    {
                        cost += Count11 > 0 ? Count11 * 600 : 0;
                        OrderInfo += OrderData("身體康健 科儀 數量", Count11.ToString());
                    }

                    // 12. 補財庫 科儀 – 單價 600
                    if (Count12 > 0)
                    {
                        cost += Count12 > 0 ? Count12 * 600 : 0;
                        OrderInfo += OrderData("補財庫 科儀 數量", Count12.ToString());
                    }

                    // 13. 補文昌 科儀 – 單價 600
                    if (Count13 > 0)
                    {
                        cost += Count13 > 0 ? Count13 * 600 : 0;
                        OrderInfo += OrderData("補文昌 科儀 數量", Count13.ToString());
                    }

                    // 14. 招貴人 科儀 – 單價 600
                    if (Count14 > 0)
                    {
                        cost += Count14 > 0 ? Count14 * 600 : 0;
                        OrderInfo += OrderData("招貴人 科儀 數量", Count14.ToString());
                    }

                    OrderInfo += "</div></div>";

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Lybc(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlybc_dh_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lybcString = dtData.Rows[i]["LybcString"].ToString();
                    string lybcType = dtData.Rows[i]["LybcType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lybcString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }


                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetLybcCost(AdminID, lybcType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_dh(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_dh.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_dh(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_dh.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_dh(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 15:
                    //護國息災梁皇大法會服務
                    reback = "https://bobibobi.tw/Temples/templeService_lybc_dh.aspx";

                    reback = CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lybc_dh(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 15, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-五股賀聖宮
        public void GetPurchaserlist_Hs_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Hs_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Hs(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Hs.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_Hs(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }

        //購買人資料列表-鹿港城隍廟
        public void GetPurchaserlist_Lk_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Lk_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }
                //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                {
                    //Total += 100;
                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());

                    DataRow firstRow = dtData.Rows[0];
                    string addressText = null;

                    // 嘗試取得 ApprAddress 或 AppAddress 欄位的值（順序優先）
                    if (dtData.Columns.Contains("ApprAddress"))
                    {
                        addressText = firstRow.Field<string>("ApprAddress");
                    }
                    else if (dtData.Columns.Contains("AppAddress"))
                    {
                        addressText = firstRow.Field<string>("AppAddress");
                    }

                    if (!string.IsNullOrWhiteSpace(addressText))
                    {
                        OrderPurchaser += OrderData("收件人地址", addressText);
                    }
                }


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
                    //cost += 100;

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //if (dtData.Rows[i]["Sendback"].ToString() == "Y")
                    //{
                    //    cost += 100;
                    //    OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
                    //    OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
                    //    OrderInfo += OrderData("收件人地址", dtData.Rows[i]["rAddress"].ToString());
                    //}

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Lk_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Lk_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                {
                    //Total += 100;
                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                    OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["AppAddress"].ToString());
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    if (dtData.Columns.Contains("HomeNum"))
                    {
                        var rawHomeNum = dtData.Rows[i]["HomeNum"];
                        if (rawHomeNum != DBNull.Value)
                        {
                            var HomeNumText = rawHomeNum.ToString();
                            OrderInfo += OrderData("祈福人市話", HomeNumText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Lk_Supplies(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_Lk_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }
                //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                {
                    //Total += 100;
                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());

                    DataRow firstRow = dtData.Rows[0];
                    string addressText = null;

                    // 嘗試取得 ApprAddress 或 AppAddress 欄位的值（順序優先）
                    if (dtData.Columns.Contains("ApprAddress"))
                    {
                        addressText = firstRow.Field<string>("ApprAddress");
                    }
                    else if (dtData.Columns.Contains("AppAddress"))
                    {
                        addressText = firstRow.Field<string>("AppAddress");
                    }

                    if (!string.IsNullOrWhiteSpace(addressText))
                    {
                        OrderPurchaser += OrderData("收件人地址", addressText);
                    }
                }


                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Lk(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Lk.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_Lk(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Lk.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_Lk(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 16:
                    //補財庫服務
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_Lk.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_Lk(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 16, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-玉敕大樹朝天宮
        public void GetPurchaserlist_ma_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_ma_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_ma_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    if (dtData.Columns.Contains("sBirth"))
                    {
                        var rawsBirth = dtData.Rows[i]["sBirth"];
                        if (rawsBirth != DBNull.Value)
                        {
                            var sBirthText = rawsBirth.ToString();
                            OrderInfo += OrderData("祈福人國曆生日", sBirthText);
                        }
                    }
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    switch (purdueType)
                    {
                        case "2":
                            OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                            break;
                        case "6":
                            OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                            break;
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Supplies(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_ma_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, suppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Lingbaolidou(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlingbaolidou_ma_Info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lingbaolidouString = dtData.Rows[i]["LingbaolidouString"].ToString();
                    string lingbaolidouType = dtData.Rows[i]["LingbaolidouType"].ToString();

                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lingbaolidouString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    int Count = 1;
                    int.TryParse(dtData.Rows[i]["Count"].ToString(), out Count);

                    //服務項目金額
                    cost = GetLingbaolidouCost(AdminID, lingbaolidouType) * Count;

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ma(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_ma.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_ma(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_ma.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_ma(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 7:
                    //天赦日招財補運
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_ma.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_ma(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 7, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 12:
                    //靈寶禮斗
                    reback = "https://bobibobi.tw/Temples/templeService_lingbaolidou_ma.aspx";
                    if (Request["fet"] != null)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_lingbaolidou_ma_fet.aspx";
                    }

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lingbaolidou_ma(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 12, 1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-台灣道教總廟無極三清總道院
        public void GetPurchaserlist_wjsan_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_wjsan_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    int lightstype = 0;
                    int.TryParse(lightsType, out lightstype);
                    if (lightstype > 24 && !payStatus)
                    {
                        payStatus = true;
                    }

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wjsan_Purdue(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_wjsan_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wjsan_AnDou(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getandou_wjsan_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string andouString = dtData.Rows[i]["AnDouString"].ToString();
                    string andouType = dtData.Rows[i]["AnDouType"].ToString();

                    int andoutype = 0;
                    int.TryParse(andouType, out andoutype);
                    if (andoutype > 24 && !payStatus)
                    {
                        payStatus = true;
                    }

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", andouString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wjsan_Huaguo(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Gethuaguo_wjsan_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppAddress"))
                {
                    DataRow firstRow = dtData.Rows[0];
                    string appAddressText = firstRow.Field<string>("AppAddress");
                    if (!string.IsNullOrWhiteSpace(appAddressText))
                    {
                        OrderPurchaser += OrderData("購買人地址", appAddressText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string huaguoString = dtData.Rows[i]["HuaguoString"].ToString();
                    string huaguoType = dtData.Rows[i]["HuaguoType"].ToString();

                    int huaguotype = 0;
                    int.TryParse(huaguoType, out huaguotype);
                    if ((huaguotype == 5 || huaguotype == 6) && !payStatus)
                    {
                        payStatus = true;
                    }

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", huaguoString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetHuaguoCost(AdminID, huaguoType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_wjsan(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_wjsan.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_wjsan(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_wjsan.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Purdue_wjsan(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 20:
                    //安斗服務
                    reback = "https://bobibobi.tw/Temples/templeService_andou_wjsan.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_AnDou_wjsan(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 20, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 21:
                    //供花供果服務
                    reback = "https://bobibobi.tw/Temples/templeService_huaguo_wjsan.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Huaguo_wjsan(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 21, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-神霄玉府財神會館
        //public void GetPurchaserlist_sx_Lights(int AdminID, int ApplicantID, string Year)
        //{
        //    LightDAC objLightDAC = new LightDAC(this);
        //    int cost = 0;

        //    DataTable dtData = objLightDAC.Getlights_sx_Info(ApplicantID, Year);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


        //        string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
        //        string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

        //        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
        //        OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
        //        OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

        //        if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
        //        {
        //            Total += 100;
        //            OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
        //            OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
        //            OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["ApprAddress"].ToString());
        //        }


        //        OrderInfo = string.Empty;

        //        for (int i = 0; i < dtData.Rows.Count; i++)
        //        {
        //            OrderInfo += "<li><div>";

        //            string lightsString = dtData.Rows[i]["LightsString"].ToString();
        //            string lightsType = dtData.Rows[i]["LightsType"].ToString();

        //            ////服務項目
        //            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

        //            //服務項目金額
        //            cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

        //            //祈福人內容列表
        //            OrderInfo += "<div class=\"ProductsInfo\">";

        //            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
        //            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
        //            //string sex = "善男";
        //            //if (dtData.Rows[i]["Sex"].ToString() == "F")
        //            //{
        //            //    sex = "信女";
        //            //}
        //            //OrderInfo += OrderData("性別", sex);
        //            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
        //            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
        //            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
        //            OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
        //            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
        //            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
        //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

        //            //if (dtData.Rows[i]["Sendback"].ToString() == "Y")
        //            //{
        //            //    cost += 100;
        //            //    OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
        //            //    OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
        //            //    OrderInfo += OrderData("收件人地址", dtData.Rows[i]["rAddress"].ToString());
        //            //}

        //            OrderInfo += "</div></div>";


        //            OrderInfo += "<div>$ " + cost + "元</div>";
        //            Total += cost;

        //            OrderInfo += "</li>";
        //        }
        //    }
        //}
        //public void GetPurchaserlist_sx_Purdue(int AdminID, int ApplicantID, string Year)
        //{
        //    LightDAC objLightDAC = new LightDAC(this);

        //    DataTable dtData = objLightDAC.Getpurdue_sx_Info(ApplicantID, Year);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                //string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                //string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

        //        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
        //        OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

        //        OrderInfo = string.Empty;

        //        for (int i = 0; i < dtData.Rows.Count; i++)
        //        {
        //            OrderInfo += "<li><div>";

        //            string purdueString = dtData.Rows[i]["PurdueString"].ToString();
        //            string purdueType = dtData.Rows[i]["PurdueType"].ToString();

        //            ////服務項目
        //            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

        //            //祈福人內容列表
        //            OrderInfo += "<div class=\"ProductsInfo\">";

        //            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
        //            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
        //            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
        //            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
        //            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
        //            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
        //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

        //            OrderInfo += "</div></div>";

        //            //服務項目金額
        //            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
        //            OrderInfo += "<div>$ " + cost + "元</div>";
        //            Total += cost;

        //            OrderInfo += "</li>";
        //        }
        //    }
        //}
        public void GetPurchaserlist_sx_Supplies(int AdminID, int ApplicantID, int kind, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;
            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 17:
                    //赦罪補庫
                    dtData = objLightDAC.Getsupplies_sx_Info(ApplicantID, Year);
                    break;
                case 19:
                    //供香轉運
                    dtData = objLightDAC.Getsupplies2_sx_Info(ApplicantID, Year);
                    break;
            }

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                DataRow firstRow = dtData.Rows[0];
                string addressText = null;

                // 嘗試取得 ApprAddress 或 AppAddress 欄位的值（順序優先）
                if (dtData.Columns.Contains("ApprAddress"))
                {
                    addressText = firstRow.Field<string>("ApprAddress");
                }
                else if (dtData.Columns.Contains("AppAddress"))
                {
                    addressText = firstRow.Field<string>("AppAddress");
                }

                if (!string.IsNullOrWhiteSpace(addressText))
                {
                    OrderPurchaser += OrderData("購買人地址", addressText);
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string suppliesString = dtData.Rows[i]["SuppliesString"].ToString();
                    string suppliesType = dtData.Rows[i]["SuppliesType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", suppliesString);

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total> 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_sx(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);
            string reback = "https://bobibobi.tw";

            switch (kind)
            {
                //case 1:
                //    //點燈服務
                //    string reback = "https://bobibobi.tw/Temples/templeService_lights_sx.aspx";
                //    if (objLightDAC.Checkedappcharge_Lights_sx(ApplicantID, AdminID, Year))
                //    {
                //        if (OrderPurchaser == "")
                //        {
                //            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                //        }
                //        else
                //        {
                //            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                //            if (dtLASTTIME.AddMinutes(20) < dtNow)
                //            {
                //                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                //    }
                //    break;
                //case 2:
                //    //普度服務
                //    reback = "https://bobibobi.tw/Temples/templeService_purdue_sx.aspx";
                //    if (objLightDAC.Checkedappcharge_Purdue_sx(ApplicantID, AdminID, Year))
                //    {
                //        if (OrderPurchaser == "")
                //        {
                //            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                //        }
                //        else
                //        {
                //            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year)
                //            if (dtLASTTIME.AddMinutes(20) < dtNow)
                //            {
                //                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                //    }
                //    break;
                case 17:
                    //補財庫服務-赦罪補庫
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_sx.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies_sx(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 17, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 19:
                    //補財庫服務-供香轉運
                    reback = "https://bobibobi.tw/Temples/templeService_supplies2_sx.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Supplies2_sx(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 19, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-桃園龍德宮
        public void GetPurchaserlist_ld_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;
            var AppSendback = "N";

            DataTable dtData = objLightDAC.Getlights_ld_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = "<div class='appsendback'>";

                OrderPurchaser += OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                if (dtData.Columns.Contains("AppSendback"))
                {
                    var rawAppSendback = dtData.Rows[0]["AppSendback"];
                    if (rawAppSendback != DBNull.Value)
                    {
                        AppSendback = rawAppSendback.ToString();
                        OrderPurchaser += OrderData("贈品處理方式", AppSendback);

                        if (AppSendback == "Y")
                        {
                            Total += AppSendback == "Y" ? 60 : 0;
                            OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                            OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                            OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["AppAddress"].ToString());
                        }
                    }
                }

                OrderPurchaser += "</div>";

                if (AppSendback == "Y")
                {
                    OrderPurchaser += "<div class='appsendback'>$ " + 60 + "元</div>";
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ld(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_ld.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_ld(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-松柏嶺受天宮
        public void GetPurchaserlist_st_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_st_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_st_Blessing(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getblessing_st_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

                OrderInfo = string.Empty;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    OrderInfo += "<li><div>";

                    string blessingString = dtData.Rows[i]["BlessingString"].ToString();
                    string blessingType = dtData.Rows[i]["BlessingType"].ToString();

                    ////服務項目
                    OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", blessingString);

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetBlessingCost(AdminID, blessingType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_st(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_st.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_st(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
                case 23:
                    //祈安植福服務
                    reback = "https://bobibobi.tw/Temples/templeService_blessing_st.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Blessing_st(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 23, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-池上北極玄天宮
        public void GetPurchaserlist_bj_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_bj_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_bj(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_bj.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_bj(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-花蓮慈惠石壁部堂
        public void GetPurchaserlist_sbbt_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_sbbt_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_sbbt(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_sbbt.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_sbbt(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-新北真武山受玄宮
        public void GetPurchaserlist_bpy_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_bpy_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_bpy(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_bpy.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_bpy(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }


        //購買人資料列表-桃園壽山巖觀音寺
        public void GetPurchaserlist_ssy_Lights(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_ssy_Info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result_mobile = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";
                string result_email = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppEmail\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result_mobile, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                if (dtData.Columns.Contains("AppEmail"))
                {
                    var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    if (rawAppEmail != DBNull.Value)
                    {
                        var appEmailText = rawAppEmail.ToString();
                        AppEmail = appEmailText;
                        OrderPurchaser += String.Format(result_email, "購買人信箱", appEmailText);
                    }
                }

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
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    if (dtData.Columns.Contains("Remark"))
                    {
                        var rawRemark = dtData.Rows[i]["Remark"];
                        if (rawRemark != DBNull.Value)
                        {
                            var remarkText = rawRemark.ToString();
                            OrderInfo += OrderData("備註", TextToHtml(remarkText));
                        }
                    }

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    payStatus = Total > 3000 ? true : false;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ssy(int AdminID, int ApplicantID, int kind, string Year)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_ssy.aspx";

                    CheckedURL(reback);

                    if (objLightDAC.Checkedappcharge_Lights_ssy(ApplicantID, AdminID, Year))
                    {
                        if (OrderPurchaser == "")
                        {
                            Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                        }
                        else
                        {
                            DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                            if (dtLASTTIME.AddMinutes(20) < dtNow)
                            {
                                Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    }
                    break;
            }

        }



        protected string OrderData(string label, string text)
        {
            string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div class=\"txt\">{1}</div>\r\n                                            </div>";

            result = String.Format(result, label, text);

            return result;
        }

        protected string Ordertextarea(string text)
        {
            string result = "<p style='width: 100%; margin: 0 3px;'>{0}</p>";

            result = String.Format(result, text);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void HidePayButton()
        {
            bindPayButton(false, false, false, false, false, false, false, false, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fetCSP"></param>
        /// <param name="card"></param>
        /// <param name="linepay"></param>
        /// <param name="jkospay"></param>
        /// <param name="chtCSP"></param>
        /// <param name="twmCSP"></param>
        /// <param name="union"></param>
        /// <param name="pxpaypluspay"></param>
        /// <param name="applepay"></param>
        protected void bindPayButton(bool fetCSP, bool card, bool linepay, bool jkospay, bool chtCSP, bool twmCSP, bool union, bool pxpaypluspay, bool applepay)
        {
            union = false;

            if (Request["twm"] != null || Request["purl"] == "twm")
            {
                fetCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = applepay = union = false;
                twmCSP = true;
            }
            else if (Request["bobi"] != null || Request["purl"] == "bobi")
            {
                fetCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = applepay = true;
                twmCSP = false;
            }
            else if (Request["jkos"] != null || Request["purl"] == "jkos")
            {
                fetCSP = twmCSP = card = linepay = chtCSP = pxpaypluspay = applepay = union = false;
                jkospay = true;
            }
            else if (Request["pxpayplues"] != null || Request["purl"] == "pxpayplues")
            {
                fetCSP = twmCSP = card = linepay = chtCSP = jkospay = applepay = union = false;
                pxpaypluspay = true;
            }
            else if (Request["fet"] != null || Request["purl"] == "fet")
            {
                twmCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = applepay = union = false;
                fetCSP = true;
            }
            else if (Request["cht"] != null || Request["purl"] == "cht")
            {
                fetCSP = twmCSP = card = linepay = jkospay = pxpaypluspay = applepay = union = false;
                chtCSP = true;
            }

            this.fetPay.Visible = fetCSP;

            this.cardPay.Visible = card;

            this.LinePay.Visible = linepay;

            this.JkosPay.Visible = jkospay;

            this.chtPay.Visible = chtCSP;

            this.twmPay.Visible = twmCSP;

            //this.unionPay.Visible = union;

            this.PXPayPlusPay.Visible = pxpaypluspay;

            this.ApplePay.Visible = applepay;
        }

        /// <summary>
        /// 檢查訪問參數並組合成 URL
        /// 支援固定參數與前綴比對 (fb 系列、in 系列)，並限制宮廟縮寫清單
        /// 僅允許一個參數生效，依優先權判斷
        /// </summary>
        /// <param name="url">原始 URL</param>
        /// <returns>加入檢查參數後的完整 URL</returns>
        protected string CheckedURL(string url)
        {
            // 定義固定參數的優先權 (數字越小優先權越高)
            Dictionary<string, int> paramPriority = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "line",   2 },
                { "jkos",   2 },
                { "pxpayplues", 2 },
                { "fb",     2 },
                { "fbad",     2 },
                { "fbda",     2 },
                { "inda",     2 },
                { "fetsms", 2 },
                { "cht",    2 },
                { "twm",    2 },

                // 特別指定 purl 的優先權更高 (1)
                { "purl",   1 }
            };

            // 定義需要做前綴比對的來源 (例：fb 系列、in 系列)
            Dictionary<string, int> prefixPriority = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "fb",  2 },   // fb 官方粉絲團/廣告/宮廟專屬
                { "in",  2 }    // 宮內看板
            };

            // 定義合法的宮廟縮寫清單
            string[] validTempleCodes = { "da", "h", "wu", "Fu", "Luer", "ty", "Fw", "dh", "Lk", "ma", "wjsan", "st" };

            string selectedParam = null;
            int selectedPriority = int.MaxValue;

            // 1️⃣ 優先檢查 purl=xxx
            if (Request["purl"] != null)
            {
                string purlValue = Request["purl"];

                // 完全比對 Dictionary
                if (paramPriority.ContainsKey(purlValue) && paramPriority["purl"] < selectedPriority)
                {
                    selectedParam = purlValue;
                    selectedPriority = paramPriority["purl"];
                }
                else
                {
                    // 前綴比對 (fb/in 系列)
                    foreach (var prefix in prefixPriority)
                    {
                        if (purlValue.StartsWith(prefix.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            // 如果只有 prefix（例如 "fb"、"in"）直接接受
                            if (purlValue.Equals(prefix.Key, StringComparison.OrdinalIgnoreCase))
                            {
                                selectedParam = purlValue;
                                selectedPriority = prefix.Value;
                                break;
                            }

                            // 如果是 prefix + 廟別縮寫，驗證縮寫是否合法
                            string suffix = purlValue.Substring(prefix.Key.Length).ToLower();
                            if (validTempleCodes.Contains(suffix) && prefix.Value < selectedPriority)
                            {
                                selectedParam = purlValue; // 保留完整，例如 fbh, fbwu, inty
                                selectedPriority = prefix.Value;
                                break;
                            }
                        }
                    }
                }
            }

            // 2️⃣ 如果沒有帶 purl=xxx，再檢查舊參數
            if (selectedParam == null)
            {
                foreach (var kv in paramPriority.Where(k => k.Key != "purl"))
                {
                    if (Request[kv.Key] != null && kv.Value < selectedPriority)
                    {
                        selectedParam = kv.Key;
                        selectedPriority = kv.Value;
                        break;
                    }
                }

                // 舊參數的前綴比對 (例如 fb=1, inda=1)
                foreach (var prefix in prefixPriority)
                {
                    foreach (var code in validTempleCodes)
                    {
                        string combined = prefix.Key + code;
                        if (Request[combined] != null && prefix.Value < selectedPriority)
                        {
                            selectedParam = combined;
                            selectedPriority = prefix.Value;
                            break;
                        }
                    }

                    if (selectedParam != null) break;
                }
            }

            // 3️⃣ 如果有找到符合的參數，就加到 URL
            if (!string.IsNullOrEmpty(selectedParam))
            {
                url += url.IndexOf("?") >= 0 ? "&purl=" + selectedParam : "?purl=" + selectedParam;
            }

            return url;
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