using twbobibobi.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Temple.data;
using WorkTime.data;
using System.Drawing;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.IO;
using Org.BouncyCastle.Utilities.Encoders;
using ZXing;

namespace Temple.Temples
{
    public partial class templeComplete : AjaxBasePage
    {
        public string ogurl = string.Empty;
        public string title = string.Empty;
        public string index = "https://bobibobi.tw/index.aspx";
        public string typeString = string.Empty;
        public string servicelist = string.Empty;
        public string OrderStateContent = string.Empty;
        public string OrderPurchaser = string.Empty;
        public string OrderInfo = string.Empty;
        public string EndDate = string.Empty;

        public int Total = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null && Request["aid"] != null)
                {
                    // 取得台北標準時間
                    DateTime dtNow = LightDAC.GetTaipeiNow();

                    if (Request["twm"] != null)
                    {
                        index += "&purl=twm";
                    }
                    else if (Request["purl"] != null)
                    {
                        index += "&purl=" + Request["purl"];
                    }

                    ogurl = "https://bobibobi.tw" + Request.RawUrl.ToString();

                    //HtmlMeta tag = new HtmlMeta();
                    //tag.Attributes.Add("property", "og:url");
                    //tag.Content = ogurl; // don't HtmlEncode() string. HtmlMeta already escapes characters.
                    //Page.Header.Controls.Add(tag);

                    int adminID = int.Parse(Request["a"]);
                    int ApplicantID = int.Parse(Request["aid"]);
                    int kind = int.Parse(Request["kind"]);
                    string Year = "2025";
                    bool ExpirationDate = true;
                    //this.purdue.Visible = false;
                    //this.purdue2.Visible = false;
                    //this.lineurl.HRef = "https://page.line.me/bobibobi.tw";
                    //this.Qrcodeimg.ImageUrl = "https://bobibobi.tw/Temples/images/QRCODE.png";

                    string sessionPaymentAuthKey = Session["PaymentAuthKey"] == null ? string.Empty : Session["PaymentAuthKey"].ToString();

                    if (sessionPaymentAuthKey == ("?kind=" + kind + "&a=" + adminID + "&aid=" + ApplicantID))
                    {
                        Session.Remove("PaymentAuthKey"); // ✅ 看完一次就失效

                        switch (kind)
                        {
                            case 1:
                                //點燈服務
                                typeString = " 2025點燈";
                                string startDate = "2025/11/01 00:00:00";
                                int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                                if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || Request["ad"] == "2")
                                {
                                    typeString = " 2026點燈";
                                    Year = "2026";
                                }

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
                                        GetStateContentlist_da(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/02/08 23:59";
                                        break;
                                    case 4:
                                        //新港奉天宮
                                        title = "新港奉天宮";
                                        GetStateContentlist_h(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 6:
                                        //北港武德宮
                                        title = "北港武德宮";
                                        GetStateContentlist_wu(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        EndDate = "2026/01/31 23:59";
                                        break;
                                    case 8:
                                        //西螺福興宮
                                        title = "西螺福興宮";
                                        GetStateContentlist_Fu(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 10:
                                        //台南正統鹿耳門聖母廟
                                        title = "台南正統鹿耳門聖母廟";
                                        GetStateContentlist_Luer(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        if (type == 2)
                                        {

                                        }
                                        else if (type == 3)
                                        {
                                            ExpirationDate = false;
                                        }
                                        break;
                                    case 14:
                                        //桃園威天宮
                                        title = "桃園威天宮";

                                        //一般點燈
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, type, Year);       //購買人資料列表
                                        EndDate = "2026/09/15 23:59";
                                        //if (type == 1)
                                        //{
                                        //}
                                        //else if (type == 2)
                                        //{
                                        //    //孝親祈福燈
                                        //    GetStateContentlist_ty(adminID, ApplicantID, kind, type, Year);       //購買人資料列表
                                        //    EndDate = "2025/05/08 23:59";
                                        //}
                                        break;
                                    case 15:
                                        //斗六五路財神宮
                                        title = "斗六五路財神宮";
                                        GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 16:
                                        //東海龍門天聖宮
                                        title = "台東東海龍門天聖宮";
                                        GetStateContentlist_dh(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 17:
                                        //五股賀聖宮
                                        title = "五股賀聖宮";
                                        GetStateContentlist_Hs(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 21:
                                        //鹿港城隍廟
                                        title = "鹿港城隍廟";
                                        GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 23:
                                        //玉敕大樹朝天宮
                                        title = "玉敕大樹朝天宮";
                                        GetStateContentlist_ma(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 31:
                                        //台灣道教總廟無極三清總道院
                                        title = "台灣道教總廟無極三清總道院";
                                        GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 32:
                                        //桃園龍德宮
                                        title = "桃園龍德宮";
                                        GetStateContentlist_ld(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 35:
                                        //松柏嶺受天宮
                                        title = "松柏嶺受天宮";
                                        GetStateContentlist_st(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 38:
                                        //池上北極玄天宮
                                        title = "池上北極玄天宮";
                                        GetStateContentlist_bj(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 39:
                                        //花蓮慈惠石壁部堂
                                        title = "花蓮慈惠石壁部堂";
                                        GetStateContentlist_sbbt(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 40:
                                        //新北真武山受玄宮
                                        title = "新北真武山受玄宮";
                                        GetStateContentlist_bpy(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                    case 41:
                                        //桃園壽山巖觀音寺
                                        title = "桃園壽山巖觀音寺";
                                        GetStateContentlist_ssy(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2026/10/31 23:59";
                                        break;
                                }
                                break;
                            case 2:
                                //普度服務
                                DateTime startTime = new DateTime(2024, 7, 11);
                                DateTime endTime = new DateTime(2024, 8, 10);

                                int start = DateTime.Compare(dtNow, startTime);
                                int end = DateTime.Compare(dtNow, endTime);

                                if (start >= 0 && end < 0)
                                {
                                    string AppTag = string.Empty;
                                    LightDAC objLightDAC = new LightDAC(this);
                                    if (objLightDAC.CheckedCode(adminID, ApplicantID, kind, Year, ref AppTag))
                                    {
                                        if (AppTag != "")
                                        {
                                            //this.purdue.Visible = true;
                                            //this.purdue2.Visible = true;
                                            string url = "https://bobibobi.tw/CheckedCode.aspx?id=" + AppTag + "&a=" + adminID + "&aid=" + ApplicantID + "&kind=" + kind;


                                            Color backgroundcolor = ColorTranslator.FromHtml("#ffffff");
                                            Color foregroundcolor = ColorTranslator.FromHtml("#18b41f");
                                            QRCodeHelper objQRCodeHelper = new QRCodeHelper();
                                            //Bitmap QRCode = objQRCodeHelper.GenerateColorLogoQRcode(url, "", 450, "logo", backgroundcolor, foregroundcolor);
                                            //string rePath = "~/images/qrcode/" + adminID + "-" + ApplicantID + "-" + kind + ".png";
                                            //string path = Server.MapPath(rePath);
                                            //var barcodeBitmap = new Bitmap(QRCode);

                                            Bitmap QRCode = QRCodeHelper.GenerateLogo(url, backgroundcolor, foregroundcolor);
                                            string rePath = "~/images/qrcode/" + adminID + "-" + ApplicantID + "-" + kind + ".png";
                                            string path = Server.MapPath(rePath);
                                            var barcodeBitmap = new Bitmap(QRCode);

                                            using (MemoryStream memory = new MemoryStream())
                                            {
                                                using (FileStream fs = new FileStream(path,
                                                   FileMode.Create, FileAccess.ReadWrite))
                                                {
                                                    barcodeBitmap.Save(memory, ImageFormat.Png);
                                                    byte[] bytes = memory.ToArray();
                                                    fs.Write(bytes, 0, bytes.Length);
                                                }
                                            }
                                            //this.lineurl.HRef = url;
                                            //this.Qrcodeimg.ImageUrl = rePath;
                                        }
                                        else
                                        {
                                            //this.purdue.Visible = false;
                                            //this.purdue2.Visible = false;
                                            //this.lineurl.HRef = "https://page.line.me/bobibobi.tw";
                                            //this.Qrcodeimg.ImageUrl = "https://bobibobi.tw/Temples/images/QRCODE.png";
                                        }
                                    }
                                    else
                                    {
                                        //this.purdue.Visible = false;
                                        //this.purdue2.Visible = false;
                                        //this.lineurl.HRef = "https://page.line.me/bobibobi.tw";
                                        //this.Qrcodeimg.ImageUrl = "https://bobibobi.tw/Temples/images/QRCODE.png";
                                    }
                                }
                                typeString = " 2025普度";

                                switch (adminID)
                                {
                                    case 3:
                                        //大甲鎮瀾宮
                                        title = "大甲鎮瀾宮";
                                        GetStateContentlist_da(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/08/27 23:59";
                                        break;
                                    case 4:
                                        //新港奉天宮
                                        title = "新港奉天宮";
                                        GetStateContentlist_h(adminID, ApplicantID, kind, Year);            //購買人資料列表
                                        EndDate = "2025/08/15 23:59";
                                        break;
                                    case 6:
                                        //北港武德宮
                                        title = "北港武德宮";
                                        GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/08/23 23:59";
                                        break;
                                    case 8:
                                        //西螺福興宮
                                        title = "西螺福興宮";
                                        GetStateContentlist_Fu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/09/09 23:59";
                                        break;
                                    case 10:
                                        //台南正統鹿耳門聖母廟
                                        title = "台南正統鹿耳門聖母廟";
                                        GetStateContentlist_Luer(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        EndDate = "2025/08/28 23:59";
                                        break;
                                    case 14:
                                        //桃園威天宮
                                        title = "桃園威天宮";
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                        EndDate = "2025/09/09 23:59";
                                        break;
                                    case 15:
                                        //斗六五路財神宮
                                        title = "斗六五路財神宮";
                                        GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/09/14 23:59";
                                        break;
                                    case 16:
                                        //台東東海龍門天聖宮
                                        title = "台東東海龍門天聖宮";
                                        GetStateContentlist_dh(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/09/19 23:59";
                                        break;
                                    case 21:
                                        //鹿港城隍廟
                                        title = "鹿港城隍廟";
                                        GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/08/21 23:59";
                                        break;
                                    case 23:
                                        //玉敕大樹朝天宮
                                        title = "玉敕大樹朝天宮";
                                        GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/08/25 23:59";
                                        break;
                                    case 31:
                                        //台灣道教總廟無極三清總道院
                                        title = "台灣道教總廟無極三清總道院";
                                        GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/09/17 23:59";
                                        break;
                                }

                                break;
                            case 3:
                                //商品販賣服務
                                //typeString = "商品販賣小舖";
                                break;
                            case 4:
                                //下元補庫
                                typeString = " 2025下元補庫";
                                Year = "2025";
                                title = "北港武德宮";
                                GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                EndDate = "2025/11/26 23:59";
                                break;
                            case 5:
                                //呈疏補庫
                                typeString = " 2025天官武財神聖誕補財庫";
                                title = "北港武德宮";
                                GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                EndDate = "2025/03/24 23:59";
                                break;
                            case 6:
                                //企業補財庫
                                title = "北港武德宮";
                                typeString = " " + dtNow.Year.ToString() + "企業補財庫";
                                ExpirationDate = false;
                                Year = dtNow.Year.ToString();

                                GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                break;
                            case 7:
                                switch (adminID)
                                {
                                    case 14:
                                        // 桃園威天宮天赦日補運
                                        typeString = " 2025天赦日招財補運";
                                        title = " 桃園威天宮";
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                        EndDate = "2025/12/18 11:00";
                                        break;
                                    case 23:
                                        // 玉敕大樹朝天宮天赦日招財補運
                                        typeString = " 2025天赦日招財補運";
                                        title = " 玉敕大樹朝天宮";
                                        GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                        EndDate = "2025/12/20 23:59";
                                        break;
                                }
                                break;
                            case 8:
                                //進寶財神廟天赦日祭改
                                break;
                            case 9:
                                //桃園威天宮關聖帝君聖誕
                                typeString = " 2025關聖帝君聖誕千秋祝壽謝恩祈福活動";
                                title = " 桃園威天宮";
                                GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                EndDate = "2025/08/12 23:59";
                                break;
                            case 10:
                                break;
                            case 11:
                                // 台東東海龍門天聖宮天貺納福添運法會
                                typeString = " 2024天貺納福添運法會";
                                title = " 台東東海龍門天聖宮";
                                GetStateContentlist_dh(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                EndDate = "2024/07/11 23:59";
                                break;
                            case 12:
                                //桃園威天宮靈寶禮斗
                                typeString = " 2025靈寶禮斗";
                                title = " 玉敕大樹朝天宮";
                                GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                EndDate = "2025/11/06 23:59";
                                break;
                            case 13:
                                //大甲鎮瀾宮七朝清醮
                                typeString = " 重修慶成祈安七朝清醮活動";
                                Year = "2024";
                                title = " 大甲鎮瀾宮";
                                GetStateContentlist_da(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                EndDate = "2024/12/01 23:59";
                                break;
                            case 14:
                                //桃園威天宮九九重陽天赦日補運
                                typeString = " 2024九九重陽天赦日 招財補運";
                                title = " 桃園威天宮";
                                GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                EndDate = "2024/10/09 16:00";
                                break;
                            //護國息災梁皇大法會
                            case 15:
                                typeString = " 護國息災梁皇大法會";
                                Year = "2025";

                                switch (adminID)
                                {
                                    case 16:
                                        //台東東海龍門天聖宮
                                        title = "台東東海龍門天聖宮";
                                        GetStateContentlist_dh(adminID, ApplicantID, kind, Year);   //購買人資料列表
                                        EndDate = "2025/11/10 23:59";
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
                                        title = "斗六五路財神宮";
                                        GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        ExpirationDate = false;
                                        EndDate = "2025/12/31 23:59";
                                        break;
                                    case 21:
                                        //鹿港城隍廟
                                        title = "鹿港城隍廟";
                                        GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        EndDate = "2025/06/25 23:59";
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
                                        GetStateContentlist_sx(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        EndDate = "2025/02/03 23:59";
                                        break;
                                }
                                break;
                            //桃園威天宮天公生招財補運
                            case 18:
                                typeString = " 2025天公生 招財補運";
                                Year = "2025";
                                title = " 桃園威天宮";
                                GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                EndDate = "2025/02/03 23:59";
                                break;
                            //供香轉運
                            case 19:
                                typeString = " 供香轉運";
                                Year = "2025";

                                switch (adminID)
                                {
                                    case 33:
                                        //神霄玉府財神會館
                                        title = "神霄玉府財神會館";
                                        GetStateContentlist_sx(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                        ExpirationDate = false;
                                        EndDate = "2025/12/31 23:59";
                                        break;
                                }
                                break;
                            //安斗服務
                            case 20:
                                typeString = " 2025安斗";

                                switch (adminID)
                                {
                                    case 15:
                                        //斗六五路財神宮
                                        title = "斗六五路財神宮";
                                        GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2025/10/31 23:59";
                                        break;
                                    case 31:
                                        //台灣道教總廟無極三清總道院
                                        title = "台灣道教總廟無極三清總道院";
                                        GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2025/10/31 23:59";
                                        break;
                                }
                                break;
                            //供花供果服務
                            case 21:
                                typeString = " 2025供花供果";

                                switch (adminID)
                                {
                                    case 31:
                                        //台灣道教總廟無極三清總道院
                                        title = "台灣道教總廟無極三清總道院";
                                        GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2025/06/30 23:59";
                                        break;
                                }
                                break;
                            //孝親祈福燈
                            case 22:
                                typeString = " 2025孝親祈福燈";

                                switch (adminID)
                                {
                                    case 14:
                                        // 桃園威天宮
                                        title = "桃園威天宮";
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, 2, Year);       //購買人資料列表
                                        EndDate = "2025/06/30 23:59";
                                        break;
                                }
                                break;
                            //祈安植福
                            case 23:
                                typeString = " 2025祈安植福";

                                switch (adminID)
                                {
                                    case 35:
                                        // 松柏嶺受天宮
                                        title = "松柏嶺受天宮";
                                        GetStateContentlist_st(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                        EndDate = "2025/12/31 23:59";
                                        break;
                                }
                                break;
                            //祈安禮斗
                            case 24:
                                typeString = " 2025祈安禮斗";
                                break;
                            //千手觀音千燈迎佛法會
                            case 25:
                                typeString = " 2025千手觀音千燈迎佛法會";

                                switch (adminID)
                                {
                                    case 14:
                                        // 桃園威天宮
                                        title = "桃園威天宮";
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);       //購買人資料列表
                                        EndDate = "2025/11/04 23:59";
                                        break;
                                }
                                break;
                        }

                        if ((dtNow >= DateTime.Parse(EndDate)) && ExpirationDate)
                        {
                            if (title != "" && typeString != "")
                            {
                                Response.Write("<script>alert('親愛的大德您好\\n" + title + typeString + "活動已截止！！\\n感謝您的支持, 謝謝!');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('訪問網址錯誤，請重新進入。');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('頁面已逾時。\\n此付款頁面已失效。\\n若您需要查詢訂單狀況，請至訂單查詢頁面。');location='https://bobibobi.tw/SearchLog.aspx'</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('訪問網址錯誤，請重新進入。');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                }
            }

            //Page.DataBind();
        }

        //購買人資料列表-大甲鎮瀾宮
        public void GetStateContentlist_da(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_da_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "大甲鎮瀾宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_da_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            string sendback = dtData.Rows[i]["Sendback"].ToString();

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
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

                            //OrderInfo += OrderData("普度組數", dtData.Rows[i]["Count"].ToString());

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

                            OrderInfo += "</div></div>";

                            //普度項目金額
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

                            //int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 1000;
                            //OrderInfo += "<div>$ " + cost + "元</div>";
                            //Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 13:
                    //七朝清醮
                    dtData = objLightDAC.GetAPPCharge_da_TaoistJiaoCeremony(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            string taoistJiaoCeremonyString = dtData.Rows[i]["TaoistJiaoCeremonyString"].ToString();
                            string taoistJiaoCeremonyType = dtData.Rows[i]["TaoistJiaoCeremonyType"].ToString();

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", taoistJiaoCeremonyString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "大甲鎮瀾宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());

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
                            int cost = GetTaoistJiaoCeremonyCost(AdminID, taoistJiaoCeremonyType);
                            cost += Sendback == "Y" ? 250 : 0;
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }


        //購買人資料列表-新港奉天宮
        public void GetStateContentlist_h(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_h_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "新港奉天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_h_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        OrderPurchaser += OrderData("農曆生日", dtData.Rows[0]["AppBirth"].ToString() + (dtData.Rows[0]["AppLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderPurchaser += OrderData("農曆時辰", dtData.Rows[0]["AppBirthTime"].ToString() == "" ? "吉" : dtData.Rows[0]["AppBirthTime"].ToString());

                        if (dtData.Columns.Contains("AppsBirth"))
                        {
                            var rawAppsBirth = dtData.Rows[0]["AppsBirth"];
                            if (rawAppsBirth != DBNull.Value)
                            {
                                var appsBirthText = rawAppsBirth.ToString();
                                OrderPurchaser += OrderData("國曆生日", appsBirthText);
                            }
                        }
                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //普度項目金額
                            int cost = 0;

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());


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
                                    var sexText = rawSex.ToString();
                                    OrderInfo += OrderData("祈福人性別", sexText);
                                }
                            }
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["Birth"].ToString() == "" ? "吉" : dtData.Rows[i]["Birth"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[0]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("國曆生日", appsBirthText);
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
                                    var rawRemark = dtData.Rows[i]["Remark"];
                                    if (rawRemark != DBNull.Value)
                                    {
                                        var remarkText = rawRemark.ToString();
                                        OrderInfo += Ordertextarea(remarkText);
                                    }
                                }
                            }

                            OrderInfo += "</div></div>";

                            cost = getTotal(dtData);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
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
        public void GetStateContentlist_wu(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_wu_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "北港武德宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_wu_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("國曆生日", appsBirthText);
                                }
                            }
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人信箱", dtData.Rows[i]["Email"].ToString());
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 3:
                    //商品販賣服務
                    break;
                case 4:
                    //下元補庫
                    dtData = objLightDAC.GetAPPCharge_wu_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", SuppliesString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "北港武德宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("地址", dtData.Rows[i]["Address"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * 650;
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 5:
                    //呈疏補庫
                    dtData = objLightDAC.GetAPPCharge_wu_Supplies2(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", SuppliesString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "北港武德宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                    break;
                case 6:
                    //企業補財庫
                    dtData = objLightDAC.GetAPPCharge_wu_Supplies3(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", "企業補財庫");

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                    break;
            }
        }


        //購買人資料列表-西螺福興宮
        public void GetStateContentlist_Fu(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_Fu_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "西螺福興宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_Fu_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //普度項目金額
                            int cost = 0;

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            switch (purdueType)
                            {
                                case "1":
                                    //贊普
                                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                    OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                                    OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                                    if (dtData.Columns.Contains("Sex"))
                                    {
                                        var rawAppsBirth = dtData.Rows[i]["Sex"];
                                        if (rawAppsBirth != DBNull.Value)
                                        {
                                            var appsBirthText = rawAppsBirth.ToString();
                                            OrderInfo += OrderData("祈福人性別", appsBirthText);
                                        }
                                    }
                                    if (dtData.Columns.Contains("Birth") && dtData.Columns.Contains("LeapMonth"))
                                    {
                                        var rawAppsBirth = dtData.Rows[i]["Birth"];
                                        if (rawAppsBirth != DBNull.Value)
                                        {
                                            var appsBirthText = rawAppsBirth.ToString();
                                            OrderInfo += OrderData("農曆生日", appsBirthText + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                                        }
                                    }
                                    if (dtData.Columns.Contains("BirthTime"))
                                    {
                                        var rawAppsBirth = dtData.Rows[i]["BirthTime"];
                                        if (rawAppsBirth != DBNull.Value)
                                        {
                                            var appsBirthText = rawAppsBirth.ToString();
                                            OrderInfo += OrderData("農曆時辰", appsBirthText);
                                        }
                                    }
                                    if (dtData.Columns.Contains("sBirth"))
                                    {
                                        var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                        if (rawAppsBirth != DBNull.Value)
                                        {
                                            var appsBirthText = rawAppsBirth.ToString();
                                            OrderInfo += OrderData("國曆生日", appsBirthText);
                                        }
                                    }
                                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                                    OrderInfo += OrderData("普品數量", dtData.Rows[i]["Count"].ToString());

                                    cost = 1500 * int.Parse(dtData.Rows[i]["Count"].ToString());
                                    break;
                                case "2":
                                    //九玄七祖
                                    cost = 600;
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
                                    cost = 600;
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
                                    cost = 600;
                                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                    OrderInfo += OrderData("超度地址", dtData.Rows[i]["Address"].ToString());

                                    GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                                    OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                                    cost += GoldPaperCount * 300;
                                    break;
                                case "5":
                                    //冤親債主-超度指定對象
                                    cost = 600;
                                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                    OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                                    OrderInfo += OrderData("超度對象地址", dtData.Rows[i]["Address"].ToString());

                                    GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                                    OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                                    cost += GoldPaperCount * 300;
                                    break;
                                case "6":
                                    //嬰靈-超度指定對象
                                    cost = 600;
                                    OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                    OrderInfo += OrderData("超度姓名", dtData.Rows[i]["DeathName"].ToString());
                                    OrderInfo += OrderData("超度對象地址", dtData.Rows[i]["Address"].ToString());

                                    GoldPaperCount = int.Parse(dtData.Rows[i]["GoldPaperCount"].ToString());
                                    OrderInfo += OrderData("加購金紙數量", dtData.Rows[i]["GoldPaperCount"].ToString());
                                    cost += GoldPaperCount * 300;
                                    break;
                                case "11":
                                    //動物靈-超度指定對象
                                    cost = 600;
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 3:
                    //商品販賣服務
                    break;
                case 4:
                    //補財庫
                    break;
                case 5:
                    //補財庫-呈疏補庫
                    break;
            }
        }


        //購買人資料列表-桃園大廟景福宮
        //public void GetStateContentlist_Jing(int AdminID, int ApplicantID)
        //{
        //    LightDAC objLightDAC = new LightDAC(this);

        //    DataTable dtData = objLightDAC.GetAPPCharge_Jing_Purdue(ApplicantID);

        //    if (dtData.Rows.Count > 0)
        //    {
        //        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

        //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
        //        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

        //        if (dtData.Columns.Contains("AppEmail"))
        //        {
        //            var rawAppEmail = dtData.Rows[0]["AppEmail"];
        //            if (rawAppEmail != DBNull.Value)
        //            {
        //                var appEmailText = rawAppEmail.ToString();
        //                OrderPurchaser += OrderData("購買人信箱", appEmailText);
        //            }
        //        }

        //        if (dtData.Columns.Contains("AppAddress"))
        //        {
        //            DataRow firstRow = dtData.Rows[0];
        //            string appAddressText = firstRow.Field<string>("AppAddress");
        //            if (!string.IsNullOrWhiteSpace(appAddressText))
        //            {
        //                OrderPurchaser += OrderData("購買人地址", appAddressText);
        //            }
        //        }

        //        OrderInfo = string.Empty;

        //        for (int i = 0; i < dtData.Rows.Count; i++)
        //        {
        //            OrderInfo += "<li><div>";

        //            string purdueString = dtData.Rows[i]["PurdueString"].ToString();
        //            string purdueType = dtData.Rows[i]["PurdueType"].ToString();

        //            ////普度項目
        //            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

        //            //祈福人內容列表
        //            OrderInfo += "<div class=\"ProductsInfo\">";

        //            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
        //            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
        //            OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
        //            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
        //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

        //            OrderInfo += "</div></div>";

        //            //普度項目金額
        //            int cost = GetpurdueCost(AdminID, purdueType, "");
        //            OrderInfo += "<div>$ " + cost + "元</div>";
        //            Total += cost;

        //            OrderInfo += "</li>";
        //        }
        //    }
        //}


        //購買人資料列表-台南正統鹿耳門聖母廟
        public void GetStateContentlist_Luer(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_Luer_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "台南正統鹿耳門聖母廟");
                            if (dtData.Rows[i]["PetName"].ToString() != "")
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                                OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                                OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                                OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                                OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                                OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                                OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                                OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                                OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                                OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                            }
                            else
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                                OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                                OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                                OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                                OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                                OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                                OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            }

                            if (dtData.Rows[i]["Msg"].ToString() != "")
                            {
                                OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_Luer_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人姓名2", dtData.Rows[i]["Name2"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("國曆生日", appsBirthText);
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

                            //普度項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }


        //購買人資料列表-桃園威天宮
        public void GetStateContentlist_ty(int AdminID, int ApplicantID, int kind, int type, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    if (type == 2)
                    {
                        //購買人資料列表-桃園威天宮孝親祈福燈
                        dtData = objLightDAC.GetAPPCharge_ty_mom_Lights(ApplicantID, Year);
                        if (dtData.Rows.Count > 0)
                        {
                            OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                            OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                            OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                            OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                            if (dtData.Columns.Contains("AppEmail"))
                            {
                                var rawAppEmail = dtData.Rows[0]["AppEmail"];
                                if (rawAppEmail != DBNull.Value)
                                {
                                    var appEmailText = rawAppEmail.ToString();
                                    OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                                OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                                OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                                OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
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

                                //服務項目金額
                                int cost = 880;
                                OrderInfo += "<div>$ " + cost + "元</div>";
                                Total += cost;

                                OrderInfo += "</li>";
                            }
                        }
                        break;
                    }
                    else
                    {
                        dtData = objLightDAC.GetAPPCharge_ty_Lights(ApplicantID, Year);
                        if (dtData.Rows.Count > 0)
                        {
                            OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                            OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                            OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                            if (dtData.Columns.Contains("AppEmail"))
                            {
                                var rawAppEmail = dtData.Rows[0]["AppEmail"];
                                if (rawAppEmail != DBNull.Value)
                                {
                                    var appEmailText = rawAppEmail.ToString();
                                    OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                                OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                                int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                                OrderInfo += "<div>$ " + cost + "元</div>";
                                Total += cost;

                                OrderInfo += "</li>";
                            }
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_ty_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            if (dtData.Columns.Contains("Sex"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["Sex"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人性別", appsBirthText);
                                }
                            }
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人國曆生日", appsBirthText);
                                }
                            }
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
                            int cost = count * GetPurdueCost(AdminID, purdueType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 7:
                    //天赦日補運
                    dtData = objLightDAC.GetAPPCharge_ty_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
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

                            //服務項目金額
                            int cost = GetSuppliesCost(AdminID, suppliesType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 9:
                    //關聖帝君聖誕
                    dtData = objLightDAC.GetAPPCharge_ty_EmperorGuansheng(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", emperorGuanshengString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = GetEmperorGuanshengCost(AdminID, emperorGuanshengType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 14:
                    //九九重陽天赦日補運
                    dtData = objLightDAC.GetAPPCharge_ty_Supplies2(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
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

                            //服務項目金額
                            int cost = GetSuppliesCost(AdminID, suppliesType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 18:
                    //天公生招財補運
                    dtData = objLightDAC.GetAPPCharge_ty_Supplies3(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
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

                            //服務項目金額
                            int cost = GetSuppliesCost(AdminID, suppliesType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 25:
                    //千手觀音千燈迎佛法會服務
                    dtData = objLightDAC.GetAPPCharge_ty_QnLight(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            string lightsString = dtData.Rows[i]["QnLightString"].ToString();
                            string lightsType = dtData.Rows[i]["QnLightType"].ToString();

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetQnLightCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 27:
                    //新春賀歲感恩招財祿位服務
                    dtData = objLightDAC.GetAPPCharge_ty_Luckaltar(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppsBirth"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            string lightsString = dtData.Rows[i]["LuckaltarString"].ToString();
                            string lightsType = dtData.Rows[i]["LuckaltarType"].ToString();

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園威天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLuckaltarCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
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
        public void GetStateContentlist_Fw(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_Fw_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "斗六五路財神宮");
                            if (dtData.Rows[i]["PetName"].ToString() != "")
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                                OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                                OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                                OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                                OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                                OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                                OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                                OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                                OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                                OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                            }
                            else
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_Fw_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int count_rice = 0;
                            int.TryParse(dtData.Rows[i]["Count_rice"].ToString(), out count_rice);

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            if (dtData.Columns.Contains("Sex"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["Sex"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人性別", appsBirthText);
                                }
                            }
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString() == "" ? "吉" : dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("國曆生日", appsBirthText);
                                }
                            }
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            if (count_rice != 0)
                            {
                                OrderInfo += OrderData("捐獻白米", dtData.Rows[i]["Count_rice"].ToString());
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

                            //普度項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                            cost += 200 * count_rice;
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 16:
                    //補財庫服務
                    dtData = objLightDAC.GetAPPCharge_Fw_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "斗六五路財神宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 20:
                    //安斗服務
                    dtData = objLightDAC.GetAPPCharge_Fw_AnDou(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "斗六五路財神宮");
                            if (dtData.Rows[i]["PetName"].ToString() != "")
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                                OrderInfo += OrderData("飼主姓名", dtData.Rows[i]["Name"].ToString());
                                OrderInfo += OrderData("飼主電話", dtData.Rows[i]["Mobile"].ToString());
                                OrderInfo += OrderData("飼主性別", dtData.Rows[i]["Sex"].ToString());
                                OrderInfo += OrderData("飼主農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                                OrderInfo += OrderData("飼主農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                                OrderInfo += OrderData("飼主國曆生日", dtData.Rows[i]["sBirth"].ToString());
                                OrderInfo += OrderData("飼主地址", dtData.Rows[i]["Address"].ToString());

                                OrderInfo += OrderData("寵物姓名", dtData.Rows[i]["PetName"].ToString());
                                OrderInfo += OrderData("寵物品種", dtData.Rows[i]["PetType"].ToString());
                            }
                            else
                            {
                                OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }


        //購買人資料列表-台東東海龍門天聖宮
        public void GetStateContentlist_dh(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_dh_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "台東東海龍門天聖宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_dh_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台東東海龍門天聖宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            if (dtData.Columns.Contains("Sex"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["Sex"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人性別", appsBirthText);
                                }
                            }
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人國曆生日", appsBirthText);
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 11:
                    //天貺納福添運法會
                    dtData = objLightDAC.GetAPPCharge_dh_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台東東海龍門天聖宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            //服務項目金額
                            int cost = 0;

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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 15:
                    //護國息災梁皇大法會
                    dtData = objLightDAC.GetAPPCharge_dh_Lybc(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lybcString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台東東海龍門天聖宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = GetLybcCost(AdminID, lybcType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }


        //購買人資料列表-五股賀聖宮
        public void GetStateContentlist_Hs(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_Hs_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            OrderInfo += OrderData("宮廟名稱", "五股賀聖宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }


        //購買人資料列表-鹿港城隍廟
        public void GetStateContentlist_Lk(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_Lk_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
                            }
                        }

                        //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回" : "不寄回");

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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            //    OrderInfo += OrderData("收件人地址", (dtData.Rows[i]["rZipCode"].ToString() != "" ? dtData.Rows[i]["rZipCode"].ToString() + " " : "") + dtData.Rows[i]["rAddress"].ToString());
                            //}

                            OrderInfo += "</div></div>";

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_Lk_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                        if (dtData.Columns.Contains("AppSendback"))
                        {
                            //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回" : "不寄回");

                            if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                            {
                                OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                                OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                                OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["AppAddress"].ToString());
                            }
                        }

                        OrderInfo = string.Empty;

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            OrderInfo += "<li><div>";

                            string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                            string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人國曆生日", appsBirthText);
                                }
                            }
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

                            //普度項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 16:
                    //補財庫服務
                    dtData = objLightDAC.GetAPPCharge_Lk_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
                            }
                        }

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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-玉敕大樹朝天宮
        public void GetStateContentlist_ma(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_ma_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "玉敕大樹朝天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    dtData = objLightDAC.GetAPPCharge_ma_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////普度項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "玉敕大樹朝天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            if (dtData.Columns.Contains("sBirth"))
                            {
                                var rawAppsBirth = dtData.Rows[i]["sBirth"];
                                if (rawAppsBirth != DBNull.Value)
                                {
                                    var appsBirthText = rawAppsBirth.ToString();
                                    OrderInfo += OrderData("祈福人國曆生日", appsBirthText);
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
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                            OrderInfo += "</div></div>";

                            //普度項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 7:
                    //天赦日招財補運
                    dtData = objLightDAC.GetAPPCharge_ma_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "玉敕大樹朝天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                            int cost = GetSuppliesCost(AdminID, suppliesType);
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 12:
                    //靈寶禮斗
                    dtData = objLightDAC.GetAPPCharge_ma_Lingbaolidou(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lingbaolidouString);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "玉敕大樹朝天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("時辰", dtData.Rows[i]["BirthTime"].ToString());
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
                            int cost = GetLingbaolidouCost(AdminID, lingbaolidouType) * Count;
                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-鎮瀾買足
        public void GetStateContentlist_mazu(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    //dtData = objLightDAC.GetAPPCharge_mazu_Lights(ApplicantID, Year);
                    //if (dtData.Rows.Count > 0)
                    //{
                    //    OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                    //    OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                    //    OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                    //    OrderInfo = string.Empty;

                    //    for (int i = 0; i < dtData.Rows.Count; i++)
                    //    {
                    //        OrderInfo += "<li><div>";

                    //        string lightsString = dtData.Rows[i]["LightsString"].ToString();
                    //        string lightsType = dtData.Rows[i]["LightsType"].ToString();

                    //        ////服務項目
                    //        OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", lightsString);

                    //        //服務項目金額
                    //        int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                    //        //祈福人內容列表
                    //        OrderInfo += "<div class=\"ProductsInfo\">";

                    //        OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                    //        OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                    //        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    //        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    //        string sex = "善男";
                    //        if (dtData.Rows[i]["Sex"].ToString() == "F")
                    //        {
                    //            sex = "信女";
                    //        }
                    //        OrderInfo += OrderData("性別", sex);
                    //        OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    //        OrderInfo += OrderData("市話", dtData.Rows[i]["HomeNum"].ToString());
                    //        //OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString() == "" ? "吉" : dtData.Rows[i]["BirthTime"].ToString());
                    //        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    //        if (dtData.Rows[i]["Sendback"].ToString() == "Y")
                    //        {
                    //            cost += 100;
                    //            OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
                    //            OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
                    //            OrderInfo += OrderData("收件人地址", (dtData.Rows[i]["rZipCode"].ToString() != "" ? dtData.Rows[i]["rZipCode"].ToString() + " " : "") + dtData.Rows[i]["rAddress"].ToString());
                    //        }

                    //        OrderInfo += "</div></div>";

                    //        OrderInfo += "<div>$ " + cost + "元</div>";
                    //        Total += cost;

                    //        OrderInfo += "</li>";
                    //    }
                    //}
                    break;
                case 2:
                    //普度服務
                    //dtData = objLightDAC.GetAPPCharge_mazu_Purdue(ApplicantID, Year);
                    //if (dtData.Rows.Count > 0)
                    //{
                    //    OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                    //    OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                    //    OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                    //    if (dtData.Columns.Contains("AppEmail"))
                    //    {
                    //        var rawAppEmail = dtData.Rows[0]["AppEmail"];
                    //        if (rawAppEmail != DBNull.Value)
                    //        {
                    //            var appEmailText = rawAppEmail.ToString();
                    //            OrderPurchaser += OrderData("購買人信箱", appEmailText);
                    //        }
                    //    }

                    //    if (dtData.Columns.Contains("AppAddress"))
                    //    {
                    //        DataRow firstRow = dtData.Rows[0];
                    //        string appAddressText = firstRow.Field<string>("AppAddress");
                    //        if (!string.IsNullOrWhiteSpace(appAddressText))
                    //        {
                    //            OrderPurchaser += OrderData("購買人地址", appAddressText);
                    //        }
                    //    }

                    //    OrderInfo = string.Empty;

                    //    for (int i = 0; i < dtData.Rows.Count; i++)
                    //    {
                    //        OrderInfo += "<li><div>";

                    //        string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    //        string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    //        ////普度項目
                    //        OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //        //祈福人內容列表
                    //        OrderInfo += "<div class=\"ProductsInfo\">";

                    //        OrderInfo += OrderData("宮廟名稱", "大甲鎮瀾宮");
                    //        OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                    //        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    //        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    //        OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    //        OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());

                    //        OrderInfo += "</div></div>";

                    //        //普度項目金額
                    //        int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    //        OrderInfo += "<div>$ " + cost + "元</div>";
                    //        Total += cost;

                    //        OrderInfo += "</li>";
                    //    }
                    //}
                    break;
            }
        }

        //購買人資料列表-台灣道教總廟無極三清總道院
        public void GetStateContentlist_wjsan(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_wjsan_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台灣道教總廟無極三清總道院");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 2:
                    //普度服務
                    //dtData = objLightDAC.GetAPPCharge_wjsan_Purdue(ApplicantID, Year);
                    //if (dtData.Rows.Count > 0)
                    //{
                    //    OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                    //    OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                    //    OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                    //    OrderInfo = string.Empty;

                    //    for (int i = 0; i < dtData.Rows.Count; i++)
                    //    {
                    //        OrderInfo += "<li><div>";

                    //        string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                    //        string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                    //        ////普度項目
                    //        OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                    //        //祈福人內容列表
                    //        OrderInfo += "<div class=\"ProductsInfo\">";

                    //        OrderInfo += OrderData("宮廟名稱", "玉敕大樹朝天宮");
                    //        OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                    //        OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                    //        OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                    //        OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                    //        OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                    //        OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                    //        OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                    //        OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    //        switch (purdueType)
                    //        {
                    //            case "2":
                    //                OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                    //                break;
                    //            case "6":
                    //                OrderInfo += OrderData("姓氏", dtData.Rows[i]["FirstName"].ToString());
                    //                break;
                    //        }
                    //        OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                    //        OrderInfo += "</div></div>";

                    //        //普度項目金額
                    //        int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                    //        OrderInfo += "<div>$ " + cost + "元</div>";
                    //        Total += cost;

                    //        OrderInfo += "</li>";
                    //    }
                    //}
                    break;
                case 20:
                    //安斗服務
                    dtData = objLightDAC.GetAPPCharge_wjsan_AnDou(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //服務項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, andouType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台灣道教總廟無極三清總道院");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 21:
                    //供花供果服務
                    dtData = objLightDAC.GetAPPCharge_wjsan_Huaguo(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            ////服務項目
                            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", huaguoString);

                            //服務項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetHuaguoCost(AdminID, huaguoType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "台灣道教總廟無極三清總道院");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-神霄玉府財神會館
        public void GetStateContentlist_sx(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                //case 1:
                //    //點燈服務
                //    dtData = objLightDAC.GetAPPCharge_sx_Lights(ApplicantID, Year);
                //    if (dtData.Rows.Count > 0)
                //    {
                //        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                //        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
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
                //            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                //            //祈福人內容列表
                //            OrderInfo += "<div class=\"ProductsInfo\">";

                //            OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                //            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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
                //            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                //            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                //            //if (dtData.Rows[i]["Sendback"].ToString() == "Y")
                //            //{
                //            //    cost += 100;
                //            //    OrderInfo += OrderData("收件人姓名", dtData.Rows[i]["rName"].ToString());
                //            //    OrderInfo += OrderData("收件人電話", dtData.Rows[i]["rMobile"].ToString());
                //            //    OrderInfo += OrderData("收件人地址", (dtData.Rows[i]["rZipCode"].ToString() != "" ? dtData.Rows[i]["rZipCode"].ToString() + " " : "") + dtData.Rows[i]["rAddress"].ToString());
                //            //}

                //            OrderInfo += "</div></div>";

                //            OrderInfo += "<div>$ " + cost + "元</div>";
                //            Total += cost;

                //            OrderInfo += "</li>";
                //        }
                //    }
                //    break;
                //case 2:
                //    //普度服務
                //    dtData = objLightDAC.GetAPPCharge_sx_Purdue(ApplicantID, Year);
                //    if (dtData.Rows.Count > 0)
                //    {
                //        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                //        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                //        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                //        OrderInfo = string.Empty;

                //        for (int i = 0; i < dtData.Rows.Count; i++)
                //        {
                //            OrderInfo += "<li><div>";

                //            string purdueString = dtData.Rows[i]["PurdueString"].ToString();
                //            string purdueType = dtData.Rows[i]["PurdueType"].ToString();

                //            ////普度項目
                //            OrderInfo += String.Format("<div class=\"ProductsName\">{0}</div>", purdueString);

                //            //祈福人內容列表
                //            OrderInfo += "<div class=\"ProductsInfo\">";

                //            OrderInfo += OrderData("宮廟名稱", "鹿港城隍廟");
                //            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                //            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                //            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                //            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                //            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                //            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                //            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                //            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                //            OrderInfo += "</div></div>";

                //            //普度項目金額
                //            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetPurdueCost(AdminID, purdueType);
                //            OrderInfo += "<div>$ " + cost + "元</div>";
                //            Total += cost;

                //            OrderInfo += "</li>";
                //        }
                //    }
                //    break;
                case 17:
                    //赦罪補庫服務
                    dtData = objLightDAC.GetAPPCharge_sx_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
                            }
                        }

                        string appAddressText = string.Empty;
                        string apprAddressText = string.Empty;
                        if (dtData.Columns.Contains("AppAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            appAddressText = firstRow.Field<string>("AppAddress");
                        }
                        else if (dtData.Columns.Contains("ApprAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            apprAddressText = firstRow.Field<string>("ApprAddress");
                        }

                        if (!string.IsNullOrWhiteSpace(appAddressText))
                        {
                            OrderPurchaser += OrderData("購買人地址", appAddressText);
                        }
                        else if (!string.IsNullOrWhiteSpace(apprAddressText))
                        {
                            OrderPurchaser += OrderData("購買人地址", apprAddressText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "神霄玉府財神會館");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 19:
                    //供香轉運服務
                    dtData = objLightDAC.GetAPPCharge_sx_Supplies2(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
                            }
                        }

                        string appAddressText = string.Empty;
                        string apprAddressText = string.Empty;
                        if (dtData.Columns.Contains("AppAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            appAddressText = firstRow.Field<string>("AppAddress");
                        }
                        else if (dtData.Columns.Contains("ApprAddress"))
                        {
                            DataRow firstRow = dtData.Rows[0];
                            apprAddressText = firstRow.Field<string>("ApprAddress");
                        }

                        if (!string.IsNullOrWhiteSpace(appAddressText))
                        {
                            OrderPurchaser += OrderData("購買人地址", appAddressText);
                        }
                        else if (!string.IsNullOrWhiteSpace(apprAddressText))
                        {
                            OrderPurchaser += OrderData("購買人地址", apprAddressText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetSuppliesCost(AdminID, suppliesType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "神霄玉府財神會館");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-桃園龍德宮
        public void GetStateContentlist_ld(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_ld_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
                            }
                        }

                        if (dtData.Columns.Contains("AppSendback"))
                        {
                            var rawAppSendback = dtData.Rows[0]["AppSendback"];
                            if (rawAppSendback != DBNull.Value)
                            {
                                var appSendbackText = rawAppSendback.ToString();
                                OrderPurchaser += OrderData("贈品處理方式", appSendbackText);

                                if (appSendbackText == "Y")
                                {
                                    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                                    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                                    OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["AppAddress"].ToString());
                                }
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園龍德宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-松柏嶺受天宮
        public void GetStateContentlist_st(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_st_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "松柏嶺受天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
                case 23:
                    //祈安植福
                    dtData = objLightDAC.GetAPPCharge_st_Blessing(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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

                            //服務項目金額
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetBlessingCost(AdminID, blessingType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "松柏嶺受天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-池上北極玄天宮
        public void GetStateContentlist_bj(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_bj_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "池上北極玄天宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-花蓮慈惠石壁部堂
        public void GetStateContentlist_sbbt(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_sbbt_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "花蓮慈惠石壁部堂");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-新北真武山受玄宮
        public void GetStateContentlist_bpy(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_bpy_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "新北真武山受玄宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
                    }
                    break;
            }
        }

        //購買人資料列表-桃園壽山巖觀音寺
        public void GetStateContentlist_ssy(int AdminID, int ApplicantID, int kind, string Year)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = new DataTable();

            switch (kind)
            {
                case 1:
                    //點燈服務
                    dtData = objLightDAC.GetAPPCharge_ssy_Lights(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

                        if (dtData.Columns.Contains("AppEmail"))
                        {
                            var rawAppEmail = dtData.Rows[0]["AppEmail"];
                            if (rawAppEmail != DBNull.Value)
                            {
                                var appEmailText = rawAppEmail.ToString();
                                OrderPurchaser += OrderData("購買人信箱", appEmailText);
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
                            int cost = int.Parse(dtData.Rows[i]["Count"].ToString()) * GetLightsCost(AdminID, lightsType);

                            //祈福人內容列表
                            OrderInfo += "<div class=\"ProductsInfo\">";

                            OrderInfo += OrderData("宮廟名稱", "桃園壽山巖觀音寺");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
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

                            OrderInfo += "<div>$ " + cost + "元</div>";
                            Total += cost;

                            OrderInfo += "</li>";
                        }
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

        protected string OrderState(string label, string text)
        {
            string result = "<div>\r\n                                <div class=\"OrderLabel\">{0}：</div>\r\n                                <div class=\"OrderTxt\">{1}</div>\r\n                            </div>";

            result = String.Format(result, label, text);

            return result;
        }

        protected int GetpurdueCost(int AdminID, string PurdueType, string Sendback)
        {
            int result = 0;

            //switch (AdminID)
            //{
            //    case 3:
            //        //安平開台天后宮
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
            //}

            switch (AdminID)
            {
                case 3:
                    //安平開台天后宮
                    if (PurdueType == "1")
                    {
                        //贊普
                        int cost = 1000;

                        cost = Sendback == "1" ? cost += 250 : cost;

                        result = cost;
                    }
                    else
                    {
                        //超拔
                        result = 620;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    break;
                case 6:
                    //北港武德宮
                    break;
                case 8:
                    //西螺福興宮
                    break;
                case 9:
                    //桃園大廟景福宮
                    result = 1000;
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    break;
            }

            return result;
        }

    }
}