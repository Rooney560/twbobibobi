using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 定義系統內各種宮廟服務的類型。
    /// 用於查詢時判斷要呼叫哪一個資料來源或資料表。
    /// </summary>
    public enum ServiceKind
    {
        /// <summary>
        /// 點燈類服務，例如太歲燈、光明燈、文昌燈等。
        /// </summary>
        Light = 1,

        /// <summary>
        /// 普度或中元普渡相關法會。
        /// </summary>
        Purdue = 2,

        /// <summary>
        /// 下元補庫。
        /// </summary>
        Supplies = 4,

        /// <summary>
        /// 祈安植福。
        /// </summary>
        Blessing = 23,

        /// <summary>
        /// 祈安植福。
        /// </summary>
        QnLight = 25

    }
}