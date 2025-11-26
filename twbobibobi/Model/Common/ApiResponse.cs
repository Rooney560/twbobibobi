using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model.Common
{
    /// <summary>
    /// 統一的 API 回傳格式容器。
    /// </summary>
    /// <typeparam name="T">實際資料的類型</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 錯誤代碼（若有）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 錯誤或成功訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 實際資料（成功時）
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 快速建立成功回傳
        /// </summary>
        public static ApiResponse<T> Ok(T data, string message = null)
            => new ApiResponse<T> { Success = true, Code = "00000", Message = message ?? "OK", Data = data };

        /// <summary>
        /// 快速建立錯誤回傳
        /// </summary>
        public static ApiResponse<T> Fail(string code, string message)
            => new ApiResponse<T> { Success = false, Code = code, Message = message, Data = default };
    }
}