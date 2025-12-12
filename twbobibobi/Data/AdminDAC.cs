/***************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：AdminDAC.cs
 * 類別說明：後台管理者資料存取層，提供管理員帳號查詢、新增、修改、刪除與系統設定操作
 * 建立日期：2025-11-14
 * 建立人員：Rooney
 * 修改記錄：2025-11-14 全面改用 DatabaseAdapter.ExecuteSql / ExecuteScalar，移除 Fill + Update；依新版 DatabaseAdapter 重構，加入錯誤記錄與分區結構
 * 目前維護人員：Rooney
 ***************************************************************************************************/

using System;
using System.Data;
using Temple.data;
using Temple.FET.APITEST;
using twbobibobi.Helpers;

namespace twbobibobi.Data
{
    /// <summary>
    /// AdminDAC：負責管理者資料的新增、修改、查詢與登入驗證邏輯。
    /// </summary>
    public class AdminDAC : SqlClientBase
    {
        /// <summary>
        /// 建構子，繼承自 <see cref="SqlClientBase"/>，用於前端頁面注入資料庫連線來源。
        /// </summary>
        /// <param name="basePage">目前執行的 BasePage 實例，其內含資料庫連線物件 DBSource。</param>
        public AdminDAC(BasePage basePage) : base(basePage) { }


        //==================================================
        #region 共用(Common)
        //==================================================

        /// <summary>
        /// 取得指定系統設定項目值。
        /// </summary>
        /// <param name="paramName">設定項目名稱。</param>
        /// <returns>設定值字串。</returns>
        public static string GetConfigValue(string paramName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[paramName];
            }
            catch (System.Configuration.ConfigurationErrorsException error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetConfigValue：\r\n" + detailedError);
                return string.Empty;
            }
        }

        /// <summary>
        /// 共用方法：依活動種類刪除對應申請資料。
        /// </summary>
        /// <param name="applicantId">購買人主鍵編號。</param>
        /// <param name="adminId">管理員編號(宮廟編號)。</param>
        /// <param name="kind">
        /// 活動種類：
        /// 1-點燈 
        /// 2-普度 
        /// 4-下元補庫 
        /// 5-呈疏補庫(天官武財神聖誕補財庫) 
        /// 6-企業補財庫 
        /// 7-天赦日補運 
        /// 8-天赦日祭改 
        /// 9-關聖帝君聖誕 
        /// 10-代燒金紙 
        /// 11-天貺納福添運法會 
        /// 12-靈寶禮斗 
        /// 13-七朝清醮 
        /// 14-九九重陽天赦日補運 
        /// 15-護國息災梁皇大法會 
        /// 16-補財庫 
        /// 17-赦罪補庫 
        /// 18-天公生招財補運 
        /// 19-供香轉運 
        /// 20-安斗 
        /// 21-供花供果 
        /// 22-孝親祈福燈 
        /// 23-祈安植福
        /// 24-祈安禮斗
        /// 25-千手觀音千燈迎佛法會
        /// 26-組合商品
        /// 27-新春賀歲感恩招財祿位
        /// <param name="type">類型，用於部分分支（例如桃園威天宮燈別）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>更新與刪除皆成功回傳 true，否則 false。</returns>
        private bool DeleteLinkedApplicantData(
            int applicantId, 
            string adminId, 
            string kind, 
            int type, 
            string year)
        {
            bool bResult = false;
            switch (kind)
            {
                case "1": bResult = DeleteLightsInfo(applicantId, adminId, type, year); break;
                case "2": bResult = DeletePurdueInfo(applicantId, adminId, year); break;
                case "3": bResult = DeleteProductInfo(applicantId, adminId, year); break;
                case "4": bResult = DeleteSuppliesInfo(applicantId, adminId, 1, year); break;
                case "5": bResult = DeleteSuppliesInfo(applicantId, adminId, 2, year); break;
                case "6": bResult = DeleteSuppliesInfo(applicantId, adminId, 3, year); break;
                case "7": bResult = DeleteSuppliesInfo(applicantId, adminId, 4, year); break;
                case "8": bResult = DeleteSuppliesInfo(applicantId, adminId, 5, year); break;
                case "9": bResult = DeleteEmperorGuanshengInfo(applicantId, adminId, year); break;
                case "10": bResult = DeleteBPOInfo(applicantId, adminId, year); break;
                case "12": bResult = DeleteLingbaolidouInfo(applicantId, adminId, year); break;
                case "13": bResult = DeleteTaoistJiaoCeremonyInfo(applicantId, adminId, year); break;
                case "16": bResult = DeleteSuppliesInfo(applicantId, adminId, 9, year); break;
                case "18": bResult = DeleteSuppliesInfo(applicantId, adminId, 18, year); break;
                case "20": bResult = DeleteAnDouInfo(applicantId, adminId, year); break;
                case "21": bResult = DeleteHuaguoInfo(applicantId, adminId, year); break;
                case "22": bResult = DeleteLightsInfo(applicantId, adminId, 2, year); break;
                case "23": bResult = DeleteBlessingInfo(applicantId, adminId, year); break;
                case "24": bResult = DeleteSuppliesInfo(applicantId, adminId, 24, year); break;
                case "25": bResult = DeleteQnLightInfo(applicantId, adminId, year); break;
                case "27": bResult = DeleteLuckaltarInfo(applicantId, adminId, year); break;
            }
            return bResult;
        }

