/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 封建用 ComboProductRuleService.cs
 * 類別說明：提供組合商品規則的商業邏輯存取，封裝 Repository 作業
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 建立初版
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System.Collections.Generic;
using twbobibobi.Data;
using twbobibobi.FET.Data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Services
{
    /// <summary>
    /// 提供組合商品展開規則的商業邏輯層。
    /// 主要封裝 Repository，讓 Service 能被 OrderService 使用。
    /// </summary>
    public class ComboProductRuleService : IComboProductRuleService
    {
        private readonly IComboProductRuleRepository _repo;

        /// <summary>
        /// 建構子。
        /// </summary>
        /// <param name="repo">組合商品規則 Repository。</param>
        public ComboProductRuleService(IComboProductRuleRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public List<ComboProductRuleDto> GetRules(int CodeId)
        {
            return _repo.GetRules(CodeId);
        }
    }
}