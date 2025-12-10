/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：ITempleCodeRepository.cs
 * 類別說明：TempleCode 資料存取介面，提供 ProductCode 查詢與 Admin/Service/Type 反查功能
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：
 *   2025-12-09 - 新增 GetCode (反查 ProductCode)
 *               - 新增 ReloadAll 以支援快取更新
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// TempleCode 查詢服務介面。
    /// 支援：
    /// 1. 依 ProductCode 查詢 TempleCode 信息
    /// 2. 依 AdminID + ServiceID + TypeID 反查 ProductCode（組合商品展開時使用）
    /// 3. 快取重新載入
    /// </summary>
    public interface ITempleCodeRepository
    {
        /// <summary>
        /// 以多個 productCode 批量讀取 TempleCode 資訊
        /// </summary>
        /// <param name="productCodes">商品代碼列表</param>
        /// <returns>
        /// 回傳字典：Key = productCode, Value = TempleCodeInfoDto
        /// </returns>
        Task<Dictionary<string, TempleCodeInfoDto>> GetByProductCodesAsync(IEnumerable<string> productCodes);

        /// <summary>
        /// 依 AdminID + ServiceID + TypeID 反查 TempleCode（組合商品展開用）。
        /// </summary>
        /// <param name="adminId">宮廟代碼</param>
        /// <param name="serviceId">服務種類（ex: 1=點燈、3=文創）</param>
        /// <param name="typeId">TypeID（ex: LightsType、PurdueType、SuppliesType）</param>
        /// <returns>TempleCodeInfoDto 或 null</returns>
        TempleCodeInfoDto GetCode(int adminId, int serviceId, string typeId);

        /// <summary>
        /// 重新載入所有 TempleCode 快取。
        /// 用於系統長期運作或資料更新後重新載入。
        /// </summary>
        void LoadAllTempleCodes();
    }
}