using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 驗證碼主模型
    /// </summary>
    public class CaptchaModel
    {
        /// <summary>購買人姓名</summary>
        public string AppName { get; set; }

        /// <summary>驗證碼</summary>
        public string Code { get; set; }
    }
}