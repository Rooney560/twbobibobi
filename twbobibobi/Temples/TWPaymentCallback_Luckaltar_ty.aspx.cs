using BCFBaseLibrary.Security;
using Microsoft.Win32;
using twbobibobi.Data;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using TempleAdmin.Helper;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;

namespace twbobibobi.Temples
{
    public partial class TWPaymentCallback_Luckaltar_ty : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 取得台北標準時間
            DateTime dtNow = LightDAC.GetTaipeiNow();

            if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            {
                try
                {
                    string uid = "Temple";
                    string tid = Request["tid"];
                    string oid = Request["oid"];
                    string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                    string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");

                    string Year = "2026";

                    string m1 = Request["m1"];
                    string m2 = Request["m2"];

                    string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                    string[] luckaltarlist = new string[0];
                    string[] Luckaltarlist = new string[0];

                    string url = "https://paygate.tw/xpay/committrans?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

                    string resp = "";
                    if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
                    {
                        //resp = "交易網址連結失敗";
                        SaveErrorLog(resp + ", CommitTransAPI 錯誤!");
                    }

                    string orderId = oid;
                    string CallbackLog = tid + "," + resp;
                    LightDAC objLightDAC = new LightDAC(this);
                    int aid = int.Parse(m1);
                    int status = 999;
                    string kind = "27";
                    Session.Remove("PaymentAuthKey"); // ✅ 看完一次就失效
                    Session["PaymentAuthKey"] = "?kind=" + kind + "&a=14&aid=" + aid;

                    DataTable dtCharge = new DataTable();

                    dtCharge = objLightDAC.GetChargeLog_Luckaltar_ty(orderId, Year);

                    if (dtCharge.Rows.Count > 0)
                    {
                        bool invStatus = true;
                        if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" || dtCharge.Rows[0]["ChargeType"].ToString() == "Cht" || dtCharge.Rows[0]["ChargeType"].ToString() == "FetCSP")
                        {
                            invStatus = false;
                        }

                        int luckaltartype = objLightDAC.GetLuckaltarType_ty(aid, Year);

                        string rebackURL = "https://bobibobi.tw/Temples/templeService_luckaltar_ty.aspx";

                        DataTable dtData = objLightDAC.Getluckaltar_ty_Info(aid, Year);
                        string purlstring = string.Empty;

                        if (dtData.Rows.Count > 0)
                        {
                            string posturl = dtData.Rows[0]["PostURL"].ToString().ToUpper();
                            if (posturl.IndexOf("TWM") > 0)
                            {
                                purlstring = "&purl=twm";
                                rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&purl=twm" : rebackURL + "?purl=twm";
                            }
                            else if (posturl.IndexOf("CHT") > 0)
                            {
                                purlstring = "&purl=cht";
                                rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&purl=cht" : rebackURL + "?purl=cht";
                            }
                            else if (posturl.IndexOf("PXPAY") > 0)
                            {
                                purlstring = "&purl=pxpayplues";
                                rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&purl=pxpayplues" : rebackURL + "?purl=pxpayplues";
                            }
                        }

                        int cost = 0;
                        int.TryParse(dtCharge.Rows[0]["Amount"].ToString(), out cost);
                        int.TryParse(dtCharge.Rows[0]["Status"].ToString(), out status);
                        if (status == 0)
                        {
                            string[] result = resp.Split("|".ToCharArray());
                            if (result[0] == "1" || result[0] == "2")
                            {
                                if (result.Length > 1)
                                {
                                    string mobile = result[5];

                                    int adminID = 14;

                                    string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + cost + "元，您的訂單編號 ";

                                    //更新普渡資料表並取得訂單編號
                                    objLightDAC.UpdateLuckaltar_ty_Info(aid, luckaltartype, Year, ref msg, ref luckaltarlist, ref Luckaltarlist);
                                    ////更新購買表內購買人狀態為已付款(Status=2)
                                    objLightDAC.Updateapplicantinfo_Luckaltar_ty(aid, cost, 2, Year);

                                    SMSHepler objSMSHepler = new SMSHepler();
                                    string ChargeType = string.Empty;
                                    int uStatus = 0;
                                    //更新流水付費表資訊(付費成功)
                                    if (objLightDAC.UpdateChargeLog_Luckaltar_ty(orderId, tid, msg, Request.UserHostAddress, CallbackLog, Year, ref ChargeType, ref uStatus))
                                    {
                                        if (invStatus)
                                        {
                                            try
                                            {
                                                twbobibobi.Data.BasePage _basePage = new twbobibobi.Data.BasePage();
                                                LightDAC objALightDAC = new LightDAC(_basePage);
                                                DataTable dtApplicantInfo = objALightDAC.GetAPPCharge_ty_Luckaltar(aid, Year);

                                                if (dtApplicantInfo.Rows.Count > 0)
                                                {
                                                    DataRow invoiceRow = dtApplicantInfo.Rows[0]; // 只取代表開發票的那一筆（通常是第一筆）

                                                    // 宮廟名稱
                                                    int adminId = Convert.ToInt32(invoiceRow["AdminID"]);
                                                    string templeName = TempleHelper.GetTempleName(adminId, _basePage);

                                                    // 組發票商品項目（你可以根據多筆資料彙總計算）
                                                    List<ProductItem> items = new List<ProductItem>();

                                                    List<InvoiceItem> Emailitems = new List<InvoiceItem>();

                                                    //訂單編號
                                                    string NumString = "";

                                                    NumString += string.Join(",", Luckaltarlist);

                                                    foreach (DataRow row in dtApplicantInfo.Rows)
                                                    {
                                                        string LuckaltarType = row["LuckaltarType"].ToString();
                                                        string LuckaltarString = row["LuckaltarString"].ToString();
                                                        string description = $"線上服務費({templeName}-{LuckaltarString})";
                                                        int count = int.TryParse(row["Count"].ToString(), out int tmp) ? tmp : 1;
                                                        string unit = InvoiceHelper.GetUnitByKind(int.Parse(kind));

                                                        int quantity = count;

                                                        // 保證至少有數量
                                                        if (quantity <= 0) quantity = 1;

                                                        // 取單價
                                                        int price = InvoiceHelper.GetUnitPrice(adminId, int.Parse(kind), LuckaltarType);

                                                        // 驗證：單筆金額
                                                        decimal calcAmount = quantity * price;
                                                        decimal rowCost = Convert.ToDecimal(row["Cost"]);

                                                        if (calcAmount != rowCost)
                                                        {
                                                            string errormsg = $"❌ 金額不符，OrderID={row["OrderID"]}, Kind={kind}, Service={LuckaltarString}, " +
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
                                                            ProductName = $"{templeName}-{LuckaltarString}",
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

                                                        if (objLightDAC.UpdateInvoiceDetail(aid, adminId, 27, invoiceNo, rawJson, "1", Year))
                                                        {
                                                            if (InvoiceEmailSender.Send(rs, Emailitems, buyerEmail, buyerName, invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000", NumString, cost, dtNow, YearROC, Month, Date, Time))
                                                            {
                                                                if (objSMSHepler.SendMsg_SL(mobile, msg))
                                                                {
                                                                    m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=" + kind +
                                                                        "&a=" + adminID +
                                                                        "&aid=" + aid +
                                                                        (ChargeType == "Cht" && purlstring != string.Empty && purlstring.IndexOf("cht") > 0 ? purlstring : "") +
                                                                        (ChargeType == "Twm" && purlstring != string.Empty && purlstring.IndexOf("twm") > 0 ? purlstring : "") +
                                                                        (ChargeType == "PXPAY" && purlstring != string.Empty && purlstring.IndexOf("pxpayplues") > 0 ? purlstring : "");

                                                                    Response.Redirect(m2, false);
                                                                    Context.ApplicationInstance.CompleteRequest();
                                                                    invStatus = false;
                                                                }
                                                                else
                                                                {
                                                                    // ❌ 傳送簡訊失敗，記錄錯誤
                                                                    SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"傳送簡訊失敗");
                                                                    Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // ❌ 寄送EMAIL失敗，記錄錯誤
                                                                SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"寄送EMAIL失敗");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // ❌ 發票失敗，記錄錯誤
                                                            SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"更新發票失敗 AdminID: {adminId}" + new Exception(rs.ErrorMessage));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // ❌ 發票開立失敗 → 只寫 Log，不影響訂單流程
                                                        SaveErrorLog("TWPaymentCallback_Luckaltar_ty 開立發票失敗 AdminID="
                                                                     + adminId + ", OrderID=" + orderId + ", Error=" + rs.ErrorMessage);
                                                    }
                                                }
                                                else
                                                {
                                                    // ❌ 個別筆數處理例外錯誤
                                                    SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"開立發票處理例外, 取得訂單錯誤。");
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                // ❌ 個別筆數處理例外錯誤
                                                SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"開立發票處理例外 ex: " + ex.InnerException.Message);
                                            }
                                        }

                                        if (invStatus)
                                        {
                                            if (objSMSHepler.SendMsg_SL(mobile, msg))
                                            {
                                                m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=" + kind +
                                                    "&a=" + adminID +
                                                    "&aid=" + aid +
                                                    (ChargeType == "Cht" && purlstring != string.Empty && purlstring.IndexOf("cht") > 0 ? purlstring : "") +
                                                    (ChargeType == "Twm" && purlstring != string.Empty && purlstring.IndexOf("twm") > 0 ? purlstring : "") +
                                                    (ChargeType == "PXPAY" && purlstring != string.Empty && purlstring.IndexOf("pxpayplues") > 0 ? purlstring : "");

                                                Response.Redirect(m2, false);
                                                Context.ApplicationInstance.CompleteRequest();
                                            }
                                            else
                                            {
                                                // ❌ 傳送簡訊失敗，記錄錯誤
                                                SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"傳送簡訊失敗");
                                                Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // ❌ 更新流水付費表資訊，記錄錯誤
                                        SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"更新流水付費表資訊失敗");
                                        Response.Write("<script>alert('付款過程失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                    }
                                }
                                else
                                {
                                    SaveErrorLog(resp + ", 回傳值 錯誤!");
                                    Response.Write("<script>window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                            else if (result[0] == "4")
                            {
                                if (objLightDAC.UpdateChargeStatus_Luckaltar_ty(orderId, -2, Request.UserHostAddress, CallbackLog, Year))
                                {
                                    // ❌ 此用戶已退款，記錄錯誤
                                    SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"此用戶已退款");
                                    Response.Write("<script>alert('此用戶已退款。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                            else
                            {
                                objLightDAC.UpdateChargeStatus_Luckaltar_ty(orderId, -1, Request.UserHostAddress, CallbackLog, Year);

                                if (m2.IndexOf("APPPaymentResult") > 0)
                                {
                                    Response.Redirect(m2, true);
                                }
                                else
                                {
                                    // ❌ 付款失敗，記錄錯誤
                                    SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"付款失敗，錯誤代碼：" + result[0]);
                                    Response.Write("<script>alert('付款失敗，錯誤代碼：" + result[0] + "。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                        }
                        else if (status == 1)
                        {
                            //已經付費成功。
                            m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=" + kind +
                                "&a=14" +
                                "&aid=" + aid +
                                (purlstring != string.Empty && purlstring.IndexOf("cht") > 0 ? purlstring : "") +
                                (purlstring != string.Empty && purlstring.IndexOf("twm") > 0 ? purlstring : "") +
                                (purlstring != string.Empty && purlstring.IndexOf("pxpayplues") > 0 ? purlstring : "");
                            Response.Redirect(m2, true);
                        }
                        else if (status == -2)
                        {
                            // ❌ 此用戶已退款，記錄錯誤
                            SaveErrorLog("TWPaymentCallback_Luckaltar_ty" + $"此用戶已退款");
                            Response.Write("<script>alert('此用戶已退款。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=14'</script>");
                        }
                        else
                        {
                            SaveErrorLog(resp + ", 此訂單已交易失敗!");
                            Response.Write("<script>alert('此訂單已交易失敗，交易代碼：" + resp + "如有疑問。請洽客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=14'</script>");
                        }
                    }
                    else
                    {
                        SaveErrorLog(resp + ", 取得付款資料失敗!");
                        Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=14'</script>");
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                    //忽略
                }
                catch (Exception ex)
                {
                    SaveErrorLog(ex.InnerException.Message + ", 不知道哪裡錯誤!");
                }
            }
            else
            {
                Response.Write("網頁參數錯誤");
                Response.End();
            }
        }


        /// <summary>
        /// 簡訊內容分割，69字數為一則
        /// </summary>
        /// <param name="inputString">參數字符串</param>
        /// <param name="msg">回傳內容陣列</param>
        /// <returns></returns>
        public void msgSubstring(string inputString, ref ArrayList msg)
        {
            int tempLen = inputString.Length;
            int num = tempLen / 69 + 1;
            if (num > 1)
            {
                int str = 0;
                for (int i = 0; i < num; i++)
                {
                    int strLen = 69;
                    if ((inputString.Length - str) / 69 == 0)
                    {
                        strLen = inputString.Length - str;
                    }
                    msg.Add(inputString.Substring(str, strLen));
                    str += 69;
                }
            }
        }
    }
}