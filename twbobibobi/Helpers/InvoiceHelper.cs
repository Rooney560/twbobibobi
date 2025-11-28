using System;
using twbobibobi.Data;
using Temple.data;
using twbobibobi.Services;

namespace TempleAdmin.Helper
{
    /// <summary>
    /// 提供發票開立相關的共用方法（單位、欄位名稱、單價計算）
    /// </summary>
    public static class InvoiceHelper
    {
        private static string Kindlist(int kind)
        {
            switch (kind)
            {
                case 1:  // 點燈
                case 2:  // 普渡
                case 4:  // 下元補庫
                case 5:  // 呈疏補庫
                case 6:  // 企業補財庫
                case 7:  // 天赦日補運
                case 8:  // 天赦日祭改
                case 9:  // 關聖帝君聖誕
                case 10: // 代燒金紙
                case 11: // 天貺納福添運法會
                case 12: // 靈寶禮斗
                case 13: // 七朝清醮
                case 14: // 九九重陽天赦日補運
                case 15: // 護國息災梁皇大法會
                case 16: // 補財庫
                case 17: // 赦罪補庫
                case 18: // 天公生招財補運
                case 19: // 供香轉運
                case 20: // 安斗
                case 21: // 供花供果
                case 22: // 孝親祈福燈
                case 23: // 祈安植福
                case 24: // 祈安禮斗
                case 25: // 千手觀音千燈迎佛法會
                default:
                    return "份";
            }
        }

        /// <summary>
        /// 根據 kind 回傳商品單位
        /// </summary>
        /// <param name="kind">服務種類代號</param>
        /// <returns>商品單位（盞 / 份 / 斗...）</returns>
        public static string GetUnitByKind(int kind)
        {
            switch (kind)
            {
                case 1:  // 點燈
                    return "盞";
                case 9:  // 關聖帝君聖誕
                    return "座";
                case 20: // 安斗
                    return "座";
                case 22: // 孝親祈福燈
                    return "盞";
                case 2:  // 普渡
                case 4:  // 下元補庫
                case 5:  // 呈疏補庫
                case 6:  // 企業補財庫
                case 7:  // 天赦日補運
                case 8:  // 天赦日祭改
                case 10: // 代燒金紙
                case 11: // 天貺納福添運法會
                case 12: // 靈寶禮斗
                case 13: // 七朝清醮
                case 14: // 九九重陽天赦日補運
                case 15: // 護國息災梁皇大法會
                case 16: // 補財庫
                case 17: // 赦罪補庫
                case 18: // 天公生招財補運
                case 19: // 供香轉運
                case 21: // 供花供果
                case 23: // 祈安植福
                case 24: // 祈安禮斗
                case 25: // 千手觀音千燈迎佛法會
                default:
                    return "份"; // 多數活動以「份」為單位
            }
        }

