using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 負責根據環境參數建立 IInvoiceService 實例的工廠類別。
    /// 可依照環境（如 "_Prod" 或 "_UAT"）動態讀取不同的 AppSettings 設定。
    /// </summary>
    public static class InvoiceServiceFactory
    {
        /// <summary>
        /// 建立發票服務的實例。
        /// 預設使用正式環境（_Prod），可傳入 "_UAT" 切換至測試環境。
        /// </summary>
        /// <param name="environment">環境字串，例："_Prod"（正式）、"_UAT"（測試）</param>
        /// <returns>IInvoiceService 實作的實例</returns>
        public static IInvoiceService Create(string environment = "_Prod")
        {
            // 從 web.config 的 appSettings 取得對應環境的參數名稱
            string configInvoiceName = "INVOICE_Name" + environment;
            string configAppKey = "INVOICE_AppKey" + environment;

            // 從設定中讀取統一編號與密鑰
            string invoiceNumber = ConfigurationManager.AppSettings[configInvoiceName];
            string secretKey = ConfigurationManager.AppSettings[configAppKey];

            // 建立並回傳發票服務實例
            return new InvoiceService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw/json/f0401",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey),
                new MobileCarrierValidator());
        }

        /// <summary>
        /// 建立發票作廢服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>IInvoiceCancelService 的實作物件</returns>
        public static IInvoiceCancelService CreateCancelService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new InvoiceCancelService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 建立發票狀態查詢服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>IInvoiceStatusService 的實作物件</returns>
        public static IInvoiceStatusService CreateStatusService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new InvoiceStatusService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 建立發票狀態查詢服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>IInvoiceStatusService 的實作物件</returns>
        public static InvoiceQueryService CreateQueryService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new InvoiceQueryService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 從 web.config 取得指定環境的 appSettings 值。
        /// </summary>
        /// <param name="keyPrefix">設定鍵的前綴，例如 INVOICE_Name</param>
        /// <param name="env">環境代碼，例如 _UAT 或 _Prod</param>
        /// <returns>對應的設定值</returns>
        private static string GetConfig(string keyPrefix, string env)
        {
            return ConfigurationManager.AppSettings[keyPrefix + env];
        }
    }
}