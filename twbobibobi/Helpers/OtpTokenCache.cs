/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：OtpTokenCache.cs
 *  類別說明：用於暫存 OTP 驗證通過後的 Token，支援記憶體與可擴充外部儲存（Redis、DB），
 *            並提供 Token 刪除機制以避免重放攻擊，並於每次儲存新 Token 前自動清除過期資料。
 *
 *  建立日期：2025-12-02
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-12-02　Rooney　建立初版；新增 Remove 方法；預留多環境擴充結構；新增自動清理過期 Token 機制
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-12-02
 **************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Configuration;
using twbobibobi.Data;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供 OTP 驗證通過後 Token 的暫存、驗證與刪除功能。
    /// 預設使用伺服端記憶體（ConcurrentDictionary）儲存，可根據環境改為外部快取。
    /// 每次 Save 時會自動清除過期 Token。
    /// </summary>
    public class OtpTokenCache
    {
        #region === 內部欄位 ===

        /// <summary>
        /// 本機記憶體快取：用於開發與單機部署環境。
        /// </summary>
        private static readonly ConcurrentDictionary<string, DateTime> _memoryCache =
            new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// 取得目前執行環境設定（Dev 或 Prod）。
        /// </summary>
        private static readonly string EnvironmentMode =
            ConfigurationManager.AppSettings["EnvironmentMode"] ?? "Dev"; // Dev / Prod

        #endregion

        #region === Public Methods ===

        /// <summary>
        /// 儲存一組 Token 與其到期時間。
        /// 儲存前會自動清理所有已過期的 Token。
        /// </summary>
        /// <param name="token">伺服端產生的 Token。</param>
        /// <param name="expire">Token 到期時間。</param>
        public static void Save(string token, DateTime expire)
        {
            if (string.IsNullOrWhiteSpace(token)) return;

            if (IsProd())
            {
                // 預留：正式環境可改為 Redis 或 DB 儲存
                // RedisCache.Set(token, expire, TimeSpan.FromMinutes(15));
            }
            else
            {
                // 儲存前清理過期項目
                CleanExpired();

                _memoryCache[token] = expire;
            }
        }

        /// <summary>
        /// 驗證 Token 是否存在且未過期。
        /// 驗證通過後不會自動移除（需呼叫 Remove）。
        /// </summary>
        /// <param name="token">待驗證的 Token。</param>
        /// <returns>true 表示有效；false 表示無效或過期。</returns>
        public static bool Validate(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            if (IsProd())
            {
                // 預留：正式環境 Redis 讀取邏輯
                // return RedisCache.Exists(token);
                return false;
            }
            else
            {
                if (_memoryCache.TryGetValue(token, out DateTime expire))
                {
                    if (LightDAC.GetTaipeiNow() <= expire)
                    {
                        return true;
                    }
                    else
                    {
                        _memoryCache.TryRemove(token, out _);
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 移除指定 Token（通常於付款完成後呼叫，以防止重複使用）。
        /// </summary>
        /// <param name="token">要刪除的 Token。</param>
        /// <returns>true 表示成功移除；false 表示不存在。</returns>
        public static bool Remove(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            if (IsProd())
            {
                // 預留：正式環境可改為 Redis / DB 刪除邏輯
                // return RedisCache.Delete(token);
                return false;
            }
            else
            {
                return _memoryCache.TryRemove(token, out _);
            }
        }

        /// <summary>
        /// 清除所有 Token 快取（僅限開發環境除錯用）。
        /// </summary>
        public static void ClearAll()
        {
            if (!IsProd())
                _memoryCache.Clear();
        }

        /// <summary>
        /// 清除所有已過期的 Token。
        /// 會在每次 Save() 時自動呼叫。
        /// </summary>
        private static void CleanExpired()
        {
            foreach (var kv in _memoryCache)
            {
                if (LightDAC.GetTaipeiNow() > kv.Value)
                {
                    _memoryCache.TryRemove(kv.Key, out _);
                }
            }
        }

        #endregion

        #region === Private Helper ===

        /// <summary>
        /// 判斷目前是否為正式環境。
        /// </summary>
        private static bool IsProd()
        {
            return EnvironmentMode.Equals("Prod", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}