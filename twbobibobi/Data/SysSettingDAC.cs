using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class SysSettingDAC : BaseDAC
    {
        public List<SysSetting> SelectActive(string groupName)
        {
            var result = new List<SysSetting>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "SELECT Item,Type,Value FROM SysSetting WHERE groupName=@groupName ORDER BY Seq";
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@groupName", SqlDbType.VarChar, 32)
                };
                parms[0].Value = groupName;

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parm in parms)
                        command.Parameters.Add(parm);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(new SysSetting()
                            {
                                Item = dr.GetString(0),
                                Type = dr.GetString(1),
                                Value = dr.GetString(2),
                            });
                        }
                    }
                }
            }
            return result;
        }
        public SysSetting SelectByPK(string item)
        {
            SysSetting result = null;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "SELECT Item,Type,Value FROM SysSetting WHERE item=@item";
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@item", SqlDbType.VarChar, 100)
                };
                parms[0].Value = item;

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parm in parms)
                        command.Parameters.Add(parm);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = new SysSetting()
                            {
                                Item = dr.GetString(0),
                                Type = dr.GetString(1),
                                Value = dr.GetString(2),
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}