        /// <summary>
        /// 將宮廟 Content 欄位進行解碼與樣式優化。
        /// Decode temple HTML content and adjust table style.
        /// </summary>
        /// <param name="dtData">包含 Content 欄位的 DataTable。</param>
        private void AddDecodedTempleContent(DataTable dtData)
        {
            if (!dtData.Columns.Contains("Content1"))
                dtData.Columns.Add("Content1", typeof(string));

            foreach (DataRow row in dtData.Rows)
            {
                if (row["Content"] != DBNull.Value)
                {
                    string content = System.Text.UnicodeEncoding.Unicode.GetString((byte[])row["Content"]);

                    // 修正 <table> 標籤樣式
                    content = System.Text.RegularExpressions.Regex.Replace(
                        content,
                        "<table",
                        "<table style=\"max-width:100%; height:auto\"",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    );

                    // 修正 width 屬性
                    string valEx = @"width=""\d+""";
                    content = System.Text.RegularExpressions.Regex.Replace(
                        content,
                        valEx,
                        "width=\"640px\" height=\"auto\"",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    );

                    row["Content1"] = content;
                }
            }
        }

        #endregion


        //==================================================
        #region 建立(Create)
        //==================================================


        #endregion


        //==================================================
        #region 查詢(Select)
        //==================================================

