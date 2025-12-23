/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceServiceFactory.cs
   類別說明：負責根據環境參數建立 IAllowanceService 實例的工廠類別。
            可依照環境（如 "_Prod" 或 "_UAT"）動態讀取不同的 AppSettings 設定。
   建立日期：2025-11-28
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System.Configuration;
using twbobibobi.ApiClients;

namespace twbobibobi.Services
{
    /// <summary>
    /// 負責根據環境參數建立 IAllowanceService 實例的工廠類別。
    /// 可依照環境（如 "_Prod" 或 "_UAT"）動態讀取不同的 AppSettings 設定。
    /// </summary>
    /// <remarks>
    /// 這個工廠類別提供了一些靜態方法來根據環境設定（如 "_Prod" 或 "_UAT"）來建立不同的折讓單服務（如建立折讓單、查詢折讓單狀態、作廢折讓單等）。
    /// 根據不同的環境，會自動從 `web.config` 中讀取對應的 AppSettings 來配置折讓單服務。
    /// </remarks>
    public class AllowanceServiceFactory
    {
        /// <summary>
        /// 建立折讓單服務的實例。
        /// 預設使用正式環境（_Prod），可傳入 "_UAT" 切換至測試環境。
        /// </summary>
        /// <param name="environment">環境字串，例："_Prod"（正式）、"_UAT"（測試）</param>
        /// <returns>IAllowanceService 實作的實例</returns>
        /// <example>AllowanceServiceFactory.Create("_Prod")</example>
        public static IInvoiceService Create(string environment = "_Prod")
        {
            // 從 web.config 的 appSettings 取得對應環境的參數名稱
            string configInvoiceName = "INVOICE_Name" + environment;
            string configAppKey = "INVOICE_AppKey" + environment;

            // 從設定中讀取統一編號與密鑰
            string invoiceNumber = ConfigurationManager.AppSettings[configInvoiceName];
            string secretKey = ConfigurationManager.AppSettings[configAppKey];

            // 建立並回傳折讓單服務實例
            return new AllowanceService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw/json/g0401",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 建立折讓單作廢服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>IInvoiceCancelService 的實作物件</returns>
        /// <example>AllowanceServiceFactory.CreateCancelService("_Prod")</example>
        public static IInvoiceCancelService CreateCancelService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new AllowanceCancelService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 建立折讓單狀態服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>IInvoiceStatusService 的實作物件</returns>
        /// <example>AllowanceServiceFactory.CreateStatusService("_UAT")</example>
        public static IInvoiceStatusService CreateStatusService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new AllowanceStatusService(
                new InvoiceApiClient(
                    apiUrl: "https://invoice-api.amego.tw",
                    invoiceNumber: invoiceNumber,
                    secretKey: secretKey));
        }

        /// <summary>
        /// 建立折讓單狀態查詢服務的實例。
        /// </summary>
        /// <param name="environment">環境名稱，例：「_UAT」或「_Prod」</param>
        /// <returns>AllowanceQueryService 的實作物件</returns>
        /// <example>AllowanceServiceFactory.CreateQueryService("_Prod")</example>
        public static AllowanceQueryService CreateQueryService(string environment = "_Prod")
        {
            string invoiceNumber = GetConfig("INVOICE_Name", environment);
            string secretKey = GetConfig("INVOICE_AppKey", environment);

            return new AllowanceQueryService(
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
        /// <example>AllowanceServiceFactory.GetConfig("INVOICE_Name", "_UAT")</example>
        private static string GetConfig(string keyPrefix, string env)
        {
            return ConfigurationManager.AppSettings[keyPrefix + env];
        }
    }
}