using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    /// <summary>
    /// Interface: IOrderRepository
    /// 定義新增購買人、批量寫入祈福明細和新增交易流水的方法
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// 新增購買人資料並回傳 ApplicantID
        /// </summary>
        Task<int> AddApplicantAsync(ApplicantDto applicant, int adminId, int totalAmount);

        /// <summary>
        /// 批量建立祈福燈明細 (使用現有 LightDAC 方法)
        /// </summary>
        Task BulkInsertPrayedPersonsAsync(int applicantId, List<PrayedPersonDto> prayedPersons);

        /// <summary>
        /// 新增交易流水紀錄 (使用現有 DatabaseHelper 方法)
        /// </summary>
        Task AddChargeLogAsync(string orderId, int applicantId, int amount, string description, string comment, string payChannelLog, string ip);
    }
}