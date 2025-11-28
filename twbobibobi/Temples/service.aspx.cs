using twbobibobi.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;

namespace Temple.Temples
{
    public partial class service : AjaxBasePage
    {
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "getActivityTime");
            AddAjaxHandler(typeof(AjaxPageHandler), "getActivityTimeBatch");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public class AjaxPageHandler
        {
            string[] purdue_da = { "大甲鎮瀾宮", "2025/07/01 00:00", "2025/08/27 23:59", "templeService_purdue_da.aspx" };
            string[] purdue_h = { "新港奉天宮", "2025/07/01 00:00", "2025/08/15 23:59", "templeService_purdue_h.aspx" };
            string[] purdue_wu = { "北港武德宮", "2025/07/01 00:00", "2025/08/23 23:59", "templeService_purdue_wu.aspx" };
            string[] purdue_Fu = { "西螺福興宮", "2025/07/01 00:00", "2025/09/09 23:59", "templeService_purdue_Fu.aspx" };
            string[] purdue_Jing = { "桃園大廟景福宮", "2023/06/24 00:00", "2023/08/21 23:59", "templeService_purdue_Jing.aspx" };
            string[] purdue_Luer = { "台南正統鹿耳門聖母廟", "2025/07/01 00:00", "2025/08/28 23:59", "templeService_purdue_Luer.aspx" };
            string[] purdue_ty = { "桃園威天宮", "2025/07/01 00:00", "2025/09/09 23:59", "templeService_purdue_ty.aspx" };
            string[] purdue_Fw = { "斗六五路財神宮", "2025/07/01 00:00", "2025/09/14 23:59", "templeService_purdue_Fw.aspx" };
            string[] purdue_dh = { "台東東海龍門天聖宮", "2025/07/01 00:00", "2025/09/19 23:59", "templeService_purdue_dh.aspx" };
            string[] purdue_Lk = { "鹿港城隍廟", "2025/07/01 00:00", "2025/08/21 23:59", "templeService_purdue_Lk.aspx" };
            string[] purdue_ma = { "玉敕大樹朝天宮", "2025/07/01 00:00", "2025/08/25 23:59", "templeService_purdue_ma.aspx" };
            string[] purdue_wjsan = { "台灣道教總廟無極三清總道院", "2025/07/01 00:00", "2025/09/17 23:59", "templeService_purdue_wjsan.aspx" };

            string[] lights_da = { "大甲鎮瀾宮", "2025/11/01 00:00", "2026/02/08 23:59", "templeService_lights_da.aspx" };
            string[] lights_h = { "新港奉天宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_h.aspx" };
            string[] lights_wu = { "北港武德宮", "2025/11/01 00:00", "2026/01/31 23:59", "templeService_lights_wu.aspx" };
            string[] lights_Fu = { "西螺福興宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_Fu.aspx" };
            string[] lights_Luer = { "台南正統鹿耳門聖母廟", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_Luer.aspx" };
            string[] lights_ty = { "桃園威天宮", "2025/11/01 00:00", "2026/09/15 23:59", "templeService_lights_ty.aspx" };
            string[] lights_Fw = { "斗六五路財神宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_Fw.aspx" };
            string[] lights_dh = { "台東東海龍門天聖宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_dh.aspx" };
            string[] lights_Lk = { "鹿港城隍廟", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_Lk.aspx" };
            string[] lights_ma = { "玉敕大樹朝天宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_ma.aspx" };
            string[] lights_wjsan = { "台灣道教總廟無極三清總道院", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_wjsan.aspx" };
            string[] lights_ld = { "桃園龍德宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_ld.aspx" };
            string[] lights_st = { "松柏嶺受天宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_st.aspx" };
            string[] lights_bj = { "池上北極玄天宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_bj.aspx" };
            string[] lights_sbbt = { "花蓮慈惠石壁部堂", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_sbbt.aspx" };
            string[] lights_bpy = { "新北真武山受玄宮", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_bpy.aspx" };
            string[] lights_ssy = { "桃園壽山巖觀音寺", "2025/11/01 00:00", "2026/10/31 23:59", "templeService_lights_ssy.aspx" };

            string[] supplies_wu = { "北港武德宮", "2025/10/09 00:00", "2025/11/26 23:59", "templeService_supplies.aspx" };
            string[] supplies_wu2 = { "北港武德宮", "2025/03/07 00:00", "2025/03/24 23:59", "templeService_supplies2.aspx" };
            string[] supplies_wu3 = { "北港武德宮", "0", "0", "templeService_supplies3.aspx" };
            string[] supplies_ty = { "桃園威天宮", "2025/10/27", "2025/12/18", "templeService_supplies_ty.aspx" };
            string[] supplies_Fw = { "斗六五路財神宮", "0", "0", "templeService_supplies_Fw.aspx" };
            string[] supplies_Lk = { "鹿港城隍廟", "2025/03/06 00:00", "2025/06/25 23:59", "templeService_supplies_Lk.aspx" };
            string[] supplies_ma = { "玉敕大樹朝天宮", "2025/10/07 00:00", "2025/12/20 23:59", "templeService_supplies_ma.aspx" };
            string[] supplies_sx2 = { "神霄玉府財神會館", "0", "0", "templeService_supplies2_sx.aspx" };
            string[] huaguo_wjsan = { "台灣道教總廟無極三清總道院", "0", "0", "templeService_huaguo_wjsan.aspx" };
            string[] blessing_st = { "松柏嶺受天宮", "0", "0", "templeService_blessing_st.aspx" };

            /// <summary>
            /// 取得活動的起訖時間
            /// </summary>
            /// <param name="basePage">基底頁面物件，包含 Request 與 JSON Helper</param>
            public void getActivityTime(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                if (basePage.Request["kind"] != null && basePage.Request["a"] != null)
                {
                    string kind = basePage.Request["kind"];
                    string adminID = basePage.Request["a"];
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
                                    //台灣道教總廟無極三清總道院
                                    name = lights_wjsan[0];
                                    startDate = lights_wjsan[1];
                                    endDate = lights_wjsan[2];
                                    Url = lights_wjsan[3];
                                    break;
                                //case "32":
                                //    //桃園龍德宮
                                //    name = lights_ld[0];
                                //    startDate = lights_ld[1];
                                //    endDate = lights_ld[2];
                                //    Url = lights_ld[3];
                                //    break;
                                case "35":
                                    //松柏嶺受天宮
                                    name = lights_st[0];
                                    startDate = lights_st[1];
                                    endDate = lights_st[2];
                                    Url = lights_st[3];
                                    break;
                                case "38":
                                    //池上北極玄天宮
                                    name = lights_bj[0];
                                    startDate = lights_bj[1];
                                    endDate = lights_bj[2];
                                    Url = lights_bj[3];
                                    break;
                                case "39":
                                    //花蓮慈惠石壁部堂
                                    name = lights_sbbt[0];
                                    startDate = lights_sbbt[1];
                                    endDate = lights_sbbt[2];
                                    Url = lights_sbbt[3];
                                    break;
                                case "40":
                                    //新北真武山受玄宮
                                    name = lights_bpy[0];
                                    startDate = lights_bpy[1];
                                    endDate = lights_bpy[2];
                                    Url = lights_bpy[3];
                                    break;
                                case "41":
                                    //桃園壽山巖觀音寺
                                    name = lights_ssy[0];
                                    startDate = lights_ssy[1];
                                    endDate = lights_ssy[2];
                                    Url = lights_ssy[3];
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
                                case "31":
                                    //無極三清總道院
                                    name = purdue_wjsan[0];
                                    startDate = purdue_wjsan[1];
                                    endDate = purdue_wjsan[2];
                                    Url = purdue_wjsan[3];
                                    break;
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
                            service = "天赦日招財補運";
                            switch (adminID)
                            {
                                case "14":
                                    //桃園威天宮
                                    name = supplies_ty[0];
                                    startDate = supplies_ty[1];
                                    endDate = supplies_ty[2];
                                    Url = supplies_ty[3];
                                    break;
                                case "23":
                                    //玉敕大樹朝天宮
                                    name = supplies_ma[0];
                                    startDate = supplies_ma[1];
                                    endDate = supplies_ma[2];
                                    Url = supplies_ma[3];
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
                        case "16":
                            //補財庫
                            service = "補財庫";
                            switch (adminID)
                            {
                                case "15":
                                    //斗六五路財神宮
                                    name = supplies_Fw[0];
                                    startDate = supplies_Fw[1];
                                    endDate = supplies_Fw[2];
                                    Url = supplies_Fw[3];
                                    break;
                                case "21":
                                    //鹿港城隍廟
                                    name = supplies_Lk[0];
                                    startDate = supplies_Lk[1];
                                    endDate = supplies_Lk[2];
                                    Url = supplies_Lk[3];
                                    break;
                            }
                            break;
                        case "19":
                            //供香轉運
                            service = "供香轉運";
                            switch (adminID)
                            {
                                case "33":
                                    //神霄玉府財神會館
                                    name = supplies_sx2[0];
                                    startDate = supplies_sx2[1];
                                    endDate = supplies_sx2[2];
                                    Url = supplies_sx2[3];
                                    break;
                            }
                            break;
                        case "21":
                            //供花供果
                            service = "供花供果";
                            switch (adminID)
                            {
                                case "31":
                                    //台灣道教總廟無極三清總道院
                                    name = huaguo_wjsan[0];
                                    startDate = huaguo_wjsan[1];
                                    endDate = huaguo_wjsan[2];
                                    Url = huaguo_wjsan[3];
                                    break;
                            }
                            break;
                        case "23":
                            //祈安植福
                            service = "祈安植福";
                            switch (adminID)
                            {
                                case "35":
                                    //松柏嶺受天宮
                                    name = blessing_st[0];
                                    startDate = blessing_st[1];
                                    endDate = blessing_st[2];
                                    Url = blessing_st[3];
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

            /// <summary>
            /// 批次取得多筆活動的起訖時間
            /// </summary>
            /// <param name="basePage">基底頁面物件，包含 Request 與 JSON Helper</param>
            public void getActivityTimeBatch(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);

                // 準備輸出用 DataTable，因為你原本 JSONHelper 有 AddDataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Kind", typeof(string));
                dt.Columns.Add("AdminID", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("StartDate", typeof(string));
                dt.Columns.Add("EndDate", typeof(string));
                dt.Columns.Add("Service", typeof(string));
                dt.Columns.Add("URL", typeof(string));

                // 取得 Request["list"]，假設是 JSON，例如: [{kind:1,a:3},{kind:2,a:4}]
                using (var reader = new StreamReader(basePage.Request.InputStream))
                {
                    string body = reader.ReadToEnd();
                    // body 會是 {"list":[{"Kind":1,"A":3},{"Kind":2,"A":4}]}

                    if (!string.IsNullOrEmpty(body))
                    {
                        // 反序列化成物件清單
                        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ActivityRequestWrapper>(body);
                        var reqList = obj.list;

                        foreach (var req in reqList)
                        {
                            string kind = req.Kind.ToString();
                            string adminID = req.A.ToString();
                            string name = string.Empty;
                            string startDate = string.Empty;
                            string endDate = string.Empty;
                            string Url = string.Empty;
                            string service = string.Empty;

                            // --- 套用你原本的方法邏輯 ---
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
                                            //台灣道教總廟無極三清總道院
                                            name = lights_wjsan[0];
                                            startDate = lights_wjsan[1];
                                            endDate = lights_wjsan[2];
                                            Url = lights_wjsan[3];
                                            break;
                                        case "35":
                                            //松柏嶺受天宮
                                            name = lights_st[0];
                                            startDate = lights_st[1];
                                            endDate = lights_st[2];
                                            Url = lights_st[3];
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
                                        case "31":
                                            //無極三清總道院
                                            name = purdue_wjsan[0];
                                            startDate = purdue_wjsan[1];
                                            endDate = purdue_wjsan[2];
                                            Url = purdue_wjsan[3];
                                            break;
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
                                    service = "天赦日招財補運";
                                    switch (adminID)
                                    {
                                        case "14":
                                            //桃園威天宮
                                            name = supplies_ty[0];
                                            startDate = supplies_ty[1];
                                            endDate = supplies_ty[2];
                                            Url = supplies_ty[3];
                                            break;
                                        case "23":
                                            //玉敕大樹朝天宮
                                            name = supplies_ma[0];
                                            startDate = supplies_ma[1];
                                            endDate = supplies_ma[2];
                                            Url = supplies_ma[3];
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
                                case "16":
                                    //補財庫
                                    service = "補財庫";
                                    switch (adminID)
                                    {
                                        case "15":
                                            //斗六五路財神宮
                                            name = supplies_Fw[0];
                                            startDate = supplies_Fw[1];
                                            endDate = supplies_Fw[2];
                                            Url = supplies_Fw[3];
                                            break;
                                        case "21":
                                            //鹿港城隍廟
                                            name = supplies_Lk[0];
                                            startDate = supplies_Lk[1];
                                            endDate = supplies_Lk[2];
                                            Url = supplies_Lk[3];
                                            break;
                                    }
                                    break;
                                case "19":
                                    //供香轉運
                                    service = "供香轉運";
                                    switch (adminID)
                                    {
                                        case "33":
                                            //神霄玉府財神會館
                                            name = supplies_sx2[0];
                                            startDate = supplies_sx2[1];
                                            endDate = supplies_sx2[2];
                                            Url = supplies_sx2[3];
                                            break;
                                    }
                                    break;
                                case "21":
                                    //供花供果
                                    service = "供花供果";
                                    switch (adminID)
                                    {
                                        case "31":
                                            //台灣道教總廟無極三清總道院
                                            name = huaguo_wjsan[0];
                                            startDate = huaguo_wjsan[1];
                                            endDate = huaguo_wjsan[2];
                                            Url = huaguo_wjsan[3];
                                            break;
                                    }
                                    break;
                                case "23":
                                    //祈安植福
                                    service = "祈安植福";
                                    switch (adminID)
                                    {
                                        case "35":
                                            //松柏嶺受天宮
                                            name = blessing_st[0];
                                            startDate = blessing_st[1];
                                            endDate = blessing_st[2];
                                            Url = blessing_st[3];
                                            break;
                                    }
                                    break;
                            }

                            // 如果有找到資料，就塞進 DataTable
                            if (name != "" && startDate != "" && endDate != "")
                            {
                                DataRow dr = dt.NewRow();
                                dr["Kind"] = kind;
                                dr["AdminID"] = adminID;
                                dr["Name"] = name;
                                dr["StartDate"] = startDate;
                                dr["EndDate"] = endDate;
                                dr["Service"] = service;
                                dr["URL"] = Url;
                                dt.Rows.Add(dr);
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            basePage.mJSonHelper.AddContent("StatusCode", 1);
                            basePage.mJSonHelper.AddDataTable("ActivityList", dt);
                        }
                    }
                }
            }
        }
    }
}
public class ActivityRequestWrapper
{
    public List<ActivityTimeRequestDto> list { get; set; }
}

/// <summary>
/// 活動時間請求的資料模型
/// </summary>
public class ActivityTimeRequestDto
{
    /// <summary>
    /// 活動種類代碼
    /// </summary>
    public int Kind { get; set; }

    /// <summary>
    /// 宮廟代碼
    /// </summary>
    public int A { get; set; }
}