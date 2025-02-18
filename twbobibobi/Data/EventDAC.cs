using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class EventDAC : BaseDAC
    {
        public List<Event> SelectActive()
        {
            List<Event> result = new List<Event>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = $"SELECT id, eventname, resourcecount FROM event WHERE status=1 ORDER BY seq ASC, id DESC";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(new Event()
                            {
                                Id = dr.GetInt32(0),
                                EventName = dr.GetString(1),
                                ResourceCount = dr.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}