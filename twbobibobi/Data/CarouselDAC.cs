using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CarouselDAC : BaseDAC
    {
        public List<Carousel> SelectActive(string groupName)
        {
            var result = new List<Carousel>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "SELECT Id, Title, Description, ImageFileType, ButtonVisible, ButtonText, ButtonLink FROM Carousel WHERE Status=1 AND groupName=@groupName ORDER BY Seq";
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
                            result.Add(new Carousel()
                            {
                                Id = dr.GetInt32(0),
                                Title = dr.GetString(1),
                                Description = dr.GetString(2),
                                ImageFileType = dr.GetString(3),
                                ButtonVisible = dr.GetInt16(4),
                                ButtonText = dr.GetString(5),
                                ButtonLink = dr.GetString(6),
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}