        /// <summary>
        /// 根據 kind 取得對應的 ServiceType 與 ServiceString 欄位名稱
        /// </summary>
        /// <param name="kind">服務種類代號</param>
        /// <returns>(TypeColumnName, StringColumnName)</returns>
        /// <exception cref="ArgumentException">若 kind 沒有對應，則寫入 ErrorLog 並丟出例外</exception>
        public static (string TypeCol, string StringCol) GetServiceName(int kind)
        {
            switch (kind)
            {
                case 1:  // 點燈
                    return ("LightsType", "LightsString");
                case 2:  // 普渡
                    return ("PurdueType", "PurdueString");
                case 4:  // 下元補庫
                    return ("SuppliesType", "SuppliesString");
                case 5:  // 呈疏補庫
                    return ("SuppliesType", "SuppliesString");
                case 6:  // 企業補財庫
                    return ("SuppliesType", "SuppliesString");
                case 7:  // 天赦日補運
                    return ("SuppliesType", "SuppliesString");
                case 8:  // 天赦日祭改
                    return ("SuppliesType", "SuppliesString");
                case 9:  // 關聖帝君聖誕
                    return ("EmperorGuanshengType", "EmperorGuanshengString");
                case 10:  // 代燒金紙
                    return ("BPOType", "BPOString");
                case 11:  // 天貺納福添運法會
                    return ("SuppliesType", "SuppliesString");
                case 12: // 靈寶禮斗
                    return ("LingbaolidouType", "LingbaolidouString");
                case 13: // 七朝清醮
                    return ("TaoistJiaoCeremonyType", "TaoistJiaoCeremonyString");
                case 14:  // 九九重陽天赦日補運
                    return ("SuppliesType", "SuppliesString");
                case 15:  // 護國息災梁皇大法會
                    return ("LybcType", "LybcString");
                case 16:  // 補財庫
                    return ("SuppliesType", "SuppliesString");
                case 17:  // 赦罪補庫
                    return ("SuppliesType", "SuppliesString");
                case 18:  // 天公生招財補運
                    return ("SuppliesType", "SuppliesString");
                case 19:  // 供香轉運
                    return ("SuppliesType", "SuppliesString");
                case 20: // 安斗
                    return ("AndouType", "AndouString");
                case 21: // 供花供果
                    return ("HuaguoType", "HuaguoString");
                case 22: // 孝親祈福燈
                    return ("LightsType", "LightsString");
                case 23: // 祈安植福
                    return ("BlessingType", "BlessingString");
                case 24: // 祈安禮斗
                    return ("SuppliesType", "SuppliesString");
                case 25: // 千手觀音千燈迎佛法會
                    return ("QnLightType", "QnLightString");
                default:
                    string msg = $"❌ 未定義的 kind={kind}，請確認對應的 ServiceType / ServiceString 欄位。";
                    SaveErrorLog(msg);
                    throw new ArgumentException(msg);
            }
        }

        /// <summary>
        /// 根據 AdminID、Kind、ServiceType 取得單價
        /// </summary>
        /// <param name="adminId">宮廟編號</param>
        /// <param name="kind">服務種類代號</param>
        /// <param name="serviceType">服務細項代號（例如某種燈種）</param>
        /// <returns>商品單價</returns>
        public static int GetUnitPrice(int adminId, int kind, string serviceType)
        {
            switch (kind)
            {
                case 1:  // 點燈
                    return AjaxBasePage.GetLightsCost(adminId, serviceType);
                case 2:  // 普渡
                    return AjaxBasePage.GetPurdueCost(adminId, serviceType);
                case 4:  // 下元補庫
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 5:  // 呈疏補庫
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 6:  // 企業補財庫
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 7:  // 天赦日補運
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 8:  // 天赦日祭改
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 9:  // 關聖帝君聖誕
                    return AjaxBasePage.GetEmperorGuanshengCost(adminId, serviceType);
                case 10: // 代燒金紙
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 11: // 天貺納福添運法會
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 12: // 靈寶禮斗
                    return AjaxBasePage.GetLingbaolidouCost(adminId, serviceType);
                case 13: // 七朝清醮
                    return AjaxBasePage.GetTaoistJiaoCeremonyCost(adminId, serviceType);
                case 14: // 九九重陽天赦日補運
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 15: // 護國息災梁皇大法會
                    return AjaxBasePage.GetLybcCost(adminId, serviceType);
                case 16: // 補財庫
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 17: // 赦罪補庫
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 18: // 天公生招財補運
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 19: // 供香轉運
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 20: // 安斗
                    return AjaxBasePage.GetLightsCost(adminId, serviceType);
                case 21: // 供花供果
                    return AjaxBasePage.GetHuaguoCost(adminId, serviceType);
                case 22: // 孝親祈福燈
                    return AjaxBasePage.GetLightsCost(adminId, serviceType);
                case 23: // 祈安植福
                    return AjaxBasePage.GetBlessingCost(adminId, serviceType);
                case 24: // 祈安禮斗
                    return AjaxBasePage.GetSuppliesCost(adminId, serviceType);
                case 25: // 千手觀音千燈迎佛法會
                    return AjaxBasePage.GetQnLightCost(adminId, serviceType);
                default:
                    string msg = $"❌ 未定義的單價計算方法 (kind={kind}, adminId={adminId}, serviceType={serviceType})";
                    SaveErrorLog(msg);
                    throw new ArgumentException(msg);
            }
        }

        /// <summary>
        /// 統一錯誤紀錄（呼叫你提供的 SaveErrorLog）
        /// </summary>
        public static void SaveErrorLog(string log)
        {
            BasePage _basePage = new BasePage();
            _basePage.SaveErrorLog(log);
        }
    }
}