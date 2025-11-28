using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Security.Cryptography;

namespace twbobibobi.Data
{
    public class JSonHelper
    {
        protected Hashtable JSonData = new Hashtable();
        protected LitJson.JsonData mJsonData = null;

        public void ClearContent()
        {
            JSonData.Clear();
        }

        public void AddContent(string name, object value)
        {
            JSonData[name] = value;
        }

        public void AddDataTable(string name, DataTable dataTable)
        {
            List<Hashtable> datalist = new List<Hashtable>();

            object DBNullValue = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                Hashtable tmp = new Hashtable();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (dataTable.Columns[i].DataType == typeof(string))
                    {
                        DBNullValue = string.Empty;
                    }
                    else if (dataTable.Columns[i].DataType == typeof(DateTime))
                    {
                        DBNullValue = "0000-00-00";
                    }
                    else
                    {
                        DBNullValue = 0;
                    }
                    if (dataTable.Columns[i].DataType == typeof(DateTime))
                    {
                        tmp[dataTable.Columns[i].ColumnName] = row[i] == DBNull.Value ? DBNullValue : DateTime.Parse(row[i].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        tmp[dataTable.Columns[i].ColumnName] = row[i] == DBNull.Value ? DBNullValue : row[i];
                    }

                }

                datalist.Add(tmp);
            }

            JSonData[name] = datalist;
        }



        public string ToMsgJSonString(int requestID, object data, object session)
        {
            Hashtable table = new Hashtable();
            table.Add("RequestID", requestID);
            if (data != null)
            {
                table.Add("Data", data);
            }
            if (session != null)
            {
                table.Add("Session", session);
            }
            return LitJson.JsonMapper.ToJson(table);
        }

        public string ToJSonString()
        {
            return LitJson.JsonMapper.ToJson(JSonData);
        }

        public string ToJSonString(Hashtable jsonData)
        {
            return LitJson.JsonMapper.ToJson(jsonData);
        }

        public string ToJSonString(object jsonData)
        {
            return LitJson.JsonMapper.ToJson(jsonData);
        }

        public string ToJSonString(System.Collections.Specialized.NameValueCollection collection)
        {
            Hashtable table = new Hashtable();
            for (int i = 0; i < collection.Count; i++)
            {
                table.Add(collection.Keys[i], collection[i]);
            }
            return LitJson.JsonMapper.ToJson(table);
        }

        public T FromJson<T>(string json)
        {
            return LitJson.JsonMapper.ToObject<T>(json);
            //return JsonFormator.ConvertToType<T>(json);
        }

        public Hashtable FromJson(string json)
        {
            Hashtable table = new Hashtable();
            LitJson.JsonReader reader = new LitJson.JsonReader(json);

            // The Read() method returns false when there's nothing else to read
            string propertyName = string.Empty;
            while (reader.Read())
            {
                if (reader.Token == LitJson.JsonToken.ArrayEnd || reader.Token == LitJson.JsonToken.ObjectEnd)
                {
                    break;
                }
                else if (reader.Token == LitJson.JsonToken.PropertyName)
                {
                    propertyName = reader.Value.ToString();
                    if (reader.Read())
                    {
                        if (reader.Token == LitJson.JsonToken.ArrayStart || reader.Token == LitJson.JsonToken.ObjectStart)
                        {
                            table.Add(propertyName, HandleJsonArray(reader));
                        }
                        else
                        {
                            table.Add(propertyName, reader.Value);
                        }
                    }

                }
            }

            return table;
        }

        protected object HandleJsonArray(LitJson.JsonReader reader)
        {
            string propertyName = string.Empty;

