/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：ComboProductRuleRepository.cs
 * 類別說明：組合商品規則的資料庫存取實作
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 建立初版
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System.Collections.Generic;
using System.Data;
using System.Linq;
using twbobibobi.Data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// 從 ComboProductRules 資料表取得組合商品展開規則。
    /// </summary>
    public class ComboProductRuleRepository : IComboProductRuleRepository
    {
        private readonly BasePage _basePage;

        /// <summary>
        /// 建構子。
        /// </summary>
        /// <param name="basePage">Web BasePage 實例。</param>
        public ComboProductRuleRepository(BasePage basePage)
        {
            _basePage = basePage;
        }

        /// <inheritdoc />
        public List<ComboProductRuleDto> GetRules(int CodeID)
        {
            string sql = @"
                SELECT *
                FROM ComboProductRules
                WHERE CodeID = @CodeID
                ORDER BY SortOrder ASC";

            using (var da = new DatabaseAdapter(sql, _basePage.DBSource))
            {
                da.AddParameterToSelectCommand("@CodeID", CodeID);

                var dt = new DataTable();
                da.Fill(dt);

                return dt.AsEnumerable()
                    .Select(r => new ComboProductRuleDto
                    {
                        RuleID = r.Field<int>("RuleID"),
                        ComboProductCode = r.Field<string>("ComboProductCode"),
                        AdminID = r.Field<int>("AdminID"),
                        ServiceID = r.Field<int>("ServiceID"),
                        TypeID = r.Field<int>("TypeID").ToString(),
                        PriceSource = r.Field<string>("PriceSource"),
                        FixedPrice = r.Field<int?>("FixedPrice"),
                        SortOrder = r.Field<int>("SortOrder"),
                        Remark = r.Field<string>("Remark")
                    })
                    .ToList();
            }
        }
    }
}