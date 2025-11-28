using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using WorkTime.data;
using Org.BouncyCastle.Utilities.Encoders;
using System.Net.Mime;
using twbobibobi.Data;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.Services;

namespace Temple
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        public string barDataUri = string.Empty;
        public string qrDataUri1 = string.Empty;
        public string qrDataUri2 = string.Empty;

        public string CustomerName = "ROONEY";
        public string LogoUrl = "https://bobibobi.tw/Temples/images/Logo_no.png";
        public string UniformInvoiceNum = "NK-06966880";
        public string UniformInvoiceNum2 = "NK06966880";
        public string yearl = "114";
        public string month = "05";
        public string nowDate = "2025-05-16";
        public string nowTime = "23:59:59";
        public string random_number = "7018";
        public string total = "620";
        public string bName = "九九商通科技有限公司";
        public string BuyName = "線上服務費";
        public string ProductName = "大甲鎮瀾宮-光明燈";
        public string bAddress = "台中市西屯區台灣大道四段925號5樓之9";
        public string taxpayerID = "54605358";  //九九商通統編
        public string buytaxpayerID = "買方：82893106";  //買方統一編號 ex: 買方：12345678
        public string OrderID = "12346578";

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. 準備 保必保庇線上宮廟平台 發票資訊
            string yearPeriod = "11406";              // 114年05-06月
            string invoiceNumber = "NK11291591";      // 發票號碼（去掉連字符）
            string randomCode = "5361";               // 隨機碼
            // 組成一維條碼的 19 碼字串
            string code39Content = $"{yearPeriod}{invoiceNumber}{randomCode}";

            // 2. 產生 Code 39 條碼
            byte[] barcodePng = EmailImageHelper.GetBarcode(code39Content, width: 300, height: 80);

            // 3. 產生兩個 QR Code（尺寸 100×100）
            //    真實情況下，qrContent 應照財政部 spec 拼出完整 77 碼以上的字串
            string qrContent = code39Content;
            byte[] qrPng1 = EmailImageHelper.GetQRcode(qrContent, width: 100, height: 100);
            byte[] qrPng2 = EmailImageHelper.GetQRcode(qrContent, width: 100, height: 100);

            // 4. 轉成 Base64 Data URI，方便直接嵌在 <img> 中
             barDataUri = "data:image/png;base64," + Convert.ToBase64String(barcodePng);
             qrDataUri1 = "data:image/png;base64," + Convert.ToBase64String(qrPng1);
             qrDataUri2 = "data:image/png;base64," + Convert.ToBase64String(qrPng2);

            Console.WriteLine("一維條碼 (Code39) Data URI：");
            Console.WriteLine(barDataUri);
            Console.WriteLine();
            Console.WriteLine("QR Code 1 Data URI：");
            Console.WriteLine(qrDataUri1);
            Console.WriteLine();
            Console.WriteLine("QR Code 2 Data URI：");
            Console.WriteLine(qrDataUri2);


            SendMail();

        }

        public void SendMail()
        {
            // 組建發票模型資料
            var model = new InvoiceModel
            {
                CustomerName = "ROONEY",
                LogoUrl = "https://bobibobi.tw/Temples/images/Logo_no.png",
                Year = "114",
                Month = "05",
                InvoiceNumber = "NK-11291591",
                Date = "2025-05-16",
                Time = "23:59:59",
                RandomCode = "5361",
                TotalAmount = 620,
                SellerName = "九九商通科技有限公司",
                SellerAddress = "台中市西屯區台灣大道四段925號5樓之9",
                SellerTaxId = "54605358",
                BuyerTaxId = "買方：82893106",
                NumString = "光012345",
                BarcodeStr = "11406AC123875974115",
                Qrcode_leftStr = "AC1238759711406244115000000a0000000a82808062312345678ynP6P9mSgOQjIIg1jusZVA==:**********:1:2:0:",
                Qrcode_rightStr = "**測試商品1:1:170",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Description = "線上點燈服務費",
                        ProductName = "大甲鎮瀾宮-光明燈",
                        Quantity    = 1,
                        UnitPrice   = 620,
                        Taxable     = true
                    }
                }
            };

            // 呼叫服務寄送 Email
            var service = new InvoiceEmailService();
            service.SendInvoice(model, "service@appssp.com", "smallpotato560@gmail.com", "保必保庇線上宮廟平台", "【保必保庇線上宮廟平台】您的電子發票");
        }

        //public void sendGmail()
        //{
        //    //https://blog.user.today/gmail-smtp-authentication-required/
        //    //寄件者:smallpotato560 兩步驗證
        //    //收件者:kill520560


        //    MailMessage mail = new MailMessage();
        //    //前面是發信email後面是顯示的名稱
        //    mail.From = new MailAddress("service@appssp.com", "保必保庇線上宮廟平台");

        //    //收信者email
        //    mail.To.Add("yh@appssp.com");

        //    //設定優先權
        //    mail.Priority = MailPriority.Normal;

        //    //標題
        //    mail.Subject = "【保必保庇線上宮廟平台】您的電子發票";

        //    //內容
        //    //mail.Body = "<table border='1'><tr><th>訂單編號</th><th>用戶名稱</th><th>用戶電話</th><th>用戶地址</th></tr>" +
        //    //            "<tr><td>HIHI,Wellcome</td><td>HIHI,Wellcome</td><td>HIHI,Wellcome</td><td>HIHI,Wellcome</td></tr></table>";

        //    string htmlBody = BuildInvoiceBody();

        //    // 建立 AlternateView
        //    var av = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

        //    var logoRes = EmailImageHelper.DownloadImageAsLinkedResource(
        //        "https://bobibobi.tw/Temples/images/Logo_no.png",
        //        "logoCid",
        //        "image/png");

        //    var barcodeRes = EmailImageHelper.Base64ImageAsLinkedResource(
        //        barDataUri,
        //        "barcodeCid",
        //        "image/png"
        //        );

        //    var qr1Res = EmailImageHelper.QRCodeImageAsLinkedResource(
        //        qrDataUri1,
        //        "qr1Cid",
        //        "image/png"
        //        );

        //    var unnamedCidRes = EmailImageHelper.DownloadImageAsLinkedResource(
        //        "https://bobibobi.tw/Temples/images/unnamed.gif",
        //        "unnamedCid",
        //        "image/png");

        //    var qr2Res = EmailImageHelper.QRCodeImageAsLinkedResource(
        //        qrDataUri2,
        //        "qr2Cid",
        //        "image/png"
        //        );

        //    // 把 LinkedResource 加到 AlternateView
        //    av.LinkedResources.Add(logoRes);
        //    av.LinkedResources.Add(barcodeRes);
        //    av.LinkedResources.Add(qr1Res);
        //    av.LinkedResources.Add(unnamedCidRes);
        //    av.LinkedResources.Add(qr2Res);

        //    // 把 AlternateView 加到 mail
        //    mail.AlternateViews.Add(av);
        //    //mail.Body = 

        //    //內容使用html
        //    mail.IsBodyHtml = true;

        //    //設定gmail的smtp (這是google的)
        //    SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

        //    //您在gmail的帳號密碼
        //    MySmtp.Credentials = new System.Net.NetworkCredential("service@appssp.com", "txju nnco hwxe lyvy");

        //    //開啟ssl
        //    MySmtp.EnableSsl = true;

        //    //發送郵件
        //    MySmtp.Send(mail);

        //    //放掉宣告出來的MySmtp
        //    MySmtp = null;

        //    //放掉宣告出來的mail
        //    mail.Dispose();
        //}

        //public string BuildInvoiceBody()
        //{
        //    return $@"
        //            <body runat=""server"">
        //              <table width=""1000"" border=""0"" style=""margin: 0 auto; background-color: #fff"">
        //                <thead>
        //                  <tr>
        //                    <td style=""font-size:12px;color:#404040;line-height:30px"">
        //                      親愛的<span style=""font-size:12px;color:#0066ff;margin:0 3px"">{CustomerName}</span>您好，以下是您的發票開立通知。
        //                    </td>
        //                  </tr>
        //                </thead>
        //                <tbody>
        //                  <tr>
        //                    <td>
        //                      <table width=""100%"" border=""0"" style=""padding:10px 0;border:1px solid #666"">
        //                        <tbody>
        //                          <tr>
        //                            <!-- 左側發票區 -->
        //                            <td width=""240"" rowspan=""2"" valign=""top"" style=""padding:0 10px;border-right:1px dotted #666;"">
        //                              <table width=""100%"" border=""0"">
        //                                <tr>
        //                                  <td height=""25"" align=""center"">
        //                                    <img src=""cid:logoCid"" width=""220"" height=""50"" style=""vertical-align:middle;"" alt=""保必保庇線上宮廟平台"" title=""保必保庇線上宮廟平台""/>
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td align=""center"" style=""font-size:15px;line-height:30px;color:#f00;"">
        //                                    *此為發票開立通知，非正式發票*
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td align=""center"" style=""font-size:30px;line-height:30px;"">
        //                                    <b style=""margin-right:3px"">{m.yearl}</b>年
        //                                    <span style=""padding:0 3px"">
        //                                      <b style=""padding-right:3px"">{m.month}</b>-<b style=""padding-left:3px"">06</b>
        //                                    </span>月
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td align=""center"" style=""font-size:14px;"">{m.InvoiceNumber}</td>
        //                                </tr>
        //                                <tr>
        //                                  <td>
        //                                    <table width=""100%"" border=""0"" cellspacing=""0"">
        //                                      <tr>
        //                                        <td valign=""baseline"">
        //                                          <span style=""margin-right:5px"">{m.Date}</span><span>{m.Time}</span>
        //                                        </td>
        //                                        <td valign=""baseline"" align=""right""></td>
        //                                      </tr>
        //                                    </table>
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td>
        //                                    <table width=""100%"" border=""0"" cellspacing=""0"">
        //                                      <tr>
        //                                        <td valign=""baseline"">隨機碼：<span style=""font-size:15px;line-height:15px"">{m.RandomCode}</span></td>
        //                                        <td valign=""baseline"" align=""right"">總計：<span style=""font-size:15px;line-height:15px"">{m.TotalAmount}</span></td>
        //                                      </tr>
        //                                    </table>
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td>
        //                                    <table width=""100%"" border=""0"" cellspacing=""0"">
        //                                      <tr>
        //                                        <td valign=""baseline"">賣方：<span style=""font-size:15px;line-height:15px"">{m.SellerTaxId}</span></td>
        //                                        <td valign=""baseline"" align=""right"">{m.BuyerTaxId}</td>
        //                                      </tr>
        //                                    </table>
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td>
        //                                    <img src=""cid:barcodeCid"" width=""230"" height=""36"" alt=""一維條碼""/>
        //                                  </td>
        //                                </tr>
        //                                <tr>
        //                                  <td align=""center"">
        //                                    <img src=""cid:qrLeftDataUri"" width=""90"" height=""90"" alt=""QR Code 左"" style=""margin-right:5px;""/>
        //                                    <img src=""cid:unnamedCid"" width=""26"" height=""90"" class=""CToWUd"" data-bit=""iit""/>
        //                                    <img src=""cid:qrRightDataUri"" width=""90"" height=""90"" alt=""QR Code 右""/>
        //                                  </td>
        //                                </tr>
        //                              </table>
        //                            </td>

        //                            <!-- 右側服務明細 -->
        //                            <td valign=""top"" style=""padding:0 10px;"">
        //                              <p style=""font-size:18px;line-height:20px;text-align:center;margin:0;padding:0;"">服務明細</p>
        //                              <table width=""100%"" border=""0"" style=""font-size:13px;line-height:15px;"">
        //                                <tr>
        //                                  <td valign=""top"">{m.SellerName}<br/>
        //                                      營業人地址：{m.SellerAddress}
        //                                  </td>
        //                                  <td width=""200"" valign=""top"">
        //                                    訂單號碼：{m.NumString}<br/>
        //                                    發票號碼：{m.InvoiceNumberNoDash}
        //                                  </td>
        //                                </tr>
        //                              </table>
        //                              <table width=""100%"" border=""0"" cellspacing=""0"" style=""margin-top:8px;font-size:13px;line-height:15px;"">
        //                                <thead>
        //                                  <tr style=""background:#f7f7f7;"">
        //                                    <th align=""left"" style=""padding:4px;"">項目</th>
        //                                    <th align=""left"" style=""padding:4px;"">品名</th>
        //                                    <th align=""center"" style=""padding:4px;"">數量</th>
        //                                    <th align=""right"" style=""padding:4px;"">單價</th>
        //                                    <th align=""right"" style=""padding:4px;"">金額</th>
        //                                    <th align=""center"" style=""padding:4px;"">TX</th>
        //                                  </tr>
        //                                </thead>
        //                                <tbody>
        //                                  <!-- 這裡可以用迴圈動態產生多筆商品 -->
        //                                {string.Join("", m.Items.Select(i => 
        //                                  $"<tr>" +
        //                                  $"<td style=\"padding:4px;\">{i.Description}</td>" +
        //                                  $"<td style=\"padding:4px;\">{i.ProductName}</td>" +
        //                                  $"<td align='center'>{i.Quantity}</td>" +
        //                                  $"<td align='right'>{i.UnitPrice}</td>" +
        //                                  $"<td align='right'>{i.Quantity * i.UnitPrice}</td>" +
        //                                  $"<td align='center'>{(i.Taxable ? "TX" : "")}</td>" +
        //                                  $"</tr>"))}
        //                                </tbody>
        //                              </table>
        //                            </td>
        //                          </tr>
        //                          <tr>
        //                            <td colspan=""2"" valign=""bottom"" style=""padding:0 10px;"">
        //                              <table width=""100%"" border=""0"">
        //                                <tr>
        //                                  <td align=""right"">
        //                                    <table border=""0"" style=""margin:0 20px 0 0;font-size:13px;line-height:15px;"">
        //                                      <tr>
        //                                        <td align=""right"">總計：</td>
        //                                        <td align=""right"" width=""90"" style=""padding:0 10px 0 0"">$<span>{m.TotalAmount}</span></td>
        //                                      </tr>
        //                                    </table>
        //                                  </td>
        //                                  <td valign=""top"">合計：<span style=""margin-left:50px"">{m.Items.Count}</span> 項</td>
        //                                </tr>
        //                              </table>
        //                            </td>
        //                          </tr>
        //                        </tbody>
        //                      </table>
        //                    </td>
        //                  </tr>
        //                </tbody>
        //                <tfoot>
        //                  <tr>
        //                    <td>
        //                      <table width=""100%"" border=""0"" style=""font-size:12px;line-height:18px;color:#666;"">
        //                        <tr><td valign=""top"">●</td><td>依財政部令此副本僅供參考，不可直接兌獎。</td></tr>
        //                        <tr><td valign=""top"">●</td><td>統一發票給獎辦法第11條規定，發票金額為 0，不能兌換。</td></tr>
        //                      </table>
        //                    </td>
        //                  </tr>
        //                </tfoot>
        //              </table>
        //            </body>
        //            ";
        //}

    }
}