            reader.Read();
            if (reader.Token == LitJson.JsonToken.ObjectStart)
            {
                List<Hashtable> list = new List<Hashtable>();
                Hashtable table = new Hashtable();
                while (reader.Read())
                {
                    if (reader.Token == LitJson.JsonToken.ObjectEnd)
                    {
                        list.Add(table);
                        table = new Hashtable();
                        continue;
                    }
                    if (reader.Token == LitJson.JsonToken.ArrayEnd)
                    {
                        break;
                    }
                    if (reader.Token == LitJson.JsonToken.PropertyName)
                    {
                        propertyName = reader.Value.ToString();
                        if (reader.Read())
                        {
                            if (reader.Token == LitJson.JsonToken.ArrayStart)
                            {
                                table.Add(propertyName, HandleJsonArray(reader));
                            }
                            else
                            {
                                table.Add(propertyName, reader.Value);
                            }
                        }

                    }
                }

                return list;
            }
            else if (reader.Token == LitJson.JsonToken.PropertyName)
            {
                Hashtable table = new Hashtable();

                propertyName = reader.Value.ToString();
                reader.Read();
                table.Add(propertyName, reader.Value);

                while (reader.Read())
                {
                    if (reader.Token == LitJson.JsonToken.ObjectEnd)
                    {
                        break;
                    }

                    if (reader.Token == LitJson.JsonToken.PropertyName)
                    {
                        propertyName = reader.Value.ToString();
                        if (reader.Read())
                        {
                            table.Add(propertyName, reader.Value);
                        }

                    }
                }
                return table;
            }
            return null;
        }

        public bool ContainsKey(string key)
        {
            return mJsonData != null;
        }
        public T ConvertObject<T>(string key)
        {
            if (!ContainsKey(key))
                return default(T);

            return FromJson<T>(mJsonData[key].ToJson());
        }

        public List<T> ConvertList<T>(string key)
        {
            if (!ContainsKey(key))
                return null;

            return ConvertListFromJson<T>(mJsonData[key].ToJson());
        }

        public T ConvertObjectFromJson<T>(string objectJson)
        {
            return FromJson<T>(objectJson);
        }

        public List<T> ConvertListFromJson<T>(string listJson)
        {
            List<T> datalist = new List<T>();
            try
            {
                LitJson.JsonData mJsonData = LitJson.JsonMapper.ToObject(listJson);
                if (mJsonData.IsArray)
                {
                    for (int i = 0; i < mJsonData.Count; i++)
                    {
                        datalist.Add(ConvertObjectFromJson<T>(mJsonData[i].ToJson()));
                    }
                }
            }
            catch
            {
            }
            return datalist;
        }

        public object GetObject(string key)
        {
            if (!ContainsKey(key))
                return null;

            return mJsonData[key];
        }

        public string GetJson(string key)
        {
            if (!ContainsKey(key))
                return null;

            return mJsonData[key].ToJson();
        }
        public bool LoadJSon(string json)
        {
            bool bResult = true;
            try
            {
                if (mJsonData != null)
                {
                    mJsonData.Clear();
                    mJsonData = null;
                }
                mJsonData = LitJson.JsonMapper.ToObject(json);
            }
            catch
            {
                bResult = false;
            }

            return bResult;
        }

        public string GetStringFromJson(string key, string json)
        {
            string value = string.Empty;
            int pos_start = json.IndexOf("\"" + key + "\"");
            if (pos_start > 0)
            {
                int pos_end = json.IndexOf(",", pos_start);
                if (pos_end > 0)
                {
                    pos_start = json.IndexOf(":", pos_start);
                    value = json.Substring(pos_start + 1, pos_end - pos_start - 1);
                }
                else
                {
                    pos_end = json.IndexOf("}", pos_start);
                    if (pos_end > 0)
                    {
                        pos_start = json.IndexOf(":", pos_start);
                        value = json.Substring(pos_start + 1, pos_end - pos_start - 1);
                    }
                }
            }

            return value.Replace("\"", "").Trim();
        }

        /// <summary>
        /// Get SHA256 hash
        /// </summary>
        /// <param name="str"></param>
        /// <example>
        /// For example:
        /// <code>
        ///    Sha256("Ruyut");
        /// </code>
        ///  results in <c>e5a47100b733b86f6fc82b9b614c4829bc9042ca3f24a65c5c2783c699ed6625</c>
        /// </example>
        public string Sha256(string str)
        {
            byte[] sha256Bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] bytes = sha256.ComputeHash(sha256Bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public string HMACSHA256(string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }
    }
}