/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceResponseExtensions.cs
   類別說明：提供將 JObject（API 回傳原始資料）轉換為折讓單回應 DTO 的擴充方法。
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using twbobibobi.Model;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供將 JObject（API 回傳原始資料）轉換為折讓單回應 DTO 的擴充方法。
    /// </summary>
    /// <remarks>
    /// 這個靜態類別提供了一些擴充方法，用於將折讓單 API 回傳的原始資料（JObject）轉換為結構化的 DTO。
    /// 這些 DTO 用於在應用程式內部傳遞處理過的折讓單資料，並包括如折讓單建立、查詢、作廢等各種回應結果。
    /// </remarks>
    public static class AllowanceResponseExtensions
    {
        /// <summary>
        /// 將 API 回傳的 JObject 轉換為結構化的 AllowanceResponseDto 物件。
        /// </summary>
        /// <param name="json">原始折讓單 API 回傳結果（JObject）</param>
        /// <returns>轉換後的 AllowanceResponseDto</returns>
        /// <remarks>
        /// 這個方法會將 API 回傳的 JObject 資料解析並轉換為結構化的 `AllowanceResponseDto` 物件，
        /// 其中包含了折讓單編號、開立時間、條碼、QR Code 等資訊。
        /// </remarks>
        public static AllowanceResponseDto ToAllowanceResponseDto(this JObject json)
        {
            return new AllowanceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<int?>("code").ToString(),
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將折讓單查詢結果的 JObject 轉換為 AllowanceQueryResponseDto。
        /// </summary>
        /// <param name="json">原始查詢結果的 JObject</param>
        /// <returns>轉換後的折讓單狀態回應 DTO</returns>
        /// <remarks>
        /// 這個方法會將折讓單查詢結果中的 JObject 轉換為結構化的 `AllowanceQueryResponseDto`，
        /// 並包含折讓單的詳細狀態（如折讓單編號、類型、狀態、折讓單日期等）。
        /// </remarks>
        public static AllowanceQueryResponseDto ToAllowanceQueryResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data
            JObject data = json["data"] as JObject;

            if (data != null)
            {
                // 建立一個 List 來儲存轉換後的發票內容
                var dataList = new List<AllowanceQueryDataResponseDto>();
                // 建立一個 List 來儲存轉換後的商品陣列
                var productitemList = new List<AllowanceProductItem>();
                // 建立一個 List 來儲存轉換後的未處理的排程陣列
                var waitList = new List<AllowanceWait>();

                // 逐筆處理每一筆發票內容
                // 嘗試抓出 product_item
                var productitemArray = data["product_item"] as JArray;
                foreach (var item in productitemArray)
                {
                    productitemList.Add(new AllowanceProductItem
                    {
                        Original_invoice_number = data.Value<string>("original_invoice_number"),
                        Original_invoice_date = data.Value<string>("original_invoice_date") ?? data.Value<int?>("original_invoice_date")?.ToString(),
                        Tax_type = data.Value<string>("tax_type") ?? data.Value<int?>("tax_type")?.ToString(),
                        Description = data.Value<string>("description"),
                        Unit_price = data.Value<string>("unit_price") ?? data.Value<int?>("unit_price")?.ToString(),
                        Quantity = data.Value<string>("quantity") ?? data.Value<int?>("quantity")?.ToString(),
                        Unit = data.Value<string>("unit"),
                        Amount = data.Value<string>("amount") ?? data.Value<int?>("amount")?.ToString(),
                        Tax = data.Value<string>("tax") ?? data.Value<int?>("tax")?.ToString()
                    });
                }

                // 嘗試抓出 wait
                var waitArray = data["wait"] as JArray;
                foreach (var wait in waitArray)
                {
                    waitList.Add(new AllowanceWait
                    {
                        Invoice_type = data.Value<string>("invoice_type"),
                        Create_date = data.Value<string>("create_date") ?? data.Value<int?>("create_date")?.ToString()
                    });
                }

                dataList.Add(new AllowanceQueryDataResponseDto
                {
                    Allowance_number = data.Value<string>("allowance_number"),
                    Invoice_type = data.Value<string>("invoice_type"),
                    Invoice_status = data.Value<string>("invoice_status") ?? data.Value<int?>("invoice_status")?.ToString(),
                    Allowance_date = data.Value<string>("allowance_date") ?? data.Value<int?>("allowance_date")?.ToString(),
                    Allowance_type = data.Value<string>("allowance_type") ?? data.Value<int?>("allowance_type")?.ToString(),
                    Buyer_identifier = data.Value<string>("buyer_identifier"),
                    Buyer_name = data.Value<string>("buyer_name"),
                    Buyer_zip = data.Value<string>("buyer_zip") ?? data.Value<int?>("buyer_zip")?.ToString(),
                    Buyer_address = data.Value<string>("buyer_address"),
                    Buyer_telephone_number = data.Value<string>("buyer_telephone_number"),
                    Buyer_email_address = data.Value<string>("buyer_email_address"),
                    Tax_amount = data.Value<string>("tax_amount") ?? data.Value<int?>("tax_amount")?.ToString(),
                    Total_amount = data.Value<string>("total_amount") ?? data.Value<int?>("total_amount")?.ToString(),
                    Cancel_date = data.Value<string>("cancel_date") ?? data.Value<int?>("cancel_date")?.ToString(),
                    Detail_vat = data.Value<string>("detail_vat") ?? data.Value<int?>("detail_vat")?.ToString(),
                    Create_date = data.Value<string>("create_date") ?? data.Value<int?>("create_date")?.ToString(),
                    Product_item = productitemList,
                    Wait = waitList
                });

                return new AllowanceQueryResponseDto
                {
                    Success = json.Value<int?>("code") == 0,
                    ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                    Data = dataList,
                    RawJson = data.ToString()
                };
            }
            else
            {
                return new AllowanceQueryResponseDto
                {
                    Success = false,
                    ErrorMessage = "取不到折讓單資訊。",
                    Data = null,
                    RawJson = json.ToString()
                };
            }
        }

        /// <summary>
        /// 將折讓單狀態結果的 JObject 轉換為 AllowanceStatusResponseDto。
        /// </summary>
        /// <param name="json">原始查詢結果的 JObject</param>
        /// <returns>轉換後的折讓單狀態回應 DTO</returns>
        /// <remarks>
        /// 這個方法會將折讓單狀態查詢結果中的 JObject 轉換為結構化的 `AllowanceStatusResponseDto`，
        /// 並包含折讓單的詳細狀態（如折讓單編號、類型、狀態、折讓單日期等）。
        /// </remarks>
        public static AllowanceStatusResponseDto ToAllowanceStatusResponseDto(this JObject json)
        {
            // 取得台北時區時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            // 嘗試抓出 data
            var dataArray = json["data"] as JArray;

            // 建立一個 List 來儲存轉換後的折讓單資料
            var dataList = new List<AllowanceStatusDataResponseDto>();

            // 逐筆處理每一筆折讓單資料
            foreach (var data in dataArray)
            {
                dataList.Add(new AllowanceStatusDataResponseDto
                {
                    AllowanceNumber = data.Value<string>("allowance_number"),
                    Type = data.Value<string>("type"),
                    Status = data.Value<string>("status") ?? data.Value<int?>("status")?.ToString(),
                    Tax_amount = data.Value<string>("tax_amount") ?? data.Value<int?>("tax_amount")?.ToString(),
                    Total_amount = data.Value<string>("total_amount") ?? data.Value<int?>("total_amount")?.ToString()
                });
            }

            return new AllowanceStatusResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<int?>("code").ToString(),
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                Data = dataList,
                RawJson = json.ToString()
            };
        }

        /// <summary>
        /// 將折讓單作廢結果的 JObject 轉換為 CancelAllowanceResponseDto。
        /// </summary>
        /// <param name="json">原始作廢結果的 JObject</param>
        /// <param name="allowancenumber">作廢的折讓單編號</param>
        /// <returns>轉換後的折讓單作廢回應 DTO</returns>
        /// <remarks>
        /// 這個方法會將折讓單作廢結果中的 JObject 轉換為結構化的 `CancelAllowanceResponseDto`，
        /// 並包含作廢折讓單的編號、作廢時間等資訊。
        /// </remarks>
        public static CancelAllowanceResponseDto ToCancelAllowanceResponseDto(this JObject json, string allowancenumber)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            return new CancelAllowanceResponseDto
            {
                Success = json.Value<int?>("code") == 0,
                Code = json.Value<string>("code").ToString(),
                ErrorMessage = json.Value<string>("msg") ?? json.Value<string>("ErrorMessage"),
                CancelAllowanceNumber = allowancenumber,
                CancelTime = dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RawJson = json.ToString()
            };
        }

    }
}