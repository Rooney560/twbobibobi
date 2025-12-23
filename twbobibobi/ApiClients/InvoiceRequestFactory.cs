/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：InvoiceRequestFactory.cs
   類別說明：根據情境自動設定各種屬性的 InvoiceRequest 工廠，負責生成發票請求資料。
   建立日期：2025-11-28
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
    /// 根據情境自動設定各種屬性的 InvoiceRequest 工廠
    /// </summary>
    /// <remarks>
    /// 這個靜態類別負責根據不同的發票開立情境（例如：標準列印、手機載具、捐贈、統一編號開立等），
    /// 自動生成對應的 `InvoiceRequest` 實例，並填充各種屬性。這有助於簡化發票請求的創建過程。
    /// </remarks>
    public static class InvoiceRequestFactory
    {
        /// <summary>
        /// 建立對應情境的 InvoiceRequest
        /// </summary>
        /// <param name="scenario">發票開立情境</param>
        /// <param name="orderId">訂單編號</param>
        /// <param name="items">商品項目清單</param>
        /// <param name="buyerIdentifier">買方統一編號，若無填入"0000000000"</param>
        /// <param name="buyerName">買方名稱</param>
        /// <param name="buyerAddress">買方地址</param>
        /// <param name="buyerTelephone">買方電話</param>
        /// <param name="buyerEmail">買方電子信箱</param>
        /// <param name="mainRemark">總備註</param>
        /// <param name="carrierType">載具類別</param>
        /// <param name="carrierId">載具碼</param>
        /// <param name="npoban">捐贈碼</param>
        /// <returns>配置好的 InvoiceRequest</returns>
        /// <remarks>
        /// 根據傳入的發票開立情境，這個方法會自動選擇對應的配置並返回填充好的 `InvoiceRequest` 實例。
        /// 若情境是手機載具或捐贈，需要提供對應的載具類別和載具碼或捐贈碼。
        /// </remarks>
        /// <example>
        /// var request = InvoiceRequestFactory.Create(InvoiceIssueScenario.MobileCarrier, "ORD123456789", items, "0000000000", "消費者", "台北市信義路", "0987654321", "buyer@example.com");
        /// </example>
        public static InvoiceRequest Create(
            InvoiceIssueScenario scenario,
            string orderId,
            List<ProductItem> items,
            // 以下買方資訊／選填都可傳 null 或空字串
            string buyerIdentifier = "0000000000",
            string buyerName = "消費者",
            string buyerAddress = null,
            string buyerTelephone = null,
            string buyerEmail = null,
            string mainRemark = null,
            // 以下僅在部分情境有用
            string carrierType = null,
            string carrierId = null,
            string npoban = null
        )
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException("orderId 不能為空", nameof(orderId));
            if (items == null || items.Count == 0)
                throw new ArgumentException("至少需要一筆商品項目", nameof(items));
            
            // 1. 建立共用部分
            var req = new InvoiceRequest
            {
                OrderId              = orderId,
                BuyerIdentifier      = buyerIdentifier,
                BuyerName            = buyerName,
                BuyerAddress         = buyerAddress,
                BuyerTelephoneNumber = buyerTelephone,
                BuyerEmailAddress    = buyerEmail,
                MainRemark           = mainRemark,
                ProductItem          = items,

                // 自動計算合計欄位
                SalesAmount          = items.Sum(i => i.Amount),
                FreeTaxSalesAmount   = 0m,
                ZeroTaxSalesAmount   = 0m,
                TaxType              = 1,
                TaxRate              = "0.05",
                TaxAmount            = 0m,
                TotalAmount          = items.Sum(i => i.Amount)
            };
            
            // 2. 各情境專屬設定
            switch (scenario)
            {
                case InvoiceIssueScenario.StandardPrint:
                    // 一般開立，需要列印明細
                    req.PrintDetail = 1;
                    break;

                case InvoiceIssueScenario.MobileCarrier:
                    // 手機載具情境：carrierType 與 carrierId 必填
                    if (string.IsNullOrWhiteSpace(carrierType) || string.IsNullOrWhiteSpace(carrierId))
                        throw new ArgumentException("MobileCarrier 情境需要提供 carrierType 與 carrierId");
                    req.CarrierType = carrierType;
                    req.CarrierId1  = carrierId;
                    req.CarrierId2  = carrierId;
                    break;

                case InvoiceIssueScenario.Donation:
                    // 捐贈情境：NPOBAN 必填
                    if (string.IsNullOrWhiteSpace(npoban))
                        throw new ArgumentException("Donation 情境需要提供 NPOBAN");
                    req.NPOBAN = npoban;
                    break;

                case InvoiceIssueScenario.TaxIdPrint:
                    // 打統編，需要計算稅額並使用未稅價
                    //req.TaxAmount   = Math.Round(req.SalesAmount * 0.05m, 2);
                    //req.TotalAmount = req.SalesAmount + req.TaxAmount;
                    //req.DetailVat   = 0;

                    req.SalesAmount = Math.Round(req.TotalAmount / 1.05m, 0); // 未稅
                    req.TaxAmount = Math.Round(req.TotalAmount - req.SalesAmount, 0); // 稅額
                    req.DetailVat = 1;
                    req.PrintDetail = 1;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
            }

            return req;
        }
    }
}