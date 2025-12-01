using BCFBaseLibrary.Net;
using BCFBaseLibrary.String;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using twbobibobi.Helpers;

namespace twbobibobi.Data
{
    public class BasePage : System.Web.UI.Page
    {
        public static string CONST_CONFIG_DB_HOST = "DBHost";
        public static string CONST_CONFIG_ERROR_LOG_PATH = "ErrorLogPath";
        public static string CONST_CONFIG_REQUEST_LOG_PATH = "RequestLogPath";
        public static string CONST_CONFIG_REQUEST_PAY_LOG_PATH = "RequestPayLogPath";
        public static string CONST_CONFIG_REQUEST_CAPTCHACode_LOG_PATH = "RequestCAPTCHACodeLogPath";
        public static string CONST_CONFIG_REQUEST_LOG_SAVE = "RequestLogSave";
        public static string CONST_CONFIG_REQUEST_JSON_LOG_SAVE = "RequestJsonLogSave";
        public static string CONST_CONFIG_TIMING_LOG_PATH = "TimingLogPath";

        private static readonly object _logLock = new object();
        protected Boolean mIsJsonPage = false;
        protected static Boolean mSupportSaveRequestURL = true;
        protected Boolean mSupportSaveRequest = false;
        public JSonHelper mJSonHelper = new JSonHelper();
        public string TransactionID = string.Empty;

        protected virtual String DatabaseSourceString
        {
            get
            {
                return null;
            }
        }

        protected String CurrentMemberID
        {
            get
            {
                if (System.Web.HttpContext.Current.Request["MemberID"] != null)
                    return System.Web.HttpContext.Current.Request["MemberID"].ToString();
                else
                    return null;
            }
        }

        public Color HexColor(String hex)
        {
            //將井字號移除
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;
            int start = 0;

            //處理ARGB字串 
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            // 將RGB文字轉成byte
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }


