using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Services
{
    /// <summary>
    /// 手機載具驗證介面
    /// </summary>
    public interface IMobileCarrierValidator
    {
        /// <summary>
        /// 驗證提供的載具類別與載具識別碼格式
        /// </summary>
        void Validate(string carrierType, string carrierId);
    }
}