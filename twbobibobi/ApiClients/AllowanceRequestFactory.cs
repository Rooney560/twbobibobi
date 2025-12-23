/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceRequestFactory.cs
   類別說明：根據情境自動設定各種屬性的 AllowanceRequest 工廠，負責生成折讓單請求資料。
   建立日期：2025-12-16
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using twbobibobi.Model;

namespace twbobibobi.ApiClients
{
    /// <summary>
    /// 根據情境自動設定各種屬性的 AllowanceRequest 工廠
    /// </summary>
    /// <remarks>
    /// 這個靜態類別負責根據不同的折讓單開立情境，
    /// 自動生成對應的 `AllowanceRequest` 實例，並填充各種屬性。這有助於簡化折讓單請求的創建過程。
    /// </remarks>
    public static class AllowanceRequestFactory
    {
        /// <summary>
        /// 建立對應情境的 AllowanceRequest
        /// </summary>
        /// <param name="allowanceNumber">折讓單編號</param>
        /// <param name="date">折讓單日期，Ymd</param>
        /// <param name="items">商品項目清單</param>
        /// <param name="taxAmount">營業稅額</param>
        /// <param name="totalAmount">金額合計(不含稅)</param>
        /// <param name="buyerIdentifier">買方統一編號，若無填入"0000000000"</param>
        /// <param name="buyerName">買方名稱</param>
        /// <param name="buyerAddress">買方地址</param>
        /// <param name="buyerTelephone">買方電話</param>
        /// <param name="buyerEmail">買方電子信箱</param>
        /// <returns>配置好的 AllowanceRequest</returns>
        /// <remarks>
        /// 根據傳入的折讓單開立情境，這個方法會自動選擇對應的配置並返回填充好的 `AllowanceRequest` 實例。
        /// 若情境是手機載具或捐贈，需要提供對應的載具類別和載具碼或捐贈碼。
        /// </remarks>
        public static AllowanceRequest Create(
            string allowanceNumber,
            string date,
            List<ProductItem_Allowance> items,
            Decimal taxAmount,
            Decimal totalAmount,
            // 以下買方資訊／選填都可傳 null 或空字串
            string buyerIdentifier = "0000000000",
            string buyerName = "消費者",
            string buyerAddress = null,
            string buyerTelephone = null,
            string buyerEmail = null
        )
        {
            if (string.IsNullOrWhiteSpace(allowanceNumber))
                throw new ArgumentException("orderId 不能為空", nameof(allowanceNumber));
            if (items == null || items.Count == 0)
                throw new ArgumentException("至少需要一筆商品項目", nameof(items));

            // 1. 建立共用部分
            var req = new AllowanceRequest
            {
                AllowanceNumber = allowanceNumber,
                AllowanceDate = date,
                AllowanceType = 2,
                BuyerIdentifier = buyerIdentifier,
                BuyerName = buyerName,
                BuyerAddress = buyerAddress,
                BuyerTelephoneNumber = buyerTelephone,
                BuyerEmailAddress = buyerEmail,
                ProductItem = items,

                // 自動計算合計欄位
                TaxAmount = taxAmount,
                TotalAmount = totalAmount
            };

            return req;
        }
    }
}