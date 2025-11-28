using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// 批量查詢時，TempleCode 資訊
    /// </summary>
    public class TempleCodeInfoDto
    {
        public string ProductCode { get; set; }
        public int AdminID { get; set; }
        public int ServiceID { get; set; }
        public string TypeID { get; set; }
        public string TypeString { get; set; }
        public string ItemTypeID { get; set; }
    }
}