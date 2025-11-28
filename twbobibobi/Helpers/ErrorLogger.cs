/**************************************************************************
 *  專案名稱：twbobibobi
 *  檔案名稱：ErrorLogger.cs
 *  類別說明：統一處理例外錯誤訊息格式化，供 try-catch 區塊使用
 *
 *  建立日期：2025-11-10
 *  建立人員：Rooney
 *
 *  修改記錄：
 *  2025-11-10　Rooney　建立初版，支援統一錯誤格式化輸出
 *
 *  目前維護人員：Rooney
 *  最後修改日期：2025-11-10
 **************************************************************************/

using System;
using System.Web;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 提供統一格式的錯誤訊息組合工具，適用於 模組內所有 try-catch 錯誤記錄。
    /// </summary>
    public class ErrorLogger
    {
        /// <summary>
        /// 格式化錯誤訊息內容（含時間、來源、訊息、StackTrace）
        /// </summary>
        /// <param name="error">捕捉到的 Exception 物件</param>
        /// <param name="sourceClass">呼叫類別的名稱（建議使用 typeof(ClassName).FullName）</param>
        /// <returns>詳細錯誤訊息字串</returns>
        public static string FormatError(Exception error, string sourceClass)
        {
            // 設定時區為台北標準時間
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            try
            {
                var msg = error.InnerException != null
                ? error.InnerException.Message
                : error.Message;

                // 取得最內層的例外（真實的 NullReferenceException 通常在這裡）
                Exception inner = error.InnerException ?? error;

                // 組合詳細錯誤訊息
                string detailedError = string.Format(
                    "==== {0} Error ====\r\n" +
                    "Time: {1}\r\n" +
                    "Request URL: {2}\r\n" +
                    "Class: {3}\r\n" +
                    "Error Type: {4}\r\n" +
                    "Message: {5}\r\n" +
                    "Inner Message: {6}\r\n" +
                    "StackTrace:\r\n{7}\r\n\r\n",
                    sourceClass,
                    dtNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    HttpContext.Current?.Request?.Url?.ToString() ?? "N/A",
                    sourceClass,
                    error.GetType().FullName,
                    error.Message,
                    error.InnerException != null ? error.InnerException.Message : "N/A",
                    inner.StackTrace ?? "(no stack trace)"
                );

                return detailedError;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

                // 若 FormatError 自己也出錯，回傳簡化訊息
                return $"[ErrorLogger.Fallback] {msg}";
            }
        }
    }
}