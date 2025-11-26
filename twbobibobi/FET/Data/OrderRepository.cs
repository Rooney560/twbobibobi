// File: OrderRepository.cs
// Namespace: twbobibobi.FET.Data
// 此檔案實作 IOrderRepository，並改用現有 LightDAC 與 DatabaseHelper 方法

using Read.data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Temple.data;
using twbobibobi.Data;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LightDAC _lightDAC;
        private readonly string _year;

        /// <summary>
        /// 注入現有的輔助類別與年度
        /// </summary>
        public OrderRepository(LightDAC lightDAC, string year)
        {
            _lightDAC = lightDAC;
            _year = year;
        }

        /// <summary>
        /// 使用 LightDAC.addapplicantinfo_lights_da 建立購買人
        /// </summary>
        public Task<int> AddApplicantAsync(ApplicantDto applicant, int adminId, int totalAmount)
        {
            // 處理 Receipt 欄位預設
            string receiptName = string.IsNullOrEmpty(applicant.reName) ? applicant.appName : applicant.reName;
            string receiptMobile = string.IsNullOrEmpty(applicant.reMobile) ? applicant.appMobile : applicant.reMobile;

            // 名稱、電話、金額等參數需依實際 DTO 對應
            int applicantId = _lightDAC.Addapplicantinfo_lights_da(
                Name: applicant.appName,
                Mobile: applicant.appMobile,
                Cost: totalAmount.ToString(),
                County: applicant.appCity,
                Dist: applicant.appRegion,
                Addr: applicant.appAddr,
                ZipCode: applicant.appzipCode,
                Sendback: applicant.sendback,
                ReceiptName: receiptName,
                ReceiptMobile: receiptMobile,
                applicant.appEmail,
                Status: 0,
                AdminID: adminId.ToString(),
                PostURL: "FETAPI",
                Year: _year
            );
            return Task.FromResult(applicantId);
        }

        /// <summary>
        /// 遍歷列表，使用 LightDAC.addLights_da 建立每筆祈福燈明細
        /// </summary>
        public Task BulkInsertPrayedPersonsAsync(int applicantId, List<PrayedPersonDto> prayedPersons)
        {
            foreach (var p in prayedPersons)
            {
                // 確保 count 為非 null
                int count = p.OfferingQty ?? 1;

                _lightDAC.AddLights_da(
                    ApplicantID: applicantId,
                    Name: p.Name,
                    Mobile: p.Mobile,  
                    Cost: p.Cost,
                    Sex: p.Gender,
                    LightsType: p.ServiceType,
                    LightsString: p.ServiceString,
                    Oversea: p.Oversea,
                    Birth: p.LunarBirthday,
                    LeapMonth: p.LeapMonth,
                    BirthTime: p.BirthTime,
                    BirthMonth: p.LunarBirthday.Substring(3, 2),
                    Age: string.Empty,
                    Zodiac: string.Empty,
                    sBirth: p.SolarBirthday,
                    Email: string.Empty,
                    Count: count,
                    Remark: p.Remark,
                    Addr: p.Addr,
                    County: p.City,
                    Dist: p.Region,
                    ZipCode: p.ZipCode,
                    Year: _year
                );
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 使用 LightDAC.AddChargeLog_Lights_da 建立交易流水
        /// </summary>
        public Task AddChargeLogAsync(string orderId, int applicantId, int amount, string description, string comment, string payChannelLog, string ip)
        {
            // status 與 ChargeType 固定或由外部傳入
            long logId = _lightDAC.AddChargeLog_Lights_da(
                OrderID: orderId,
                ApplicantID: applicantId,
                Amount: amount,
                ChargeType: "FETAPI",
                Status: 0,
                Description: description,
                Comment: comment,
                PayChannelLog: payChannelLog,
                IP: ip,
                Year: _year
            );
            return Task.CompletedTask;
        }
    }
}
