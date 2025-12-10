/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：IComboProductRuleRepository.cs
 * 類別說明：組合商品規則 Repository 介面，提供讀取 ComboProductRules 資料的操作定義
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 建立介面
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System.Collections.Generic;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// 定義組合商品規則的資料讀取行為。
    /// </summary>
    public interface IComboProductRuleRepository
    {
        /// <summary>
        /// 根據組合商品的 ProductCode 取得所有展開規則。
        /// </summary>
        /// <param name="CodeId">產品編號表的Id。</param>
        /// <returns>展開規則的清單。</returns>
        List<ComboProductRuleDto> GetRules(int CodeId);
    }
}