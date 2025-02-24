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
using System.Xml.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Temple.FET.APITEST
{
    public partial class QueryOrder : AjaxBasePage
    {
        public string checkedkey;
        public string key;
        public string Year = "2025";
        public string[] adminlist = { "3", "4", "6", "8", "10", "14", "15", "16", "21", "23", "29", "31", "32" };

        protected void Page_Load(object sender, EventArgs e)
        {
            var sr = new StreamReader(Request.InputStream);//读取流

            var stream = sr.ReadToEnd();//读取所有数据

            string FETVALUE = mJSonHelper.GetStringFromJson("FETVALUE", stream);
            string channel = mJSonHelper.GetStringFromJson("channel", stream);
            string transmitTime = mJSonHelper.GetStringFromJson("transmitTime", stream);
            string clientOrderNumber = mJSonHelper.GetStringFromJson("clientOrderNumber", stream);
            //string encryptText = mJSonHelper.GetStringFromJson("paramContent", stream);
            string URL = Request.Url.Authority;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            Year = dtNow.Year.ToString();

            //channel = "FETnet";
            //clientOrderNumber = "CMPO20231019000076";
            //FETVALUE = "79896cad0402d66d7912c492a5d98c6d25d7c0178c282e6ee48cd9a6dca70c39";

            key = "g%Qx7h_eOg";
            if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
            {
                key = "koB#pPv2%t";
            }


            string F = mJSonHelper.Sha256(channel + key + clientOrderNumber);

            checkedkey = "shh#upsu6lyoeBkx";

            if (URL.IndexOf("20.6.8.46") >= 0 || URL.IndexOf("fettest") >= 0)
            {
                checkedkey = "lvrd5bidxr^dqlwX";
            }

            //checkedkey = "ukjmi3jipjknkI+t";
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
                                    writer2.WriteValue("1003");

                                    writer2.WriteEndObject();

                                    encrypt = AESHelper.AesEncrypt(sw2.ToString(), checkedkey);
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
                    AdminDAC objAdminDAC = new AdminDAC(this);
                    LightDAC objLightDAC = new LightDAC(this);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(stream);

                    string total = string.Empty;

                    //if (obj["paramContent"] != null)
                    //{
                    //    JObject paramContent = (JObject)obj["paramContent"];
                    //    total = paramContent["totalAmount"].ToString();
                    //}

                    DataTable dtData = new DataTable();
                    DataTable dtAdmin = new DataTable();
                    string AdminID = string.Empty;
                    string kind = string.Empty;
                    string Description = string.Empty;
                    string OrderID = string.Empty;
                    string[] LightsList = new string[0];
                    string[] clist = { "CMPO20241119001243", "CMPO20241119001241", "CMPO20250115015318" };

                    //DataTable dtAdmin = objAdminDAC.GetAdminList(9);
                    for (int j = 1; j <= 18; j++)
                    {
                        for (int i = 0; i < adminlist.Length; i++)
                        {
                            string adminID = adminlist[i].ToString();

                            dtData = objLightDAC.Getappcharge(clientOrderNumber, adminID, j.ToString(), Year);

                            for (int k = 0; k < dtData.Rows.Count; k++)
                            {
                                var tempList_lights = LightsList.ToList();

                                string NumString = dtData.Rows[k]["Num2String"].ToString();

                                tempList_lights.Add(NumString);

                                LightsList = tempList_lights.ToArray();

                                if (Description == string.Empty)
                                {
                                    Description = dtData.Rows[0]["Description"].ToString().Replace("\r\n", "");
                                }

                                if (OrderID == string.Empty)
                                {
                                    OrderID = dtData.Rows[0]["OrderID"].ToString();
                                }
                            }
                        }

                        if (LightsList.Length > 0)
                        {
                            kind = j.ToString();
                            break;
                        }
                    }

                    if (LightsList.Length == 0)
                    {
                        dtAdmin = objAdminDAC.GetAdminList(8);
                        for (int i = 0; i < dtAdmin.Rows.Count; i++)
                        {
                            string adminID = dtAdmin.Rows[i]["AdminID"].ToString();

                            dtData = objLightDAC.GetappchargeNum2String(clientOrderNumber, adminID, "3", dtNow.Year.ToString());
                            if (dtData.Rows.Count > 0)
                            {
                                kind = "3";

                                if (Description == string.Empty)
                                {
                                    Description = dtData.Rows[0]["Description"].ToString().Replace("\r\n", "");
                                }

                                if (OrderID == string.Empty)
                                {
                                    OrderID = dtData.Rows[0]["OrderID"].ToString();
                                }
                                break;
                            }
                        }
                    }

                    if (dtData.Rows.Count > 0 || LightsList.Length > 0)
                    {
                        //Description = dtData.Rows[0]["Description"].ToString().Replace("\r\n","");
                        if (Description != string.Empty && OrderID != string.Empty)
                        {
                            JArray itemsInfo = JsonConvert.DeserializeObject<JArray>(Description);

                            JSONStringOrder(checkedkey, clientOrderNumber, OrderID, encrypt, kind, LightsList, itemsInfo);
                        }
                        else
                        {
                            JSONCancelOrder(checkedkey, clientOrderNumber, clist, encrypt);
                        }
                    }
                    else
                    {
                        JSONCancelOrder(checkedkey, clientOrderNumber, clist, encrypt);
                    }
                }
            }
            catch (Exception ex)
            {
                JSONErrorOrder(checkedkey, encrypt, "1003", "0000", ex.Message);
            }
        }

    }
}