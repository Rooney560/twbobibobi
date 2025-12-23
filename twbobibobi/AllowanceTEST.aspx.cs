using BCFBaseLibrary.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using twbobibobi.ApiClients;
using twbobibobi.Helpers;
using twbobibobi.Model;


namespace twbobibobi
{
    public partial class AllowanceTEST : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 注册异步 Task，ASP.NET 会等待它完成再结束请求
                RegisterAsyncTask(new PageAsyncTask(PostAllowanceAsync));
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
        /// 實際呼叫 AllowanceApiClient 並將結果寫到前端
        /// </summary>
        private async Task PostAllowanceAsync()
        {
            // 1. 取得台北時區的訂單編號
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);
            string orderId = TimeZoneInfo.ConvertTime(DateTime.Now, tz)
                                         .ToString("yyyyMMddHHmmssfff");

            //Decimal Total = 620 + 520;
            //Decimal SalesAmount = Math.Round(Total / 1.05m, 0); // 未稅
            //Decimal TaxAmount = Math.Round(Total - SalesAmount, 0); // 稅額
            Decimal Total = 1140;
            Decimal SalesAmount = Math.Round(Total / 1.05m, 0); // 未稅
            Decimal TaxAmount = Math.Round(Total - SalesAmount, 0); // 稅額

            Total = 520;
            TaxAmount = TaxAmount - 30;
            SalesAmount = Total - TaxAmount;

            // 準備商品項目
            var items = new List<ProductItem_Allowance>
            {
                //new ProductItem_Allowance
                //{
                //    OriginalInvoiceNumber = "EX10008041",
                //    OriginalInvoiceDate    = 20251216,
                //    OriginalDescription    = "線上服務費(大甲鎮瀾宮-光明燈)",
                //    Quantity    = 1m,
                //    UnitPrice   = 590.47m,
                //    Amount      = 590,
                //    Tax = 30,
                //    TaxType     = 1
                //},
                new ProductItem_Allowance
                {
                    OriginalInvoiceNumber = "EX10008041",
                    OriginalInvoiceDate    = 20251216,
                    OriginalDescription    = "線上服務費(大甲鎮瀾宮-安太歲)",
                    Quantity    = 1m,
                    UnitPrice   = SalesAmount,
                    Amount      = SalesAmount,
                    Tax = TaxAmount,
                    TaxType     = 1
                }
            };
            //var items = new List<ProductItem_Allowance>
            //{
            //    new ProductItem_Allowance
            //    {
            //        OriginalInvoiceNumber = "EX10008041",
            //        OriginalInvoiceDate    = 20251216,
            //        OriginalDescription    = "折讓線上服務費(大甲鎮瀾宮-光明燈、安太歲)",
            //        Quantity    = 1m,
            //        UnitPrice   = SalesAmount,
            //        Amount      = SalesAmount,
            //        Tax = TaxAmount,
            //        TaxType     = 1
            //    },
            //};


            // 初始化 DTO：依照不同情境呼叫 Factory
            // 1) 一般開立 (需列印)
            var standardRequest = AllowanceRequestFactory.Create(
                orderId,
                dtNow.ToString("yyyyMMdd"),
                items,
                TaxAmount,
                SalesAmount,
                buyerIdentifier: "0000000000",
                buyerName: "ROONEY"
            );

            // 2) 手機載具
            //var mobileRequest = AllowanceRequestFactory.Create(
            //    AllowanceIssueScenario.MobileCarrier,
            //    orderId,
            //    items,
            //    buyerIdentifier: "0000000000",
            //    buyerName: "香客",
            //    buyerAddress: null,
            //    buyerTelephone: null,
            //    buyerEmail: null,
            //    mainRemark: null,
            //    carrierType: "3J0002",
            //    carrierId: "/JISELLQ"
            //);

            // 3) 捐贈
            //var donationRequest = AllowanceRequestFactory.Create(
            //    AllowanceIssueScenario.Donation,
            //    orderId,
            //    items,
            //    buyerIdentifier: "0000000000",
            //    buyerName: "香客",
            //    npoban: "275"
            //);

            // 4) 打統編 (需列印，未稅價)
            //var taxIdRequest = AllowanceRequestFactory.Create(
            //    AllowanceIssueScenario.TaxIdPrint,
            //    orderId,
            //    items,
            //    buyerIdentifier: "28080623",
            //    buyerName: "光貿科技有限公司"
            //);

            //const string apiUrl_mobile = "https://invoice-api.amego.tw/json/barcode";


            var form = new Dictionary<string, string>
            {
                ["barCode"] = "/JISELLQ",
            };

            // 選擇其中一個請求 DTO 進行呼叫
            var request = standardRequest; // 或 mobileRequest, donationRequest, taxIdRequest

            // 建立 API Client 實例並發送請求
            const string apiUrl = "https://invoice-api.amego.tw/json/g0401";
            const string invoiceNumber = "12345678";
            const string apiSecret = "sHeq7t8G1wiQvhAuIM27";

            using (var client = new InvoiceApiClient(apiUrl, invoiceNumber, apiSecret))
            {
                string rawJson = await client.SendAllowanceAsync(request);

                // 呼叫 API 並解析成 JObject
                //string rawJson = await client.SendAllowanceAsync("12345678", request);

                var jObj = JObject.Parse(rawJson);

                if (jObj["code"]?.Value<int>() == 0)
                {
                    // 只有參數不為 null 或空，才加入回傳
                    if (!string.IsNullOrWhiteSpace(request.BuyerIdentifier))
                        jObj["buyerIdentifier"] = request.BuyerIdentifier;
                    if (!string.IsNullOrWhiteSpace(request.BuyerName))
                        jObj["buyerName"] = request.BuyerName;

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