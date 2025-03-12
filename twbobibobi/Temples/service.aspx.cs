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
    public partial class service : AjaxBasePage
    {
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "getActivityTime");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
            string[] lights_h = { "新港奉天宮", "2024/11/01 00:00", "2025/10/31 23:59", "templeService_lights_h.aspx" };
            string[] lights_wu = { "北港武德宮", "2024/11/01 00:00", "2025/01/19 23:59", "templeService_lights_wu.aspx" };
            string[] lights_Fu = { "西螺福興宮", "2024/11/01 00:00", "2025/10/31 23:59", "templeService_lights_Fu.aspx" };
            string[] lights_Luer = { "台南正統鹿耳門聖母廟", "2024/11/01 00:00", "2025/10/31 23:59", "templeService_lights_Luer.aspx" };
            string[] lights_ty = { "桃園威天宮", "2024/11/01 00:00", "2025/10/17 23:59", "templeService_lights_ty.aspx" };
            string[] lights_Fw = { "斗六五路財神宮", "2024/11/01 00:00", "2025/10/31 23:59", "templeService_lights_Fw.aspx" };
            string[] lights_dh = { "台東東海龍門天聖宮", "2024/11/01 00:00", "2025/08/15 23:59", "templeService_lights_dh.aspx" };
            string[] lights_Lk = { "鹿港城隍廟", "2024/11/01 00:00", "2025/10/31 23:59", "templeService_lights_Lk.aspx" };
            string[] lights_ma = { "玉敕大樹朝天宮", "2024/11/01 00:00", "2025/06/30 23:59", "templeService_lights_ma.aspx" };
            string[] lights_wjsan = { "台灣道教總廟無極三清總道院", "2024/11/01 00:00", "2025/03/15 23:59", "templeService_lights_wjsan.aspx" };

            string[] supplies_wu = { "北港武德宮", "2023/10/15 00:00", "2023/11/21 23:59", "templeService_supplies.aspx" };
            string[] supplies_wu2 = { "北港武德宮", "2025/03/07 00:00", "2025/04/06 23:59", "templeService_supplies2.aspx" };
            string[] supplies_wu3 = { "北港武德宮", "0", "0", "templeService_supplies3.aspx" };
            string[] supplies_ty = { "桃園威天宮", "2025/02/18 00:00", "2025/03/06 23:59", "templeService_supplies_ty.aspx" };
            string[] supplies_Fw = { "斗六五路財神宮", "0", "0", "templeService_supplies_Fw.aspx" };
            string[] supplies_Lk = { "鹿港城隍廟", "2025/03/06 00:00", "2025/04/09 23:59", "templeService_supplies_Lk.aspx" };
            string[] supplies_ma = { "玉敕大樹朝天宮", "2025/002/18 00:00", "2025/03/06 23:59", "templeService_supplies_ma.aspx" };

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