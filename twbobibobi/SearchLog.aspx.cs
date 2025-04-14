using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MotoSystem.Data;
using Temple.data;
using System.Globalization;
using System.Web.Services;
using Read.data;
using Org.BouncyCastle.Utilities.Encoders;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Reflection;
using Org.BouncyCastle.Asn1.X9;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Linq;
using static ZXing.QrCode.Internal.Mode;

namespace Temple
{
    public partial class SearchLog : AjaxBasePage
    {
        public int applicantID = 0;
        public int adminID = 0;
        public int kind = 0;
        public string AppName;
        public string AppMobile;
        public string m_birth;
        public string m_address;
        public string m_email;
        public string Datalist;
        public string Remark;
        public string dataName;
        public int Total;

        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "checkedapplicant");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //applicantID = 1;
                //if (Request["aid"] != null && Request["a"] != null && Request["kind"] != null)
                //{
                //    if (int.TryParse(Request["kind"], out kind) && int.TryParse(Request["a"], out adminID) && int.TryParse(Request["aid"], out applicantID))
                //    {
                //        LightDAC objLightDAC = new LightDAC(this);
                //        DataTable dtapplecantinfo = new DataTable();
                //        DataTable dtdatainfo = new DataTable();
                //        //DataTable dtapplecant = objLightDAC.Getapplicantinfo_wu_Lights(applicantID, adminID, 2);
                //        switch (kind)
                //        {
                //            case 1:
                //                //點燈服務
                //                dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfo(applicantID, adminID, 2);
                //                dtdatainfo = objLightDAC.GetLights_info(applicantID, adminID);
                //                break;
                //            case 2:
                //                //普度服務
                //                //dtapplecantinfo = objLightDAC.Getapplicantinfo_wu_Lights(applicantID, adminID, 2);
                //                //dtdatainfo = objLightDAC.GetLights_wu_info(applicantID);
                //                break;
                //            case 3:
                //                //商品販賣服務
                //                switch (adminID)
                //                {
                //                    case 3:
                //                        //大甲鎮瀾宮
                //                        break;
                //                    case 4:
                //                        //新港奉天宮
                //                        break;
                //                    case 5:
                //                        //商品販賣小舖-新港奉天宮
                //                        dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(applicantID, adminID, 2);
                //                        dtdatainfo = objLightDAC.GetProduct_info(applicantID);
                //                        break;
                //                    case 6:
                //                        //北港武德宮
                //                        break;
                //                }
                //                break;
                //            case 4:
                //                //補財庫
                //                switch (adminID)
                //                {
                //                    case 6:
                //                        //北港武德宮
                //                        string type = Request["type"];
                //                        switch (type)
                //                        {
                //                            case "1":
                //                                //下元補庫
                //                                dtapplecantinfo = objLightDAC.Getapplicantinfo_wu_Supplies(applicantID, adminID, 2);
                //                                dtdatainfo = objLightDAC.GetSupplies_wu_info(applicantID);
                //                                break;
                //                            case "2":
                //                                //呈疏補庫
                //                                dtapplecantinfo = objLightDAC.Getapplicantinfo_wu_Supplies2(applicantID, adminID, 2);
                //                                dtdatainfo = objLightDAC.GetSupplies_wu_info2(applicantID);
                //                                break;
                //                            case "3":
                //                                //企業補財庫
                //                                dtapplecantinfo = objLightDAC.Getapplicantinfo_wu_Supplies3(applicantID, adminID, 2);
                //                                dtdatainfo = objLightDAC.GetSupplies_wu_info3(applicantID);
                //                                break;
                //                        }
                //                        break;
                //                }
                //                break;
                //        }

                //        if (dtapplecantinfo.Rows.Count > 0)
                //        {
                //            AppName = dtapplecantinfo.Rows[0]["Name"].ToString();
                //            AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[0]["Mobile"].ToString(), 5 , 3);

                //            if (dtdatainfo.Rows.Count > 0)
                //            {
                //                switch (adminID)
                //                {
                //                    case 3:
                //                        //大甲鎮瀾宮
                //                        for (int i = 0; i < dtdatainfo.Rows.Count; i++)
                //                        {
                //                            string Name = dtdatainfo.Rows[i]["Name"].ToString();
                //                            string Mobile = UtilDataMask.MaskMobile(dtdatainfo.Rows[i]["Mobile"].ToString(), 5, 3);
                //                            string Address = UtilDataMask.MaskTWAddr(dtdatainfo.Rows[i]["Address"].ToString());
                //                            string ZipCode = dtdatainfo.Rows[i]["ZipCode"].ToString();
                //                            string Birth = dtdatainfo.Rows[i]["Birth"].ToString();
                //                            string LeamMonth = dtdatainfo.Rows[i]["LeamMonth"].ToString();
                //                            string AppStatus = dtdatainfo.Rows[i]["AppStatus"].ToString();
                //                            string Num2String = dtdatainfo.Rows[i]["Num2String"].ToString();
                //                            string discountType = dtdatainfo.Rows[i]["DiscountType"].ToString();
                //                            int LightType = int.Parse(dtdatainfo.Rows[i]["type"].ToString());
                //                            Datalist += add_lights_blessed(adminID, Num2String, AppName, AppMobile, Name, Mobile, Address, ZipCode, LightType, i, Birth, LeamMonth, AppStatus, discountType, ref Total);
                //                        }
                //                        break;
                //                    case 4:
                //                        //新港奉天宮
                //                        for (int i = 0; i < dtdatainfo.Rows.Count; i++)
                //                        {
                //                            string Name = dtdatainfo.Rows[i]["Name"].ToString();
                //                            string Mobile = UtilDataMask.MaskMobile(dtdatainfo.Rows[i]["Mobile"].ToString(), 5, 3);
                //                            string Address = UtilDataMask.MaskTWAddr(dtdatainfo.Rows[i]["Address"].ToString());
                //                            string ZipCode = dtdatainfo.Rows[i]["ZipCode"].ToString();
                //                            string Birth = dtdatainfo.Rows[i]["Birth"].ToString();
                //                            string LeamMonth = dtdatainfo.Rows[i]["LeamMonth"].ToString();
                //                            string AppStatus = dtdatainfo.Rows[i]["AppStatus"].ToString();
                //                            string Num2String = dtdatainfo.Rows[i]["Num2String"].ToString();
                //                            int LightType = int.Parse(dtdatainfo.Rows[i]["type"].ToString());
                //                            Datalist += add_lights_blessed(adminID, Num2String, AppName, AppMobile, Name, Mobile, Address, ZipCode, LightType, i, Birth, LeamMonth, AppStatus, "", ref Total);
                //                        }
                //                        break;
                //                    case 5:
                //                        //商品販賣小舖-新港奉天宮
                //                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                //                        {
                //                            int applicantID = 0;
                //                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                //                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                //                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                //                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                //                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                //                            string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                //                            string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                //                            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                //                            int count_B = 0;
                //                            int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                //                            Total += int.Parse(cost_B) * count_B;

                //                            ProductDAC objProductDAC = new ProductDAC(this);
                //                            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                //                            Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                //                        }
                //                        break;
                //                    case 6:
                //                        //北港武德宮
                //                        //for (int i = 0; i < dtdatainfo.Rows.Count; i++)
                //                        //{
                //                        //    string Name = dtdatainfo.Rows[i]["Name"].ToString();
                //                        //    string Homenum = dtdatainfo.Rows[i]["Homenum"].ToString();
                //                        //    string Sex = dtdatainfo.Rows[i]["Sex"].ToString();
                //                        //    string Count = dtdatainfo.Rows[i]["Count"].ToString();
                //                        //    string Remark = dtdatainfo.Rows[i]["Remark"].ToString();
                //                        //    string Mobile = UtilDataMask.MaskMobile(dtdatainfo.Rows[i]["Mobile"].ToString(), 5, 3);
                //                        //    string Address = UtilDataMask.MaskTWAddr(dtdatainfo.Rows[i]["Address"].ToString());
                //                        //    string ZipCode = dtdatainfo.Rows[i]["ZipCode"].ToString();
                //                        //    string Birth = dtdatainfo.Rows[i]["Birth"].ToString() + " " + dtdatainfo.Rows[i]["BirthTime"].ToString();
                //                        //    string Email = dtdatainfo.Rows[i]["Email"].ToString();
                //                        //    string AppStatus = dtdatainfo.Rows[i]["AppStatus"].ToString();
                //                        //    string Num2String = dtdatainfo.Rows[i]["Num2String"].ToString();
                //                        //    string type = Request["type"];

                //                        //    switch (kind)
                //                        //    {
                //                        //        case 1:
                //                        //            //點燈
                //                        //            string LightType = LightsType2String(dtdatainfo.Rows[i]["type"].ToString());
                //                        //            Datalist += add_Lights_wu_blessed(AppName, AppMobile, Name, Homenum, Sex, Count, Remark, Mobile, Address, Birth, Email, ZipCode, LightType, i, AppStatus, Num2String, ref Total);
                //                        //            break;
                //                        //        case 2:
                //                        //            //普渡
                //                        //            break;
                //                        //        case 4:
                //                        //            //補財庫
                //                        //            string Join_date = dtdatainfo.Rows[i]["Join_date"].ToString();
                //                        //            Datalist += add_Supplies_blessed(AppName, AppMobile, Name, Homenum, Sex, Count, Remark, Mobile, Address, Birth, Email, ZipCode, Join_date, i, AppStatus, Num2String, ref Total);
                //                        //            break;
                //                        //    }
                                            
                //                        //}
                //                        break;
                //                    case 7:
                //                        //大甲鎮瀾宮繞境商品






                //                        break;

                //                }

                //                //Total += getTotal(dtdatainfo, adminID, kind);
                //            }

                //        }
                //    }

