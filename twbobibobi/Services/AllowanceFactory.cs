/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceFactory.cs
   類別說明：折讓單處理工具類別，負責將 CreateAllowanceDto 傳送至折讓單 API，並處理回應結果
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using twbobibobi.Model;

namespace twbobibobi.Services
{
    /// <summary>
    /// 折讓單處理工具類別，負責將 CreateAllowanceDto 傳送至折讓單 API，並處理回應結果
    /// </summary>
    /// <remarks>
    /// 這個類別提供了一個靜態方法 `CreateAllowance`，該方法會將 `CreateAllowanceDto` 傳送至折讓單 API，
    /// 並解析回應結果，將其轉換為結構化的 `AllowanceResponseDto` 物件。
    /// </remarks>
    public static class AllowanceFactory
    {
        /// <summary>
        /// 呼叫 AllowanceAPI.aspx 折讓單 API，傳入 DTO 並取得結果
        /// </summary>
        /// <param name="dto">已組裝好的折讓單資料</param>
        /// <returns>AllowanceResponseDto，包含是否成功、折讓單號碼、原始回應 JSON 等</returns>
        /// <remarks>
        /// 這個方法會將傳入的 `CreateAllowanceDto` 資料序列化為 JSON 格式，並透過 HTTP POST 請求將資料傳送至折讓單 API。
        /// API 回應的結果會被解析為 `AllowanceResponseDto`，並返回給呼叫者。
        /// </remarks>
        public static AllowanceResponseDto CreateAllowance(CreateAllowanceDto dto)
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
                    apiUrl = host + appPath + "/Api/AllowanceAPI.aspx";
                }
                else
                {
                    // 正式上線環境
                    //apiUrl = "https://bobibobi.tw/Api/AllowanceAPI.aspx";
                    apiUrl = host + "/Api/AllowanceAPI.aspx";
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

                // 假設回應 JSON 可直接對應 AllowanceResponseDto，反序列化
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

                var resultDto = new AllowanceResponseDto
                {
                    Success = (code == 0),
                    ErrorMessage = msg,
                    RawJson = resultJson
                };

                return resultDto;
            }
            catch (WebException ex)
            {
                // 失敗時包裝錯誤訊息
                return new AllowanceResponseDto
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    RawJson = null
                };
            }
        }
    }
}