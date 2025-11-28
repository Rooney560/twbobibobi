using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.ApiClients
{
    /// <summary>
    /// 根據情境自動設定各種屬性的 InvoiceRequest 工廠
    /// </summary>
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