        /// <summary>
        /// 取得管理員列表（分頁版本）。
        /// </summary>
        /// <param name="PageIndex">頁索引（從 1 起算）。</param>
        /// <param name="PageSize">每頁筆數。</param>
        /// <param name="permission">權限等級。</param>
        /// <param name="adminID">管理員編號(宮廟編號)。</param>
        /// <returns>管理員資料表。</returns>
        /// <summary>
        public DataTable GetAdminList(
            int PageIndex, 
            int PageSize, 
            int permission, 
            int adminID)
        {
            try
            {
                string sql = "SELECT * FROM Admin";
                if (permission != 0)
                {
                    sql += " WHERE AdminID = @AdminID OR UpperAdminID = @UpperAdminID";
                }

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    if (permission != 0)
                    {
                        adapter.AddParameterToSelectCommand("@AdminID", adminID);
                        adapter.AddParameterToSelectCommand("@UpperAdminID", adminID);
                    }

                    // 建立 DataSet 接收分頁結果
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, (PageIndex - 1) * PageSize, PageSize, "dtGetData");
                    return ds.Tables["dtGetData"];
                }
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetAdminList(PageIndex)：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 取得管理員列表（依權限等級）。
        /// </summary>
        /// <param name="permission">權限等級。</param>
        /// <returns>管理員資料表。</returns>
        public DataTable GetAdminList(int permission)
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE Permission = @Permission";

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.AddParameterToSelectCommand("@Permission", permission);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetAdminList(permission)：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 取得所有正常（Status = 0）的管理員列表。
        /// </summary>
        /// <returns>管理員資料表。</returns>
        public DataTable GetAdminList()
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE Status = 0";

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetAdminList()：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 取得全部宮廟資訊 (Get all temple info)
        /// </summary>
        /// <returns>回傳包含宮廟資料的 DataTable。</returns>
        public DataTable GetTempleInfo()
        {
            try
            {
                DataTable dtData = new DataTable();
                string sql = "SELECT * FROM view_TempleInfo WHERE Status = 0 ORDER BY Sort";

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.Fill(dtData);
                }

                // 若有資料，解碼 Content 欄位
                if (dtData.Rows.Count > 0)
                {
                    AddDecodedTempleContent(dtData);
                }

                return dtData;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetTempleInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 依據指定宮廟編號取得宮廟內容 (Get temple info by AdminID)
        /// </summary>
        /// <param name="AdminID">宮廟編號（例如：3=大甲鎮瀾宮、4=新港奉天宮）。</param>
        /// <returns>回傳包含指定宮廟資料的 DataTable。</returns>
        public DataTable GetTempleInfo(string AdminID)
        {
            try
            {
                DataTable dtData = new DataTable();
                string sql = "SELECT * FROM view_TempleInfo WHERE AdminID=@AdminID AND Status=0";

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.AddParameterToSelectCommand("@AdminID", AdminID);
                    adapter.Fill(dtData);
                }

                if (dtData.Rows.Count > 0)
                {
                    AddDecodedTempleContent(dtData);
                }

                return dtData;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetTempleInfo(AdminID)：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 取得管理員資訊 (Get administrator info by username and password)
        /// </summary>
        /// <param name="UserName">管理員帳號。</param>
        /// <param name="Password">管理員密碼。</param>
        /// <returns>回傳符合條件的管理員資料表。</returns>
        public DataTable GetAdminInfo(string UserName, string Password)
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE UserName=@UserName AND Password=@Password AND Status=0";
                DataTable dtGetData = new DataTable();

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.AddParameterToSelectCommand("@UserName", UserName);
                    adapter.AddParameterToSelectCommand("@Password", Password);
                    adapter.Fill(dtGetData);
                }

                return dtGetData;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetAdminInfo(UserName, Password)：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 取得管理員資訊 (Get administrator info by AdminID)
        /// </summary>
        /// <param name="AdminID">管理員編號。</param>
        /// <returns>回傳符合管理員編號的資料表。</returns>
        public DataTable GetAdminInfo(int AdminID)
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE AdminID=@AdminID AND Status=0";
                DataTable dtGetData = new DataTable();

                using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                {
                    adapter.AddParameterToSelectCommand("@AdminID", AdminID);
                    adapter.Fill(dtGetData);
                }

                return dtGetData;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.GetAdminInfo(AdminID)：\r\n" + detailedError);
                throw;
            }
        }


        #endregion


        //==================================================
        #region 更新(Update)
        //==================================================

        /// <summary>
        /// 更新管理員資訊。
        /// </summary>
        public int UpdateAdmin(
            string adminId, 
            string Username, 
            string Password, 
            string Nickname, 
            string Permission,
            int status)
        {
            string sql = @"
                UPDATE Admin 
                SET Username=@Username, Password=@Password, Nickname=@Nickname, Permission=@Permission, Status=@Status 
                WHERE AdminID=@AdminID";

            using (var adapter = new DatabaseAdapter(sql, this.DBSource))
            {
                adapter.AddParameterToSelectCommand("@AdminID", adminId);
                adapter.AddParameterToSelectCommand("@Username", Username);
                adapter.AddParameterToSelectCommand("@Password", Password);
                adapter.AddParameterToSelectCommand("@Nickname", Nickname);
                adapter.AddParameterToSelectCommand("@Permission", Permission);
                adapter.AddParameterToSelectCommand("@Status", status);
                return adapter.ExecuteSql();
            }
        }

        /// <summary>
        /// 更新 APPCharge 狀態，並依據活動種類（kind）同步刪除對應申請人資料。
        /// </summary>
        /// <param name="UniqueID">對應 APPCharge 表的主鍵編號。</param>
        /// <param name="Status">要更新的狀態值。</param>
        /// <param name="AdminID">管理員編號(宮廟編號)。</param>
        /// <param name="kind">活動種類： 
        /// 1-點燈 
        /// 2-普度 
        /// 4-下元補庫 
        /// 5-呈疏補庫(天官武財神聖誕補財庫) 
        /// 6-企業補財庫 
        /// 7-天赦日補運 
        /// 8-天赦日祭改 
        /// 9-關聖帝君聖誕 
        /// 10-代燒金紙 
        /// 11-天貺納福添運法會 
        /// 12-靈寶禮斗 
        /// 13-七朝清醮 
        /// 14-九九重陽天赦日補運 
        /// 15-護國息災梁皇大法會 
        /// 16-補財庫 
        /// 17-赦罪補庫 
        /// 18-天公生招財補運 
        /// 19-供香轉運 
        /// 20-安斗 
        /// 21-供花供果 
        /// 22-孝親祈福燈 
        /// 23-祈安植福
        /// 24-祈安禮斗
        /// 25-千手觀音千燈迎佛法會
        /// </param>
        /// <param name="type">類型，用於部分分支（例如桃園威天宮燈別）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>更新與刪除皆成功回傳 true，否則 false。</returns>
        public bool Updatestatus2appcharge(
            int UniqueID, 
            int Status, 
            string AdminID, 
            string kind, 
            int type, 
            string Year)
        {
            bool result = false;
            try
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtDataList = new DataTable();

                //===============================================
                // 各廟別、活動類別分支
                //===============================================
                switch (kind)
                {
                    case "1":
                        // 點燈
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..APPCharge_da_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "4":
                                //新港奉天宮
                                view = $"Temple_{Year}..APPCharge_h_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..APPCharge_wu_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "8":
                                //西螺福興宮
                                view = $"Temple_{Year}..APPCharge_Fu_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "10":
                                //台南正統鹿耳門聖母廟
                                view = $"Temple_{Year}..APPCharge_Luer_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "14":
                                //桃園威天宮
                                if (type == 2)
                                    view = $"Temple_{Year}..APPCharge_ty_mom_Lights";
                                else
                                    view = $"Temple_{Year}..APPCharge_ty_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..APPCharge_Fw_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "16":
                                //台東東海龍門天聖宮
                                view = $"Temple_{Year}..APPCharge_dh_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..APPCharge_Lk_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..APPCharge_ma_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..APPCharge_jb_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..APPCharge_wjsan_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "32":
                                //桃園龍德宮
                                view = $"Temple_{Year}..APPCharge_ld_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..APPCharge_wh_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "35":
                                //松柏嶺受天宮
                                view = $"Temple_{Year}..APPCharge_st_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "38":
                                //池上北極玄天宮
                                view = $"Temple_{Year}..APPCharge_bj_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "39":
                                //慈惠石壁部堂
                                view = $"Temple_{Year}..APPCharge_sbbt_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "40":
                                //真武山受玄宮
                                view = $"Temple_{Year}..APPCharge_bpy_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "41":
                                //壽山巖觀音寺
                                view = $"Temple_{Year}..APPCharge_ssy_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "2":
                        // 普度
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..APPCharge_da_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "4":
                                //新港奉天宮
                                view = $"Temple_{Year}..APPCharge_h_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..APPCharge_wu_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "8":
                                //西螺福興宮
                                view = $"Temple_{Year}..APPCharge_Fu_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "10":
                                //台南正統鹿耳門聖母廟
                                view = $"Temple_{Year}..APPCharge_Luer_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..APPCharge_Fw_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "16":
                                //台東東海龍門天聖宮
                                view = $"Temple_{Year}..APPCharge_dh_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..APPCharge_Lk_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..APPCharge_ma_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..APPCharge_jb_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..APPCharge_wjsan_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "32":
                                //桃園龍德宮
                                view = $"Temple_{Year}..APPCharge_ld_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..APPCharge_wh_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "3":
                        //商品小舖
                        Year = dtNow.Year.ToString();
                        switch (AdminID)
                        {
                            case "5":
                                //文創商品-新港奉天宮
                                view = $"Temple_{Year}..APPCharge_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "7":
                                //大甲鎮瀾宮繞境商品小舖
                                view = $"Temple_{Year}..APPCharge_Pilgrimage";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "11":
                                //新港奉天宮錢母商品小舖
                                view = $"Temple_{Year}..APPCharge_Moneymother";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "20":
                                //文創商品-西螺福興宮
                                view = $"Temple_{Year}..APPCharge_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "22":
                                //流金富貴商品小舖
                                view = $"Temple_{Year}..APPCharge_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "28":
                                //財神小舖商品小舖
                                view = $"Temple_{Year}..APPCharge_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "4":
                        //下元補庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..APPCharge_wu_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "5":
                        //呈疏補庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..APPCharge_wu_Supplies2";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "6":
                        //企業補財庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..APPCharge_wu_Supplies3";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "7":
                        //天赦日補運
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "23":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ma_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "8":
                        //天赦日祭改
                        switch (AdminID)
                        {
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..APPCharge_jb_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "9":
                        //關帝聖君聖誕
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_EmperorGuansheng";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "10":
                        //代燒金紙
                        switch (AdminID)
                        {
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..APPCharge_jb_BPO";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "12":
                        //靈寶禮斗
                        switch (AdminID)
                        {
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..APPCharge_ma_Lingbaolidou";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "13":
                        //七朝清醮
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..APPCharge_da_TaoistJiaoCeremony";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "16":
                        //補財庫
                        switch (AdminID)
                        {
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..APPCharge_Fw_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..APPCharge_Lk_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "17":
                        //赦罪補庫
                        switch (AdminID)
                        {
                            case "33":
                                //神霄玉府財神會館
                                view = $"Temple_{Year}..APPCharge_sx_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "18":
                        //招財補運
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_Supplies3";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "20":
                        //安斗
                        switch (AdminID)
                        {
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..APPCharge_Fw_AnDou";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..APPCharge_wjsan_AnDou";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "21":
                        //供花供果
                        switch (AdminID)
                        {
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..APPCharge_wjsan_Huaguo";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "22":
                        //孝親祈福燈
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_mom_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "23":
                        //祈安植福
                        switch (AdminID)
                        {
                            case "35":
                                //松柏嶺受天宮
                                view = $"Temple_{Year}..APPCharge_st_Blessing";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "24":
                        //祈安禮斗
                        switch (AdminID)
                        {
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..APPCharge_wh_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "25":
                        //千手觀音千燈迎佛法會
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_QnLight";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                    case "27":
                        //新春賀歲感恩招財祿位
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..APPCharge_ty_Luckaltar";
                                sql = $"SELECT * FROM {view} WHERE Status=1 AND UniqueID=@UniqueID";
                                break;
                        }
                        break;
                }

                //===============================================
                // 讀取 APPCharge 資料
                //===============================================
                if (!string.IsNullOrEmpty(sql))
                {
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@UniqueID", UniqueID);
                        adapter.Fill(dtDataList);
                    }

                    if (dtDataList.Rows.Count > 0 && Convert.ToInt32(dtDataList.Rows[0]["Status"]) == 1)
                    {
                        if (!string.IsNullOrEmpty(view))
                        {
                            // 使用參數化 SQL 更新 Status
                            string updateSql = $"UPDATE {view} SET Status=@Status WHERE UniqueID=@UniqueID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Status", Status);
                                updateAdapter.AddParameterToSelectCommand("@UniqueID", UniqueID);
                                int res = updateAdapter.ExecuteSql();

                                if (res > 0)
                                {
                                    int applicantId = Convert.ToInt32(dtDataList.Rows[0]["ApplicantID"]);
                                    if (Updatestatus2applicantInfo(applicantId, Status, AdminID, kind, type, Year))
                                    {
                                        // 刪除對應資料
                                        result = DeleteLinkedApplicantData(applicantId, AdminID, kind, type, Year);
                                    }
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.Updatestatus2appcharge：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 更新 ApplicantInfo 狀態（由 APPCharge 同步呼叫）。
        /// </summary>
        /// <param name="ApplicantID">購買人主鍵編號。</param>
        /// <param name="Status">要更新的狀態值。</param>
        /// <param name="AdminID">管理員編號(宮廟編號)。</param>
        /// <param name="kind">活動種類： 
        /// 1-點燈 
        /// 2-普度 
        /// 4-下元補庫 
        /// 5-呈疏補庫(天官武財神聖誕補財庫) 
        /// 6-企業補財庫 
        /// 7-天赦日補運 
        /// 8-天赦日祭改 
        /// 9-關聖帝君聖誕 
        /// 10-代燒金紙 
        /// 11-天貺納福添運法會 
        /// 12-靈寶禮斗 
        /// 13-七朝清醮 
        /// 14-九九重陽天赦日補運 
        /// 15-護國息災梁皇大法會 
        /// 16-補財庫 
        /// 17-赦罪補庫 
        /// 18-天公生招財補運 
        /// 19-供香轉運 
        /// 20-安斗 
        /// 21-供花供果 
        /// 22-孝親祈福燈 
        /// 23-祈安植福
        /// 24-祈安禮斗
        /// 25-千手觀音千燈迎佛法會
        /// </param>
        /// <param name="type">類別參數，用於特例（例如桃園威天宮的燈別）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>更新成功回傳 true，否則 false。</returns>
        public bool Updatestatus2applicantInfo(
            int ApplicantID, 
            int Status, 
            string AdminID, 
            string kind, 
            int type, 
            string Year)
        {
            bool result = false;
            try
            {
                TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtDataList = new DataTable();

                //===============================================
                // 根據 kind + AdminID 決定 ApplicantInfo 目標資料表
                //===============================================
                switch (kind)
                {
                    case "1":
                        // 點燈
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..ApplicantInfo_da_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "4":
                                //新港奉天宮
                                view = $"Temple_{Year}..ApplicantInfo_h_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..ApplicantInfo_wu_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "8":
                                //西螺福興宮
                                view = $"Temple_{Year}..ApplicantInfo_Fu_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "10":
                                //台南正統鹿耳門聖母廟
                                view = $"Temple_{Year}..ApplicantInfo_Luer_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "14":
                                //桃園威天宮
                                if (type == 2)
                                    view = $"Temple_{Year}..ApplicantInfo_ty_mom_Lights";
                                else
                                    view = $"Temple_{Year}..ApplicantInfo_ty_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..ApplicantInfo_Fw_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "16":
                                //台東東海龍門天聖宮
                                view = $"Temple_{Year}..ApplicantInfo_dh_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..ApplicantInfo_Lk_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..ApplicantInfo_ma_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..ApplicantInfo_jb_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..ApplicantInfo_wjsan_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "32":
                                //桃園龍德宮
                                view = $"Temple_{Year}..ApplicantInfo_ld_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..ApplicantInfo_wh_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "35":
                                //松柏嶺受天宮
                                view = $"Temple_{Year}..ApplicantInfo_st_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "38":
                                //池上北極玄天宮
                                view = $"Temple_{Year}..ApplicantInfo_bj_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "39":
                                //慈惠石壁部堂
                                view = $"Temple_{Year}..ApplicantInfo_sbbt_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "40":
                                //真武山受玄宮
                                view = $"Temple_{Year}..ApplicantInfo_bpy_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "41":
                                //壽山巖觀音寺
                                view = $"Temple_{Year}..ApplicantInfo_ssy_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "2":
                        // 普度
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..ApplicantInfo_da_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "4":
                                //新港奉天宮
                                view = $"Temple_{Year}..ApplicantInfo_h_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..ApplicantInfo_wu_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "8":
                                //西螺福興宮
                                view = $"Temple_{Year}..ApplicantInfo_Fu_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "10":
                                //台南正統鹿耳門聖母廟
                                view = $"Temple_{Year}..ApplicantInfo_Luer_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..ApplicantInfo_Fw_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "16":
                                //台東東海龍門天聖宮
                                view = $"Temple_{Year}..ApplicantInfo_dh_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..ApplicantInfo_Lk_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..ApplicantInfo_ma_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..ApplicantInfo_jb_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..ApplicantInfo_wjsan_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "32":
                                //桃園龍德宮
                                view = $"Temple_{Year}..ApplicantInfo_ld_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..ApplicantInfo_wh_Purdue";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "3":
                        //商品小舖
                        Year = dtNow.Year.ToString();
                        switch (AdminID)
                        {
                            case "5":
                                //文創商品-新港奉天宮
                                view = $"Temple_{Year}..ApplicantInfo_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "7":
                                //大甲鎮瀾宮繞境商品小舖
                                view = $"Temple_{Year}..ApplicantInfo_Pilgrimage";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "11":
                                //新港奉天宮錢母商品小舖
                                view = $"Temple_{Year}..ApplicantInfo_Moneymother";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "20":
                                //文創商品-西螺福興宮
                                view = $"Temple_{Year}..ApplicantInfo_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "22":
                                //流金富貴商品小舖
                                view = $"Temple_{Year}..ApplicantInfo_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "28":
                                //財神小舖商品小舖
                                view = $"Temple_{Year}..ApplicantInfo_Product";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "4":
                        //下元補庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..ApplicantInfo_wu_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "5":
                        //呈疏補庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..ApplicantInfo_wu_Supplies2";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "6":
                        //企業補財庫
                        switch (AdminID)
                        {
                            case "6":
                                //北港武德宮
                                view = $"Temple_{Year}..ApplicantInfo_wu_Supplies3";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "7":
                        //天赦日補運
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "23":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ma_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "8":
                        //天赦日祭改
                        switch (AdminID)
                        {
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..ApplicantInfo_jb_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "9":
                        //關帝聖君聖誕
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_EmperorGuansheng";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "10":
                        //代燒金紙
                        switch (AdminID)
                        {
                            case "29":
                                //進寶財神廟
                                view = $"Temple_{Year}..ApplicantInfo_jb_BPO";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "12":
                        //靈寶禮斗
                        switch (AdminID)
                        {
                            case "23":
                                //玉敕大樹朝天宮
                                view = $"Temple_{Year}..ApplicantInfo_ma_Lingbaolidou";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "13":
                        //七朝清醮
                        switch (AdminID)
                        {
                            case "3":
                                //大甲鎮瀾宮
                                view = $"Temple_{Year}..ApplicantInfo_da_TaoistJiaoCeremony";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "16":
                        //補財庫
                        switch (AdminID)
                        {
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..ApplicantInfo_Fw_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "21":
                                //鹿港城隍廟
                                view = $"Temple_{Year}..ApplicantInfo_Lk_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "17":
                        //赦罪補庫
                        switch (AdminID)
                        {
                            case "33":
                                //神霄玉府財神會館
                                view = $"Temple_{Year}..ApplicantInfo_sx_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "18":
                        //招財補運
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_Supplies3";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "20":
                        //安斗
                        switch (AdminID)
                        {
                            case "15":
                                //斗六五路財神宮
                                view = $"Temple_{Year}..ApplicantInfo_Fw_AnDou";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..ApplicantInfo_wjsan_AnDou";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "21":
                        //供花供果
                        switch (AdminID)
                        {
                            case "31":
                                //台灣道教總廟無極三清總道院
                                view = $"Temple_{Year}..ApplicantInfo_wjsan_Huaguo";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "22":
                        //孝親祈福燈
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_mom_Lights";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "23":
                        //祈安植福
                        switch (AdminID)
                        {
                            case "35":
                                //松柏嶺受天宮
                                view = $"Temple_{Year}..ApplicantInfo_st_Blessing";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "24":
                        //祈安禮斗
                        switch (AdminID)
                        {
                            case "34":
                                //基隆悟玄宮
                                view = $"Temple_{Year}..ApplicantInfo_wh_Supplies";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "25":
                        //千手觀音千燈迎佛法會
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_QnLight";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                    case "27":
                        //新春賀歲感恩招財祿位
                        switch (AdminID)
                        {
                            case "14":
                                //桃園威天宮
                                view = $"Temple_{Year}..ApplicantInfo_ty_Luckaltar";
                                sql = $"SELECT * FROM {view} WHERE Status=2 AND ApplicantID=@ApplicantID";
                                break;
                        }
                        break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    // 讀取現有 ApplicantInfo 狀態
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtDataList);
                    }

                    // 若有資料，執行狀態更新
                    if (dtDataList.Rows.Count > 0)
                    {
                        string updateSql = $@"
                            UPDATE {view}
                            SET Status=@Status,
                                UpdateinfoDate=@UpdateinfoDate,
                                UpdateinfoDateString=@UpdateinfoDateString
                            WHERE ApplicantID=@ApplicantID";

                        using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                        {
                            updateAdapter.AddParameterToSelectCommand("@Status", Status);
                            updateAdapter.AddParameterToSelectCommand("@UpdateinfoDate", dtNow);
                            updateAdapter.AddParameterToSelectCommand("@UpdateinfoDateString", dtNow.ToString("yyyy-MM-dd"));
                            updateAdapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);

                            int res = updateAdapter.ExecuteSql();
                            if (res > 0) result = true;
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.Updatestatus2applicantInfo：\r\n" + detailedError);
                throw;
            }
        }


        #endregion


        //==================================================
        #region 刪除(Delete)
        //==================================================

        /// <summary>
        /// 刪除點燈訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：3=大甲鎮瀾宮、4=新港奉天宮、6=北港武德宮等）。</param>
        /// <param name="type">燈別類型（用於特殊廟別，如桃園威天宮孝親祈福燈）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteLightsInfo(
            int ApplicantID, 
            string AdminID, 
            int type, 
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "3":
                        view = $"Temple_{Year}..Lights_da_info"; break;
                    case "4":
                        view = $"Temple_{Year}..Lights_h_info"; break;
                    case "6":
                        view = $"Temple_{Year}..Lights_wu_info"; break;
                    case "8":
                        view = $"Temple_{Year}..Lights_Fu_info"; break;
                    case "10":
                        view = $"Temple_{Year}..Lights_Luer_info"; break;
                    case "14":
                        view = type == 2
                            ? $"Temple_{Year}..Lights_ty_mom_info"
                            : $"Temple_{Year}..Lights_ty_info";
                        break;
                    case "15":
                        view = $"Temple_{Year}..Lights_Fw_info"; break;
                    case "16":
                        view = $"Temple_{Year}..Lights_dh_info"; break;
                    case "17":
                        view = $"Temple_{Year}..Lights_Hs_info"; break;
                    case "21":
                        view = $"Temple_{Year}..Lights_Lk_info"; break;
                    case "23":
                        view = $"Temple_{Year}..Lights_ma_info"; break;
                    case "29":
                        view = $"Temple_{Year}..Lights_jb_info"; break;
                    case "31":
                        view = $"Temple_{Year}..Lights_wjsan_info"; break;
                    case "32":
                        view = $"Temple_{Year}..Lights_ld_info"; break;
                    case "34":
                        view = $"Temple_{Year}..Lights_wh_info"; break;
                    case "35":
                        view = $"Temple_{Year}..Lights_st_info"; break;
                    case "38":
                        view = $"Temple_{Year}..Lights_bj_info"; break;
                    case "39":
                        view = $"Temple_{Year}..Lights_sbbt_info"; break;
                    case "40":
                        view = $"Temple_{Year}..Lights_bpy_info"; break;
                    case "41":
                        view = $"Temple_{Year}..Lights_ssy_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT LightsID FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int lightsId = Convert.ToInt32(row["LightsID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE LightsID=@LightsID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@LightsID", lightsId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteLightsInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除普度訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：3=大甲鎮瀾宮、4=新港奉天宮、6=北港武德宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeletePurdueInfo(
            int ApplicantID, 
            string AdminID, 
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "3": view = $"Temple_{Year}..Purdue_da_info"; break;
                    case "4": view = $"Temple_{Year}..Purdue_h_info"; break;
                    case "6": view = $"Temple_{Year}..Purdue_wu_info"; break;
                    case "8": view = $"Temple_{Year}..Purdue_Fu_info"; break;
                    case "10": view = $"Temple_{Year}..Purdue_Luer_info"; break;
                    case "14": view = $"Temple_{Year}..Purdue_ty_info"; break;
                    case "15": view = $"Temple_{Year}..Purdue_Fw_info"; break;
                    case "16": view = $"Temple_{Year}..Purdue_dh_info"; break;
                    case "21": view = $"Temple_{Year}..Purdue_Lk_info"; break;
                    case "23": view = $"Temple_{Year}..Purdue_ma_info"; break;
                    case "29": view = $"Temple_{Year}..Purdue_jb_info"; break;
                    case "31": view = $"Temple_{Year}..Purdue_wjsan_info"; break;
                    case "32": view = $"Temple_{Year}..Purdue_ld_info"; break;
                    case "34": view = $"Temple_{Year}..Purdue_wh_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT PurdueID FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應普度記錄
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int purdueId = Convert.ToInt32(row["PurdueID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE PurdueID=@PurdueID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@PurdueID", purdueId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeletePurdueInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除商品小舖訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：5=文創商品、7=繞境商品小舖、11=錢母商品小舖）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteProductInfo(
            int ApplicantID, 
            string AdminID, 
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();
                BasePage basePage = new BasePage();
                LightDAC _lightDAC = new LightDAC(basePage);

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "5":
                        // 文創商品-新港奉天宮
                        view = $"Temple_{Year}..ProductInfo";
                        break;
                    case "7":
                        // 繞境商品小舖
                        view = $"Temple_{Year}..ApplicantInfo_Pilgrimage";
                        break;
                    case "11":
                        // 錢母商品小舖
                        view = $"Temple_{Year}..ApplicantInfo_Moneymother";
                        break;
                    case "20":
                        //文創商品-西螺福興宮
                        view = $"Temple_{Year}..ProductInfo";
                        break;
                    case "22":
                        //流金富貴商品小舖
                        view = $"Temple_{Year}..ProductInfo";
                        break;
                    case "28":
                        //財神小舖商品小舖
                        view = $"Temple_{Year}..ProductInfo";
                        break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應商品記錄
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int buyId = Convert.ToInt32(row["BuyID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE BuyID=@BuyID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@BuyID", buyId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }

                            if (result)
                            {
                                int productID = 0;
                                int.TryParse(row["ProductID"].ToString(), out productID);

                                int typeID = 0;
                                int.TryParse(row["TypeID"].ToString(), out typeID);

                                int count = 0;
                                int.TryParse(row["Count"].ToString(), out count);

                                if (productID > 0)
                                {
                                    if (count > 0)
                                    {
                                        //更新購買數量至商品表or商品類別表
                                        if (!_lightDAC.UpdateCount2Product(
                                        ProductID: productID,
                                        TypeID: typeID,
                                        Count: -count))
                                        {
                                            // 沒有更新到數量表，都當作失敗
                                            throw new InvalidOperationException("更新數量狀態異常");
                                        }
                                    }
                                    else
                                    {
                                        // 沒有更新到數量表，都當作失敗
                                        throw new InvalidOperationException("更新數量狀態異常；取得數量失敗；");
                                    }
                                }
                                else
                                {
                                    // 沒有更新到數量表，都當作失敗
                                    throw new InvalidOperationException("更新數量狀態異常；取得ProductID失敗；");
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteProductInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除法會訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：6=北港武德宮、14=桃園威天宮、15=斗六五路財神宮等）。</param>
        /// <param name="SuppliesType">法會類型（1=下元補庫、2=呈疏補庫、3=企業補財庫、4=天赦日補運、9=補財庫、11=赦罪補庫、18=天公生招財補運）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteSuppliesInfo(
            int ApplicantID, 
            string AdminID, 
            int SuppliesType, 
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID + SuppliesType 決定資料表名稱
                //──────────────────────────────
                switch (AdminID)
                {
                    case "6": // 北港武德宮
                        if (SuppliesType == 1)
                            view = $"Temple_{Year}..Supplies_wu_info";
                        else if (SuppliesType == 2)
                            view = $"Temple_{Year}..Supplies_wu_info2";
                        else if (SuppliesType == 3)
                            view = $"Temple_{Year}..Supplies_wu_info3";
                        break;

                    case "14": // 桃園威天宮
                        if (SuppliesType == 4)
                            view = $"Temple_{Year}..Supplies_ty_info";
                        else if (SuppliesType == 18)
                            view = $"Temple_{Year}..Supplies3_ty_info";
                        break;

                    case "15": // 斗六五路財神宮（補財庫）
                        if (SuppliesType == 9)
                            view = $"Temple_{Year}..Supplies_Fw_info";
                        break;

                    case "21": // 鹿港城隍廟（補財庫）
                        if (SuppliesType == 9)
                            view = $"Temple_{Year}..Supplies_Lk_info";
                        break;

                    case "23": // 玉敕大樹朝天宮（天赦日補運）
                        if (SuppliesType == 4)
                            view = $"Temple_{Year}..Supplies_ma_info";
                        break;

                    case "29": // 進寶財神廟（天赦日祭改）
                        if (SuppliesType == 5)
                            view = $"Temple_{Year}..Supplies_jb_info";
                        break;

                    case "33": // 神霄玉府財神會館（赦罪補庫）
                        if (SuppliesType == 11)
                            view = $"Temple_{Year}..Supplies_sx_info";
                        break;

                    case "34": // 基隆悟玄宮（祈安禮斗）
                        view = $"Temple_{Year}..Supplies_wh_info";
                        break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應補財庫記錄
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int suppliesId = Convert.ToInt32(row["SuppliesID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE SuppliesID=@SuppliesID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@SuppliesID", suppliesId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteSuppliesInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除關帝聖君聖誕訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：14=桃園威天宮）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteEmperorGuanshengInfo(
            int ApplicantID, 
            string AdminID, 
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 決定資料表名稱
                //──────────────────────────────
                switch (AdminID)
                {

                    case "14": 
                        // 桃園威天宮
                        view = $"Temple_{Year}..EmperorGuansheng_ty_info";
                        break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應補財庫記錄
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int EmperorGuanshengID = Convert.ToInt32(row["EmperorGuanshengID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE EmperorGuanshengID=@EmperorGuanshengID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@EmperorGuanshengID", EmperorGuanshengID);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteEmperorGuanshengInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除代燒金紙訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：29=進寶財神廟等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteBPOInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "29":
                        view = $"Temple_{Year}..BPO_jb_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int bpoId = Convert.ToInt32(row["BPOID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE BPOID=@BPOID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@BPOID", bpoId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteBPOInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除靈寶禮斗訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：23=玉敕大樹朝天宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteLingbaolidouInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "23":
                        //玉敕大樹朝天宮
                        view = $"Temple_{Year}..Lingbaolidou_ma_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int lingbaolidouId = Convert.ToInt32(row["LingbaolidouID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE LingbaolidouID=@LingbaolidouID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@LingbaolidouID", lingbaolidouId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteLingbaolidouInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除七朝清醮訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：3=大甲鎮瀾宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteTaoistJiaoCeremonyInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "3":
                        //大甲鎮瀾宮
                        view = $"Temple_{Year}..TaoistJiaoCeremony_da_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int taoistJiaoCeremonyId = Convert.ToInt32(row["TaoistJiaoCeremonyID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE TaoistJiaoCeremonyID=@TaoistJiaoCeremonyID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@TaoistJiaoCeremonyID", taoistJiaoCeremonyId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteTaoistJiaoCeremonyInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除安斗訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：15=斗六五路財神宮、31=台灣道教總廟無極三清總道院等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteAnDouInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "15":
                        view = $"Temple_{Year}..AnDou_Fw_info"; break;
                    case "31":
                        view = $"Temple_{Year}..AnDou_wjsan_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int andouId = Convert.ToInt32(row["AnDouID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE AnDouID=@AnDouID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@AnDouID", andouId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteAnDouInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除供花供果訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：31=台灣道教總廟無極三清總道院等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteHuaguoInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "31":
                        view = $"Temple_{Year}..Huaguo_wjsan_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int huaguoId = Convert.ToInt32(row["HuaguoID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE HuaguoID=@HuaguoID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@HuaguoID", huaguoId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteHuaguoInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除祈安植福訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：35=松柏嶺受天宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteBlessingInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "35":
                        //松柏嶺受天宮
                        view = $"Temple_{Year}..Blessing_st_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int blessingId = Convert.ToInt32(row["BlessingID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE BlessingID=@BlessingID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@BlessingID", blessingId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteBlessingInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除千手觀音千燈迎佛法會訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：14=桃園威天宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteQnLightInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "14":
                        view = $"Temple_{Year}..QnLight_ty_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int qnlightId = Convert.ToInt32(row["QnLightID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE QnLightID=@QnLightID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@QnLightID", qnlightId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteQnLightInfo：\r\n" + detailedError);
                throw;
            }
        }

        /// <summary>
        /// 刪除新春賀歲感恩招財祿位訂單編號（將 Num 與 Num2String 清空為 0 與空字串）。
        /// </summary>
        /// <param name="ApplicantID">購買人編號。</param>
        /// <param name="AdminID">廟宇編號（例如：14=桃園威天宮等）。</param>
        /// <param name="Year">資料年份。</param>
        /// <returns>清除成功回傳 true，否則 false。</returns>
        public bool DeleteLuckaltarInfo(
            int ApplicantID,
            string AdminID,
            string Year)
        {
            bool result = false;
            try
            {
                string sql = string.Empty;
                string view = string.Empty;
                DataTable dtUpdateStatus = new DataTable();

                //──────────────────────────────
                // 根據 AdminID 指定正確的 Temple 資料表
                //──────────────────────────────
                switch (AdminID)
                {
                    case "14":
                        view = $"Temple_{Year}..Luckaltar_ty_info"; break;
                }

                if (!string.IsNullOrEmpty(view))
                {
                    sql = $"SELECT * FROM {view} WHERE ApplicantID=@ApplicantID AND Status=0";

                    // 讀取對應燈號
                    using (var adapter = new DatabaseAdapter(sql, this.DBSource))
                    {
                        adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                        adapter.Fill(dtUpdateStatus);
                    }

                    if (dtUpdateStatus.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtUpdateStatus.Rows)
                        {
                            int qnlightId = Convert.ToInt32(row["LuckaltarID"]);

                            // 使用參數化 SQL 清除 Num 與 Num2String
                            string updateSql = $"UPDATE {view} SET Num2String=@Num2String, Num=@Num WHERE LuckaltarID=@LuckaltarID";
                            using (var updateAdapter = new DatabaseAdapter(updateSql, this.DBSource))
                            {
                                updateAdapter.AddParameterToSelectCommand("@Num2String", string.Empty);
                                updateAdapter.AddParameterToSelectCommand("@Num", 0);
                                updateAdapter.AddParameterToSelectCommand("@LuckaltarID", qnlightId);

                                int res = updateAdapter.ExecuteSql();
                                if (res > 0)
                                    result = true;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception error)
            {
                string detailedError = ErrorLogger.FormatError(error, typeof(AdminDAC).FullName);
                twbobibobi.Data.BasePage basePage = new twbobibobi.Data.BasePage();
                basePage.SaveErrorLog("AdminDAC.DeleteLuckaltarInfo：\r\n" + detailedError);
                throw;
            }
        }

        #endregion
    }
}