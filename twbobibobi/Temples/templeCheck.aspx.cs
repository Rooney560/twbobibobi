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
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.Temples
{
    public partial class templeCheck : AjaxBasePage
    {
        private static object _thisLock = new object();
        public string AppMobile = string.Empty;
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null && Request["aid"] != null)
                {
                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                    ogurl = Request.RawUrl.ToString() + "?a=" + Request["a"] + "&aid=" + Request["aid"] + "&kind=" + Request["kind"]; ;

                    int adminID = int.Parse(Request["a"]);
                    int ApplicantID = int.Parse(Request["aid"]);
                    int kind = int.Parse(Request["kind"]);
                    bool ExpirationDate = true;

                    switch (kind)
                    {
                        //點燈服務
                        case 1:
                            typeString = " 2024點燈";
                            string startDate = "2024/11/01 00:00:00";
                            int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || Request["ad"] == "2")
                            {
                                typeString = " 2025點燈";
                                Year = "2025";
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
                                    GetPurchaserlist_da_Lights(adminID, ApplicantID, Year);          //大甲鎮瀾宮資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    if (Request["ad"] == "2299")
                                    {
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetPurchaserlist_h_Lights(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_h(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/10/31 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Lights(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/01/19 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetPurchaserlist_Fu_Lights(adminID, ApplicantID, Year);          //西螺福興宮資料列表
                                    Checkedtemple_Fu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/10/31 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetPurchaserlist_Luer_Lights(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_Luer(adminID, ApplicantID, kind, Year, type);
                                    EndDate = "2025/10/31 23:59";
                                    if (type == 2)
                                    {
                                        //月老姻緣燈
                                    }
                                    else if (type == 3)
                                    {
                                        //月老姻緣燈-台哥大專屬
                                        ExpirationDate = false;
                                        bindPayButton(false, false, false, false, false, true, false, false);
                                    }
                                    else
                                    {
                                        //一般點燈
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Lights(adminID, ApplicantID, type, Year);          //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, type, Year);
                                    EndDate = "2025/10/17 23:59";
                                    if (type == 2)
                                    {
                                        EndDate = "2025/05/08 23:59";
                                    }
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_Lights(adminID, ApplicantID, Year);          //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/10/31 23:59";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getlights_Fw_info(ApplicantID, Year);
                                    bool checkedPayStatus = true;

                                    foreach (DataRow dr in dtData.Rows)
                                    {
                                        int lightstype = 0;
                                        if (int.TryParse(dr["LightsType"].ToString(), out lightstype))
                                        {
                                            if (lightstype > 37)
                                            {
                                                checkedPayStatus = false;
                                                break;
                                            }
                                        }
                                    }

                                    if (checkedPayStatus)
                                    {
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    else
                                    {
                                        bindPayButton(false, true, true, true, false, false, true, true);
                                    }
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Lights(adminID, ApplicantID, Year);          //台東東海龍門天聖宮資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/08/15 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 17:
                                    //五股賀聖宮
                                    title = "五股賀聖宮";
                                    GetPurchaserlist_Hs_Lights(adminID, ApplicantID, Year);          //五股賀聖宮資料列表
                                    Checkedtemple_Hs(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    bindPayButton(false, true, true, false, false, false, true, true);
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Lights(adminID, ApplicantID, Year);          //鹿港城隍廟資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/10/31 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Lights(adminID, ApplicantID, Year);          //玉敕大樹朝天宮資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 29:
                                    //進寶財神廟
                                    title = "進寶財神廟";
                                    //GetPurchaserlist_jb_Lights(adminID, ApplicantID, Year);          //進寶財神廟資料列表
                                    //Checkedtemple_jb(adminID, ApplicantID, kind, Year);
                                    //EndDate = "2024/10/31 23:59";
                                    //bindPayButton(false, false, false, false, false, false, false);
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    bool payStatus = false;
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_Lights(adminID, ApplicantID, Year, ref payStatus);          //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    if (payStatus)
                                    {
                                        bindPayButton(false, true, true, true, false, false, true, true);
                                    }
                                    else
                                    {
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    break;
                            }
                            break;
                        //普度服務
                        case 2:
                            typeString = " 2024普度";

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_da_Purdue(adminID, ApplicantID, Year);             //大甲鎮瀾宮資料列表
                                    Checkedtemple_da(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/09 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetPurchaserlist_h_Purdue(adminID, ApplicantID, Year);              //新港奉天宮資料列表
                                    Checkedtemple_h(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/07/31 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Purdue(adminID, ApplicantID, Year);             //北港武德宮資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/11 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetPurchaserlist_Fu_Purdue(adminID, ApplicantID, Year);                    //西螺福興宮人資料列表
                                    Checkedtemple_Fu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/20 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 9:
                                    //桃園大廟景福宮
                                    title = "桃園大廟景福宮";
                                    //GetPurchaserlist_Jing(adminID, ApplicantID, Year);                  //桃園大廟景福宮資料列表
                                    //Checkedtemple_Jing(adminID, ApplicantID, Year);
                                    EndDate = "2023/08/25 23:59";
                                    bindPayButton(false, false, false, false, false, false, false, false);
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetPurchaserlist_Luer_Purdue(adminID, ApplicantID, Year);           //台南正統鹿耳門聖母廟資料列表
                                    Checkedtemple_Luer(adminID, ApplicantID, kind, Year, 0);
                                    EndDate = "2024/08/15 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Purdue(adminID, ApplicantID, Year);             //桃園威天宮資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2024/08/21 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_Purdue(adminID, ApplicantID, Year);             //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/29 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Purdue(adminID, ApplicantID, Year);             //台東東海龍門天聖宮資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/28 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Purdue(adminID, ApplicantID, Year);             //鹿港城隍廟資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/05 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Purdue(adminID, ApplicantID, Year);             //玉敕大樹朝天宮資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/04 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                                case 30:
                                    //鎮瀾買足
                                    title = "大甲鎮瀾宮";
                                    GetPurchaserlist_mazu_Purdue(adminID, ApplicantID, Year);           //鎮瀾買足資料列表
                                    Checkedtemple_mazu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/15 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                            }
                            break;
                        //商品販賣服務
                        case 3:
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
                        //下元補庫
                        case 4:
                            typeString = " 2024下元補庫";
                            Year = "2024";

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/11/10 23:59";
                                    bindPayButton(true, true, true, true, true, true, false, false);
                                    break;
                            }
                            break;
                        //呈疏補庫
                        case 5:
                            typeString = " 2025天官武財神聖誕補財庫";

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies2(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/03/24 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                            }
                            break;
                        //企業補財庫
                        case 6:
                            typeString = " " + dtNow.Year.ToString() + "企業補財庫";
                            ExpirationDate = false;
                            Year = dtNow.Year.ToString();

                            switch (adminID)
                            {
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetPurchaserlist_wu_Supplies3(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_wu(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/08/22 23:59";
                                    bindPayButton(true, false, false, false, false, false, false, false);
                                    break;
                            }
                            break;
                        //天赦日招財補運
                        case 7:
                            typeString = " 2025天赦日招財補運";

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/05/22 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/05/22 23:59";
                                    bindPayButton(true, true, true, true, true, true, true, true);
                                    break;
                            }
                            break;
                        //天赦日祭改
                        case 8:
                            typeString = " 2024天赦日祭改";

                            switch (adminID)
                            {
                                case 29:
                                    //進寶財神廟
                                    title = "進寶財神廟";
                                    //GetPurchaserlist_ty_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    //Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    //EndDate = "2024/05/28 23:59";
                                    bindPayButton(false, false, false, false, false, false, false, false);
                                    break;
                            }
                            break;
                        //關聖帝君聖誕
                        case 9:
                            typeString = " 2024關聖帝君聖誕";

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_EmperorGuansheng(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2024/07/26 23:59";
                                    bindPayButton(true, false, false, false, false, false, false, false);
                                    break;
                            }
                            break;
                        //代燒金紙
                        case 10:
                            break;
                        //天貺納福添運法會
                        case 11:
                            typeString = " 2024天貺納福添運法會";

                            switch (adminID)
                            {
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/07/11 23:59";
                                    bindPayButton(true, true, true, true, true, false, false, false);
                                    break;
                            }
                            break;
                        //靈寶禮斗
                        case 12:
                            typeString = " 2024靈寶禮斗";

                            switch (adminID)
                            {
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetPurchaserlist_ma_Lingbaolidou(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ma(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/10/20 23:59";
                                    bindPayButton(true, true, true, true, true, true, false, false);
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
                                    if (Request["fetsms"] != null)
                                    {
                                        bindPayButton(true, false, false, false, false, false, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(true, true, true, true, true, true, false, false);
                                    }
                                    break;
                            }
                            break;
                        //九九重陽天赦日補運
                        case 14:
                            typeString = " 2024九九重陽天赦日雙重加持招財補運";

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies2(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2024/10/09 16:00";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getsupplies2_ty_info(ApplicantID, Year);
                                    bool checkedPayStatus = true;

                                    foreach (DataRow dr in dtData.Rows)
                                    {
                                        if (dr["SuppliesType"].ToString() == "8")
                                        {
                                            checkedPayStatus = false;
                                            break;
                                        }
                                    }

                                    if (checkedPayStatus)
                                    {
                                        bindPayButton(true, true, true, true, true, true, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(false, true, true, true, false, false, false, false);
                                    }
                                    break;
                            }
                            break;
                        //護國息災梁皇大法會
                        case 15:
                            typeString = " 護國息災梁皇大法會";
                            Year = "2024";

                            switch (adminID)
                            {
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetPurchaserlist_dh_Lybc(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_dh(adminID, ApplicantID, kind, Year);
                                    EndDate = "2024/12/18 23:59";
                                    if (Request["fetsms"] != null)
                                    {
                                        bindPayButton(true, false, false, false, false, false, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(true, true, true, true, true, true, false, false);
                                    }
                                    break;
                            }
                            break;
                        //補財庫
                        case 16:
                            typeString = " 補財庫";
                            Year = "2025";

                            switch (adminID)
                            {
                                case 15:
                                    //斗六五路財神宮
                                    ExpirationDate = false;
                                    GetPurchaserlist_Fw_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2026/01/23 23:59";
                                    if (Request["fetsms"] != null)
                                    {
                                        bindPayButton(true, false, false, false, false, false, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetPurchaserlist_Lk_Supplies(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_Lk(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/04/09 23:59";
                                    if (Request["fetsms"] != null)
                                    {
                                        bindPayButton(true, false, false, false, false, false, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(true, true, true, true, true, true, true, true);
                                    }
                                    break;
                            }
                            break;
                        //赦罪補庫
                        case 17:
                            typeString = " 赦罪補庫";
                            Year = "2025";

                            switch (adminID)
                            {
                                case 33:
                                    //神霄玉府財神會館
                                    title = "神霄玉府財神會館";
                                    GetPurchaserlist_sx_Supplies(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    Checkedtemple_sx(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/02/03 23:59";
                                    bindPayButton(false, true, true, false, false, false, false, false);
                                    break;
                            }
                            break;
                        //天公生招財補運
                        case 18:
                            typeString = " 2025天公生招財補運";
                            Year = "2025";

                            switch (adminID)
                            {
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetPurchaserlist_ty_Supplies3(adminID, ApplicantID, Year);          //購買人資料列表
                                    Checkedtemple_ty(adminID, ApplicantID, kind, 1, Year);
                                    EndDate = "2025/02/03 23:59";

                                    LightDAC objLightDAC = new LightDAC(this);
                                    DataTable dtData = objLightDAC.Getsupplies3_ty_info(ApplicantID, Year);
                                    bool checkedPayStatus = true;

                                    foreach (DataRow dr in dtData.Rows)
                                    {
                                        if (dr["SuppliesType"].ToString() == "8")
                                        {
                                            checkedPayStatus = false;
                                            break;
                                        }
                                    }

                                    if (checkedPayStatus)
                                    {
                                        bindPayButton(true, true, true, true, true, true, false, false);
                                    }
                                    else
                                    {
                                        bindPayButton(false, true, true, true, false, false, false, false);
                                    }
                                    break;
                            }
                            break;
                        //供香轉運
                        case 19:
                            typeString = " 供香轉運";
                            Year = "2025";

                            switch (adminID)
                            {
                                case 33:
                                    //神霄玉府財神會館
                                    ExpirationDate = false;
                                    title = "神霄玉府財神會館";
                                    GetPurchaserlist_sx_Supplies(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    Checkedtemple_sx(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/12/31 23:59";
                                    bindPayButton(true, true, true, false, true, true, true, true);
                                    break;
                            }
                            break;
                        //安斗服務
                        case 20:
                            typeString = " 2025安斗";
                            //startDate = "2024/11/01 00:00:00";
                            //ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            //if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || Request["ad"] == "2")
                            //{
                            //    typeString = " 2025點燈";
                            //    Year = "2025";
                            //}

                            //type = 1;
                            //if (Request["type"] != null)
                            //{
                            //    type = int.Parse(Request["type"]);
                            //}

                            switch (adminID)
                            {
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetPurchaserlist_Fw_AnDou(adminID, ApplicantID, Year);          //斗六五路財神宮資料列表
                                    Checkedtemple_Fw(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/10/31 23:59";
                                    bindPayButton(false, true, true, true, false, false, true, true);
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    bool payStatus = false;
                                    title = "台灣道教總廟無極三清總道院";
                                    GetPurchaserlist_wjsan_AnDou(adminID, ApplicantID, Year, ref payStatus);          //台灣道教總廟無極三清總道院資料列表
                                    Checkedtemple_wjsan(adminID, ApplicantID, kind, Year);
                                    EndDate = "2025/06/30 23:59";
                                    bindPayButton(false, true, true, true, false, false, true, true);
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
                    //        //台南正統鹿耳門聖母廟
                    //        title = "台南正統鹿耳門聖母廟";
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
                    //    case 23:
                    //        //玉敕大樹朝天宮
                    //        title = "玉敕大樹朝天宮";
                    //        EndDate = "2023/08/24 23:59";
                    //        break;
                    //    case 29:
                    //        //進寶財神廟
                    //        title = "進寶財神廟";
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
                    //        //下元補庫
                    //        break;
                    //    case 5:
                    //        //呈疏補庫
                    //        break;
                    //    case 6:
                    //        //企業補財庫
                    //        break;
                    //    case 7:
                    //        //天赦日招財補運
                    //        break;
                    //    case 8:
                    //        //天赦日祭改
                    //        break;
                    //    case 9:
                    //        //關聖帝君聖誕
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
            public int type = 0;

            public void gotopay(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                string ChargeType = basePage.Request["ChargeType"];
                string AppMobile = basePage.Request["AppMobile"];
                string Code = basePage.Request["Code"];
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

                if (basePage.Request["type"] != null)
                {
                    type = int.Parse(basePage.Request["type"].ToString());
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
                    //        //台南正統鹿耳門聖母廟
                    //        break;
                    //    case 14:
                    //        //桃園威天宮
                    //        break;
                    //    case 15:
                    //        //斗六五路財神宮
                    //        break;
                    //    case 16:
                    //        //台東東海龍門天聖宮
                    //        break;
                    //    case 17:
                    //        //五股賀聖宮
                    //        break;
                    //    case 18:
                    //        //外澳接天宮
                    //        break;
                    //    case 19:
                    //        //安平開台天后宮
                    //        break;
                    //    case 21:
                    //        //鹿港城隍廟
                    //        break;
                    //    case 23:
                    //        //玉敕大樹朝天宮
                    //        break;
                    //    case 29:
                    //        //進寶財神廟
                    //        break;
                    //}

                    //bool isValid = true;
                    bool isValid = false;
                    string CodeError = "0";

                    //switch (kind)
                    //{
                    //    case 1:
                    //        //點燈服務
                    //        string startDate = "2024/11/01 00:00:00";
                    //        int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                    //        if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || basePage.Request["ad"] == "2")
                    //        {
                    //            Year = "2025";
                    //        }

                    //        if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, Year, ref CodeError))
                    //        {
                    //            if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, Year))
                    //            {
                    //                isValid = true;
                    //            }
                    //            else
                    //            {
                    //                basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //                basePage.mJSonHelper.AddContent("CodeError", "-4");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //            basePage.mJSonHelper.AddContent("CodeError", CodeError);
                    //        }
                    //        break;
                    //    case 6:
                    //        Year = dtNow.Year.ToString();

                    //        if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, Year, ref CodeError))
                    //        {
                    //            if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, Year))
                    //            {
                    //                isValid = true;
                    //            }
                    //            else
                    //            {
                    //                basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //                basePage.mJSonHelper.AddContent("CodeError", "-4");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //            basePage.mJSonHelper.AddContent("CodeError", CodeError);
                    //        }
                    //        break;
                    //    case 16:
                    //        Year = "2025";

                    //        if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, Year, ref CodeError))
                    //        {
                    //            if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, Year))
                    //            {
                    //                isValid = true;
                    //            }
                    //            else
                    //            {
                    //                basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //                basePage.mJSonHelper.AddContent("CodeError", "-4");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //            basePage.mJSonHelper.AddContent("CodeError", CodeError);
                    //        }
                    //        break;
                    //    case 17:
                    //        Year = "2025";

                    //        if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, Year, ref CodeError))
                    //        {
                    //            if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, Year))
                    //            {
                    //                isValid = true;
                    //            }
                    //            else
                    //            {
                    //                basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //                basePage.mJSonHelper.AddContent("CodeError", "-4");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                    //            basePage.mJSonHelper.AddContent("CodeError", CodeError);
                    //        }
                    //        break;
                    //}
                    Year = "2025";

                    if (objLightDAC.CheckedCAPTCHACode(Code, ApplicantID, AdminID, kind, Year, ref CodeError))
                    {
                        if (objLightDAC.UpdateCAPTCHACodeStatus(AdminID, ApplicantID, kind, Year))
                        {
                            isValid = true;
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
                        basePage.mJSonHelper.AddContent("CodeError", CodeError);
                    }

                    if (isValid)
                    {
                        if (Total > 0)
                        {
                            switch (kind)
                            {
                                case 1:
                                    //點燈服務
                                    string startDate = "2024/11/01 00:00:00";
                                    int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                                    if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || basePage.Request["ad"] == "2")
                                    {
                                        Year = "2025";
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
                                                    string lightsType = lightstypelist[i];
                                                    int c = 0;

                                                    c += lightsType == "3" ? count_da_lights[0] : 0;
                                                    c += lightsType == "4" ? count_da_lights[1] : 0;
                                                    c += lightsType == "5" ? count_da_lights[2] : 0;

                                                    if (objLightDAC.checkedLightsNum(lightsType, AdminID.ToString(), c, -1, Year))
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
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_da(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                        link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_h(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                    string lightsType = lightstypelist[i];
                                                    int c = 0;

                                                    c += lightsType == "3" ? count_wu_lights[0] : 0;
                                                    c += lightsType == "4" ? count_wu_lights[1] : 0;
                                                    c += lightsType == "6" ? count_wu_lights[2] : 0;

                                                    if (objLightDAC.checkedLightsNum(lightsType, AdminID.ToString(), c, -1, Year))
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
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_wu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                        link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_Fu(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                name = "台南正統鹿耳門聖母廟點燈服務";

                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_Luer(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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

                                                    if (objLightDAC.checkedLightsNum(lightsType, AdminID.ToString(), c, -1, Year))
                                                    {
                                                        checkednum_ty = false;

                                                        basePage.mJSonHelper.AddContent("overnumType", lightstypelist[i]);
                                                        basePage.mJSonHelper.AddContent("LightsString", LightsStringlist[i]);

                                                        break;
                                                    }
                                                }

                                                int type = 1;
                                                int.TryParse(basePage.Request["type"], out type);
                                                if (checkednum_ty)
                                                {
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]) + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            //if (basePage.Request["ad"] == "2290")
                                                            //{
                                                            //    cost = 10;
                                                            //}
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]) + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                            break;
                                                        case "TwmCSP":
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
                                                            break;
                                                        case "UnionPay":
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), type, Year);
                                                            break;
                                                        default:
                                                            link = TWWebPay_lights_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + ("&type=" + basePage.Request["type"]), type, Year);
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
                                                        link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                        link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_dh(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                name = "五股賀聖宮";

                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_lights_Hs(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                            case 21:
                                                //鹿港城隍廟
                                                name = "鹿港城隍廟點燈服務";

                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_lights_Lk(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                            case 23:
                                                //玉敕大樹朝天宮
                                                name = "玉敕大樹朝天宮點燈服務";
                                                dtData = objLightDAC.Getlights_ma_info(ApplicantID, Year);

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

                                                    if (objLightDAC.checkedLightsNum(lightsType, AdminID.ToString(), c, -1, Year))
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
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                                (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_ma(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
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
                                                dtData = objLightDAC.Getlights_wjsan_info(ApplicantID, Year);

                                                bool checkednum_wjsan = true;
                                                if (checkednum_wjsan)
                                                {
                                                    switch (ChargeType)
                                                    {
                                                        case "LinePay":
                                                            link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                                (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
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
                                                        default:
                                                            link = TWWebPay_lights_wjsan(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" +
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
                                                //link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                string name = "大甲鎮瀾宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 4:
                                                //新港奉天宮
                                                //link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "新港奉天宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 6:
                                                //北港武德宮
                                                //link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "北港武德宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 8:
                                                //西螺福興宮
                                                //link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "西螺福興宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 9:
                                                //桃園大廟景福宮
                                                //link = TWWebPay_purdue_Jing(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case 10:
                                                //台南正統鹿耳門聖母廟
                                                //link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "台南正統鹿耳門聖母廟普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 14:
                                                //桃園威天宮
                                                //link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "桃園威天宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 15:
                                                //斗六五路財神宮
                                                //link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "斗六五路財神宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 16:
                                                //台東東海龍門天聖宮
                                                //link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "台東東海龍門天聖宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 21:
                                                //鹿港城隍廟
                                                //link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "鹿港城隍廟普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 23:
                                                //玉敕大樹朝天宮
                                                //link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "玉敕大樹朝天宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                }
                                                break;
                                            case 30:
                                                //鎮瀾買足
                                                //link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                                                name = "大甲鎮瀾宮普度服務";
                                                switch (ChargeType)
                                                {
                                                    case "LinePay":
                                                        link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    default:
                                                        link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮下元補庫&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //if (basePage.Request["ad"] != null)
                                                //{
                                                //    cost = 10;
                                                //}
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮下元補庫&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮呈疏補庫&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //if (basePage.Request["ad"] != null && basePage.Request["ad"] == "2290")
                                                //{
                                                //    cost = 10;
                                                //}
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮天官武財神聖誕補財庫&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                    Year = dtNow.Year.ToString();

                                    dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 6, type, Year);
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
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        //if (basePage.Request["ad"] == "2299")
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                        //link = "https://localhost:56437/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        //if (basePage.Request["ad"] == "2299")
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=玉敕大樹朝天宮天赦日招財補運&orderId=" + orderId;
                                                        //link = "https://localhost:56437/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=玉敕大樹朝天宮天赦日招財補運&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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

                                    dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 8, type, Year);
                                    if (dtLASTTIME.AddMinutes(20) < dtNow)
                                    {
                                        basePage.mJSonHelper.AddContent("Timeover", 1);
                                    }
                                    else
                                    {
                                        //int cost = Total;
                                        //string link = string.Empty;

                                        //switch (ChargeType)
                                        //{
                                        //    case "LinePay":
                                        //        link = TWWebPay_supplies_jb(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                        //        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                        //        break;
                                        //    case "JkosPay":
                                        //        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                        //        break;
                                        //    case "PXPayPlus":
                                        //        link = TWWebPay_supplies_jb(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                        //            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                        //        break;
                                        //    case "ChtCSP":
                                        //        link = TWWebPay_supplies_jb(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                        //        break;
                                        //    case "TwmCSP":
                                        //        link = TWWebPay_supplies_jb(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                        //        break;
                                        //    default:
                                        //        link = TWWebPay_supplies_jb(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                        //        break;
                                        //}

                                        //if (link != "")
                                        //{
                                        //    basePage.SavePayLog(link);

                                        //    basePage.mJSonHelper.AddContent("StatusCode", 1);
                                        //    basePage.mJSonHelper.AddContent("redirect", link);

                                        //    basePage.Session["ApplicantID"] = ApplicantID;
                                        //}
                                    }
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮關聖帝君聖誕&orderId=" + orderId;
                                                //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + 10 + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮關聖帝君聖誕&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_emperorGuansheng_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=台東東海龍門天聖宮-天貺納福添運法會&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //cost = basePage.Request["ad"] == "1" ? 10 : cost;
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=台東東海龍門天聖宮-天貺納福添運法會&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            default:
                                                link = TWWebPay_supplies_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=玉敕大樹朝天宮靈寶禮斗&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : "") + "&name=玉敕大樹朝天宮靈寶禮斗&orderId=" + orderId;
                                                //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + 10 + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=玉敕大樹朝天宮靈寶禮斗&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_lingbaolidou_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["line"] != null ? "&line=1" : "") + (basePage.Request["fb"] != null ? "&fb=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //if (basePage.Request["ad"] != null)
                                                //{
                                                //    cost = 10;
                                                //}
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                    "&name=大甲鎮瀾宮重修慶成祈安七朝清醮活動&orderId=" + orderId;
                                                //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + 10 + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮關聖帝君聖誕&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                    (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_taoistJiaoCeremony_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                    (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //if (basePage.Request["ad"] != null)
                                                //{
                                                //    cost = 10;
                                                //}
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                    "&name=桃園威天宮九九重陽天赦日雙重加持招財補運活動&orderId=" + orderId;
                                                //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + 10 + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮關聖帝君聖誕&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_supplies2_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮天赦日招財補運&orderId=" + orderId;
                                                break;
                                            case "JkosPay":
                                                //if (basePage.Request["ad"] != null)
                                                //{
                                                //    cost = 10;
                                                //}
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                    "&name=台東東海龍門天聖宮護國息災梁皇大法會&orderId=" + orderId;
                                                //link = "https://localhost:44399/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + 10 + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") + "&name=桃園威天宮關聖帝君聖誕&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                    (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_lybc_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                    ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                    (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                        //if (basePage.Request["ad"] != null && basePage.Request["ad"] == "2290")
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                            (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                            "&name=斗六五路財神宮補財庫&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                        //if (basePage.Request["ad"] != null)
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                            (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                            "&name=鹿港城隍廟補財庫&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                        //if (basePage.Request["ad"] != null)
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                            (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                            "&name=神霄玉府財神會館赦罪補庫&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies_sx(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                    "&name=桃園威天宮天公生招財補運活動&orderId=" + orderId;
                                                break;
                                            case "PXPayPlus":
                                                link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                break;
                                            case "ChtCSP":
                                                link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                break;
                                            case "TwmCSP":
                                                link = TWWebPay_supplies3_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                    "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                        //if (basePage.Request["ad"] != null)
                                                        //{
                                                        //    cost = 10;
                                                        //}
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                            (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["bobi"] != null ? "&bobi=1" : "") +
                                                            "&name=神霄玉府財神會館供香轉運&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_supplies2_sx(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" +
                                                            ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") +
                                                            (basePage.Request["bobi"] != null ? "&bobi=1" : ""), Year);
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
                                                        //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "JkosPay":
                                                        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                        break;
                                                    case "PXPayPlus":
                                                        link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                            "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "ChtCSP":
                                                        link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "TwmCSP":
                                                        link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                        break;
                                                    case "UnionPay":
                                                        link = TWWebPay_andou_Fw(basePage, orderId, ApplicantID, "CreditCard", "UNIONPAY", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
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
                                                            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "JkosPay":
                                                            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind +
                                                                (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                                                            break;
                                                        case "PXPayPlus":
                                                            link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "PXPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
                                                                "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                                                            break;
                                                        case "ChtCSP":
                                                            link = TWWebPay_andou_wjsan(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID +
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
                            }
                        }
                    }
                }
            }

            public void sendsms(BasePage basePage)
            {
                lock (_thisLock)
                {
                    basePage.mJSonHelper.AddContent("StatusCode", 0);

                    string AppMobile = basePage.Request["AppMobile"];

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
                        SMSHepler objSMSHepler = new SMSHepler();

                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                        string orderId = dtNow.ToString("yyyyMMddHHmmssfff");
                        string Year = "2025";
                        string Codeerror = "0";

                        string code = CreateRandomWord(6);

                        string msg = "【保必保庇】線上宮廟服務平臺，購買人電話OTP認證，【"+ code + "】簡訊密碼180秒有效，驗證碼請勿提供他人，以防詐騙";

                        Year = "2025";

                        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        {
                            if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                            {
                                if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                                {
                                    string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                                    basePage.SaveCAPTCHACodeLog(log);

                                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                                    basePage.Session["ApplicantID"] = ApplicantID;
                                }
                            }
                        }
                        else
                        {
                            basePage.mJSonHelper.AddContent("StatusCode", 0);
                            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        }
                        //switch (kind)
                        //{
                        //    case 1:
                        //        //點燈服務
                        //        string startDate = "2024/11/01 00:00:00";
                        //        int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                        //        if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || basePage.Request["ad"] == "2")
                        //        {
                        //            Year = "2025";
                        //        }

                        //        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //        {
                        //            if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //            {
                        //                if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                {
                        //                    string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                    basePage.SaveCAPTCHACodeLog(log);

                        //                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                    basePage.Session["ApplicantID"] = ApplicantID;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //        }
                        //        break;
                        //    case 2:
                        //        //普度服務

                        //        //dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, type, Year);
                        //        //if (dtLASTTIME.AddMinutes(20) < dtNow)
                        //        //{
                        //        //    basePage.mJSonHelper.AddContent("Timeover", 1);
                        //        //}
                        //        //else
                        //        //{
                        //        //    int cost = Total;
                        //        //    string link = string.Empty;
                        //        //    switch (AdminID)
                        //        //    {
                        //        //        case 3:
                        //        //            //大甲鎮瀾宮
                        //        //            //link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            string name = "大甲鎮瀾宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_da(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 4:
                        //        //            //新港奉天宮
                        //        //            //link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "新港奉天宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_h(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 6:
                        //        //            //北港武德宮
                        //        //            //link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "北港武德宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 8:
                        //        //            //西螺福興宮
                        //        //            //link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "西螺福興宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_Fu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 9:
                        //        //            //桃園大廟景福宮
                        //        //            //link = TWWebPay_purdue_Jing(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //        case 10:
                        //        //            //台南正統鹿耳門聖母廟
                        //        //            //link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "台南正統鹿耳門聖母廟普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_Luer(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 14:
                        //        //            //桃園威天宮
                        //        //            //link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "桃園威天宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_ty(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 15:
                        //        //            //斗六五路財神宮
                        //        //            //link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "斗六五路財神宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_Fw(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 16:
                        //        //            //台東東海龍門天聖宮
                        //        //            //link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "台東東海龍門天聖宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_dh(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 21:
                        //        //            //鹿港城隍廟
                        //        //            //link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "鹿港城隍廟普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_Lk(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 23:
                        //        //            //玉敕大樹朝天宮
                        //        //            //link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "玉敕大樹朝天宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_ma(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //        case 30:
                        //        //            //鎮瀾買足
                        //        //            //link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);

                        //        //            name = "大甲鎮瀾宮普度服務";
                        //        //            switch (ChargeType)
                        //        //            {
                        //        //                case "LinePay":
                        //        //                    link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "JkosPay":
                        //        //                    link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=" + name + "&orderId=" + orderId;
                        //        //                    break;
                        //        //                case "ChtCSP":
                        //        //                    link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                case "TwmCSP":
                        //        //                    link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //                default:
                        //        //                    link = TWWebPay_purdue_mazu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //                    break;
                        //        //            }
                        //        //            break;
                        //        //    }

                        //        //    if (link != "")
                        //        //    {
                        //        //        basePage.SavePayLog(link);

                        //        //        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        //        //        basePage.mJSonHelper.AddContent("redirect", link);

                        //        //        basePage.Session["ApplicantID"] = ApplicantID;
                        //        //    }
                        //        //}
                        //        break;
                        //    case 3:
                        //        //商品販賣服務
                        //        //typeString = "商品販賣小舖";

                        //        //switch (adminID)
                        //        //{
                        //        //    case 5:
                        //        //        //商品小舖
                        //        //        title = "文創商品販賣小舖";
                        //        //        GetPurchaserlist_da(adminID, ApplicantID, Year);          //大甲鎮瀾宮資料列表
                        //        //        Checkedtemple_da(adminID, ApplicantID, kind, Year);
                        //        //        EndDate = "2024/08/21 23:59";
                        //        //        break;
                        //        //}
                        //        break;
                        //    case 4:
                        //        //下元補庫

                        //        //dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 4, type, Year);
                        //        //if (dtLASTTIME.AddMinutes(20) < dtNow)
                        //        //{
                        //        //    basePage.mJSonHelper.AddContent("Timeover", 1);
                        //        //}
                        //        //else
                        //        //{
                        //        //    int cost = Total;
                        //        //    string link = string.Empty;

                        //        //    switch (ChargeType)
                        //        //    {
                        //        //        case "LinePay":
                        //        //            link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "LINEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            //link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮下元補庫&orderId=" + orderId;
                        //        //            break;
                        //        //        case "JkosPay":
                        //        //            //if (basePage.Request["ad"] != null)
                        //        //            //{
                        //        //            //    cost = 10;
                        //        //            //}
                        //        //            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮下元補庫&orderId=" + orderId;
                        //        //            break;
                        //        //        case "ChtCSP":
                        //        //            link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //        case "TwmCSP":
                        //        //            link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //        default:
                        //        //            link = TWWebPay_supplies_wu(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //    }

                        //        //    if (link != "")
                        //        //    {
                        //        //        basePage.SavePayLog(link);

                        //        //        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        //        //        basePage.mJSonHelper.AddContent("redirect", link);

                        //        //        basePage.Session["ApplicantID"] = ApplicantID;
                        //        //    }
                        //        //}
                        //        break;
                        //    case 5:
                        //        //呈疏補庫

                        //        //dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 5, type, Year);
                        //        //if (dtLASTTIME.AddMinutes(20) < dtNow)
                        //        //{
                        //        //    basePage.mJSonHelper.AddContent("Timeover", 1);
                        //        //}
                        //        //else
                        //        //{
                        //        //    int cost = Total;
                        //        //    string link = string.Empty;

                        //        //    switch (ChargeType)
                        //        //    {
                        //        //        case "LinePay":
                        //        //            link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮呈疏補庫&orderId=" + orderId;
                        //        //            break;
                        //        //        case "JkosPay":
                        //        //            link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=" + AdminID + "&aid=" + ApplicantID + "&Total=" + cost + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name=北港武德宮呈疏補庫&orderId=" + orderId;
                        //        //            break;
                        //        //        case "ChtCSP":
                        //        //            link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //        case "TwmCSP":
                        //        //            link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, "TELEPAY", "twm", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //        default:
                        //        //            link = TWWebPay_supplies_wu2(basePage, orderId, ApplicantID, ChargeType, "", cost, AppMobile, "a=" + AdminID + "&aid=" + ApplicantID + "&kind=" + kind + (basePage.Request["twm"] != null ? "&twm=1" : ""), Year);
                        //        //            break;
                        //        //    }

                        //        //    if (link != "")
                        //        //    {
                        //        //        basePage.SavePayLog(link);

                        //        //        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        //        //        basePage.mJSonHelper.AddContent("redirect", link);

                        //        //        basePage.Session["ApplicantID"] = ApplicantID;
                        //        //    }
                        //        //}
                        //        break;
                        //    case 6:
                        //        //企業補財庫
                        //        Year = dtNow.Year.ToString();

                        //        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //        {
                        //            if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //            {
                        //                if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                {
                        //                    string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                    basePage.SaveCAPTCHACodeLog(log);

                        //                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                    basePage.Session["ApplicantID"] = ApplicantID;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //        }
                        //        break;
                        //    case 7:
                        //        //桃園威天宮天赦日招財補運
                        //        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //        {
                        //            if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //            {
                        //                if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                {
                        //                    string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                    basePage.SaveCAPTCHACodeLog(log);

                        //                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                    basePage.Session["ApplicantID"] = ApplicantID;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //        }
                        //        break;
                        //    case 8:
                        //        //進寶財神廟天赦日祭改
                        //        break;
                        //    case 9:
                        //        //桃園威天宮關聖帝君聖誕
                        //        break;
                        //    case 10:
                        //        break;
                        //    case 11:
                        //        //台東東海龍門天聖宮天貺納福添運法會
                        //        break;
                        //    case 12:
                        //        //玉敕大樹朝天宮靈寶禮斗
                        //        break;
                        //    case 13:
                        //        //大甲鎮瀾宮七朝清醮
                        //        break;
                        //    case 14:
                        //        //桃園威天宮九九重陽天赦日招財補運
                        //        break;
                        //    case 15:
                        //        //台東東海龍門天聖宮護國息災梁皇大法會
                        //        break;
                        //    case 16:
                        //        //補財庫-鹿港城隍廟
                        //        Year = "2025";

                        //        if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //        {
                        //            if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //            {
                        //                if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                {
                        //                    string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                    basePage.SaveCAPTCHACodeLog(log);

                        //                    basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                    basePage.Session["ApplicantID"] = ApplicantID;
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //            basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //        }
                        //        break;
                        //    case 17:
                        //        //赦罪補庫-神霄玉府財神會館
                        //        switch (AdminID)
                        //        {
                        //            case 33:
                        //                Year = "2025";

                        //                if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //                {
                        //                    if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //                    {
                        //                        if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                        {
                        //                            string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                            basePage.SaveCAPTCHACodeLog(log);

                        //                            basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                            basePage.Session["ApplicantID"] = ApplicantID;
                        //                        }
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //                    basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //                }
                        //                break;
                        //        }
                        //        break;
                        //    case 18:
                        //        //桃園威天宮天公生招財補運
                        //        switch (AdminID)
                        //        {
                        //            case 14:
                        //                Year = "2025";

                        //                if (objLightDAC.CheckedCAPTCHACodeCount(ApplicantID, AdminID, kind, AppMobile, Year, ref Codeerror))
                        //                {
                        //                    if (objLightDAC.addCAPTCHACode(ApplicantID, AdminID, kind, code, AppMobile, Year))
                        //                    {
                        //                        if (objSMSHepler.SendMsg_SL(AppMobile, msg))
                        //                        {
                        //                            string log = String.Format("aid={0}&a={1}&kind={2}&Year={3}&code={4}", ApplicantID, AdminID, kind, Year, code);
                        //                            basePage.SaveCAPTCHACodeLog(log);

                        //                            basePage.mJSonHelper.AddContent("StatusCode", 1);

                        //                            basePage.Session["ApplicantID"] = ApplicantID;
                        //                        }
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    basePage.mJSonHelper.AddContent("StatusCode", 0);
                        //                    basePage.mJSonHelper.AddContent("CodeError", Codeerror);
                        //                }
                        //                break;
                        //        }
                        //        break;
                        //}
                    }
                    else
                    {

                    }
                }
            }

            /// <summary>
            /// 產生隨機字串
            /// </summary>
            /// <param name="length"></param>
            /// <returns></returns>
            private string CreateRandomWord(int length = 6)
            {
                string code = "";
                //var letters = "ABCDEFGHJKMPQRSTUVWXYZ23456789abcdefghjkmpqrstuvwxyz".ToArray();
                var letters = "1234567890".ToArray();

                Random r = new Random();
                for (int i = 0; i < length; i++)
                {
                    int index = r.Next(0, letters.Length);
                    code += letters[index];
                }

                return code;
            }

            public void checkedDiscountCode(BasePage basePage)
            {

            }

            protected string TWWebPay_lights_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_h(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_Fu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_Luer(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                int type, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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
                DatabaseHelper objdatabaseHelper = new DatabaseHelper(basePage);
                LightDAC objLightDAC = new LightDAC(basePag);
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

                //int.TryParse(basePag.Request["type"].ToString(), out type);
                switch (type)
                {
                    case 1:
                        //一般點燈
                        long id = objdatabaseHelper.AddChargeLog_Lights_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                        if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ty(applicantID, AdminID, price, Year))
                        {
                            link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                                + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                                + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                        }
                        break;
                    case 2:
                        //活動-孝親祈福燈
                        id = objdatabaseHelper.AddChargeLog_Lights_ty_mom(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                        if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ty_mom(applicantID, AdminID, price, Year))
                        {
                            link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                                + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                                + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                        }
                        break;
                }

                return link;
            }

            protected string TWWebPay_lights_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_andou_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_AnDou_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_AnDou_Fw(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_Hs(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
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

            protected string TWWebPay_lights_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);

                long id = objdatabaseHelper.AddChargeLog_Lights_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_ma(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lights_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);

                long id = objdatabaseHelper.AddChargeLog_Lights_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lights_wjsan(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_andou_wjsan(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);

                long id = objdatabaseHelper.AddChargeLog_AnDou_wjsan(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_AnDou_wjsan(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_purdue_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_da(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_da(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_da(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //}

                return link;
            }

            protected string TWWebPay_purdue_h(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_h(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_h(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_h(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_h(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //}

                return link;
            }

            protected string TWWebPay_purdue_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_wu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_wu(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_wu(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //}

                return link;
            }

            protected string TWWebPay_purdue_Fu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_Fu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Fu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_Fu(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Fu(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //}

                return link;
            }

            protected string TWWebPay_purdue_Jing(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_Jing(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Jing(applicantID, AdminID, price, Year))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //}
                //long id = objdatabaseHelper.AddChargeLog_Purdue_Jing(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

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
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_Luer(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Luer(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //long id = objdatabaseHelper.AddChargeLog_Purdue_Luer(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress);
                ////long id = 6;

                //LightDAC objLightDAC = new LightDAC(basePag);

                //if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Luer(applicantID, AdminID, price))
                //{
                //    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                //}

                return link;
            }

            protected string TWWebPay_purdue_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_purdue_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Fw(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_purdue_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_dh(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_purdue_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_Lk(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_purdue_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);

                long id = objdatabaseHelper.AddChargeLog_Purdue_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_ma(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }
                //if (objdatabaseHelper.CheckedAPPChargeHaving(applicantID, AdminID, 2, Year, ref oid))
                //{
                //    long id = objdatabaseHelper.AddChargeLog_Purdue_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //    //long id = 6;

                //    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_ma(applicantID, AdminID, price, Year))
                //    {
                //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //    }
                //}
                //else
                //{
                //    if (objLightDAC.Updatecost2applicantinfo_Purdue_ma(applicantID, AdminID, price, Year))
                //    {
                //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                //    }
                //}

                return link;
            }

            protected string TWWebPay_purdue_mazu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-DajiaCeremony";    //大甲鎮瀾宮普渡法會(CSENT64199)
                string item = "大甲鎮瀾宮普度法會";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePag.GetConfigValue("PaymentPurdue_mazu_ReceiveURL");
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Purdue_mazu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Purdue_mazu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_emperorGuansheng_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, 
                string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-TaoyuanWeitian";    //桃園威天宮_宮廟服務(PR00004719)
                string item = "桃園威天宮關聖帝君聖誕";
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_EmperorGuansheng_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_EmperorGuansheng_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_lingbaolidou_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);
                //檢查此購買人訂單是否有訂單產品紀錄
                if (!objdatabaseHelper.CheckedAPPChargeHaving(applicantID, 14, 12, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "靈寶禮斗取得AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objdatabaseHelper.AddChargeLog_Lingbaolidou_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lingbaolidou_ma(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "靈寶禮斗更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_taoistJiaoCeremony_da(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, 
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);
                if (!objdatabaseHelper.CheckedAPPChargeHaving(applicantID, 3, 13, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "七朝清醮的AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objdatabaseHelper.AddChargeLog_TaoistJiaoCeremony_da(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_TaoistJiaoCeremony_da(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "七朝清醮更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_lybc_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone,
                string returnUrl, string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }

                LightDAC objLightDAC = new LightDAC(basePag);
                if (!objdatabaseHelper.CheckedAPPChargeHaving(applicantID, 16, 15, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "護國息災梁皇大法會的AppChargeLog ID 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objdatabaseHelper.AddChargeLog_Lybc_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Lybc_dh(applicantID, AdminID, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "護國息災梁皇大法會更新購買人資料 錯誤！";
                        basePag.SaveErrorLog(link);
                    }
                }

                return link;
            }

            protected string TWWebPay_supplies_sx(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_sx(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_sx(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies2_sx(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies2_sx(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies2_sx(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_Lk(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_Lk(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_Lk(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_Fw(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_Fw(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_Fw(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_dh(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_dh(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_dh(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_ma(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_ma(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_ma(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies2_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies2_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies2_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies3_ty(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies3_ty(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies3_ty(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_wu(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_wu(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_wu2(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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

                if (ChargeType == "CreditCard")
                {
                    if (telco == "UNIONPAY")
                    {
                        ChargeType = "UNIONPAY";
                    }
                }
                long id = objdatabaseHelper.AddChargeLog_Supplies_wu2(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu2(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

            protected string TWWebPay_supplies_wu3(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl, 
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
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
                DatabaseHelper objdatabaseHelper = new DatabaseHelper(basePage);
                long id = objdatabaseHelper.AddChargeLog_Supplies_wu3(oid, applicantID, price, paytype, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
                //long id = 6;

                LightDAC objLightDAC = new LightDAC(basePag);

                if (id > 0 && objLightDAC.Updatecost2applicantinfo_Supplies_wu3(applicantID, AdminID, price, Year))
                {
                    link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                        + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                        + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                }

                return link;
            }

        }

        //購買人資料列表-大甲鎮瀾宮
        public void GetPurchaserlist_da_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_da_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
        public void GetPurchaserlist_da_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_da_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_da_TaoistJiaoCeremony(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.GettaoistJiaoCeremony_da_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", (dtData.Rows[0]["AppZipCode"].ToString() != "" ? dtData.Rows[0]["AppZipCode"].ToString() + " " : "") + dtData.Rows[0]["AppAddress"].ToString());

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
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_da.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_da(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_da.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_da(ApplicantID, AdminID, Year))
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

                    if (objLightDAC.checkedappcharge_TaoistJiaoCeremony_da(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_h_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_h_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_h_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_h_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("農曆生日", dtData.Rows[0]["AppBirth"].ToString() + (dtData.Rows[0]["AppLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                OrderPurchaser += OrderData("農曆時辰", dtData.Rows[0]["AppBirthTime"].ToString());
                OrderPurchaser += OrderData("購買人信箱", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                    OrderInfo += OrderData("農曆生日", dtData.Rows[0]["Birth"].ToString() + (dtData.Rows[0]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("農曆時辰", dtData.Rows[0]["BirthTime"].ToString());
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
                        OrderInfo += Ordertextarea(TextToHtml(dtData.Rows[0]["Remark"].ToString()));
                        //OrderInfo += Ordertextarea(dtData.Rows[0]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                    }

                    OrderInfo += "</div></div>";

                    cost = getTotal(dtData);
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
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_h.aspx?a=4" + (Request["ad"] != null ? "&ad=1" : "") + (Request["twm"] != null ? "&twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_h(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_h.aspx?a=4" + (Request["ad"] != null ? "&ad=1" : "") + (Request["twm"] != null ? "&twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_h(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_wu_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_wu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    //string sex = "善男";
                    //if(dtData.Rows[i]["Sex"].ToString() == "F")
                    //{
                    //    sex = "信女";
                    //}
                    //OrderInfo += OrderData("性別", sex);
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    //OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_wu_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_wu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }

        public void GetPurchaserlist_wu_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getsupplies_wu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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

            DataTable dtData = objLightDAC.Getsupplies_wu_info2(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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

            DataTable dtData = objLightDAC.Getsupplies_wu_info3(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    //OrderInfo += OrderData("數量", dtData.Rows[i]["Count"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_wu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_wu(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_wu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_wu(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_wu(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies2.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_wu2(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies3.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_wu3(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_Fu_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fu_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Fu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppZipCode"].ToString() + " " + dtData.Rows[0]["AppAddress"].ToString());

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


                    OrderInfo += "</div></div>";

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
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_Fu(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Fu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_Fu(ApplicantID, AdminID, Year))
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


        //購買人資料列表-桃園大廟景福宮
        public void GetPurchaserlist_Jing(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Jing_info(ApplicantID);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Jing(int AdminID, int ApplicantID, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            string reback = "https://bobibobi.tw/Temples/templeService_purdue_Jing.aspx" + (Request["twm"] != null ? "?twm=1" : "");
            if (objLightDAC.checkedappcharge_Purdue_Jing(ApplicantID, AdminID))
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
        }


        //購買人資料列表-台南正統鹿耳門聖母廟
        public void GetPurchaserlist_Luer_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Luer_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                        OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
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

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Luer_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Luer_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("普度組數", dtData.Rows[i]["Count"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Luer(int AdminID, int ApplicantID, int kind, string Year, int type)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Luer.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (type == 2)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    }
                    else if (type == 3)
                    {
                        reback = "https://bobibobi.tw/Temples/templeService_marriagelights_Luer_twm.aspx?twm=1";
                    }
                    if (objLightDAC.checkedappcharge_Lights_Luer(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Luer.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_Luer(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_ty_Lights(int AdminID, int ApplicantID, int type, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            switch (type)
            {
                case 1:
                    //一般點燈
                    DataTable dtData = objLightDAC.Getlights_ty_info(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                        string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                        OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //活動-孝親祈福燈
                    dtData = objLightDAC.Getlights_ty_mom_info(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                        string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                        OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());
                        OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            cost = GetLightsCost(AdminID, lightsType);

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }
        public void GetPurchaserlist_ty_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_ty_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = count * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_ty_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, suppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_EmperorGuansheng(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.GetemperorGuansheng_ty_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppBirth"].ToString() + (dtData.Rows[0]["AppLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", (dtData.Rows[0]["AppZipCode"].ToString() != "" ? dtData.Rows[0]["AppZipCode"].ToString() + " " : "") + dtData.Rows[0]["AppAddress"].ToString());

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
                    OrderInfo += OrderData("國歷生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetEmperorGuanshengCost(AdminID, emperorGuanshengType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ty_Supplies2(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies2_ty_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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

            DataTable dtData = objLightDAC.Getsupplies3_ty_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("國歷生日", dtData.Rows[0]["AppsBirth"].ToString());
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, SuppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ty(int AdminID, int ApplicantID, int kind, int type, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
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
                            reback = "https://bobibobi.tw/Temples/templeService_lights_ty.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                            if (objLightDAC.checkedappcharge_Lights_ty(ApplicantID, AdminID, Year))
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
                            reback = "https://bobibobi.tw/Temples/templeService_lights_ty_mom.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                            if (objLightDAC.checkedappcharge_Lights_ty_mom(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_ty.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_ty(ApplicantID, AdminID, Year))
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
                        (Request["ad"] != null ? "&ad=" + Request["ad"] : "") +
                        (Request["jkos"] != null ? "&jkos=1" : "") +
                        (Request["twm"] != null ? "&twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_ty(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_EmperorGuansheng_ty(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_Supplies2_ty(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_Supplies3_ty(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_Fw_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Fw_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                        OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Fw_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
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

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_Fw_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());


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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Fw_AnDou(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getandou_Fw_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                        OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    }

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);

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
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Fw.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_Fw(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Fw.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_Fw(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_Fw.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_Fw(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_andou_Fw.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_AnDou_Fw(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_dh_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_dh_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_dh_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_dh_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0, Count5 = 0, Count6 = 0, Count7 = 0, Count8 = 0;

                    int.TryParse(dtData.Rows[i]["Count1"].ToString(), out Count1);
                    int.TryParse(dtData.Rows[i]["Count2"].ToString(), out Count2);
                    int.TryParse(dtData.Rows[i]["Count3"].ToString(), out Count3);
                    int.TryParse(dtData.Rows[i]["Count4"].ToString(), out Count4);
                    int.TryParse(dtData.Rows[i]["Count5"].ToString(), out Count5);
                    int.TryParse(dtData.Rows[i]["Count6"].ToString(), out Count6);
                    int.TryParse(dtData.Rows[i]["Count7"].ToString(), out Count7);
                    int.TryParse(dtData.Rows[i]["Count8"].ToString(), out Count8);

                    if (Count1 > 0)
                    {
                        cost += Count1 > 0 ? Count1 * 3000 : 0;
                        OrderInfo += OrderData("『32天帝燈』數量", Count1.ToString());
                    }

                    if (Count2 > 0)
                    {
                        cost += Count2 > 0 ? Count2 * 800 : 0;
                        OrderInfo += OrderData("黑虎將軍補財庫 數量", Count2.ToString());
                    }

                    if (Count3 > 0)
                    {
                        cost += Count3 > 0 ? Count3 * 600 : 0;
                        OrderInfo += OrderData("消災解厄 科儀 數量", Count3.ToString());
                    }

                    if (Count4 > 0)
                    {
                        cost += Count4 > 0 ? Count4 * 600 : 0;
                        OrderInfo += OrderData("身體康健 科儀 數量", Count4.ToString());
                    }

                    if (Count5 > 0)
                    {
                        cost += Count5 > 0 ? Count5 * 600 : 0;
                        OrderInfo += OrderData("補運 科儀 數量", Count5.ToString());
                    }

                    if (Count6 > 0)
                    {
                        cost += Count6 > 0 ? Count6 * 600 : 0;
                        OrderInfo += OrderData("補財庫 科儀 數量", Count6.ToString());
                    }

                    if (Count7 > 0)
                    {
                        cost += Count7 > 0 ? Count7 * 600 : 0;
                        OrderInfo += OrderData("補文昌 科儀 數量", Count7.ToString());
                    }

                    if (Count8 > 0)
                    {
                        cost += Count8 > 0 ? Count8 * 600 : 0;
                        OrderInfo += OrderData("招貴人 科儀 數量", Count8.ToString());
                    }

                    OrderInfo += "</div></div>";

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_dh_Lybc(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlybc_dh_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));


                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetLybcCost(lybcType, AdminID);

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
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_dh.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_dh(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_dh.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_dh(ApplicantID, AdminID, Year))
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
                    //護國息災梁皇大法會服務
                    reback = "https://bobibobi.tw/Temples/templeService_lybc_dh.aspx";

                    reback = CheckedURL(reback);

                    if (objLightDAC.checkedappcharge_Lybc_dh(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_Hs_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Hs_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_Hs(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Hs.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_Hs(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_Lk_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_Lk_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                {
                    //Total += 100;
                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                    OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["ApprAddress"].ToString());
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
                    cost += 100;

                    //祈福人內容列表
                    OrderInfo += "<div class=\"ProductsInfo\">";

                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    //string sex = "善男";
                    //if (dtData.Rows[i]["Sex"].ToString() == "F")
                    //{
                    //    sex = "信女";
                    //}
                    //OrderInfo += OrderData("性別", sex);
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Lk_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_Lk_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_Lk_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_Lk_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                {
                    //Total += 100;
                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                    OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["ApprAddress"].ToString());
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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_Lk.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_Lk(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_Lk.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_Lk(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_Lk.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_Lk(ApplicantID, AdminID, Year))
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
        public void GetPurchaserlist_ma_Lights(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlights_ma_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_ma_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    switch (purdueType)
                    {
                        case "2":
                            OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                            break;
                        case "6":
                            OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                            break;
                    }
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Supplies(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getsupplies_ma_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人信箱", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                    OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetSuppliesCost(AdminID, suppliesType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void GetPurchaserlist_ma_Lingbaolidou(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getlingbaolidou_ma_info(ApplicantID, Year);
            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += String.Format(result, "購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                OrderPurchaser += OrderData("購買人地址", (dtData.Rows[0]["AppZipCode"].ToString() != "" ? dtData.Rows[0]["AppZipCode"].ToString() + " " : "") + dtData.Rows[0]["AppAddress"].ToString());

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

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = GetLingbaolidouCost(AdminID, lingbaolidouType);

                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_ma(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_ma.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    //if (objLightDAC.checkedappcharge_Lights_ma(ApplicantID, AdminID, Year))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    //}
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_ma.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_ma(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_ma.aspx" +
                        (Request["ad"] != null ? "&ad=" + Request["ad"] : "") +
                        (Request["jkos"] != null ? "&jkos=1" : "") +
                        (Request["twm"] != null ? "&twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_ma(ApplicantID, AdminID, Year))
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
                    if (objLightDAC.checkedappcharge_Lingbaolidou_ma(ApplicantID, AdminID, Year))
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

            DataTable dtData = objLightDAC.Getlights_wjsan_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        //public void GetPurchaserlist_wjsan_Purdue(int AdminID, int ApplicantID, string Year)
        //{
        //    LightDAC objLightDAC = new LightDAC(this);

        //    DataTable dtData = objLightDAC.Getpurdue_wjsan_info(ApplicantID, Year);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


        //        string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

        //        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
        //        OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
        //            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
        //            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
        //            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
        //            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
        //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

        //            switch (purdueType)
        //            {
        //                case "2":
        //                    OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
        //                    break;
        //                case "6":
        //                    OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
        //                    break;
        //            }
        //            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

        //            OrderInfo += "</div></div>";

        //            //服務項目金額
        //            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
        //            OrderInfo += "<div>$ " + cost + "元</div>";
        //            Total += cost;

        //            OrderInfo += "</li>";
        //        }
        //    }
        //}
        public void GetPurchaserlist_wjsan_AnDou(int AdminID, int ApplicantID, string Year, ref bool payStatus)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;

            DataTable dtData = objLightDAC.Getandou_wjsan_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    //OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_wjsan(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_wjsan.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Lights_wjsan(ApplicantID, AdminID, Year))
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
                    //reback = "https://bobibobi.tw/Temples/templeService_purdue_wjsan.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    //if (objLightDAC.checkedappcharge_Purdue_wjsan(ApplicantID, AdminID, Year))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 2, -1, Year);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    //}
                    break;
                case 20:
                    //安斗服務
                    reback = "https://bobibobi.tw/Temples/templeService_andou_wjsan.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_AnDou_wjsan(ApplicantID, AdminID, Year))
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


        //購買人資料列表-神霄玉府財神會館
        //public void GetPurchaserlist_sx_Lights(int AdminID, int ApplicantID, string Year)
        //{
        //    LightDAC objLightDAC = new LightDAC(this);
        //    int cost = 0;

        //    DataTable dtData = objLightDAC.Getlights_sx_info(ApplicantID, Year);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


        //        string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

        //        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
        //        OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
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

        //    DataTable dtData = objLightDAC.Getpurdue_sx_info(ApplicantID, Year);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


        //        string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

        //        AppMobile = dtData.Rows[0]["AppMobile"].ToString();
        //        OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
        public void GetPurchaserlist_sx_Supplies(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);
            int cost = 0;
            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 17:
                    //赦罪補庫
                    dtData = objLightDAC.Getsupplies_sx_info(ApplicantID, Year);
                    break;
                case 19:
                    //供香轉運
                    dtData = objLightDAC.Getsupplies2_sx_info(ApplicantID, Year);
                    break;
            }

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                if (kind == 19)
                {
                    OrderPurchaser += String.Format(result, "購買人信箱", dtData.Rows[0]["AppEmail"].ToString());
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
                    OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";


                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_sx(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);
            string reback = "https://bobibobi.tw";

            switch (kind)
            {
                //case 1:
                //    //點燈服務
                //    string reback = "https://bobibobi.tw/Temples/templeService_lights_sx.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                //    if (objLightDAC.checkedappcharge_Lights_sx(ApplicantID, AdminID, Year))
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
                //    reback = "https://bobibobi.tw/Temples/templeService_purdue_sx.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                //    if (objLightDAC.checkedappcharge_Purdue_sx(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies_sx.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies_sx(ApplicantID, AdminID, Year))
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
                    reback = "https://bobibobi.tw/Temples/templeService_supplies2_sx.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Supplies2_sx(ApplicantID, AdminID, Year))
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


        public void GetPurchaserlist_mazu_Purdue(int AdminID, int ApplicantID, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.Getpurdue_mazu_info(ApplicantID, Year);

            if (dtData.Rows.Count > 0)
            {
                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());


                string result = "<div class=\"OrderData\">\r\n                                                <div class=\"label\">{0}：</div>\r\n                                                <div id=\"AppMobile\" class=\"txt\">{1}</div>\r\n                                            </div>";

                AppMobile = dtData.Rows[0]["AppMobile"].ToString();
                OrderPurchaser += String.Format(result, "購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppZipCode"].ToString() + " " + dtData.Rows[0]["AppAddress"].ToString());

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
                    OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());

                    OrderInfo += "</div></div>";

                    //服務項目金額
                    int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }
        public void Checkedtemple_mazu(int AdminID, int ApplicantID, int kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            LightDAC objLightDAC = new LightDAC(this);

            switch (kind)
            {
                case 1:
                    //點燈服務
                    string reback = "https://bobibobi.tw/Temples/templeService_lights_mazu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    //if (objLightDAC.checkedappcharge_Lights_mazu(ApplicantID, AdminID, Year))
                    //{
                    //    if (OrderPurchaser == "")
                    //    {
                    //        Response.Write("<script>alert('讀取購買人資料錯誤。請重新購買。');location='" + reback + "'</script>");
                    //    }
                    //    else
                    //    {
                    //        DateTime dtLASTTIME = objLightDAC.GetInfoLastDate(ApplicantID, AdminID, 1, -1, Year);
                    //        if (dtLASTTIME.AddMinutes(20) < dtNow)
                    //        {
                    //            Response.Write("<script>alert('此購買人付款時間已超時20分鐘，請重新購買。');location='" + reback + "'</script>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('此購買人已進行付款動作。請重新購買。');location='" + reback + "'</script>");
                    //}
                    break;
                case 2:
                    //普度服務
                    reback = "https://bobibobi.tw/Temples/templeService_purdue_mazu.aspx" + (Request["twm"] != null ? "?twm=1" : "");
                    if (objLightDAC.checkedappcharge_Purdue_mazu(ApplicantID, AdminID, Year))
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

        protected void bindPayButton(bool fetCSP, bool card, bool linepay, bool jkospay, bool chtCSP, bool twmCSP, bool union, bool pxpaypluspay)
        {
            if (Request["twm"] != null)
            {
                fetCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = union = false;
                twmCSP = true;
            }
            else if (Request["bobi"] != null)
            {
                fetCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = true;
                twmCSP = false;
            }
            else if (Request["jkos"] != null)
            {
                fetCSP = twmCSP = card = linepay = chtCSP = pxpaypluspay = union = false;
                jkospay = true;
            }
            else if (Request["pxpayplues"] != null)
            {
                fetCSP = twmCSP = card = linepay = chtCSP = jkospay = union = false;
                pxpaypluspay = true;
            }
            else if (Request["fet"] != null)
            {
                twmCSP = card = linepay = jkospay = chtCSP = pxpaypluspay = union = false;
                fetCSP = true;
            }
            //else if (Request["fet"] != null)
            //{
            //    twmCSP = card = linepay = jkospay = chtCSP = union = false;
            //    fetCSP = true;
            //}

            this.fetPay.Visible = fetCSP;

            this.cardPay.Visible = card;

            this.LinePay.Visible = linepay;

            this.JkosPay.Visible = jkospay;

            this.chtPay.Visible = chtCSP;

            this.twmPay.Visible = twmCSP;

            this.unionPay.Visible = union;

            this.PXPayPlusPay.Visible = pxpaypluspay;
        }

        protected string CheckedURL(string url)
        {
            if (Request["twm"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&twm=1" : "?twm=1";
            }

            if (Request["line"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&line=1" : "?line=1";
            }

            if (Request["fb"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&fb=1" : "?fb=1";
            }

            if (Request["cht"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&cht=1" : "?cht=1";
            }

            if (Request["fetsms"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&fetsms=1" : "?fetsms=1";
            }

            if (Request["jkos"] != null)
            {
                url += url.IndexOf("?") >= 0 ? "&jkos=1" : "?jkos=1";
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