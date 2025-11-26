/***************************************************************************************************
 * 專案名稱：twbobibobi / MotoSystem / Temple 共用資料層
 * 檔案名稱：SqlClientBase.cs
 * 類別說明：資料庫操作基底類別，提供基本查詢、計算與指令執行功能
 * 建立日期：2025-11-11
 * 建立人員：Rooney
 * 修改記錄：2025-11-11 全面改用 using 機制、整合 DatabaseAdapter、移除共用連線設計
 * 目前維護人員：Rooney
 ***************************************************************************************************/

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace twbobibobi.Data
{
    /// <summary>
    /// 提供統一的資料庫操作基底類別，封裝基本查詢、計算與 SQL 執行功能。
    /// 改良版採安全 using 機制，不再共用同一 SqlConnection，避免連線池耗盡。
    /// </summary>
    public class SqlClientBase
    {
        #region 欄位與建構子

        /// <summary>
        /// 建構 SqlClientBase 實例。
        /// </summary>
        /// <param name="dbSource">外部提供的 SqlConnection 或 BasePage 的 DBSource。</param>
        public SqlClientBase(object dbSource)
        {
            if (dbSource is SqlConnection conn)
                this.DBSource = conn;
            else if (dbSource is BasePage page)
                this.DBSource = page.DBSource;
            else
                throw new ArgumentException("dbSource 必須為 SqlConnection 或 BasePage。");
        }

        /// <summary>
        /// 保留原始連線來源。
        /// </summary>
        protected SqlConnection DBSource;

        #endregion

        #region 基本查詢方法

        /// <summary>
        /// 取得指定資料表欄位的最大值。
        /// </summary>
        /// <param name="tableName">指定的資料表名稱。</param>
        /// <param name="column">指定的欄位名稱</param>
        /// <returns>指定欄位的最大值</returns>
        public int GetMaxID(string tableName, string column)
        {
            string sql = $"SELECT ISNULL(MAX({column}), 0) FROM {tableName}";
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                object result = adapter.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// 取得指定資料表的紀錄數量。
        /// </summary>
        /// <param name="tableName">指定的資料表名稱。</param>
        /// <returns>指定資料表的紀錄數量</returns>
        public int GetRecordCount(string tableName)
        {
            string sql = $"SELECT COUNT(*) FROM {tableName}";
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                object result = adapter.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// 取得指定條件下的筆數。
        /// </summary>
        /// <param name="tableName">指定的資料表名稱</param>
        /// <param name="whereParams">指定資料表的過濾條件</param>
        /// <param name="paramValues">過濾條件的參數值</param>
        /// <returns>指定資料表的紀錄數量</returns>
        public int GetRecordCount(string tableName, string whereParams, object[] paramValues)
        {
            string sql = $"SELECT COUNT(*) FROM {tableName} WHERE {whereParams}";
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                for (int i = 0; i < paramValues.Length; i++)
                    adapter.AddParameterToSelectCommand($"@p{i}", paramValues[i]);
                object result = adapter.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// 取得指定欄位的總和（SUM）。
        /// </summary>
        /// <param name="tableName">指定的資料表名稱</param>
        /// <param name="column">指定的欄位名稱</param>
        /// <param name="whereParams">指定資料表的過濾條件</param>
        /// <param name="paramValues">過濾條件的參數值</param>
        /// <returns>指定資料表的欄位總和</returns>
        public float GetSum(string tableName, string column, string whereParams, object[] paramValues)
        {
            string sql = $"SELECT SUM({column}) FROM {tableName} WHERE {whereParams}";
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                for (int i = 0; i < paramValues.Length; i++)
                    adapter.AddParameterToSelectCommand($"@p{i}", paramValues[i]);
                object result = adapter.ExecuteScalar();
                return result == DBNull.Value ? 0f : Convert.ToSingle(result);
            }
        }

        #endregion

        #region 執行 SQL 指令
        /// <summary>
        /// 執行 SQL 指令（INSERT/UPDATE/DELETE），回傳受影響筆數。
        /// </summary>
        public int ExecuteSql(string sql)
        {
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                return adapter.ExecuteSql();
            }
        }

        /// <summary>
        /// 執行查詢並回傳單一結果字串。
        /// </summary>
        public string ExecuteScalarSql(string sql)
        {
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                object result = adapter.ExecuteScalar();
                return result?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// 取得最近一筆新增的 Identity。
        /// </summary>
        public int GetIdentity()
        {
            const string sql = "SELECT CAST(@@IDENTITY AS INT)";
            using (var adapter = new DatabaseAdapter(sql, DBSource))
            {
                object result = adapter.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        #endregion
    }
}