using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供將 JObject（API 回傳原始資料）轉換為發票回應 DTO 的擴充方法。
    /// </summary>
    public static class InvoiceResponseExtensions
    {
        /// <summary>
        /// 將 API 回傳的 JObject 轉換為結構化的 InvoiceResponseDto 物件。
        /// </summary>
        /// <param name="json">原始發票 API 回傳結果（JObject）</param>
        /// <returns>轉換後的 InvoiceResponseDto</returns>
        public static InvoiceResponseDto ToInvoiceResponseDto(this JObject json)
        {
            return new InvoiceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                InvoiceNumber = json.Value<string>("invoice_number"),
                InvoiceTime = json.Value<long?>("invoice_time") ?? 0,
                RandomNumber = json.Value<string>("random_number"),
                Barcode = json.Value<string>("barcode"),
                QrCodeLeft = json.Value<string>("qrcode_left"),
                QrCodeRight = json.Value<string>("qrcode_right"),
                Base64Data = json.Value<string>("base64_data"),
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將發票查詢結果的 JObject 轉換為 InvoiceQueryResponseDto。
        /// </summary>
        /// <param name="json">原始查詢結果的 JObject</param>
        /// <returns>轉換後的發票狀態回應 DTO</returns>
        public static InvoiceQueryResponseDto ToInvoiceQueryResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data[0]
            var dataArray = json["data"] as JArray;
            var data = dataArray?.FirstOrDefault() as JObject;

            return new InvoiceQueryResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                InvoiceNumber = data?.Value<string>("invoice_number"),
                Type = data?.Value<string>("type"),
                Status = data?.Value<string>("status") ?? data?.Value<int?>("status")?.ToString(),
                InvoiceDate = data?.Value<string>("invoiceDate") ?? dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將發票查詢結果的 JObject 轉換為 InvoiceStatusResponseDto。
        /// </summary>
        /// <param name="json">原始查詢結果的 JObject</param>
        /// <returns>轉換後的發票狀態回應 DTO</returns>
        public static InvoiceStatusResponseDto ToInvoiceStatusResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data[0]
            var dataArray = json["data"] as JArray;
            var data = dataArray?.FirstOrDefault() as JObject;

            return new InvoiceStatusResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                InvoiceNumber = data?.Value<string>("invoice_number"),
                Type = data?.Value<string>("type"),
                Status = data?.Value<string>("status") ?? data?.Value<int?>("status")?.ToString(),
                InvoiceDate = data?.Value<string>("invoiceDate") ?? dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將發票作廢結果的 JObject 轉換為 CancelInvoiceResponseDto。
        /// </summary>
        /// <param name="json">原始作廢結果的 JObject</param>
        /// <param name="invocienumber">作廢的發票號碼</param>
        /// <returns>轉換後的發票作廢回應 DTO</returns>
        public static CancelInvoiceResponseDto ToCancelInvoiceResponseDto(this JObject json, string invocienumber)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            return new CancelInvoiceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                CancelInvoiceNumber = invocienumber,
                CancelTime = dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RawJson = json.ToString()
            };
        }

    }
}