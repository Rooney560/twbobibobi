using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供付款成功頁面的一次性 Token 管理功能
    /// </summary>
    public class TokenHelper
    {
        /// <summary>
        /// 暫存所有付款成功的 Token 與到期時間
        /// </summary>
        private static readonly ConcurrentDictionary<string, DateTime> TokenCache = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// 建立新的一次性 Token
        /// </summary>
        /// <param name="orderNo">訂單編號</param>
        /// <param name="minutes">Token 有效分鐘數（預設 10 分鐘）</param>
        /// <returns>新建立的 Token 字串</returns>
        public static string CreateToken(string orderNo, int minutes = 10)
        {
            string token = Guid.NewGuid().ToString("N");
            DateTime expireTime = DateTime.Now.AddMinutes(minutes);
            TokenCache[token] = expireTime;
            return token;
        }

        /// <summary>
        /// 驗證 Token 是否有效
        /// </summary>
        /// <param name="token">要驗證的 Token</param>
        /// <returns>若 Token 有效則回傳 true；否則 false</returns>
        public static bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            if (TokenCache.TryGetValue(token, out DateTime expireTime))
            {
                if (DateTime.Now <= expireTime)
                    return true;
                else
                    TokenCache.TryRemove(token, out _); // 過期移除
            }
            return false;
        }

        /// <summary>
        /// 使 Token 立即失效（例如頁面顯示完後清除）
        /// </summary>
        /// <param name="token">要刪除的 Token</param>
        public static void InvalidateToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
                TokenCache.TryRemove(token, out _);
        }
    }
}