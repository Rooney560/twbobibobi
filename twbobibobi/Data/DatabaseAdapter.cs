using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MotoSystem.Data
{
    public class DatabaseAdapter
    {
        public DatabaseAdapter(string sql, object DbSource)
        {
            this.SelectSql = sql;

            DBConnection = (System.Data.SqlClient.SqlConnection)DbSource;
            DBDataAdapter = new System.Data.SqlClient.SqlDataAdapter(SelectSql, DBConnection);


        }

        public int Fill(DataTable dataTable)
        {

            return DBDataAdapter.Fill(dataTable);

        }

        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
        {
            return DBDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);
        }

        public int Fill(DataSet dataSet, string srcTable)
        {
            return DBDataAdapter.Fill(dataSet, srcTable);
        }

        public int Update(DataTable dataTable)
        {
            return DBDataAdapter.Update(dataTable);
        }

        public void SetSqlCommandBuilder()
        {
            DBCommandBuilder = new System.Data.SqlClient.SqlCommandBuilder(DBDataAdapter);
        }

        public void AddParameterToSelectCommand(string paramName, object paramValue)
        {
            System.Data.SqlClient.SqlParameter param = DBDataAdapter.SelectCommand.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            DBDataAdapter.SelectCommand.Parameters.Add(param);
        }



        private string SelectSql;

        private System.Data.SqlClient.SqlConnection DBConnection;

        public System.Data.SqlClient.SqlDataAdapter DBDataAdapter;

        public System.Data.SqlClient.SqlCommandBuilder DBCommandBuilder;


    }

    public class DBCommand
    {
        public DBCommand(object DbSource)
        {

            DBConnection = (System.Data.SqlClient.SqlConnection)DbSource;

        }

        private System.Data.SqlClient.SqlConnection DBConnection;

        public int ExecuteSql(string sql)
        {
            System.Data.SqlClient.SqlCommand CmdObj = DBConnection.CreateCommand();
            CmdObj.CommandText = sql;
            return CmdObj.ExecuteNonQuery();
        }


    }
}