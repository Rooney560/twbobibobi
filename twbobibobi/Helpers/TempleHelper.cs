/****************************************************
* 專案名稱：twbobibobi
* 檔案名稱：TempleHelper.cs
* 類別說明：提供宮廟相關資料查詢、燈種檢查與容量判斷之輔助方法、容量紀錄功能
* 建立日期：2025-11-01
* 建立人員：Rooney
* 修改記錄：2025-11-01 合併多宮廟燈種檢查邏輯。
*           2025-11-17 加入強制下架機制、統一錯誤紀錄與XML註解格式；新增每日燈種容量自動紀錄功能（LogDailyLightsCapacity）
* 目前維護人員：Rooney
****************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using Temple.data;
using twbobibobi.Data;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 【TempleHelper】
    /// 提供宮廟輔助工具類別：
    /// <list type="bullet">
    /// <item>依據 AdminID 取得宮廟暱稱</item>
    /// <item>檢查指定燈種剩餘數量是否額滿</item>
    /// <item>提供強制下架機制（Force Close）</item>
    /// </list>
    /// </summary>
    public class TempleHelper : SqlClientBase
    {
        /// <summary>
        /// 建構子（初始化連線來源）
        /// </summary>
        /// <param name="basePage">基底頁面，包含 DB 連線資訊。</param>
        public TempleHelper(BasePage basePage) : base(basePage)
        {
        }

        #region 查詢：取得宮廟名稱
        /// <summary>
        /// 根據 AdminID 從資料庫查出對應宮廟名稱（Nickname）
        /// </summary>
        /// <param name="adminId">宮廟編號。</param>
        /// <param name="_basePage">基底頁面物件。</param>
        /// <returns>回傳宮廟中文名稱，查無資料則為「未知宮廟」。</returns>
        public static string GetTempleName(int adminId, 
            BasePage _basePage)
        {
            try
            {
                AdminDAC adminDAC = new AdminDAC(_basePage);
                DataTable dt = adminDAC.GetAdminInfo(adminId);

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Nickname"].ToString();
                }
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(TempleHelper).FullName);
                _basePage.SaveErrorLog("TempleHelper.GetTempleName " + $", AdminID: {adminId}：\r\n" + detailedError);
            }

            return "未知宮廟";
        }
        #endregion

        #region 檢查：燈種容量與下架判斷
        /// <summary>
        /// 檢查指定宮廟之指定燈種剩餘數量是否即將額滿，必要時發送警示簡訊通知。
        /// </summary>
        /// <param name="LightsType">燈種代碼。
        /// 3-光明燈、元神光明燈(鹿港城隍廟)、貴人燈(斗六五路財神宮)、平安燈(北極玄天宮、壽山巖觀音寺)
        /// 4-安太歲、太歲燈、太歲平安符(鹿港城隍廟)、安奉太歲
        /// 5-文昌燈、五文昌燈、文魁智慧燈(鹿港城隍廟)、文昌功名燈(桃園龍德宮)
        /// 6-財神燈、發財燈、福財燈、正財福報燈(鹿港城隍廟)、招財燈(壽山巖觀音寺)、五路財神燈(桃園龍德宮)
        /// 7-姻緣燈、桃花燈、月老桃花燈
        /// 8-藥師燈 、藥師佛燈、消災延壽燈(斗六五路財神宮)、特別健康燈(石壁部堂)、健康延壽燈(桃園龍德宮)
        /// 9-財利燈 
        /// 10-貴人燈 、特別貴人燈(石壁部堂)
        /// 11-福祿燈、福壽燈 
        /// 12-寵物平安燈 
        /// 13-龍王燈 
        /// 14-虎爺燈 
        /// 15-轉運納福燈 
        /// 16-光明燈上層、玉皇燈(五股賀聖宮)
        /// 17-偏財旺旺燈 
        /// 18-廣進安財庫 
        /// 19-財庫燈 
        /// 20-月老姻緣燈 
        /// 21-孝親祈福燈 
        /// 22-事業燈、特別事業燈(石壁部堂)
        /// 23-全家光明燈 
        /// 24-觀音佛祖燈 
        /// 25-財神斗、財神斗/一個月(斗六五路財神宮)
        /// 26-事業斗 
        /// 27-平安斗 
        /// 28-文昌斗 
        /// 29-藥師斗 
        /// 30-元神斗 
        /// 31-福祿壽斗 
        /// 32-觀音斗 
        /// 33-明心智慧燈
        /// 34-發財斗/一個月
        /// 35-姻緣斗/一個月
        /// 36-貴人斗/一個月
        /// 37-消災延壽斗/一個月
        /// 38-財神斗/三個月
        /// 39-發財斗/三個月
        /// 40-姻緣斗/三個月
        /// 41-貴人斗/三個月
        /// 42-消災延壽斗/三個月
        /// 43-元辰斗燈、元辰燈(壽山巖觀音寺)
        /// 44-求子燈
        /// 45-護子燈
        /// 46-添丁燈
        /// 47-婚姻燈
        /// 48-安太陰
        /// 49-安太陽
        /// </param>
        /// <param name="AdminID">宮廟編號（例如：14=桃園威天宮）。</param>
        /// <param name="Count">本次欲新增數量。</param>
        /// <param name="Year">資料年份（例如 "2025"）。</param>
        /// <param name="_basePage">基底頁面物件。</param>
        /// <returns>
        /// 回傳 <c>true</c> 表示該燈種已額滿或設定為下架；
        /// 回傳 <c>false</c> 表示仍可新增。
        /// </returns>
        public bool CheckLightsCapacity(
            string LightsType, 
            string AdminID, 
            int Count, 
            string Year, 
            BasePage _basePage)
        {
            bool result = false;
            int remainingCount = 0;

            try
            {
                string TempleCode = GetTempleCode(AdminID);
                if (string.IsNullOrEmpty(TempleCode) || string.IsNullOrEmpty(Year))
                    return false;

                string sql = $"SELECT * FROM Temple_{Year}..view_Lights_{TempleCode}_infowithAPPCharge " +
                             "WHERE AdminID = @AdminID AND LightsType = @LightsType AND AppStatus = 2 AND Num > 0 AND AppcStatus = 1";

                DataTable dtGetData = new DataTable();
                using (DatabaseAdapter adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.AddParameterToSelectCommand("AdminID", AdminID);
                    adapter.AddParameterToSelectCommand("LightsType", LightsType);
                    adapter.Fill(dtGetData);
                }

                // 沒有任何資料時直接回傳 false（不額滿）
                //if (dtGetData.Rows.Count == 0)
                //    return false;

                string lightsStr = string.Empty;
                string msg = string.Empty;

                // 建立簡訊內容
                if (dtGetData.Rows.Count > 0)
                {
                    // 取得宮廟名稱與燈種名稱
                    lightsStr = dtGetData.Rows[0]["LightsString"] == DBNull.Value ? "取不到服務項目" : dtGetData.Rows[0]["LightsString"].ToString();

                    int adminid = 0;
                    if (int.TryParse(AdminID, out adminid))
                    {
                        AdminDAC objAdminDAC = new AdminDAC(_basePage);
                        DataTable dtadminInfo = objAdminDAC.GetAdminInfo(adminid);
                        if (dtadminInfo.Rows.Count > 0)
                            msg = dtadminInfo.Rows[0]["Nickname"].ToString() + " " + lightsStr + " 快額滿了。";
                    }
                }

                // 取得該宮廟設定的燈種上限、檢查開關與下架狀態
                int totalLimit = 0;
                bool needCheck = true;
                bool isForceClosed = false;
                GetLightLimit(AdminID, LightsType, ref totalLimit, ref needCheck, ref isForceClosed);

                // ⛔ 若已強制下架，不論資料數量，直接視為額滿
                if (isForceClosed)
                    return true;

                // ✅ 若需要檢查容量
                if (needCheck && totalLimit > 0)
                {
                    if (dtGetData.Rows.Count >= totalLimit || dtGetData.Rows.Count + Count > totalLimit)
                        result = true;

                    remainingCount = totalLimit - dtGetData.Rows.Count;
                }

                // ⚠️ 若剩餘數低於 50，發送預警簡訊
                if (remainingCount > 0 && remainingCount <= 50 && needCheck)
                {
                    try
                    {
                        SMSHepler objSMSHepler = new SMSHepler();
                        objSMSHepler.SendMsg_SL("0934315020", msg);
                    }
                    catch (Exception error)
                    {
                        string detailedError = ErrorLogger.FormatError(error, typeof(TempleHelper).FullName);
                        _basePage.SaveErrorLog("[TempleHelper SMS Error]：\r\n" + detailedError);
                    }
                }
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(TempleHelper).FullName);
                _basePage.SaveErrorLog("[CheckLightsCapacity ERROR]：\r\n" + detailedError);
            }

            return result;
        }
        #endregion

        #region 共用：取得 TempleCode 對應表
        /// <summary>
        /// 根據 AdminID 取得宮廟對應代碼（TempleCode）。
        /// </summary>
        /// <param name="adminId">宮廟 AdminID。</param>
        /// <returns>對應之 TempleCode，若無則回傳空字串。</returns>
        private string GetTempleCode(string adminId)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("3", "da");
            map.Add("4", "h");
            map.Add("6", "wu");
            map.Add("8", "Fu");
            map.Add("10", "Luer");
            map.Add("14", "ty");
            map.Add("15", "Fw");
            map.Add("16", "dh");
            map.Add("21", "Lk");
            map.Add("23", "ma");
            map.Add("29", "jb");
            map.Add("31", "wjsan");
            map.Add("32", "ld");
            map.Add("33", "sx");
            map.Add("34", "wh");
            map.Add("35", "st");
            map.Add("36", "sl");
            map.Add("37", "nt");
            map.Add("38", "bj");
            map.Add("39", "sbbt");
            map.Add("40", "bpy");
            map.Add("41", "ssy");

            if (map.ContainsKey(adminId))
                return map[adminId];
            else
                return string.Empty;
        }
        #endregion

        #region 共用：取得燈種上限與狀態
        /// <summary>
        /// 取得燈種限制設定，並支援「強制下架」功能。
        /// </summary>
        /// <param name="AdminID">宮廟編號。</param>
        /// <param name="lightsType">燈種代碼。</param>
        /// <param name="totalLimit">最大可用數量（by ref）。</param>
        /// <param name="checkCount">是否檢查容量（by ref）。</param>
        /// <param name="isForceClosed">是否強制關閉該燈種（by ref）。</param>
        private void GetLightLimit(
            string AdminID, 
            string lightsType, 
            ref int totalLimit, 
            ref bool checkCount,
            ref bool isForceClosed)
        {
            totalLimit = 0;
            checkCount = true;
            isForceClosed = false;

            switch (AdminID)
            {
                case "3":
                    // 大甲鎮瀾宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 3500; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 6000; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "4":
                    // 新港奉天宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "6":
                    // 北港武德宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 財神燈
                        case "6": totalLimit = 1000; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "8":
                    // 西螺福興宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財神燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 藥師佛燈
                        case "8": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 觀音佛祖燈
                        case "24": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "10":
                    // 台南正統鹿耳門聖母廟
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 姻緣燈
                        case "7": totalLimit = 9999; checkCount = true; isForceClosed= true; break;
                        // 財利燈
                        case "9": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 福壽燈
                        case "11": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 寵物平安燈
                        case "12": totalLimit = 9999; checkCount = false; isForceClosed= true; break;
                        // 月老姻緣燈
                        case "20": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "14":
                    // 桃園威天宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 6000; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 3000; checkCount = true; isForceClosed= false; break;
                        // 財神燈
                        case "6": totalLimit = 3000; checkCount = true; isForceClosed= false; break;
                        // 藥師燈
                        case "8": totalLimit = 300; checkCount = true; isForceClosed= false; break;
                        // 貴人燈
                        case "10": totalLimit = 1000; checkCount = true; isForceClosed= false; break;
                        // 福祿燈
                        case "11": totalLimit = 300; checkCount = true; isForceClosed= false; break;
                        // 智慧燈
                        case "33": totalLimit = 3000; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "15":
                    // 斗六五路財神宮
                    switch (lightsType)
                    {
                        // 貴人燈(光明燈)
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 發財燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 月老桃花燈
                        case "7": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 消災延壽燈
                        case "8": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 寵物平安燈
                        case "12": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財庫燈
                        case "19": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "16":
                    // 台東東海龍門天聖宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財利燈
                        case "9": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 龍王燈
                        case "13": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 虎爺燈
                        case "14": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "21":
                    // 鹿港城隍廟
                    switch (lightsType)
                    {
                        // 元神光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 太歲平安符
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文魁智慧燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 正財福報燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 轉運納福燈
                        case "15": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 光明燈上層
                        case "16": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 偏財旺旺燈
                        case "17": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 廣進安財庫
                        case "18": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "23":
                    // 玉敕大樹朝天宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 350; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 250; checkCount = true; isForceClosed= false; break;
                        // 五文昌燈
                        case "5": totalLimit = 300; checkCount = true; isForceClosed= false; break;
                        // 福財燈
                        case "6": totalLimit = 250; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "29":
                    // 進寶財神廟
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財利燈
                        case "9": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 貴人燈
                        case "10": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 寵物平安燈
                        case "12": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "31":
                    // 台灣道教總廟無極三清總道院
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財神燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 藥師燈
                        case "8": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 事業燈
                        case "22": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 全家光明燈
                        case "23": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "32":
                    // 桃園龍德宮
                    switch (lightsType)
                    {
                        // 元辰光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌功名燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 五路財神燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 健康延壽燈
                        case "8": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 月老姻緣燈
                        case "20": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 明心智慧燈
                        case "33": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "34":
                    // 基隆悟玄宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed = false; break;
                        // 寵物平安燈
                        case "12": totalLimit = 9999; checkCount = true; isForceClosed = false; break;
                        // 光明燈+批流年
                        case "50": totalLimit = 9999; checkCount = true; isForceClosed = false; break;
                        // 文昌燈+批流年
                        case "51": totalLimit = 9999; checkCount = true; isForceClosed = false; break;
                    }
                    break;
                case "35":
                    // 松柏嶺受天宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安奉太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財神燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "38":
                    // 北極玄天宮
                    switch (lightsType)
                    {
                        // 平安燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "39":
                    // 慈惠石壁部堂
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 1500; checkCount = true; isForceClosed= false; break;
                        // 太歲燈
                        case "4": totalLimit = 1000; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 特別健康燈
                        case "8": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 特別貴人燈
                        case "10": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 特別事業登
                        case "22": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 求子燈
                        case "44": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                        // 護子燈
                        case "45": totalLimit = 500; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "40":
                    // 真武山受玄宮
                    switch (lightsType)
                    {
                        // 光明燈
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 財利燈
                        case "9": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                    }
                    break;
                case "41":
                    // 壽山巖觀音寺
                    switch (lightsType)
                    {
                        // 平安燈(光明燈)
                        case "3": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太歲
                        case "4": totalLimit = 165; checkCount = true; isForceClosed= false; break;
                        // 文昌燈
                        case "5": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 招財燈
                        case "6": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 姻緣燈
                        case "7": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 貴人燈
                        case "10": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 元辰燈
                        case "43": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 添丁燈
                        case "46": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 婚姻燈
                        case "47": totalLimit = 9999; checkCount = true; isForceClosed= false; break;
                        // 安太陰
                        case "48": totalLimit = 165; checkCount = true; isForceClosed= false; break;
                        // 安太陽
                        case "49": totalLimit = 165; checkCount = true; isForceClosed= false; break;
                    }
                    break;
            }
        }
        #endregion

        #region 自動紀錄：每日燈種剩餘量紀錄
        /// <summary>
        /// 每日自動執行：紀錄所有啟用宮廟的燈種剩餘量。
        /// </summary>
        /// <param name="year">年度字串（例如 "2025"）。</param>
        /// <param name="_basePage">基底頁面，用於存取 DB 來源與錯誤記錄。</param>
        /// <returns>成功回傳 true，若有錯誤則回傳 false。</returns>
        public bool LogDailyLightsCapacity(string year, BasePage _basePage)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            bool success = false;
            string logDate = dtNow.ToString("yyyy-MM-dd");
            string logFileName = $"LightsCapacityLog_{logDate}.txt";
            string logFolder = HttpContext.Current.Server.MapPath("~/Log/LightNum/");
            string logFilePath = Path.Combine(logFolder, logFileName);

            try
            {
                // 確保資料夾存在
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                // 1️⃣ 取得所有啟用中的宮廟
                string sqlTemple = "SELECT AdminID, Nickname FROM view_TempleInfo WHERE Status = 0 ORDER BY Sort";
                DataTable dtTemple = new DataTable();

                using (DatabaseAdapter adapterTemple = new DatabaseAdapter(sqlTemple, this.DBSource))
                {
                    adapterTemple.Fill(dtTemple);
                }

                if (dtTemple.Rows.Count == 0)
                    return false;

                // 2️⃣ 開始建立日誌內容
                using (StreamWriter writer = new StreamWriter(logFilePath, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("===========================================");
                    writer.WriteLine($"【每日燈種容量紀錄】執行時間：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"年度：{year}");
                    writer.WriteLine("===========================================");

                    foreach (DataRow temple in dtTemple.Rows)
                    {
                        string adminId = temple["AdminID"].ToString();
                        string nickname = temple["Nickname"].ToString();
                        string templeCode = GetTempleCode(adminId);

                        if (string.IsNullOrEmpty(templeCode))
                            continue;

                        writer.WriteLine($"\r\n--- {nickname} (AdminID={adminId}, Code={templeCode}) ---");

                        try
                        {
                            // 查詢該宮廟所有燈種數量
                            string sqlLights = $"SELECT LightsType, LightsString, COUNT(*) AS CurrentCount " +
                                               $"FROM Temple_{year}..view_Lights_{templeCode}_infowithAPPCharge " +
                                               $"WHERE AppStatus = 2 AND Num > 0 AND AppcStatus = 1 " +
                                               $"GROUP BY LightsType, LightsString ORDER BY LightsType";

                            DataTable dtLights = new DataTable();
                            using (DatabaseAdapter adapter = new DatabaseAdapter(sqlLights, this.DBSource))
                            {
                                adapter.Fill(dtLights);
                            }

                            if (dtLights.Rows.Count == 0)
                            {
                                writer.WriteLine("　(無資料)");
                                continue;
                            }

                            foreach (DataRow row in dtLights.Rows)
                            {
                                string lightsType = row["LightsType"].ToString();
                                string lightsName = row["LightsString"].ToString();
                                int currentCount = Convert.ToInt32(row["CurrentCount"]);

                                // 取得該燈種限制與狀態
                                int totalLimit = 0;
                                bool checkCount = true;
                                bool isForceClosed = false;
                                GetLightLimit(adminId, lightsType, ref totalLimit, ref checkCount, ref isForceClosed);

                                string statusNote;
                                if (isForceClosed)
                                    statusNote = "⛔ 已下架";
                                else if (!checkCount)
                                    statusNote = "🔸 暫不檢查";
                                else
                                {
                                    int remaining = totalLimit - currentCount;
                                    if (remaining <= 50)
                                        statusNote = $"⚠️ 即將額滿 (剩 {remaining})";
                                    else
                                        statusNote = $"✅ 可用 (剩 {remaining})";
                                }

                                writer.WriteLine($"　[{lightsType}] {lightsName}：{currentCount}/{totalLimit}　{statusNote}");
                            }
                        }
                        catch (Exception innerEx)
                        {
                            string errDetail = ErrorLogger.FormatError(innerEx, typeof(TempleHelper).FullName);
                            _basePage.SaveErrorLog($"[LogDailyLightsCapacity temple loop error] {errDetail}");
                            writer.WriteLine($"　❌ 錯誤：{innerEx.Message}");
                        }
                    }

                    writer.WriteLine("\r\n");
                    success = true;
                }
            }
            catch (Exception ex)
            {
                string detailedError = ErrorLogger.FormatError(ex, typeof(TempleHelper).FullName);
                _basePage.SaveErrorLog("[LogDailyLightsCapacity ERROR]：\r\n" + detailedError);
            }

            return success;
        }
        #endregion
    }
}