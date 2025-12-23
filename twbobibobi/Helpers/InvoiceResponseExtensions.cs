/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceResponseExtensions.cs
   類別說明：提供將 JObject（API 回傳原始資料）轉換為發票回應 DTO 的擴充方法。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using twbobibobi.Model;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供將 JObject（API 回傳原始資料）轉換為發票回應 DTO 的擴充方法。
    /// </summary>
    /// <remarks>
    /// 這個靜態類別提供了一些擴充方法，用於將發票 API 回傳的原始資料（JObject）轉換為結構化的 DTO。
    /// 這些 DTO 用於在應用程式內部傳遞處理過的發票資料，並包括如發票建立、查詢、作廢等各種回應結果。
    /// </remarks>
    public static class InvoiceResponseExtensions
    {
        /// <summary>
        /// 將 API 回傳的 JObject 轉換為結構化的 InvoiceResponseDto 物件。
        /// </summary>
        /// <param name="json">原始發票 API 回傳結果（JObject）</param>
        /// <returns>轉換後的 InvoiceResponseDto</returns>
        /// <remarks>
        /// 這個方法會將 API 回傳的 JObject 資料解析並轉換為結構化的 `InvoiceResponseDto` 物件，
        /// 其中包含了發票號碼、開立時間、條碼、QR Code 等資訊。
        /// </remarks>
        public static InvoiceResponseDto ToInvoiceResponseDto(this JObject json)
        {
            return new InvoiceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<int?>("code").ToString(),
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
        /// <remarks>
        /// 這個方法會將發票查詢結果中的 JObject 轉換為結構化的 `InvoiceQueryResponseDto`，
        /// 並包含發票的詳細狀態（如發票號碼、類型、狀態、發票日期等）。
        /// </remarks>
        public static InvoiceQueryResponseDto ToInvoiceQueryResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data
            var dataArray = json["data"] as JArray;

            // 建立一個 List 來儲存轉換後的發票內容
            var dataList = new List<InvoiceQueryDataResponseDto>();
            // 建立一個 List 來儲存轉換後的商品陣列
            var productitemList = new List<InvoiceProductItem>();
            // 建立一個 List 來儲存轉換後的未處理的排程陣列
            var waitList = new List<InvoiceWait>();

            // 逐筆處理每一筆發票內容
            foreach (var data in dataArray)
            {
                // 嘗試抓出 product_item
                var productitemArray = json["product_item"] as JArray;
                foreach (var item in productitemArray)
                {
                    productitemList.Add(new InvoiceProductItem
                    {
                        Tax_type = data.Value<string>("tax_type") ?? data.Value<int?>("tax_type")?.ToString(),
                        Description = data.Value<string>("description"),
                        Unit_price = data.Value<string>("unit_price") ?? data.Value<int?>("unit_price")?.ToString(),
                        Quantity = data.Value<string>("quantity") ?? data.Value<int?>("quantity")?.ToString(),
                        Unit = data.Value<string>("unit"),
                        Amount = data.Value<string>("amount") ?? data.Value<int?>("amount")?.ToString(),
                        Remark = data.Value<string>("remark")
                    });
                }

                // 嘗試抓出 wait
                var waitArray = json["wait"] as JArray;
                foreach (var wait in waitArray)
                {
                    waitList.Add(new InvoiceWait
                    {
                        Invoice_type = data.Value<string>("invoice_type"),
                        Create_date = data.Value<string>("create_date") ?? data.Value<int?>("create_date")?.ToString()
                    });
                }

                dataList.Add(new InvoiceQueryDataResponseDto
                {
                    Invoice_number = data.Value<string>("invoice_number"),
                    Invoice_type = data.Value<string>("invoice_type"),
                    Invoice_status = data.Value<string>("invoice_status") ?? data.Value<int?>("invoice_status")?.ToString(),
                    Invoice_date = data.Value<string>("invoice_date"),
                    Invoice_time = data.Value<string>("invoice_time"),
                    Buyer_identifier = data.Value<string>("buyer_identifier"),
                    Buyer_name = data.Value<string>("buyer_name"),
                    Buyer_zip = data.Value<string>("buyer_zip") ?? data.Value<int?>("buyer_zip")?.ToString(),
                    Buyer_address = data.Value<string>("buyer_address"),
                    Buyer_telephone_number = data.Value<string>("buyer_telephone_number"),
                    Buyer_email_address = data.Value<string>("buyer_email_address"),
                    Sales_amount = data.Value<string>("sales_amount") ?? data.Value<int?>("sales_amount")?.ToString(),
                    Free_tax_sales_amount = data.Value<string>("free_tax_sales_amount") ?? data.Value<int?>("free_tax_sales_amount")?.ToString(),
                    Zero_tax_sales_amount = data.Value<string>("zero_tax_sales_amount") ?? data.Value<int?>("zero_tax_sales_amount")?.ToString(),
                    Tax_type = data.Value<string>("tax_type") ?? data.Value<int?>("tax_type")?.ToString(),
                    Tax_rate = data.Value<string>("tax_rate"),
                    Tax_amount = data.Value<string>("tax_amount") ?? data.Value<int?>("tax_amount")?.ToString(),
                    Total_amount = data.Value<string>("total_amount") ?? data.Value<int?>("total_amount")?.ToString(),
                    Print_mark = data.Value<string>("print_mark"),
                    Random_number = data.Value<string>("random_number"),
                    Main_remark = data.Value<string>("main_remark"),
                    Customs_clearance_mark = data.Value<string>("customs_clearance_mark") ?? data.Value<int?>("customs_clearance_mark")?.ToString(),
                    Carrier_type = data.Value<string>("carrier_type"),
                    Carrier_id1 = data.Value<string>("carrier_id1"),
                    Carrier_id2 = data.Value<string>("carrier_id2"),
                    Npoban = data.Value<string>("npoban"),
                    Cancel_date = data.Value<string>("cancel_date") ?? data.Value<int?>("cancel_date")?.ToString(),
                    Invoice_lottery = data.Value<string>("invoice_lottery") ?? data.Value<int?>("invoice_lottery")?.ToString(),
                    Order_id = data.Value<string>("order_id"),
                    Detail_vat = data.Value<string>("detail_vat") ?? data.Value<int?>("detail_vat")?.ToString(),
                    Detail_amount_round = data.Value<string>("detail_amount_round") ?? data.Value<int?>("detail_amount_round")?.ToString(),
                    Create_date = data.Value<string>("create_date") ?? data.Value<int?>("create_date")?.ToString(),
                    Product_item = productitemList,
                    Wait = waitList
                });
            }

            return new InvoiceQueryResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                Data = dataList,
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將發票狀態結果的 JObject 轉換為 InvoiceStatusResponseDto。
        /// </summary>
        /// <param name="json">原始查詢結果的 JObject</param>
        /// <returns>轉換後的發票狀態回應 DTO</returns>
        /// <remarks>
        /// 這個方法會將發票狀態查詢結果中的 JObject 轉換為結構化的 `InvoiceStatusResponseDto`，
        /// 並包含發票的詳細狀態（如發票號碼、類型、狀態、發票日期等）。
        /// </remarks>
        public static InvoiceStatusResponseDto ToInvoiceStatusResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data
            var dataArray = json["data"] as JArray;

            // 建立一個 List 來儲存轉換後的發票資料
            var dataList = new List<InvoiceStatusDataResponseDto>();

            // 逐筆處理每一筆發票資料
            foreach (var data in dataArray)
            {
                dataList.Add(new InvoiceStatusDataResponseDto
                {
                    InvoiceNumber = data.Value<string>("invoice_number"),
                    Type = data.Value<string>("type"),
                    Status = data.Value<string>("status") ?? data.Value<int?>("status")?.ToString(),
                    Total_amount = data.Value<string>("total_amount") ?? data.Value<int?>("total_amount")?.ToString()
                });
            }

            // 返回轉換後的 InvoiceStatusResponseDto
            return new InvoiceStatusResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<int?>("code").ToString(),
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                Data = dataList,  // 將資料陣列傳入 DTO
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將發票作廢結果的 JObject 轉換為 CancelInvoiceResponseDto。
        /// </summary>
        /// <param name="json">原始作廢結果的 JObject</param>
        /// <param name="invocienumber">作廢的發票號碼</param>
        /// <returns>轉換後的發票作廢回應 DTO</returns>
        /// <remarks>
        /// 這個方法會將發票作廢結果中的 JObject 轉換為結構化的 `CancelInvoiceResponseDto`，
        /// 並包含作廢發票的號碼、作廢時間等資訊。
        /// </remarks>
        public static CancelInvoiceResponseDto ToCancelInvoiceResponseDto(this JObject json, string invocienumber)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            return new CancelInvoiceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<int?>("code").ToString(),
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                CancelInvoiceNumber = invocienumber,
                CancelTime = dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RawJson = json.ToString()
            };
        }

    }
}