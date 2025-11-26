using System.Collections.Generic;
using System.Threading.Tasks;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// File: ITempleOrderProcessor.cs
    /// Namespace: twbobibobi.FET.Processors
    /// Interface: ITempleOrderProcessor
    /// 定義單一宮廟的訂單處理策略，負責該宮廟的實際資料組裝與資料庫寫入流程。
    /// 每個實作類別 (例如 DajiaOrderProcessor、TywtgOrderProcessor 等)
    /// 均對應特定的宮廟與業務邏輯。
    /// </summary>
    public interface ITempleOrderProcessor
    {
        /// <summary>
        /// 處理指定宮廟的訂單，並回傳該宮廟所有產生的 partnerOrderNumbers。
        /// 此方法應包含：
        /// 1. 購買人資料與被祈福者資料組裝；
        /// 2. 寫入主表與明細表；
        /// 3. 回傳對應的宮廟訂單編號清單。
        /// </summary>
        /// <param name="applicant">購買人資料（例如姓名、電話、信箱等）。</param>
        /// <param name="prayedPersons">被祈福者名單集合，每筆代表一位祈福對象。</param>
        /// <param name="tid">Temple 系統內部的臨時交易識別碼 (clientOrderNumber)。</param>
        /// <param name="fetOrderNumber">FET 系統訂單編號。</param>
        /// <param name="kind">服務種類代碼 (例如 1=點燈、2=普度...等)。</param>
        /// <param name="totalAmount">訂單總金額（字串型別以保持格式一致）。</param>
        /// <param name="itemsInfo">訂單項目資訊 JSON 字串，用於明細記錄。</param>
        /// <param name="OrderId">Temple 系統內部生成的訂單識別碼 (yyyyMMddHHmmssfff 格式)。</param>
        /// <returns>
        /// 回傳 Task，內容為該宮廟產生的所有子訂單編號清單。
        /// 若處理失敗應回傳空清單或擲出例外。
        /// </returns>
        Task<List<string>> ProcessAsync(
            ApplicantDto applicant, 
            List<PrayedPersonDto> prayedPersons, 
            string tid, 
            string fetOrderNumber, 
            string kind, 
            string totalAmount, 
            string itemsInfo,
            string OrderId);
    }
}