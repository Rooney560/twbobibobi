/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：TempleCodeRepository.cs
 * 類別說明：TempleCode 查詢服務，提供 ProductCode 查詢與 AdminID/ServiceID/TypeID 反查功能
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：
 *   2025-12-09 - 1. 加入 GetCode 反查方法（組合商品展開用）
 *               2. 加入 LoadAllTempleCodes 以快取所有 TempleCode
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using twbobibobi.Data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// 提供 TempleCode 資訊查詢，包括：
    /// 1. 依 ProductCode 查 TempleCodeInfo（舊流程）
    /// 2. 依 AdminID + ServiceID + TypeID 反查 ProductCode（供組合商品展開使用）
    /// </summary>
    public class TempleCodeRepository : ITempleCodeRepository
    {
        private readonly LightDAC _lightDAC;

        /// <summary>
        /// 所有 TempleCode 快取，用於反查 AdminID + ServiceID + TypeID → ProductCode。
        /// Key 不重要，Value 才是 TempleCodeInfo。
        /// </summary>
        private List<TempleCodeInfoDto> _allCodes;

        /// <summary>
        /// 注入 LightDAC
        /// </summary>
        public TempleCodeRepository(LightDAC lightDAC)
        {
            _lightDAC = lightDAC;
            LoadAllTempleCodes();
        }

        /// <summary>
        /// 依多個 ProductCode 批次查詢 TempleCodeInfo。
        /// </summary>
        /// <param name="productCodes">商品代碼清單。</param>
        /// <returns>ProductCode → TempleCodeInfoDto 的字典。</returns>
        public Task<Dictionary<string, TempleCodeInfoDto>> GetByProductCodesAsync(IEnumerable<string> productCodes)
        {
            var result = new Dictionary<string, TempleCodeInfoDto>(StringComparer.OrdinalIgnoreCase);
            foreach (var code in productCodes.Distinct())
            {
                // 逐一呼叫舊有方法
                var dt = _lightDAC.GetTempleCodeInfo(code);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var info = new TempleCodeInfoDto
                    {
                        CodeID = Convert.ToInt32(row["CodeID"]),
                        ProductCode = code,
                        AdminID = Convert.ToInt32(row["AdminID"]),
                        ServiceID = Convert.ToInt32(row["ServiceID"]),
                        TypeID = row["TypeID"].ToString(),
                        TypeString = row["TypeString"].ToString(),
                        ItemTypeID = row["ItemTypeID"].ToString()
                    };
                    result[code] = info;
                }
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 載入所有 TempleCode 資料至記憶體，用於組合商品展開時的反查。
        /// </summary>
        public void LoadAllTempleCodes()
        {
            _allCodes = new List<TempleCodeInfoDto>();

            // 取得所有的TempleCode資料
            DataTable dt = _lightDAC.GetTempleCodeInfo();

            foreach (DataRow row in dt.Rows)
            {
                _allCodes.Add(new TempleCodeInfoDto
                {
                    CodeID = Convert.ToInt32(row["CodeID"]),
                    ProductCode = row["ProductCode"].ToString(),
                    AdminID = Convert.ToInt32(row["AdminID"]),
                    ServiceID = Convert.ToInt32(row["ServiceID"]),
                    TypeID = row["TypeID"].ToString(),
                    TypeString = row["TypeString"].ToString(),
                    ItemTypeID = row["ItemTypeID"].ToString()
                });
            }
        }

        /// <summary>
        /// 反查 TempleCode（組合商品展開用）
        /// </summary>
        /// <param name="adminId">宮廟代碼</param>
        /// <param name="serviceId">服務代碼，例如 1 = 點燈、3 = 文創</param>
        /// <param name="typeId">類型代碼，例如LigtsType、PurdueType、SuppliesType</param>
        /// <returns>TempleCodeInfoDto 或 null</returns>
        public TempleCodeInfoDto GetCode(int adminId, int serviceId, string typeId)
        {
            return _allCodes
                .FirstOrDefault(x =>
                    x.AdminID == adminId &&
                    x.ServiceID == serviceId &&
                    x.TypeID.Equals(typeId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
