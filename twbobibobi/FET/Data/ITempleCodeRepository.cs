// File: ITempleCodeRepository.cs
// Namespace: twbobibobi.FET.Data
// 放置於項目中的 Data 資料夾，用於存取 TempleCode 資訊

using System.Collections.Generic;
using System.Threading.Tasks;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// Interface: ITempleCodeRepository
    /// 定義批量查詢 TempleCodeInfo 的方法
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
    }
}