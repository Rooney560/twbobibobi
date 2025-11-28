using BCFBaseLibrary.Security;
using Read.data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Temple.data;
using TempleAdmin.Helper;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;

namespace Temple
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TWPaymentCallback_Supplies_dh : BasePage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            if (Request["uid"] != null && Request["oid"] != null && Request["tid"] != null && Request["mac"] != null)
            {
                try
                {
                    string uid = "Temple";
                    string tid = Request["tid"];
                    string oid = Request["oid"];
                    string ValidationKey = "Ov7BmaT5l1C89t5FNj0cEsR";
                    string Timestamp = dtNow.ToString("yyyyMMddHHmmssfff");

                    string Year = dtNow.Year.ToString();

                    string m1 = Request["m1"];
                    string m2 = Request["m2"];

                    string mac = MD5.Encode(uid + tid + oid + Timestamp + ValidationKey).Replace("-", "");

                    string[] supplieslist = new string[0];
                    string[] Supplieslist = new string[0];

                    string url = "https://paygate.tw/xpay/committrans?uid=" + uid + "&tid=" + tid + "&oid=" + oid + "&timestamp=" + Timestamp + "&mac=" + mac;

                    string resp = "";
                    if (!BCFBaseLibrary.Net.HTTPClient.Get(url, string.Empty, ref resp))
                    {
                        //resp = "交易網址連結失敗";
                        SaveErrorLog(resp + ", CommitTransAPI 錯誤!");
                    }

                    //if (Request["ad"] != null)
                    //{
                    //    resp = "1|18062216041218500003|100|TELEPAY|twm|0934315020|20180622160559423|F6C5E389052469CC441A402A3F0D0C9F";
                    //}

                    string orderId = oid;
                    string CallbackLog = tid + "," + resp;
                    LightDAC objLightDAC = new LightDAC(this);
                    int aid = int.Parse(m1);
                    int status = 999;
                    string kind = "11";
                    int adminID = 16;
                    Session.Remove("PaymentAuthKey"); // ✅ 看完一次就失效
                    Session["PaymentAuthKey"] = "?kind=" + kind + "&a=16&aid=" + aid;

                    DataTable dtCharge = objLightDAC.GetChargeLog_Supplies_dh(orderId, Year);

                    if (dtCharge.Rows.Count > 0)
                    {
                        bool invStatus = true;
                        if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" || dtCharge.Rows[0]["ChargeType"].ToString() == "Cht" || dtCharge.Rows[0]["ChargeType"].ToString() == "FetCSP")
                        {
                            invStatus = false;
                        }

                        string rebackURL = "https://bobibobi.tw/Temples/templeService_supplies_dh.aspx";

                        if (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm")
                        {
                            rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&twm=1" : rebackURL + "?twm=1";
                        }

                        if (dtCharge.Rows[0]["ChargeType"].ToString() == "Cht")
                        {
                            rebackURL = rebackURL.IndexOf("?") > 0 ? rebackURL + "&cht=1" : rebackURL + "?cht=1";
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

                                    string msg = "感謝購買,已成功付款" + cost + "元,您的訂單編號 ";

                                    //更新天貺納福添運法會資料表並取得訂單編號
                                    objLightDAC.UpdateSupplies_dh_Info(aid, Year, ref msg, ref supplieslist, ref Supplieslist);
                                    objLightDAC.Updateapplicantinfo_Supplies_dh(aid, cost, 2, Year); //更新購買表內購買人狀態為已付款(Status=2)

                                    SMSHepler objSMSHepler = new SMSHepler();
                                    string ChargeType = string.Empty;
                                    int uStatus = 0;

                                    //更新流水付費表資訊(付費成功)
                                    if (objLightDAC.UpdateChargeLog_Supplies_dh(orderId, tid, msg, Request.UserHostAddress, CallbackLog, Year, ref ChargeType, ref uStatus))
                                    {
                                        if (invStatus)
                                        {
                                            try
                                            {
                                                twbobibobi.Data.BasePage _basePage = new twbobibobi.Data.BasePage();
                                                LightDAC objALightDAC = new LightDAC(_basePage);
                                                DataTable dtApplicantInfo = objALightDAC.GetAPPCharge_dh_Supplies(aid, Year);

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

                                                    NumString += string.Join(",", Supplieslist);

                                                    foreach (DataRow row in dtApplicantInfo.Rows)
                                                    {
                                                        string SuppliesType = row["SuppliesType"].ToString();
                                                        string SuppliesString = row["SuppliesString"].ToString();
                                                        string description = $"線上服務費({templeName}-{SuppliesString})";
                                                        string unit = InvoiceHelper.GetUnitByKind(int.Parse(kind));

                                                        int count = int.TryParse(row["Count"].ToString(), out int tmp) ? tmp : 1;
                                                        int quantity = count;

                                                        int Count1 = int.TryParse(row["Count1"].ToString(), out int tmp1) ? tmp1 : 0;
                                                        int Count2 = int.TryParse(row["Count2"].ToString(), out int tmp2) ? tmp2 : 0;
                                                        int Count3 = int.TryParse(row["Count3"].ToString(), out int tmp3) ? tmp3 : 0;
                                                        int Count4 = int.TryParse(row["Count4"].ToString(), out int tmp4) ? tmp4 : 0;
                                                        int Count5 = int.TryParse(row["Count5"].ToString(), out int tmp5) ? tmp5 : 0;
                                                        int Count6 = int.TryParse(row["Count6"].ToString(), out int tmp6) ? tmp6 : 0;
                                                        int Count7 = int.TryParse(row["Count7"].ToString(), out int tmp7) ? tmp7 : 0;
                                                        int Count8 = int.TryParse(row["Count8"].ToString(), out int tmp8) ? tmp8 : 0;
                                                        int Count9 = int.TryParse(row["Count9"].ToString(), out int tmp9) ? tmp9 : 0;
                                                        int Count10 = int.TryParse(row["Count10"].ToString(), out int tmp10) ? tmp10 : 0;
                                                        int Count11 = int.TryParse(row["Count11"].ToString(), out int tmp11) ? tmp11 : 0;
                                                        int Count12 = int.TryParse(row["Count12"].ToString(), out int tmp12) ? tmp12 : 0;
                                                        int Count13 = int.TryParse(row["Count13"].ToString(), out int tmp13) ? tmp13 : 0;
                                                        int Count14 = int.TryParse(row["Count14"].ToString(), out int tmp14) ? tmp14 : 0;

                                                        int Cost = 0;
                                                        Cost += Count1 > 0 ? Count1 * 2388 : 0;
                                                        Cost += Count2 > 0 ? Count2 * 800 : 0;
                                                        Cost += Count3 > 0 ? Count3 * 800 : 0;
                                                        Cost += Count4 > 0 ? Count4 * 700 : 0;
                                                        Cost += Count5 > 0 ? Count5 * 1000 : 0;
                                                        Cost += Count6 > 0 ? Count6 * 1000 : 0;
                                                        Cost += Count7 > 0 ? Count7 * 200 : 0;
                                                        Cost += Count8 > 0 ? Count8 * 600 : 0;
                                                        Cost += Count9 > 0 ? Count9 * 600 : 0;
                                                        Cost += Count10 > 0 ? Count10 * 600 : 0;
                                                        Cost += Count11 > 0 ? Count11 * 600 : 0;
                                                        Cost += Count12 > 0 ? Count12 * 600 : 0;
                                                        Cost += Count13 > 0 ? Count13 * 600 : 0;
                                                        Cost += Count14 > 0 ? Count14 * 600 : 0;

                                                        // 保證至少有數量
                                                        if (quantity <= 0) quantity = 1;

                                                        // 驗證：單筆金額
                                                        decimal calcAmount = quantity * Cost;
                                                        decimal rowCost = Convert.ToDecimal(row["Cost"]);

                                                        if (calcAmount != rowCost)
                                                        {
                                                            string errormsg = $"❌ 金額不符，OrderID={row["OrderID"]}, Kind={kind}, Service={SuppliesString}, " +
                                                                         $"數量={quantity}, 單價={Cost}, 計算金額={calcAmount}, DB金額={rowCost}";
                                                            InvoiceHelper.SaveErrorLog(errormsg);
                                                        }

                                                        items.Add(new ProductItem
                                                        {
                                                            Description = description,
                                                            Quantity = quantity,
                                                            Unit = unit,
                                                            UnitPrice = Cost,
                                                            Amount = calcAmount,
                                                            TaxType = 1
                                                        });

                                                        Emailitems.Add(new InvoiceItem
                                                        {
                                                            Description = "線上服務費",
                                                            ProductName = $"{templeName}-{SuppliesString}",
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

                                                        if (objLightDAC.UpdateInvoiceDetail(aid, adminId, 11, invoiceNo, rawJson, "1", Year))
                                                        {
                                                            if (InvoiceEmailSender.Send(rs, Emailitems, buyerEmail, buyerName, invoiceRow["BuyerIdentifier"]?.ToString() ?? "0000000000", NumString, cost, dtNow, YearROC, Month, Date, Time))
                                                            {
                                                                if (objSMSHepler.SendMsg_SL(mobile, msg))
                                                                {
                                                                    m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=" + kind +
                                                                        "&a=" + adminID +
                                                                        "&aid=" + aid +
                                                                        (ChargeType == "Cht" ? "&cht=1" : "") +
                                                                        (ChargeType == "Twm" ? "&twm=1" : "");

                                                                    Response.Redirect(m2, false);
                                                                    Context.ApplicationInstance.CompleteRequest();
                                                                    invStatus = false;
                                                                }
                                                                else
                                                                {
                                                                    // ❌ 傳送簡訊失敗，記錄錯誤
                                                                    SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"傳送簡訊失敗");
                                                                    Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // ❌ 寄送EMAIL失敗，記錄錯誤
                                                                SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"寄送EMAIL失敗");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // ❌ 發票失敗，記錄錯誤
                                                            SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"更新發票失敗 AdminID: {adminId}" + new Exception(rs.ErrorMessage));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // ❌ 發票開立失敗 → 只寫 Log，不影響訂單流程
                                                        SaveErrorLog("TWPaymentCallback_Supplies_dh 開立發票失敗 AdminID="
                                                                     + adminId + ", OrderID=" + orderId + ", Error=" + rs.ErrorMessage);
                                                    }
                                                }
                                                else
                                                {
                                                    // ❌ 個別筆數處理例外錯誤
                                                    SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"開立發票處理例外, 取得訂單錯誤。");
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                // ❌ 個別筆數處理例外錯誤
                                                SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"開立發票處理例外 ex: " + ex.InnerException.Message);
                                            }

                                            if (invStatus)
                                            {
                                                if (objSMSHepler.SendMsg_SL(mobile, msg))
                                                {
                                                    m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?a=" + adminID + "&aid=" + aid + "&kind=" + kind +
                                                        (ChargeType == "Twm" ? "&twm=1" : "") +
                                                        (ChargeType == "Cht" ? "&cht=1" : "");
                                                    Response.Redirect(m2, true);
                                                }
                                                else
                                                {
                                                    // ❌ 傳送簡訊失敗，記錄錯誤
                                                    SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"傳送簡訊失敗");
                                                    Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (objSMSHepler.SendMsg_SL(mobile, msg))
                                            {
                                                m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?a=" + adminID + "&aid=" + aid + "&kind=" + kind +
                                                    (ChargeType == "Twm" ? "&twm=1" : "") +
                                                    (ChargeType == "Cht" ? "&cht=1" : "");
                                                Response.Redirect(m2, true);
                                            }
                                            else
                                            {
                                                // ❌ 傳送簡訊失敗，記錄錯誤
                                                SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"傳送簡訊失敗");
                                                Response.Write("<script>alert('傳送簡訊失敗。請聯繫管理員。客服電話：04-36092299。');window.location.href='" + rebackURL + "'</script>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // ❌ 更新流水付費表資訊失敗，記錄錯誤
                                        SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"更新流水付費表資訊失敗");
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
                                if (objLightDAC.UpdateChargeStatus_Supplies_dh(orderId, -2, Request.UserHostAddress, CallbackLog, Year))
                                {
                                    // ❌ 此用戶已退款，記錄錯誤
                                    SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"此用戶已退款");
                                    Response.Write("<script>alert('此用戶已退款。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                            else
                            {
                                objLightDAC.UpdateChargeStatus_Supplies_dh(orderId, -1, Request.UserHostAddress, CallbackLog, Year);

                                if (m2.IndexOf("APPPaymentResult") > 0)
                                {
                                    Response.Redirect(m2, true);
                                }
                                else
                                {
                                    // ❌ 付款失敗，記錄錯誤
                                    SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"付款失敗，錯誤代碼：" + result[0]);
                                    Response.Write("<script>alert('付款失敗。錯誤代碼：" + result[0] + "，請聯繫管理員。');window.location.href='" + rebackURL + "'</script>");
                                }
                            }
                        }
                        else if (status == 1)
                        {
                            //已經付費成功。
                            m2 = "https://bobibobi.tw/Temples/templeComplete.aspx?kind=" + kind + "&a=" + adminID + "&aid=" + aid +
                                (dtCharge.Rows[0]["ChargeType"].ToString() == "Twm" ? "&twm=1" : "") +
                                (dtCharge.Rows[0]["ChargeType"].ToString() == "Cht" ? "&cht=1" : "");
                            Response.Redirect(m2, true);
                        }
                        else if (status == -2)
                        {
                            // ❌ 此用戶已退款，記錄錯誤
                            SaveErrorLog("TWPaymentCallback_Supplies_dh" + $"此用戶已退款");
                            Response.Write("<script>alert('此用戶已退款。');window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=16'</script>");
                        }
                        else
                        {
                            SaveErrorLog(resp + ", 此訂單已交易失敗!");
                            Response.Write("<script>alert('此訂單已交易失敗，交易代碼：" + resp + "如有疑問。請洽客服電話：04-36092299。');" +
                                "window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=" + adminID + "'</script>");
                        }
                    }
                    else
                    {
                        SaveErrorLog(resp + ", 取得付款資料失敗!");
                        Response.Write("<script>alert('取得付款資料失敗，錯誤代碼：" + resp + "。客服電話：04-36092299。');" +
                            "window.location.href='https://bobibobi.tw/Temples/templeInfo.aspx?a=" + adminID + "'</script>");
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