using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twbobibobi
{
    /// <summary>
    /// purl 修復工具頁面（純 Log 模式）：從日誌中找出 templeCheck 請求，
    /// 若查詢字串包含 purl=xxx 或自訂旗標(如 line、fb) 等於 1，則列出並產出建議字串。
    /// 不連資料庫。
    /// </summary>
    public partial class LogPurlScanner : System.Web.UI.Page
    {
        /// <summary>
        /// 自訂旗標參數鍵清單（預設：line, fb, fbad, twm, cht）。
        /// 亦可在 Web.config 以 appSettings["PurlFlagKeys"]="line,fb,fbad,twm,cht" 覆寫。
        /// </summary>
        private static readonly string[] DefaultFlagKeys = new[] { "line", "fb", "fbad", "twm", "cht", "fetsms", "jkos", "ig" };

        /// <summary>
        /// 頁面載入事件：支援以 querystring 帶入 date=YYYYMMDD 與 code（宮廟代號或 AdminID）自動帶入。
        /// </summary>
        /// <param name="sender">事件來源。</param>
        /// <param name="e">事件參數。</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qsDate = Request["date"];
                txtDate.Text = !string.IsNullOrWhiteSpace(qsDate) ? qsDate.Trim() : DateTime.Now.ToString("yyyyMMdd");

                string qsCode = Request["code"];
                if (!string.IsNullOrWhiteSpace(qsCode))
                    txtCode.Text = qsCode.Trim();
            }
        }

        /// <summary>
        /// 掃描按鈕事件：依日期載入日誌，過濾 templeCheck 之 purl/旗標=1 紀錄，並輸出結果（支援分頁）。
        /// </summary>
        /// <param name="sender">事件來源。</param>
        /// <param name="e">事件參數。</param>
        protected void btnScan_Click(object sender, EventArgs e)
        {
            BindGridForDate(txtDate.Text);
        }

        /// <summary>
        /// GridView 換頁事件：切換頁面後維持相同日期搜尋結果。
        /// </summary>
        /// <param name="sender">事件來源。</param>
        /// <param name="e">頁面變更事件參數。</param>
        protected void gvResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResults.PageIndex = e.NewPageIndex;
            BindGridForDate(txtDate.Text);
        }

        /// <summary>
        /// 依日期綁定 GridView（讀取檔案→解析→過濾→顯示）。
        /// </summary>
        /// <param name="datePart">日期字串(YYYYMMDD)。</param>
        private void BindGridForDate(string datePart)
        {
            litStatus.Text = string.Empty;
            gvResults.DataSource = null;
            gvResults.DataBind();

            string dp = (datePart ?? string.Empty).Trim();
            if (!Regex.IsMatch(dp, @"^\d{8}$"))
            {
                litStatus.Text = "<span class='err'>請輸入正確日期格式 YYYYMMDD。</span>";
                return;
            }

            string filePath = ResolveLogPath(dp, out string altPath);
            if (!File.Exists(filePath) && !string.IsNullOrEmpty(altPath) && File.Exists(altPath))
                filePath = altPath;

            if (!File.Exists(filePath))
            {
                litStatus.Text = $"<span class='warn'>找不到日誌檔：{HttpUtility.HtmlEncode(filePath)}</span>";
                return;
            }

            try
            {
                // 讀檔與解析
                var lines = File.ReadLines(filePath).ToList();
                int totalLineCount = lines.Count;

                var results = ScanLines(lines).ToList();

                // templeCheck 才顯示（你需求限定 templeCheck）
                var display = results
                    .Where(r => r.PageName.Equals("templeCheck.aspx", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(r => r.LogTime)
                    .ToList();

                // ★ 代號 / AdminID 過濾（可選）
                string codeRaw = (txtCode.Text ?? string.Empty).Trim();
                if (!string.IsNullOrEmpty(codeRaw))
                {
                    string codeLower = codeRaw.ToLowerInvariant();
                    bool isNumeric = codeRaw.All(char.IsDigit);

                    if (isNumeric)
                    {
                        // 使用 AdminID（a）過濾，例如輸入 "3"
                        display = display
                            .Where(r => string.Equals((r.A ?? "").Trim(), codeRaw, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }
                    else
                    {
                        // 使用宮廟代號（TempleCode）過濾，例如輸入 "da"
                        display = display
                            .Where(r => string.Equals((r.TempleCode ?? "").Trim(), codeLower, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }
                }

                gvResults.DataSource = display;
                gvResults.DataBind();

                Debug.WriteLine($"[LogPurlScanner] filePath={filePath}");
                Debug.WriteLine($"[LogPurlScanner] totalLines={totalLineCount}, matched={display.Count}, codeFilter='{codeRaw}'");

                string filterNote = string.IsNullOrEmpty(codeRaw) ? "" : $"，代號/ID 過濾：{HttpUtility.HtmlEncode(codeRaw)}";
                litStatus.Text = $"<span class='ok'>讀取完成。檔案行數：{totalLineCount}，擷取筆數：{display.Count}（templeCheck{filterNote}）。</span>";
            }
            catch (Exception ex)
            {
                litStatus.Text = $"<span class='err'>讀取/解析失敗：{HttpUtility.HtmlEncode(ex.Message)}</span>";
            }
        }

        /// <summary>
        /// 解析日誌行，擷取 templeCheck 之 purl 或旗標參數=1 的請求。
        /// </summary>
        /// <param name="lines">日誌行序列。</param>
        /// <returns>符合條件的紀錄集合。</returns>
        private IEnumerable<PurlRecord> ScanLines(IEnumerable<string> lines)
        {
            // 只抓 templeCheck 或 templeService（保留彈性），你要限定 templeCheck 就只會被 where 篩掉
            var urlRegex = new Regex(
                @"(?<time>^\s*[\[\(]?(?<ts>[\d/\-\:\s]{8,})[\]\)]?\s*)?.*?(?<url>(templeCheck\.aspx\?[^\s]+|templeService_[\w]+\.aspx\?[^\s]+))",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string[] flagKeys = GetFlagKeys();

            int lineNo = 0;
            foreach (var raw in lines)
            {
                lineNo++;

                var m = urlRegex.Match(raw);
                if (!m.Success) continue;

                string url = m.Groups["url"].Value;

                // 取 page 與 query
                string page = url;
                string query = string.Empty;
                int q = url.IndexOf('?');
                if (q >= 0)
                {
                    page = url.Substring(0, q);
                    query = url.Substring(q + 1);
                }

                // 只處理 templeCheck
                if (!page.Equals("templeCheck.aspx", StringComparison.OrdinalIgnoreCase))
                    continue;

                NameValueCollection nv = HttpUtility.ParseQueryString(query);

                // 條件 1：purl=xxx
                string purl = nv["purl"];
                bool matchByPurl = !string.IsNullOrWhiteSpace(purl);

                // 條件 2：flagKeys 任一鍵 = "1"
                string hitFlagKey = flagKeys.FirstOrDefault(k => string.Equals(nv[k], "1", StringComparison.OrdinalIgnoreCase));
                bool matchByFlag = !string.IsNullOrEmpty(hitFlagKey);

                if (!matchByPurl && !matchByFlag)
                    continue;

                // 組紀錄
                var rec = new PurlRecord
                {
                    LineNo = lineNo,
                    LogTime = TryParseTimestamp(m.Groups["ts"].Value),
                    LogTimeText = TryParseTimestamp(m.Groups["ts"].Value)?.ToString("yyyy/MM/dd HH:mm:ss") ?? "",
                    RawUrl = url.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? url : ("/" + page + "?" + query),
                    DisplayUrl = page + "?" + query,
                    PageName = page,
                    Kind = nv["kind"],
                    A = nv["a"],
                    Aid = nv["aid"],
                };

                if (matchByPurl)
                {
                    rec.MatchType = "purl";
                    rec.MatchKey = "purl";
                    rec.MatchValue = purl.Trim();
                    rec.SuffixTail = rec.MatchValue.ToUpperInvariant();
                }
                else
                {
                    rec.MatchType = "flag=1";
                    rec.MatchKey = hitFlagKey;
                    rec.MatchValue = "1";
                    rec.SuffixTail = rec.MatchKey.ToUpperInvariant();
                }

                // 宮廟/服務對照（供建議字串使用）
                MapTempleAndService(rec);

                // 建議：Purdue_{代碼}_Index_{尾碼}（kind=1 則 Lights_...）
                rec.SuggestedText = BuildSuggested(rec);

                yield return rec;
            }
        }

        /// <summary>
        /// 取得旗標鍵清單：優先讀取 Web.config appSettings["PurlFlagKeys"]，否則使用預設值。
        /// </summary>
        /// <returns>旗標鍵陣列。</returns>
        private static string[] GetFlagKeys()
        {
            string csv = ConfigurationManager.AppSettings["PurlFlagKeys"];
            if (!string.IsNullOrWhiteSpace(csv))
                return csv.Split(new[] { ',', '；', ';', '、', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(s => s.Trim()).ToArray();

            return DefaultFlagKeys;
        }

        /// <summary>
        /// 自日期字串產生日誌路徑：優先單底線，備援雙底線。
        /// </summary>
        /// <param name="datePart">YYYYMMDD。</param>
        /// <param name="altPath">雙底線路徑。</param>
        /// <returns>單底線主路徑。</returns>
        private string ResolveLogPath(string datePart, out string altPath)
        {
            string baseDir = Server.MapPath("~/Log");
            string file1 = Path.Combine(baseDir, $"Temple_req_{datePart}.txt");
            string file2 = Path.Combine(baseDir, $"Temple_req__{datePart}.txt");
            altPath = file2;
            return file1;
        }

        /// <summary>
        /// 嘗試解析時間戳，容錯多種格式。
        /// </summary>
        /// <param name="raw">原始時間字串。</param>
        /// <returns>成功回傳 DateTime，否則 null。</returns>
        private static DateTime? TryParseTimestamp(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;
            var candidates = new[]
            {
                "yyyy/MM/dd HH:mm:ss","yyyy-MM-dd HH:mm:ss","MM/dd/yyyy HH:mm:ss","yyyy/M/d H:m:s","M/d/yyyy H:m:s"
            };
            foreach (var fmt in candidates)
            {
                if (DateTime.TryParseExact(raw.Trim(), fmt, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    return dt;
            }
            if (DateTime.TryParse(raw, out var dt2)) return dt2;
            return null;
        }

        /// <summary>
        /// 依 a(宮廟 AdminID) 與 kind(服務) 填入宮廟名稱、代碼與服務名稱（供建議字串組合）。
        /// </summary>
        /// <param name="rec">要更新的紀錄。</param>
        private static void MapTempleAndService(PurlRecord rec)
        {
            // a → 宮廟代碼
            switch ((rec.A ?? "").Trim())
            {
                case "3": rec.TempleName = "大甲鎮瀾宮"; rec.TempleCode = "da"; break;
                case "4": rec.TempleName = "新港奉天宮"; rec.TempleCode = "h"; break;
                case "6": rec.TempleName = "北港武德宮"; rec.TempleCode = "wu"; break;
                case "8": rec.TempleName = "西螺福興宮"; rec.TempleCode = "Fu"; break;
                case "10": rec.TempleName = "台南正統鹿耳門聖母廟"; rec.TempleCode = "Luer"; break;
                case "14": rec.TempleName = "桃園威天宮"; rec.TempleCode = "ty"; break;
                case "15": rec.TempleName = "斗六五路財神宮"; rec.TempleCode = "Fw"; break;
                case "16": rec.TempleName = "台東東海龍門天聖宮"; rec.TempleCode = "dh"; break;
                case "21": rec.TempleName = "鹿港城隍廟"; rec.TempleCode = "Lk"; break;
                case "23": rec.TempleName = "玉敕大樹朝天宮"; rec.TempleCode = "ma"; break;
                case "31": rec.TempleName = "台灣道教總廟無極三清總道院"; rec.TempleCode = "wjsan"; break;
                default: rec.TempleName = "(未知宮廟)"; rec.TempleCode = "xx"; break;
            }

            // kind → 服務
            switch ((rec.Kind ?? "").Trim())
            {
                case "1": rec.ServiceName = "點燈"; break;
                case "2": rec.ServiceName = "普渡"; break;
                case "7": rec.ServiceName = "天赦日招財補運"; break;
                case "16": rec.ServiceName = "補財庫"; break;
                default: rec.ServiceName = "(未知服務)"; break;
            }
        }

        /// <summary>
        /// 依服務 kind 建立服務鍵：1→Lights、2→Purdue，其餘為 Svc{kind}。
        /// </summary>
        /// <param name="kind">服務代碼。</param>
        /// <returns>服務鍵名。</returns>
        private static string GetServiceKey(string kind)
        {
            switch ((kind ?? "").Trim())
            {
                case "1": return "Lights";
                case "2": return "Purdue";
                case "7": return "Supplies";
                case "16": return "Supplise";
                default: return $"Svc{(kind ?? "").Trim()}";
            }
        }

        /// <summary>
        /// 產生完整建議字串，如：Purdue_da_Index_LINE；若代碼未知以 xx 代替。
        /// </summary>
        /// <param name="rec">目前紀錄。</param>
        /// <returns>建議字串。</returns>
        private static string BuildSuggested(PurlRecord rec)
        {
            string svcKey = GetServiceKey(rec.Kind);
            string code = string.IsNullOrWhiteSpace(rec.TempleCode) ? "xx" : rec.TempleCode.Trim();
            string tail = (rec.SuffixTail ?? "").Trim();
            if (tail.Length == 0) return string.Empty;
            return $"{svcKey}_{code}_Index_{tail}";
        }

        /// <summary>
        /// 掃描結果資料結構（供 GridView 綁定）。
        /// </summary>
        private sealed class PurlRecord
        {
            /// <summary>來源行號（除錯用）。</summary>
            public int LineNo { get; set; }
            /// <summary>日誌時間。</summary>
            public DateTime? LogTime { get; set; }
            /// <summary>日誌時間字串。</summary>
            public string LogTimeText { get; set; }
            /// <summary>原始網址（可能相對）。</summary>
            public string RawUrl { get; set; }
            /// <summary>顯示用網址。</summary>
            public string DisplayUrl { get; set; }
            /// <summary>頁面名（templeCheck.aspx）。</summary>
            public string PageName { get; set; }
            /// <summary>觸發類型（purl / flag=1）。</summary>
            public string MatchType { get; set; }
            /// <summary>觸發鍵（purl 或 line/fb 等）。</summary>
            public string MatchKey { get; set; }
            /// <summary>觸發值（purl 的值或 "1"）。</summary>
            public string MatchValue { get; set; }
            /// <summary>建議尾碼（大寫）。</summary>
            public string SuffixTail { get; set; }
            /// <summary>建議字串（例如 Purdue_da_Index_LINE）。</summary>
            public string SuggestedText { get; set; }
            /// <summary>kind 服務代碼。</summary>
            public string Kind { get; set; }
            /// <summary>a（AdminID）。</summary>
            public string A { get; set; }
            /// <summary>aid（ApplicantID）。</summary>
            public string Aid { get; set; }
            /// <summary>宮廟名稱。</summary>
            public string TempleName { get; set; }
            /// <summary>宮廟代碼（da、h、wu…）。</summary>
            public string TempleCode { get; set; }
            /// <summary>服務名稱。</summary>
            public string ServiceName { get; set; }
        }
    }
}
