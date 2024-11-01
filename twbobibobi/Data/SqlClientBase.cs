using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MotoSystem.Data
{
    public class SqlClientBase
    {
        public SqlClientBase(BasePage basePage)
        {
            this.DBSource = basePage.DBSource;
        }

        public SqlClientBase(System.Data.SqlClient.SqlConnection dbSource)
        {
            this.DBSource = dbSource;
        }

        protected System.Data.SqlClient.SqlConnection DBSource = null;

        /// <summary>
        /// 获取指定数据表中的指定数据列的最大值。
        /// </summary>
        /// <param name="tableName">指定的数据表名称。</param>
        /// <param name="column">指定的数据列名称</param>
        /// <returns>指定数据列的最大值</returns>
        public int GetMaxID(string tableName, string column)
        {
            System.Data.SqlClient.SqlDataAdapter AdapterObj = new System.Data.SqlClient.SqlDataAdapter(string.Format("Select Max({0}) From {1}", column, tableName), this.DBSource);
            DataTable dtTable = new DataTable();
            AdapterObj.Fill(dtTable);
            if (dtTable.Rows.Count > 0)
            {
                return int.Parse(dtTable.Rows[0][0].ToString());
            }
            return 0;
        }

        /// <summary>
        /// 获取指定数据表的记录数量
        /// </summary>
        /// <param name="tableName">指定的数据表名称。</param>
        /// <returns>指定数据表的记录数量</returns>
        public int GetRecordCount(string tableName)
        {
            System.Data.SqlClient.SqlDataAdapter AdapterObj = new System.Data.SqlClient.SqlDataAdapter(string.Format("Select count(*) From {0}", tableName), this.DBSource);
            DataTable dtTable = new DataTable();
            AdapterObj.Fill(dtTable);
            if (dtTable.Rows.Count > 0)
            {
                return int.Parse(dtTable.Rows[0][0].ToString());
            }
            return 0;
        }


        /// <summary>
        /// 获取指定数据表的记录数量
        /// </summary>
        /// <param name="tableName">指定的数据表名称</param>
        /// <param name="whereParams">指定数据表的过滤参数字符</param>
        /// <param name="paramValues">过滤条件参数值</param>
        /// <returns>指定数据表的记录数量</returns>
        public int GetRecordCount(string tableName, string whereParams, object[] paramValues)
        {
            string sql = string.Format("Select count(*) From {0} where {1}", tableName, whereParams);
            System.Data.SqlClient.SqlDataAdapter AdapterObj = new System.Data.SqlClient.SqlDataAdapter(string.Format("Select count(*) From {0} where {1}", tableName, whereParams), this.DBSource);
            System.Data.SqlClient.SqlParameter param;
            for (int i = 0; i < paramValues.Length; i++)
            {
                param = AdapterObj.SelectCommand.CreateParameter();
                param.Value = paramValues[i];
                AdapterObj.SelectCommand.Parameters.Add(param);
            }
            DataTable dtTable = new DataTable();
            AdapterObj.Fill(dtTable);
            if (dtTable.Rows.Count > 0)
            {
                return int.Parse(dtTable.Rows[0][0].ToString());
            }
            return 0;
        }

        public float GetSum(string tableName, string column, string whereParams, object[] paramValues)
        {
            string sql = string.Format("Select Sum({2}) From {0} where {1}", tableName, whereParams, column);
            System.Data.SqlClient.SqlDataAdapter AdapterObj = new System.Data.SqlClient.SqlDataAdapter(sql, this.DBSource);
            System.Data.SqlClient.SqlParameter param;
            for (int i = 0; i < paramValues.Length; i++)
            {
                param = AdapterObj.SelectCommand.CreateParameter();
                param.Value = paramValues[i];
                AdapterObj.SelectCommand.Parameters.Add(param);
            }
            DataTable dtTable = new DataTable();
            AdapterObj.Fill(dtTable);
            if (dtTable.Rows.Count > 0 && dtTable.Rows[0][0] != System.DBNull.Value)
            {
                return float.Parse(dtTable.Rows[0][0].ToString());
            }
            return 0F;
        }

        public int GetIdentity()
        {
            int uniqueID = 0;
            System.Data.SqlClient.SqlCommand CmdObj = DBSource.CreateCommand();
            CmdObj.CommandText = "Select @@Identity";
            object newid = CmdObj.ExecuteScalar();
            if (newid != null)
            {
                uniqueID = int.Parse(newid.ToString());
            }
            return uniqueID;
        }

        public int ExecuteSql(string sql)
        {
            System.Data.SqlClient.SqlCommand CmdObj = DBSource.CreateCommand();
            CmdObj.CommandText = sql;
            return CmdObj.ExecuteNonQuery();
        }

        public string ExecuteScalarSql(string sql)
        {
            System.Data.SqlClient.SqlCommand CmdObj = DBSource.CreateCommand();
            CmdObj.CommandText = sql;
            return CmdObj.ExecuteScalar().ToString();
        }
    }
}