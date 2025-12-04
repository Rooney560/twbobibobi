/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：OtpTokenHelper.cs
 *  類別說明：根據動態參數與內部金鑰產生可重建式 OTP Token，用於跨階段驗證比對
 *
 *  建立日期：2025-12-02
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-12-02　Rooney　建立初版；支援動態參數組合生成 Token，移除資料庫依賴
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-12-02
 **************************************************************************/

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供 OTP 驗證 Token 的生成與比對功能。
    /// Token 可依據傳入參數自動組合產生，並使用內部金鑰加鹽，確保安全性與可重建性。
    /// </summary>
    public class OtpTokenHelper
    {
        /// <summary>
        /// 系統內部金鑰，用於強化雜湊不可逆性（請勿外洩或寫入設定檔）。
        /// </summary>
        private const string SecretKey = "BobiBobi@OTP-SecretKey-2025";

        /// <summary>
        /// 產生可重建的 OTP Token。
        /// 可傳入任意數量參數，系統會自動串接為一組雜湊。
        /// </summary>
        /// <param name="parameters">用於生成 Token 的參數集合（例如 aid、kind、a、code 等）。</param>
        /// <returns>以 SHA256 雜湊後的 Token 字串（小寫十六進位）。</returns>
        public static string GenerateToken(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                throw new ArgumentException("必須至少傳入一個參數以生成 Token。", nameof(parameters));

            // 將參數轉成統一格式字串
            string concatParams = string.Join("-", parameters.Select(p => p?.ToString()?.Trim() ?? string.Empty));

            // 加入金鑰與時間戳的鹽值（固定字串，不含時間影響）
            string raw = $"{concatParams}-{SecretKey}";

            // SHA256 雜湊
            using (var sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 驗證兩個 Token 是否相同。
        /// </summary>
        /// <param name="tokenA">Token A。</param>
        /// <param name="tokenB">Token B。</param>
        /// <returns>true 表示完全相同（不分大小寫）。</returns>
        public static bool ValidateToken(string tokenA, string tokenB)
        {
            if (string.IsNullOrEmpty(tokenA) || string.IsNullOrEmpty(tokenB))
                return false;

            return string.Equals(tokenA, tokenB, StringComparison.OrdinalIgnoreCase);
        }
    }
}