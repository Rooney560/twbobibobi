using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.Model;
using twbobibobi.Repository;

namespace twbobibobi.Services
{
    /// <summary>
    /// 提供查詢申請人紀錄的商業邏輯層
    /// </summary>
    public class SearchLogService
    {
        private readonly ILogQueryRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public SearchLogService(ILogQueryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 查詢指定姓名/電話於所有年度與服務的資料
        /// </summary>
        public List<ApplicantResult> SearchApplicant(string name, string mobile, string[] years)
        {
            var results = new List<ApplicantResult>();
            Parallel.ForEach(years, year =>
            {
                var adminList = _repository.GetAdminList();
                Parallel.ForEach(adminList, adminId =>
                {
                    foreach (ServiceKind kind in Enum.GetValues(typeof(ServiceKind)))
                    {
                        var dt = _repository.GetApplicantInfo(year, adminId, kind, name, mobile);
                        lock (results)
                        {
                            results.AddRange(dt.ToApplicantResults());
                        }
                    }
                });
            });
            return results;
        }
    }
}