using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Services
{
    /// <summary>
    /// 封裝手機載具驗證邏輯的共用服務
    /// </summary>
    public class CarrierChecker
    {
        private readonly IMobileCarrierValidator _validator;

        /// <summary>
        /// 建構子：注入手機載具驗證器
        /// </summary>
        /// <param name="validator">手機載具驗證器實例</param>
        public CarrierChecker(IMobileCarrierValidator validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// 驗證手機載具格式是否合法
        /// </summary>
        /// <param name="carrierType">載具類別，例如 "3J0002"</param>
        /// <param name="carrierId">載具識別碼，例如 "/TRM+O+P"</param>
        /// <exception cref="ArgumentException">當載具類別或識別碼為空或格式錯誤時拋出例外</exception>
        public void Validate(string carrierType, string carrierId)
        {
            _validator.Validate(carrierType, carrierId);
        }
    }
}
