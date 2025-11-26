using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 感謝狀主模型
    /// </summary>
    public class ThankYouModel
    {
        /// <summary>用戶名稱</summary>
        public string UserName { get; set; }

        /// <summary>感謝狀內容</summary>
        public string Message { get; set; }
    }
}