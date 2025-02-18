using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class EventTitbitsDAC : BaseDAC
    {
        public List<EventTitbits> SelectActive(int topRows)
        {
            List<EventTitbits> result = new List<EventTitbits>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = $"SELECT TOP {topRows} a.id, a.eventid, a.type, a.imagefiletype FROM eventtitbits AS a INNER JOIN event AS b ON a.eventid=b.id WHERE a.status=1 and b.status=1 ORDER BY a.seq ASC, a.id DESC";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(new EventTitbits()
                            {
                                Id = dr.GetInt64(0),
                                EventId = dr.GetInt32(1),
                                Type = dr.GetInt16(2),
                                ImageFileType = dr.GetString(3)
                            });
                        }
                    }
                }
            }
            return result;
        }
        public List<EventTitbits> SelectByEventId(int eventId)
        {
            List<EventTitbits> result = new List<EventTitbits>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = $"SELECT id, eventid, type, imagefiletype, resourceuri FROM eventtitbits WHERE status=1 AND eventid={eventId} ORDER BY seq ASC, id DESC";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(new EventTitbits()
                            {
                                Id = dr.GetInt64(0),
                                EventId = dr.GetInt32(1),
                                Type = dr.GetInt16(2),
                                ImageFileType = dr.GetString(3),
                                ResourceUri = dr.GetString(4)
                            });
                        }
                    }
                }
            }
            return result;
        }
        public EventTitbits SelectFirst(int eventId)
        {
            EventTitbits result = null;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = $"SELECT TOP 1 id, eventid, type, imagefiletype, resourceuri FROM eventtitbits WHERE status=1 AND eventid={eventId} ORDER BY seq ASC, id DESC";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = new EventTitbits()
                            {
                                Id = dr.GetInt64(0),
                                EventId = dr.GetInt32(1),
                                Type = dr.GetInt16(2),
                                ImageFileType = dr.GetString(3),
                                ResourceUri = dr.GetString(4)
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}