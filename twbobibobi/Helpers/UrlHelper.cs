using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// URL 解析與格式化的協助工具。
    /// 規則：
    /// 1) 僅接受 purl（且值非純數字）或白名單旗標鍵（如 line、fb 等）值為 "1" 的參數作為後綴來源。
    /// 2) 依查詢字串內「參數出現順序」決定優先權：誰先符合規則就使用誰（purl 亦遵守此順序）。
    /// 3) 其它參數（例如 fbclid/gclid/utm_*）一律忽略，不再使用「第一個參數」fallback。
    /// 4) 後綴以大寫附加到 <c>urlString</c> 後，並自動補單一下劃線。
    /// 5) 旗標白名單可由 Web.config 的 appSettings["PurlFlagKeys"] 覆寫。
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// 取得來源網址的查詢參數，依規則將服務項目名稱 <paramref name="urlString"/> 加上後綴。
        /// 只會因應 purl（非純數字）或旗標白名單鍵 = "1" 產生後綴，並依參數出現順序取第一個符合者。
        /// </summary>
        /// <param name="url">來源網址（可為絕對或相對，須包含查詢字串）。</param>
        /// <param name="urlString">服務項目名稱（例如：Purdue_da_Index）。</param>
        /// <returns>結合後字串，例如：Purdue_da_Index_LINE；若無符合規則則回傳原值。</returns>
        public static string GetRequestURL(string url, string urlString)
        {
            if (string.IsNullOrWhiteSpace(urlString))
                return urlString ?? string.Empty;

            try
            {
                string query = ExtractQuery(url);
                if (string.IsNullOrEmpty(query))
                    return urlString;

                // 旗標白名單（用 HashSet 做 O(1) 判斷；大小寫不敏感）
                var flagKeys = GetFlagKeysFromConfig();

                // 逐參數、依原始出現順序判斷
                foreach (var kv in EnumerateQueryPreserveOrder(query))
                {
                    string key = kv.Key;      // 已 UrlDecode、原大小寫保留
                    string lkey = key.ToLowerInvariant();
                    string val = kv.Value ?? string.Empty; // 已 UrlDecode

                    // 規則 1：purl 且值非純數字 → 用 purl 值
                    if (lkey == "purl" && !IsAllDigits(val))
                    {
                        return AppendSuffix(urlString, val.Trim().ToUpperInvariant());
                    }

                    // 規則 2：白名單旗標 = "1" → 用鍵名
                    if (flagKeys.Contains(lkey) && string.Equals(val, "1", StringComparison.OrdinalIgnoreCase))
                    {
                        // 你之前特別處理 pxpayplues → PXPAY（若需要保留這個特例）
                        string suffix = lkey.Equals("pxpayplues", StringComparison.OrdinalIgnoreCase)
                            ? "PXPAY"
                            : key.Trim().ToUpperInvariant(); // 用原鍵名的大寫（保留可能的大小寫語意）
                        return AppendSuffix(urlString, suffix);
                    }

                    // 其餘一律忽略（不使用第一參數 fallback）
                }

                // 沒有任何符合規則者 → 回傳原值
                return urlString;
            }
            catch
            {
                // 解析失敗時維持原值（必要時可加 Log）
                return urlString;
            }
        }

        /// <summary>
        /// 自 <paramref name="url"/> 抽取查詢字串（去除前導 '?'）。支援絕對與相對 URL。
        /// </summary>
        /// <param name="url">來源網址（可為絕對或相對）。</param>
        /// <returns>查詢字串（不含 '?'）。</returns>
        private static string ExtractQuery(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            // 先嘗試以絕對 URI 解析
            if (Uri.TryCreate(url, UriKind.Absolute, out var abs))
                return abs.Query.TrimStart('?');

            // 相對或一般字串：手動抓 '?'
            int q = url.IndexOf('?');
            if (q >= 0 && q < url.Length - 1)
                return url.Substring(q + 1);

            return string.Empty;
        }

        /// <summary>
        /// 逐一列舉查詢參數，保留原始順序並進行 UrlDecode。
        /// 支援以 '&' 或 ';' 分隔參數，鍵名/值皆允許為空字串（但缺值時會回傳空字串）。
        /// </summary>
        /// <param name="query">查詢字串（不含 '?'）。</param>
        /// <returns>依序的鍵值對列舉。</returns>
        private static IEnumerable<KeyValuePair<string, string>> EnumerateQueryPreserveOrder(string query)
        {
            if (string.IsNullOrEmpty(query)) yield break;

            // 以 &、; 作為分隔符（兩者在實務上都常見）
            var parts = query.Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                int eq = part.IndexOf('=');
                string rawKey, rawVal;
                if (eq >= 0)
                {
                    rawKey = part.Substring(0, eq);
                    rawVal = part.Substring(eq + 1);
                }
                else
                {
                    rawKey = part;
                    rawVal = string.Empty;
                }

                // UrlDecode 鍵與值
                string key = HttpUtility.UrlDecode(rawKey ?? string.Empty) ?? string.Empty;
                string val = HttpUtility.UrlDecode(rawVal ?? string.Empty) ?? string.Empty;

                yield return new KeyValuePair<string, string>(key, val);
            }
        }

        /// <summary>
        /// 從 Web.config 讀取旗標白名單（appSettings["PurlFlagKeys"]），
        /// 若未設定則使用預設：line, fb, fbad, twm, cht。
        /// 以 HashSet 回傳供快速不分大小寫比對。
        /// </summary>
        /// <returns>旗標白名單集合（鍵名以小寫存放）。</returns>
        private static HashSet<string> GetFlagKeysFromConfig()
        {
            var keys = new List<string>();
            string csv = ConfigurationManager.AppSettings["PurlFlagKeys"];
            if (!string.IsNullOrWhiteSpace(csv))
            {
                keys.AddRange(
                    csv.Split(new[] { ',', ';', '、', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => s.Trim())
                );
            }
            else
            {
                // 預設白名單（可依需要調整或擴充）
                keys.AddRange(new[] { "line", "fb", "fbad", "twm", "cht", "fetsms", "jkos", "ig" });
            }

            return new HashSet<string>(keys.Select(k => k.ToLowerInvariant()), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判斷字串是否為「全部為數字」。
        /// </summary>
        /// <param name="s">輸入字串。</param>
        /// <returns>全部為數字則為 true，否則 false。</returns>
        private static bool IsAllDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// 在 <paramref name="baseText"/> 後補上底線與 <paramref name="suffix"/>。
        /// 若 <paramref name="baseText"/> 已以下劃線結尾，則不重複增加。
        /// </summary>
        /// <param name="baseText">基底字串（例如：Purdue_da_Index）。</param>
        /// <param name="suffix">要附加的後綴（建議已轉為大寫）。</param>
        /// <returns>合併後的字串。</returns>
        private static string AppendSuffix(string baseText, string suffix)
        {
            if (string.IsNullOrEmpty(baseText)) return suffix ?? string.Empty;
            if (string.IsNullOrEmpty(suffix)) return baseText;

            if (!baseText.EndsWith("_", StringComparison.Ordinal))
            {
                baseText += "_";
            }
            return baseText + suffix;
        }
    }
}