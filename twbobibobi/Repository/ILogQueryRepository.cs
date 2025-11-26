using System.Collections.Generic;
using System.Data;
using twbobibobi.Model;

namespace twbobibobi.Repository
{
    /// <summary>
    /// 定義查詢多年度、多服務類型的資料存取介面。
    /// </summary>
    public interface ILogQueryRepository
    {
        /// <summary>
        /// 取得全部可查詢的宮廟編號清單。
        /// </summary>
        /// <returns>整數集合，代表各宮廟 AdminID。</returns>
        List<int> GetAdminList();

        /// <summary>
        /// 查詢指定年度、宮廟、服務類型與姓名電話條件的資料。
        /// </summary>
        /// <param name="year">查詢年度，例如 2025 或 2024。</param>
        /// <param name="adminId">宮廟編號。</param>
        /// <param name="type">服務類型列舉值。</param>
        /// <param name="name">申請人姓名，可為空字串。</param>
        /// <param name="mobile">申請人電話，可為空字串。</param>
        /// <returns>符合條件的 DataTable。</returns>
        DataTable GetApplicantInfo(string year, int adminId, ServiceKind kind, string name, string mobile);
    }
}
