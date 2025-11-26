using twbobibobi.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Read.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using TempleAdmin.Helper;
using twbobibobi.ApiClients;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;
using ZXing.QrCode.Internal;

namespace twbobibobi.Temples
{
    /// <summary>
    /// 建立手動傳送 Email 並開立發票 API 頁面：處理 /Api/InvInfosendMail.aspx POST 請求
    /// </summary>
    public partial class InvInfosendMail : AjaxBasePage
    {
        /// <summary>
        /// Page Load 事件：僅接受 POST
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["aid"] != null && Request["a"] != null && Request["kind"] != null)
                {
                    //PostInvoiceAsync();
                    //PostInvoiceAsync_jkos(
                    //    16,
                    //    "陳宣鈴",
                    //    "0989084461",
                    //    "shining770606@gmail.com",
                    //    "",
                    //    "",
                    //    "",
                    //    1500,
                    //    1,
                    //    "贊普",
                    //    true);
                }
                else
                {
                    Response.Write("訪問參數錯誤！");
                }
            }
        }

        /// <summary>
        /// 實際呼叫 InvoiceApiClient 並將結果寫到前端
        /// </summary>
        private void PostInvoiceAsync()
        {
            // 1. 取得台北時區的訂單編號
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            int aid = 0;
            int.TryParse(Request["aid"].ToString(), out aid);
            int a = 0;
            int.TryParse(Request["a"].ToString(), out a);
            string kind = Request["kind"].ToString();
            string Year = dtNow.Year.ToString();
            string[] Lightslist = { "WJSY1171", "WJSY1172" };
            string mobile = "0900671268";
            string msg = "感謝購買,已成功付款1000元,您的訂單編號 藥師燈:WJSY1171,藥師燈:WJSY1172。客服電話：04-36092299。";
            int cost = 1000;

            twbobibobi.Data.BasePage _basePage = new twbobibobi.Data.BasePage();
            LightDAC objALightDAC = new LightDAC(_basePage);
            SMSHepler objSMSHepler = new SMSHepler();
            DataTable dtApplicantInfo = objALightDAC.GetAPPCharge_wjsan_Lights(aid, Year);
            if (dtApplicantInfo.Rows.Count > 0)
            {
                DataRow invoiceRow = dtApplicantInfo.Rows[0]; // 只取代表開發票的那一筆（通常是第一筆）

                string orderId = invoiceRow["OrderID"].ToString();

                bool invStatus = true;
                if (invoiceRow["ChargeType"].ToString() == "Twm" || invoiceRow["ChargeType"].ToString() == "Cht" || invoiceRow["ChargeType"].ToString() == "FetCSP")
                {
                    invStatus = false;
                }

                if (invStatus)
                {
                    // 宮廟名稱
                    int adminId = Convert.ToInt32(invoiceRow["AdminID"]);
                    string templeName = TempleHelper.GetTempleName(adminId, _basePage);
                    List<ProductItem> items = new List<ProductItem>();
                    List<InvoiceItem> Emailitems = new List<InvoiceItem>();

                    //訂單編號
                    string NumString = "";

                    NumString += string.Join(",", Lightslist);
                    foreach (DataRow row in dtApplicantInfo.Rows)
                    {
                        string LightsType = row["LightsType"].ToString();
                        string LightsString = row["LightsString"].ToString();
                        string description = $"線上服務費({templeName}-{LightsString})";
                        int count = int.TryParse(row["Count"].ToString(), out int tmp) ? tmp : 1;
                        string unit = InvoiceHelper.GetUnitByKind(int.Parse(kind));

                        int quantity = count;

                        // 保證至少有數量
                        if (quantity <= 0) quantity = 1;

                        // 取單價
                        int price = InvoiceHelper.GetUnitPrice(adminId, int.Parse(kind), LightsType);

                        // 驗證：單筆金額
                        decimal calcAmount = quantity * price;
                        decimal rowCost = Convert.ToDecimal(row["Cost"]);

                        if (calcAmount != rowCost)
                        {
                            string errormsg = $"❌ 金額不符，OrderID={row["OrderID"]}, Kind={kind}, Service={LightsString}, " +
                                         $"數量={quantity}, 單價={price}, 計算金額={calcAmount}, DB金額={rowCost}";
                            InvoiceHelper.SaveErrorLog(errormsg);
                        }

                        items.Add(new ProductItem
                        {
                            Description = description,
                            Quantity = quantity,
                            Unit = unit,
                            UnitPrice = price,
                            Amount = calcAmount,
                            TaxType = 1
                        });

                        Emailitems.Add(new InvoiceItem
                        {
                            Description = "線上服務費",
                            ProductName = $"{templeName}-{LightsString}",
                            Quantity = quantity,
                            UnitPrice = calcAmount,
                            Taxable = true
                        });
                    }

                    // 載具欄位
                    string carrierId = invoiceRow["CarrierCode"]?.ToString() ?? "";
                    string carrierType = string.IsNullOrEmpty(carrierId) ? "" : "3J0002";

                    // 處理購買人聯絡資訊 fallback
                    string buyerName = !string.IsNullOrEmpty(invoiceRow["AppName"]?.ToString())
                        ? invoiceRow["AppName"].ToString()
                        : (!string.IsNullOrEmpty(invoiceRow["Name"]?.ToString())
                            ? invoiceRow["Name"].ToString()
                            : "");

                    string buyerMobile = !string.IsNullOrEmpty(invoiceRow["AppMobile"]?.ToString())
                        ? invoiceRow["AppMobile"].ToString()
                        : (!string.IsNullOrEmpty(invoiceRow["Mobile"]?.ToString())
                            ? invoiceRow["Mobile"].ToString()
                            : "");

                    string buyerEmail = !string.IsNullOrEmpty(invoiceRow["AppEmail"]?.ToString())
                        ? invoiceRow["AppEmail"].ToString()
                        : (!string.IsNullOrEmpty(invoiceRow["Email"]?.ToString())
                            ? invoiceRow["Email"].ToString()
                            : "");

                    //string buyerTaxId = invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000";

                    // 組發票輸入資料
                    var input = new InvoiceWrapperInput
                    {
                        OrderId = orderId,
                        Scenario = InvoiceProcessor.GetScenario(invoiceRow["InvoiceType"].ToString()),
                        Items = items,
                        BuyerIdentifier = invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000",
                        BuyerName = string.IsNullOrEmpty(invoiceRow["BuyerName"]?.ToString()) ? buyerName : invoiceRow["BuyerName"].ToString(),
                        BuyerAddress = "",
                        BuyerTelephoneNumber = buyerMobile,
                        BuyerEmailAddress = buyerEmail,
                        MainRemark = "",
                        CarrierType = carrierType,
                        CarrierId = carrierId,
                        NPOBAN = ""
                    };

                    // 呼叫共用發票處理器
                    var rs = InvoiceProcessor.ProcessInvoice(input);

                    if (rs.Success)
                    {
                        // 成功：準備寫入 InvoiceDetail
                        // ✅ 成功：可以從 result 中取出你要寫入 DB 的資料
                        string invoiceNo = rs.InvoiceNumber;
                        string barcode = rs.Barcode;
                        string random = rs.RandomNumber;
                        string qrcode1 = rs.QrCodeLeft;
                        string qrcode2 = rs.QrCodeRight;
                        string rawJson = rs.RawJson;

                        string YearROC = (dtNow.Year - 1911).ToString("000");
                        string Month = dtNow.Month.ToString("00"); ;
                        string Date = dtNow.ToString("yyyy-MM-dd");
                        string Time = dtNow.ToString("HH:mm:ss");

                        if (objALightDAC.UpdateInvoiceDetail(aid, adminId, 1, invoiceNo, rawJson, "1", Year))
                        {
                            SendEmailandSMS(
                                rs,
                                Emailitems,
                                buyerEmail,
                                buyerName,
                                invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000",
                                NumString,
                                cost,
                                dtNow,
                                YearROC,
                                Month,
                                Date,
                                Time,
                                mobile,
                                msg,
                                orderId,
                                invoiceNo);
                            //if (InvoiceEmailSender.Send(rs, Emailitems, buyerEmail, buyerName, invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000", NumString, cost, dtNow, YearROC, Month, Date, Time))
                            //{
                            //    Response.Write($"{orderId} 寄送完成！");
                            //}
                        }
                        else
                        {
                            // ❌ 發票失敗，記錄錯誤
                            SaveErrorLog("TWPaymentCallback_Lights_wjsan" + $"更新發票失敗");
                        }
                    }
                    else
                    {
                        // ❌ 發票失敗，記錄錯誤
                        SaveErrorLog("TWPaymentCallback_Lights_wjsan" + $"開立發票失敗 AdminID: {adminId}, " + new Exception(rs.ErrorMessage));
                    }
                }
                else
                {
                    Response.Write($"{orderId} 是小額付費！");
                }
            }
        }

        /// <summary>
        /// 實際呼叫 InvoiceApiClient 並將結果寫到前端 (純開立發票後顯示發票號碼)
        /// </summary>
        private void PostInvoiceAsync_jkos(
            int adminId,
            string appName,
            string appMobile,
            string appEmail,
            string orderId,
            string mobile,
            string msg,
            int cost,
            int ServiceType,
            string ServiceString,
            bool sendEmail = false, 
            bool sendSMS = false,
            string[] orderlist = null,
            string[] Orderlist = null)
        {
            // 1. 取得台北時區的訂單編號
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            string Year = dtNow.Year.ToString();
            //mobile = "0927788782";
            //msg = "您已完成中元普渡線上報名！訂單編號TY1360。廟方將於農曆七月期間舉辦法會，屆時由專人代為誦經超薦。感謝您的參與與功德發心。";
            //cost = 3600;

            twbobibobi.Data.BasePage _basePage = new twbobibobi.Data.BasePage();
            LightDAC objALightDAC = new LightDAC(_basePage);
            SMSHepler objSMSHepler = new SMSHepler();

            if (adminId > 0)
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    orderId = dtNow.ToString("yyyyMMddHHmmssfff");
                }

                string templeName = TempleHelper.GetTempleName(adminId, _basePage);
                List<ProductItem> items = new List<ProductItem>();
                List<InvoiceItem> Emailitems = new List<InvoiceItem>();

                // 如果是 null，就轉成空陣列，避免 NullReferenceException
                orderlist = orderlist ?? new string[0];
                Orderlist = Orderlist ?? new string[0];

                //訂單編號
                string NumString = "";

                NumString += string.Join(",", Orderlist);

                string PurdueType = ServiceType.ToString();
                string PurdueString = ServiceString;
                string description = $"線上服務費({templeName}-{PurdueString})";

                //int count = 0;
                //int.TryParse(row["Count"].ToString(), out count);
                //int count_3rice = 0;
                //int.TryParse(row["Count_3rice"].ToString(), out count_3rice);
                //int count_50rice = 0;
                //int.TryParse(row["Count_50rice"].ToString(), out count_50rice);

                //int quantity = count + count_3rice + count_50rice;
                int quantity = 1;

                items.Add(new ProductItem
                {
                    Description = description,
                    Quantity = quantity,
                    Unit = "份",
                    UnitPrice = cost,
                    Amount = cost,
                    TaxType = 1
                });

                Emailitems.Add(new InvoiceItem
                {
                    Description = "線上服務費",
                    ProductName = $"{templeName}-{PurdueString}",
                    Quantity = quantity,
                    UnitPrice = cost,
                    Taxable = true
                });

                // 載具欄位
                //string carrierId = invoiceRow["CarrierCode"]?.ToString() ?? "";
                //string carrierType = string.IsNullOrEmpty(carrierId) ? "" : "3J0002";

                // 處理購買人聯絡資訊 fallback
                string buyerName = !string.IsNullOrEmpty(appName)
                    ? appName : "";

                string buyerMobile = !string.IsNullOrEmpty(appMobile)
                    ? appMobile : "";

                string buyerEmail = !string.IsNullOrEmpty(appEmail)
                    ? appEmail : "";

                //string buyerTaxId = invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000";

                // 組發票輸入資料
                var input = new InvoiceWrapperInput
                {
                    OrderId = orderId,
                    Scenario = InvoiceProcessor.GetScenario("1"),
                    Items = items,
                    BuyerIdentifier = "0000000000",
                    BuyerName = buyerName,
                    BuyerAddress = "",
                    BuyerTelephoneNumber = buyerMobile,
                    BuyerEmailAddress = buyerEmail,
                    MainRemark = "",
                    CarrierType = "",
                    CarrierId = "",
                    NPOBAN = ""
                };

                // 呼叫共用發票處理器
                var rs = InvoiceProcessor.ProcessInvoice(input);

                if (rs.Success)
                {
                    // 成功：準備寫入 InvoiceDetail
                    // ✅ 成功：可以從 result 中取出你要寫入 DB 的資料
                    string invoiceNo = rs.InvoiceNumber;
                    string barcode = rs.Barcode;
                    string random = rs.RandomNumber;
                    string qrcode1 = rs.QrCodeLeft;
                    string qrcode2 = rs.QrCodeRight;
                    string rawJson = rs.RawJson;

                    string YearROC = (dtNow.Year - 1911).ToString("000");
                    string Month = dtNow.Month.ToString("00"); ;
                    string Date = dtNow.ToString("yyyy-MM-dd");
                    string Time = dtNow.ToString("HH:mm:ss");

                    Response.Write($"{mobile} 寄送完成！OrderID: {orderId}, 發票號碼: {invoiceNo}");
                }
                else
                {
                    // ❌ 發票失敗，記錄錯誤
                    SaveErrorLog("TWPaymentCallback_Purdue_ty" + $"開立發票失敗 AdminID: {adminId}, " + new Exception(rs.ErrorMessage));
                }
            }
        }

        /// <summary>
        /// 寄送簡訊及EMAIL
        /// </summary>
        private void SendEmailandSMS(
            InvoiceResponseDto rs, 
            List<InvoiceItem> Emailitems,
            string buyerEmail,
            string buyerName,
            string buyerTaxId,
            string NumString,
            int cost,
            DateTime dtNow,
            string YearROC,
            string Month,
            string Date,
            string Time,
            string mobile,
            string msg,
            string orderID, 
            string invoiceNo)
        {
            SMSHepler objSMSHepler = new SMSHepler();
            if (InvoiceEmailSender.Send(rs, Emailitems, buyerEmail, buyerName, buyerTaxId, NumString, cost, dtNow, YearROC, Month, Date, Time))
            {
                //Response.Write($"{mobile} 寄送完成！OrderID: {orderID}, 發票號碼: {invoiceNo}");
                if (objSMSHepler.SendMsg_SL(mobile, msg))
                {
                    Response.Write($"{mobile} 寄送完成！OrderID: {orderID}, 發票號碼: {invoiceNo}");
                }
            }
        }
    }
}