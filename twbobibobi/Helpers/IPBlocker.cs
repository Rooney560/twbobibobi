using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供 IP 黑名單判斷與封鎖邏輯，使用實體檔案作為黑名單來源，並快取於記憶體中，避免頻繁 I/O。
    /// </summary>
    public static class IPBlocker
    {
        private static HashSet<string> _blockedIpCache = new HashSet<string>();
        private static DateTime _lastLoadedTime = DateTime.MinValue;
        private static readonly object _lock = new object();
        private static readonly string _logPath = HttpContext.Current.Server.MapPath("~/Log/BlockedIP/BlockedIP.txt");

        /// <summary>
        /// 檢查指定 IP 是否在黑名單中（每 60 秒自動重載）
        /// </summary>
        /// <param name="ip">目標 IP 位址</param>
        /// <returns>若為黑名單 IP 回傳 true</returns>
        public static bool IsIpBlocked(string ip)
        {
            lock (_lock)
            {
                if ((DateTime.Now - _lastLoadedTime).TotalSeconds > 60)
                {
                    ReloadBlockedIpFile();
                }
                return _blockedIpCache.Contains(ip);
            }
        }

        /// <summary>
        /// 將指定 IP 加入黑名單（寫入檔案並同步至記憶體）
        /// </summary>
        /// <param name="ip">要封鎖的 IP</param>
        public static void BlockIp(string ip)
        {
            lock (_lock)
            {
                if (!_blockedIpCache.Contains(ip))
                {
                    _blockedIpCache.Add(ip);
                    Directory.CreateDirectory(Path.GetDirectoryName(_logPath));
                    File.AppendAllLines(_logPath, new[] { ip });
                }
            }
        }

        /// <summary>
        /// 重新讀取黑名單檔案，並快取至記憶體中
        /// </summary>
        private static void ReloadBlockedIpFile()
        {
            _blockedIpCache.Clear();

            if (File.Exists(_logPath))
            {
                var lines = File.ReadAllLines(_logPath);
                foreach (var line in lines)
                {
                    var ip = line.Trim();
                    if (!string.IsNullOrEmpty(ip))
                    {
                        _blockedIpCache.Add(ip);
                    }
                }
            }

            _lastLoadedTime = DateTime.Now;
        }
    }
}
