using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BCFBaseLibrary.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;
using twbobibobi.Helpers;
using twbobibobi.Model;
using twbobibobi.ApiClients;
using Newtonsoft.Json.Linq;
using System.Web;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading;


namespace twbobibobi
{
    public partial class InvoiceTEST : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 注册异步 Task，ASP.NET 会等待它完成再结束请求
                RegisterAsyncTask(new PageAsyncTask(PostInvoiceAsync));
            }
        }

        private async Task PostFormAsync()
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string orderId = dtNow.ToString("yyyyMMddHHmmssfff");
            string url = "https://invoice-api.amego.tw/json/f0401";
            string invoice = "12345678";
            // string data = "{\"barCode\": \"/TRM+O+P\"}";
            string data = $@"{{
                ""OrderId"": ""{orderId}"",
                ""BuyerIdentifier"": ""28080623"",
                ""BuyerName"": ""光貿科技有限公司"",
                ""NPOBAN"": """",
                ""ProductItem"": [
                    {{
                        ""Description"": ""測試商品1"",
                        ""Quantity"": ""1"",
                        ""UnitPrice"": ""170"",
                        ""Amount"": ""170"",
                        ""Remark"": """",
                        ""TaxType"": ""1""
                    }},
                    {{
                        ""Description"": ""會員折抵"",
                        ""Quantity"": ""1"",
                        ""UnitPrice"": ""-2"",
                        ""Amount"": ""-2"",
                        ""Remark"": """",
                        ""TaxType"": ""1""
                    }}
                ],
                ""SalesAmount"": ""160"",
                ""FreeTaxSalesAmount"": ""0"",
                ""ZeroTaxSalesAmount"": ""0"",
                ""TaxType"": ""1"",
                ""TaxRate"": ""0.05"",
                ""TaxAmount"": ""8"",
                ""TotalAmount"": ""168""
            }}";
            string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            string signBefore = data + time + "sHeq7t8G1wiQvhAuIM27";
            string sign = "";

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(signBefore);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            sign = ToHexString(hashBytes).ToLower();

            Console.WriteLine("invoice: {0}", invoice);
            Console.WriteLine("data: {0}", data);
            Console.WriteLine("time: {0}", time);
            Console.WriteLine("sign: {0}", sign);

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                var form = new Dictionary<string, string>
                {
                    ["invoice"] = invoice,
                    ["data"] = data,
                    ["time"] = time,
                    ["sign"] = sign
                };

                using (var content = new FormUrlEncodedContent(form))
                {
                    // ConfigureAwait(false) 避免回到同步上下文造成死锁
                    HttpResponseMessage response = await client
                        .PostAsync(url, content)
                        .ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();
                    string body = await response.Content
                        .ReadAsStringAsync()
                        .ConfigureAwait(false);
                    string result = Regex.Unescape(body);

                    // 切回主线程安全写到前端
                    Page.Response.Write("<pre>responseBody: "
                        + Server.HtmlEncode(result)
                        + "</pre>");
                }
            }
        }

        /// <summary>
        /// 實際呼叫 InvoiceApiClient 並將結果寫到前端
        /// </summary>
        private async Task PostInvoiceAsync()
        {
            // 1. 取得台北時區的訂單編號
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            string orderId = TimeZoneInfo.ConvertTime(DateTime.Now, tz)
                                         .ToString("yyyyMMddHHmmssfff");

            // 準備商品項目
            var items = new List<ProductItem>
            {
                new ProductItem
                {
                    Description = "線上服務費(大甲鎮瀾宮-光明燈)",
                    Quantity    = 1m,
                    Unit        = "盞",
                    UnitPrice   = 620m,
                    Amount      = 620m,
                    TaxType     = 1
                },
                new ProductItem
                {
                    Description = "線上服務費(大甲鎮瀾宮-安太歲)",
                    Quantity    = 1m,
                    Unit        = "盞",
                    UnitPrice   = 520m,
                    Amount      = 520m,
                    TaxType     = 1
                }
            };

            // 初始化 DTO：依照不同情境呼叫 Factory
            // 1) 一般開立 (需列印)
            var standardRequest = InvoiceRequestFactory.Create(
                InvoiceIssueScenario.StandardPrint,
                orderId,
                items,
                buyerIdentifier: "0000000000",
                buyerName:       "ROONEY"
            );

            // 2) 手機載具
            var mobileRequest = InvoiceRequestFactory.Create(
                InvoiceIssueScenario.MobileCarrier,
                orderId,
                items,
                buyerIdentifier: "0000000000",
                buyerName: "香客",
                buyerAddress:    null,
                buyerTelephone:  null,
                buyerEmail:      null,
                mainRemark:      null,
                carrierType:     "3J0002",
                carrierId:       "/JISELLQ"
            );

            // 3) 捐贈
            var donationRequest = InvoiceRequestFactory.Create(
                InvoiceIssueScenario.Donation,
                orderId,
                items,
                buyerIdentifier: "0000000000",
                buyerName: "香客",
                npoban:          "275"
            );

            // 4) 打統編 (需列印，未稅價)
            var taxIdRequest = InvoiceRequestFactory.Create(
                InvoiceIssueScenario.TaxIdPrint,
                orderId,
                items,
                buyerIdentifier: "28080623",
                buyerName:       "光貿科技有限公司"
            );

            // 建立對應情境請求
            //var request = InvoiceRequestFactory.Create(
            //    InvoiceIssueScenario.StandardPrint,
            //    orderId,
            //    items
            //);


            const string apiUrl_mobile = "https://invoice-api.amego.tw/json/barcode";


            var form = new Dictionary<string, string>
            {
                ["barCode"] = "/JISELLQ",
            };

            // 選擇其中一個請求 DTO 進行呼叫
            var request = standardRequest; // 或 mobileRequest, donationRequest, taxIdRequest

            // 建立 API Client 實例並發送請求
            const string apiUrl = "https://invoice-api.amego.tw/json/f0401";
            const string invoiceNumber = "12345678";
            const string apiSecret = "sHeq7t8G1wiQvhAuIM27";

            using (var client = new InvoiceApiClient(apiUrl, invoiceNumber, apiSecret))
            {
                string rawJson = await client.SendInvoiceAsync(request);

                // 呼叫 API 並解析成 JObject
                //string rawJson = await client.SendInvoiceAsync("12345678", request);

                var jObj = JObject.Parse(rawJson);

                if (jObj["code"]?.Value<int>() == 0)
                {
                    // 只有參數不為 null 或空，才加入回傳
                    if (!string.IsNullOrWhiteSpace(request.CarrierType))
                        jObj["carrierType"] = request.CarrierType;
                    if (!string.IsNullOrWhiteSpace(request.CarrierId1))
                        jObj["carrierId"] = request.CarrierId1;
                    if (!string.IsNullOrWhiteSpace(request.BuyerIdentifier))
                        jObj["buyerIdentifier"] = request.BuyerIdentifier;
                    if (!string.IsNullOrWhiteSpace(request.BuyerName))
                        jObj["buyerName"] = request.BuyerName;
                    if (!string.IsNullOrWhiteSpace(request.NPOBAN))
                        jObj["npoban"] = request.NPOBAN;

                    Response.ContentType = "application/json";
                    Response.Write(jObj.ToString(Formatting.Indented));
                }
                else
                {
                    Response.Write(HttpUtility.HtmlEncode(rawJson));
                }
            }
        }


        // 把之前那個 PostFormAsync 直接搬進來
        private async Task PostFormAsync(string url, string invoice, string data, string time, string sign)
        {
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                    ["invoice"] = invoice,
                    ["data"] = data,
                    ["time"] = time,
                    ["sign"] = sign
                };

                using (var content = new FormUrlEncodedContent(form))
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    // 在 WebForm 你可能想把結果顯示到某個 Literal 或 Label 控制項上
                    string result = Regex.Unescape(responseBody);
                    Response.Write("<pre>responseBody: " + Server.HtmlEncode(result) + "</pre>");
                }
            }
        }

        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}