                //    dataName = "結果";
                //}
                //else
                //{
                //    dataName = "查詢";
                //}
            }
        }

        public static string add_lights_blessed(int adminID, string num2string, string AppName, string AppMobile, string name, string Mobile, string address, string zipcodes, int type, int numLightsBlessed, string birthday, string LeamMonth, string status, string discountType, ref int total)
        {
            string lightslist = string.Empty;

            int cost = 0;
            string templeName = string.Empty;
            string lightstype = string.Empty;
            switch (adminID)
            {
                case 3:
                    switch (type)
                    {
                        case 3:
                            lightstype = "光明燈";
                            cost = 620;
                            break;
                        case 4:
                            lightstype = "安太歲";
                            cost = 520;
                            break;
                        case 5:
                            lightstype = "文昌燈";
                            cost = 820;
                            break;

                    }
                    templeName = "大甲鎮瀾宮";
                    break;
                case 4:
                    switch (type)
                    {
                        case 3:
                            lightstype = "光明燈";
                            cost = 620;
                            break;
                        case 4:
                            lightstype = "安太歲";
                            cost = 620;
                            break;
                    }
                    templeName = "新港奉天宮";
                    break;
            }

            if (birthday.Length == 6)
            {
                birthday = "民國" + birthday.Substring(0, 2) + "年" + birthday.Substring(2, 2) + "月" + birthday.Substring(4, 2) + "日";
            }
            else if (birthday.Length == 7)
            {
                birthday = "民國" + birthday.Substring(0, 3) + "年" + birthday.Substring(3, 2) + "月" + birthday.Substring(5, 2) + "日";
            }

            if (LeamMonth == "1")
            {
                birthday += " 閏月";
            }

            if (discountType != "")
            {
                cost = discountType == "1" ? 0 : cost - 100;
            }

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", templeName);
            lightslist += addData_Content("訂單編號", num2string);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("居住地址", zipcodes + " " + address);
            lightslist += addData_Content("生日(農曆)", birthday);
            lightslist += addData_Content("燈種", lightstype);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(status)));

            lightslist += "</li>";

            total += cost;

            return lightslist;
        }

        public static string add_Lights_da_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, string discountType, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(3, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            if (discountType != "")
            {
                cost = discountType == "1" ? 0 : cost - 100;
            }


            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "大甲鎮瀾宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            //lightslist += GetLightsImg(LightsString, name);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_h_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(4, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "新港奉天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_wu_blessed(string AppName, string AppMobile, string name, string Homenum, string Sex, string Count, string Remark, string Mobile, string address, 
            string Birth, string LeapMonth, string BirthTime, string Email, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, 
            string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(6, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "北港武德宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            if (Sex == "M")
            {
                Sex = "善男";
            }
            else if (Sex == "F")
            {
                Sex = "信女";
            }
            lightslist += addData_Content("性別", Sex);
            lightslist += addData_Content("市話", Homenum);
            lightslist += addData_Content("E-mail", Email);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("數量", Count);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_Fu_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(8, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "西螺福興宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_Luer_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, string PetName, string PetType, 
            string Msg, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(10, LightsType);

            //switch (LightsType)
            //{
            //    case "3":
            //        //光明燈
            //        break;
            //    case "4":
            //        //安太歲
            //        break;
            //    case "7":
            //        //姻緣燈
            //        cost = 999;
            //        break;
            //}

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "台南正統鹿耳門聖母廟");
            lightslist += addData_Content("訂單編號", Num2String);
            if (PetName != "")
            {
                lightslist += addData_Content("飼主姓名", name);
                lightslist += addData_Content("飼主電話", Mobile);
                lightslist += addData_Content("飼主農曆生日", Birth + (LeapMonth == "Y" ? "閏月" : ""));
                lightslist += addData_Content("飼主農曆時辰", BirthTime);
                lightslist += addData_Content("飼主居住地址", address);
                lightslist += addData_Content("寵物姓名", PetName);
                lightslist += addData_Content("寵物品種", PetType);
            }
            else
            {
                lightslist += addData_Content("祈福人姓名", name);
                lightslist += addData_Content("祈福人電話", Mobile);
                lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? "閏月" : ""));
                lightslist += addData_Content("農曆時辰", BirthTime);
                lightslist += addData_Content("居住地址", address);
            }

            if (Msg != "")
            {
                lightslist += addData_Content("祈福小語", Msg);
            }

            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_ty_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth,
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(14, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_ty_mom_blessed(string AppName, string AppMobile, string AppBirth, string AppLeapMonth, string AppBirthTime, string AppZipCode, string AppAddress, 
            string name, string Mobile, string Birth, string LeapMonth, string BirthTime, string zipCode, string address, string LightsType, int numZampBlessed, string AppStatus, 
            string Num2String, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(14, LightsType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("國曆生日", AppBirth + (AppLeapMonth == "Y" ? " 閏月" : ""));
                //lightslist += addData_Content("農曆時辰", AppBirthTime);
                lightslist += addData_Content("購買人居住地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            //lightslist += addData_Content("國曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            //lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_Fw_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, string PetName, string PetType, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(15, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "斗六五路財神宮");
            lightslist += addData_Content("訂單編號", Num2String);
            if (PetName != "")
            {
                lightslist += addData_Content("飼主姓名", name);
                lightslist += addData_Content("飼主電話", Mobile);
                lightslist += addData_Content("飼主農曆生日", Birth + (LeapMonth == "Y" ? "閏月" : ""));
                lightslist += addData_Content("飼主農曆時辰", BirthTime);
                lightslist += addData_Content("飼主居住地址", address);
                lightslist += addData_Content("寵物姓名", PetName);
                lightslist += addData_Content("寵物品種", PetType);
            }
            else
            {
                lightslist += addData_Content("祈福人姓名", name);
                lightslist += addData_Content("祈福人電話", Mobile);
                lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? "閏月" : ""));
                lightslist += addData_Content("農曆時辰", BirthTime);
                lightslist += addData_Content("居住地址", address);
            }

            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_dh_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string address, string Birth, string LeapMonth, 
            string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(16, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "台東東海龍門天聖宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_Lk_blessed(string AppName, string AppMobile, string name, string Count, string Mobile, string Sex, string HomeNum, string address, 
            string Birth, string LeapMonth, string BirthTime, string zipCode, string LightsType, int numZampBlessed, string AppStatus, string Num2String, string LightsString, string ChargeDate, 
            string Sendback, string rName, string rMobile, string rAddress, string rZipCode, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(21, LightsType);

            cost = cost * int.Parse(Count);

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "鹿港城隍廟");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("市話", HomeNum);
            lightslist += addData_Content("居住地址", address);
            lightslist += addData_Content("服務項目", LightsString);
            lightslist += addData_Content("贈品處理方式", Sendback == "Y" ? "寄回" : "不寄回");

            if (Sendback == "Y")
            {
                cost += 100;
                lightslist += addData_Content("收件人姓名", rName);
                lightslist += addData_Content("收件人電話", rMobile);
                lightslist += addData_Content("收件人地址", rAddress);
            }

            total += cost;

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_ma_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime, 
            string Email, string Count, string Address, int numZampBlessed, string AppStatus, string Num2String, string LightsType, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(23, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "玉敕大樹朝天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人Email", Email);
            lightslist += addData_Content("祈福人地址", Address);
            lightslist += addData_Content("服務項目", LightsString);

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_jb_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime,
            string Email, string Count, string Address, int numZampBlessed, string AppStatus, string Num2String, string LightsType, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(29, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "進寶財神廟");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人Email", Email);
            lightslist += addData_Content("祈福人地址", Address);
            lightslist += addData_Content("服務項目", LightsString);

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_wjsan_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime,
            string Email, string Count, string Address, int numZampBlessed, string AppStatus, string Num2String, string LightsType, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(31, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "台灣道教總廟無極三清總道院");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人Email", Email);
            lightslist += addData_Content("祈福人地址", Address);
            lightslist += addData_Content("服務項目", LightsString);

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lights_ld_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime,
            string Email, string Count, string Address, int numZampBlessed, string AppStatus, string Num2String, string LightsType, string LightsString, string ChargeDate, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLightsCost(32, LightsType);

            cost = cost * int.Parse(Count);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }


            lightslist += addData_Content("宮廟名稱", "桃園龍德宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人Email", Email);
            lightslist += addData_Content("祈福人地址", Address);
            lightslist += addData_Content("服務項目", LightsString);

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_purdue_blessed(string name, string Homenum, string Sex, string Count, string Remark, string Mobile, string address, string Birth, string Email, int sendbacktype, int sendback_thx, int numZampBlessed, string AppStatus, string Num2String)
        {
            string purduelist = string.Empty;
            int cost = 0;

            purduelist = "<div id='blessed_zamp-" + numZampBlessed + "'>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name public-lamp-circle-position'>" +
                                    "<div class='number-circle'>" + (numZampBlessed + 1) + "</div>" +
                                    "<span style='font-size:25px;font-weight:bold;margin-left:5px;'>中元普度贊普</span></div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>姓名：</div><div class='lamp-result-item-name item-content '>" + name + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>性別：</div><div class='lamp-result-item-name item-content '>" + (Sex == "M" ? "善男" : "信女") + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>農曆生日：</div><div class='lamp-result-item-name item-content '>" + Birth + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>電話：</div><div class='lamp-result-item-name item-content '>" + Mobile + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>市話：</div><div class='lamp-result-item-name item-content '>" + Homenum + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>E-mail：</div><div class='lamp-result-item-name item-content '>" + Email + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>通訊地址：</div><div class='lamp-result-item-name item-content '>" + address + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>數量：</div><div class='lamp-result-item-name item-content '>" + Count + "</div></div>" +
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>備註：</div><div class='lamp-result-item-name item-content '>" + Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "") + "</div></div>";

            if (sendbacktype == 1 || sendbacktype == 2)
            {
                string sendback = "";

                if (sendbacktype == 1)
                {
                    sendback = "領回";
                }

                if (sendbacktype == 2)
                {
                    sendback = "捐贈";
                }

                purduelist +=
                            "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>處理方式：</div><div class='lamp-result-item-name item-content '>" + sendback + "</div></div>";
            }


            //purduelist += "<div class='col-sm-12'>" +
            //                    "<div class='lamp-result-item-name item-name '>感謝狀處理方式：</div><div class='lamp-result-item-name item-content '>" + (sendback_thx == 0 ? "燒化" : "寄出") + "</div></div>";

            cost += (sendbacktype == 0 ? 1700 : 1500);

            cost = cost * int.Parse(Count);

            purduelist += "<div class='col-sm-12'>" +
                                "<div class='lamp-result-item-name item-name '>金額：</div><div class='lamp-result-item-name item-content '>" + cost.ToString() + "</div></div>" +
                        "<div class='col-sm-12'>" +
                            "<div class='lamp-result-item-name item-name '>狀態：</div><div class='lamp-result-item-name item-content '>" + Status2String(int.Parse(AppStatus)) + "</div></div>" +
                        "<div class='col-sm-12'>" +
                                    "<div class='lamp-result-item-name item-name '>訂單編號：</div><div class='lamp-result-item-name item-content '>" + Num2String + "</div></div>" +
                        "<div class='col-sm-12'>" +
                                "<hr class='lamp-result-line' /></div></div>";
            return purduelist;
        }

        public static string add_Purdue_da_blessed(string AppName, string AppMobile, string Name, string Name2, string Mobile, string Birth, string LeapMonth, string BirthTime, 
            string Address, string ZipCode, string PurdueType, string PurdueString, string DeathName, string Birthday, string DeathLeapMonth, string DeathBirthTime, 
            string Deathday, string FirstName, string Sendback, string rName, string rMobile, string rAddress, string rZipCode, int numZampBlessed, string AppStatus, 
            string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            if (PurdueType == "1")
            {
                //贊普
                cost = 1000;
                if (Sendback == "1")
                {
                    cost += 250;
                }
            }
            else
            {
                cost = 620;
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "大甲鎮瀾宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人姓名2", Name2);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("地址", Address);
            Purduelist += addData_Content("普度項目", PurdueString);

            if (PurdueType == "1")
            {
                //贊普
                if (Sendback == "1")
                {
                    Purduelist += addData_Content("寄送方式", "寄回(加收運費250元)");
                    Purduelist += addData_Content("收件人姓名", rName);
                    Purduelist += addData_Content("收件人電話", rMobile);
                    Purduelist += addData_Content("收件人地址", rZipCode + " " + rAddress);
                }
                else
                {
                    Purduelist += addData_Content("寄送方式", "不寄回(會轉送給弱勢團體)");
                }
            }
            else if (PurdueType == "2" || PurdueType == "6")
            {
                //九玄七祖 or 嬰靈
                Purduelist += addData_Content("姓氏", FirstName);
            }
            else if (PurdueType == "3" )
            {
                //指名亡者
                Purduelist += addData_Content("亡者姓名", DeathName);
                Purduelist += addData_Content("亡者農曆生日", Birthday + (DeathLeapMonth == "Y" ? " 閏月" : ""));
                Purduelist += addData_Content("亡者農曆時辰", DeathBirthTime);
                Purduelist += addData_Content("亡者死亡日期", Deathday);
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_h_blessed(DataTable dtData, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;
            int landlordnum = 0;
            int babynum = 0;
            int animalnum = 0;
            int purduenum = 0;
            int ricenum = 0;
            int mmoneynum = 0;

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                string Merit = dtData.Rows[i]["Merit"].ToString();
                string MeritText = dtData.Rows[i]["MeritText"].ToString();
                string Life = dtData.Rows[i]["Life"].ToString();
                string Redress = dtData.Rows[i]["Redress"].ToString();
                string Creditor = dtData.Rows[i]["Creditor"].ToString();

                string Ancestor = dtData.Rows[i]["Ancestor"].ToString();
                string AncestorLastname = dtData.Rows[i]["AncestorLastname"].ToString();
                string AncestorAddress = dtData.Rows[i]["AncestorAddress"].ToString();

                string Deceased = dtData.Rows[i]["Deceased"].ToString();
                string DeceasedName = dtData.Rows[i]["DeceasedName"].ToString();
                string DeceasedAddress = dtData.Rows[i]["DeceasedAddress"].ToString();

                string Landlord = dtData.Rows[i]["Landlord"].ToString();
                int.TryParse(dtData.Rows[i]["LandlordNum"].ToString(), out landlordnum);

                string Baby = dtData.Rows[i]["Baby"].ToString();
                int.TryParse(dtData.Rows[i]["BabyNum"].ToString(), out babynum);

                string Animal = dtData.Rows[i]["Animal"].ToString();
                int.TryParse(dtData.Rows[i]["AnimalNum"].ToString(), out animalnum);

                int.TryParse(dtData.Rows[i]["PurdueNum"].ToString(), out purduenum);
                int.TryParse(dtData.Rows[i]["RiceNum"].ToString(), out ricenum);
                int.TryParse(dtData.Rows[i]["mMoneyNum"].ToString(), out mmoneynum);

                cost = getTotal(Merit, Life, Redress, Creditor, Ancestor, Deceased, Landlord, landlordnum, Baby, babynum, Animal, animalnum, purduenum, ricenum, mmoneynum);

                total += cost;

                string Num2String = dtData.Rows[i]["Num2String"].ToString();
                string AppName = UtilDataMask.MaskName(dtData.Rows[i]["AppName"].ToString());
                string AppMobile = UtilDataMask.MaskMobile(dtData.Rows[i]["AppMobile"].ToString(), 5, 3);
                string AppBirth = dtData.Rows[i]["AppBirth"].ToString();
                string AppLeapMonth = dtData.Rows[i]["AppLeapMonth"].ToString();
                string AppBirthTime = dtData.Rows[i]["AppBirthTime"].ToString();
                string AppEmail = dtData.Rows[i]["AppEmail"].ToString();
                string AppAddress = UtilDataMask.MaskTWAddr(dtData.Rows[i]["AppAddress"].ToString());
                string AppZipCode = dtData.Rows[i]["AppZipCode"].ToString();

                string Name = UtilDataMask.MaskName(dtData.Rows[i]["Name"].ToString());
                string Birth = dtData.Rows[i]["Birth"].ToString();
                string LeapMonth = dtData.Rows[i]["LeapMonth"].ToString();
                string BirthTime = dtData.Rows[i]["BirthTime"].ToString();
                string ZipCode = dtData.Rows[i]["ZipCode"].ToString();
                string Address = UtilDataMask.MaskTWAddr(dtData.Rows[i]["Address"].ToString());
                string ChargeDate = dtData.Rows[i]["ChargeDateString"].ToString();
                string Remark = dtData.Rows[i]["Remark"].ToString();

                Purduelist += "<li>";

                Purduelist += addData_Content("宮廟名稱", "新港奉天宮");
                Purduelist += addData_Content("訂單編號", Num2String);
                Purduelist += addData_Content("受理時間", ChargeDate);

                if (AppName != "" && AppMobile != "" && AppBirth != "" && AppEmail != "" && AppAddress != "" && AppZipCode != "")
                {
                    Purduelist += addData_Content("購買人姓名", AppName);
                    Purduelist += addData_Content("購買人電話", AppMobile);
                    Purduelist += addData_Content("購買人信箱", AppEmail);
                    Purduelist += addData_Content("農曆生日", AppBirth + (AppLeapMonth == "Y" ? " 閏月" : ""));
                    Purduelist += addData_Content("農曆時辰", AppBirthTime);
                    Purduelist += addData_Content("購買人地址", AppAddress);
                }


                if (Merit == "1")
                {
                    Purduelist += addData_Content("購買項目", "功德主($3000)");
                    if (MeritText != "")
                    {
                        Purduelist += addData_Content("購買項目", "功德主($3000)");
                        Purduelist += addData_Content("功德主內容", MeritText.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                    }
                }

                if (Life == "1")
                {
                    Purduelist += addData_Content("購買項目", "祝燈延壽消災($600)");
                }

                if (Redress == "1")
                {
                    Purduelist += addData_Content("購買項目", "四十九愿解冤釋結($600)");
                }

                if (Creditor == "1")
                {
                    Purduelist += addData_Content("購買項目", "冤親債主($500)");
                }

                if (Ancestor == "1")
                {
                    Purduelist += addData_Content("購買項目", "九玄七祖 (歷代祖先，迴向功德)($500)");
                    Purduelist += addData_Content("超度亡者姓氏", AncestorLastname);
                    Purduelist += addData_Content("祖先牌位地址", AncestorAddress);
                }

                if (Deceased == "1")
                {
                    Purduelist += addData_Content("購買項目", "功德迴向往生者($500)");
                    Purduelist += addData_Content("超度亡者姓名", DeceasedName);
                    Purduelist += addData_Content("祖先牌位地址", DeceasedAddress);
                }

                if (Landlord == "1")
                {
                    Purduelist += addData_Content("購買項目", "地祇主($500)");
                    Purduelist += addData_Content("數量", landlordnum.ToString());
                }

                if (Baby == "1")
                {
                    Purduelist += addData_Content("購買項目", "嬰靈($500)");
                    Purduelist += addData_Content("數量", babynum.ToString());
                }

                if (Animal == "1")
                {
                    Purduelist += addData_Content("購買項目", "動物靈($500)");
                    Purduelist += addData_Content("數量", animalnum.ToString());
                }

                Purduelist += addData_Content("祈福人姓名", Name);
                Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
                Purduelist += addData_Content("農曆時辰", BirthTime);
                Purduelist += addData_Content("祈福人地址", Address);

                if (purduenum != 0)
                {
                    Purduelist += addData_Content("購買項目", "普 度 品($1000)");
                    Purduelist += addData_Content("數量", purduenum.ToString());
                }

                if (ricenum != 0)
                {
                    Purduelist += addData_Content("購買項目", "白米認捐($200)");
                    Purduelist += addData_Content("數量", ricenum.ToString());
                }

                if (mmoneynum != 0)
                {
                    Purduelist += addData_Content("購買項目", "金紙部分($200)");
                    Purduelist += addData_Content("數量", mmoneynum.ToString());
                }

                if (Remark != "")
                {
                    Purduelist += addData_Content("備註說明", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                }

                Purduelist += "</li>";
            }

            return Purduelist;
        }

        public static int getTotal(string Merit, string Life, string Redress, string Creditor, string Ancestor, string Deceased, string Landlord, int LandlordNum, string Baby, 
            int BabyNum, string Animal, int AnimalNum, int PurdueNum, int RiceNum, int mMoneyNum)
        {
            int result = 0;

            //功德主 $3000
            if (Merit == "1")
            {
                result += 3000;
            }

            //祝燈延壽消災 $600
            if (Life == "1")
            {
                result += 600;
            }

            //四十九愿解冤釋結 $600
            if (Redress == "1")
            {
                result += 600;
            }

            //冤親債主 $500
            if (Creditor == "1")
            {
                result += 500;
            }

            //九玄七祖 $500
            if (Ancestor == "1")
            {
                result += 500;
            }

            //功德迴向往生者 $500
            if (Deceased == "1")
            {
                result += 500;
            }

            //地祇主 $500
            if (Landlord == "1")
            {
                result += (500 * LandlordNum);
            }

            //嬰靈 $500
            if (Baby == "1")
            {
                result += (500 * BabyNum);
            }

            //動物靈 $500
            if (Animal == "1")
            {
                result += (500 * AnimalNum);
            }

            //普頓品 $1000
            result += (1000 * PurdueNum);

            //白米 $200
            result += (200 * RiceNum);

            //金紙 $200
            result += (200 * mMoneyNum);

            return result;
        }

        public static string add_Purdue_h_blessed(string AppName, string AppMobile, string AppEmail, string AppBirth, string AppLeapMonth, string AppBirthTime, string AppAddress, 
            string AppZipCode, DataTable dtData, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 1500;

            cost = getTotal(dtData);

            total += cost;

            Purduelist = "<li>";

            Purduelist += addData_Content("宮廟名稱", "新港奉天宮");
            Purduelist += addData_Content("訂單編號", Num2String);

            if (AppName != "" && AppMobile != "" && AppBirth != "" && AppEmail != "" && AppAddress != "" && AppZipCode != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
                Purduelist += addData_Content("購買人信箱", AppEmail);
                Purduelist += addData_Content("農曆生日", AppBirth + (AppLeapMonth == "Y" ? " 閏月" : ""));
                Purduelist += addData_Content("農曆時辰", AppBirthTime);
                Purduelist += addData_Content("購買人地址", AppAddress);
            }


            if (dtData.Rows[0]["Merit"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "功德主($3000)");
                if (dtData.Rows[0]["MeritText"].ToString() != "")
                {
                    Purduelist += addData_Content("購買項目", "功德主($3000)");
                    Purduelist += addData_Content("功德主內容", dtData.Rows[0]["MeritText"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                }
            }

            if (dtData.Rows[0]["Life"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "祝燈延壽消災($600)");
            }

            if (dtData.Rows[0]["Redress"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "四十九愿解冤釋結($600)");
            }

            if (dtData.Rows[0]["Creditor"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "冤親債主($500)");
            }

            if (dtData.Rows[0]["Ancestor"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "九玄七祖 (歷代祖先，迴向功德)($500)");
                Purduelist += addData_Content("超度亡者姓氏", dtData.Rows[0]["AncestorLastname"].ToString());
                Purduelist += addData_Content("祖先牌位地址", dtData.Rows[0]["AncestorAddress"].ToString());
            }

            if (dtData.Rows[0]["Deceased"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "功德迴向往生者($500)");
                Purduelist += addData_Content("超度亡者姓名", dtData.Rows[0]["DeceasedName"].ToString());
                Purduelist += addData_Content("祖先牌位地址", dtData.Rows[0]["DeceasedAddress"].ToString());
            }

            if (dtData.Rows[0]["Landlord"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "地祇主($500)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["LandlordNum"].ToString());
            }

            if (dtData.Rows[0]["Baby"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "嬰靈($500)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["BabyNum"].ToString());
            }

            if (dtData.Rows[0]["Animal"].ToString() == "1")
            {
                Purduelist += addData_Content("購買項目", "動物靈($500)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["AnimalNum"].ToString());
            }

            Purduelist += addData_Content("祈福人姓名", dtData.Rows[0]["Name"].ToString());
            Purduelist += addData_Content("農曆生日", dtData.Rows[0]["Birth"].ToString() + (dtData.Rows[0]["LeapMonth"].ToString() == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", dtData.Rows[0]["BirthTime"].ToString());
            Purduelist += addData_Content("祈福人地址", dtData.Rows[0]["Address"].ToString());

            if (dtData.Rows[0]["PurdueNum"].ToString() != "0")
            {
                Purduelist += addData_Content("購買項目", "普 度 品($1000)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["PurdueNum"].ToString());
            }

            if (dtData.Rows[0]["RiceNum"].ToString() != "0")
            {
                Purduelist += addData_Content("購買項目", "白米認捐($200)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["RiceNum"].ToString());
            }

            if (dtData.Rows[0]["mMoneyNum"].ToString() != "0")
            {
                Purduelist += addData_Content("購買項目", "金紙部分($200)");
                Purduelist += addData_Content("數量", dtData.Rows[0]["mMoneyNum"].ToString());
            }

            if (dtData.Rows[0]["Remark"].ToString() != "")
            {
                Purduelist += addData_Content("備註說明", dtData.Rows[0]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            }
            Purduelist += "</li>";

            return Purduelist;
        }
        
        public static int getTotal(DataTable dtdata)
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

        public static string add_Purdue_wu_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string Sex, 
            string Homenum, string Email, string ZipCode, string Address, string Count, string Remark, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 1500;

            cost = cost * int.Parse(Count);

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "北港武德宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("性別", Sex);
            Purduelist += addData_Content("市話", Homenum);
            Purduelist += addData_Content("E-mail", Email);
            Purduelist += addData_Content("地址", Address);
            Purduelist += addData_Content("數量", Count);
            Purduelist += addData_Content("普度項目", "贊普");
            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);
            Purduelist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_Fu_blessed(string AppName, string AppMobile, string Name, string Name2, string Mobile, string Birth, string LeapMonth, string BirthTime, 
            string ZipCode, string Address, string PurdueType, string PurdueString, string Count, string GoldPaperCount, string DeathName, string FirstName, int numZampBlessed, 
            string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            if (PurdueType == "1")
            {
                //贊普
                cost = 1500 * int.Parse(Count);
            }
            else
            {
                cost = 600;

                if (Count == "1")
                {
                    //加購普品
                    cost += 1500;
                }

                //加購金紙
                cost += (300 * int.Parse(GoldPaperCount));
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "西螺福興宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            switch (PurdueType)
            {
                case "1":
                    //贊普
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("祈福人姓名2", Name2);
                    Purduelist += addData_Content("祈福人電話", Mobile);
                    Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
                    Purduelist += addData_Content("農曆時辰", BirthTime);
                    Purduelist += addData_Content("居住地址", Address);
                    Purduelist += addData_Content("數量", Count);
                    break;
                case "2":
                    //九玄七祖
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("祖先姓氏", FirstName);
                    Purduelist += addData_Content("超度對象地址", Address);
                    if (Count == "1")
                    {
                        Purduelist += addData_Content("加購普品 $1500 元", "加購");
                    }
                    else
                    {
                        Purduelist += addData_Content("加購普品 $1500 元", "不加購");
                    }
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
                case "3":
                    //亡者
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("超度姓名", DeathName);
                    Purduelist += addData_Content("超度對象地址", Address);
                    if (Count == "1")
                    {
                        Purduelist += addData_Content("加購普品 $1500 元", "加購");
                    }
                    else
                    {
                        Purduelist += addData_Content("加購普品 $1500 元", "不加購");
                    }
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
                case "4":
                    //地基主
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("超度地址", Address);
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
                case "5":
                    //冤親債主
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("超度姓名", DeathName);
                    Purduelist += addData_Content("超度地址", Address);
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
                case "6":
                    //嬰靈
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("超度姓名", DeathName);
                    Purduelist += addData_Content("超度地址", Address);
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
                case "12":
                    //動物靈
                    Purduelist += addData_Content("祈福人姓名", Name);
                    Purduelist += addData_Content("超度姓名", DeathName);
                    Purduelist += addData_Content("超度地址", Address);
                    Purduelist += addData_Content("加購金紙數量", GoldPaperCount);
                    break;
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_Jing_blessed(string AppName, string AppMobile, string Name, string Name2, string Mobile, string Birth, string LeapMonth, string BirthTime, 
            string Address, string ZipCode, string PurdueType, string PurdueString, string FirstName, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 1000;

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "桃園大廟景福宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人姓名2", Name2);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("普度項目", PurdueString);

            if (PurdueType == "2")
            {
                //祖先
                Purduelist += addData_Content("祖先姓氏", FirstName);
            }
            else if (PurdueType == "6")
            {
                //嬰靈
                Purduelist += addData_Content("姓氏", FirstName);
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_Luer_blessed(string AppName, string AppMobile, string Name, string Name2, string Mobile, string Birth, string LeapMonth, string BirthTime, 
            string Address, string ZipCode, string PurdueString, string Email, string Count, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 1000;

            cost = cost * int.Parse(Count);

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "台南正統鹿耳門聖母廟");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人姓名2", Name2);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("祈福人信箱", Email);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("普度項目", PurdueString);
            Purduelist += addData_Content("普渡植福數量 (含金紙)", Count);
            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_ty_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime,
            string ZipCode, string Address, string Remark, string PurdueString, string PurdueType, string Count, string PurdueItem, string DeathName, string FirstName,
            string MomName, string LastName, string Reason, string LicenseNum, string DeathAddress, string PurdueItem1, string DeathName1, string FirstName1,
            string MomName1, string LastName1, string Reason1, string LicenseNum1, string DeathAddress1, int numZampBlessed, string AppStatus, 
            string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = GetPurdueCost(14, PurdueType);

            cost = cost * int.Parse(Count);

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "桃園威天宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
            Purduelist += addData_Content("普度項目", PurdueString);

            switch (PurdueType)
            {
                case "14":
                    //孝道功德主
                    TabletChecked(PurdueItem, DeathName, FirstName, MomName, LastName, Reason, LicenseNum, DeathAddress, PurdueItem1, DeathName1, FirstName1, MomName1, LastName1,
                        Reason1, LicenseNum1, DeathAddress1, ref Purduelist);
                    break;
                case "15":
                    //光明功德主
                    TabletChecked(PurdueItem, DeathName, FirstName, MomName, LastName, Reason, LicenseNum, DeathAddress, PurdueItem1, DeathName1, FirstName1, MomName1, LastName1,
                        Reason1, LicenseNum1, DeathAddress1, ref Purduelist);
                    break;
                case "16":
                    //發心功德主
                    TabletChecked(PurdueItem, DeathName, FirstName, MomName, LastName, Reason, LicenseNum, DeathAddress, PurdueItem1, DeathName1, FirstName1, MomName1, LastName1,
                        Reason1, LicenseNum1, DeathAddress1, ref Purduelist);
                    break;
                default:
                    Purduelist += addData_Content("數量", Count);
                    break;
            } 

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_Fw_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string ZipCode, 
            string Address, string PurdueType, string PurdueString, string Count, string Count_rice, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 1200;

            //捐獻白米
            cost += (200 * int.Parse(Count_rice));

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "斗六五路財神宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("數量", Count);
            if (Count_rice != "0")
            {
                Purduelist += addData_Content("捐獻白米", Count_rice);
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_dh_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string ZipCode, 
            string Address, string PurdueType, string PurdueString, string Count, string DeathName, string Deathday, string DeathBirth, string DeathLeapMonth, string DeathBirthTime, 
            string FirstName, string DeathAddress, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            if (PurdueType == "1")
            {
                //贊普
                cost = 1500 * int.Parse(Count);
            }
            else
            {
                cost = 500;
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "台東東海龍門天聖宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("數量", Count);

            switch (PurdueType)
            {
                case "2":
                    //超薦『歷代祖先』 $500
                    Purduelist += addData_Content("祖先姓氏", FirstName);
                    Purduelist += addData_Content("牌位地址", DeathAddress);
                    break;
                case "3":
                    //超薦『往生親友』 $500
                    Purduelist += addData_Content("往生親友姓名", DeathName);
                    Purduelist += addData_Content("往生日期", Deathday);
                    Purduelist += addData_Content("牌位地址", DeathAddress);
                    break;
                case "5":
                    //超薦『冤親債主』 $500
                    Purduelist += addData_Content("超薦者姓名", DeathName);
                    Purduelist += addData_Content("農曆生日", DeathBirth + (DeathLeapMonth == "Y" ? " 閏月" : ""));
                    Purduelist += addData_Content("農曆時辰", DeathBirthTime);
                    Purduelist += addData_Content("超薦者地址", DeathAddress);
                    break;
                case "6":
                    //超薦『嬰靈(無緣子女)』 $500
                    Purduelist += addData_Content("往生日期", Deathday);
                    break;
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_Lk_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string Sex, 
            string Homenum, string ZipCode, string Address, string PurdueType, string PurdueString, string Count, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, 
            ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            if (PurdueType == "1")
            {
                //贊普
                cost = 2000 * int.Parse(Count);
            }
            else
            {
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "鹿港城隍廟");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("性別", Sex);
            Purduelist += addData_Content("市話", Homenum);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("數量", Count);

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_ma_blessed(string AppName, string AppMobile, string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string Sex,
            string Email, string ZipCode, string Address, string PurdueType, string PurdueString, string Count, string Remark, string FirstName, int numZampBlessed, 
            string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            switch (PurdueType)
            {
                case "1":
                    //贊普
                    cost = 1200 * int.Parse(Count);
                    break;
                case "12":
                    //法船 $580
                    cost = 580 * int.Parse(Count);
                    break;
                case "13":
                    //壽生錢 $1500
                    cost = 1500 * int.Parse(Count);
                    break;
                default:
                    //超拔
                    cost = 300 * int.Parse(Count);
                    break;
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
            }

            Purduelist += addData_Content("宮廟名稱", "玉敕大樹朝天宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("性別", Sex);
            Purduelist += addData_Content("E-mail", Email);
            Purduelist += addData_Content("居住地址", Address);
            Purduelist += addData_Content("數量", Count);

            switch (PurdueType)
            {
                case "2":
                    //九玄七祖
                    Purduelist += addData_Content("姓氏", FirstName);
                    break;
                case "6":
                    //嬰靈
                    Purduelist += addData_Content("姓氏", FirstName);
                    break;
            }

            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);
            Purduelist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

            Purduelist += "</li>";

            return Purduelist;
        }

        public static string add_Purdue_mazu_blessed(string AppName, string AppMobile, string AppZipCode, string AppAddress, string Name, string Mobile, string Birth, string LeapMonth, 
            string BirthTime, string PurdueType, string PurdueString, string Count, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, ref int total)
        {
            string Purduelist = string.Empty;

            int cost = 0;

            switch (PurdueType)
            {
                default:
                    cost = 1100 * int.Parse(Count);
                    break;
            }

            total += cost;

            Purduelist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Purduelist += addData_Content("購買人姓名", AppName);
                Purduelist += addData_Content("購買人電話", AppMobile);
                Purduelist += addData_Content("購買人地址", AppAddress);
            }

            Purduelist += addData_Content("宮廟名稱", "大甲鎮瀾宮");
            Purduelist += addData_Content("訂單編號", Num2String);
            Purduelist += addData_Content("普度項目", PurdueString);

            Purduelist += addData_Content("祈福人姓名", Name);
            Purduelist += addData_Content("祈福人電話", Mobile);
            Purduelist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Purduelist += addData_Content("農曆時辰", BirthTime);
            Purduelist += addData_Content("數量", Count);
            Purduelist += addData_Content("金額", cost.ToString());
            Purduelist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Purduelist += addData_Content("受理日期", ChargeDate);

            Purduelist += "</li>";

            return Purduelist;
        }

        //public static string add_product_blessed(int applicantID, int adminID, string num2string, string name, string Mobile, string address, string zipcodes, int numproductBlessed, string productname, string ItemName, string Count, string cost, int status)
        //{
        //    string productlist = string.Empty;
        //    switch (adminID)
        //    {
        //        case 3:
        //            break;
        //        case 4:
        //            break;
        //    }

        //    productlist = "<div id='blessed_product-" + numproductBlessed + "'>" +
        //                    "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name public-lamp-circle-position'>" +
        //                            "<div class='number-circle'>" + (numproductBlessed + 1) + "</div>" +
        //                            "<span style='font-size:25px;font-weight:bold;margin-left:5px;'>" + productname + "</span></div></div>" +
        //                    "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>訂單編號：</div><p>" + num2string + "</p></div>" +
        //                    "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>購買姓名：</div><p>" + name + "</p></div>" +
        //                    "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>購買電話：</div><p>" + Mobile + "</p></div>" +
        //                    "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>居住地址：</div><p>" + zipcodes + " " + address + "</p></div>" +
        //                        "<div class='col-sm-12'>" +
        //                            "<div class='lamp-result-item-name'>購買數量：</div><p>" + Count + "</p></div>";
        //    if (ItemName != "")
        //    {
        //        productlist += "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>產品類別：</div><p>" + ItemName + "</p></div>";
        //    }


        //    productlist += "<div class='col-sm-12'>" +
        //                        "<div class='lamp-result-item-name'>金額：</div><p>" + cost + "</p></div>" +
        //                    "<div class='col-sm-12'>" +
        //                            "<div class='lamp-result-item-name'>狀態：</div><p>" + Status2String(status) + "</p></div>" +
        //                "<div class='col-sm-12'>" +
        //                        "<hr class='lamp-result-line' /></div></div>";
        //    return productlist;
        //}

        //public static string add_Supplies_blessed(string AppName, string AppMobile, string name, string Homenum, string Sex, string Count, string Remark, string Mobile, string address, string Birth, string Email, string zipCode, string Join_date, int numZampBlessed, string AppStatus, string Num2String, ref int total)
        //{
        //    string Supplieslist = string.Empty;

        //    int cost = 600;

        //    cost = cost * int.Parse(Count);

        //    total += cost;

        //    Supplieslist = "<li>";

        //    if (AppName != "" && AppMobile != "")
        //    {
        //        Supplieslist += addData_Content("購買人姓名", AppName);
        //        Supplieslist += addData_Content("購買人電話", AppMobile);
        //    }

        //    Supplieslist += addData_Content("宮廟名稱", "北港武德宮");
        //    Supplieslist += addData_Content("訂單編號", Num2String);
        //    Supplieslist += addData_Content("祈福人姓名", name);
        //    Supplieslist += addData_Content("祈福人電話", Mobile);
        //    Supplieslist += addData_Content("性別", Sex);
        //    Supplieslist += addData_Content("市話", Homenum);
        //    Supplieslist += addData_Content("E-mail", Email);
        //    Supplieslist += addData_Content("居住地址", zipCode + " " + address);
        //    Supplieslist += addData_Content("生日(農曆)", Birth);
        //    Supplieslist += addData_Content("數量", Count);
        //    Supplieslist += addData_Content("服務項目", Join_date);
        //    Supplieslist += addData_Content("金額", cost.ToString());
        //    Supplieslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
        //    Supplieslist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

        //    Supplieslist += "</li>";

        //    return Supplieslist;
        //}

        //public static string add_Supplies_blessed2(string AppName, string AppMobile, string name, string Homenum, string Sex, string Count, string Remark, string Mobile, string address, string Birth, string Email, string zipCode, string Join_date, int numZampBlessed, string AppStatus, string Num2String, ref int total)
        //{
        //    string Supplieslist = string.Empty;

        //    int cost = 600;

        //    cost = cost * int.Parse(Count);

        //    total += cost;

        //    Supplieslist = "<li>";

        //    if (AppName != "" && AppMobile != "")
        //    {
        //        Supplieslist += addData_Content("購買人姓名", AppName);
        //        Supplieslist += addData_Content("購買人電話", AppMobile);
        //    }

        //    Supplieslist += addData_Content("宮廟名稱", "北港武德宮");
        //    Supplieslist += addData_Content("訂單編號", Num2String);
        //    Supplieslist += addData_Content("祈福人姓名", name);
        //    Supplieslist += addData_Content("祈福人電話", Mobile);
        //    Supplieslist += addData_Content("性別", (Sex == "M" ? "善男" : "信女"));
        //    Supplieslist += addData_Content("市話", Homenum);
        //    Supplieslist += addData_Content("E-mail", Email);
        //    Supplieslist += addData_Content("居住地址", zipCode + " " + address);
        //    Supplieslist += addData_Content("生日(農曆)", Birth);
        //    Supplieslist += addData_Content("數量", Count);
        //    Supplieslist += addData_Content("服務項目", Join_date);
        //    Supplieslist += addData_Content("金額", cost.ToString());
        //    Supplieslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
        //    Supplieslist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

        //    Supplieslist += "</li>";

        //    return Supplieslist;
        //}

        public static string add_Supplies_sx_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime,
            string address, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(33, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "神霄玉府財神會館");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies2_sx_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime,
            string address, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(33, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "神霄玉府財神會館");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_Lk_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime, 
            string address, string Sendback, string rName, string rMobile, string rAddress, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, 
            string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(21, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "鹿港城隍廟");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("贈品處理方式", Sendback == "Y" ? "寄回" : "不寄回");

            if (Sendback == "Y")
            {
                //cost += 100;
                lightslist += addData_Content("收件人姓名", rName);
                lightslist += addData_Content("收件人電話", rMobile);
                lightslist += addData_Content("收件人地址", rAddress);
            }

            total += cost;

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_Fw_blessed(string AppName, string AppMobile, string AppAddress, string Name, string Mobile, string Sex, string Birth, string LeapMonth, 
            string BirthTime, string address, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(15, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("購買人地址", AppAddress);
            }

            lightslist += addData_Content("宮廟名稱", "斗六五路財神宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);

            total += cost;

            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_jb_blessed(string AppName, string AppMobile, string ReceiptName, string AppZipCode, string AppAddress, string name, string Mobile, 
            string Sex, string Birth, string LeapMonth, string BirthTime, string zipCode, string address, string SuppliesType, int numZampBlessed, string AppStatus, 
            string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(29, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("收件人姓名", ReceiptName);
                lightslist += addData_Content("收件人地址", AppAddress);
            }

            lightslist += addData_Content("宮廟名稱", "進寶財神廟");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", zipCode + " " + address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_ma_blessed(string AppName, string AppMobile, string name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime, 
            string sBirth, string Email, string HomeNum, string Remark, string address, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, 
            string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(23, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "玉敕大樹朝天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("國曆生日", sBirth);
            lightslist += addData_Content("祈福人信箱", Email);
            lightslist += addData_Content("祈福人市話", HomeNum);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("備註", Remark);
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_ty_blessed(string AppName, string AppMobile, string AppsBirth, string AppAddress, string name, string Mobile, string sBirth, 
            string address, string SuppliesType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(14, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("國曆生日", AppsBirth);
                lightslist += addData_Content("購買人地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人國曆生日", sBirth);
            lightslist += addData_Content("祈福人居住地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies2_ty_blessed(string AppName, string AppMobile, string AppEmail, string AppBirth, string AppAddress, string name, string Mobile, 
            string Birth, string address, string SuppliesType, string Remark, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(14, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("購買人信箱", AppEmail);
                lightslist += addData_Content("國曆生日", AppBirth);
                lightslist += addData_Content("購買人地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("國曆生日", Birth);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("備註", Remark);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies3_ty_blessed(string AppName, string AppMobile, string AppEmail, string AppBirth, string AppAddress, string name, string Mobile,
            string Birth, string address, string SuppliesType, string Remark, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string SuppliesString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetSuppliesCost(14, SuppliesType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("購買人信箱", AppEmail);
                lightslist += addData_Content("國曆生日", AppBirth);
                lightslist += addData_Content("購買人地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("國曆生日", Birth);
            lightslist += addData_Content("祈福人地址", address);
            lightslist += addData_Content("服務項目", SuppliesString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("備註", Remark);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Supplies_blessed(int AdminID, string AppName, string AppMobile, string AppEmail, string name, string Mobile, string Num2String, string ChargeDate, 
            string SuppliesType, string SuppliesString, string Sex, string Birth, string LeapMonth, string BirthTime, string Homenum, string Email, string address, string zipCode, 
            int numZampBlessed, string AppStatus, string Count, string Remark, ref int total)
        {
            string Supplieslist = string.Empty;

            int count = 1;
            int.TryParse(Count, out count);

            int cost = 650 * (count < 1 ? 1 : count);

            total += cost;

            Supplieslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                Supplieslist += addData_Content("購買人姓名", AppName);
                Supplieslist += addData_Content("購買人電話", AppMobile);
            }

            if (AppEmail != "")
            {
                Supplieslist += addData_Content("購買人Email", AppEmail);
            }

            Supplieslist += addData_Content("宮廟名稱", TempleName(AdminID));
            Supplieslist += addData_Content("訂單編號", Num2String);
            Supplieslist += addData_Content("祈福人姓名", name);
            Supplieslist += addData_Content("祈福人電話", Mobile);
            if (Sex == "M")
            {
                Sex = "善男";
            }
            else if (Sex == "F")
            {
                Sex = "信女";
            }
            Supplieslist += addData_Content("祈福人性別", Sex);
            Supplieslist += addData_Content("祈福人市話", Homenum);
            Supplieslist += addData_Content("祈福人信箱", Email);
            Supplieslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            Supplieslist += addData_Content("農曆時辰", BirthTime);
            Supplieslist += addData_Content("數量", Count);
            Supplieslist += addData_Content("祈福地址", address);
            Supplieslist += addData_Content("服務項目", SuppliesString);
            Supplieslist += addData_Content("金額", cost.ToString());
            Supplieslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            Supplieslist += addData_Content("受理日期", ChargeDate);
            Supplieslist += addData_Content("備註", Remark.Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", ""));

            Supplieslist += "</li>";

            return Supplieslist;
        }

        public static string add_EmperorGuansheng_ty_blessed(string AppName, string AppMobile, string AppBirth, string AppEmail, string AppZipCode, string AppAddress, string name, 
            string Mobile, string Birth, string zipCode, string address, string EmperorGuanshengType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, 
            string EmperorGuanshengString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetEmperorGuanshengCost(14, EmperorGuanshengType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("國曆生日", AppBirth);
                lightslist += addData_Content("購買人信箱", AppEmail);
                lightslist += addData_Content("購買人居住地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "桃園威天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("國曆生日", Birth );
            lightslist += addData_Content("居住地址", zipCode + " " + address);
            lightslist += addData_Content("服務項目", EmperorGuanshengString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lingbaolidou_ma_blessed(string AppName, string AppMobile, string AppEmail, string AppZipCode, string AppAddress, string name, string Mobile, 
            string Sex, string Birth, string LeapMonth, string BirthTime, string sBirth, string zipCode, string address, string LingbaolidouType, int numZampBlessed, 
            string AppStatus, string Num2String, string ChargeDate, string LingbaolidouString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLingbaolidouCost(23, LingbaolidouType);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("購買人信箱", AppEmail);
                lightslist += addData_Content("購買人居住地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "玉敕大樹朝天宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("時辰", BirthTime);
            lightslist += addData_Content("國曆生日", sBirth);
            lightslist += addData_Content("居住地址", zipCode + " " + address);
            lightslist += addData_Content("服務項目", LingbaolidouString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_TaoistJiaoCeremony_da_blessed(string AppName, string AppMobile, string AppEmail, string AppAddress, string Name, string Name2, 
            string Name3, string Name4, string Name5, string Name6, string Mobile, string Address, string Sendback, string rName, string rMobile, string rAddress, 
            string TaoistJiaoCeremonyType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string TaoistJiaoCeremonyString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetTaoistJiaoCeremonyCost(3, TaoistJiaoCeremonyType);
            cost += Sendback == "Y" ? 250 : 0;

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
                lightslist += addData_Content("購買人信箱", AppEmail);
                lightslist += addData_Content("購買人地址", AppAddress);
            }


            lightslist += addData_Content("宮廟名稱", "大甲鎮瀾宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);

            switch (TaoistJiaoCeremonyType)
            {
                case "1":
                    lightslist += addData_Content("祈福人姓名2", Name2);
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);

                    lightslist += addData_Content("寄送方式", Sendback == "Y" ? "寄回(加收運費250元)" : "不寄回(會轉送給弱勢團體)");
                    if (Sendback == "Y")
                    {
                        lightslist += addData_Content("收件人姓名", rName);
                        lightslist += addData_Content("收件人電話", rMobile);
                        lightslist += addData_Content("收件人地址", rAddress);
                    }
                    break;
                case "2":
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
                case "3":
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
                case "4":
                    lightslist += addData_Content("祈福人姓名2", Name2);
                    lightslist += addData_Content("祈福人姓名3", Name3);
                    lightslist += addData_Content("祈福人姓名4", Name4);
                    lightslist += addData_Content("祈福人姓名5", Name5);
                    lightslist += addData_Content("祈福人姓名6", Name6);
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
                case "5":
                    lightslist += addData_Content("祈福人姓名2", Name2);
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
                case "6":
                    lightslist += addData_Content("祈福人姓名2", Name2);
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
                case "7":
                    lightslist += addData_Content("祈福人姓名2", Name2);
                    lightslist += addData_Content("祈福人電話", Mobile);
                    lightslist += addData_Content("祈福人地址", Address);
                    break;
            }

            lightslist += addData_Content("服務項目", TaoistJiaoCeremonyString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("受理日期", ChargeDate);
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_Lybc_dh_blessed(string AppName, string AppMobile, string Name, string Mobile, string Sex, string Birth, string LeapMonth, string BirthTime, string Address,
            string LybcType, int numZampBlessed, string AppStatus, string Num2String, string ChargeDate, string LybcString, ref int total)
        {
            string lightslist = string.Empty;
            int cost = GetLybcCost(LybcType, 16);

            total += cost;

            lightslist = "<li>";

            if (AppName != "" && AppMobile != "")
            {
                lightslist += addData_Content("購買人姓名", AppName);
                lightslist += addData_Content("購買人電話", AppMobile);
            }

            lightslist += addData_Content("宮廟名稱", "台東東海龍門天聖宮");
            lightslist += addData_Content("訂單編號", Num2String);
            lightslist += addData_Content("祈福人姓名", Name);
            lightslist += addData_Content("祈福人電話", Mobile);
            lightslist += addData_Content("祈福人性別", Sex);
            lightslist += addData_Content("農曆生日", Birth + (LeapMonth == "Y" ? " 閏月" : ""));
            lightslist += addData_Content("農曆時辰", BirthTime);
            lightslist += addData_Content("祈福人地址", Address);
            lightslist += addData_Content("服務項目", LybcString);
            lightslist += addData_Content("金額", cost.ToString());
            lightslist += addData_Content("狀態", Status2String(int.Parse(AppStatus)));
            lightslist += addData_Content("受理日期", ChargeDate);

            lightslist += "</li>";

            return lightslist;
        }

        public static string add_product_blessed(string Num2String, string ChargeDate, string name, string Mobile, string address, string name_A, string name_B, string cost_B, int count_B, 
            string img_B)
        {
            string productlist = string.Empty;

            productlist = "<li>";

            productlist += addData_Content("受理日期", ChargeDate);
            productlist += addData_Content("訂單編號", Num2String);
            productlist += addData_Content("購買人", name);
            productlist += addData_Content("連絡電話", Mobile);
            productlist += addData_Content("收件地址", address);

            if (count_B > 0)
            {
                productlist += addData_Content("商品名稱", name_A);
                if (name_B != null)
                {
                    productlist += addData_Content("商品類別", name_B);
                }
                productlist += addData_Content("商品金額", cost_B);
                productlist += addData_Content("商品數量", count_B.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_B + "\" style=\"width: 50%\" alt=\"\">");
            }

            productlist += "</li>";
            return productlist;
        }

        public static string add_product_Pilgrimage_blessed(string Num2String, string ChargeDate, string name, string Mobile, string address, string[] SculptureName, string name_A, string cost_A, 
            int count_A, string img_A, string name_B, string cost_B, int count_B, string img_B)
        {
            string productlist = string.Empty;

            productlist = "<li>";

            productlist += addData_Content("受理日期", ChargeDate);
            productlist += addData_Content("訂單編號", Num2String);
            productlist += addData_Content("購買人", name);
            productlist += addData_Content("連絡電話", Mobile);
            productlist += addData_Content("收件地址", address);

            if (count_A > 0)
            {
                productlist += addData_Content("商品名稱", name_A);
                productlist += addData_Content("商品金額", cost_A);
                productlist += addData_Content("商品數量", count_A.ToString());

                for (int i = 0; i < SculptureName.Length; i++)
                {
                    productlist += addData_Content("屬名雕刻服務-編號" + (i + 1), SculptureName[i]);
                }
                productlist += addData_Content("商品圖片", "<img src=\"" + img_A + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_B > 0)
            {
                productlist += addData_Content("商品名稱", name_B);
                productlist += addData_Content("商品金額", cost_B);
                productlist += addData_Content("商品數量", count_B.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_B + "\" style=\"width: 50%\" alt=\"\">");
            }

            productlist += "</li>";
            return productlist;
        }

        public static string add_product_Moneymother_blessed(string Num2String, string ChargeDate, string Appname, string Mobile, string address, string name, int cost, int count, 
            string img)
        {
            string productlist = string.Empty;

            productlist = "<li>";

            productlist += addData_Content("受理日期", ChargeDate);
            productlist += addData_Content("訂單編號", Num2String);
            productlist += addData_Content("購買人姓名", Appname);
            productlist += addData_Content("購買人電話", Mobile);
            productlist += addData_Content("收件地址", address);

            if (count > 0)
            {
                productlist += addData_Content("商品名稱", name);
                productlist += addData_Content("商品金額", cost.ToString());
                productlist += addData_Content("商品數量", count.ToString());
                if (name.IndexOf("大嘴貓") >= 0)
                {
                    productlist += addData_Content("商品圖片", "<br><img src=\"" + img + "\" style=\"width: 100%\" alt=\"\">");
                }
                else
                {
                    productlist += addData_Content("商品圖片", "<img src=\"" + img + "\" style=\"width: 50%\" alt=\"\">");
                }
            }

            productlist += "</li>";
            return productlist;
        }

        public static string add_product_Moneymother_blessed(string Num2String, string ChargeDate, string name, string Mobile, string address, string name_A, int cost_A, int count_A, string img_A, 
            string name_B, int cost_B, int count_B, string img_B, string name_C, int cost_C, int count_C, string img_C, string name_D, int cost_D, int count_D, string img_D, 
            string name_E, int cost_E, int count_E, string img_E, string name_F, int cost_F, int count_F, string img_F, string name_G, int cost_G, int count_G, string img_G)
        {
            string productlist = string.Empty;

            productlist = "<li>";

            productlist += addData_Content("受理日期", ChargeDate);
            productlist += addData_Content("訂單編號", Num2String);
            productlist += addData_Content("購買人", name);
            productlist += addData_Content("連絡電話", Mobile);
            productlist += addData_Content("收件地址", address);

            if (count_A > 0)
            {
                productlist += addData_Content("商品名稱", name_A);
                productlist += addData_Content("商品金額", cost_A.ToString());
                productlist += addData_Content("商品數量", count_A.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_A + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_B > 0)
            {
                productlist += addData_Content("商品名稱", name_B);
                productlist += addData_Content("商品金額", cost_B.ToString());
                productlist += addData_Content("商品數量", count_B.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_B + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_C > 0)
            {
                productlist += addData_Content("商品名稱", name_C);
                productlist += addData_Content("商品金額", cost_C.ToString());
                productlist += addData_Content("商品數量", count_C.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_C + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_D > 0)
            {
                productlist += addData_Content("商品名稱", name_D);
                productlist += addData_Content("商品金額", cost_D.ToString());
                productlist += addData_Content("商品數量", count_D.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_D + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_E > 0)
            {
                productlist += addData_Content("商品名稱", name_E);
                productlist += addData_Content("商品金額", cost_E.ToString());
                productlist += addData_Content("商品數量", count_E.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_E + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_F > 0)
            {
                productlist += addData_Content("商品名稱", name_F);
                productlist += addData_Content("商品金額", cost_F.ToString());
                productlist += addData_Content("商品數量", count_F.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_F + "\" style=\"width: 50%\" alt=\"\">");
            }

            if (count_G > 0)
            {
                productlist += addData_Content("商品名稱", name_G);
                productlist += addData_Content("商品金額", cost_G.ToString());
                productlist += addData_Content("商品數量", count_G.ToString());
                productlist += addData_Content("商品圖片", "<img src=\"" + img_G + "\" style=\"width: 50%\" alt=\"\">");
            }

            productlist += "</li>";
            return productlist;
        }

        public static string addData_Content(string Name, string Value)
        {
            string result = string.Empty;

            result = $"<div><div>{Name}</div><div>{Value}</div></div>";

            return result;
        }

        public static void TabletChecked(string purdueItem, string deathName, string firstName, string momName, string lastName, string reason, string licenseNum, 
            string deathaddress, string purdueItem1, string deathName1, string firstName1, string momName1, string lastName1, string reason1, string licenseNum1, 
            string deathaddress1, ref string Purduelist)
        {
            Purduelist += addData_Content("超薦項目", purdueItem);
            purdueItem = purdueItem.IndexOf("超薦") >= 0 ? purdueItem.Substring(5, purdueItem.Length - 5) : purdueItem;
            switch (purdueItem)
            {
                case "顯考O公O府君":
                    Purduelist += addData_Content("顯考(姓氏)公", firstName);
                    Purduelist += addData_Content("(名字)府君", lastName);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "顯妣O母 氏OO夫人":
                    Purduelist += addData_Content("(夫姓)母", momName);
                    Purduelist += addData_Content("(本姓)氏", firstName);
                    Purduelist += addData_Content("(名字)夫人", lastName);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "O氏歷代祖先":
                    Purduelist += addData_Content("(姓氏)氏", firstName);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "空白牌":
                    Purduelist += addData_Content("超薦事由", reason);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "車號OOO車輛誤傷之生靈":
                    Purduelist += addData_Content("車牌(車牌數字)", licenseNum);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "無緣子女":
                    Purduelist += addData_Content("無緣子女(姓名)", deathName);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "累劫冤親債主":
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "地基主":
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "過去飼養一切動物之靈":
                    Purduelist += addData_Content("寵物(姓名)", deathName);
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
                case "十方法界一切有情眾生":
                    Purduelist += addData_Content("被超薦者地址", deathaddress);
                    break;
            }

            if (purdueItem1 != "")
            {
                Purduelist += addData_Content("超薦項目", purdueItem1);
                purdueItem1 = purdueItem1.IndexOf("超薦") >= 0 ? purdueItem1.Substring(5, purdueItem1.Length - 5) : purdueItem1;
                switch (purdueItem1)
                {
                    case "顯考O公O府君":
                        Purduelist += addData_Content("顯考(姓氏)公", firstName1);
                        Purduelist += addData_Content("(名字)府君", lastName1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "顯妣O母 氏OO夫人":
                        Purduelist += addData_Content("(夫姓)母", momName1);
                        Purduelist += addData_Content("(本姓)氏", firstName1);
                        Purduelist += addData_Content("(名字)夫人", lastName1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "O氏歷代祖先":
                        Purduelist += addData_Content("(姓氏)氏", firstName1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "空白牌":
                        Purduelist += addData_Content("超薦事由", reason1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "車號OOO車輛誤傷之生靈":
                        Purduelist += addData_Content("車牌(車牌數字)", licenseNum1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "無緣子女":
                        Purduelist += addData_Content("無緣子女(姓名)", deathName1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "累劫冤親債主":
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "地基主":
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "過去飼養一切動物之靈":
                        Purduelist += addData_Content("寵物(姓名)", deathName1);
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                    case "十方法界一切有情眾生":
                        Purduelist += addData_Content("被超薦者地址", deathaddress1);
                        break;
                }
            }
        }

        public class AjaxPageHandler
        {
            public void checkedapplicant(BasePage basePage)
            {
                basePage.mJSonHelper.AddContent("StatusCode", 0);
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                string StartYear = "2022";

                string m_Name = basePage.Request["m_Name"].ToString().Trim();
                string m_Mobile = basePage.Request["m_Mobile"].ToString().Trim();
                //string m_Num2String = basePage.Request["m_Num2String"].ToString().Trim();
                string Datalist = string.Empty;
                int Total = 0;
                string Year = dtNow.Year.ToString();
                string[] Yearlist = { "2025", "2024" };

                LightDAC objLightDAC = new LightDAC(basePage);
                DataTable dtAdminID = new DataTable();
                DataTable dtapplecantinfo = new DataTable();
                DataTable dtdatainfo = new DataTable();

                basePage.SaveRequestLog(basePage.Request.Url.ToString());

                try
                {
                    for (int y = 0; y < Yearlist.Length; y++)
                    {
                        dtAdminID = objLightDAC.GetAdminList(9);
                        if (dtAdminID.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtAdminID.Rows.Count; k++)
                            {
                                int adminID = int.Parse(dtAdminID.Rows[k]["AdminID"].ToString());
                                if (adminID > 0)
                                {
                                    //點燈服務
                                    //Year = "2024";
                                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfo(m_Mobile, adminID);
                                    //switch (adminID)
                                    //{
                                    //    //case 10:
                                    //    //    //台南正統鹿耳門聖母廟 l=1:一般點燈 l=2:月老姻緣燈 l=3:月老姻緣燈-台哥大專屬
                                    //    //    for (int l = 1; l < 3; l++)
                                    //    //    {
                                    //    //        dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfo(m_Name, m_Mobile, adminID, l, Year);
                                    //    //    }
                                    //    //    break;
                                    //    //case 14:
                                    //    //    //桃園威天宮 l=1:一般點燈 l=2:孝親祈福燈
                                    //    //    for (int l = 1; l < 3; l++)
                                    //    //    {
                                    //    //        dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfo(m_Name, m_Mobile, adminID, l, Year);
                                    //    //        if (dtapplecantinfo.Rows.Count > 0)
                                    //    //        {
                                    //    //            for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                    //    //            {
                                    //    //                string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();

                                    //    //                if (LightsType == "21")
                                    //    //                {
                                    //    //                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    //    //                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    //    //                    string AppBirth = dtapplecantinfo.Rows[i]["AppBirth"].ToString();
                                    //    //                    string AppLeapMonth = dtapplecantinfo.Rows[i]["AppLeapMonth"].ToString();
                                    //    //                    string AppBirthTime = dtapplecantinfo.Rows[i]["AppBirthTime"].ToString();
                                    //    //                    string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                    //    //                    string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                                    //    //                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    //    //                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    //    //                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    //    //                    string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                    //    //                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    //    //                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                    //    //                    string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                    //    //                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    //    //                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    //    //                    string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                                    //    //                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    //    //                    Datalist += add_Lights_ty_mom_blessed(AppName, AppMobile, AppBirth, AppLeapMonth, AppBirthTime, AppZipCode, AppAddress, Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, LightsType, i, AppStatus, Num2String, LightsString, ref Total);
                                    //    //                }
                                    //    //                else
                                    //    //                {
                                    //    //                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    //    //                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    //    //                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    //    //                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    //    //                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    //    //                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    //    //                    string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                    //    //                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    //    //                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                    //    //                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    //    //                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    //    //                    string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                                    //    //                    Datalist += add_Lights_ty_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, ZipCode, LightsType, i, AppStatus, Num2String, LightsString, ref Total);
                                    //    //                }

                                    //    //            }
                                    //    //        }
                                    //    //    }
                                    //    //    break;
                                    //    default:
                                    //        //bool checkedyear = false;
                                    //        //string[] yearlist = { "2025", "2024" };
                                    //        //string startDate = "2024/11/01 00:00:00";
                                    //        //int ijj = DateTime.Compare(DateTime.Parse(startDate), dtNow);
                                    //        //if (DateTime.Compare(DateTime.Parse(startDate), dtNow) < 0 || basePage.Request["ad"] == "2")
                                    //        //{
                                    //        //    checkedyear = true;
                                    //        //}

                                    //        //if (checkedyear)
                                    //        //{
                                    //        //    for (int i = 0; i < yearlist.Length; i++)
                                    //        //    {
                                    //        //        GetLightsInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, yearlist[i], ref Datalist, ref Total);
                                    //        //    }
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        //    GetLightsInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, Year, ref Datalist, ref Total);
                                    //        //}
                                    //        break;
                                    //}

                                    if (adminID > 0 && adminID == 14)
                                    {
                                        GetLightsInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, 1, adminID, Yearlist[y], ref Datalist, ref Total);
                                        GetLightsInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, 2, adminID, Yearlist[y], ref Datalist, ref Total);
                                    }
                                    else
                                    {
                                        GetLightsInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, 1, adminID, Yearlist[y], ref Datalist, ref Total);
                                    }

                                    //普度服務
                                    GetPurdueInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, Yearlist[y], ref Datalist, ref Total);
                                    //for (int i = 0; i < Yearlist.Length; i++)
                                    //{
                                    //    GetPurdueInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, Yearlist[i], ref Datalist, ref Total);
                                    //}

                                    //下元補庫-北港武德宮
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 1, Yearlist[y], ref Datalist, ref Total);

                                    //呈疏補庫-北港武德宮
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 2, Yearlist[y], ref Datalist, ref Total);

                                    //企業補財庫-北港武德宮
                                    //for (int i = 0; i < Yearlist.Length; i++)
                                    //{
                                    //    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 3, Yearlist[i], ref Datalist, ref Total);
                                    //}
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 3, Yearlist[y], ref Datalist, ref Total);

                                    //補財庫服務 (天赦日補運)
                                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_ty(m_Name, m_Mobile, adminID, 2);
                                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 4, Yearlist[y]);
                                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_tyfromNum2String(m_Num2String, adminID, 1, Year);
                                    //if (dtapplecantinfo.Rows.Count > 0)
                                    //{
                                    //    //桃園威天宮
                                    //    for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                    //    {
                                    //        string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    //        string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    //        string AppsBirth = dtapplecantinfo.Rows[i]["AppsBirth"].ToString();
                                    //        string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                    //        string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    //        string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    //        string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    //        string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    //        string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    //        string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    //        string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                    //        string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                    //        string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    //        string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                                    //        if (Yearlist[])
                                    //        Datalist += add_Supplies_ty_blessed(AppName, AppMobile, AppsBirth, AppAddress, Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                    //    }
                                    //}
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 4, Yearlist[y], ref Datalist, ref Total);

                                    //法會服務 (天赦日祭改)
                                    //for (int i = 0; i < Yearlist.Length; i++)
                                    //{
                                    //    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 5, Yearlist[i], ref Datalist, ref Total);
                                    //}
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 5, Yearlist[y], ref Datalist, ref Total);

                                    //關聖帝君聖誕
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_EmperorGuanshengInfo(m_Name, m_Mobile, adminID, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //桃園威天宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string AppBirth = dtapplecantinfo.Rows[i]["AppBirth"].ToString();
                                            string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                            string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string EmperorGuanshengType = dtapplecantinfo.Rows[i]["EmperorGuanshengType"].ToString();
                                            string EmperorGuanshengString = dtapplecantinfo.Rows[i]["EmperorGuanshengString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_EmperorGuansheng_ty_blessed(AppName, AppMobile, AppBirth, AppEmail, AppZipCode, AppAddress, Name, Mobile, Birth, ZipCode, Address, EmperorGuanshengType, i, AppStatus, Num2String, ChargeDate, EmperorGuanshengString, ref Total);
                                        }
                                    }

                                    //靈寶禮斗
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_LingbaolidouInfo(m_Name, m_Mobile, adminID, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //玉敕大樹朝天宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                            string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                            string sBirth = dtapplecantinfo.Rows[i]["sBirth"].ToString();
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string LingbaolidouType = dtapplecantinfo.Rows[i]["LingbaolidouType"].ToString();
                                            string LingbaolidouString = dtapplecantinfo.Rows[i]["LingbaolidouString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Lingbaolidou_ma_blessed(AppName, AppMobile, AppEmail, AppZipCode, AppAddress, Name, Mobile, Sex, Birth, LeapMonth,
                                                BirthTime, sBirth, ZipCode, Address, LingbaolidouType, i, AppStatus, Num2String, ChargeDate, LingbaolidouString, ref Total);
                                        }
                                    }

                                    //補財庫服務 (九九重陽天赦日補運)
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 71, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //桃園威天宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                            string AppBirth = dtapplecantinfo.Rows[i]["AppsBirth"].ToString();
                                            string AppLeapMonth = dtapplecantinfo.Rows[i]["AppLeapMonth"].ToString();
                                            string AppBirthTime = dtapplecantinfo.Rows[i]["AppBirthTime"].ToString();
                                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            string Birth = dtapplecantinfo.Rows[i]["sBirth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Supplies2_ty_blessed(AppName, AppMobile, AppEmail, AppBirth, AppAddress, Name, Mobile, Birth, Address, SuppliesType, Remark,
                                                i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                        }
                                    }

                                    //七朝清醮
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_TaoistJiaoCeremonyInfo(m_Name, m_Mobile, adminID, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //大甲鎮瀾宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());

                                            string Name = dtapplecantinfo.Rows[i]["Name"].ToString();
                                            string Name2 = dtapplecantinfo.Rows[i]["Name2"].ToString();
                                            string Name3 = dtapplecantinfo.Rows[i]["Name3"].ToString();
                                            string Name4 = dtapplecantinfo.Rows[i]["Name4"].ToString();
                                            string Name5 = dtapplecantinfo.Rows[i]["Name5"].ToString();
                                            string Name6 = dtapplecantinfo.Rows[i]["Name6"].ToString();

                                            if (Name != "")
                                            {
                                                Name = UtilDataMask.MaskName(Name);
                                            }
                                            if (Name2 != "")
                                            {
                                                Name2 = UtilDataMask.MaskName(Name2);
                                            }
                                            if (Name3 != "")
                                            {
                                                Name3 = UtilDataMask.MaskName(Name3);
                                            }
                                            if (Name4 != "")
                                            {
                                                Name4 = UtilDataMask.MaskName(Name4);
                                            }
                                            if (Name5 != "")
                                            {
                                                Name5 = UtilDataMask.MaskName(Name5);
                                            }
                                            if (Name6 != "")
                                            {
                                                Name6 = UtilDataMask.MaskName(Name6);
                                            }

                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                                            string Sendback = dtapplecantinfo.Rows[i]["Sendback"].ToString();
                                            string rName = string.Empty;
                                            string rMobile = string.Empty;
                                            string rAddress = string.Empty;
                                            if (Sendback == "Y")
                                            {
                                                rName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["rName"].ToString());
                                                rMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["rMobile"].ToString(), 5, 3);
                                                rAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["rAddress"].ToString());
                                            }
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string TaoistJiaoCeremonyType = dtapplecantinfo.Rows[i]["TaoistJiaoCeremonyType"].ToString();
                                            string TaoistJiaoCeremonyString = dtapplecantinfo.Rows[i]["TaoistJiaoCeremonyString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_TaoistJiaoCeremony_da_blessed(AppName, AppMobile, AppEmail, AppAddress, Name, Name2, Name3, Name4, Name5, Name6, Mobile,
                                                Address, Sendback, rName, rMobile, rAddress, TaoistJiaoCeremonyType, i, AppStatus, Num2String, ChargeDate, TaoistJiaoCeremonyString, ref Total);
                                        }
                                    }

                                    //護國息災梁皇大法會
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Lybc(m_Name, m_Mobile, adminID, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //大甲鎮瀾宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                                            string Name = dtapplecantinfo.Rows[i]["Name"].ToString();
                                            if (Name != "")
                                            {
                                                Name = UtilDataMask.MaskName(Name);
                                            }
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string LybcType = dtapplecantinfo.Rows[i]["LybcType"].ToString();
                                            string LybcString = dtapplecantinfo.Rows[i]["LybcString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Lybc_dh_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, LybcType, i, AppStatus, Num2String, ChargeDate, 
                                                LybcString, ref Total);
                                        }
                                    }

                                    //補財庫服務
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 9, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        switch (adminID)
                                        {
                                            case 15:
                                                //斗六五路財神宮
                                                for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                                {
                                                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                                    string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                                    string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                                    string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                                    string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                                    string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                                    string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                                    string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                                    Datalist += add_Supplies_Fw_blessed(AppName, AppMobile, AppAddress, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, 
                                                        SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                                }
                                                break;
                                            case 21:
                                                //鹿港城隍廟
                                                for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                                {
                                                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                                    string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                                    string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                                    //string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                                    string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                                    string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                                    string Sendback = dtapplecantinfo.Rows[i]["AppSendback"].ToString();
                                                    string rName = dtapplecantinfo.Rows[i]["ReceiptName"].ToString();
                                                    string rMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["ReceiptMobile"].ToString(), 5, 3);
                                                    string rAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["ApprAddress"].ToString());
                                                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                                    string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                                    Datalist += add_Supplies_Lk_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, Sendback, rName, rMobile,
                                                        rAddress, SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                                }
                                                break;
                                        }
                                    }

                                    //赦罪補庫服務
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 111, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //神霄玉府財神會館
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            //string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Supplies_sx_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, SuppliesType, i, AppStatus,
                                                Num2String, ChargeDate, SuppliesString, ref Total);
                                        }
                                    }

                                    //供香祈福服務
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 121, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //神霄玉府財神會館
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            //string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Supplies2_sx_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, SuppliesType, i, AppStatus,
                                                Num2String, ChargeDate, SuppliesString, ref Total);
                                        }
                                    }

                                    //補財庫服務 (天公生招財補運)
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 18, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //桃園威天宮
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                            string AppBirth = dtapplecantinfo.Rows[i]["AppsBirth"].ToString();
                                            string AppLeapMonth = dtapplecantinfo.Rows[i]["AppLeapMonth"].ToString();
                                            string AppBirthTime = dtapplecantinfo.Rows[i]["AppBirthTime"].ToString();
                                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            string Birth = dtapplecantinfo.Rows[i]["sBirth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Supplies3_ty_blessed(AppName, AppMobile, AppEmail, AppBirth, AppAddress, Name, Mobile, Birth, Address, SuppliesType, Remark,
                                                i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                        }
                                    }

                                    //供香祈福服務
                                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 121, Yearlist[y]);
                                    if (dtapplecantinfo.Rows.Count > 0)
                                    {
                                        //神霄玉府財神會館
                                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                        {
                                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                            //string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString().Replace("\n", "<br>").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                            Datalist += add_Supplies2_sx_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Address, SuppliesType, i, AppStatus,
                                                Num2String, ChargeDate, SuppliesString, ref Total);
                                        }
                                    }

                                    //法會服務 (財神賜福-消災補庫法會)
                                    //for (int i = 0; i < Yearlist.Length; i++)
                                    //{
                                    //    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 5, Yearlist[i], ref Datalist, ref Total);
                                    //}
                                    GetSuppliesInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, 10, Yearlist[y], ref Datalist, ref Total);
                                }
                            }
                        }

                        //Year = dtNow.Year.ToString();
                        dtapplecantinfo = new DataTable();
                        dtAdminID = objLightDAC.GetAdminList(8);
                        if (dtAdminID.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtAdminID.Rows.Count; k++)
                            {
                                int adminID = int.Parse(dtAdminID.Rows[k]["AdminID"].ToString());
                                if (adminID > 0)
                                {
                                    //for (int i = 0; i < Yearlist.Length; i++)
                                    //{
                                    //    GetProductInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, Yearlist[i], ref Datalist, ref Total);
                                    //}
                                    GetProductInfo(basePage, dtapplecantinfo, m_Name, m_Mobile, adminID, Yearlist[y], ref Datalist, ref Total);
                                }
                            }
                        }
                    }

                    if ((Datalist != ""))
                    {
                        basePage.mJSonHelper.AddContent("StatusCode", 1);
                        basePage.mJSonHelper.AddContent("Datalist", Datalist);
                        basePage.mJSonHelper.AddContent("Total", Total);
                    }
                    else
                    {
                        basePage.mJSonHelper.AddContent("DataInfo", -1);
                    }

                }
                catch (Exception ex)
                {
                    basePage.SaveErrorLog(ex);
                }
            }
        }

        protected static void GetLightsInfo(BasePage basePage, DataTable dtapplecantinfo, string m_Name, string m_Mobile, int type, int adminID, string Year, ref string Datalist, 
            ref int Total)
        {
            LightDAC objLightDAC = new LightDAC(basePage);

            dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfo(m_Name, m_Mobile, type, adminID, Year);
            //dtapplecantinfo = objLightDAC.Getapplicantinfo_LightsInfofromNum2String(m_Num2String, adminID);
            if (dtapplecantinfo.Rows.Count > 0)
            {
                switch (adminID)
                {
                    case 3:
                        //大甲鎮瀾宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string discountType = dtapplecantinfo.Rows[i]["DiscountType"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_da_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                                AppStatus, Num2String, LightsString, ChargeDate, discountType, ref Total);
                        }
                        break;
                    case 4:
                        //新港奉天宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_h_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                                AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 6:
                        //北港武德宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_wu_blessed(AppName, AppMobile, Name, Homenum, Sex, Count, Remark, Mobile, Address, Birth, LeapMonth,
                                BirthTime, Email, ZipCode, LightsType, i, AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 8:
                        //西螺福興宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_Fu_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                                AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 10:
                        //台南正統鹿耳門聖母廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string PetName = dtapplecantinfo.Rows[i]["PetName"].ToString();
                            string PetType = dtapplecantinfo.Rows[i]["PetType"].ToString();
                            string Msg = dtapplecantinfo.Rows[i]["Msg"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_Luer_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode,
                                LightsType, i, AppStatus, Num2String, LightsString, ChargeDate, PetName, PetType, Msg, ref Total);
                        }
                        break;
                    case 14:
                        //桃園威天宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();

                            if (LightsType == "21")
                            {
                                string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                string AppBirth = dtapplecantinfo.Rows[i]["AppBirth"].ToString();
                                string AppLeapMonth = dtapplecantinfo.Rows[i]["AppLeapMonth"].ToString();
                                string AppBirthTime = dtapplecantinfo.Rows[i]["AppBirthTime"].ToString();
                                string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                                string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                                string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                                string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                Datalist += add_Lights_ty_mom_blessed(AppName, AppMobile, AppBirth, AppLeapMonth, AppBirthTime, AppZipCode, AppAddress,
                                    Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, LightsType, i, AppStatus, Num2String, LightsString, ChargeDate,
                                    ref Total);
                            }
                            else
                            {
                                string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                                string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                                string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                Datalist += add_Lights_ty_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType,
                                    i, AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                            }

                        }
                        //for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        //{
                        //    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                        //    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                        //    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                        //    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                        //    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                        //    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                        //    string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                        //    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                        //    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                        //string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                        //    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                        //    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                        //    string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                        //    string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                        //    Datalist += add_Lights_ty_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                        //    AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                        //}
                        break;
                    case 15:
                        //斗六五路財神宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string PetName = dtapplecantinfo.Rows[i]["PetName"].ToString();
                            string PetType = dtapplecantinfo.Rows[i]["PetType"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_Fw_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                                AppStatus, Num2String, LightsString, ChargeDate, PetName, PetType, ref Total);
                        }
                        break;
                    case 16:
                        //台東東海龍門天聖宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_dh_blessed(AppName, AppMobile, Name, Count, Mobile, Address, Birth, LeapMonth, BirthTime, ZipCode, LightsType, i,
                                AppStatus, Num2String, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 21:
                        //鹿港城隍廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string HomeNum = dtapplecantinfo.Rows[i]["HomeNum"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string Sendback = dtapplecantinfo.Rows[i]["AppSendback"].ToString();
                            string rName = dtapplecantinfo.Rows[i]["ReceiptName"].ToString();
                            string rMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["ReceiptMobile"].ToString(), 5, 3);
                            string rZipCode = "0";
                            string rAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["ApprAddress"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_Lk_blessed(AppName, AppMobile, Name, Count, Mobile, Sex, HomeNum, Address, Birth, LeapMonth, BirthTime, ZipCode,
                                LightsType, i, AppStatus, Num2String, LightsString, ChargeDate, Sendback, rName, rMobile, rAddress, rZipCode, ref Total);
                        }
                        break;
                    case 23:
                        //玉敕大樹朝天宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_ma_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Email, Count,
                                Address, i, AppStatus, Num2String, LightsType, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 29:
                        //進寶財神廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_jb_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Email, Count,
                                Address, i, AppStatus, Num2String, LightsType, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 31:
                        //台灣道教總廟無極三清總道院
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_wjsan_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Email, Count,
                                Address, i, AppStatus, Num2String, LightsType, LightsString, ChargeDate, ref Total);
                        }
                        break;
                    case 32:
                        //桃園龍德宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString() != "" ? dtapplecantinfo.Rows[i]["BirthTime"].ToString() : "吉";
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string LightsType = dtapplecantinfo.Rows[i]["LightsType"].ToString();
                            string LightsString = dtapplecantinfo.Rows[i]["LightsString"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Lights_ld_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, Email, Count,
                                Address, i, AppStatus, Num2String, LightsType, LightsString, ChargeDate, ref Total);
                        }
                        break;
                }
            }
        }

        protected static void GetPurdueInfo(BasePage basePage, DataTable dtapplecantinfo, string m_Name, string m_Mobile, int adminID, string Year, ref string Datalist,
            ref int Total)
        {
            LightDAC objLightDAC = new LightDAC(basePage);
            dtapplecantinfo = objLightDAC.Getapplicantinfo_PurdueInfo(m_Name, m_Mobile, adminID, Year);
            //dtapplecantinfo = objLightDAC.Getapplicantinfo_PurduefromNum2String(m_Num2String, adminID);
            if (dtapplecantinfo.Rows.Count > 0)
            {
                switch (adminID)
                {
                    case 3:
                        //大甲鎮瀾宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Name2 = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name2"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Sendback = dtapplecantinfo.Rows[i]["Sendback"].ToString();
                            string rName = dtapplecantinfo.Rows[i]["rName"].ToString();
                            string rMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["rMobile"].ToString(), 5, 3);
                            string rAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["rAddress"].ToString());
                            string rZipCode = dtapplecantinfo.Rows[i]["rZipCode"].ToString();

                            string DeathName = dtapplecantinfo.Rows[i]["DeathName"].ToString();
                            string Birthday = dtapplecantinfo.Rows[i]["Birthday"].ToString();
                            string DeathLeapMonth = dtapplecantinfo.Rows[i]["DeathLeapMonth"].ToString();
                            string DeathBirthTime = dtapplecantinfo.Rows[i]["DeathBirthTime"].ToString();
                            string Deathday = dtapplecantinfo.Rows[i]["Deathday"].ToString();
                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            //string discountType = dtapplecantinfo.Rows[i]["DiscountType"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_da_blessed(AppName, AppMobile, Name, Name2, Mobile, Birth, LeapMonth, BirthTime, Address, ZipCode, PurdueType,
                                PurdueString, DeathName, Birthday, DeathLeapMonth, DeathBirthTime, Deathday, FirstName, Sendback, rName, rMobile, rAddress,
                                rZipCode, i, AppStatus, Num2String, ChargeDate, ref Total);
                        }
                        break;
                    case 4:
                        //新港奉天宮
                        Datalist += add_Purdue_h_blessed(dtapplecantinfo, ref Total);
                        //for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        //{
                        //    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                        //    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                        //    string AppBirth = dtapplecantinfo.Rows[i]["AppBirth"].ToString();
                        //    string AppLeapMonth = dtapplecantinfo.Rows[i]["AppLeapMonth"].ToString();
                        //    string AppBirthTime = dtapplecantinfo.Rows[i]["AppBirthTime"].ToString();
                        //    string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                        //    string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                        //    string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();

                        //    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                        //    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                        //    Datalist += add_Purdue_h_blessed(AppName, AppMobile, AppBirth, AppLeapMonth, AppBirthTime, AppEmail, AppAddress, AppZipCode,
                        //        dtapplecantinfo, i, AppStatus, Num2String, ref Total);
                        //}
                        break;
                    case 6:
                        //北港武德宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_wu_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, Sex, Homenum, Email, ZipCode, Address,
                                Count, Remark, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 8:
                        //西螺福興宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Name2 = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name2"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string GoldPaperCount = dtapplecantinfo.Rows[i]["GoldPaperCount"].ToString();

                            string DeathName = dtapplecantinfo.Rows[i]["DeathName"].ToString();
                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_Fu_blessed(AppName, AppMobile, Name, Name2, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, PurdueType,
                                PurdueString, Count, GoldPaperCount, DeathName, FirstName, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 9:
                        //桃園大廟景福宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Name2 = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name2"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();

                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_Jing_blessed(AppName, AppMobile, Name, Name2, Mobile, Birth, LeapMonth, BirthTime, Address, ZipCode, PurdueType,
                                PurdueString, FirstName, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 10:
                        //台南正統鹿耳門聖母廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Name2 = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name2"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();

                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_Luer_blessed(AppName, AppMobile, Name, Name2, Mobile, Birth, LeapMonth, BirthTime, Address, ZipCode, "贊普",
                                Email, Count, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 14:
                        //桃園威天宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();

                            if (PurdueType == "18") { Count = dtapplecantinfo.Rows[i]["Count_50rice"].ToString(); };
                            if (PurdueType == "19") { Count = dtapplecantinfo.Rows[i]["Count_3rice"].ToString(); };

                            string PurdueItem = dtapplecantinfo.Rows[i]["PurdueItem"].ToString();
                            string DeathName = dtapplecantinfo.Rows[i]["DeathName"].ToString();
                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();
                            string MomName = dtapplecantinfo.Rows[i]["MomName"].ToString();
                            string LastName = dtapplecantinfo.Rows[i]["LastName"].ToString();
                            string Reason = dtapplecantinfo.Rows[i]["Reason"].ToString();
                            string LicenseNum = dtapplecantinfo.Rows[i]["LicenseNum"].ToString();
                            string DeathAddress = dtapplecantinfo.Rows[i]["DeathAddress"].ToString();

                            string PurdueItem1 = dtapplecantinfo.Rows[i]["PurdueItem1"].ToString();
                            string DeathName1 = dtapplecantinfo.Rows[i]["DeathName1"].ToString();
                            string FirstName1 = dtapplecantinfo.Rows[i]["FirstName1"].ToString();
                            string MomName1 = dtapplecantinfo.Rows[i]["MomName1"].ToString();
                            string LastName1 = dtapplecantinfo.Rows[i]["LastName1"].ToString();
                            string Reason1 = dtapplecantinfo.Rows[i]["Reason1"].ToString();
                            string LicenseNum1 = dtapplecantinfo.Rows[i]["LicenseNum1"].ToString();
                            string DeathAddress1 = dtapplecantinfo.Rows[i]["DeathAddress1"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_ty_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, Remark, PurdueString,
                                PurdueType, Count, PurdueItem, DeathName, FirstName, MomName, LastName, Reason, LicenseNum, DeathAddress, PurdueItem1, DeathName1,
                                FirstName1, MomName1, LastName1, Reason1, LicenseNum1, DeathAddress1, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 15:
                        //斗六五路財神宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Count_rice = dtapplecantinfo.Rows[i]["Count_rice"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_Fw_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, PurdueType,
                                PurdueString, Count, Count_rice, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 16:
                        //台東東海龍門天聖宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();

                            string DeathName = dtapplecantinfo.Rows[i]["DeathName"].ToString();
                            string Deathday = dtapplecantinfo.Rows[i]["Deathday"].ToString();
                            string DeathBirth = dtapplecantinfo.Rows[i]["DeathBirth"].ToString();
                            string DeathLeapMonth = dtapplecantinfo.Rows[i]["DeathLeapMonth"].ToString();
                            string DeathBirthTime = dtapplecantinfo.Rows[i]["DeathBirthTime"].ToString();
                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();
                            string DeathAddress = dtapplecantinfo.Rows[i]["DeathAddress"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_dh_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, ZipCode, Address, PurdueType,
                                PurdueString, Count, DeathName, Deathday, DeathBirth, DeathLeapMonth, DeathBirthTime, FirstName, DeathAddress, i, AppStatus,
                                Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 21:
                        //鹿港城隍廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_Lk_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, Sex, Homenum, ZipCode, Address,
                                PurdueType, PurdueString, Count, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 23:
                        //玉敕大樹朝天宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();

                            string FirstName = dtapplecantinfo.Rows[i]["FirstName"].ToString();

                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_ma_blessed(AppName, AppMobile, Name, Mobile, Birth, LeapMonth, BirthTime, Sex, Email, ZipCode, Address,
                                PurdueType, PurdueString, Count, Remark, FirstName, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                    case 30:
                        //鎮瀾買足
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());

                            string PurdueType = dtapplecantinfo.Rows[i]["PurdueType"].ToString();
                            string PurdueString = dtapplecantinfo.Rows[i]["PurdueString"].ToString();

                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Purdue_mazu_blessed(AppName, AppMobile, AppZipCode, AppAddress, Name, Mobile, Birth, LeapMonth, BirthTime, PurdueType,
                                PurdueString, Count, i, AppStatus, Num2String, ChargeDate,ref Total);
                        }
                        break;
                }
            }
        }

        protected static void GetSuppliesInfo(BasePage basePage, DataTable dtapplecantinfo, string m_Name, string m_Mobile, int adminID, int suppliesType, string Year, ref string Datalist,
            ref int Total)
        {
            LightDAC objLightDAC = new LightDAC(basePage);
            switch (suppliesType)
            {
                case 1:
                    //下元補庫
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wu(m_Name, m_Mobile, adminID, 2);
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 1, Year);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wufromNum2String(m_Num2String, adminID, 1, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        //北港武德宮
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                            string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Supplies_blessed(adminID, AppName, AppMobile, "", Name, Mobile, Num2String, ChargeDate, SuppliesType, SuppliesString, Sex, Birth,
                                LeapMonth, BirthTime, Homenum, Email, Address, ZipCode, i, AppStatus, Count, Remark, ref Total);
                        }
                    }
                    break;
                case 2:
                    //呈疏補庫
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wu(m_Name, m_Mobile, adminID, 2);
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 2, Year);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wufromNum2String(m_Num2String, adminID, 2, Year);
                    if (Year == "2024")
                    {
                        if (dtapplecantinfo.Rows.Count > 0)
                        {
                            //北港武德宮
                            for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                            {
                                string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                                string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                                string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                                string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                Datalist += add_Supplies_blessed(adminID, AppName, AppMobile, "", Name, Mobile, Num2String, ChargeDate, SuppliesType, SuppliesString, Sex, Birth, LeapMonth, BirthTime, Homenum, Email, Address, ZipCode, i, AppStatus, Count, Remark, ref Total);
                            }
                        }
                    }
                    else
                    {
                        if (dtapplecantinfo.Rows.Count > 0)
                        {
                            //北港武德宮
                            for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                            {
                                string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                string AppEmail = dtapplecantinfo.Rows[i]["AppEmail"].ToString();
                                string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                                string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                                string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                                string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                Datalist += add_Supplies_blessed(adminID, AppName, AppMobile, AppEmail, Name, Mobile, Num2String, ChargeDate, SuppliesType, SuppliesString, Sex, Birth, LeapMonth, BirthTime, Homenum, Email, Address, ZipCode, i, AppStatus, Count, Remark, ref Total);
                            }
                        }
                    }
                    break;
                case 3:
                    //企業補財庫
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wu(m_Name, m_Mobile, adminID, 2);
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 3, Year);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Supplies_wufromNum2String(m_Num2String, adminID, 3, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        switch (adminID)
                        {
                            case 6:
                                //北港武德宮
                                for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                {
                                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                    string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                    string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                    string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                    string Homenum = dtapplecantinfo.Rows[i]["Homenum"].ToString();
                                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                                    string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                    Datalist += add_Supplies_blessed(adminID, AppName, AppMobile, "", Name, Mobile, Num2String, ChargeDate, SuppliesType, SuppliesString, Sex, Birth, LeapMonth, BirthTime, Homenum, Email, Address, ZipCode, i, AppStatus, Count, Remark, ref Total);
                                }
                                break;
                        }
                    }
                    break;
                case 4:
                    //天赦日招財補運
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 4, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        switch (adminID)
                        {
                            case 14:
                                //桃園威天宮
                                for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                {
                                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    string AppsBirth = string.Empty;
                                    string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    string sBirth = string.Empty;
                                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                    string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                                    if (Year == "2024")
                                    {
                                        AppsBirth = dtapplecantinfo.Rows[i]["AppBirth"].ToString();
                                        sBirth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    }
                                    else
                                    {
                                        AppsBirth = dtapplecantinfo.Rows[i]["AppsBirth"].ToString();
                                        sBirth = dtapplecantinfo.Rows[i]["sBirth"].ToString();
                                    }

                                    Datalist += add_Supplies_ty_blessed(AppName, AppMobile, AppsBirth, AppAddress, Name, Mobile, sBirth, Address, SuppliesType, i, AppStatus, 
                                        Num2String, ChargeDate, SuppliesString, ref Total);
                                }
                                break;
                            case 23:
                                //玉敕大樹朝天宮
                                for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                                {
                                    string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                                    string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                                    string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                                    string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                                    string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                                    string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                                    string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                                    string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                                    string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                                    string sBirth = dtapplecantinfo.Rows[i]["sBirth"].ToString();
                                    string Email = dtapplecantinfo.Rows[i]["Email"].ToString();
                                    string HomeNum = dtapplecantinfo.Rows[i]["HomeNum"].ToString();
                                    string Remark = dtapplecantinfo.Rows[i]["Remark"].ToString();
                                    string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                                    string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                                    string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                                    string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                                    string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                                    string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                                    Datalist += add_Supplies_ma_blessed(AppName, AppMobile, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, sBirth, Email, HomeNum, Remark, 
                                        Address, SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                                }
                                break;
                        }
                    }
                    break;
                case 5:
                    //天赦日祭改
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 5, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        //進寶財神廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string ReceiptName = dtapplecantinfo.Rows[i]["ReceiptName"].ToString();
                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                            string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Supplies_jb_blessed(AppName, AppMobile, ReceiptName, AppZipCode, AppAddress, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, ZipCode, 
                                Address, SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                        }
                    }
                    break;
                case 10:
                    //財神賜福-消災補庫法會
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_SuppliesInfo(m_Name, m_Mobile, adminID, 10, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        //進寶財神廟
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["AppMobile"].ToString(), 5, 3);
                            string ReceiptName = dtapplecantinfo.Rows[i]["ReceiptName"].ToString();
                            string AppAddress = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["AppAddress"].ToString());
                            string AppZipCode = dtapplecantinfo.Rows[i]["AppZipCode"].ToString();
                            string Name = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string Mobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ZipCode = dtapplecantinfo.Rows[i]["ZipCode"].ToString();
                            string Sex = dtapplecantinfo.Rows[i]["Sex"].ToString();
                            string Birth = dtapplecantinfo.Rows[i]["Birth"].ToString();
                            string LeapMonth = dtapplecantinfo.Rows[i]["LeapMonth"].ToString();
                            string BirthTime = dtapplecantinfo.Rows[i]["BirthTime"].ToString();
                            string AppStatus = dtapplecantinfo.Rows[i]["AppStatus"].ToString();
                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string SuppliesType = dtapplecantinfo.Rows[i]["SuppliesType"].ToString();
                            string SuppliesString = dtapplecantinfo.Rows[i]["SuppliesString"].ToString();
                            string Count = dtapplecantinfo.Rows[i]["Count"].ToString();
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            Datalist += add_Supplies_jb_blessed(AppName, AppMobile, ReceiptName, AppZipCode, AppAddress, Name, Mobile, Sex, Birth, LeapMonth, BirthTime, ZipCode,
                                Address, SuppliesType, i, AppStatus, Num2String, ChargeDate, SuppliesString, ref Total);
                        }
                    }
                    break;
            }
        }

        protected static void GetProductInfo(BasePage basePage, DataTable dtapplecantinfo, string m_Name, string m_Mobile, int adminID, string Year, ref string Datalist,
            ref int Total)
        {
            LightDAC objLightDAC = new LightDAC(basePage);
            switch (adminID)
            {
                case 5:
                    //新港奉天宮商品小舖
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            ProductDAC objProductDAC = new ProductDAC(basePage);
                            int applicantID = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                            DataTable dtProduct = objProductDAC.GetProductInfo(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()), int.Parse(dtapplecantinfo.Rows[i]["TypeID"].ToString()));
                            string name_A = string.Empty;
                            string name_B = string.Empty;
                            if (dtProduct.Rows.Count > 0)
                            {
                                name_A = dtProduct.Rows[0]["Title"].ToString();
                                name_B = dtProduct.Rows[0]["ItemName"].ToString();
                            }
                            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                            int count_B = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["Count"].ToString(), out count_B);

                            Total += int.Parse(cost_B) * count_B;

                            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                            Datalist += add_product_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                        }
                    }

                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Num2String);
                    //if (dtapplecantinfo.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //    {
                    //        int applicantID = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //        string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //        string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //        string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //        string Address =UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //        string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //        string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //        string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //        int count_B = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //        Total += int.Parse(cost_B) * count_B;

                    //        ProductDAC objProductDAC = new ProductDAC(basePage);
                    //        string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //        Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //    }
                    //}
                    //else
                    //{
                    //    dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    //    if (dtapplecantinfo.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //        {
                    //            int applicantID = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //            string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //            string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //            int count_B = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //            Total += int.Parse(cost_B) * count_B;

                    //            ProductDAC objProductDAC = new ProductDAC(basePage);
                    //            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //            Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //        }
                    //    }
                    //}
                    break;
                case 7:
                    //繞境商品販賣服務
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Pilgrimage(m_Name, m_Mobile);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID);
                    //if (dtapplecantinfo.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //    {
                    //        int applicantID = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //        string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //        string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //        string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //        string Address = dtapplecantinfo.Rows[i]["ZipCode"].ToString() + " " + UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //        DataTable dtProductInfo_A = objLightDAC.GetProduct_Pilgrimage(applicantID, 1);
                    //        string name_A = "鎮宅、開運錢母擺件";
                    //        string cost_A = "1680";
                    //        string pd_A_img = "https://bobibobi.tw/Pilgrimage/images/products/products_A_1.jpg";
                    //        int count_A = 0;
                    //        string[] SculptureName = new string[0];

                    //        if (dtProductInfo_A.Rows.Count > 0)
                    //        {
                    //           count_A = dtProductInfo_A.Rows.Count;

                    //            SculptureName = new string[dtProductInfo_A.Rows.Count];
                    //            for (int j = 0; j < dtProductInfo_A.Rows.Count; j++)
                    //            {
                    //                SculptureName[j] = dtProductInfo_A.Rows[j]["SculptureName"].ToString();

                    //                Total += 1680;
                    //            }
                    //        }

                    //        string name_B = "開運隨身御守";
                    //        string cost_B = "499";
                    //        DataTable dtProductInfo_B = objLightDAC.GetProduct_Pilgrimage(applicantID, 2);
                    //        string pd_B_img = "https://bobibobi.tw/Pilgrimage/images/products/products_B_1.jpg";
                    //        int count_B = 0;

                    //        if (dtProductInfo_B.Rows.Count > 0)
                    //        {
                    //            int.TryParse(dtProductInfo_B.Rows[0]["Count"].ToString(), out count_B);

                    //            Total += 499 * count_B;
                    //        }

                    //        Datalist += add_product_Pilgrimage_blessed(Num2String, AppName, AppMobile, Address, SculptureName, name_A, cost_A, count_A, pd_A_img, name_B, cost_B, count_B, pd_B_img);
                    //    }
                    //}
                    break;
                case 11:
                    //錢母商品販賣服務
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Moneymother(m_Name, m_Mobile);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID);
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Moneymother(m_Name, m_Mobile, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            int applicantID = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["AppName"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();
                            string typpe = dtapplecantinfo.Rows[i]["Type"].ToString();

                            DataTable dtProductInfo_A = objLightDAC.GetProduct_Moneymother(applicantID, 1, Year);
                            string name_A = string.Empty;
                            int cost_A = 0;
                            string pd_A_img = "https://bobibobi.tw/Product/images/products/products_A_1.jpg";
                            int count_A = 0;
                            string name_D = string.Empty;
                            int cost_D = 0;
                            string pd_D_img = "https://bobibobi.tw/Product/images/products/products_D_1.png";
                            int count_D = 0;
                            string name_E = string.Empty;
                            int cost_E = 0;
                            string pd_E_img = "https://bobibobi.tw/Product/images/products/products_E_1.png";
                            int count_E = 0;
                            string name_F = string.Empty;
                            int cost_F = 0;
                            string pd_F_img = "https://bobibobi.tw/Product/images/products/products_F_1.png";
                            int count_F = 0;
                            string name_G = string.Empty;
                            int cost_G = 0;
                            string pd_G_img = "https://bobibobi.tw/Product/images/products/products_G_1.png";
                            int count_G = 0;

                            switch (typpe)
                            {
                                case "1":
                                    name_A = dtapplecantinfo.Rows[i]["Name"].ToString();
                                    count_A = int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    cost_A = 1258 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    Total += 1258 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());

                                    Datalist += add_product_Moneymother_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_A, cost_A, count_A, pd_A_img);
                                    break;
                                case "2": break;
                                case "3": break;
                                case "4": break;
                                case "5":
                                    name_D = dtapplecantinfo.Rows[i]["Name"].ToString();
                                    count_D = int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    cost_D = 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    Total += 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());

                                    Datalist += add_product_Moneymother_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_D, cost_D, count_D, pd_D_img);
                                    break;
                                case "6":
                                    name_E = dtapplecantinfo.Rows[i]["Name"].ToString();
                                    count_E = int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    cost_E = 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    Total += 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());

                                    Datalist += add_product_Moneymother_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_E, cost_E, count_E, pd_E_img);
                                    break;
                                case "7":
                                    name_F = dtapplecantinfo.Rows[i]["Name"].ToString();
                                    count_F = int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    cost_F = 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    Total += 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());

                                    Datalist += add_product_Moneymother_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_F, cost_F, count_F, pd_F_img);
                                    break;
                                case "8":
                                    name_G = dtapplecantinfo.Rows[i]["Name"].ToString();
                                    count_G = int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    cost_G = 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());
                                    Total += 399 * int.Parse(dtapplecantinfo.Rows[i]["Count"].ToString());

                                    Datalist += add_product_Moneymother_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_G, cost_G, count_G, pd_G_img);
                                    break;
                            }

                            //DataTable dtProductInfo_B = objLightDAC.GetProduct_Moneymother(applicantID, 1, Year);
                            //string name_B = dtProductInfo_B.Rows[0]["Name"].ToString();
                            string name_B = string.Empty;
                            int cost_B = 0;
                            string pd_B_img = "https://bobibobi.tw/Product/images/products/products_B_1.jpg";
                            int count_B = 0;

                            //if (dtProductInfo_B.Rows.Count > 0)
                            //{
                            //    count_B = int.Parse(dtProductInfo_B.Rows[0]["Count"].ToString());
                            //    cost_B = 499 * int.Parse(dtProductInfo_B.Rows[0]["Count"].ToString());
                            //    Total += 499 * int.Parse(dtProductInfo_B.Rows[0]["Count"].ToString());
                            //}

                            //DataTable dtProductInfo_C = objLightDAC.GetProduct_Moneymother(applicantID, 1, Year);
                            //string name_C = dtProductInfo_C.Rows[0]["Name"].ToString();
                            string name_C = string.Empty;
                            int cost_C = 0;
                            string pd_C_img = "https://bobibobi.tw/Product/images/products/products_C_1.jpg";
                            int count_C = 0;

                            //if (dtProductInfo_C.Rows.Count > 0)
                            //{
                            //    count_C = int.Parse(dtProductInfo_C.Rows[0]["Count"].ToString());
                            //    cost_C = 2980 * int.Parse(dtProductInfo_C.Rows[0]["Count"].ToString());
                            //    Total += 2980 * int.Parse(dtProductInfo_C.Rows[0]["Count"].ToString());
                            //}

                        }
                    }
                    break;
                case 20:
                    //西螺福興宮商品小舖
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            ProductDAC objProductDAC = new ProductDAC(basePage);
                            int applicantID = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                            DataTable dtProduct = objProductDAC.GetProductInfo(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()), int.Parse(dtapplecantinfo.Rows[i]["TypeID"].ToString()));
                            string name_A = string.Empty;
                            string name_B = string.Empty;
                            if (dtProduct.Rows.Count > 0)
                            {
                                name_A = dtProduct.Rows[0]["Title"].ToString();
                                name_B = dtProduct.Rows[0]["ItemName"].ToString();
                            }
                            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                            int count_B = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["Count"].ToString(), out count_B);

                            Total += int.Parse(cost_B) * count_B;

                            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                            Datalist += add_product_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                        }
                    }
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Num2String);
                    //if (dtapplecantinfo.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //    {
                    //        int applicantID = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //        string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //        string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //        string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //        string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //        string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //        string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //        string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //        int count_B = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //        Total += int.Parse(cost_B) * count_B;

                    //        ProductDAC objProductDAC = new ProductDAC(basePage);
                    //        string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //        Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //    }
                    //}
                    //else
                    //{
                    //    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    //    if (dtapplecantinfo.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //        {
                    //            int applicantID = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //            string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //            string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //            int count_B = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //            Total += int.Parse(cost_B) * count_B;

                    //            ProductDAC objProductDAC = new ProductDAC(basePage);
                    //            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //            Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //        }
                    //    }
                    //}
                    break;
                case 22:
                    //流金富貴販賣服務
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            ProductDAC objProductDAC = new ProductDAC(basePage);
                            int applicantID = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                            DataTable dtProduct = objProductDAC.GetProductInfo(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()), int.Parse(dtapplecantinfo.Rows[i]["TypeID"].ToString()));
                            string name_A = string.Empty;
                            string name_B = string.Empty;
                            if (dtProduct.Rows.Count > 0)
                            {
                                name_A = dtProduct.Rows[0]["Title"].ToString();
                                name_B = dtProduct.Rows[0]["ItemName"].ToString();
                            }
                            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                            int count_B = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["Count"].ToString(), out count_B);

                            Total += int.Parse(cost_B) * count_B;

                            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                            Datalist += add_product_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                        }
                    }
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID);
                    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Num2String);
                    //if (dtapplecantinfo.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //    {
                    //        int applicantID = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //        string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //        string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //        string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //        string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //        string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //        string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //        string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //        int count_B = 0;
                    //        int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //        Total += int.Parse(cost_B) * count_B;

                    //        ProductDAC objProductDAC = new ProductDAC(basePage);
                    //        string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //        Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //    }
                    //}
                    //else
                    //{
                    //    //dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    //    if (dtapplecantinfo.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                    //        {
                    //            int applicantID = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                    //            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                    //            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                    //            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                    //            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());

                    //            string name_A = dtapplecantinfo.Rows[i]["productName"].ToString();
                    //            string name_B = dtapplecantinfo.Rows[i]["ItemName"].ToString();
                    //            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                    //            int count_B = 0;
                    //            int.TryParse(dtapplecantinfo.Rows[0]["Count"].ToString(), out count_B);

                    //            Total += int.Parse(cost_B) * count_B;

                    //            ProductDAC objProductDAC = new ProductDAC(basePage);
                    //            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                    //            Datalist += add_product_blessed(Num2String, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                    //        }
                    //    }
                    //}
                    break;
                case 28:
                    //流金富貴販賣服務
                    dtapplecantinfo = objLightDAC.Getapplicantinfo_Product(m_Name, m_Mobile, adminID, Year);
                    if (dtapplecantinfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtapplecantinfo.Rows.Count; i++)
                        {
                            ProductDAC objProductDAC = new ProductDAC(basePage);
                            int applicantID = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["ApplicantID"].ToString(), out applicantID);

                            string Num2String = dtapplecantinfo.Rows[i]["Num2String"].ToString();
                            string AppName = UtilDataMask.MaskName(dtapplecantinfo.Rows[i]["Name"].ToString());
                            string AppMobile = UtilDataMask.MaskMobile(dtapplecantinfo.Rows[i]["Mobile"].ToString(), 5, 3);
                            string Address = UtilDataMask.MaskTWAddr(dtapplecantinfo.Rows[i]["Address"].ToString());
                            string ChargeDate = dtapplecantinfo.Rows[i]["ChargeDateString"].ToString();

                            DataTable dtProduct = objProductDAC.GetProductInfo(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()), int.Parse(dtapplecantinfo.Rows[i]["TypeID"].ToString()));
                            string name_A = string.Empty;
                            string name_B = string.Empty;
                            if (dtProduct.Rows.Count > 0)
                            {
                                name_A = dtProduct.Rows[0]["Title"].ToString();
                                name_B = dtProduct.Rows[0]["ItemName"].ToString();
                            }
                            string cost_B = dtapplecantinfo.Rows[i]["Cost"].ToString();
                            int count_B = 0;
                            int.TryParse(dtapplecantinfo.Rows[i]["Count"].ToString(), out count_B);

                            Total += int.Parse(cost_B) * count_B;

                            string pd_B_img = objProductDAC.GetProductImgages(int.Parse(dtapplecantinfo.Rows[i]["ProductID"].ToString()));

                            Datalist += add_product_blessed(Num2String, ChargeDate, AppName, AppMobile, Address, name_A, name_B, cost_B, count_B, pd_B_img);
                        }
                    }
                    break;
            }
        }

        protected static string GetLightsImg(string lightsType, string Name)
        {
            string result = string.Empty;

            if (lightsType != "" && Name != "")
            {
                result = "<div class=\"lights2\">" +
                    "<div>" +
                    "<span class=\"lightsType\">" + lightsType + "</span>" +
                    "<span class=\"name\">" + Name + "</span>" +
                    "<img src=\"images/lights2.png?t=589\" width=\"188\" height=\"373\" alt=\"\" />" +
                    "<div class=\"Light\">" +
                    "<img src=\"images/lights2_Light.png?t=589\" width=\"315\" height=\"315\" alt=\"\" />" +
                    "</div></div></div>";
            }

            return result;
        }

        protected static string TempleName(int AdminID)
        {
            string result = string.Empty;

            switch (AdminID)
            {
                case 6:
                    result = "北港武德宮";
                    break;
            }

            return result;
        }

        /// <summary>
        /// 轉換狀態文字
        /// </summary>
        /// <param name="status">status=狀態文字 0-未付款 1-正在付款 2-已付款 -1-"" -2-已退款</param>
        /// <returns></returns>
        public static string Status2String(int status)
        {
            string result = string.Empty;
            switch (status)
            {
                case 0:
                    result = "未付款";
                    break;
                case 1:
                    result = "正在付款";
                    break;
                case 2:
                    result = "已付款";
                    break;
                case -1:
                    result = "";
                    break;
                case -2:
                    result = "已退款";
                    break;
            }
            return result;
        }


        public class MD5
        {
            public static string Encode(string text)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(text)));
            }
        }
    }
}