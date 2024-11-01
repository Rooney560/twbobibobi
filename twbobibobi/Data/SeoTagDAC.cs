using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace twbobibobi.Data
{
    public class SeoTagDAC : BaseDAC
    {
        public List<string> SelectActive(string page)
        {
            List<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "SELECT TAG FROM SeoTag WHERE Status=1 AND Page=@page ORDER BY Seq";
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@page", SqlDbType.VarChar, 32)
                };
                parms[0].Value = page;

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parm in parms)
                        command.Parameters.Add(parm);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                           result.Add(dr.GetString(0));
                        }
                    }
                }
            }
            return result;
        }
    }
}