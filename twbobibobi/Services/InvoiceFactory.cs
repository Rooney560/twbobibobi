using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 發票處理工具類別，負責將 CreateInvoiceDto 傳送至發票 API，並處理回應結果
    /// </summary>
    public static class InvoiceFactory
    {
        /// <summary>
        /// 呼叫 InvoiceAPI.aspx 發票 API，傳入 DTO 並取得結果
        /// </summary>
        /// <param name="dto">已組裝好的發票資料</param>
        /// <returns>InvoiceResponseDto，包含是否成功、發票號碼、原始回應 JSON 等</returns>
        public static InvoiceResponseDto CreateInvoice(CreateInvoiceDto dto)
        {
            try
            {
                // 序列化成 JSON 字串
                var serializer = new JavaScriptSerializer();
                string jsonBody = serializer.Serialize(dto);

                // 設定請求

                // 取得完整主機部分 (含 http/https、網域、Port)
                string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                // 取得目前應用程式的根目錄（例如 "/twbobibobi"）
                string appPath = HttpContext.Current.Request.ApplicationPath.TrimEnd('/');

                string apiUrl;

                // 判斷是否為本地端環境（含 localhost 或 127.0.0.1）
                if (host.Contains("localhost") || host.Contains("127.0.0.1"))
                {
                    // 本地開發環境
                    apiUrl = host + appPath + "/Api/InvoiceAPI.aspx";
                }
                else
                {
                    // 正式上線環境
                    //apiUrl = "https://bobibobi.tw/Api/InvoiceAPI.aspx";
                    apiUrl = host + "/Api/InvoiceAPI.aspx";
                }
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 60000; // 最多等 10 秒

                // 寫入 JSON 資料到 request stream
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonBody);
                    streamWriter.Flush();
                }

                // 讀取回應
                string resultJson;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    resultJson = reader.ReadToEnd();
                }

                // 假設回應 JSON 可直接對應 InvoiceResponseDto，反序列化
                var jsonObj = JObject.Parse(resultJson);

                int code = jsonObj.Value<int>("code");
                string msg = jsonObj.Value<string>("msg");

                // 直接把 data 當成 JObject
                var data = jsonObj["data"] as JObject;
                if (data == null)
                {
                    // (萬一有時候也是陣列) 再做備援
                    var dataArr = jsonObj["data"] as JArray;
                    data = dataArr?.First as JObject;
                }

                var resultDto = new InvoiceResponseDto
                {
                    Success = (code == 0),
                    ErrorMessage = msg,
                    RawJson = resultJson
                };

                if (resultDto.Success)
                {
                    resultDto.InvoiceNumber = data.Value<string>("InvoiceNumber");
                    resultDto.InvoiceTime = data.Value<long>("InvoiceTime");
                    resultDto.RandomNumber = data.Value<string>("RandomNumber");
                    resultDto.Barcode = data.Value<string>("Barcode");
                    resultDto.QrCodeLeft = data.Value<string>("QrCodeLeft");
                    resultDto.QrCodeRight = data.Value<string>("QrCodeRight");
                    resultDto.Base64Data = data.Value<string>("Base64Data")?? "";
                }

                return resultDto;
            }
            catch (WebException ex)
            {
                // 失敗時包裝錯誤訊息
                return new InvoiceResponseDto
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    RawJson = null
                };
            }
        }
    }
}