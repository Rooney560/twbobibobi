using MotoSystem.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temple.data;
using LitJson;
using System.Reflection;

namespace Temple.FET.APITEST
{
    public partial class CancelOrder : AjaxBasePage
    {
        public string key;
        public string checkedkey;
        public string Year = "2025";

        protected void Page_Load(object sender, EventArgs e)
        {
            var sr = new StreamReader(Request.InputStream);//读取流

            var stream = sr.ReadToEnd();//读取所有数据

            string FETVALUE = mJSonHelper.GetStringFromJson("FETVALUE", stream);
            string channel = mJSonHelper.GetStringFromJson("channel", stream);
            string transmitTime = mJSonHelper.GetStringFromJson("transmitTime", stream);
            string clientOrderNumber = mJSonHelper.GetStringFromJson("clientOrderNumber", stream);
            string encryptText = mJSonHelper.GetStringFromJson("paramContent", stream);
            string URL = Request.Url.Authority;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            //channel = "FETnet";
            //clientOrderNumber = "CMPO20231019000076";
            //FETVALUE = "79896cad0402d66d7912c492a5d98c6d25d7c0178c282e6ee48cd9a6dca70c39";

            //key = "koB#pPv2%t";
            key = "g%Qx7h_eOg";
            if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
            {
                key = "koB#pPv2%t";
            }

            string F = mJSonHelper.Sha256(channel + key + clientOrderNumber);

            //checkedkey = "lvrd5bidxr^dqlwX";
            checkedkey = "shh#upsu6lyoeBkx";
            if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
            {
                checkedkey = "lvrd5bidxr^dqlwX";
            }

            string decrypt = string.Empty;
            string encrypt = string.Empty;

            SaveRequestLog(Request.Url + stream);

            try
            {
                if (F != FETVALUE)
                {
                    //JSON寫入到檔案
                    using (StringWriter sw = new StringWriter())
                    {
                        using (JsonTextWriter writer = new JsonTextWriter(sw))
                        {
                            //建立物件
                            writer.WriteStartObject();

                            //物件名稱
                            writer.WritePropertyName("detail");

                            using (StringWriter sw2 = new StringWriter())
                            {
                                using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                                {
                                    //建立物件
                                    writer2.WriteStartObject();

                                    writer2.WritePropertyName("orderMsg");
                                    writer2.WriteValue("fail");

                                    writer2.WritePropertyName("orderStatus");
                                    writer2.WriteValue("1001");

                                    writer2.WriteEndObject();

                                    encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                    writer2.Flush();
                                    writer2.Close();
                                    sw2.Flush();
                                    sw2.Close();
                                }
                            }

                            writer.WriteValue(encrypt);

                            writer.WritePropertyName("resultCode"); writer.WriteValue("9999");

                            writer.WriteEndObject();

                            writer.Flush();
                            writer.Close();
                            sw.Flush();
                            sw.Close();

                            //輸出結果
                            Response.Write(sw.ToString());

                            SaveRequestLog(Request.Url + sw.ToString());
                        }
                    }
                }
                else
                {
                    AdminDAC objAdminDAC = new AdminDAC(this);
                    LightDAC objLightDAC = new LightDAC(this);
                    //JObject obj = JsonConvert.DeserializeObject<JObject>(stream);


                    string total = string.Empty;

                    //if (obj["paramContent"] != null)
                    //{
                    //    JObject paramContent = (JObject)obj["paramContent"];
                    //    total = paramContent["totalAmount"].ToString();
                    //}

                    DataTable dtData = new DataTable();
                    string AdminID = string.Empty;
                    string kind = string.Empty;
                    bool checkeddata = true;
                    int type = 1;

                    while (dtData.Rows.Count > 0 || checkeddata)
                    {
                        DataTable dtAdmin = objAdminDAC.GetAdminList(9);
                        for (int i = 0; i < dtAdmin.Rows.Count; i++)
                        {
                            string adminID = dtAdmin.Rows[i]["AdminID"].ToString();

                            for (int j = 1; j <= 20; j++)
                            {
                                //if (j == 1 || j == 16)
                                //{
                                //}
                                //else
                                //{
                                //    Year = "2024";
                                //}

                                if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
                                {
                                    Year = "TEST";
                                }

                                dtData = objLightDAC.Getappcharge(clientOrderNumber, adminID, j.ToString(), type, Year);

                                if (dtData.Rows.Count > 0)
                                {
                                    kind = j.ToString();
                                    break;
                                }
                                else if (j == 1 && adminID == "14")
                                {
                                    type = 2;
                                    dtData = objLightDAC.Getappcharge(clientOrderNumber, adminID, j.ToString(), type, Year);
                                    if (dtData.Rows.Count > 0)
                                    {
                                        kind = j.ToString();
                                        break;
                                    }
                                }
                            }

                            if (dtData.Rows.Count > 0)
                            {
                                break;
                            }
                        }

                        if (dtData.Rows.Count == 0)
                        {
                            dtAdmin = objAdminDAC.GetAdminList(8);
                            for (int i = 0; i < dtAdmin.Rows.Count; i++)
                            {
                                string adminID = dtAdmin.Rows[i]["AdminID"].ToString();

                                Year = dtNow.Year.ToString();

                                if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
                                {
                                    Year = "TEST";
                                }
                                dtData = objLightDAC.Getappcharge(clientOrderNumber, adminID, "3", type, Year);

                                if (dtData.Rows.Count > 0)
                                {
                                    kind = "3";
                                    break;
                                }
                            }
                        }

                        if (dtData.Rows.Count > 0)
                        {
                            string adminID = dtData.Rows[0]["AdminID"].ToString();

                            //if (kind == "3")
                            //{
                            //    Year = dtNow.Year.ToString();
                            //}

                            //if (kind == "1")
                            //{
                            //    Year = "2025";
                            //}

                            if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
                            {
                                Year = "TEST";
                            }

                            if (dtData.Rows[0]["AppcStatus"].ToString() == "-2")
                            {
                            }
                            else
                            {
                                checkeddata = objAdminDAC.Updatestatus2appcharge(int.Parse(dtData.Rows[0]["UniqueID"].ToString()), -2, adminID, kind, type, Year);
                                //if (objAdminDAC.Updatestatus2appcharge(int.Parse(dtData.Rows[0]["UniqueID"].ToString()), -2, adminID, kind, Year))
                                //{
                                //    //JSON寫入到檔案
                                //    using (StringWriter sw = new StringWriter())
                                //    {
                                //        using (JsonTextWriter writer = new JsonTextWriter(sw))
                                //        {
                                //            //建立物件
                                //            writer.WriteStartObject();

                                //            //物件名稱
                                //            writer.WritePropertyName("detail");

                                //            using (StringWriter sw2 = new StringWriter())
                                //            {
                                //                using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                                //                {
                                //                    //建立物件
                                //                    writer2.WriteStartObject();

                                //                    writer2.WritePropertyName("orderMsg");
                                //                    writer2.WriteValue("success");

                                //                    writer2.WritePropertyName("orderStatus");
                                //                    writer2.WriteValue("2000");

                                //                    writer2.WriteEndObject();

                                //                    encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                //                    writer2.Flush();
                                //                    writer2.Close();
                                //                    sw2.Flush();
                                //                    sw2.Close();
                                //                }
                                //            }

                                //            writer.WriteValue(encrypt);

                                //            writer.WritePropertyName("resultCode"); writer.WriteValue("0000");

                                //            writer.WriteEndObject();

                                //            writer.Flush();
                                //            writer.Close();
                                //            sw.Flush();
                                //            sw.Close();

                                //            //輸出結果
                                //            Response.Write(sw.ToString());

                                //            SaveRequestLog(Request.Url + sw.ToString());
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    //JSON寫入到檔案
                                //    using (StringWriter sw = new StringWriter())
                                //    {
                                //        using (JsonTextWriter writer = new JsonTextWriter(sw))
                                //        {
                                //            //建立物件
                                //            writer.WriteStartObject();

                                //            //物件名稱
                                //            writer.WritePropertyName("detail");

                                //            using (StringWriter sw2 = new StringWriter())
                                //            {
                                //                using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                                //                {
                                //                    //建立物件
                                //                    writer2.WriteStartObject();

                                //                    writer2.WritePropertyName("orderMsg");
                                //                    writer2.WriteValue("fail");

                                //                    writer2.WritePropertyName("orderStatus");
                                //                    writer2.WriteValue("2001");

                                //                    writer2.WriteEndObject();

                                //                    encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                //                    writer2.Flush();
                                //                    writer2.Close();
                                //                    sw2.Flush();
                                //                    sw2.Close();
                                //                }
                                //            }

                                //            writer.WriteValue(encrypt);

                                //            writer.WritePropertyName("resultCode"); writer.WriteValue("9999");

                                //            writer.WriteEndObject();

                                //            writer.Flush();
                                //            writer.Close();
                                //            sw.Flush();
                                //            sw.Close();

                                //            //輸出結果
                                //            Response.Write(sw.ToString());

                                //            SaveRequestLog(Request.Url + sw.ToString());
                                //        }
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            checkeddata = false;
                        }
                    }

                    if (!checkeddata && dtData.Rows.Count == 0)
                    {
                        //JSON寫入到檔案
                        using (StringWriter sw = new StringWriter())
                        {
                            using (JsonTextWriter writer = new JsonTextWriter(sw))
                            {
                                //建立物件
                                writer.WriteStartObject();

                                //物件名稱
                                writer.WritePropertyName("detail");

                                using (StringWriter sw2 = new StringWriter())
                                {
                                    using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                                    {
                                        //建立物件
                                        writer2.WriteStartObject();

                                        writer2.WritePropertyName("orderMsg");
                                        writer2.WriteValue("success");

                                        writer2.WritePropertyName("orderStatus");
                                        writer2.WriteValue("2000");

                                        writer2.WriteEndObject();

                                        encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                        writer2.Flush();
                                        writer2.Close();
                                        sw2.Flush();
                                        sw2.Close();
                                    }
                                }

                                writer.WriteValue(encrypt);

                                writer.WritePropertyName("resultCode"); writer.WriteValue("0000");

                                writer.WriteEndObject();

                                writer.Flush();
                                writer.Close();
                                sw.Flush();
                                sw.Close();

                                //輸出結果
                                Response.Write(sw.ToString());

                                SaveRequestLog(Request.Url + sw.ToString());
                            }
                        }
                    }
                    else
                    {
                        //JSON寫入到檔案
                        using (StringWriter sw = new StringWriter())
                        {
                            using (JsonTextWriter writer = new JsonTextWriter(sw))
                            {
                                //建立物件
                                writer.WriteStartObject();

                                //物件名稱
                                writer.WritePropertyName("detail");

                                using (StringWriter sw2 = new StringWriter())
                                {
                                    using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                                    {
                                        //建立物件
                                        writer2.WriteStartObject();

                                        writer2.WritePropertyName("orderMsg");
                                        writer2.WriteValue("fail");

                                        writer2.WritePropertyName("orderStatus");
                                        writer2.WriteValue("2001");

                                        writer2.WriteEndObject();

                                        encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                        writer2.Flush();
                                        writer2.Close();
                                        sw2.Flush();
                                        sw2.Close();
                                    }
                                }

                                writer.WriteValue(encrypt);

                                writer.WritePropertyName("resultCode"); writer.WriteValue("9999");

                                writer.WriteEndObject();

                                writer.Flush();
                                writer.Close();
                                sw.Flush();
                                sw.Close();

                                //輸出結果
                                Response.Write(sw.ToString());

                                SaveRequestLog(Request.Url + sw.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //JSON寫入到檔案
                using (StringWriter sw = new StringWriter())
                {
                    using (JsonTextWriter writer = new JsonTextWriter(sw))
                    {
                        //建立物件
                        writer.WriteStartObject();

                        //物件名稱
                        writer.WritePropertyName("detail");

                        using (StringWriter sw2 = new StringWriter())
                        {
                            using (JsonTextWriter writer2 = new JsonTextWriter(sw2))
                            {
                                //建立物件
                                writer2.WriteStartObject();

                                writer2.WritePropertyName("orderMsg");
                                writer2.WriteValue("fail");

                                writer2.WritePropertyName("orderStatus");
                                writer2.WriteValue("1001");

                                writer2.WriteEndObject();

                                encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);

                                writer2.Flush();
                                writer2.Close();
                                sw2.Flush();
                                sw2.Close();
                            }
                        }

                        writer.WriteValue(encrypt);

                        writer.WritePropertyName("detail2");
                        writer.WriteValue(ex.ToString());

                        writer.WritePropertyName("resultCode"); writer.WriteValue("9999");

                        writer.WriteEndObject();

                        writer.Flush();
                        writer.Close();
                        sw.Flush();
                        sw.Close();

                        //輸出結果
                        Response.Write(sw.ToString());

                        SaveRequestLog(Request.Url + sw.ToString());
                    }
                }
            }
        }
    }
}