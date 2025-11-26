/***************************************************************************************************
 * 專案名稱：Temple / twbobibobi 共用資料層
 * 檔案名稱：DatabaseAdapter.cs
 * 類別說明：安全且統一的資料庫操作輔助類別，支援 SELECT / INSERT / UPDATE / DELETE / Scalar 查詢
 * 建立日期：2025-11-11
 * 建立人員：Rooney
 * 修改記錄：2025-11-11 當日合併舊版功能、刪除 DBCommand、全面釋放資源避免 Connection Pool 耗盡
 * 修改記錄：2025-11-26 加入 RowUpdated 以支援 Identity 回傳
 * 目前維護人員：Rooney
 ***************************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using twbobibobi.Helpers;

namespace twbobibobi.Data
{
    /// <summary>
    /// 資料庫操作輔助類別，提供安全的 CRUD 操作封裝。
    /// 此類別可用於 DataTable 查詢（Fill）、一般 SQL 執行（ExecuteSql）、
    /// 以及回傳單一值查詢（ExecuteScalar）。
    /// </summary>
    public class DatabaseAdapter : IDisposable
    {
        #region 私有欄位
        private readonly string _selectSql;
        private readonly SqlConnection _connection;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _builder;
        private bool _disposed;

        // ★ 儲存 InsertCommand 執行後回傳的 Identity
        private int _lastInsertedId = 0;
        #endregion

        #region 建構子

        /// <summary>
        /// 初始化 DatabaseAdapter 實例。
        /// </summary>
        /// <param name="sql">SQL 指令。</param>
        /// <param name="DbSource">可為 SqlConnection 或連線字串。</param>
        public DatabaseAdapter(string sql, object DbSource)
        {
            _selectSql = sql;

            if (DbSource is string connStr)
                _connection = new SqlConnection(connStr);
            else if (DbSource is SqlConnection sqlConn)
                _connection = sqlConn;
            else
                throw new ArgumentException("DbSource 必須為 SqlConnection 或連線字串。");

            _adapter = new SqlDataAdapter(_selectSql, _connection);
        }

        #endregion

        #region SELECT 查詢類方法

        /// <summary>
        /// 執行查詢並填入 DataTable。
        /// </summary>
        /// <param name="table">目標 DataTable。</param>
        /// <returns>受影響筆數。</returns>
        //public int Fill(DataTable table)
        //{
        //    using (var adapter = new SqlDataAdapter(_selectSql, _connection))
        //    {
        //        foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
        //        {
        //            adapter.SelectCommand.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
        //        }

        //        if (_connection.State != ConnectionState.Open)
        //            _connection.Open();

        //        int count = adapter.Fill(table);
        //        _connection.Close();
        //        return count;
        //    }
        //}

        /// <summary>
        /// 執行查詢並填入 DataTable。
        /// </summary>
        /// <param name="table">目標 DataTable。</param>
        /// <returns>受影響筆數。</returns>
        public int Fill(DataTable table)
        {
            string sqlToRun = _selectSql.Trim();

            // 判斷是否為 INSERT 指令
            bool isInsert =
                sqlToRun.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) ||
                sqlToRun.StartsWith(" insert", StringComparison.OrdinalIgnoreCase);

            // 如果是 INSERT，自動補上 SCOPE_IDENTITY()
            if (isInsert && !sqlToRun.Contains("SCOPE_IDENTITY"))
            {
                sqlToRun += ";SELECT CAST(SCOPE_IDENTITY() AS INT);";
            }

            using (var cmd = new SqlCommand(sqlToRun, _connection))
            {
                // 帶入參數
                foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
                {
                    cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                if (isInsert)
                {
                    // ★ 取得 identity，並存入變數供 GetLastIdentity() 取用
                    object result = cmd.ExecuteScalar();
                    _connection.Close();

                    if (result != DBNull.Value && result != null)
                        _lastInsertedId = Convert.ToInt32(result);
                    else
                        _lastInsertedId = 0;

                    return 1; // Fill 回傳 1 表示成功，Identity 另外由 GetLastIdentity() 取
                }
                else
                {
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        int count = adapter.Fill(table);
                        _connection.Close();
                        return count;
                    }
                }
            }
        }

        /// <summary>
        /// 執行查詢並填入 DataSet。
        /// </summary>
        /// <param name="dataSet">目標 DataSet。</param>
        /// <param name="tableName">表格名稱。</param>
        /// <returns>受影響筆數。</returns>
        public int Fill(DataSet dataSet, string tableName)
        {
            using (var adapter = new SqlDataAdapter(_selectSql, _connection))
            {
                foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
                {
                    adapter.SelectCommand.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                int count = adapter.Fill(dataSet, tableName);
                _connection.Close();
                return count;
            }
        }

        /// <summary>
        /// 執行查詢並填入 DataSet。
        /// </summary>
        /// <param name="dataSet">目標 DataSet。</param>
        /// <param name="startRecord">開始筆數</param>
        /// <param name="maxRecords">最大筆數</param>
        /// <param name="tableName">表格名稱。</param>
        /// <returns>受影響筆數。</returns>
        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string tableName)
        {
            using (var adapter = new SqlDataAdapter(_selectSql, _connection))
            {
                foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
                {
                    adapter.SelectCommand.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                int count = adapter.Fill(dataSet, startRecord, maxRecords, tableName);
                _connection.Close();
                return count;
            }
        }

        #endregion

        #region 寫入 / 更新 / 刪除 類方法

        /// <summary>
        /// 執行 INSERT / UPDATE / DELETE 類 SQL 指令，回傳受影響筆數。
        /// </summary>
        /// <returns>受影響筆數。</returns>
        public int ExecuteSql()
        {
            using (SqlCommand cmd = new SqlCommand(_selectSql, _connection))
            {
                foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
                {
                    cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                int affected = cmd.ExecuteNonQuery();
                _connection.Close();
                return affected;
            }
        }

        /// <summary>
        /// 執行查詢並回傳單一結果（例如 SELECT COUNT(*) 或 SCOPE_IDENTITY()）。
        /// </summary>
        /// <returns>查詢結果。</returns>
        public object ExecuteScalar()
        {
            using (SqlCommand cmd = new SqlCommand(_selectSql, _connection))
            {
                foreach (SqlParameter p in _adapter.SelectCommand.Parameters)
                {
                    cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                object result = cmd.ExecuteScalar();
                _connection.Close();
                return result;
            }
        }

        #endregion

        #region DataAdapter Update 類別方法（保留相容性）

        /// <summary>
        /// 設定自動產生 Insert/Update/Delete 命令。
        /// </summary>
        public void SetSqlCommandBuilder()
        {
            _builder = new SqlCommandBuilder(_adapter);
        }

        /// <summary>
        /// 使用 DataAdapter 更新 DataTable 至資料庫。
        /// （建議僅用於 SELECT 結果集同步回寫）
        /// </summary>
        /// <param name="table">目標 DataTable。</param>
        /// <returns>更新筆數。</returns>
        public int Update(DataTable table)
        {
            if (_builder == null)
                _builder = new SqlCommandBuilder(_adapter);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            int result = _adapter.Update(table);
            _connection.Close();
            return result;
        }

        /// <summary>
        /// 取得最近一次 INSERT 的 Identity。
        /// </summary>
        public int GetLastIdentity()
        {
            return _lastInsertedId;
        }

        /// <summary>
        /// 使用與前一筆 INSERT 相同的 SqlConnection 取得 SCOPE_IDENTITY()。
        /// （此方法必須在同一個 DatabaseAdapter 生命週期中呼叫）
        /// </summary>
        /// <returns>最新建立的 Identity 值</returns>
        public int GetIdentitySameConnection()
        {
            const string sql = "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        #endregion

        #region 參數相關

        /// <summary>
        /// 新增查詢參數。
        /// </summary>
        /// <param name="paramName">參數名稱。</param>
        /// <param name="paramValue">參數值。</param>
        public void AddParameterToSelectCommand(string paramName, object paramValue)
        {
            SqlParameter param = _adapter.SelectCommand.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue ?? DBNull.Value;
            _adapter.SelectCommand.Parameters.Add(param);
        }

        #endregion

        #region 資源釋放

        /// <summary>
        /// 釋放資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 實際釋放資源的實作。
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _builder?.Dispose();
                    _adapter?.Dispose();
                    if (_connection != null && _connection.State != ConnectionState.Closed)
                        _connection.Close();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}