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
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.Temples
{
    public partial class templeInfo : AjaxBasePage
    {
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "getActivityTime");
        }

        public string ogurl = string.Empty;
        public string title = string.Empty;
        public string description = string.Empty;
        public string servicelist = string.Empty;
        public string imgURL = "images/temple/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["a"] != null)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                ogurl = "https://bobibobi.tw" + Request.RawUrl.ToString();

                string adminID = Request["a"].ToString();
                bool adminIDhaving = true;

                string lightsurl = "templeService_lights";
                string purdueurl = "templeService_purdue";
                string suppliesurl = "templeService_supplies";
                string supplies2url = "templeService_supplies2";
                string supplies3url = "templeService_supplies3";
                string lights2url = "templeService_marriagelights";
                switch (adminID)
                {
                    case "3":
                        //大甲鎮瀾宮
                        lightsurl += "_da.aspx";
                        purdueurl += "_da.aspx";
                        suppliesurl += "_da.aspx";
                        supplies2url += "_da.aspx";
                        break;
                    case "4":
                        //新港奉天宮
                        lightsurl += "_h.aspx";
                        purdueurl += "_h.aspx";
                        suppliesurl += "_h.aspx";
                        supplies2url += "_h.aspx";
                        break;
                    case "6":
                        //北港武德宮
                        lightsurl += "_wu.aspx";
                        purdueurl += "_wu.aspx";
                        suppliesurl += ".aspx";
                        supplies2url += ".aspx";
                        supplies3url += ".aspx";
                        break;
                    case "8":
                        //西螺福興宮
                        lightsurl += "_Fu.aspx";
                        purdueurl += "_Fu.aspx";
                        suppliesurl += "_Fu.aspx";
                        supplies2url += "_Fu.aspx";
                        break;
                    case "9":
                        //桃園大廟景福宮
                        lightsurl += "_Jing.aspx";
                        purdueurl += "_Jing.aspx";
                        suppliesurl += "_Jing.aspx";
                        supplies2url += "_Jing.aspx";
                        break;
                    case "10":
                        //台南正統鹿耳門聖母廟
                        lightsurl += "_Luer.aspx";
                        purdueurl += "_Luer.aspx";
                        suppliesurl += "_Luer.aspx";
                        supplies2url += "_Luer.aspx";
                        lights2url += "_Luer.aspx";
                        break;
                    case "14":
                        //桃園威天宮
                        lightsurl += "_ty.aspx";
                        purdueurl += "_ty.aspx";
                        suppliesurl += "_ty.aspx";
                        supplies2url += "_ty.aspx";
                        lights2url += "_ty.aspx";
                        break;
                    case "15":
                        //斗六五路財神宮
                        lightsurl += "_Fw.aspx";
                        purdueurl += "_Fw.aspx";
                        suppliesurl += "_Fw.aspx";
                        supplies2url += "_Fw.aspx";
                        lights2url += "_Fw.aspx";
                        break;
                    case "16":
                        //台東東海龍門天聖宮
                        lightsurl += "_dh.aspx";
                        purdueurl += "_dh.aspx";
                        suppliesurl += "_dh.aspx";
                        supplies2url += "_dh.aspx";
                        lights2url += "_dh.aspx";
                        break;
                    case "21":
                        //鹿港城隍廟
                        lightsurl += "_Lk.aspx";
                        purdueurl += "_Lk.aspx";
                        suppliesurl += "_Lk.aspx";
                        supplies2url += "_Lk.aspx";
                        lights2url += "_Lk.aspx";
                        break;
                    case "23":
                        //玉敕大樹朝天宮
                        lightsurl += "_ma.aspx";
                        purdueurl += "_ma.aspx";
                        break;
                    case "31":
                        //無極三清總道院
                        lightsurl += "_wjsan.aspx";
                        purdueurl += "_wjsan.aspx";
                        break;
                    default:
                        adminIDhaving = false;
                        int a = 0;
                        int.TryParse(adminID, out a);
                        if (a == 0)
                        {
                            adminID = "0";
                        }
                        break;
                }

                if (adminID != "0" && adminIDhaving)
                {
                    AdminDAC objAdminDAC = new AdminDAC(this);
                    DataTable dtTempleInfo = objAdminDAC.GetTempleInfo(adminID);

                    if (dtTempleInfo.Rows.Count > 0)
                    {
                        title = dtTempleInfo.Rows[0]["Name"].ToString();
                        imgURL = dtTempleInfo.Rows[0]["OriginalImageAddress"].ToString();
                        description = dtTempleInfo.Rows[0]["Content1"].ToString();
                        string lightsService = dtTempleInfo.Rows[0]["LightsService"].ToString();
                        string purdueService = dtTempleInfo.Rows[0]["PurdueService"].ToString();
                        string suppliesService = dtTempleInfo.Rows[0]["SuppliesService"].ToString();
                        string supplies2Service = dtTempleInfo.Rows[0]["Supplies2Service"].ToString();
                        string supplies3Service = dtTempleInfo.Rows[0]["Supplies3Service"].ToString();
                        string lights2Service = dtTempleInfo.Rows[0]["Lights2Service"].ToString();

                        if (lightsService == "1")
                        {
                            //servicelist += "<li><a href=\"" + lightsurl + "\" title=\"祈福點燈\"><span>祈福點燈</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(1, " + adminID + ")\" href = \"javascript: void(0)\" title=\"祈福點燈\"><span>祈福點燈</span></a></li>";
                            
                        }

                        if (purdueService == "1")
                        {
                            //servicelist += "<li><a href=\"" + purdueurl + "\" title=\"中元普渡\"><span>中元普渡</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(2, " + adminID + ")\" href = \"javascript: void(0)\" title=\"中元普渡\"><span>中元普渡</span></a></li>";
                        }

                        if (suppliesService == "1")
                        {
                            //servicelist += "<li><a href=\"" + suppliesurl + "\" title=\"下元補庫\"><span>下元補庫</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(4, " + adminID + ")\" href = \"javascript: void(0)\" title=\"下元補庫\"><span>下元補庫</span></a></li>";
                        }

                        if (supplies2Service == "1")
                        {
                            //servicelist += "<li><a href=\"" + supplies2url + "\" title=\"呈疏補庫\"><span>呈疏補庫</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(5, " + adminID + ")\" href = \"javascript: void(0)\" title=\"呈疏補庫\"><span>呈疏補庫</span></a></li>";
                        }

                        if (supplies3Service == "1")
                        {
                            //servicelist += "<li><a href=\"" + supplies3url + "\" title=\"企業補財庫\"><span>企業補財庫</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(6, " + adminID + ")\" href = \"javascript: void(0)\" title=\"企業補財庫\"><span>企業補財庫</span></a></li>";
                        }

                        if (adminID == "3")
                        {
                            //servicelist += "<li><a href=\"" + supplies3url + "\" title=\"企業補財庫\"><span>企業補財庫</span></a></li>";
                            servicelist += "<li><a onclick = \"ActivityTime(13, " + adminID + ")\" href = \"javascript: void(0)\" title=\"大甲鎮瀾宮重修慶成祈安七朝清醮\"><span>七朝清醮</span></a></li>";
                        }

                        //if (lights2Service == "1")
                        //{
                        //    //servicelist += "<li><a href=\"" + lights2url + "\" title=\"月老姻緣燈\"><span>月老姻緣燈</span></a></li>";
                        //    servicelist += "<li><a onclick = \"ActivityTime(7, " + adminID + ")\" href = \"javascript: void(0)\" title=\"月老姻緣燈\"><span>月老姻緣燈</span></a></li>";
                        //}

                    }
                }
                else
                {
                    Response.Write("<script>alert('訪問網址錯誤，請重新進入。');location='https://bobibobi.tw/Temples/temple.aspx'</script>");
                }

                //switch (adminID)
                //{
                //    case "3":
                //        //大甲鎮瀾宮
                //        title = "大甲鎮瀾宮";
                //        imgURL += "sample.jpg";
                //        description = "大甲鎮瀾宮自古以來即是台灣媽祖信仰的重鎮，不僅是人民團結互助的所在，更是信仰、文化及政經的中心，具有慰藉民眾心靈、安定社會的功能；因此長期以來，本宮全體董監事均秉持著「取諸社會、回饋社會」之理念，關懷社會、熱心公益，每年參與推動各類社會公益及文化藝術活動。";

                //        servicelist = "<li><a href=\"templeService_lights_da.aspx?t=3\" title=\"祈福點燈\"><span>祈福點燈</span></a></li>";
                //        servicelist += "<li><a href=\"templeService_purdue_da.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";

                //        break;
                //    case "4":
                //        //新港奉天宮
                //        title = "新港奉天宮";
                //        imgURL += "sample_h.jpg";
                //        description = "座落於新港街市中心，新民路與中山路交會的丁字路口。主神恭奉天上聖母，為先民於明朝天啟2年（1622年）恭請「船仔媽」來台，神示永駐笨港。\r\n清康熙39年(1700年)，笨港居民合建天妃廟。\r\n嘉慶初年烏水氾濫，敬遷蔴園寮，經水師提督王得祿捐奉倡建，紳商居民鳩資合建，於嘉慶16年（1811年）奉天宮竣工落成，紹承古笨港歷史遺續。\r\n奉天宮為一座歷史悠久，古蹟紛陳，除了珍貴交趾陶，廟裡保留歷代文物與珍貴的民間信仰文化資產。為當地居民信仰中心，香火鼎盛，分靈遍佈全球，每年數百萬香客進香，歡迎全國善信蒞宮參拜，媽祖婆保佑國泰民安，事事如意。";

                //        servicelist = "<li><a href=\"templeService_lights_h.aspx?t=3\" title=\"祈福點燈\"><span>祈福點燈</span></a></li>";
                //        servicelist += "<li><a href=\"templeService_purdue_h.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";

                //        break;
                //    case "6":
                //        //北港武德宮
                //        title = "北港武德宮";
                //        imgURL += "sample_wu.jpg";
                //        description = "台灣五路武財神的信仰始於北港，而傳奇的開始，則在北港的中山路上；北港與目前新港大部舊時合稱笨港，開埠可追溯至四百多年前顏思齊率部屯墾開始，後來因著地利逐漸演變為中部貨運吞吐的商港，在北港溪未嚴重淤積前可說是商賈雲集，舊時還有一府二笨的說法。而中山路則位居北港最繁華的區段，發展也最早。";

                //        servicelist = "<li><a href=\"templeService_lights_wu.aspx?t=3\" title=\"祈福點燈\"><span>祈福點燈</span></a></li>";
                //        servicelist += "<li><a href=\"templeService_purdue_wu.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";
                //        servicelist += "<li><a href=\"templeService_supplies.aspx?a=6\" title=\"補財庫\"><span>補財庫</span></a></li>";

                //        break;
                //    case "8":
                //        //西螺福興宮
                //        title = "西螺福興宮";
                //        imgURL += "sample_Fu.jpg";
                //        description = "福興宮，主祀天上聖母，居民稱「媽祖宮」，亦稱「西螺媽祖廟」，由來久矣。康熙五十六年（1717）福建湧泉寺明海禪師及業戶鄭時敏，同將隨身所奉的媽祖安茅奉祀於螺陽建立草廟，因媽祖顯聖庇民香火鼎盛，於雍正元年（1723）西螺商人及居民共同集資興修福興宮。";

                //        servicelist = "<li><a href=\"templeService_purdue_Fu.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";

                //        break;
                //    case "9":
                //        //桃園大廟景福宮
                //        title = "桃園大廟景福宮";
                //        imgURL += "sample_Jing.jpg";
                //        description = "桃園景福宮為台灣開漳聖王信仰一大重鎮，位於桃園市中心，香火鼎盛，所在之地自清初以來即是全台河洛漳州移民聚居密度最高之處。據考證與文獻記載，初建於清乾隆十年（一七四五），而有香火祭祀與開基神像之年代更早於此前。本地於康熙年間即有漳州南靖吳姓、龍溪謝姓等拓墾，續又有漳州龍溪郭光天宗族及漳浦陳華壇宗族自漳州原鄉迎來香火與開基神像至此，且有文獻記載及家族傳述歷歷可證，是故較可確信本廟為桃園地區最早之開漳聖王廟，亦是北台最早跨縣市區域之開漳信仰中心。";

                //        servicelist = "<li><a href=\"templeService_purdue_Jing.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";

                //        break;
                //    case "10":
                //        //台南正統鹿耳門聖母廟
                //        title = "台南正統鹿耳門聖母廟";
                //        imgURL += "sample_Luer.jpg";
                //        description = "「正統鹿耳門聖母廟」⚜️為臺灣少數歷史傳承超過四百年的媽祖廟，相傳於宋代漁民往返福建、台灣之際，位於台江內海中北汕尾沙洲北側的「鹿耳門」已建起小廟奉祀「鹿耳門媽祖」。";

                //        servicelist = "<li><a href=\"templeService_purdue_Luer.aspx\" title=\"中元普渡\"><span>中元普渡</span></a></li>";

                //        break;
                //}
            }
        }

        public class AjaxPageHandler
        {
            string[] purdue_da = { "大甲鎮瀾宮", "2024/06/24 00:00", "2024/08/07 23:59", "templeService_purdue_da.aspx" };
            string[] purdue_h = { "新港奉天宮", "2024/06/24 00:00", "2024/07/31 23:59", "templeService_purdue_h.aspx" };
            string[] purdue_wu = { "北港武德宮", "2024/06/24 00:00", "2024/08/11 23:59", "templeService_purdue_wu.aspx" };
            string[] purdue_Fu = { "西螺福興宮", "2024/06/24 00:00", "2024/08/20 23:59", "templeService_purdue_Fu.aspx" };
            string[] purdue_Jing = { "桃園大廟景福宮", "2023/06/24 00:00", "2023/08/21 23:59", "templeService_purdue_Jing.aspx" };
            string[] purdue_Luer = { "台南正統鹿耳門聖母廟", "2024/06/24 00:00", "2024/08/11 23:59", "templeService_purdue_Luer.aspx" };
            string[] purdue_ty = { "桃園威天宮", "2024/06/28 00:00", "2024/08/21 23:59", "templeService_purdue_ty.aspx" };
            string[] purdue_Fw = { "斗六五路財神宮", "2024/06/24 00:00", "2024/08/29 23:59", "templeService_purdue_Fw.aspx" };
            string[] purdue_dh = { "台東東海龍門天聖宮", "2024/06/24 00:00", "2024/08/28 23:59", "templeService_purdue_dh.aspx" };
            string[] purdue_Lk = { "鹿港城隍廟", "2024/06/28 00:00", "2024/08/05 23:59", "templeService_purdue_Lk.aspx" };
            string[] purdue_ma = { "玉敕大樹朝天宮", "2024/06/24 00:00", "2024/08/04 23:59", "templeService_purdue_ma.aspx" };
            string[] purdue_wjsan = { "台灣道教總廟無極三清總道院", "2024/06/24 00:00", "2024/08/04 23:59", "templeService_purdue_wjsan.aspx" };

            string[] lights_da = { "大甲鎮瀾宮", "2024/11/01 00:00", "2025/06/30 23:59", "templeService_lights_da.aspx" };
            string[] lights_h = { "新港奉天宮", "2023/12/22 00:00", "2024/10/31 23:59", "templeService_lights_h.aspx" };
            string[] lights_wu = { "北港武德宮", "2024/11/01 00:00", "2025/02/04 23:59", "templeService_lights_wu.aspx" };
            string[] lights_Fu = { "西螺福興宮", "2023/12/22 00:00", "2024/10/31 23:59", "templeService_lights_Fu.aspx" };
            string[] lights_Luer = { "台南正統鹿耳門聖母廟", "2023/12/22 00:00", "2024/10/31 23:59", "templeService_lights_Luer.aspx" };
            string[] lights_ty = { "桃園威天宮", "2024/11/01 00:00", "2024/10/17 23:59", "templeService_lights_ty.aspx" };
            string[] lights_Fw = { "斗六五路財神宮", "2023/12/22 00:00", "2024/10/31 23:59", "templeService_lights_Fw.aspx" };
            string[] lights_dh = { "台東東海龍門天聖宮", "2024/11/01 00:00", "2024/10/17 23:59", "templeService_lights_dh.aspx" };
            string[] lights_Lk = { "鹿港城隍廟", "2023/12/22 00:00", "2024/10/31 23:59", "templeService_lights_Lk.aspx" };
            string[] lights_ma = { "玉敕大樹朝天宮", "2024/11/01 00:00", "2025/02/01 23:59", "templeService_lights_ma.aspx" };
            string[] lights_wjsan = { "台灣道教總廟無極三清總道院", "2024/11/01 00:00", "2025/03/15 23:59", "templeService_lights_wjsan.aspx" };

            string[] supplies_wu = { "北港武德宮", "2023/10/15 00:00", "2023/11/21 23:59", "templeService_supplies.aspx" };
            string[] supplies_wu2 = { "北港武德宮", "2024/03/12 00:00", "2024/04/17 23:59", "templeService_supplies2.aspx" };
            string[] supplies_wu3 = { "北港武德宮", "0", "0", "templeService_supplies3.aspx" };
            string[] supplies_ty = { "桃園威天宮", "2024/05/18 00:00", "2024/05/28 23:59", "templeService_supplies_ty.aspx" };

            public void getActivityTime(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                if (basePage.Request["kind"] != null && basePage.Request["admin"] != null)
                {
                    string kind = basePage.Request["kind"];
                    string adminID = basePage.Request["admin"];
                    string name = string.Empty;
                    string startDate = string.Empty;
                    string endDate = string.Empty;
                    string Url = string.Empty;
                    string service = string.Empty;

                    switch (kind)
                    {
                        case "1":
                            //點燈
                            service = "祈福點燈";
                            switch (adminID)
                            {
                                case "3":
                                    //大甲鎮瀾宮
                                    name = lights_da[0];
                                    startDate = lights_da[1];
                                    endDate = lights_da[2];
                                    Url = lights_da[3];
                                    break;
                                case "4":
                                    //新港奉天宮
                                    name = lights_da[0];
                                    startDate = lights_h[1];
                                    endDate = lights_h[2];
                                    Url = lights_h[3];
                                    break;
                                case "6":
                                    //北港武德宮
                                    name = lights_wu[0];
                                    startDate = lights_wu[1];
                                    endDate = lights_wu[2];
                                    Url = lights_wu[3];
                                    break;
                                case "8":
                                    //西螺福興宮
                                    name = lights_Fu[0];
                                    startDate = lights_Fu[1];
                                    endDate = lights_Fu[2];
                                    Url = lights_Fu[3];
                                    break;
                                case "10":
                                    //台南正統鹿耳門聖母廟
                                    name = lights_Luer[0];
                                    startDate = lights_Luer[1];
                                    endDate = lights_Luer[2];
                                    Url = lights_Luer[3];
                                    break;
                                case "14":
                                    //桃園威天宮
                                    name = lights_ty[0];
                                    startDate = lights_ty[1];
                                    endDate = lights_ty[2];
                                    Url = lights_ty[3];
                                    break;
                                case "15":
                                    //斗六五路財神宮
                                    name = lights_Fw[0];
                                    startDate = lights_Fw[1];
                                    endDate = lights_Fw[2];
                                    Url = lights_Fw[3];
                                    break;
                                case "16":
                                    //台東東海龍門天聖宮
                                    name = lights_dh[0];
                                    startDate = lights_dh[1];
                                    endDate = lights_dh[2];
                                    Url = lights_dh[3];
                                    break;
                                case "21":
                                    //鹿港城隍廟
                                    name = lights_Lk[0];
                                    startDate = lights_Lk[1];
                                    endDate = lights_Lk[2];
                                    Url = lights_Lk[3];
                                    break;
                                case "23":
                                    //玉敕大樹朝天宮
                                    name = lights_ma[0];
                                    startDate = lights_ma[1];
                                    endDate = lights_ma[2];
                                    Url = lights_ma[3];
                                    break;
                                case "31":
                                    //無極三清總道院
                                    name = lights_wjsan[0];
                                    startDate = lights_wjsan[1];
                                    endDate = lights_wjsan[2];
                                    Url = lights_wjsan[3];
                                    break;
                            }
                            break;
                        case "2":
                            //普度
                            service = "中元普度";
                            switch (adminID)
                            {
                                case "3":
                                    //大甲鎮瀾宮
                                    name = purdue_da[0];
                                    startDate = purdue_da[1];
                                    endDate = purdue_da[2];
                                    Url = purdue_da[3];
                                    break;
                                case "4":
                                    //新港奉天宮
                                    name = purdue_h[0];
                                    startDate = purdue_h[1];
                                    endDate = purdue_h[2];
                                    Url = purdue_h[3];
                                    break;
                                case "6":
                                    //北港武德宮
                                    name = purdue_wu[0];
                                    startDate = purdue_wu[1];
                                    endDate = purdue_wu[2];
                                    Url = purdue_wu[3];
                                    break;
                                case "8":
                                    //西螺福興宮
                                    name = purdue_Fu[0];
                                    startDate = purdue_Fu[1];
                                    endDate = purdue_Fu[2];
                                    Url = purdue_Fu[3];
                                    break;
                                case "9":
                                    //桃園大廟景福宮
                                    name = purdue_Jing[0];
                                    startDate = purdue_Jing[1];
                                    endDate = purdue_Jing[2];
                                    Url = purdue_Jing[3];
                                    break;
                                case "10":
                                    //台南正統鹿耳門聖母廟
                                    name = purdue_Luer[0];
                                    startDate = purdue_Luer[1];
                                    endDate = purdue_Luer[2];
                                    Url = purdue_Luer[3];
                                    break;
                                case "14":
                                    //桃園威天宮
                                    name = purdue_ty[0];
                                    startDate = purdue_ty[1];
                                    endDate = purdue_ty[2];
                                    Url = purdue_ty[3];
                                    break;
                                case "15":
                                    //斗六五路財神宮
                                    name = purdue_Fw[0];
                                    startDate = purdue_Fw[1];
                                    endDate = purdue_Fw[2];
                                    Url = purdue_Fw[3];
                                    break;
                                case "16":
                                    //台東東海龍門天聖宮
                                    name = purdue_dh[0];
                                    startDate = purdue_dh[1];
                                    endDate = purdue_dh[2];
                                    Url = purdue_dh[3];
                                    break;
                                case "21":
                                    //鹿港城隍廟
                                    name = purdue_Lk[0];
                                    startDate = purdue_Lk[1];
                                    endDate = purdue_Lk[2];
                                    Url = purdue_Lk[3];
                                    break;
                                case "23":
                                    //玉敕大樹朝天宮
                                    name = purdue_ma[0];
                                    startDate = purdue_ma[1];
                                    endDate = purdue_ma[2];
                                    Url = purdue_ma[3];
                                    break;
                                //case "31":
                                //    //無極三清總道院
                                //    name = purdue_wjsan[0];
                                //    startDate = purdue_wjsan[1];
                                //    endDate = purdue_wjsan[2];
                                //    Url = purdue_wjsan[3];
                                //    break;
                            }
                            break;
                        case "3":
                            //商品小舖
                            service = "商品小舖";
                            break;
                        case "4":
                            //下元補庫
                            service = "下元補庫";
                            switch (adminID)
                            {
                                case "6":
                                    //北港武德宮
                                    name = supplies_wu[0];
                                    startDate = supplies_wu[1];
                                    endDate = supplies_wu[2];
                                    Url = supplies_wu[3];
                                    break;
                            }
                            break;
                        case "5":
                            //呈疏補庫
                            service = "呈疏補庫";
                            switch (adminID)
                            {
                                case "6":
                                    //北港武德宮
                                    name = supplies_wu2[0];
                                    startDate = supplies_wu2[1];
                                    endDate = supplies_wu2[2];
                                    Url = supplies_wu2[3];
                                    break;
                            }
                            break;
                        case "6":
                            //企業補財庫
                            service = "企業補財庫";
                            switch (adminID)
                            {
                                case "6":
                                    //北港武德宮
                                    name = supplies_wu3[0];
                                    startDate = supplies_wu3[1];
                                    endDate = supplies_wu3[2];
                                    Url = supplies_wu3[3];
                                    break;
                            }
                            break;
                        case "7":
                            //天赦日補運
                            service = "天赦日補運";
                            switch (adminID)
                            {
                                case "14":
                                    //桃園威天宮
                                    name = supplies_ty[0];
                                    startDate = supplies_ty[1];
                                    endDate = supplies_ty[2];
                                    Url = supplies_ty[3];
                                    break;
                            }
                            break;
                        case "13":
                            //七朝清醮
                            service = "重修慶成祈安七朝清醮";
                            switch (adminID)
                            {
                                case "3":
                                    //大甲鎮瀾宮
                                    name = "大甲鎮瀾宮";
                                    startDate = "2024/09/23 00:00";
                                    endDate = "2024/11/30 23:59";
                                    Url = "templeService_TaoistJiaoCeremony_da.aspx";
                                    break;
                            }
                            break;
                    }

                    if (name != "" && startDate != "" && endDate != "")
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);

                        basePage.mJSonHelper.AddContent("Name", name);
                        basePage.mJSonHelper.AddContent("StartDate", startDate);
                        basePage.mJSonHelper.AddContent("EndDate", endDate);
                        basePage.mJSonHelper.AddContent("Service", service);
                        basePage.mJSonHelper.AddContent("URL", Url);
                    }
                }
            }
        }
    }
}