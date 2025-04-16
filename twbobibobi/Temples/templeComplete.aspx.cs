using MotoSystem.Data;
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
        public string EndDate = "2023/07/09 23:59";

        public int Total = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["a"] != null && Request["aid"] != null)
                {
                    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                    index += (Request["twm"] != null ? "&twm=1" : "");

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

                    switch (kind)
                    {
                        case 1:
                            //點燈服務
                            typeString = " 2024點燈";
                            string startDate = "2024/11/01 00:00:00";
                            int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                            if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || Request["ad"] == "2")
                            {
                                typeString = " 2025點燈";
                                Year = "2025";
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
                                    EndDate = "2025/06/30 23:59";
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetStateContentlist_h(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    EndDate = "2025/10/31 23:59";
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetStateContentlist_wu(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    EndDate = "2025/01/19 23:59";
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetStateContentlist_Fu(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/10/31 23:59";
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetStateContentlist_Luer(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/10/31 23:59";
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
                                    if (type == 1)
                                    {
                                        //一般點燈
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, type, Year);       //購買人資料列表
                                        EndDate = "2025/10/17 23:59";
                                    }
                                    else if (type == 2)
                                    {
                                        //孝親祈福燈
                                        GetStateContentlist_ty(adminID, ApplicantID, kind, type, Year);       //購買人資料列表
                                        EndDate = "2025/05/08 23:59";
                                    }
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/10/31 23:59";
                                    break;
                                case 16:
                                    //東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetStateContentlist_dh(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/08/15 23:59";
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/10/31 23:59";
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetStateContentlist_ma(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/06/30 23:59";
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    title = "台灣道教總廟無極三清總道院";
                                    GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);       //購買人資料列表
                                    EndDate = "2025/03/15 23:59";
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
                            typeString = " 2024普度";

                            switch (adminID)
                            {
                                case 3:
                                    //大甲鎮瀾宮
                                    title = "大甲鎮瀾宮";
                                    GetStateContentlist_da(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/09 23:59";
                                    break;
                                case 4:
                                    //新港奉天宮
                                    title = "新港奉天宮";
                                    GetStateContentlist_h(adminID, ApplicantID, kind, Year);            //購買人資料列表
                                    EndDate = "2024/07/31 23:59";
                                    break;
                                case 6:
                                    //北港武德宮
                                    title = "北港武德宮";
                                    GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/11 23:59";
                                    break;
                                case 8:
                                    //西螺福興宮
                                    title = "西螺福興宮";
                                    GetStateContentlist_Fu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/20 23:59";
                                    break;
                                case 9:
                                    //桃園大廟景福宮
                                    title = "桃園大廟景福宮";
                                    //GetStateContentlist_Jing(adminID, ApplicantID);                     //購買人資料列表
                                    EndDate = "2023/08/25 23:59";
                                    break;
                                case 10:
                                    //台南正統鹿耳門聖母廟
                                    title = "台南正統鹿耳門聖母廟";
                                    GetStateContentlist_Luer(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    EndDate = "2024/08/15 23:59";
                                    break;
                                case 14:
                                    //桃園威天宮
                                    title = "桃園威天宮";
                                    GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                    EndDate = "2024/08/21 23:59";
                                    break;
                                case 15:
                                    //斗六五路財神宮
                                    title = "斗六五路財神宮";
                                    GetStateContentlist_Fw(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/29 23:59";
                                    break;
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";
                                    GetStateContentlist_dh(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/28 23:59";
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/05 23:59";
                                    break;
                                case 23:
                                    //玉敕大樹朝天宮
                                    title = "玉敕大樹朝天宮";
                                    GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2024/08/04 23:59";
                                    break;
                                case 30:
                                    //鎮瀾買足
                                    title = "大甲鎮瀾宮";
                                    GetStateContentlist_mazu(adminID, ApplicantID, kind, Year);         //購買人資料列表
                                    EndDate = "2024/08/15 23:59";
                                    break;
                                case 31:
                                    //台灣道教總廟無極三清總道院
                                    //title = "台灣道教總廟無極三清總道院";
                                    //GetStateContentlist_wjsan(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    //EndDate = "2024/08/04 23:59";
                                    break;
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
                            //        GetPurchaserlist_da(adminID, ApplicantID);          //購買人資料列表
                            //        Checkedtemple_da(adminID, ApplicantID, kind, Year);
                            //        EndDate = "2024/08/21 23:59";
                            //        break;
                            //}
                            break;
                        case 4:
                            //下元補庫
                            typeString = " 2024下元補庫";
                            Year = "2024";
                            title = "北港武德宮";
                            GetStateContentlist_wu(adminID, ApplicantID, kind, Year);           //購買人資料列表
                            EndDate = "2024/11/10 23:59";
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
                            //EndDate = "2023/11/20 23:59";
                            break;
                        case 7:
                            switch (adminID)
                            {
                                case 14:
                                    // 桃園威天宮天赦日補運
                                    typeString = " 2025天赦日招財補運";
                                    title = " 桃園威天宮";
                                    GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                                    EndDate = "2025/05/22 23:59";
                                    break;
                                case 23:
                                    // 玉敕大樹朝天宮天赦日招財補運
                                    typeString = " 2025天赦日招財補運";
                                    title = " 玉敕大樹朝天宮";
                                    GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                                    EndDate = "2025/05/22 23:59";
                                    break;
                            }
                            break;
                        case 8:
                            //進寶財神廟天赦日祭改
                            typeString = " 2024天赦日祭改";
                            title = " 進寶財神廟";
                            //GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                            EndDate = "2024/05/28 23:59";
                            break;
                        case 9:
                            //桃園威天宮關聖帝君聖誕
                            typeString = " 2024關聖帝君聖誕";
                            title = " 桃園威天宮";
                            GetStateContentlist_ty(adminID, ApplicantID, kind, 1, Year);           //購買人資料列表
                            EndDate = "2024/07/26 23:59";
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
                            typeString = " 2024靈寶禮斗";
                            title = " 玉敕大樹朝天宮";
                            GetStateContentlist_ma(adminID, ApplicantID, kind, Year);           //購買人資料列表
                            EndDate = "2024/10/20 23:59";
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
                            Year = "2024";

                            switch (adminID)
                            {
                                case 16:
                                    //台東東海龍門天聖宮
                                    title = "台東東海龍門天聖宮";    
                                    GetStateContentlist_dh(adminID, ApplicantID, kind, Year);   //購買人資料列表
                                    EndDate = "2024/12/18 23:59";
                                    //if (Request["fetsms"] != null)
                                    //{
                                    //    bindPayButton(true, false, false, false, false, false, false);
                                    //}
                                    //else
                                    //{
                                    //    bindPayButton(true, true, true, true, true, true, false);
                                    //}
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
                                    EndDate = "2026/01/23 23:59";
                                    break;
                                case 21:
                                    //鹿港城隍廟
                                    title = "鹿港城隍廟";
                                    GetStateContentlist_Lk(adminID, ApplicantID, kind, Year);          //購買人資料列表
                                    EndDate = "2025/04/09 23:59";
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
                                    EndDate = "2025/02/03 23:59";
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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            //OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());

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
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
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
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            //OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());

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
                        OrderPurchaser += OrderData("購買人信箱", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["Birth"].ToString() == "" ? "吉" : dtData.Rows[i]["Birth"].ToString());
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
                                OrderInfo += Ordertextarea(dtData.Rows[0]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
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
                            //string sex = "善男";
                            //if (dtData.Rows[i]["Sex"].ToString() == "F" || dtData.Rows[i]["Sex"].ToString() == "信女")
                            //{
                            //    sex = "信女";
                            //}
                            //OrderInfo += OrderData("性別", sex);
                            OrderInfo += OrderData("祈福人性別", dtData.Rows[i]["Sex"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            //OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
                    break;
                case 5:
                    //呈疏補庫
                    dtData = objLightDAC.GetAPPCharge_wu_Supplies2(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());

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
                    break;
                case 6:
                    //企業補財庫
                    dtData = objLightDAC.GetAPPCharge_wu_Supplies3(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", dtData.Rows[0]["AppcCreateDateString"].ToString());

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            //OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());

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
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppZipCode"].ToString() + " " + dtData.Rows[0]["AppAddress"].ToString());

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
        public void GetStateContentlist_Jing(int AdminID, int ApplicantID)
        {
            LightDAC objLightDAC = new LightDAC(this);

            DataTable dtData = objLightDAC.GetAPPCharge_Jing_Purdue(ApplicantID);

            if (dtData.Rows.Count > 0)
            {
                OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                    OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                    OrderInfo += "</div></div>";

                    //普度項目金額
                    int cost = GetpurdueCost(AdminID, purdueType, "");
                    OrderInfo += "<div>$ " + cost + "元</div>";
                    Total += cost;

                    OrderInfo += "</li>";
                }
            }
        }


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
                                OrderInfo += OrderData("飼主Email", dtData.Rows[i]["Email"].ToString());
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
                                OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                                OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            }

                            if (dtData.Rows[i]["Msg"].ToString() != "")
                            {
                                OrderInfo += OrderData("祈福小語", dtData.Rows[i]["Msg"].ToString());
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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
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
                            OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                                OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
                                OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                                OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
                        OrderPurchaser += OrderData("國曆生日", dtData.Rows[0]["AppBirth"].ToString() + (dtData.Rows[0]["AppLeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", (dtData.Rows[0]["AppZipCode"].ToString() != "" ? dtData.Rows[0]["AppZipCode"].ToString() + " " : "") + dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("國曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppAddress"].ToString());

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
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            int cost = GetSuppliesCost(AdminID, suppliesType);
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
                                OrderInfo += OrderData("飼主Email", dtData.Rows[i]["Email"].ToString());
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
                                OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                                OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
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
                            OrderInfo += OrderData("農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("農曆時辰", dtData.Rows[i]["BirthTime"].ToString() == "" ? "吉" : dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            if (count_rice != 0)
                            {
                                OrderInfo += OrderData("捐獻白米", dtData.Rows[i]["Count_rice"].ToString());
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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                    break;
                case 11:
                    //天貺納福添運法會
                    dtData = objLightDAC.GetAPPCharge_dh_Supplies(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());
                            OrderInfo += OrderData("祈福人國曆生日", dtData.Rows[i]["sBirth"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

                            //服務項目金額
                            int cost = 0;
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
                    break;
                case 15:
                    //護國息災梁皇大法會
                    dtData = objLightDAC.GetAPPCharge_dh_Lybc(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            int cost = GetLybcCost(lybcType, AdminID);
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
                        OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                        if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                        {
                            Total += 100;
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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                            OrderInfo += OrderData("祈福人信箱", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());
                            OrderInfo += OrderData("備註", TextToHtml(dtData.Rows[i]["Remark"].ToString()));

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
                        OrderPurchaser += OrderData("購買人Email", dtData.Rows[0]["AppEmail"].ToString());
                        OrderPurchaser += OrderData("購買人地址", (dtData.Rows[0]["AppZipCode"].ToString() != "" ? dtData.Rows[0]["AppZipCode"].ToString() + " " : "") + dtData.Rows[0]["AppAddress"].ToString());

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

                            OrderInfo += "</div></div>";

                            //服務項目金額
                            int cost = GetLingbaolidouCost(AdminID, lingbaolidouType);
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
                    dtData = objLightDAC.GetAPPCharge_mazu_Purdue(ApplicantID, Year);
                    if (dtData.Rows.Count > 0)
                    {
                        OrderStateContent = OrderState("付款時間", DateTime.Parse(dtData.Rows[0]["ChargeDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

                        OrderPurchaser = OrderData("購買人姓名", dtData.Rows[0]["AppName"].ToString());
                        OrderPurchaser += OrderData("購買人電話", dtData.Rows[0]["AppMobile"].ToString());
                        OrderPurchaser += OrderData("購買人地址", dtData.Rows[0]["AppZipCode"].ToString() + " " + dtData.Rows[0]["AppAddress"].ToString());

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

                            OrderInfo += OrderData("宮廟名稱", "大甲鎮瀾宮");
                            OrderInfo += OrderData("訂單編號", dtData.Rows[i]["Num2String"].ToString());
                            OrderInfo += OrderData("祈福人姓名", dtData.Rows[i]["Name"].ToString());
                            OrderInfo += OrderData("祈福人電話", dtData.Rows[i]["Mobile"].ToString());
                            OrderInfo += OrderData("祈福人農曆生日", dtData.Rows[i]["Birth"].ToString() + (dtData.Rows[i]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
                            OrderInfo += OrderData("祈福人農曆時辰", dtData.Rows[i]["BirthTime"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                        //OrderPurchaser += OrderData("贈品處理方式", dtData.Rows[0]["AppSendback"].ToString() == "Y" ? "寄回（運費+$100）" : "不寄回");

                        //if (dtData.Rows[0]["AppSendback"].ToString() == "Y")
                        //{
                        //    Total += 100;
                        //    OrderPurchaser += OrderData("收件人姓名", dtData.Rows[0]["ReceiptName"].ToString());
                        //    OrderPurchaser += OrderData("收件人電話", dtData.Rows[0]["ReceiptMobile"].ToString());
                        //    OrderPurchaser += OrderData("收件人地址", dtData.Rows[0]["ApprAddress"].ToString());
                        //}

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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
                        OrderPurchaser += OrderData("購買人信箱", dtData.Rows[0]["AppEmail"].ToString());

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
                            OrderInfo += OrderData("祈福人Email", dtData.Rows[i]["Email"].ToString());
                            OrderInfo += OrderData("祈福人市話", dtData.Rows[i]["HomeNum"].ToString());
                            OrderInfo += OrderData("祈福人地址", dtData.Rows[i]["Address"].ToString());

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