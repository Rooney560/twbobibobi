using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace twbobibobi.Services
{
    /// <summary>
    /// 手機載具驗證實作：根據不同載具類別，檢查 carrierId 格式是否合法
    /// </summary>
    public class MobileCarrierValidator : IMobileCarrierValidator
    {
        // 支援的固定載具類別 (須與後台一致)
        private static readonly string[] KnownCarrierTypes =
        {
            "3J0002", // 手機條碼
            "CQ0001", // 自然人憑證條碼
            // 其他指定字軌可補充
        };

        // amego 會員載具：a+手機號碼 (10~15 數字) or 電子郵件
        //private static readonly Regex AmegoPhonePattern =
        //    new Regex("^a[0-9]{10,15}$", RegexOptions.Compiled);
        //private static readonly Regex AmegoEmailPattern =
        //    new Regex("^a.+@.+\..+$", RegexOptions.Compiled);

        /// <summary>
        /// 驗證載具類別與載具識別碼
        /// </summary>
        /// <param name="carrierType">載具類別，例如 "3J0002"、"amego"</param>
        /// <param name="carrierId">載具識別碼，例如 "/TRM+O+P" 或 "a0912345678" 或 "auser@example.com"</param>
        /// <exception cref="ArgumentException">格式不合法時拋出</exception>
        public void Validate(string carrierType, string carrierId)
        {
            // 空值檢查
            if (string.IsNullOrWhiteSpace(carrierType) || string.IsNullOrWhiteSpace(carrierId))
                throw new ArgumentException("手機載具情境必須提供 carrierType 與 carrierId");

            // 處理不同類型
            if (KnownCarrierTypes.Contains(carrierType))
            {
                // 固定格式載具
                if (!BasePage.IsTWMobileCode(carrierId))
                    throw new ArgumentException($"載具識別碼格式不符：{carrierId}");
            }
            //else if (carrierType.Equals("amego", StringComparison.OrdinalIgnoreCase))
            //{
            //    // amego 會員載具
            //    if (!(AmegoPhonePattern.IsMatch(carrierId) || AmegoEmailPattern.IsMatch(carrierId)))
            //        throw new ArgumentException($"amego 載具識別碼需為 a+手機號碼 或 a+電子郵件：{carrierId}");
            //}
            else
            {
                // 非預期的載具類別
                throw new ArgumentException($"不支援的載具類別：{carrierType}");
            }
        }
    }
}