// File: TempleCodeRepository.cs
// Namespace: twbobibobi.FET.Data
// 放置於項目中的 Data 資料夾

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Temple.data;
using twbobibobi.Data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// Class: TempleCodeRepository
    /// 實作 ITempleCodeRepository，透過 LightDAC 批量取得 TempleCode 資訊
    /// </summary>
    public class TempleCodeRepository : ITempleCodeRepository
    {
        private readonly LightDAC _lightDAC;

        /// <summary>
        /// 注入 LightDAC
        /// </summary>
        public TempleCodeRepository(LightDAC lightDAC)
        {
            _lightDAC = lightDAC;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
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
    }
}
