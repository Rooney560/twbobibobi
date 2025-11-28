/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：EmailTemplateService.cs
 *  類別說明：統一管理 Email HTML 模板內容，依不同用途回傳對應樣板
 *
 *  建立日期：2025-11-10
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版，支援發票、驗證碼、感謝狀三種模板
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using System;
using System.Linq;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 統一管理 Email HTML 模板內容的服務
    /// </summary>
    public static class EmailTemplateService
    {
        /// <summary>
        /// 根據模板類型與資料模型，生成對應的 HTML 郵件內容
        /// </summary>
        /// <param name="type">模板類型</param>
        /// <param name="model">模板資料模型</param>
        /// <returns>HTML 格式字串</returns>
        public static string GetTemplate(EmailTemplateType type, object model)
        {
            switch (type)
            {
                case EmailTemplateType.Invoice:
                    return BuildInvoiceHtml((InvoiceModel)model);
                case EmailTemplateType.Captcha:
                    return BuildCaptchaHtml((CaptchaModel)model);
                case EmailTemplateType.ThankYou:
                    return BuildThankYouHtml((ThankYouModel)model);
                default:
                    throw new NotSupportedException($"不支援的模板類型：{type}");
            }
        }

        /// <summary>
        /// 根據指定的日期，計算對應的統一發票雙月期別文字。
        /// 每年共 6 期：01-02、03-04、05-06、07-08、09-10、11-12。
        /// </summary>
        /// <param name="invoiceDate">開立發票的日期</param>
        /// <returns>格式化的發票期別文字（例如：114年 05-06 月）</returns>
        public static string GetInvoicePeriodHtml(DateTime invoiceDate)
        {
            int month = invoiceDate.Month;
            int year = invoiceDate.Year - 1911;

            int periodStart = ((month - 1) / 2) * 2 + 1;
            int periodEnd = periodStart + 1;

            return $@"<b style=""margin-right:3px"">{year}</b>年
              <span style=""padding:0 3px"">
                <b style=""padding-right:3px"">{periodStart:00}</b>-<b style=""padding-left:3px"">{periodEnd:00}</b>
              </span>月";
        }

        /// <summary>
        /// 發票模板 - 發票 Email 的 HTML 內容，可替換為使用模板引擎
        /// </summary>
        private static string BuildInvoiceHtml(InvoiceModel m)
        {
            // 設定時區為台北標準時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            var ts = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
            return $@"
                      <body runat=""server"" style=""margin:0;font-size:13px;color:#333333;"">
                      <!-- 隱藏區塊，用極小尺寸＋透明色留一個實際文字 -->
                      <span style=""font-size:1px;line-height:1px;color:transparent;display:block;"">
                        &#8205;{ts}&#8205;
                      </span>
                      <table width=""1000"" border=""0"" style=""margin: 0 auto; background-color: #fff!important"">
                        <thead>
                          <tr>
                            <td style=""font-size:12px;color:#404040;line-height:30px"">
                              親愛的<span style=""font-size:12px;color:#0066ff;margin:0 3px"">{m.CustomerName}</span>您好，以下是您的發票開立通知。
                            </td>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td>
                              <table width=""100%"" border=""0"" style=""padding:10px 0;border:1px solid #666"">
                                <tbody>
                                  <tr>
                                    <!-- 左側發票區 -->
                                    <td width=""240"" rowspan=""2"" valign=""top"" style=""padding:0 10px;border-right:1px dotted #666;"">
                                      <table width=""100%"" border=""0"">
                                        <tr>
                                          <td height=""25"" align=""center"">
                                            <img src=""cid:logoCid"" width=""220"" height=""50"" style=""vertical-align:middle;"" alt=""保必保庇線上宮廟平台"" title=""保必保庇線上宮廟平台""/>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align=""center"" style=""font-size:15px;line-height:30px;color:#f00;"">
                                            *此為發票開立通知，非正式發票*
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align=""center"" style=""font-size:30px;line-height:30px;"">
                                            {GetInvoicePeriodHtml(m.dtNow)}
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align=""center"" style=""font-size:14px;"">{m.InvoiceNumberWithDash}</td>
                                        </tr>
                                        <tr>
                                          <td>
                                            <table width=""100%"" border=""0"" cellspacing=""0"">
                                              <tr>
                                                <td valign=""baseline"">
                                                  <span style=""margin-right:5px"">{m.Date}</span><span>{m.Time}</span>
                                                </td>
                                                <td valign=""baseline"" align=""right""></td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td>
                                            <table width=""100%"" border=""0"" cellspacing=""0"">
                                              <tr>
                                                <td valign=""baseline"">隨機碼：<span style=""font-size:15px;line-height:15px"">{m.RandomCode}</span></td>
                                                <td valign=""baseline"" align=""right"">總計：<span style=""font-size:15px;line-height:15px"">{m.TotalAmount}</span></td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td>
                                            <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                              <tr>
                                                <td valign=""baseline"">賣方：<span style=""font-size:15px;line-height:15px"">{m.SellerTaxId}</span></td>
                                                <td valign=""baseline"" align=""right"">{(m.BuyerTaxId == "0000000000" ? "" : $"賣方：<span style=\"font-size:15px;line-height:15px\">{m.BuyerTaxId}")}</span></td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td>
                                            <img src=""cid:barcodeCid"" width=""230"" height=""36"" alt=""一維條碼""/>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td align=""center"">
                                            <img
                                              src=""cid:qrLeftDataUri""
                                              alt=""QR 左""
                                              style=""
                                                width:90px!important;
                                                height:90px!important;
                                                max-width:none!important;
                                                display:inline-block;
                                                margin-right:5px;
                                              ""/>
                                            <img src=""cid:unnamedCid"" width=""26"" height=""90"" class=""CToWUd"" data-bit=""iit""/>
                                            <img
                                              src=""cid:qrRightDataUri""
                                              alt=""QR 右""
                                              style=""
                                                width:90px!important;
                                                height:90px!important;
                                                max-width:none!important;
                                                display:inline-block;
                                              ""/>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>

                                    <!-- 右側服務明細 -->
                                    <td valign=""top"" style=""padding:0 10px;"">
                                      <p style=""font-size:18px;line-height:20px;text-align:center;margin:0;padding:0;"">服務明細</p>
                                      <table width=""100%"" border=""0"" style=""font-size:13px;line-height:15px;"">
                                        <tr>
                                          <td valign=""top"">{m.SellerName}<br/>
                                              營業人地址：{m.SellerAddress}
                                          </td>
                                          <td width=""200"" valign=""top"">
                                            訂單號碼：{m.NumString}<br/>
                                            發票號碼：{m.InvoiceNumber}
                                          </td>
                                        </tr>
                                      </table>
                                      <table width=""100%"" border=""0"" cellspacing=""0"" style=""margin-top:8px;font-size:13px;line-height:15px;"">
                                        <thead>
                                          <tr style=""background:#f7f7f7;"">
                                            <th align=""left"" style=""padding:4px;"">項目</th>
                                            <th align=""left"" style=""padding:4px;"">品名</th>
                                            <th align=""center"" style=""padding:4px;"">數量</th>
                                            <th align=""right"" style=""padding:4px;"">單價</th>
                                            <th align=""right"" style=""padding:4px;"">金額</th>
                                            <th align=""center"" style=""padding:4px;"">TX</th>
                                          </tr>
                                        </thead>
                                        <tbody>
                                          <!-- 這裡可以用迴圈動態產生多筆商品 -->
                                        {string.Join("", m.Items.Select(i =>
                                          $"<tr>" +
                                          $"<td style=\"padding:4px;\">{i.Description}</td>" +
                                          $"<td style=\"padding:4px;\">{i.ProductName}</td>" +
                                          $"<td align='center'>{i.Quantity}</td>" +
                                          $"<td align='right'>{i.UnitPrice}</td>" +
                                          $"<td align='right'>{i.Quantity * i.UnitPrice}</td>" +
                                          $"<td align='center'>{(i.Taxable ? "TX" : "")}</td>" +
                                          $"</tr>"))}
                                        </tbody>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td colspan=""2"" valign=""bottom"" style=""padding:0 10px;"">
                                      <table width=""100%"" border=""0"">
                                        <tr>
                                          <td align=""right"">
                                            <table border=""0"" style=""margin:0 20px 0 0;font-size:13px;line-height:15px;"">
                                              <tr>
                                                <td align=""right"">總計：</td>
                                                <td align=""right"" width=""90"" style=""padding:0 10px 0 0"">$<span>{m.TotalAmount}</span></td>
                                              </tr>
                                            </table>
                                          </td>
                                          <td valign=""top"">合計：<span style=""margin-left:50px"">{m.Items.Count}</span> 項</td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                        <tfoot>
                          <tr>
                            <td>
                              <table width=""100%"" border=""0"" style=""font-size:12px;line-height:18px;color:#666;"">
                                <tr><td valign=""top"">●</td><td>依財政部令此副本僅供參考，不可直接兌獎。</td></tr>
                                <tr><td valign=""top"">●</td><td>統一發票給獎辦法第11條規定，發票金額為 0，不能兌換。</td></tr>
                              </table>
                            </td>
                          </tr>
                        </tfoot>
                      </table>
                    </body>
                    ";
        }

        /// <summary>
        /// 驗證碼模板 - 驗證碼 Email 的 HTML 內容，可替換為使用模板引擎
        /// </summary>
        private static string BuildCaptchaHtml(CaptchaModel m)
        {
            return $@"
                    <div style='font-family:Microsoft JhengHei,Arial,sans-serif;font-size:15px;line-height:1.6;'>
                        <p>親愛的用戶 {m.AppName} 您好：</p>
                        <p>您正在進行 <b>保必保庇線上宮廟服務平台</b> 的身分驗證。</p>
                        <p>請輸入以下驗證碼以完成驗證：</p>
                        <p style='font-size:24px;font-weight:bold;color:#D63384;letter-spacing:3px;'>{m.Code}</p>
                        <p>驗證碼有效時間為 <b>180秒</b>，請勿提供他人使用。</p>
                        <br/>
                        <p style='color:#888;'>若您未申請此驗證，請忽略此信件。</p>
                        <hr/>
                        <p style='font-size:13px;color:#aaa;'>本郵件由系統自動發送，請勿直接回覆。</p>
                    </div>";
        }

        /// <summary>
        /// 感謝狀模板
        /// </summary>
        private static string BuildThankYouHtml(ThankYouModel m)
        {
            return $@"
                <div style='font-family:Microsoft JhengHei,Arial,sans-serif;font-size:15px;'>
                    <p>親愛的 {m.UserName}：</p>
                    <p>感謝您參與本次活動，以下是感謝狀內容：</p>
                    <p style='font-size:18px;font-weight:bold;color:#444;'>{m.Message}</p>
                    <p>祝您平安順心！</p>
                    <hr/>
                    <p style='font-size:13px;color:#aaa;'>保必保庇線上宮廟平台 敬上</p>
                </div>";
        }
    }
}