using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Temple.data;
using twbobibobi.Model;

namespace twbobibobi.Repository
{
    /// <summary>
    /// 透過 LightDAC 實作查詢多年度、多服務類別資料的資料存取層。
    /// </summary>
    public class LogQueryRepository : ILogQueryRepository
    {
        private readonly BasePage _basePage;

        /// <summary>
        /// 初始化資料存取層。
        /// </summary>
        /// <param name="basePage">BasePage 物件，用於共用資料庫連線與輔助方法。</param>
        public LogQueryRepository(BasePage basePage)
        {
            _basePage = basePage ?? throw new ArgumentNullException(nameof(basePage));
        }

        /// <summary>
        /// 取得可查詢的宮廟編號清單。
        /// 可根據實際業務調整（例如從資料庫動態取得）。
        /// </summary>
        /// <returns>宮廟 AdminID 清單。</returns>
        public List<int> GetAdminList()
        {
            // 目前先寫死，若未來要改成 DB 取值，可改成從 TempleInfo 取出。
            return new List<int> { 14, 18, 19, 21, 22, 23 };
        }

        /// <summary>
        /// 查詢申請人資料。
        /// 依據服務類型選擇不同的 LightDAC 方法。
        /// </summary>
        /// <param name="year">年度，例如 "2025"</param>
        /// <param name="adminId">宮廟編號</param>
        /// <param name="kind">服務類型
        /// 1-點燈 
        /// 2-普度 
        /// 4-下元補庫 
        /// 5-呈疏補庫(天官武財神聖誕補財庫) 
        /// 6-企業補財庫 
        /// 7-天赦日補運 
        /// 8-天赦日祭改 
        /// 9-關聖帝君聖誕 
        /// 10-代燒金紙 
        /// 11-天貺納福添運法會 
        /// 12-靈寶禮斗 
        /// 13-七朝清醮 
        /// 14-九九重陽天赦日補運 
        /// 15-護國息災梁皇大法會 
        /// 16-補財庫 
        /// 17-赦罪補庫 
        /// 18-天公生招財補運 
        /// 19-供香轉運 
        /// 20-安斗 
        /// 21-供花供果 
        /// 22-孝親祈福燈 
        /// 23-祈安植福
        /// 24-祈安禮斗
        /// 25-千手觀音千燈迎佛法會
        /// </param>
        /// <param name="name">姓名</param>
        /// <param name="mobile">電話</param>
        /// <returns>DataTable，包含查詢結果</returns>
        public DataTable GetApplicantInfo(string year, int adminId, ServiceKind kind, string name, string mobile)
        {
            var lightDAC = new LightDAC(_basePage);
            DataTable result = new DataTable();

            try
            {
                switch (kind)
                {
                    //case ServiceKind.Light:
                    //    result = lightDAC.Getapplicantinfo_Lights(year, adminId, name, mobile);
                    //    break;

                    //case ServiceKind.Blessing:
                    //    result = lightDAC.Getapplicantinfo_blessing(year, adminId, name, mobile);
                    //    break;

                    //case ServiceKind.Purdue:
                    //    result = lightDAC.Getapplicantinfo_purdue(year, adminId, name, mobile);
                    //    break;

                    //case ServiceKind.Supplies:
                    //    result = lightDAC.Getapplicantinfo_supplies(year, adminId, name, mobile);
                    //    break;
                }
            }
            catch (Exception ex)
            {
                // 錯誤保護：避免查詢中斷整體流程
                string log = $"【LogQueryRepository.GetApplicantInfo】錯誤：{ex.Message} (Year:{year}, Admin:{adminId}, Kind:{kind})";
                _basePage.SaveErrorLog(log);
            }

            return result;
        }
    }
}
