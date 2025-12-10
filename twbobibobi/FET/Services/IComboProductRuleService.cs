/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：IComboProductRuleService.cs
 * 類別說明：組合商品規則服務介面
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 建立介面
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System.Collections.Generic;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Services
{
    /// <summary>
    /// 定義組合商品展開規則的商業邏輯服務行為。
    /// </summary>
    public interface IComboProductRuleService
    {
        /// <summary>
        /// 取得組合商品的展開規則。
        /// </summary>
        /// <param name="CodeId">產品編號表的Id。</param>
        /// <returns>規則列表。</returns>
        List<ComboProductRuleDto> GetRules(int CodeId);
    }
}