        public String AdminURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AdminURL"] + "/";
            }
        }
        public String WebURLPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WebURLPrefix"] + "/";
            }
        }

        public String VideoURLPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["VideoURLPrefix"] + "/";
            }
        }

        public String ImageURLPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImageURLPrefix"] + "/";
            }
        }

        public String VideoHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["VideoHome"]+@"\";
            }
        }
        

        public String ImageHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImageHome"] + @"\";
            }
        }

        public String ImageWebHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImageWebHome"] + @"\";
            }
        }

        public String MenuIconURLPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MenuIconURLPrefix"] + "/";
            }
        }

        public String MenuIconHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MenuIconHome"] + @"\";
            }
        }

        public String ADImageURLPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ADImageURLPrefix"] + "/";
            }
        }

        public String ADImageHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ADImageHome"] + @"\";
            }
        }

        public String FFmpegHome
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FfmpegHome"] + @"\";
            }
        }

        public int OriginalImageWidth
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["OriginalImageWidth"]);
            }
        }

        public int DisplayImageWidth
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["DisplayImageWidth"]);
            }
        }

        public int ThumbnailImageWidth
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["ThumbnailImageWidth"]);
            }
        }

        public int ThumbnailImageHeight
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["ThumbnailImageHeight"]);
            }
        }

        public int VideoThumbnailImageWidth
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["VideoThumbnailImageWidth"]);
            }
        }

        public int VideoThumbnailImageHeight
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["VideoThumbnailImageHeight"]);
            }
        }

        public string MumberDefaultPassword
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MumberDefaultPassword"];
            }
        }

        public void ValidateCurrentMemebet()
        {
            if (CurrentMemberID == null)
            {
                Response.End();
            }
        }

        private System.Data.SqlClient.SqlConnection m_DBSource = null;

        protected int m_PageIndex = -1;

        public int PageIndex
        {
            get
            {
                if (m_PageIndex == -1)
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        try
                        {
                            m_PageIndex = int.Parse(Request.QueryString["Page"]);
                        }
                        catch
                        {
                            m_PageIndex = 1;
                        }
                    }
                    else
                    {
                        m_PageIndex = 1;
                    }
                }
                return m_PageIndex;
            }
            set
            {
                m_PageIndex = value;
            }
        }

        public int PageListSize
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageListSize"]);
            }
        }

        public void Scriptsfunction(string function, string obj1)
        {
            if (function != "" && obj1 != "")
                this.Page.Controls.Add(new LiteralControl("<script>" + function + "('" + obj1 + "');</script>"));
        }

        public void Scriptsfunction(string function, string obj1, string obj2)
        {
            if (function != "" && obj1 != "" && obj2 != "")
                this.Page.Controls.Add(new LiteralControl("<script>" + function + "('" + obj1 + "','" + obj2 + "');</script>"));
        }

        public void ScriptsfunctionOpenURL(string url)
        {
            if (url != "")
                this.Page.Controls.Add(new LiteralControl("<script>var w = window.open('" + url + "'); w.focus();</script>"));
        }

        public void ScriptsfunctionRedirect(string url)
        {
            if (url != "")
                this.Page.Controls.Add(new LiteralControl("<script>var w = window.location.assign('" + url + "'); w.focus();</script>"));
        }

        public void ScriptsfunctionRedirect(string function, string obj1, string url)
        {
            if (url != "" && function != "" && obj1 != "")
                this.Page.Controls.Add(new LiteralControl("<script>" + function + "('" + obj1 + "');var w = window.location.assign('" + url + "'); w.focus();</script>"));
        }

        public void ScriptsAlert(string msg)
        {
            if (msg != "")
                this.Page.Controls.Add(new LiteralControl("<script>alert('" + msg + "');</script>"));
        }

        public void ScriptsAlertRedirect(string msg, string url)
        {
            if (msg != "")
                this.Page.Controls.Add(new LiteralControl("<script>alert('" + msg + "'); var w = window.location.assign('" + url + "'); w.focus();</script>"));
        }

        public static string[] ShortUrl(string url)
        {
            //可以自定义生成MD5加密字符传前的混合KEY
            string key = "Rooney";
            //要使用生成URL的字符
            string[] chars = new string[]{
            "a","b","c","d","e","f","g","h",
            "i","j","k","l","m","n","o","p",
            "q","r","s","t","u","v","w","x",
            "y","z","0","1","2","3","4","5",
            "6","7","8","9","A","B","C","D",
            "E","F","G","H","I","J","K","L",
            "M","N","O","P","Q","R","S","T",
            "U","V","W","X","Y","Z"
          };
            //对传入网址进行MD5加密
            string hex = GetMD5Hash(key + url);

            string[] resUrl = new string[4];

            for (int i = 0; i < 4; i++)
            {
                //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
                int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                string outChars = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                    int index = 0x0000003D & hexint;
                    //把取得的字符相加
                    outChars += chars[index];
                    //每次循环按位右移5位
                    hexint = hexint >> 5;
                }
                //把字符串存入对应索引的输出数组
                resUrl[i] = outChars;
            }
            return resUrl;
        }

        public static string GetMD5Hash(string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();

        }

        public string OTPVercode(string mobileNo, string msg)
        {
            string resp = string.Empty;
            string Service = "QKee";

            string url = "http://im.holdline.info/EChatService/Interface/PhoneUser.aspx?APIMethod=SendSMS&MobileNo=" + mobileNo +
                            "&Message=" + msg + "&Service=" + Service;

            HTTPClient.Get(url, string.Empty, ref resp);

            return resp;
        }

        public System.Data.SqlClient.SqlConnection DBSource
        {
            get
            {
                if (this.m_DBSource == null)
                {
                    string DBDriverInfo ;
                    if (DatabaseSourceString != null && DatabaseSourceString == "SasaIsland")
                    {
                        DBDriverInfo = System.Configuration.ConfigurationManager.ConnectionStrings["SasaIslandDB"].ConnectionString;
                    }
                    else
                    {
                        DBDriverInfo = System.Configuration.ConfigurationManager.ConnectionStrings["DBHost"].ConnectionString;
                    }
                    m_DBSource = new System.Data.SqlClient.SqlConnection(DBDriverInfo);
                    m_DBSource.Open();
                }
                return m_DBSource;
            }
        }

        protected string ReplaceStr(string str, string key, string value, bool ignorecase)
        {
            string newstr = str.Replace(key, value);
            int i = newstr.IndexOf(key, StringComparison.OrdinalIgnoreCase);
            if (i > -1 && ignorecase)
            {
                key = newstr.Substring(i, key.Length);
                return ReplaceStr(newstr, key, value, ignorecase);
            }
            else
            {
                return newstr;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (this.m_DBSource != null)
            {
                this.m_DBSource.Close();
                this.m_DBSource = null;
            }
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.TransactionID = Session.SessionID + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            //// 擷取訪問者 IP
            //string clientIp = GetClientIp();

            //// 黑名單 IP：直接封鎖
            //if (IPBlocker.IsIpBlocked(clientIp))
            //{
            //    Response.StatusCode = 403;
            //    Response.End();
            //    return;
            //}

            //// 請求過於頻繁：封鎖 + 加入黑名單
            //if (IsRequestTooFrequent(clientIp, 20, TimeSpan.FromSeconds(1)))
            //{
            //    IPBlocker.BlockIp(clientIp);
            //    LogBlockedAttempt(clientIp, HttpContext.Current.Request.Url.ToString());
            //    Response.StatusCode = 429;
            //    Response.End();
            //    return;
            //}

            string szLogSaveFlag = GetConfigValue(CONST_CONFIG_REQUEST_LOG_SAVE);
            if (mSupportSaveRequest || szLogSaveFlag == "1")
            {
                SaveRequestLog(string.Empty);
            }
            
            //if (Session["Permission"] == null)
            //{
            //    if (this.Request.Url.ToString().IndexOf("Login") <= 0)
            //    {
            //        Response.Redirect("Login.aspx?ReturnUrl=" + Server.UrlEncode(this.Request.Url.ToString()));
            //    }
            //}
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            if (this.m_DBSource != null)
            {
                this.m_DBSource.Close();
                this.m_DBSource = null;
            }
            Application.Lock();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + @"\ErrorLog.txt", true);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine(Request.Url.ToString());
            sw.WriteLine(Server.GetLastError().Message);
            sw.WriteLine(Server.GetLastError().StackTrace);
            //sw.WriteLine(Server.GetLastError().ToString());
            sw.Close();
            Application.UnLock();

            if (mIsJsonPage)
            {
                this.mJSonHelper.AddContent("StatusCode", -1);
                this.ResponseJSonString();
            }
        }

        /// </summary>
        /// <param name="ip">用戶 IP</param>
        /// <param name="maxCount">最大請求次數</param>
        /// <param name="interval">時間區間</param>
        /// <returns>若超過限制則回傳 true</returns>
        private bool IsRequestTooFrequent(string ip, int maxCount, TimeSpan interval)
        {
            string key = $"REQ_COUNT_{ip}";
            object obj = HttpRuntime.Cache[key];

            int count = (obj == null) ? 0 : (int)obj;
            count++;

            if (count > maxCount)
                return true;

            if (obj == null)
            {
                HttpRuntime.Cache.Insert(
                    key,
                    count,
                    null,
                    DateTime.UtcNow.Add(interval),
                    System.Web.Caching.Cache.NoSlidingExpiration
                );
            }
            else
            {
                HttpRuntime.Cache[key] = count;
            }

            return false;
        }

        /// <summary>
        /// 取得訪問者真實 IP（考慮 Proxy）
        /// </summary>
        /// <returns>用戶 IP 字串</returns>
        private string GetClientIp()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                return ip.Split(',')[0];
            }
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 將封鎖紀錄寫入 Log（每 IP 僅記錄一次）
        /// </summary>
        /// <param name="ip">被封鎖的 IP</param>
        /// <param name="url">封鎖時嘗試訪問的 URL</param>
        private void LogBlockedAttempt(string ip, string url)
        {
            try
            {
                string logDir = Server.MapPath("~/Log/BlockedIP");
                Directory.CreateDirectory(logDir);

                string filePath = Path.Combine(logDir, "BlockedAttempt.log");

                // 檢查是否已經存在
                var lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];
                foreach (var line in lines)
                {
                    if (line.Contains(ip)) return; // 已存在就不重複記錄
                }

                string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | IP: {ip} | URL: {url}";
                File.AppendAllLines(filePath, new[] { log });
            }
            catch { /* 忽略錯誤 */ }
        }

        protected string RemoveHTML(string html)
        {
            System.Text.RegularExpressions.Regex objRegExp = new System.Text.RegularExpressions.Regex("<(.|\n)+?>");
            string strOutput = objRegExp.Replace(html, "");
            strOutput = strOutput.Replace("<", "<");
            strOutput = strOutput.Replace(">", "");
            return strOutput;
        }

        protected void SetListIndexSelected(System.Web.UI.WebControls.DropDownList list, string value)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Value == value)
                {
                    list.SelectedIndex = i;
                    break;
                }
            }
        }

        protected void SetListIndexSelected(System.Web.UI.WebControls.RadioButtonList list, string value)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Value == value)
                {
                    list.SelectedIndex = i;
                    break;
                }
            }
        }

        public void ResponseJSonString2()
        {
            Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            string jsonString = mJSonHelper.ToJSonString();
            Response.Write(jsonString);
            if (mSupportSaveRequest)
            {
                SaveRequestLog(jsonString);
            }
            //Response.End();
        }

        public void NoExceptionResponseEnd()
        {
            //將Buffer中的內容送出
            HttpContext.Current.Response.Flush();
            //忽視之後透過Response.Write輸出的內容
            HttpContext.Current.Response.SuppressContent = true;
            //忽略之後ASP.NET Pipeline的處理步驟，直接跳關到EndRequest
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        public void ResponseJSonString(int? httpStatus = null)
        {
            // 清掉任何既有輸出 (可選)
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();

            //if (httpStatus.HasValue)
            //    response.StatusCode = httpStatus.Value;

            // 設定 Content-Type
            Response.ContentType = "application/json; charset=UTF-8";

            // 產生 JSON
            string jsonString = mJSonHelper.ToJSonString();

            // 如果需記錄請求，保留
            if (mSupportSaveRequest)
            {
                SaveRequestLog(jsonString);
            }

            // 把 "\uXXXX" 形式的 unicode escape 全部 decode 成真正的字元
            //    這樣 raw response 就會直接是中文，而不是 \uXXXX
            //jsonString = Regex.Unescape(jsonString);


            string json = JsonConvert.SerializeObject(jsonString);

            // 寫入 JSON
            Response.Write(jsonString);

            // 推送到客戶端
            Response.Flush();

            // 5. 強制中止後續 ASP.NET 處理，立即送出
            response.End();
        }
        //public void ResponseJSonString()
        //{
        //    Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
        //    string jsonString = mJSonHelper.ToJSonString();
        //    Response.Write(jsonString);
        //    if (mSupportSaveRequest)
        //    {
        //        SaveRequestLog(jsonString);
        //    }
        //    try
        //    {
        //        //Response.End();
        //    }
        //    catch (Exception error)
        //    {

        //    }
        //}

        public void ResponseJSonString(string jsonString)
        {
            Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            Response.Write(jsonString);
            if (mSupportSaveRequest)
            {
                SaveRequestLog(jsonString);
            }
            Response.End();
        }

        private static readonly object logLock = new object();

        //public void SaveErrorLog(Exception e)
        //{
        //    Application.Lock();
        //    string logPath = GetConfigValue(CONST_CONFIG_ERROR_LOG_PATH);

        //    if (logPath == string.Empty)
        //        logPath = @"\err";


        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + logPath + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
        //    sw.WriteLine(System.DateTime.Now.ToString());
        //    sw.WriteLine(Request.Url.ToString());
        //    sw.WriteLine(TransactionID);
        //    sw.WriteLine(e.Message);
        //    sw.WriteLine(e.StackTrace);
        //    sw.Close();
        //    Application.UnLock();
        //}

        //public void SaveErrorLog(string log)
        //{
        //    Application.Lock();
        //    string logPath = GetConfigValue(CONST_CONFIG_ERROR_LOG_PATH);

        //    if (logPath == string.Empty)
        //        logPath = @"\err";


        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + logPath + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
        //    sw.WriteLine(System.DateTime.Now.ToString());

        //    if (mSupportSaveRequestURL)
        //    {
        //        sw.WriteLine(Request.Url.ToString());
        //    }
        //    if (log != string.Empty)
        //    {
        //        sw.WriteLine(log);
        //    }
        //    sw.Close();
        //    Application.UnLock();
        //}

        //public void SaveRequestLog(string log)
        //{
        //    Application.Lock();
        //    string logPath = GetConfigValue(CONST_CONFIG_REQUEST_LOG_PATH);

        //    if (logPath == string.Empty)
        //        logPath = @"\request";


        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + logPath + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
        //    sw.WriteLine(System.DateTime.Now.ToString());

        //    if (mSupportSaveRequestURL)
        //    {
        //        sw.WriteLine(Request.Url.ToString());
        //    }
        //    if (log != string.Empty)
        //    {
        //        sw.WriteLine(log);
        //    }
        //    sw.Close();
        //    Application.UnLock();
        //}

        //public void SavePayLog(string log)
        //{
        //    Application.Lock();
        //    string logPath = GetConfigValue(CONST_CONFIG_REQUEST_PAY_LOG_PATH);

        //    if (logPath == string.Empty)
        //        logPath = @"\request";


        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + logPath + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
        //    sw.WriteLine(System.DateTime.Now.ToString());

        //    if (mSupportSaveRequestURL)
        //    {
        //        sw.WriteLine(Request.Url.ToString());
        //    }
        //    if (log != string.Empty)
        //    {
        //        sw.WriteLine(log);
        //    }
        //    sw.Close();
        //    Application.UnLock();
        //}

        //public void SaveCAPTCHACodeLog(string log)
        //{
        //    Application.Lock();
        //    string logPath = GetConfigValue(CONST_CONFIG_REQUEST_PAY_LOG_PATH);

        //    if (logPath == string.Empty)
        //        logPath = @"\request";


        //    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(Request.ApplicationPath) + logPath + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
        //    sw.WriteLine(System.DateTime.Now.ToString());

        //    if (mSupportSaveRequestURL)
        //    {
        //        sw.WriteLine(Request.Url.ToString());
        //    }
        //    if (log != string.Empty)
        //    {
        //        sw.WriteLine(log);
        //    }
        //    sw.Close();
        //    Application.UnLock();
        //}

        public void SaveErrorLog(Exception e)
        {
            string logPath = GetConfigValue(CONST_CONFIG_ERROR_LOG_PATH);
            if (string.IsNullOrEmpty(logPath))
                logPath = @"\err";

            string fullPath = Path.Combine(Server.MapPath("~"), logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            lock (logLock)
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(Request.Url.ToString());
                    sw.WriteLine(TransactionID);
                    sw.WriteLine(e.Message);
                    sw.WriteLine(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// 写错误日志到：{BaseDirectory}/{ErrorLogPath}/{Prefix}_{yyyyMMdd}.txt
        /// </summary>
        /// <param name="log">要写入的错误内容</param>
        /// <param name="TransactionID">可选的交易编号</param>
        public void SaveErrorLog(string log, string TransactionID = null)
        {
            try
            {
                // 取当前 HttpContext（可能为 null）
                HttpContext context = HttpContext.Current;

                // 从配置里拿到 "./Log/Temple_err"
                string configValue = ConfigurationManager.AppSettings[CONST_CONFIG_ERROR_LOG_PATH];
                if (string.IsNullOrWhiteSpace(configValue))
                {
                    // 万一没配，就用这个
                    configValue = "Log/Temple_err";
                }

                // 把配置拆成：目录 + 前缀
                // 目录部分 => "./Log"
                string relativeDir = Path.GetDirectoryName(configValue);
                if (string.IsNullOrWhiteSpace(relativeDir))
                {
                    // 如果只有一个段，比如 configValue="Temple_err"
                    relativeDir = configValue;
                }

                // 前缀 => "Temple_err"
                string prefix = Path.GetFileName(configValue) ?? "error";

                // 应用程序根目录
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // 合成并规范化目录路径（去掉 "./"）
                string fullLogDir = Path.GetFullPath(Path.Combine(basePath, relativeDir));

                // 确保目录存在
                Directory.CreateDirectory(fullLogDir);

                // 构建文件名：Temple_err_yyyyMMdd.txt
                string fileName = $"{prefix}_{DateTime.Now:yyyyMMdd}.txt";

                // 最终完整路径
                string filePath = Path.Combine(fullLogDir, fileName);

                // 多线程安全写入
                lock (_logLock)
                {
                    using (var writer = new StreamWriter(filePath, true))
                    {
                        // 时间精确到毫秒
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                        if (mSupportSaveRequestURL && context != null)
                        {
                            writer.WriteLine(context.Request?.Url?.ToString() ?? "[No Request URL]");
                        }

                        if (!string.IsNullOrWhiteSpace(TransactionID))
                        {
                            writer.WriteLine(TransactionID);
                        }

                        if (!string.IsNullOrEmpty(log))
                        {
                            writer.WriteLine(log);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果写日志失败，可择一：写到 EventLog，或静默略过，避免二次异常
            }
        }

        //public void SaveErrorLog(string log)
        //{
        //    string logPath = GetConfigValue(CONST_CONFIG_ERROR_LOG_PATH);
        //    if (string.IsNullOrEmpty(logPath))
        //        logPath = @"\err";

        //    string fullPath = Server.MapPath(Request.ApplicationPath) + logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

        //    lock (logLock)
        //    {
        //        using (StreamWriter sw = new StreamWriter(fullPath, true))
        //        {
        //            sw.WriteLine(DateTime.Now.ToString());

        //            if (mSupportSaveRequestURL)
        //            {
        //                sw.WriteLine(Request.Url.ToString());
        //            }
        //            if (!string.IsNullOrEmpty(log))
        //            {
        //                sw.WriteLine(log);
        //            }
        //        }  // `using` 结束时，文件会自动释放
        //    }
        //}

        public void SaveRequestLog(string log)
        {
            var path = Request.Path.ToLowerInvariant();
            if (path.Contains(".aspx/js") || path.Contains(".aspx/css") || path.Contains(".aspx/images"))
            {
                // 直接 return 或跳過 logging
                Response.StatusCode = 404;
                Response.End();
                return;
            }
            else
            {
                string logPath = GetConfigValue(CONST_CONFIG_REQUEST_LOG_PATH);
                if (string.IsNullOrEmpty(logPath))
                    logPath = "\request";

                string fullPath = Path.Combine(Server.MapPath("~"), logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                lock (logLock)
                {
                    using (StreamWriter sw = new StreamWriter(fullPath, true))
                    {
                        sw.WriteLine(DateTime.Now.ToString());
                        if (mSupportSaveRequestURL)
                        {
                            sw.WriteLine(Request.Url.ToString());
                        }
                        if (!string.IsNullOrEmpty(log))
                        {
                            sw.WriteLine(log);
                        }
                    }
                }
            }
        }

        public void SavePayLog(string log)
        {
            string logPath = GetConfigValue(CONST_CONFIG_REQUEST_PAY_LOG_PATH);
            if (string.IsNullOrEmpty(logPath))
                logPath = "\request";

            string fullPath = Path.Combine(Server.MapPath("~"), logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            lock (logLock)
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    if (!string.IsNullOrEmpty(log))
                    {
                        sw.WriteLine(log);
                    }
                }
            }
        }

        public void SaveCAPTCHACodeLog(string log)
        {
            string logPath = GetConfigValue(CONST_CONFIG_REQUEST_CAPTCHACode_LOG_PATH);
            if (string.IsNullOrEmpty(logPath))
                logPath = "\request";

            string fullPath = Path.Combine(Server.MapPath("~"), logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            lock (logLock)
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    if (!string.IsNullOrEmpty(log))
                    {
                        sw.WriteLine(log);
                    }
                }
            }
        }

        public void SaveTimingLog(string stepDescription, long elapsedMilliseconds)
        {
            string logPath = GetConfigValue(CONST_CONFIG_TIMING_LOG_PATH);
            if (string.IsNullOrEmpty(logPath))
                logPath = @"\err";

            string fullPath = Server.MapPath(Request.ApplicationPath) + logPath + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}\t[{stepDescription}]\t耗时：{elapsedMilliseconds} ms";

            lock (logLock)
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine(DateTime.Now.ToString());

                    if (mSupportSaveRequestURL)
                    {
                        sw.WriteLine(Request.Url.ToString());
                    }
                    if (!string.IsNullOrEmpty(logLine))
                    {
                        sw.WriteLine(logLine + Environment.NewLine);
                    }
                }  // `using` 结束时，文件会自动释放
            }
        }


        public string GetConfigValue(string paramName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[paramName];
            }
            catch (System.Configuration.ConfigurationErrorsException e)
            {
                return string.Empty;
            }
        }

        private static Regex RegMoblie = new Regex("^09[0-9]{8}|^8869[0-9]{8}");
        private static Regex RegTWMobile = new Regex("^(8869)[0-9]{8}$");
        private static Regex RegTWMobileCoed = new Regex("^\\/[0-9A-Z\\+\\-\\.]{7}$");
        private static Regex RegTWCDD = new Regex("^TP[0-9]{14}$");

        /// <summary>
        /// 檢查 台灣手機號碼 的格式
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsMoblie(string inputData)
        {
            Match m = RegMoblie.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 檢查 台灣手機號碼 的格式
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTWMobile(string inputData)
        {
            Match m = RegTWMobile.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 檢查 手機載具 的格式
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTWMobileCode(string inputData)
        {
            Match m = RegTWMobileCoed.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 檢查 自然人憑證 的格式
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTWCDC(string inputData)
        {
            Match m = RegTWCDD.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否為TW手機號
        /// </summary>
        public static string[] ReplaceCode(string[] NumList)
        {
            string[] newNumList = new string[NumList.Length];
            for (int i = 0; i < NumList.Length; i++)
            {
                if (NumList[i] != null)
                {
                    if (IsTWMobile(NumList[i]))
                    {
                        newNumList[i] = NumList[i];
                    }
                    else
                    {
                        newNumList[i] = NumList[i].Replace("09", "8869");
                    }
                }
            }

            return newNumList;
        }

        protected bool IsTransparent(Bitmap image)
        {
            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    if (image.GetPixel(x, y).A != 255)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public System.Drawing.Bitmap DownloadImage(string url)
        {
            System.Drawing.Bitmap tmpImage = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Timeout = 5000;
                request.Method = "GET";

                request.ContentType = "text/html;charset=UTF-8";



                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    System.IO.Stream webStream = response.GetResponseStream();

                    if (webStream != null) tmpImage = new Bitmap(System.Drawing.Bitmap.FromStream(webStream));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                return null;
            }

            return tmpImage;
        }

        protected override void InitializeCulture()
        {
            if (Request["lang"] != null)
            {
                Page.UICulture = Request["lang"].ToString();
            }
        }
    }
}