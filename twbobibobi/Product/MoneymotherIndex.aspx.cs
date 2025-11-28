using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Read.data;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using twbobibobi.Data;
using twbobibobi.Services;

namespace twbobibobi.Product
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MoneymotherIndex : AjaxBasePage
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void InitAjaxHandler()
        {
            AddAjaxHandler(typeof(AjaxPageHandler), "gotopay");
        }

        /// <summary>
        /// 初始化商品顯示/隱藏
        /// </summary>
        protected void productInit()
        {
            this.type_1.Visible = false;
            //this.type_5.Visible = false;
            //this.type_6.Visible = false;
            //this.type_7.Visible = false;
            //this.type_8.Visible = true;
            this.type_9.Visible = false;

            this.product_1.Visible = false;
            //this.product_5.Visible = false;
            //this.product_6.Visible = false;
            //this.product_7.Visible = false;
            //this.product_8.Visible = true;
            this.product_9.Visible = false;
            this.product_10.Visible = false;
            this.product_11.Visible = false;

            this.productCount_1.Visible = true;
            //this.productCount_5.Visible = true;
            //this.productCount_6.Visible = true;
            //this.productCount_7.Visible = true;
            //this.productCount_8.Visible = false;
            this.productCount_9.Visible = true;
            this.productCount_10.Visible = true;
            this.productCount_11.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Year = "2026";
            int overStatus = 0;
            LightDAC objLightDAC = new LightDAC(this);

            productInit();
            if (objLightDAC.CheckedProductstock(1, "19", out overStatus, Year))
            {
                if (overStatus == -1)
                {
                    this.type_1.Visible = true;
                    this.product_1.Visible = true;
                    this.productCount_1.Visible = false;
                }
            }
            //else if (objLightDAC.CheckedProductstock_Moneymother(pd_3, 3, out overStatus, Year))
            //{
            //    basePage.mJSonHelper.AddContent("Stock", 3);
            //    basePage.mJSonHelper.AddContent("overStatus", overStatus);
            //}
            //else if (objLightDAC.CheckedProductstock_Moneymother(pd_4, 4, out overStatus, Year))
            //{
            //    basePage.mJSonHelper.AddContent("Stock", 4);
            //    basePage.mJSonHelper.AddContent("overStatus", overStatus);
            //}
            //else if (objLightDAC.CheckedProductstock(1, "72", out overStatus, Year))
            //{
            //    if (overStatus == -1)
            //    {
            //        this.type_5.Visible = true;
            //        this.product_5.Visible = true;
            //        this.productCount_5.Visible = false;
            //    }
            //}
            //else if (objLightDAC.CheckedProductstock(1, "70", out overStatus, Year))
            //{
            //    if (overStatus == -1)
            //    {
            //        this.type_6.Visible = true;
            //        this.product_6.Visible = true;
            //        this.productCount_6.Visible = false;
            //    }
            //}
            //else if (objLightDAC.CheckedProductstock(1, "73", out overStatus, Year))
            //{
            //    if (overStatus == -1)
            //    {
            //        this.type_7.Visible = true;
            //        this.product_7.Visible = true;
            //        this.productCount_7.Visible = false;
            //    }
            //}
            //else if (objLightDAC.CheckedProductstock(1, "71", out overStatus, Year))
            //{
            //    if (overStatus == -1)
            //    {
            //        this.type_8.Visible = true;
            //        this.product_8.Visible = true;
            //        this.productCount_8.Visible = false;
            //    }
            //}
            else if (objLightDAC.CheckedProductstock(6, "91", out overStatus, Year))
            {
                if (overStatus == -1)
                {
                    //this.type_11.Visible = true;
                    this.product_11.Visible = true;
                    this.productCount_11.Visible = false;
                }
            }
            else if (objLightDAC.CheckedProductstock(3, "91", out overStatus, Year))
            {
                if (overStatus == -1)
                {
                    //this.type_10.Visible = true;
                    this.product_10.Visible = true;
                    this.productCount_10.Visible = false;
                }
            }
            else if (objLightDAC.CheckedProductstock(1, "91", out overStatus, Year))
            {
                if (overStatus == -1)
                {
                    //this.type_9.Visible = false;
                    this.product_9.Visible = true;
                    this.productCount_9.Visible = false;
                }
            }
        }

        public class AjaxPageHandler
        {
            public int applicantID = 0;
            public int buyID = 0;
            public int Total = 0;

            public void gotopay(BasePage basePage)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

                basePage.mJSonHelper.AddContent("StatusCode", 0);

                string Year = "2026";
                string AdminID = "11";
                string kind = "3";
                string ChargeType = basePage.Request["ChargeType"];
                string pd_A = basePage.Request["pd_A"];
                //string pd_B = basePage.Request["pd_B"];
                //string pd_C = basePage.Request["pd_C"];
                string pd_D = basePage.Request["pd_D"];
                string pd_E = basePage.Request["pd_E"];
                string pd_F = basePage.Request["pd_F"];
                string pd_G = basePage.Request["pd_G"];
                string pd_H = basePage.Request["pd_H"];
                string pd_I = basePage.Request["pd_I"];
                string pd_J = basePage.Request["pd_J"];
                string cusName = basePage.Request["cusName"];
                string cusTel = basePage.Request["cusTel"];
                string cusEmail = basePage.Request["cusEmail"];
                string cusCounty = basePage.Request["cusCounty"];
                string cusDistrict = basePage.Request["cusDistrict"];
                string cusAddr = basePage.Request["cusAddr"];
                string cusZipCode = basePage.Request["cusZipCode"];
                string invType = basePage.Request["invType"];
                string carrier = basePage.Request["carrier"];
                string invCode = basePage.Request["invCode"];
                string invName = basePage.Request["invName"];
                Total = int.Parse(basePage.Request["Money"]);

                int pd_1 = 0; //驗證數量參數-擺件
                int.TryParse(pd_A, out pd_1);
                //int pd_3 = 0; //驗證數量參數-香火袋
                //int.TryParse(pd_B, out pd_3);
                //int pd_4 = 0; //驗證數量參數-黃金符令手鍊
                //int.TryParse(pd_C, out pd_4);
                int pd_5 = 0; //驗證數量參數-招財大嘴貓(白)
                int.TryParse(pd_D, out pd_5);
                int pd_6 = 0; //驗證數量參數-招財大嘴貓(藍)
                int.TryParse(pd_E, out pd_6);
                int pd_7 = 0; //驗證數量參數-招財大嘴貓(粉)
                int.TryParse(pd_F, out pd_7);
                int pd_8 = 0; //驗證數量參數-招財大嘴貓(橘)
                int.TryParse(pd_G, out pd_8);
                int pd_9 = 0; //驗證數量參數-午時水/1罐
                int.TryParse(pd_H, out pd_9);
                int pd_10 = 0; //驗證數量參數-午時水/3罐
                int.TryParse(pd_I, out pd_10);
                int pd_11 = 0; //驗證數量參數-午時水/6罐
                int.TryParse(pd_J, out pd_11);
                int overStatus = 0; //超過數量的狀態 -1-已額滿 -2-數量不足

                string error = string.Empty;
                LightDAC objLightDAC = new LightDAC(basePage);
                if (objLightDAC.CheckedProductstock(pd_1, "19", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 1);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                //else if (objLightDAC.CheckedProductstock_Moneymother(pd_3, 3, out overStatus, Year))
                //{
                //    basePage.mJSonHelper.AddContent("Stock", 3);
                //    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                //}
                //else if (objLightDAC.CheckedProductstock_Moneymother(pd_4, 4, out overStatus, Year))
                //{
                //    basePage.mJSonHelper.AddContent("Stock", 4);
                //    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                //}
                else if (objLightDAC.CheckedProductstock(pd_5, "72", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 5);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(pd_6, "70", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 6);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(pd_7, "73", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 7);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(pd_8, "71", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 8);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(pd_9, "91", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 9);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(3, "91", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 10);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if (objLightDAC.CheckedProductstock(6, "91", out overStatus, Year))
                {
                    basePage.mJSonHelper.AddContent("Stock", 11);
                    basePage.mJSonHelper.AddContent("overStatus", overStatus);
                }
                else if(invType == "2" && (!ValidateMobileCarrier(carrier, out error)))
                {
                    basePage.mJSonHelper.AddContent("Stock", 12);
                    basePage.mJSonHelper.AddContent("overStatus", error);
                }
                else
                {
                    applicantID = InsertApplicantInfo(basePage, cusName, cusTel, cusEmail, "11", cusCounty, cusDistrict, cusAddr, cusZipCode, Year);

                    int id = objLightDAC.AddInvoiceDetail(applicantID, 11, 3, int.Parse(invType), carrier, invCode, invName, Year);

                    if (id == 0)
                    {
                        basePage.mJSonHelper.AddContent("Stock", 13);
                        basePage.mJSonHelper.AddContent("overStatus", error);
                        return;
                    }

                    int Cost = 0;

                    if (pd_1 > 0)
                    {
                        Cost = pd_1 * 1480;
                        buyID = InsertProductInfo(basePage, applicantID, "鎮宅、開運錢母擺件", 1, Cost, 1480, pd_1, Year);
                    }

                    //if (pd_3 > 0)
                    //{
                    //    buyID = InsertProductInfo(basePage, applicantID, "開運隨身御守", 3, pd_3, Year);
                    //}

                    //if (pd_4 > 0)
                    //{
                    //    buyID = InsertProductInfo(basePage, applicantID, "2024新港奉天宮黃金符令手鍊", 4, pd_4, Year);
                    //}

                    if (pd_5 > 0)
                    {
                        Cost = pd_5 * 399;
                        buyID = InsertProductInfo(basePage, applicantID, "招財大嘴貓(白色)", 5, Cost, 399, pd_5, Year);
                    }

                    if (pd_6 > 0)
                    {
                        Cost = pd_6 * 399;
                        buyID = InsertProductInfo(basePage, applicantID, "招財大嘴貓(藍色)", 6, Cost, 399, pd_6, Year);
                    }

                    if (pd_7 > 0)
                    {
                        Cost = pd_7 * 399;
                        buyID = InsertProductInfo(basePage, applicantID, "招財大嘴貓(粉色)", 7, Cost, 399, pd_7, Year);
                    }

                    if (pd_8 > 0)
                    {
                        Cost = pd_8 * 399;
                        buyID = InsertProductInfo(basePage, applicantID, "招財大嘴貓(橘色)", 8, Cost, 399, pd_8, Year);
                    }

                    if (pd_9 > 0)
                    {
                        Cost = pd_9 * 228;
                        buyID = InsertProductInfo(basePage, applicantID, "午時水/1罐", 9, Cost, 228, pd_9, Year);
                    }

                    if (pd_10 > 0)
                    {
                        Cost = pd_10 * 478;
                        buyID = InsertProductInfo(basePage, applicantID, "午時水/3罐", 10, Cost, 478, pd_10, Year);
                    }

                    if (pd_11 > 0)
                    {
                        Cost = pd_11 * 688;
                        buyID = InsertProductInfo(basePage, applicantID, "午時水/6罐", 11, Cost, 688, pd_11, Year);
                    }

                    if (applicantID > 0 && buyID > 0)
                    {
                        string orderId = dtNow.ToString("yyyyMMddHHmmssfff");

                        if (Total > 0)
                        {
                            int cost = Total;
                            string link = string.Empty;

                            //switch (ChargeType)
                            //{
                            //    case "LinePay":
                            //        link = "https://bobibobi.tw/Admin/line/LinePay.aspx?a=11&aid=" + applicantID + "&Total=" + cost + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name='新港奉天宮錢母擺件'&kind=3&orderId=" + orderId;
                            //        break;
                            //    case "JkosPay":
                            //        link = "https://bobibobi.tw/Admin/jkos/jkosPay.aspx?a=11&aid=" + applicantID + "&Total=" + cost + (basePage.Request["twm"] != null ? "&twm=1" : "") + "&name='新港奉天宮錢母擺件'&kind=3&orderId=" + orderId;
                            //        break;
                            //    case "ChtCSP":
                            //        link = TWWebPay(basePage, orderId, applicantID, "TELEPAY", "", cost, cusTel, basePage.GetConfigValue("PaymentMoneymother_ResultUrl") + "?a=11&aid=" + applicantID + (basePage.Request["twm"] != null ? "&twm=1" : ""));
                            //        break;
                            //    case "TwmCSP":
                            //        link = TWWebPay(basePage, orderId, applicantID, "TELEPAY", "twm", cost, cusTel, basePage.GetConfigValue("PaymentMoneymother_ResultUrl") + "?a=11&aid=" + applicantID + (basePage.Request["twm"] != null ? "&twm=1" : ""));
                            //        break;
                            //    default:
                            //        link = TWWebPay(basePage, orderId, applicantID, ChargeType, "", cost, cusTel, basePage.GetConfigValue("PaymentMoneymother_ResultUrl") + "?a=11&aid=" + applicantID + (basePage.Request["twm"] != null ? "&twm=1" : ""));
                            //        break;
                            //}

                            switch (ChargeType)
                            {
                                case "LinePay":
                                    link = TWWebPay(basePage, orderId, applicantID, "LINEPAY", "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind + 
                                        (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "JkosPay":
                                    //if (basePage.Request["ad"] != null)
                                    //{
                                    //    if (basePage.Request["ad"] == "55688")
                                    //    {
                                    //        cost = 10;
                                    //    }
                                    //}
                                    link = TWWebPay(basePage, orderId, applicantID, "JKOPAY", "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind +
                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "PXPayPlus":
                                    link = TWWebPay(basePage, orderId, applicantID, "PXPAY", "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind +
                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "ChtCSP":
                                    link = TWWebPay(basePage, orderId, applicantID, "TELEPAY", "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind + 
                                        (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "TwmCSP":
                                    link = TWWebPay(basePage, orderId, applicantID, "TELEPAY", "twm", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind + 
                                        (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "UnionPay":
                                    link = TWWebPay(basePage, orderId, applicantID, "CreditCard", "UNIONPAY", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind +
                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                case "ApplePay":
                                    link = TWWebPay(basePage, orderId, applicantID, "APPLEPAY", "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind +
                                    (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                                default:
                                    link = TWWebPay(basePage, orderId, applicantID, ChargeType, "", cost, cusTel, "a=" + AdminID + "&aid=" + applicantID + "&kind=" + kind + 
                                        (basePage.Request["twm"] != null ? "&twm=1" : "") + (basePage.Request["ftg"] != null ? "&ftg=" + basePage.Request["ftg"].ToString() : ""), Year);
                                    break;
                            }

                            if (link != "")
                            {
                                basePage.SavePayLog(link);

                                basePage.mJSonHelper.AddContent("StatusCode", 1);
                                basePage.mJSonHelper.AddContent("redirect", link);

                                basePage.Session["applicantID"] = applicantID;
                            }
                        }
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

            //protected string TWWebPay(BasePage basePag, string orderid, int applicantID, string paytype, string telco, int price, string m_phone, string returnUrl)
            //{
            //    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            //    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            //    BasePage basePage = new BasePage();
            //    string Year = dtNow.Year.ToString();
            //    string oid = orderid;
            //    string uid = "Temple";
            //    string Sid = "Temple-Donation";    //廟宇拜拜添油錢功德金 PR00004021
            //    string item = "新港奉天宮授權開運商品";
            //    string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
            //    string link = "https://paygate.tw/xpay/pay?uid=";
            //    string PaymentReceiveURL = basePag.GetConfigValue("PaymentMoneymother_ReceiveURL");
            //    string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");
            //    string msisdn = m_phone;
            //    string chrgtype = "1";
            //    string m1 = applicantID.ToString();
            //    string m2 = returnUrl;
            //    string mac = MD5.MD5Encrypt(uid + oid + price + item + paytype + Sid + PaymentReceiveURL + m1 + m2
            //                          + telco + chrgtype + msisdn + Timestamp + ValidationKey).Replace("-", "").ToLower();

            //    string paymentChannelLog = returnUrl;
            //    DatabaseHelper objdatabaseHelper = new DatabaseHelper(basePage);
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
            //    long id = objdatabaseHelper.AddChargeLog_Moneymother(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePag.Request.UserHostAddress, Year);
            //    //long id = 6;

            //    LightDAC objLightDAC = new LightDAC(basePag);

            //    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Moneymother(applicantID, 11, price, Year))
            //    {
            //        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
            //            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
            //            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

            //    }

            //    return link;
            //}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="basePag"></param>
            /// <param name="orderid"></param>
            /// <param name="applicantID"></param>
            /// <param name="paytype"></param>
            /// <param name="telco"></param>
            /// <param name="price"></param>
            /// <param name="m_phone"></param>
            /// <param name="returnUrl"></param>
            /// <param name="Year"></param>
            /// <returns></returns>
            protected string TWWebPay(
                BasePage basePage, 
                string orderid, 
                int applicantID,
                string paytype, 
                string telco, 
                int price, 
                string m_phone, 
                string returnUrl,
                string Year)
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                string oid = orderid;
                string uid = "Temple";
                string Sid = "Temple-Products";    //廟宇拜拜添油錢功德金 PR00004021
                string item = "新港奉天宮授權開運商品";
                string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                string link = "https://paygate.tw/xpay/pay?uid=";
                string PaymentReceiveURL = basePage.GetConfigValue("PaymentMoneymother_ReceiveURL");
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

                // 檢查此購買人訂單是否有訂單產品紀錄
                if (!objLightDAC.CheckedAPPChargeHaving(applicantID, 11, 3, Year, ref oid))
                {
                    if (oid != "")
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;
                    }
                    else
                    {
                        link = "鎮宅錢母擺件文創小販部 文創商品 AppChargeLog ID 錯誤！";
                        basePage.SaveErrorLog(link);
                    }
                }
                else
                {
                    long id = objLightDAC.AddChargeLog_Moneymother(oid, applicantID, price, ChargeType, 0, "", "", paymentChannelLog, basePage.Request.UserHostAddress, Year);
                    //long id = 6;

                    if (id > 0 && objLightDAC.Updatecost2applicantinfo_Moneymother(applicantID, 11, price, Year))
                    {
                        link = link + uid + "&oid=" + oid + "&returl=" + basePage.Server.UrlEncode(PaymentReceiveURL) + "&point=" + price + "&pw=" + paytype
                            + "&sid=" + Sid + "&item=" + item + "&timestamp=" + Timestamp + "&chrgtype=" + chrgtype + "&mac="
                            + mac + "&m1=" + basePage.Server.UrlEncode(m1) + "&m2=" + System.Web.HttpUtility.UrlEncode(m2) + "&telco=" + telco + "&msisdn=" + msisdn;

                    }
                    else
                    {
                        link = "鎮宅錢母擺件文創小販部 文創商品 更新購買人資料 錯誤！";
                        basePage.SaveErrorLog(link);
                    }
                }

                return link;
            }

            //public int InsertApplicantInfo(BasePage basePage, string AppName, string AppMobile, string AdminID, string County, string Dist, string Address, string ZipCode)
            //{
            //    LightDAC objLightDAC = new LightDAC(basePage);

            //    string postURL = "MoneyomotherIndex";

            //    int ApplicantID = objLightDAC.AddApplicantInfo_MoneyMother(AppName, AppMobile, AdminID, County, Dist, Address, ZipCode, postURL);

            //    return ApplicantID;
            //}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="basePage"></param>
            /// <param name="AppName"></param>
            /// <param name="AppMobile"></param>
            /// <param name="AppEmail"></param>
            /// <param name="AdminID"></param>
            /// <param name="County"></param>
            /// <param name="Dist"></param>
            /// <param name="Address"></param>
            /// <param name="ZipCode"></param>
            /// <param name="Year"></param>
            /// <returns></returns>
            public int InsertApplicantInfo(BasePage basePage, string AppName, string AppMobile, string AppEmail, string AdminID, string County, string Dist, string Address, string ZipCode, string Year)
            {
                LightDAC objLightDAC = new LightDAC(basePage);

                string postURL = "MoneyomotherIndex";

                postURL += basePage.Request["twm"] != null ? "_TWM" : "";

                postURL += basePage.Request["cht"] != null ? "_CHT" : "";

                postURL += basePage.Request["line"] != null ? "_LINE" : "";

                postURL += basePage.Request["fb"] != null ? "_FB" : "";

                postURL += basePage.Request["ig"] != null ? "_IG" : "";

                postURL += basePage.Request["fetsms"] != null ? "_fetSMS" : "";

                postURL += basePage.Request["jkos"] != null ? "_JKOS" : "";

                postURL += basePage.Request["gads"] != null ? "_GADS" : "";

                postURL += basePage.Request["tads"] != null ? "_TADS" : "";

                postURL += basePage.Request["ftg"] != null ? "_FTG" : "";

                int ApplicantID = objLightDAC.Addapplicantinfo_Moneymother(
                    AppName, 
                    AppMobile, 
                    AppEmail, 
                    AdminID, 
                    County, 
                    Dist, 
                    Address, 
                    ZipCode, 
                    postURL, 
                    Year);

                return ApplicantID;
            }

            //public int InsertProductInfo(BasePage basePag, int ApplicantID, string Name, int Type, int Count)
            //{
            //    LightDAC objLightDAC = new LightDAC(basePag);

            //    int BuyID = objLightDAC.AddProduct_Moneymother(ApplicantID, Name, Type, Count);

            //    return BuyID;
            //}

            public int InsertProductInfo(BasePage basePag, int ApplicantID, string Name, int Type, int Cost, int Price, int Count, string Year)
            {
                LightDAC objLightDAC = new LightDAC(basePag);

                int BuyID = objLightDAC.AddProduct_MoneyMother(ApplicantID, Name, Type, Cost, Price, Count, Year);

                return BuyID;
            }
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