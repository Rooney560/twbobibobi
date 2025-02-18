using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CeremonyDAC : BaseDAC
    {
        public List<Ceremony> SelectActive()
        {
            var result = new List<Ceremony>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = "SELECT Id, Title, Description, ImageFileType, ButtonVisible, ButtonText, ButtonLink FROM Ceremony WHERE Status=1 ORDER BY Seq";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(new Ceremony()
                            {
                                Id = dr.GetInt32(0),
                                Title = dr.GetString(1),
                                Description = dr.GetString(2),
                                ImageFileType = dr.GetString(3),
                                ButtonVisible = dr.GetInt16(4),
                                ButtonText = dr.GetString(5),
                                ButtonLink = dr.GetString(6)
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}