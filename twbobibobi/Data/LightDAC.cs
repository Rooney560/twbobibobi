using MotoSystem.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using twbobibobi.Entities;
using System.Configuration;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Utilities.Encoders;
using Temple.FET.APITEST;

namespace Temple.data
{
    public class LightDAC : SqlClientBase
    {
        protected string ConnString = "";
        protected bool result = false;
        public LightDAC(BasePage basePage) : base(basePage)
        {

        }

        public static string GetConfigValue(string paramName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[paramName];
            }
            catch (System.Configuration.ConfigurationErrorsException e)
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 檢查普度法會人姓名資料
        /// <param name="name">name=姓名</param>
        /// <param name="type">type=燈別 0-光明燈 1-安太歲 2-文昌燈 3-贊普 4-超拔</param>
        /// </summary>
        public bool checkedname(string name, int type)
        {
            bool result = false;
            string sql = string.Empty;
            string lights = "BrightlightsInfo";

            if (type == 1)
            {
                lights = "BrightlightsInfo";
            }
            else if (type == 2)
            {
                lights = "BrightlightsInfo";
            }
            else if (type == 3)
            {
                lights = "ZampInfo";
            }
            else if (type == 4)
            {
                lights = "SalvationInfo";
            }

            sql = "Select * from " + lights + " Where Name = @Name and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@Name", name);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count == 0)
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// 新增驗證碼
        /// </summary>
        /// <param name="ApplicantID"></param>
        /// <param name="AdminID">宮廟編號</param>
        /// <param name="Kind">服務項目</param>
        /// <param name="Code">驗證碼</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool addCAPTCHACode(int ApplicantID, int AdminID, int Kind, string Code, string AppMobile, string Year)
        {
            bool result = false;

            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..CAPTCHACode(ApplicantID, AdminID, Kind, Code, AppMobile, ExpirationDate, ExpirationDateString, Status, CreateDate, CreateDateString) " +
                "values(@ApplicantID, @AdminID, @Kind, @Code, @AppMobile, @ExpirationDate, @ExpirationDateString, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Kind", Kind);
            Adapter.AddParameterToSelectCommand("@Code", Code);
            Adapter.AddParameterToSelectCommand("@AppMobile", AppMobile);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-未使用 1-已使用 -1-錯誤
            Adapter.AddParameterToSelectCommand("@ExpirationDate", dtNow.AddSeconds(100).ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@ExpirationDateString", dtNow.AddSeconds(100).ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            if(this.GetIdentity() > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 建立普度法會人資料
        /// <param name="Name">Name=姓名</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Solar_v">Solar_v=國曆生日</param>
        /// <param name="Lunar_v">Lunar_v=農曆生日</param>
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// </summary>
        public int addlightsinfo(string Name, string Phone, string Address, string Addr, string County, string Dist, string ZipCode, DateTime Solar_v,DateTime Lunar_v, string type)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ";
            int lastnum = 0;

            switch (type)
            {
                case "0":
                    sql += "BrightlightsInfo";
                    lastnum = GetListNum(0);
                    break;
                case "1":
                    sql += "AntaisuilightsInfo";
                    lastnum = GetListNum(1);
                    break;
                case "2":
                    sql += "WenchanglightsInfo";
                    lastnum = GetListNum(2);
                    break;
            }
            if (lastnum > 0)
            {
                ++lastnum;
            }
            else
            {
                lastnum = 1001;
            }


            sql += "(Name, Mobile, Num, Address, Addr, County, Dist, ZipCode, BirthDay, Lunarcalendar, CreateDate) values(@Name, @Mobile, @Num, @Address, @Addr, @County, @Dist, @ZipCode, @BirthDay, @Lunarcalendar, @CreateDate)"; 
            
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Num", lastnum);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@BirthDay", Solar_v);
            Adapter.AddParameterToSelectCommand("@Lunarcalendar", Lunar_v);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }


        /// <summary>
        /// 建立贊普人資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addzampinfo(int applicantID, string Name, string Name2, string Phone, int Sendback, string Address, string Addr, string County, string Dist, string ZipCode)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            //int lastnum = GetListNum(3);
            //if (lastnum > 0)
            //{
            //    ++lastnum;
            //}
            //else
            //{
            //    lastnum = 1001;
            //}

            string sql = "Insert into ZampInfo(ApplicantID, Name, Name2, Mobile, Num, Sendback, Address, Addr, County, Dist, ZipCode, CreateDate) values(@ApplicantID, @Name, @Name2, @Mobile, @Num, @Sendback, @Address, @Addr, @County, @Dist, @ZipCode, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Num", 0);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立收件人資料
        /// <param name="zampID">zampID=贊普人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addreceiveInfo(int zampID, string Name, string Phone, string Address, string Addr, string County, string Dist, string ZipCode)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ReceiveInfo(ZampID, Name, Mobile, Address, Addr, County, Dist, ZipCode, CreateDate) values(@ZampID, @Name, @Mobile, @Address, @Addr, @County, @Dist, @ZipCode, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ZampID", zampID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立超拔人資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Name2">Name2=姓名2</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Birthday">Birthday=亡者生日</param>
        /// <param name="Deathday">Deathday=亡者忌日</param>
        /// <param name="type">type=普度類型 1-指名亡者 2-本境地基主 3-()家堂上歷代九玄七祖 4-()家英靈 5-冤親債主 6-為國捐軀三軍將士英靈 7-鐵公路車傷死亡眾魂 8-本境水難傷亡諸魂 9-本境男女無嗣孤魂等眾 10-六畜往生
        /// </summary>
        public int addSalvationinfo(int applicantID, string Name, string Name2, string DeathName, string FirstName, string Phone, string Address, string Addr, string County, string Dist, string ZipCode, string Birthday, string Deathday, string type)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            //int lastnum = GetListNum(4);
            //if (lastnum > 0)
            //{
            //    ++lastnum;
            //}
            //else
            //{
            //    lastnum = 1001;
            //}



            string sql = string.Empty;


            if (Birthday != "" && Deathday != "")
            {
                sql = "Insert into SalvationInfo(ApplicantID, Name, Name2, Num, DeathName, FirstName, Mobile, Address, Addr, County, Dist, ZipCode, Birthday, Deathday, Type, CreateDate) values(@ApplicantID, @Name, @Name2, @Num, @DeathName, @FirstName, @Mobile, @Address, @Addr, @County, @Dist, @ZipCode, " + Birthday + ", " + Deathday + ", @Type, @CreateDate)";

            }
            else
            {
                sql = "Insert into SalvationInfo(ApplicantID, Name, Name2, Num, DeathName, FirstName, Mobile, Address, Addr, County, Dist, ZipCode, Type, CreateDate) values(@ApplicantID, @Name, @Name2, @Num, @DeathName, @FirstName, @Mobile, @Address, @Addr, @County, @Dist, @ZipCode, @Type, @CreateDate)";

            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Num", 0);
            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);
            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Type", type);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        //普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人
        public static int GetPurdueCost(string PurdueType, int AdminID)
        {
            int result = 0;

            switch (AdminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1000;
                            break;
                        default:
                            //超拔
                            result = 620;
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    break;
                case 6:
                    //北港武德宮         
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                        default:
                            //超拔
                            result = 600;
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟    
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1000;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 600;
                            break;
                        case "14":
                            //孝道功德主 兩項普渡項目$3000/2
                            result = 3000;
                            break;
                        case "15":
                            //光明功德主
                            result = 1000;
                            break;
                        case "16":
                            //發心功德主
                            result = 600;
                            break;
                        case "18":
                            //白米50台斤
                            result = 1600;
                            break;
                        case "19":
                            //白米3台斤
                            result = 400;
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1200;
                            break;
                        case "2":
                            //超薦祖先
                            result = 2000;
                            break;
                        case "4":
                            //地基主
                            result = 2000;
                            break;
                        case "5":
                            //冤親債主
                            result = 2000;
                            break;
                        case "6":
                            //超渡嬰靈
                            result = 2000;
                            break;
                        case "12":
                            //動物靈
                            result = 1000;
                            break;
                        case "17":
                            //誦經迴向
                            result = 1200;
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1500;
                            break;
                        default:
                            //超拔
                            result = 500;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 2000;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (PurdueType)
                    {
                        case "1":
                            //贊普
                            result = 1200;
                            break;
                        case "12":
                            //法船
                            result = 580;
                            break;
                        case "13":
                            //壽生錢
                            result = 1500;
                            break;
                        default:
                            //法會其他項目
                            result = 300;
                            break;
                    }
                    break;
                case 30:
                    //鎮瀾買足
                    switch (PurdueType)
                    {
                        default:
                            result = 1100;
                            break;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 建立大甲鎮瀾宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Address">Address=祈福人地址(全)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="Sendback">Sendback=寄送方式 0-不寄回(會轉送給弱勢團體) 1-寄回(加收運費250元)</param>
        /// <param name="rName">rName=收件人姓名</param>
        /// <param name="rMobile">rMobile=收件人電話</param>
        /// <param name="rAddress">rAddress=收件人地址(全)</param>
        /// <param name="rAddr">rAddr=收件人地址(部分)</param>
        /// <param name="rCounty">rCounty=收件人縣市</param>
        /// <param name="rDist">rDist=收件人區域</param>
        /// <param name="rZipCode">rZipCode=收件人郵遞區號</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// <param name="Birthday">Birthday=亡者農曆生日</param>
        /// <param name="Deathday">Deathday=亡者死亡日期</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// </summary>
        public int addpurdue_da(int applicantID, string Name, string Name2, string Mobile, string PurdueType, string PurdueString, string Address, string Addr, string County, string Dist, string ZipCode, string Sendback, string rName, string rMobile, string rAddress, string rAddr, string rCounty, string rDist, string rZipCode, string DeathName, string Birthday, string Deathday, string FirstName)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_da_info(ApplicantID, AdminID, Name, Name2, Mobile, PurdueType, PurdueString, Address, Addr, County, dist, ZipCode, Sendback, rName, rMobile, rAddress, rAddr, rCounty, rdist, rZipCode, DeathName, Birthday, Deathday, FirstName, CreateDate, CreateDateString) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @PurdueType, @PurdueString, @Address, @Addr, @County, @dist, @ZipCode, @Sendback, @rName, @rMobile, @rAddress, @rAddr, @rCounty, @rdist, @rZipCode, @DeathName, @Birthday, @Deathday, @FirstName, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 3);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@rName", rName);
            Adapter.AddParameterToSelectCommand("@rMobile", rMobile);
            Adapter.AddParameterToSelectCommand("@rAddress", rAddress);
            Adapter.AddParameterToSelectCommand("@rAddr", rAddr);
            Adapter.AddParameterToSelectCommand("@rCounty", rCounty);
            Adapter.AddParameterToSelectCommand("@rdist", rDist);
            Adapter.AddParameterToSelectCommand("@rZipCode", rZipCode);

            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);
            Adapter.AddParameterToSelectCommand("@Birthday", Birthday);
            Adapter.AddParameterToSelectCommand("@Deathday", Deathday);
            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }


        /// <summary>
        /// 建立新港奉天宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Merit">Merit=功德主</param>
        /// <param name="MeritText">MeritText=功德主內容</param>
        /// <param name="Life">Life=祝燈延壽消災</param>
        /// <param name="Redress">Redress=四十九愿解冤釋結</param>
        /// <param name="Creditor">Creditor=冤親債主</param>
        /// <param name="Ancestor">Ancestor=九玄七祖</param>
        /// <param name="AncestorLastname">AncestorLastname=超度亡者姓氏</param>
        /// <param name="AncestorAddress">AncestorAddress=祖先牌位地址</param>
        /// <param name="Deceased">Deceased=功德迴向往生者</param>
        /// <param name="DeceasedName">DeceasedName=超度亡者姓名</param>
        /// <param name="DeceasedAddress">DeceasedAddress=祖先牌位地址</param>
        /// <param name="Landlord">Landlord=地祇主</param>
        /// <param name="LandlordNum">LandlordNum=地祇主數量</param>
        /// <param name="Baby">Baby=嬰靈</param>
        /// <param name="BabyNum">BabyNum=嬰靈數量</param>
        /// <param name="Animal">Animal=動物靈</param>
        /// <param name="AnimalNum">AnimalNum=動物靈數量</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Address">Address=地址</param>
        /// <param name="PurdueNum">PurdueNum=普度數量</param>
        /// <param name="RiceNum">RiceNum=白米數量</param>
        /// <param name="mMoneyNum">mMoneyNum=金紙數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addpurdue_hsinkangmazu(int applicantID, string AdminID, string Merit, string MeritText, string Life, string Redress, string Creditor, string Ancestor, string AncestorLastname, string AncestorAddress, string Deceased, string DeceasedName, string DeceasedAddress
            , string Landlord, string LandlordNum, string Baby, string BabyNum, string Animal, string AnimalNum, string Name, string Address, int PurdueNum, int RiceNum, int mMoneyNum, string Remark)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_h_info(ApplicantID, AdminID, Merit, MeritText, Life, Redress, Creditor, Ancestor, AncestorLastname, AncestorAddress, Deceased, DeceasedName, DeceasedAddress, Landlord, LandlordNum, Baby, BabyNum, Animal, AnimalNum, Name, " +
                "Address, PurdueNum, RiceNum, mMoneyNum, Remark, CreateDate, CreateDateString) values(@ApplicantID, @AdminID, @Merit, @MeritText, @Life, @Redress, @Creditor, @Ancestor, @AncestorLastname, @AncestorAddress, @Deceased, @DeceasedName, @DeceasedAddress, @Landlord, @LandlordNum, @Baby, " +
                "@BabyNum, @Animal, @AnimalNum, @Name, @Address, @PurdueNum, @RiceNum, @mMoneyNum, @Remark, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Merit", Merit);
            Adapter.AddParameterToSelectCommand("@MeritText", MeritText);

            Adapter.AddParameterToSelectCommand("@Life", Life);
            Adapter.AddParameterToSelectCommand("@Redress", Redress);

            Adapter.AddParameterToSelectCommand("@Creditor", Creditor);

            Adapter.AddParameterToSelectCommand("@Ancestor", Ancestor);
            Adapter.AddParameterToSelectCommand("@AncestorLastname", AncestorLastname);
            Adapter.AddParameterToSelectCommand("@AncestorAddress", AncestorAddress);

            Adapter.AddParameterToSelectCommand("@Deceased", Deceased);
            Adapter.AddParameterToSelectCommand("@DeceasedName", DeceasedName);
            Adapter.AddParameterToSelectCommand("@DeceasedAddress", DeceasedAddress);

            Adapter.AddParameterToSelectCommand("@Landlord", Landlord);
            Adapter.AddParameterToSelectCommand("@LandlordNum", LandlordNum);

            Adapter.AddParameterToSelectCommand("@Baby", Baby);
            Adapter.AddParameterToSelectCommand("@BabyNum", BabyNum);

            Adapter.AddParameterToSelectCommand("@Animal", Animal);
            Adapter.AddParameterToSelectCommand("@AnimalNum", AnimalNum);

            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Address", Address);

            Adapter.AddParameterToSelectCommand("@PurdueNum", PurdueNum);
            Adapter.AddParameterToSelectCommand("@RiceNum", RiceNum);
            Adapter.AddParameterToSelectCommand("@mMoneyNum", mMoneyNum);

            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立新港奉天宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Merit">Merit=功德主</param>
        /// <param name="MeritText">MeritText=功德主內容</param>
        /// <param name="Life">Life=祝燈延壽消災</param>
        /// <param name="Redress">Redress=四十九愿解冤釋結</param>
        /// <param name="Creditor">Creditor=冤親債主</param>
        /// <param name="Ancestor">Ancestor=九玄七祖</param>
        /// <param name="AncestorLastname">AncestorLastname=超度亡者姓氏</param>
        /// <param name="AncestorAddress">AncestorAddress=祖先牌位地址</param>
        /// <param name="Deceased">Deceased=功德迴向往生者</param>
        /// <param name="DeceasedName">DeceasedName=超度亡者姓名</param>
        /// <param name="DeceasedAddress">DeceasedAddress=祖先牌位地址</param>
        /// <param name="Landlord">Landlord=地祇主</param>
        /// <param name="LandlordNum">LandlordNum=地祇主數量</param>
        /// <param name="Baby">Baby=嬰靈</param>
        /// <param name="BabyNum">BabyNum=嬰靈數量</param>
        /// <param name="Animal">Animal=動物靈</param>
        /// <param name="AnimalNum">AnimalNum=動物靈數量</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Address">Address=地址</param>
        /// <param name="PurdueNum">PurdueNum=普度數量</param>
        /// <param name="RiceNum">RiceNum=白米數量</param>
        /// <param name="mMoneyNum">mMoneyNum=金紙數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addpurdue_h(int applicantID, string AdminID, string Merit, string MeritText, string Life, string Redress, string Creditor, string Ancestor, string AncestorLastname, string AncestorAddress, string AncestorCounty, string AncestorDist, string AncestorAddr, string AncestorZipCode, string Deceased, string DeceasedName, string DeceasedAddress, string DeceasedCounty, string DeceasedDist, string DeceasedAddr, string DeceasedZipCode
            , string Landlord, string LandlordNum, string Baby, string BabyNum, string Animal, string AnimalNum, string Name, string PurdueType, string PurdueString, string Address, string County, string dist, string Addr, string zipCode, int PurdueNum, int RiceNum, int mMoneyNum, string Remark)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_h_info(ApplicantID, AdminID, Merit, MeritText, Life, Redress, Creditor, Ancestor, AncestorLastname, AncestorAddress, AncestorCounty, Ancestordist, AncestorAddr, AncestorZipCode, Deceased, DeceasedName, DeceasedAddress, DeceasedCounty, Deceaseddist, DeceasedAddr, DeceasedZipCode, Landlord, LandlordNum, Baby, BabyNum, Animal, AnimalNum, Name, " +
                "PurdueType, PurdueString, Address, Addr, County, dist, ZipCode, PurdueNum, RiceNum, mMoneyNum, Remark, CreateDate, CreateDateString) values(@ApplicantID, @AdminID, @Merit, @MeritText, @Life, @Redress, @Creditor, @Ancestor, @AncestorLastname, @AncestorAddress, @AncestorCounty, @Ancestordist, @AncestorAddr, @AncestorZipCode, @Deceased, @DeceasedName, @DeceasedAddress, @DeceasedCounty, @Deceaseddist, @DeceasedAddr, @DeceasedZipCode, @Landlord, @LandlordNum, @Baby, " +
                "@BabyNum, @Animal, @AnimalNum, @Name, @PurdueType, @PurdueString, @Address, @Addr, @County, @dist, @ZipCode, @PurdueNum, @RiceNum, @mMoneyNum, @Remark, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Merit", Merit);
            Adapter.AddParameterToSelectCommand("@MeritText", MeritText);

            Adapter.AddParameterToSelectCommand("@Life", Life);
            Adapter.AddParameterToSelectCommand("@Redress", Redress);

            Adapter.AddParameterToSelectCommand("@Creditor", Creditor);

            Adapter.AddParameterToSelectCommand("@Ancestor", Ancestor);
            Adapter.AddParameterToSelectCommand("@AncestorLastname", AncestorLastname);
            Adapter.AddParameterToSelectCommand("@AncestorAddress", AncestorAddress);
            Adapter.AddParameterToSelectCommand("@AncestorAddr", AncestorAddr);
            Adapter.AddParameterToSelectCommand("@AncestorCounty", AncestorCounty);
            Adapter.AddParameterToSelectCommand("@Ancestordist", AncestorDist);
            Adapter.AddParameterToSelectCommand("@AncestorZipCode", AncestorZipCode);

            Adapter.AddParameterToSelectCommand("@Deceased", Deceased);
            Adapter.AddParameterToSelectCommand("@DeceasedName", DeceasedName);
            Adapter.AddParameterToSelectCommand("@DeceasedAddress", DeceasedAddress);
            Adapter.AddParameterToSelectCommand("@DeceasedAddr", DeceasedAddr);
            Adapter.AddParameterToSelectCommand("@DeceasedCounty", DeceasedCounty);
            Adapter.AddParameterToSelectCommand("@Deceaseddist", DeceasedDist);
            Adapter.AddParameterToSelectCommand("@DeceasedZipCode", DeceasedZipCode);

            Adapter.AddParameterToSelectCommand("@Landlord", Landlord);
            Adapter.AddParameterToSelectCommand("@LandlordNum", LandlordNum);

            Adapter.AddParameterToSelectCommand("@Baby", Baby);
            Adapter.AddParameterToSelectCommand("@BabyNum", BabyNum);

            Adapter.AddParameterToSelectCommand("@Animal", Animal);
            Adapter.AddParameterToSelectCommand("@AnimalNum", AnimalNum);

            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", zipCode);

            Adapter.AddParameterToSelectCommand("@PurdueNum", PurdueNum);
            Adapter.AddParameterToSelectCommand("@RiceNum", RiceNum);
            Adapter.AddParameterToSelectCommand("@mMoneyNum", mMoneyNum);

            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// <param name="Sendback">Sendback=是否領回: 1-領回 2-捐贈</param>
        /// <param name="Sendback_thx">Sendback_thx=感謝狀處理方式: 0-燒化 1-寄出</param>
        /// </summary>
        public int addpurdue_wude(int applicantID, string AdminID, string Name, string Sex, string Birth, string Phone, string HomeNum, string Email, string Address, string Count, string Remark, string Sendback, string Sendback_thx)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_wu_info(ApplicantID, AdminID, Name, Sex, Birth, Mobile, HomeNum, Email, Address, Count, Sendback, Sendback_thx, Remark, CreateDate) values(@ApplicantID, @AdminID, @Name, @Sex, @Birth, @Mobile" +
                ", @HomeNum, @Email, @Address, @Count, @Sendback, @Sendback_thx, @Remark, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@Sendback_thx", Sendback_thx);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// <param name="Sendback">Sendback=是否領回: 1-領回 0-捐贈</param>
        /// </summary>
        public int addpurdue_wu(int applicantID, string Name, string Mobile, string PurdueType, string PurdueString, string Sex, string Birth, string HomeNum, string Email, string Address, string Addr, string County, string Dist, string ZipCode, string Count, string Remark)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_wu_info(ApplicantID, AdminID, Name, Mobile, PurdueType, PurdueString, Sex, Birth, HomeNum, Email, Address, Addr, County, dist, ZipCode, Count, Remark, CreateDate, CreateDateString) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @PurdueType, @PurdueString, @Sex, @Birth, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Count, @Remark, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Count">Count=普品數量 defult:0</param>
        /// <param name="GoldPaperCount">GoldPaperCount=金紙數量 defult:0</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// </summary>
        public int addpurdue_Fu(int applicantID, string Name, string Name2, string Mobile, string PurdueType, string PurdueString, string Count, string GoldPaperCount, string Address, string Addr, string County, string Dist, string ZipCode, string FirstName, string DeathName)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_Fu_info(ApplicantID, AdminID, Name, Name2, Mobile, PurdueType, PurdueString, Count, GoldPaperCount, Address, Addr, County, dist, ZipCode, FirstName, DeathName, CreateDate, CreateDateString) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @PurdueType, @PurdueString, @Count, @GoldPaperCount, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @DeathName, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 8);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@GoldPaperCount", GoldPaperCount);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園大廟景福宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Address">Address=祈福人地址(全)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// </summary>
        public int addpurdue_Jing(int applicantID, string Name, string Name2, string Mobile, string PurdueType, string PurdueString, string Address, string Addr, string County, string Dist, string ZipCode, string FirstName)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_Jing_info(ApplicantID, AdminID, Name, Name2, Mobile, PurdueType, PurdueString, Address, Addr, County, dist, ZipCode, FirstName, CreateDate, CreateDateString) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @PurdueType, @PurdueString, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 9);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Email">Email</param>
        /// <param name="Address">Address=祈福人地址(全)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="Count">Count=普度組數</param>
        /// </summary>
        public int addpurdue_Luer(int applicantID, string Name, string Name2, string Mobile, string Email, string PurdueType, string PurdueString, string Address, string Addr, string County, string Dist, string ZipCode, string Count)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Purdue_Luer_info(ApplicantID, AdminID, Name, Name2, Mobile, Email, PurdueType, PurdueString, Address, Addr, County, dist, ZipCode, Count, CreateDate, CreateDateString) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @Email, @PurdueType, @PurdueString, @Address, @Addr, @County, @dist, @ZipCode, @Count, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 10);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Count", Count);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="Sendback">Sendback=寄送方式 0-不寄回(會轉送給弱勢團體) 1-寄回(加收運費250元)</param>
        /// <param name="rName">rName=收件人姓名</param>
        /// <param name="rMobile">rMobile=收件人電話</param>
        /// <param name="rAddress">rAddress=收件人地址(全)</param>
        /// <param name="rAddr">rAddr=收件人地址(部分)</param>
        /// <param name="rCounty">rCounty=收件人縣市</param>
        /// <param name="rDist">rDist=收件人區域</param>
        /// <param name="rZipCode">rZipCode=收件人郵遞區號</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// <param name="Birthday">Birthday=亡者農曆生日</param>
        /// <param name="Deathday">Deathday=亡者死亡日期</param>
        /// <param name="DeathAge">DeathAge=亡者年紀</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// </summary>
        public int addpurdue_da(int applicantID, string Name, string Name2, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Sendback, 
            string rName, string rMobile, string rAddr, string rCounty, string rDist, string rZipCode, string DeathName, string Birthday, string DeathLeapMonth, 
            string DeathBirthTime, string Deathday, string DeathAge, string FirstName, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_da_info(ApplicantID, AdminID, Name, Name2, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, Sendback, rName, rMobile, rAddress, rAddr, rCounty, rdist, rZipCode, DeathName, " +
                "Birthday, DeathLeapMonth, DeathBirthTime, Deathday, DeathAge, FirstName, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, " +
                "@Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Sendback, @rName, @rMobile, @rAddress, @rAddr, @rCounty, @rdist, @rZipCode, @DeathName, @Birthday, " +
                "@DeathLeapMonth, @DeathBirthTime, @Deathday, @DeathAge, @FirstName, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 3);

            Cost += Sendback == "1" ? 250 : 0;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 3);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@rName", rName);
            Adapter.AddParameterToSelectCommand("@rMobile", rMobile);
            Adapter.AddParameterToSelectCommand("@rAddress", rCounty + rDist + rAddr);
            Adapter.AddParameterToSelectCommand("@rAddr", rAddr);
            Adapter.AddParameterToSelectCommand("@rCounty", rCounty);
            Adapter.AddParameterToSelectCommand("@rdist", rDist);
            Adapter.AddParameterToSelectCommand("@rZipCode", rZipCode);

            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);
            Adapter.AddParameterToSelectCommand("@Birthday", Birthday);
            Adapter.AddParameterToSelectCommand("@DeathLeapMonth", DeathLeapMonth);
            Adapter.AddParameterToSelectCommand("@DeathBirthTime", DeathBirthTime);
            Adapter.AddParameterToSelectCommand("@Deathday", Deathday);
            Adapter.AddParameterToSelectCommand("@DeathAge", DeathAge);
            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立新港奉天宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Merit">Merit=功德主</param>
        /// <param name="MeritText">MeritText=功德主內容</param>
        /// <param name="Life">Life=祝燈延壽消災</param>
        /// <param name="Redress">Redress=四十九愿解冤釋結</param>
        /// <param name="Creditor">Creditor=冤親債主</param>
        /// <param name="Ancestor">Ancestor=九玄七祖</param>
        /// <param name="AncestorLastname">AncestorLastname=超度亡者姓氏</param>
        /// <param name="AncestorAddress">AncestorAddress=祖先牌位地址</param>
        /// <param name="Deceased">Deceased=功德迴向往生者</param>
        /// <param name="DeceasedName">DeceasedName=超度亡者姓名</param>
        /// <param name="DeceasedAddress">DeceasedAddress=祖先牌位地址</param>
        /// <param name="Landlord">Landlord=地祇主</param>
        /// <param name="LandlordNum">LandlordNum=地祇主數量</param>
        /// <param name="Baby">Baby=嬰靈</param>
        /// <param name="BabyNum">BabyNum=嬰靈數量</param>
        /// <param name="Animal">Animal=動物靈</param>
        /// <param name="AnimalNum">AnimalNum=動物靈數量</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Address">Address=地址</param>
        /// <param name="PurdueNum">PurdueNum=普度數量</param>
        /// <param name="RiceNum">RiceNum=白米數量</param>
        /// <param name="mMoneyNum">mMoneyNum=金紙數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addpurdue_h(int applicantID, string AdminID, string Merit, string MeritText, string Life, string Redress, string Creditor, string Ancestor, string AncestorLastname, 
            string AncestorAddress, string AncestorCounty, string AncestorDist, string AncestorAddr, string AncestorZipCode, string Deceased, string DeceasedName, string DeceasedAddress, 
            string DeceasedCounty, string DeceasedDist, string DeceasedAddr, string DeceasedZipCode, string Landlord, string LandlordNum, string Baby, string BabyNum, string Animal, 
            string AnimalNum, string PurdueType, string PurdueString, string Name, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, 
            string oversea, string County, string dist, string Addr, string zipCode, int PurdueNum, int RiceNum, int mMoneyNum, string Remark, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_h_info(ApplicantID, AdminID, Merit, MeritText, Life, Redress, Creditor, Ancestor, AncestorLastname, " +
                "AncestorAddress, AncestorCounty, Ancestordist, AncestorAddr, AncestorZipCode, Deceased, DeceasedName, DeceasedAddress, DeceasedCounty, Deceaseddist, DeceasedAddr, " +
                "DeceasedZipCode, Landlord, LandlordNum, Baby, BabyNum, Animal, AnimalNum, PurdueType, PurdueString, Name, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, " +
                "oversea, Address, Addr, County, dist, ZipCode, PurdueNum, RiceNum, mMoneyNum, Cost, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Merit, @MeritText, @Life, @Redress, @Creditor, @Ancestor, @AncestorLastname, @AncestorAddress, @AncestorCounty, @Ancestordist, " +
                "@AncestorAddr, @AncestorZipCode, @Deceased, @DeceasedName, @DeceasedAddress, @DeceasedCounty, @Deceaseddist, @DeceasedAddr, @DeceasedZipCode, @Landlord, " +
                "@LandlordNum, @Baby, @BabyNum, @Animal, @AnimalNum, @PurdueType, @PurdueString, @Name, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @oversea, " +
                "@Address, @Addr, @County, @dist, @ZipCode, @PurdueNum, @RiceNum, @mMoneyNum, @Cost, @Remark, @CreateDate)";

            int Cost = 0;
            Cost = getTotal(Merit, Life, Redress, Creditor, Ancestor, Deceased, Landlord, LandlordNum, Baby, BabyNum, Animal, AnimalNum, PurdueNum.ToString(), RiceNum.ToString(),
                mMoneyNum.ToString());

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Merit", Merit);
            Adapter.AddParameterToSelectCommand("@MeritText", MeritText);

            Adapter.AddParameterToSelectCommand("@Life", Life);
            Adapter.AddParameterToSelectCommand("@Redress", Redress);

            Adapter.AddParameterToSelectCommand("@Creditor", Creditor);

            Adapter.AddParameterToSelectCommand("@Ancestor", Ancestor);
            Adapter.AddParameterToSelectCommand("@AncestorLastname", AncestorLastname);
            Adapter.AddParameterToSelectCommand("@AncestorAddress", AncestorAddress);
            Adapter.AddParameterToSelectCommand("@AncestorAddr", AncestorAddr);
            Adapter.AddParameterToSelectCommand("@AncestorCounty", AncestorCounty);
            Adapter.AddParameterToSelectCommand("@Ancestordist", AncestorDist);
            Adapter.AddParameterToSelectCommand("@AncestorZipCode", AncestorZipCode);

            Adapter.AddParameterToSelectCommand("@Deceased", Deceased);
            Adapter.AddParameterToSelectCommand("@DeceasedName", DeceasedName);
            Adapter.AddParameterToSelectCommand("@DeceasedAddress", DeceasedAddress);
            Adapter.AddParameterToSelectCommand("@DeceasedAddr", DeceasedAddr);
            Adapter.AddParameterToSelectCommand("@DeceasedCounty", DeceasedCounty);
            Adapter.AddParameterToSelectCommand("@Deceaseddist", DeceasedDist);
            Adapter.AddParameterToSelectCommand("@DeceasedZipCode", DeceasedZipCode);

            Adapter.AddParameterToSelectCommand("@Landlord", Landlord);
            Adapter.AddParameterToSelectCommand("@LandlordNum", LandlordNum);

            Adapter.AddParameterToSelectCommand("@Baby", Baby);
            Adapter.AddParameterToSelectCommand("@BabyNum", BabyNum);

            Adapter.AddParameterToSelectCommand("@Animal", Animal);
            Adapter.AddParameterToSelectCommand("@AnimalNum", AnimalNum);

            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", zipCode);

            Adapter.AddParameterToSelectCommand("@PurdueNum", PurdueNum);
            Adapter.AddParameterToSelectCommand("@RiceNum", RiceNum);
            Adapter.AddParameterToSelectCommand("@mMoneyNum", mMoneyNum);

            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        public int getTotal(string Merit, string Life, string Redress, string Creditor, string Ancestor, string Deceased, string Landlord, string LandlordNum, string Baby,
            string BabyNum, string Animal, string AnimalNum, string PurdueNum, string RiceNum, string mMoneyNum)
        {
            int result = 0;

            //功德主 $3000
            if (Merit == "1")
            {
                result += 3000;
            }

            //祝燈延壽消災 $600
            if (Life == "1")
            {
                result += 600;
            }

            //四十九愿解冤釋結 $600
            if (Redress == "1")
            {
                result += 600;
            }

            //冤親債主 $500
            if (Creditor == "1")
            {
                result += 500;
            }

            //九玄七祖 $500
            if (Ancestor == "1")
            {
                result += 500;
            }

            //功德迴向往生者 $500
            if (Deceased == "1")
            {
                result += 500;
            }

            //地祇主 $500
            if (Landlord == "1")
            {
                result += (500 * int.Parse(LandlordNum));
            }

            //嬰靈 $500
            if (Baby == "1")
            {
                result += (500 * int.Parse(BabyNum));
            }

            //動物靈 $500
            if (Animal == "1")
            {
                result += (500 * int.Parse(AnimalNum));
            }

            //普頓品 $1000
            result += (1000 * int.Parse(PurdueNum));

            //白米 $200
            result += (200 * int.Parse(RiceNum));

            //金紙 $200
            result += (200 * int.Parse(mMoneyNum));

            return result;
        }

        /// <summary>
        /// 建立北港武德宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addpurdue_wu(int applicantID, string Name, string Mobile, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Sex, string HomeNum, string Email, string Count, string Addr, string County, string Dist, string ZipCode, string Remark, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_wu_info(ApplicantID, AdminID, Name, Mobile, Cost, PurdueType, PurdueString, Sex, HomeNum, Email, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @PurdueType, @PurdueString, @Sex, @HomeNum, @Email, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 6);
            Cost = Cost * int.Parse(Count);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Count">Count=普品數量 defult:0</param>
        /// <param name="GoldPaperCount">GoldPaperCount=金紙數量 defult:0</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// </summary>
        public int addpurdue_Fu(int applicantID, string Name, string Name2, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Count, string GoldPaperCount, string Addr, string County, string Dist, string ZipCode, string FirstName, string DeathName, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Fu_info(ApplicantID, AdminID, Name, Name2, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, GoldPaperCount, Address, Addr, County, dist, ZipCode, FirstName, DeathName, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @GoldPaperCount, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @DeathName, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 8);

            if (PurdueType == "1")
            {
                int c = 1;
                int.TryParse(Count, out c);
                Cost = Cost * c;
            }
            else
            {
                int c = 0;
                int.TryParse(Count, out c);

                int gc = 0;
                int.TryParse(GoldPaperCount, out gc);

                Cost += 1500 * c; //加購普品
                Cost += 300 * gc; //加購金紙
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 8);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@GoldPaperCount", GoldPaperCount);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園大廟景福宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Address">Address=祈福人地址(全)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// </summary>
        public int addpurdue_Jing(int applicantID, string Name, string Name2, string Mobile, string PurdueType, string PurdueString, string Address, string Addr, string County, string Dist, string ZipCode, string FirstName, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Jing_info(ApplicantID, AdminID, Name, Name2, Mobile, PurdueType, PurdueString, Address, Addr, County, dist, ZipCode, FirstName, CreateDate, CreateDateString) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @PurdueType, @PurdueString, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 9);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Name2">Name2=祈福人2姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Email">Email</param>
        /// <param name="Address">Address=祈福人地址(全)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="Count">Count=普度組數</param>
        /// </summary>
        public int addpurdue_Luer(int applicantID, string Name, string Name2, string Mobile, string Sex, string PurdueType, string PurdueString, string Email, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Luer_info(ApplicantID, AdminID, Name, Name2, Mobile, Cost, Sex, PurdueType, PurdueString, Email, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "           values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @Email, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 10);

            int c = 1;
            int.TryParse(Count, out c);
            Cost = Cost * c;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 10);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }


        /// <summary>
        /// 建立桃園威天宮普度資料
        /// </summary>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="BirthMonth"></param>
        /// <param name="Age"></param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Count">Count=普度品數量</param>
        /// <param name="Count_3rice">Count_3rice=普度白米3台斤數量</param>
        /// <param name="Count_50rice">Count_50rice=普度白米50台斤數量</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode"></param>
        /// <param name="Remark">Remark=備註</param>
        /// 
        /// <param name="PurdueItem">PurdueItem=超拔項目</param>
        /// <param name="DeathName">DeathName=名字(府君、夫人、無緣子女、寵物姓名)</param>
        /// <param name="FirstName">FirstName=姓氏(顯考-公、本姓-氏、姓氏-氏</param>
        /// <param name="MomName">MomName=夫姓(母)</param>
        /// <param name="LastName">LastName=名字(府君、夫人、無緣子女、寵物姓名)</param>
        /// <param name="Reason">Reason=超薦事由</param>
        /// <param name="LicenseNum">LicenseNum=車牌號碼</param>
        /// <param name="DeathAddr">DeathAddr=被超薦者地址(部分)</param>
        /// <param name="DeathCounty">DeathCounty=被超薦者縣市</param>
        /// <param name="DeathDist">DeathDist=被超薦者區域</param>
        /// <param name="DeathZipCode">DeathZipCode=被超薦者郵遞區號</param>
        /// <param name="PurdueItem1">PurdueItem1=超拔項目</param>
        /// <param name="DeathName1">DeathName1=名字(府君、夫人、無緣子女、寵物姓名)</param>
        /// <param name="FirstName1">FirstName1=姓氏(顯考-公、本姓-氏、姓氏-氏</param>
        /// <param name="MomName1">MomName1=夫姓(母)</param>
        /// <param name="LastName1">LastName1=名字(府君、夫人、無緣子女、寵物姓名)</param>
        /// <param name="Reason1">Reason1=超薦事由</param>
        /// <param name="LicenseNum1">LicenseNum1=車牌號碼</param>
        /// <param name="DeathAddr1">DeathAddr1=被超薦者地址(部分)</param>
        /// <param name="DeathCounty1">DeathCounty1=被超薦者縣市</param>
        /// <param name="DeathDist1">DeathDist1=被超薦者區域</param>
        /// <param name="DeathZipCode1">DeathZipCode1=被超薦者郵遞區號</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int addpurdue_ty(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, int Count_3rice, int Count_50rice, string Addr, string County, string Dist, string ZipCode, 
            string Remark, string PurdueItem, string DeathName, string FirstName, string MomName, string LastName, string Reason, string LicenseNum, string DeathAddr, 
            string DeathCounty, string DeathDist, string DeathZipCode, string PurdueItem1, string DeathName1, string FirstName1, string MomName1, string LastName1, string Reason1, 
            string LicenseNum1, string DeathAddr1, string DeathCounty1, string DeathDist1, string DeathZipCode1, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, Count, Count_3rice, Count_50rice, Address, Addr, County, dist, ZipCode, Remark, PurdueItem, DeathName, FirstName, MomName, " +
                "LastName, Reason, LicenseNum, DeathAddress, DeathAddr, DeathCounty, Deathdist, DeathZipCode, PurdueItem1, DeathName1, FirstName1, MomName1, LastName1, Reason1, " +
                "LicenseNum1, DeathAddress1, DeathAddr1, DeathCounty1, Deathdist1, DeathZipCode1, Status, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@Count, @Count_3rice, @Count_50rice, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @PurdueItem, @DeathName, @FirstName, @MomName, @LastName, @Reason, " +
                "@LicenseNum, @DeathAddress, @DeathAddr, @DeathCounty, @Deathdist, @DeathZipCode, @PurdueItem1, @DeathName1, @FirstName1, @MomName1, @LastName1, @Reason1, " +
                "@LicenseNum1, @DeathAddress1, @DeathAddr1, @DeathCounty1, @Deathdist1, @DeathZipCode1, @Status, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 14);

            switch (PurdueType)
            {
                case "1":
                    //贊普
                    Cost = Cost * Count;
                    break;
                case "14":
                    //孝道功德主 兩項普渡項目$3000/2
                    Cost = Cost * Count;
                    break;
                case "15":
                    //光明功德主
                    Cost = Cost * Count;
                    break;
                case "16":
                    //發心功德主
                    Cost = Cost * Count;
                    break;
                case "18":
                    //白米50台斤
                    Cost = Cost * Count_50rice;
                    break;
                case "19":
                    //白米3台斤
                    Cost = Cost * Count_3rice;
                    break;
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Count_3rice", Count_3rice);
            Adapter.AddParameterToSelectCommand("@Count_50rice", Count_50rice);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);

            Adapter.AddParameterToSelectCommand("@PurdueItem", PurdueItem);
            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);
            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@MomName", MomName);
            Adapter.AddParameterToSelectCommand("@LastName", LastName);
            Adapter.AddParameterToSelectCommand("@Reason", Reason);
            Adapter.AddParameterToSelectCommand("@LicenseNum", LicenseNum);
            Adapter.AddParameterToSelectCommand("@DeathAddress", DeathCounty + DeathDist + DeathAddr);
            Adapter.AddParameterToSelectCommand("@DeathAddr", DeathAddr);
            Adapter.AddParameterToSelectCommand("@DeathCounty", DeathCounty);
            Adapter.AddParameterToSelectCommand("@Deathdist", DeathDist);
            Adapter.AddParameterToSelectCommand("@DeathZipCode", DeathZipCode);

            Adapter.AddParameterToSelectCommand("@PurdueItem1", PurdueItem1);
            Adapter.AddParameterToSelectCommand("@DeathName1", DeathName1);
            Adapter.AddParameterToSelectCommand("@FirstName1", FirstName1);
            Adapter.AddParameterToSelectCommand("@MomName1", MomName1);
            Adapter.AddParameterToSelectCommand("@LastName1", LastName1);
            Adapter.AddParameterToSelectCommand("@Reason1", Reason1);
            Adapter.AddParameterToSelectCommand("@LicenseNum1", LicenseNum1);
            Adapter.AddParameterToSelectCommand("@DeathAddress1", DeathCounty1 + DeathDist1 + DeathAddr1);
            Adapter.AddParameterToSelectCommand("@DeathAddr1", DeathAddr1);
            Adapter.AddParameterToSelectCommand("@DeathCounty1", DeathCounty1);
            Adapter.AddParameterToSelectCommand("@Deathdist1", DeathDist1);
            Adapter.AddParameterToSelectCommand("@DeathZipCode1", DeathZipCode1);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Count">Count=數量 defult:1</param>
        /// <param name="Count_rice">Count_rice=捐獻白米數量 defult:0</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addpurdue_Fw(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, int Count_rice, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Fw_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Count_rice, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Count_rice, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 15);
            if (Count_rice > 0)
            {
                Cost += Count_rice * 200;
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 15);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Count_rice", Count_rice);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Count">Count=普品數量 defult:0</param>
        /// <param name="GoldPaperCount">GoldPaperCount=金紙數量 defult:0</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="DeathName">DeathName=亡者姓名</param>
        /// </summary>
        public int addpurdue_Fw(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Count, string Addr, string County, string Dist, string ZipCode, string FirstName, string DeathName, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Fu_info(ApplicantID, AdminID, Name, Name2, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, GoldPaperCount, Address, Addr, County, dist, ZipCode, FirstName, DeathName, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Name2, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @GoldPaperCount, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @DeathName, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 15);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 8);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮普度資料
        /// </summary>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="DeathName">DeathName=往生者姓名</param>
        /// <param name="Deathday">Deathday=往生日期</param>
        /// <param name="DeathBirth">DeathBirth=農曆生日</param>
        /// <param name="DeathLeapMonth">DeathLeapMonth=閏月 Y-是 N-否</param>
        /// <param name="DeathBirthTime">DeathBirthTime=農曆時辰</param>
        /// <param name="DeathZodiac">DeathZodiac=生肖</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="DeathAddr">DeathAddr=地址(部分)</param>
        /// <param name="DeathCounty">DeathCounty=縣市</param>
        /// <param name="DeathDist">DeathDist=區域</param>
        /// <param name="DeathZipCode">DeathZipCode=郵遞區號</param>
        /// <returns></returns>
        public int addpurdue_dh(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string DeathName, string Deathday,
            string DeathBirth, string DeathLeapMonth, string DeathBirthTime, string DeathBirthMonth, string DeathAge, string DeathZodiac, string FirstName, string DeathAddr, 
            string DeathCounty, string DeathDist, string DeathZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_dh_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, DeathName, Deathday, DeathBirth, DeathLeapMonth, DeathBirthTime, DeathBirthMonth, " +
                "DeathAge, DeathZodiac, FirstName, DeathAddress, DeathAddr, DeathCounty, Deathdist, DeathZipCode, Status, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@Count, @Address, @Addr, @County, @dist, @ZipCode, @DeathName, @Deathday, @DeathBirth, @DeathLeapMonth, @DeathBirthTime, @DeathBirthMonth, @DeathAge, @DeathZodiac, " +
                "@FirstName, @DeathAddress, @DeathAddr, @DeathCounty, @Deathdist, @DeathZipCode, @Status, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 16);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 16);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@DeathName", DeathName);
            Adapter.AddParameterToSelectCommand("@Deathday", Deathday);
            Adapter.AddParameterToSelectCommand("@DeathBirth", DeathBirth);
            Adapter.AddParameterToSelectCommand("@DeathLeapMonth", DeathLeapMonth);
            Adapter.AddParameterToSelectCommand("@DeathBirthTime", DeathBirthTime);
            Adapter.AddParameterToSelectCommand("@DeathBirthMonth", DeathBirthMonth);
            Adapter.AddParameterToSelectCommand("@DeathAge", DeathAge);
            Adapter.AddParameterToSelectCommand("@DeathZodiac", DeathZodiac);
            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@DeathAddress", DeathCounty + DeathDist + DeathAddr);
            Adapter.AddParameterToSelectCommand("@DeathAddr", DeathAddr);
            Adapter.AddParameterToSelectCommand("@DeathCounty", DeathCounty);
            Adapter.AddParameterToSelectCommand("@Deathdist", DeathDist);
            Adapter.AddParameterToSelectCommand("@DeathZipCode", DeathZipCode);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="HomeNum">HomeNum=市話(選填)</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// </summary>
        public int addpurdue_Lk(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string HomeNum, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_Lk_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, HomeNum, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @HomeNum, @Count, @Address, @Addr, @County, @dist, @ZipCode,  @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 21);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 21);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// <param name="FirstName">FirstName=姓氏</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addpurdue_ma(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Email, string Addr, string County, string Dist, string ZipCode, string FirstName, string Remark, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_ma_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Email, Address, Addr, County, dist, ZipCode, FirstName, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Email, @Address, @Addr, @County, @dist, @ZipCode, @FirstName, @Remark, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 23);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 23);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@FirstName", FirstName);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);

            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鎮瀾買足網普度資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="PurdueType">PurdueType=普度項目 1-贊普 2-九玄七祖 3-指名亡者 4-本境地基主 5-冤親債主 6-嬰靈 7-為國捐軀三軍將士英靈 8-鐵公路車傷死亡眾魂 9-本境水難傷亡諸魂 10-本境男女無嗣孤魂等眾 11-六畜往生 12-法船 13-壽生錢 14-孝道功德主 15-光明功德主 16-發心功德主 17-誦經迴向 18-白米50台斤 19-白米3台斤 20-寵物普度-毛小孩 21-寵物普度-喵星人</param>
        /// <param name="PurdueString">PurdueString=普度項目字串</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Addr">Addr=祈福人地址(部分)</param>
        /// <param name="County">County=祈福人縣市</param>
        /// <param name="Dist">Dist=祈福人區域</param>
        /// <param name="ZipCode">ZipCode=祈福人郵遞區號</param>
        /// </summary>
        public int addpurdue_mazu(int applicantID, string Name, string Mobile, string Sex, string PurdueType, string PurdueString, string oversea, string Birth,
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Purdue_mazu_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, PurdueType, PurdueString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @PurdueType, @PurdueString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, " +
                "@Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetPurdueCost(PurdueType, 30);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 30);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@PurdueType", PurdueType);
            Adapter.AddParameterToSelectCommand("@PurdueString", PurdueString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料(下元補庫)
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// <param name="join_date">join_date=參加日期</param>
        /// </summary>
        public int addSupplies_wude(int applicantID, string AdminID, string Name, string Sex, string Birth, string Phone, string HomeNum, string Email, string Address, string Count, string Remark, string join_date)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Supplies_wu_info(ApplicantID, AdminID, Name, Sex, Birth, Mobile, HomeNum, Email, Address, Count, Join_date, Remark, CreateDate) values(@ApplicantID, @AdminID, @Name, @Sex, @Birth, @Mobile" +
                ", @HomeNum, @Email, @Address, @Count, @Join_date, @Remark, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Join_date", join_date);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=農曆月份</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// <param name="join_date">join_date=參加日期</param>
        /// </summary>
        public int addSupplies_wude2(int applicantID, string AdminID, string Name, string Sex, string Birth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Phone, string HomeNum, string Email, string County, string dist, string Addr, string ZipCode, string Count, string Remark, string join_date)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Supplies_wu_info2(ApplicantID, AdminID, Name, Sex, Birth, BirthTime, BirthMonth, Age, Zodiac, Mobile, HomeNum, Email, County, dist, Addr, Address, ZipCode, Count, Remark, Join_date, CreateDateString, CreateDate) values(@ApplicantID, @AdminID, @Name, @Sex" +
                ", @Birth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Mobile, @HomeNum, @Email, @County, @dist, @Addr, @Address, @ZipCode, @Count, @Remark, @Join_date, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Join_date", join_date);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        //補財庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-代燒金紙 7-招財補運 8-招財補運九九重陽升級版 9-補財庫 10-財神賜福-消災補庫法會 11-地母廟-赦罪解業
        //          12-地母廟-補財庫 13-地母廟-赦罪解業+補財庫 14-草屯敦和宮-赦罪解業 15-草屯敦和宮-補財庫 16-草屯敦和宮-赦罪解業+補財庫 17-紫南宮-赦罪解業 18-紫南宮-補財庫
        //          19-紫南宮-赦罪解業+補財庫 20-天公生招財補運
        public static int GetSuppliesCost(string SuppliesType, int AdminID)
        {
            int result = 0;

            switch (AdminID)
            {
                case 6:
                    //北港武德宮         
                    switch (SuppliesType)
                    {
                        case "1":
                            //下元補庫
                            result = 650;
                            break;
                        case "2":
                            //呈疏補庫
                            result = 650;
                            break;
                        case "3":
                            //企業補財庫
                            result = 1300;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (SuppliesType)
                    {
                        case "4":
                            //天赦日補運
                            result = 1280;
                            break;
                        case "7":
                            //招財補運
                            result = 1280;
                            break;
                        case "8":
                            //招財補運九九重陽升級版
                            result = 5880;
                            break;
                        case "20":
                            //天公生招財補運
                            result = 1680;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (SuppliesType)
                    {
                        case "9":
                            //補財庫
                            result = 1500;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (SuppliesType)
                    {
                        case "4":
                            //天赦日招財補運
                            result = 500;
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (SuppliesType)
                    {
                        case "5":
                            //天赦日祭改
                            result = 2180;
                            break;
                        case "6":
                            //代燒金紙
                            result = 898;
                            break;
                        case "10":
                            //財神賜福-消災補庫法會
                            result = 2180;
                            break;
                    }
                    break;
                case 33:
                    //神霄玉府赦罪補庫
                    switch (SuppliesType)
                    {
                        case "13":
                            result = 2800;
                            break;
                        case "16":
                            result = 2800;
                            break;
                        case "19":
                            result = 2800;
                            break;
                        default:
                            result = 1500;
                            break;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 建立桃園威天宮關聖帝君聖誕資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="EmperorGuanshengType">EmperorGuanshengType=關聖帝君聖誕項目 1-忠義狀功德主 2-富貴狀功德主 3-招財補運 4-招財補運紀念版</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addEmperorGuansheng_ty(int applicantID, string Name, string Mobile, string Sex, string EmperorGuanshengType, string EmperorGuanshengString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..EmperorGuansheng_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, EmperorGuanshengType, EmperorGuanshengString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @EmperorGuanshengType, @EmperorGuanshengString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            switch (EmperorGuanshengType)
            {
                case "1":
                    //忠義狀功德主
                    Cost = 800;
                    break;
                case "2":
                    //富貴狀功德主
                    Cost = 3000;
                    break;
                case "3":
                    //招財補運
                    Cost = 1280;
                    break;
                case "4":
                    //招財補運紀念版
                    Cost = 5880;
                    break;
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@EmperorGuanshengType", EmperorGuanshengType);
            Adapter.AddParameterToSelectCommand("@EmperorGuanshengString", EmperorGuanshengString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮靈寶禮斗資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LingbaolidouType">LingbaolidouType=靈寶禮斗項目 1-靈寶禮斗-功德主 2-靈寶禮斗-隨喜功德主 3-靈寶禮斗-消災解厄科儀</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=生日月份(農曆)</param>
        /// <param name="Age">Age=年齡(農曆)</param>
        /// <param name="Zodiac">Zodiac=生肖(農曆)</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLingbaolidou_ma(int applicantID, string Name, string Mobile, string Sex, string LingbaolidouType, string LingbaolidouString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lingbaolidou_ma_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LingbaolidouType, LingbaolidouString, oversea, " +
                "Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LingbaolidouType, @LingbaolidouString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, " +
                "@Zodiac, @sBirth, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            switch (LingbaolidouType)
            {
                case "1":
                    //靈寶禮斗-功德主
                    Cost = 6800;
                    break;
                case "2":
                    //靈寶禮斗-隨喜功德主
                    Cost = 1000;
                    break;
                case "3":
                    //靈寶禮斗-消災解厄科儀
                    Cost = 550;
                    break;
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 23);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LingbaolidouType", LingbaolidouType);
            Adapter.AddParameterToSelectCommand("@LingbaolidouString", LingbaolidouString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮七朝清醮資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addTaoistJiaoCeremony_da(int applicantID, string Name, string Name2, string Name3, string Name4, string Name5, string Name6, string Mobile, string Sex, 
            string TaoistJiaoCeremonyType, string TaoistJiaoCeremonyString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, 
            string Zodiac, string sBirth, int Count, string Addr, string County, string Dist, string ZipCode, string Sendback, string rName, string rMobile, string rAddr, 
            string rCounty, string rDist, string rZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..TaoistJiaoCeremony_da_info(ApplicantID, AdminID, Name, Name2, Name3, Name4, Name5, Name6, Mobile, Cost, Sex, " +
                "TaoistJiaoCeremonyType, TaoistJiaoCeremonyString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Count, Address, Addr, County, dist, " +
                "ZipCode, Sendback, rName, rMobile, rAddress, rAddr, rCounty, rdist, rZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Name2, @Name3, @Name4, @Name5, @Name6, @Mobile, @Cost, @Sex, @TaoistJiaoCeremonyType, @TaoistJiaoCeremonyString, @oversea, " +
                "@Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @sBirth, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Sendback, @rName, @rMobile, @rAddress, @rAddr, @rCounty, @rdist, @rZipCode, @CreateDate)";

            int Cost = 0;
            Cost = AjaxBasePage.GetTaoistJiaoCeremonyCost(3, TaoistJiaoCeremonyType);
            Cost += Sendback == "Y" ? 250 : 0;
            //switch (TaoistJiaoCeremonyType)
            //{
            //    case "1":
            //        //祈安七朝清醮-普渡施食
            //        Cost = 1000;
            //        break;
            //    case "2":
            //        //祈安七朝清醮-王船添載(天錢天庫)
            //        Cost = 600;
            //        break;
            //    case "3":
            //        //祈安七朝清醮-王船添載(添載物資)
            //        Cost = 600;
            //        break;
            //    case "4":
            //        //祈安七朝清醮-公斗
            //        Cost = 1000;
            //        break;
            //    case "5":
            //        //祈安七朝清醮-燃放水燈(大)
            //        Cost = 600;
            //        break;
            //    case "6":
            //        //祈安七朝清醮-燃放水燈(中)
            //        Cost = 1000;
            //        break;
            //    case "7":
            //        //祈安七朝清醮-燃放水燈(小)
            //        Cost = 2200;
            //        break;
            //}

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 3);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Name2", Name2);
            Adapter.AddParameterToSelectCommand("@Name3", Name3);
            Adapter.AddParameterToSelectCommand("@Name4", Name4);
            Adapter.AddParameterToSelectCommand("@Name5", Name5);
            Adapter.AddParameterToSelectCommand("@Name6", Name6);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@TaoistJiaoCeremonyType", TaoistJiaoCeremonyType);
            Adapter.AddParameterToSelectCommand("@TaoistJiaoCeremonyString", TaoistJiaoCeremonyString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@rName", rName);
            Adapter.AddParameterToSelectCommand("@rMobile", rMobile);
            Adapter.AddParameterToSelectCommand("@rAddress", rCounty + rDist + rAddr);
            Adapter.AddParameterToSelectCommand("@rAddr", rAddr);
            Adapter.AddParameterToSelectCommand("@rCounty", rCounty);
            Adapter.AddParameterToSelectCommand("@rdist", rDist);
            Adapter.AddParameterToSelectCommand("@rZipCode", rZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮護國息災梁皇大法會資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLybc_dh(int applicantID, string Name, string Mobile, string Sex, string LybcType, string LybcString, string oversea, string Birth, string LeapMonth,
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Remark, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lybc_dh_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LybcType, LybcString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LybcType, @LybcString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @CreateDate)";

            int Cost = 0;
            Cost = GetLybcCost(LybcType, 16);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 16);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LybcType", LybcType);
            Adapter.AddParameterToSelectCommand("@LybcString", LybcString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟代燒金紙資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="BPOType">BPOType=代燒金紙項目 1-代燒-貔貅金紙 2-代燒-狐仙金紙 3-代燒-夯枷金紙 4-代燒-財神金紙</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addBPO_jb(int applicantID, string Name, string Mobile, string Sex, string BPOType, string BPOString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..BPO_jb_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, BPOType, BPOString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @BPOType, @BPOString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost("6", 29) * Count;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 29);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@BPOType", BPOType);
            Adapter.AddParameterToSelectCommand("@BPOString", BPOString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟天赦日祭改資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版
        ///                                                 9-補財庫 10-財神賜福-消災補庫法會</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_jb(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_jb_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 29);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 29);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮天赦日招財補運資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版
        ///                                                 9-補財庫 10-財神賜福-消災補庫法會</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_ma(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_ma_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 23);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 29);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮補運資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_ty(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, int Count, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 14);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮九九重陽天赦日補運資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=生日月份(農曆)</param>
        /// <param name="Age">Age=年齡(農曆)</param>
        /// <param name="Zodiac">Zodiac=生肖(農曆)</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies2_ty(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, int Count, string Remark, string Addr, string County, string Dist, 
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies2_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Count, Remark, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Count, @Remark, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 14);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮天公生招財補運資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運升級版 20-天公生招財補運</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=生日月份(農曆)</param>
        /// <param name="Age">Age=年齡(農曆)</param>
        /// <param name="Zodiac">Zodiac=生肖(農曆)</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies3_ty(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth,
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, int Count, string Remark, string Addr, string County, string Dist,
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies3_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Count, Remark, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Count, @Remark, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 14);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建台東東海龍門天聖宮補運資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="SuppliesType">SuppliesType=補庫項目 1-下元補庫 2-呈疏補庫 3-企業補財庫 4-天赦日補運 5-天赦日祭改 6-天貺納福添運法會 7-招財補運 8-招財補運九九重陽升級版</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_dh(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string sBirth, string BirthMonth, string Age, string Zodiac, int Count1, int Count2, int Count3, int Count4, int Count5, int Count6, int Count7, 
            int Count8, string Addr, string County, string Dist, string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_dh_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, " +
                "BirthTime, sBirth, BirthMonth, Age, Zodiac, Count1, Count2, Count3, Count4, Count5, Count6, Count7, Count8, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @sBirth, @BirthMonth, @Age, " +
                "@Zodiac, @Count1, @Count2, @Count3, @Count4, @Count5, @Count6, @Count7, @Count8, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost += Count1 > 0 ? Count1 * 3000 : 0;
            Cost += Count2 > 0 ? Count2 * 800 : 0;
            Cost += Count3 > 0 ? Count3 * 600 : 0;
            Cost += Count4 > 0 ? Count4 * 600 : 0;
            Cost += Count5 > 0 ? Count5 * 600 : 0;
            Cost += Count6 > 0 ? Count6 * 600 : 0;
            Cost += Count7 > 0 ? Count7 * 600 : 0;
            Cost += Count8 > 0 ? Count8 * 600 : 0;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count1", Count1);
            Adapter.AddParameterToSelectCommand("@Count2", Count2);
            Adapter.AddParameterToSelectCommand("@Count3", Count3);
            Adapter.AddParameterToSelectCommand("@Count4", Count4);
            Adapter.AddParameterToSelectCommand("@Count5", Count5);
            Adapter.AddParameterToSelectCommand("@Count6", Count6);
            Adapter.AddParameterToSelectCommand("@Count7", Count7);
            Adapter.AddParameterToSelectCommand("@Count8", Count8);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立神霄玉府赦罪補庫資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_sx(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth,
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, string HomeNum, int Count, string Addr, string County, string Dist,
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_sx_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, " +
                "LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, HomeNum, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, " +
                "@Zodiac, @sBirth, @Email, @HomeNum, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetSuppliesCost(SuppliesType, 33);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 33);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟補財庫資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addSupplies_Lk(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, string LeapMonth,
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, string HomeNum, int Count, string Addr, string County, string Dist,
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_Lk_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, " +
                "LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, HomeNum, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, " +
                "@Zodiac, @sBirth, @Email, @HomeNum, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = 1600;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 21);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料(下元補庫)
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthMonth">BirthMonth=農曆月份</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addsupplies_wu(int applicantID, string Name, string Mobile, string SuppliesType, string SuppliesString, string Sex, string Birth, string BirthMonth, string Age, string Zodiac, string HomeNum, string Email, string Address, string Addr, string County, string Dist, string ZipCode, string Count, string Remark)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Supplies_wu_info(ApplicantID, AdminID, Name, Mobile, SuppliesType, SuppliesString, Sex, Birth, BirthMonth, Age, Zodiac, HomeNum, Email, Address, Addr, County, dist, ZipCode, Count, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @SuppliesType, @SuppliesString, @Sex, @Birth, @BirthMonth, @Age, @Zodiac, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Count, @Remark, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="Address">Address=通訊地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Count">Count=數量</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addsupplies_wu2(int applicantID, string Name, string Mobile, string SuppliesType, string SuppliesString, string Sex, string Birth, string HomeNum, string Email, string Address, string Addr, string County, string Dist, string ZipCode, string Count, string Remark)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into supplies_wu_info2(ApplicantID, AdminID, Name, Mobile, SuppliesType, SuppliesString, Sex, Birth, HomeNum, Email, Address, Addr, County, dist, ZipCode, Count, Remark, CreateDate, CreateDateString) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @SuppliesType, @SuppliesString, @Sex, @Birth, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Count, @Remark, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料(下元補庫)
        /// </summary>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=祈福人姓名</param>
        /// <param name="Mobile">Mobile=祈福人電話</param>
        /// <param name="Sex">Sex=性別 善男 信女</param>
        /// <param name="SuppliesType">補財庫項目 1-下元補庫</param>
        /// <param name="SuppliesString">補財庫項目字串</param>
        /// <param name="oversea">oversea=地址屬國內外 1:國內 2:國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-閏月 N-非閏月</param>
        /// <param name="BirthTime">Birth=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=農曆月份</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="HomeNum">HomeNum=祈福人市話</param>
        /// <param name="Email">Email=祈福人信箱</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Remark">Remark=備註</param>
        /// <param name="Count"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int addsupplies_wu(int applicantID, string Name, string Mobile, string Sex, string SuppliesType, string SuppliesString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string HomeNum, string Email, string Addr, string County, string Dist, 
            string ZipCode, string Remark, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_wu_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, SuppliesType, SuppliesString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, HomeNum, Email, Address, Addr, County, dist, ZipCode, Remark, Count, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @SuppliesType, @SuppliesString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @Count, @CreateDate)";

            int count = 1;
            int.TryParse(Count, out count);
            int cost = 650;
            cost = cost * count;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", cost);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料(呈疏補庫)
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=地址屬國內外 1:國內 2:國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">Birth=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=農曆月份</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-閏月 N-非閏月</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addsupplies_wu2(int applicantID, string Name, string Mobile, string SuppliesType, string SuppliesString, string Sex, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string HomeNum, string Email, string Address, string Addr, string County, string Dist, string ZipCode, string Remark, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_wu_info2(ApplicantID, AdminID, Name, Mobile, Cost, SuppliesType, SuppliesString, Sex, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, HomeNum, Email, Address, Addr, County, dist, ZipCode, Remark, Count, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @SuppliesType, @SuppliesString, @Sex, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @Count, @CreateDate)";
           
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", 650);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫資料(企業補財庫)
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="oversea">oversea=地址屬國內外 1:國內 2:國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">Birth=農曆時辰</param>
        /// <param name="BirthMonth">BirthMonth=農曆月份</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-閏月 N-非閏月</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="HomeNum">HomeNum=市話</param>
        /// <param name="Email">Email</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Remark">Remark=備註</param>
        /// </summary>
        public int addsupplies_wu3(int applicantID, string Name, string Mobile, string SuppliesType, string SuppliesString, string Sex, string oversea, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string HomeNum, string Email, string Address, string Addr, string County, string Dist, string ZipCode, string Remark, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Supplies_wu_info3(ApplicantID, AdminID, Name, Mobile, Cost, SuppliesType, SuppliesString, Sex, oversea, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, HomeNum, Email, Address, Addr, County, dist, ZipCode, Remark, Count, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @SuppliesType, @SuppliesString, @Sex, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @HomeNum, @Email, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @Count, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", 1300);
            Adapter.AddParameterToSelectCommand("@SuppliesType", SuppliesType);
            Adapter.AddParameterToSelectCommand("@SuppliesString", SuppliesString);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }



        public static int GetLightsCost(string LightsType, int AdminID)
        {
            int result = 0;

            switch (AdminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 620;
                            break;
                        case "4":
                            //安太歲
                            result = 520;
                            break;
                        case "5":
                            //文昌燈
                            result = 820;
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 620;
                            break;
                        case "4":
                            //安太歲
                            result = 620;
                            break;
                    }
                    break;
                case 6:
                    //北港武德宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //安太歲
                            result = 600;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //安太歲
                            result = 300;
                            break;
                        case "5":
                            //文昌燈
                            result = 600;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                        case "8":
                            //藥師燈
                            result = 600;
                            break;
                        case "24":
                            //觀音佛祖燈
                            result = 600;
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "7":
                            //姻緣燈
                            result = 999;
                            break;
                        case "9":
                            //財利燈
                            result = 600;
                            break;
                        case "11":
                            //福壽燈
                            result = 999;
                            break;
                        case "12":
                            //寵物平安燈
                            result = 500;
                            break;
                        case "20":
                            //月老姻緣燈
                            result = 999;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 300;
                            break;
                        case "4":
                            //太歲燈
                            result = 300;
                            break;
                        case "6":
                            //財神燈
                            result = 600;
                            break;
                        case "8":
                            //藥師燈
                            result = 600;
                            break;
                        case "10":
                            //貴人燈
                            result = 600;
                            break;
                        case "11":
                            //福祿燈
                            result = 600;
                            break;
                        case "21":
                            //孝親祈福燈
                            result = 880;
                            break;
                        case "33":
                            //智慧燈
                            result = 300;
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (LightsType)
                    {
                        case "3":
                            //貴人燈(光明燈)
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "6":
                            //發財燈
                            result = 500;
                            break;
                        case "7":
                            //月老桃花燈
                            result = 500;
                            break;
                        case "8":
                            //消災延壽燈
                            result = 500;
                            break;
                        case "12":
                            //寵物平安燈
                            result = 500;
                            break;
                        case "19":
                            //財庫燈
                            result = 500;
                            break;
                        case "25":
                            //財神斗/一個月
                            result = 1200;
                            break;
                        case "34":
                            //發財斗/一個月
                            result = 1200;
                            break;
                        case "35":
                            //姻緣斗/一個月
                            result = 1200;
                            break;
                        case "36":
                            //貴人斗/一個月
                            result = 1200;
                            break;
                        case "37":
                            //消災延壽斗/一個月
                            result = 1200;
                            break;
                        case "38":
                            //財神斗/三個月
                            result = 3000;
                            break;
                        case "39":
                            //發財斗/三個月
                            result = 3000;
                            break;
                        case "40":
                            //姻緣斗/三個月
                            result = 3000;
                            break;
                        case "41":
                            //貴人斗/三個月
                            result = 3000;
                            break;
                        case "42":
                            //消災延壽斗/三個月
                            result = 3000;
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //安太歲
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "9":
                            //財利燈
                            result = 500;
                            break;
                        case "13":
                            //龍王燈
                            result = 800;
                            break;
                        case "14":
                            //虎爺燈
                            result = 500;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (LightsType)
                    {
                        case "3":
                            //元神光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲平安符
                            result = 500;
                            break;
                        case "5":
                            //文魁智慧燈
                            result = 500;
                            break;
                        case "6":
                            //正財福報燈
                            result = 500;
                            break;
                        case "15":
                            //轉運納福燈
                            result = 1000;
                            break;
                        case "16":
                            //光明燈上層
                            result = 1000;
                            break;
                        case "17":
                            //偏財旺旺燈
                            result = 500;
                            break;
                        case "18":
                            //廣進安財庫
                            result = 300;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲燈
                            result = 500;
                            break;
                        case "5":
                            //五文昌燈
                            result = 500;
                            break;
                        case "6":
                            //福財燈
                            result = 500;
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 600;
                            break;
                        case "4":
                            //太歲燈
                            result = 600;
                            break;
                        case "9":
                            //財利燈
                            result = 600;
                            break;
                        case "10":
                            //貴人燈
                            result = 600;
                            break;
                        case "12":
                            //寵物平安燈
                            result = 600;
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            result = 500;
                            break;
                        case "4":
                            //太歲燈
                            result = 500;
                            break;
                        case "5":
                            //文昌燈
                            result = 500;
                            break;
                        case "6":
                            //財神燈
                            result = 500;
                            break;
                        case "8":
                            //藥師燈
                            result = 500;
                            break;
                        case "22":
                            //事業燈
                            result = 500;
                            break;
                        case "23":
                            //全家光明燈
                            result = 1000;
                            break;
                        case "25":
                            //財神斗
                            result = 3000;
                            break;
                        case "26":
                            //事業斗
                            result = 3000;
                            break;
                        case "27":
                            //平安斗
                            result = 3000;
                            break;
                        case "28":
                            //文昌斗
                            result = 3000;
                            break;
                        case "29":
                            //藥師斗
                            result = 3000;
                            break;
                        case "30":
                            //元神斗
                            result = 3000;
                            break;
                        case "31":
                            //福祿壽斗
                            result = 3000;
                            break;
                        case "32":
                            //觀音斗
                            result = 3000;
                            break;
                    }
                    break;
                case 32:
                    //桃園龍德宮
                    switch (LightsType)
                    {
                        default:
                            result = 560;
                            break;
                    }
                    break;
            }

            return result;
        }

        public static int GetLybcCost(string LybcType, int AdminID)
        {
            int result = 0;

            switch (AdminID)
            {
                case 16:
                    //台東東海龍門天聖宮
                    switch (LybcType)
                    {
                        case "1":
                            //財寶袋
                            result = 300;
                            break;
                        case "2":
                            //普度供桌
                            result = 1500;
                            break;
                        case "3":
                            //福慧水晶燈
                            result = 500;
                            break;
                        case "4":
                            //重建募款
                            result = 500;
                            break;
                        case "5":
                            //重建募款
                            result = 1000;
                            break;
                        case "6":
                            //重建募款
                            result = 2000;
                            break;
                    }
                    break;
            }

            return result;
        }


        /// <summary>
        /// 建立大甲鎮瀾宮廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_da(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_da_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 3);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 3);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立_新港奉天宮廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_h(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_h_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 4);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 4);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_wu(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, string HomeNum, int Count, string Addr, string County, string Dist, 
            string ZipCode, string Remark, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_wu_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, HomeNum, Count, Address, Addr, County, dist, ZipCode, Remark, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @HomeNum, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Remark, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 6);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 6);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_Fu(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_Fu_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 8);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 8);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_Luer(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Msg, string PetName, string PetType, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_Luer_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, Msg, PetName, PetType, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Msg, @PetName, @PetType, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 10);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 10);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Msg", Msg);
            Adapter.AddParameterToSelectCommand("@PetName", PetName);
            Adapter.AddParameterToSelectCommand("@PetType", PetType);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_ty(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_ty_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 14);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮廟孝親祈福燈點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=國歷生日</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_ty_mom(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, 
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_ty_mom_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 14);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 14);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_Fw(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string PetName, string PetType, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_Fw_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, PetName, PetType, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @PetName, @PetType, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 15);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 15);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@PetName", PetName);
            Adapter.AddParameterToSelectCommand("@PetType", PetType);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_dh(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode, 
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_dh_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)"; ;

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 16);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 16);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Sendback">Sendback=寄送方式 N-不寄回 Y-寄回(加收運費120元)</param>
        /// <param name="rName">rName=收件人姓名</param>
        /// <param name="rMobile">rMobile=收件人電話</param>
        /// <param name="rAddress">rAddress=收件人地址(全)</param>
        /// <param name="rAddr">rAddr=收件人地址(部分)</param>
        /// <param name="rCounty">rCounty=收件人縣市</param>
        /// <param name="rDist">rDist=收件人區域</param>
        /// <param name="rZipCode">rZipCode=收件人郵遞區號</param>
        /// </summary>
        public int addLights_Lk(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth, 
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, string HomeNum, int Count, string Addr, string County, string Dist, 
            string ZipCode, string Sendback, string rName, string rMobile, string rAddress, string rAddr, string rCounty, string rDist, string rZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_Lk_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, HomeNum, Count, Address, Addr, County, dist, ZipCode, Sendback, rName, rMobile, rAddress, rAddr, rCounty, " +
                "rdist, rZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @HomeNum, @Count, @Address, @Addr, @County, @dist, @ZipCode, @Sendback, @rName, @rMobile, @rAddress, @rAddr, @rCounty, @rdist, @rZipCode, " +
                "@CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 21);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 21);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);

            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@rName", rName);
            Adapter.AddParameterToSelectCommand("@rMobile", rMobile);
            Adapter.AddParameterToSelectCommand("@rAddress", rAddress);
            Adapter.AddParameterToSelectCommand("@rAddr", rAddr);
            Adapter.AddParameterToSelectCommand("@rCounty", rCounty);
            Adapter.AddParameterToSelectCommand("@rdist", rDist);
            Adapter.AddParameterToSelectCommand("@rZipCode", rZipCode);

            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_ma(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth,
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode,
            string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_ma_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 23);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 23);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_jb(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, string LeapMonth,
            string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, string ZipCode,
            string PetName, string PetType, string PetSex, string PetBirth, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_jb_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, PetName, PetType, PetSex, PetBirth, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @PetName, @PetType, @PetSex, @PetBirth, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 29);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 29);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@PetName", PetName);
            Adapter.AddParameterToSelectCommand("@PetType", PetType);
            Adapter.AddParameterToSelectCommand("@PetSex", PetSex);
            Adapter.AddParameterToSelectCommand("@PetBirth", PetBirth);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台灣道教總廟無極三清總道院點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_wjsan(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth, 
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist, 
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_wjsan_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 31);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 31);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園龍德宮點燈資料
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Mobile">Mobile=手機號碼</param>
        /// <param name="Sex">Sex=性別</param>
        /// <param name="LightsType">LightsType=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 
        /// 15-轉運納福燈 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 
        /// 28-文昌斗 29-藥師斗 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈 </param>
        /// <param name="oversea">oversea=1-國內 2-國外</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="BirthTime">BirthTime=農曆時辰</param>
        /// <param name="LeapMonth">LeapMonth=閏月 Y-是 N-否</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="sBirth">sBirth=國曆生日</param>
        /// <param name="County">County=縣市</param>
        /// <param name="dist">dist=區域</param>
        /// <param name="Addr">Addr=部分地址</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// </summary>
        public int addLights_ld(int applicantID, string Name, string Mobile, string Sex, string LightsType, string LightsString, string oversea, string Birth,
            string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string sBirth, string Email, int Count, string Addr, string County, string Dist,
            string ZipCode, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Lights_ld_info(ApplicantID, AdminID, Name, Mobile, Cost, Sex, LightsType, LightsString, oversea, Birth, LeapMonth, " +
                "BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Count, Address, Addr, County, dist, ZipCode, CreateDate) " +
                "values(@ApplicantID, @AdminID, @Name, @Mobile, @Cost, @Sex, @LightsType, @LightsString, @oversea, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, " +
                "@sBirth, @Email, @Count, @Address, @Addr, @County, @dist, @ZipCode, @CreateDate)";

            int Cost = 0;
            Cost = GetLightsCost(LightsType, 32);

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", 32);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sex", Sex);
            Adapter.AddParameterToSelectCommand("@LightsType", LightsType);
            Adapter.AddParameterToSelectCommand("@LightsString", LightsString);
            Adapter.AddParameterToSelectCommand("@oversea", oversea);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Addr);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立點燈人資料
        /// <param name="AdminID">AdminID=宮廟編號</param>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="ZipCode">ZipCode=郵遞區號</param>
        /// <param name="Birthday">Birthday=生日農曆</param>
        /// <param name="BirthMonth">BirthMonth=生日月份</param>
        /// <param name="Age">Age=年齡</param>
        /// <param name="Zodiac">Zodiac=生肖</param>
        /// <param name="leamMonth">leamMonth=閏月</param>
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// </summary>
        public int addLightsinfo(int adminID, int applicantID, string Name, string Phone, string Address, string Addr, string County, string Dist, string ZipCode, string Birthday, string BirthMonth, string Age, string Zodiac, string leamMonth, string type)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into LightsInfo(AdminID, ApplicantID, Name, Num, Mobile, Address, Addr, County, Dist, ZipCode, Birthday, BirthMonth, Age, Zodiac, LeamMonth, Type, Status, CreateDate, CreateDateString) values(@AdminID, @ApplicantID, @Name, @Num, @Mobile, @Address, @Addr, @County, @Dist, @ZipCode, @Birthday, @BirthMonth, @Age, @Zodiac, @LeamMonth, @Type, @Status, @CreateDate, @CreateDateString)";


            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Num", 0);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Birthday", Birthday);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@LeamMonth", leamMonth);
            Adapter.AddParameterToSelectCommand("@Type", type);
            Adapter.AddParameterToSelectCommand("@Status", 0);                  //0-待付款 1-付款中 2-已付款
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo(string Name, string Phone)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo(Name, Mobile, CreateDate) values(@Name, @Mobile, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立繞境商品購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Pilgrimage(string Name, string Mobile, string adminID, string County, string Dist, string Address, string ZipCode, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Pilgrimage(Name, Mobile, AdminID, County, Dist, Addr, Address, ZipCode, postURL, CreateDateString, CreateDate) values(@Name, @Mobile, @AdminID, @County, @Dist, @Addr, @Address, @ZipCode, @postURL, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Address);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Address);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立繞境商品資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int addProduct_Pilgrimage(int ApplicantID, string Name, string SculptureName, int SculptureStatus, int Type, int Count)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Product_Pilgrimage(ApplicantID, Name, SculptureName, SculptureStatus, Type, Count, CreateDateString, CreateDate) values(@ApplicantID, @Name, @SculptureName, @SculptureStatus, @Type, @Count, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@SculptureName", SculptureName);
            Adapter.AddParameterToSelectCommand("@SculptureStatus", SculptureStatus);
            Adapter.AddParameterToSelectCommand("@Type", Type);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立錢母商品購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Moneymother(string Name, string Mobile, string adminID, string County, string Dist, string Address, string ZipCode, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Moneymother(Name, Mobile, AdminID, County, Dist, Addr, Address, ZipCode, postURL, CreateDateString, CreateDate) values(@Name, @Mobile, @AdminID, @County, @Dist, @Addr, @Address, @ZipCode, @postURL, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Address);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Address);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立錢母商品購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Moneymother(string Name, string Mobile, string adminID, string County, string Dist, string Address, string ZipCode, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Moneymother(Name, Mobile, AdminID, County, Dist, Addr, Address, ZipCode, postURL, CreateDateString, CreateDate) values(@Name, @Mobile, @AdminID, @County, @Dist, @Addr, @Address, @ZipCode, @postURL, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Address);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Address);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立錢母商品資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int addProduct_Moneymother(int ApplicantID, string Name, int Type, int Count)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Product_Moneymother(ApplicantID, Name, Type, Count, CreateDateString, CreateDate) values(@ApplicantID, @Name, @Type, @Count, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Type", Type);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立錢母商品資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int addProduct_Moneymother(int ApplicantID, string Name, int Type, int Cost, int Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..Product_Moneymother(ApplicantID, Name, Type, Cost, Count, CreateDate) " +
                "values(@ApplicantID, @Name, @Type, @Cost, @Count, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Type", Type);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立點燈購買人資料(大甲鎮瀾宮、新港奉天宮)
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Lights(string Name, string Phone, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Lights(Name, Mobile, AdminID, CreateDateString, CreateDate) values(@Name, @Mobile, @AdminID, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立點燈購買人資料(大甲鎮瀾宮、新港奉天宮)
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Lights(string Name, string Phone, string postURL, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Lights(Name, Mobile, PostURL, AdminID, CreateDateString, CreateDate) values(@Name, @Mobile, @postURL, @AdminID, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue(string Name, string Phone, int adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Purdue(Name, Mobile, AdminID, CreateDate) values(@Name, @Mobile, @AdminID, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_da(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_da_Purdue(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summa
        /// <summary>
        /// 建立新港奉天宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_hsinkangmazu(string Name, string Phone, string Birth, string Address, string Email, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_h_Purdue(Name, Mobile, Address, Birthday, Email, AdminID, CreateDate) values(@Name, @Mobile, @Address, @Birthday, @Email, @AdminID, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Birthday", Birth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summa
        /// <summary>
        /// 建立新港奉天宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_h(string Name, string Mobile, string Birth, string Email, string adminID, string County, string Dist, string Address, string ZipCode, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_h_Purdue(Name, Mobile, PostURL, Birthday, Email, AdminID, County, dist, Addr, Address, ZipCode, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @Birthday, @Email, @AdminID, @County, @dist, @Addr, @Address, @ZipCode, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Birthday", Birth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Address);
            Adapter.AddParameterToSelectCommand("@Address", County + (Dist == "*" ? "" : Dist) + Address);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_wude(string Name, string Phone, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Purdue(Name, Mobile, AdminID, CreateDate) values(@Name, @Mobile, @AdminID, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_wu(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Purdue(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_Fu(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Fu_Purdue(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立武景福普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_Jing(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Jing_Purdue(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Purdue_Luer(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Luer_Purdue(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_da(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_da_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立新港奉天宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_h(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Email, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_h_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Email, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Email, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_wu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wu_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_Fu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Fu_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園大廟景福宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_Jing(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Jing_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_Luer(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Luer_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_ty(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_Fw(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Fw_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_dh(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_dh_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_Lk(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Lk_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_ma(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ma_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鎮瀾買足網普度購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_purdue_mazu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_mazu_Purdue(Name, Mobile, Cost, County, dist, Addr, ZipCode, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @ZipCode, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_da(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_da_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立新港奉天宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_h(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_h_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_wu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wu_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立西螺福興宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Fu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Fu_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Luer(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Luer_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ty(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮孝親祈福燈點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ty(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, 
            string sBirth, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, 
            string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Lights(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Cost, ZipCode, " +
                "County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Birth, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, " +
                "@ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮孝親祈福燈點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ty_mom(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_mom_Lights(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Cost, ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Fw(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Fw_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_dh(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_dh_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立五股賀聖宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Hs(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Hs_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立外澳接天宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Jt(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Jt_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立安平開台天后宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Am(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Am_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台南正統鹿耳門聖母廟點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Luer(string Name, string Mobile, string Cost, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Luer_Lights(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ty(string Name, string Mobile, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Lights(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立斗六五路財神宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Fw(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Fw_Lights(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_dh(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_dh_Lights(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_Lk(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Lk_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ma(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback,
            string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ma_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, " +
                "AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, " +
                "@CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_jb(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, 
            string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_jb_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, " +
                "AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, " +
                "@CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台灣道教總廟無極三清總道院點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_wjsan(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback,
            string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wjsan_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, " +
                "AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, " +
                "@CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園龍德宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_lights_ld(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback,
            string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ld_Lights(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, " +
                "AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, " +
                "@CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(下元補庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_wu(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wu_Supplies(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(呈疏補庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_wu2(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wu_Supplies3(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(企業補財庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_wu3(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_wu_Supplies3(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, NewUser, AddDate, AddDateString, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @NewUser, @AddDate, @AddDateString, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@NewUser", 1);
            Adapter.AddParameterToSelectCommand("@AddDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@AddDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮天赦日補運購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_ty(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Supplies(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Cost, ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮九九重陽天赦日補運購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies2_ty(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, 
            string sBirth, string Email, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, 
            string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Supplies2(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Cost, " +
                "ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @sBirth, @Email, @Cost, @ZipCode, @County, @dist, @Addr, @Address, " +
                "@Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮天公生招財補運購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies3_ty(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac,
            string sBirth, string Email, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status,
            string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_Supplies3(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, sBirth, Email, Cost, " +
                "ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @sBirth, @Email, @Cost, @ZipCode, @County, @dist, @Addr, @Address, " +
                "@Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@sBirth", sBirth);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮天貺納福添運法會購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_dh(string Name, string Mobile, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_dh_Supplies(Name, Mobile, Cost, ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立鹿港城隍廟補財庫購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_Lk(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Lk_Supplies(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, " +
                "ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, " +
                "@CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立神霄玉府赦罪補庫購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_sx(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName,
            string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_sx_Supplies(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, " +
                "ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, " +
                "@CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟代燒金紙購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_BPO_jb(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_jb_BPO(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立進寶財神廟天赦日祭改購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_jb(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_jb_Supplies(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建玉敕大樹朝天宮天赦日招財補運購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_ma(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ma_Supplies(Name, Mobile, Cost, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立桃園威天宮關聖帝君聖誕購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_EmperorGuansheng_ty(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, string Zodiac, string Email, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ty_EmperorGuansheng(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Email, Cost, ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Email, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立玉敕大樹朝天宮靈寶禮斗購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Lingbaolidou_ma(string Name, string Mobile, string Email, string Cost, string ZipCode, string County, string dist, string Addr, 
            string Sendback, string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_ma_Lingbaolidou(Name, Mobile, Email, Cost, ZipCode, County, dist, Addr, Address, Sendback, " +
                "ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Email, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, " +
                "@Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立大甲鎮瀾宮七朝清醮購買人資料
        /// </summary>
        /// <param name="Name">購買人姓名</param>
        /// <param name="Mobile">購買人電話</param>
        /// <param name="Birth">Birth=農曆生日</param>
        /// <param name="LeapMonth">閏月-Y 非閏月-N</param>
        /// <param name="BirthTime">時辰</param>
        /// <param name="BirthMonth">生日月份</param>
        /// <param name="Age">年齡</param>
        /// <param name="Zodiac">生肖</param>
        /// <param name="Email">Email</param>
        /// <param name="Cost">金額</param>
        /// <param name="ZipCode">郵遞區號</param>
        /// <param name="County">縣市</param>
        /// <param name="dist">區域</param>
        /// <param name="Addr">部分地址</param>
        /// <param name="Sendback">寄回-Y 不寄回-N</param>
        /// <param name="ReceiptName">收件人姓名</param>
        /// <param name="ReceiptMobile">收件人電話</param>
        /// <param name="Status">狀態 -2-已退款 0-正常 1-付款中 2-付款完成</param>
        /// <param name="adminID">宮廟編號</param>
        /// <param name="postURL">來源URL</param>
        /// <param name="Year">年份</param>
        /// <returns></returns>
        public int addapplicantinfo_TaoistJiaoCeremony_da(string Name, string Mobile, string Birth, string LeapMonth, string BirthTime, string BirthMonth, string Age, 
            string Zodiac, string Email, string Cost, string ZipCode, string County, string dist, string Addr, string Sendback, string ReceiptName, string ReceiptMobile, 
            int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_da_TaoistJiaoCeremony(Name, Mobile, Birth, LeapMonth, BirthTime, BirthMonth, Age, Zodiac, Email, Cost, " +
                "ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Birth, @LeapMonth, @BirthTime, @BirthMonth, @Age, @Zodiac, @Email, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, " +
                "@ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Birth", Birth);
            Adapter.AddParameterToSelectCommand("@LeapMonth", LeapMonth);
            Adapter.AddParameterToSelectCommand("@BirthTime", BirthTime);
            Adapter.AddParameterToSelectCommand("@BirthMonth", BirthMonth);
            Adapter.AddParameterToSelectCommand("@Age", Age);
            Adapter.AddParameterToSelectCommand("@Zodiac", Zodiac);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立台東東海龍門天聖宮護國息災梁皇大法會購買人資料
        /// </summary>
        /// <param name="Name">購買人姓名</param>
        /// <param name="Mobile">購買人電話</param>
        /// <param name="Cost">金額</param>
        /// <param name="ZipCode">郵遞區號</param>
        /// <param name="County">縣市</param>
        /// <param name="dist">區域</param>
        /// <param name="Addr">部分地址</param>
        /// <param name="Sendback">寄回-Y 不寄回-N</param>
        /// <param name="ReceiptName">收件人姓名</param>
        /// <param name="ReceiptMobile">收件人電話</param>
        /// <param name="Status">狀態 -2-已退款 0-正常 1-付款中 2-付款完成</param>
        /// <param name="adminID">宮廟編號</param>
        /// <param name="postURL">來源URL</param>
        /// <param name="Year">年份</param>
        /// <returns></returns>
        public int addapplicantinfo_Lybc_dh(string Name, string Mobile, string Cost, string County, string dist, string Addr, string ZipCode, string Sendback, 
            string ReceiptName, string ReceiptMobile, int Status, string adminID, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_dh_Lybc(Name, Mobile, Cost, ZipCode, County, dist, Addr, Address, Sendback, ReceiptName, ReceiptMobile, " +
                "PostURL, AdminID, Status, CreateDate, CreateDateString) " +
                "       values(@Name, @Mobile, @Cost, @ZipCode, @County, @dist, @Addr, @Address, @Sendback, @ReceiptName, @ReceiptMobile, @PostURL, @AdminID, @Status, " +
                "@CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@ZipCode", ZipCode);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@dist", dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", County + dist + Addr);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(下元補庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_wude(string Name, string Phone, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Supplies(Name, Mobile, AdminID, CreateDate) values(@Name, @Mobile, @AdminID, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Supplies_wude2(string Name, string Phone, string postURL, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Supplies2(Name, Mobile, AdminID, PostURL, CreateDate, CreateDateString) values(@Name, @Mobile, @AdminID, @postURL, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(下元補庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_supplies_wu(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Supplies(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_supplies_wu2(string Name, string Mobile, string adminID, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_supplies2(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立補財庫(企業補財庫)購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_supplies_wu3(string Name, string Mobile, string adminID, string postURL, string year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + year + "..ApplicantInfo_wu_Supplies3(Name, Mobile, PostURL, AdminID, CreateDate, CreateDateString) values(@Name, @Mobile, @PostURL, @AdminID, @CreateDate, @CreateDateString)";
           
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Mobile);
            Adapter.AddParameterToSelectCommand("@PostURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int addapplicantinfo_Lights_wude(string Name, string Phone, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Lights(Name, Mobile, AdminID, CreateDateString, CreateDate) values(@Name, @Mobile, @AdminID, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立北港武德宮點燈購買人資料
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Lights_wude(string Name, string Phone, string postURL, string adminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_wu_Lights(Name, Mobile, PostURL, AdminID, CreateDateString, CreateDate) values(@Name, @Mobile, @postURL, @AdminID, @CreateDateString, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@AdminID", adminID);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dtNow.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@CreateDate", dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 取得最後一名編號資料
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// </summary>
        public int GetListNum(int type)
        {
            int result = 0;
            string sql = string.Empty;
            string lights = "BrightlightsInfo";

            if (type == 1)
            {
                lights = "BrightlightsInfo";
            }
            else if (type == 2)
            {
                lights = "BrightlightsInfo";
            }
            else if (type == 3)
            {
                lights = "ZampInfo";
            }
            else if (type == 4)
            {
                lights = "SalvationInfo";
            }

            sql = "Select * from " + lights + " Where Status = 0 Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = int.Parse(dtGetData.Rows[0]["Num"].ToString());
            }
            return result;
        }

        /// <summary>
        /// 取得最後一名編號資料
        /// <param name="adminID">adminID=宮廟編號 3-大甲鎮瀾宮 4-新港奉天宮 5-商品販賣小舖-新港奉天宮 6-北港武德宮</param>
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// </summary>
        public int GetListNum(int adminID, int type)
        {
            int result = 0;
            string sql = string.Empty;

            sql = "Select * from LightsInfo Where Status = 0 and AdminID = @AdminID and Type = @Type Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = int.Parse(dtGetData.Rows[0]["Num"].ToString());
            }
            return result;
        }

        /// <summary>
        /// 取得最後一名編號資料
        /// <param name="adminID">adminID=宮廟編號 3-大甲鎮瀾宮 4-新港奉天宮 5-商品販賣小舖-新港奉天宮 6-北港武德宮</param>
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// </summary>
        public int GetLightsCount(int adminID, int type)
        {
            int result = 0;
            string sql = string.Empty;

            switch(adminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    sql = "Select * from LightsInfo Where Status = 0 and AdminID = @AdminID and Type = @Type and Num > 0 Order by Num Desc";
                    break;
                case 4:
                    //新港奉天宮
                    sql = "Select * from LightsInfo Where Status = 0 and AdminID = @AdminID and Type = @Type and Num > 0 Order by Num Desc";
                    break;
                case 5:
                    //商品販賣小舖-新港奉天宮
                    break;
                case 6:
                    //北港武德宮
                    sql = "Select * from Lights_wu_Info Where Status = 0 and AdminID = @AdminID and Type = @Type and Num > 0 Order by Num Desc";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                result = dtGetData.Rows.Count;
            }

            return result;
        }

        /// <summary>
        /// 取得購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo(int applicantID, int Status)
        {
            string sql = "Select * from ApplicantInfo Where Status = @Status and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廠商編號 7-繞境商品</param>
        /// <param name="Status">Status=訂單狀態 0-待付款 1-付款中 2-已付款</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(int applicantID, int adminID, int Status)
        {
            string sql = string.Empty;

            sql = "Select * from ApplicantInfo_Moneymother Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得繞境商品購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廠商編號 7-繞境商品</param>
        /// <param name="Status">Status=訂單狀態 0-待付款 1-付款中 2-已付款</param>
        /// </summary>
        public DataTable Getapplicantinfo_Pilgrimage(int applicantID, int adminID, int Status)
        {
            string sql = string.Empty;

            sql = "Select * from ApplicantInfo_Pilgrimage Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廠商編號 5-文創商品小舖</param>
        /// <param name="Status">Status=訂單狀態 0-待付款 1-付款中 2-已付款</param>
        /// </summary>
        public DataTable Getapplicantinfo_Product(int applicantID, int adminID, int Status)
        {
            string sql = string.Empty;

            sql = "Select * from ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="Status">Status=訂單狀態 0-待付款 1-付款中 2-已付款</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfo(int applicantID, int adminID, int Status)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from ApplicantInfo_Lights Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case 4:
                    sql = "Select * from ApplicantInfo_Lights Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case 6:
                    sql = "Select * from ApplicantInfo_wu_Lights Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料(大甲鎮瀾宮、新港奉天宮)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Lights(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_Lights Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Purdue(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_Purdue Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮普度購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_h_Purdue(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_h_Purdue Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_wu_Purdue(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_wu_Purdue Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_wu_Supplies(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_wu_Supplies Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_wu_Supplies2(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_wu_Supplies2 Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(企業補財庫)購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_wu_Supplies3(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_wu_Supplies3 Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮點燈購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_wu_Lights(int applicantID, int adminID, int Status)
        {
            string sql = "Select * from ApplicantInfo_wu_Lights Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得普度購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮 8-西螺福興宮 9-景福宮 10-台南正統鹿耳門聖母廟</param>
        /// </summary>
        public DataTable Getapplicantinfo_PurduefromNum2String(string Num2String, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_Purdue_da_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 4:
                    sql = "Select * from view_Purdue_h_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 6:
                    sql = "Select * from view_Purdue_wu_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 8:
                    sql = "Select * from view_Purdue_Fu_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 9:
                    sql = "Select * from view_Purdue_Jing_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 10:
                    sql = "Select * from view_Purdue_Luer_info Where AppStatus = 2 and Num > 0 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
            }

            DataTable dtGetData = new DataTable();
            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfofromNum2String(string Num2String, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 4:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 6:
                    sql = "Select * from view_Lights_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
                case 10:
                    sql = "Select * from view_Lights_Luer_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                    break;
            }

            DataTable dtGetData = new DataTable();
            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="phone">phone=購買人手機or祈福人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfo(string phone, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 4:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 6:
                    sql = "Select * from view_Lights_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfo(string name, string phone, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 4:
                    sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 6:
                    sql = "Select * from view_Lights_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfo(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    sql = "Select * from Temple_" + Year + "..view_Lights_da_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 4:
                    sql = "Select * from Temple_" + Year + "..view_Lights_h_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 6:
                    sql = "Select * from Temple_" + Year + "..view_Lights_wu_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 8:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Fu_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 10:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Luer_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 14:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 15:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Fw_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 16:
                    sql = "Select * from Temple_" + Year + "..view_Lights_dh_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 21:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Lk_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 23:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ma_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 29:
                    sql = "Select * from Temple_" + Year + "..view_Lights_jb_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 31:
                    sql = "Select * from Temple_" + Year + "..view_Lights_wjsan_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 32:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ld_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LightsInfo(string name, string phone, int adminID, int type, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    sql = "Select * from Temple_" + Year + "..view_Lights_da_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 4:
                    sql = "Select * from Temple_" + Year + "..view_Lights_h_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 6:
                    sql = "Select * from Temple_" + Year + "..view_Lights_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 8:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Fu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 10:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Luer_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 14:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ty_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 15:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Fw_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 16:
                    sql = "Select * from Temple_" + Year + "..view_Lights_dh_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 21:
                    sql = "Select * from Temple_" + Year + "..view_Lights_Lk_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 23:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ma_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 29:
                    sql = "Select * from Temple_" + Year + "..view_Lights_jb_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 31:
                    sql = "Select * from Temple_" + Year + "..view_Lights_wjsan_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 32:
                    sql = "Select * from Temple_" + Year + "..view_Lights_ld_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得普度購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_PurdueInfo(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_da_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 4:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_h_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 6:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_wu_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 8:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_Fu_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                //case 9:
                //    sql = "Select * from Temple_" + Year + "..view_Purdue_Jing_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                //    break;
                case 10:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_Luer_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 14:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_ty_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 15:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_Fw_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 16:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_dh_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 21:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_Lk_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 23:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_ma_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 30:
                    sql = "Select * from Temple_" + Year + "..view_Purdue_mazu_InfowithAPPCharge Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="kind">kind=補財庫項目 1-下元補庫-北港武德宮 2-呈疏補庫-北港武德宮 3-企業補財庫-北港武德宮 4-天赦日補運-桃園威天宮 5-天赦日祭改-進寶財神廟 
        /// 71-九九重陽天赦日補運-桃園威天宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_SuppliesInfo(string name, string phone, int adminID, int kind, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (kind)
            {
                case 1:
                    //下元補庫-北港武德宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 2:
                    //呈疏補庫-北港武德宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge2 Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 3:
                    //企業補財庫-北港武德宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 4:
                    //天赦日補運-桃園威天宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies_ty_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 5:
                    //天赦日祭改-進寶財神廟
                    sql = "Select * from Temple_" + Year + "..view_Supplies_jb_InfowithAPPCharge Where SuppliesType = 5 and Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 71:
                    //九九重陽天赦日補運-桃園威天宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies2_ty_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 9:
                    //補財庫
                    switch (adminID)
                    {
                        case 21:
                            sql = "Select * from Temple_" + Year + "..view_Supplies_Lk_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and " +
                                "(AppName = @Name and AppMobile = @Mobile)";
                            break;
                    }
                    break;
                case 10:
                    //財神賜福-消災補庫法會-進寶財神廟
                    sql = "Select * from Temple_" + Year + "..view_Supplies_jb_InfowithAPPCharge Where SuppliesType = 10 and Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 111:
                    //赦罪補庫-神霄玉府財神會館
                    sql = "Select * from Temple_" + Year + "..view_Supplies_sx_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
                case 18:
                    //天公生招財補運-桃園威天宮
                    sql = "Select * from Temple_" + Year + "..view_Supplies3_ty_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得關聖帝君聖誕購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_EmperorGuanshengInfo(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            //關聖帝君聖誕-桃園威天宮
            sql = "Select * from Temple_" + Year + "..view_EmperorGuansheng_ty_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";


            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得靈寶禮斗購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_LingbaolidouInfo(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            //關聖帝君聖誕-桃園威天宮
            sql = "Select * from Temple_" + Year + "..view_Lingbaolidou_ma_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";


            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得七朝清醮購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_TaoistJiaoCeremonyInfo(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            //七朝清醮-大甲鎮瀾宮
            sql = "Select * from Temple_" + Year + "..view_TaoistJiaoCeremony_da_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and " +
                "(AppName = @Name and AppMobile = @Mobile)";


            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得護國息災梁皇大法會購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable Getapplicantinfo_Lybc(string name, string phone, int adminID, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            //護國息災梁皇大法會-台東東海龍門天聖宮
            sql = "Select * from Temple_" + Year + "..view_Lybc_dh_InfowithAPPCharge Where Num > 0 and AppStatus = 2 and Status = 0 and AdminID = @AdminID and " +
                "(AppName = @Name and AppMobile = @Mobile)";


            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈購買人資料(大甲鎮瀾宮、新港奉天宮)
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Lights(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_Lights Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Purdue(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_Purdue Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮普度購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Purdue_h(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_h_Purdue Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Purdue_wu(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_wu_Purdue Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(string Num2String)
        {
            string sql = "Select * from ApplicantInfo_Moneymother Where Status = 2 and Num2String = @Num2String";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得繞境商品購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Pilgrimage(string Num2String)
        {
            string sql = "Select * from ApplicantInfo_Pilgrimage Where Status = 2 and Num2String = @Num2String";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品小舖購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Product(string Num2String)
        {
            string sql = "Select * from view_ProductInfolistwithAPPCharge Where AppStatus = 2 and Num2String = @Num2String";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品小舖購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廠商編號 5-文創商品小舖</param>
        /// </summary>
        public DataTable Getapplicantinfo_Product(string name, string mobile, int adminID)
        {
            string sql = string.Empty;

            sql = "Select * from view_ProductInfolistwithAPPCharge Where AppStatus = 2 and Name = @AppName and Mobile = @AppMoible and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AppName", name);
            objDatabaseAdapter.AddParameterToSelectCommand("@AppMoible", mobile);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品小舖購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廠商編號 5-文創商品小舖</param>
        /// </summary>
        public DataTable Getapplicantinfo_Product(string name, string mobile, int adminID, string Year)
        {
            string sql = string.Empty;

            sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppStatus = 2 and Name = @Name and Mobile = @Mobile and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("@Mobile", mobile);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(string name, string phone, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where AppStatus = 2 and AppcStatus = 1 and Mobile = @Mobile and AppName = @Name ";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_Moneymother Where Status = 2 and Mobile = @Mobile";

            if (name != "")
            {
                sql += " and Name = @Name ";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if (name != "")
            {
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            }
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買資料
        /// <param name="ApplicantID">ApplicantID=編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(int ApplicantID, int AdminID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where AppStatus = 2 and Num > 0 and AppcStatus = 1 and " +
                "ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", ApplicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品購買資料
        /// <param name="ApplicantID">ApplicantID=編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Moneymother(int ApplicantID, int AdminID)
        {
            string sql = "Select * from view_Product_MoneymotherwithAPPCharge Where AppStatus = 2 and ApplicantID = @ApplicantID and AdminID = @AdminID and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", ApplicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得繞境商品購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Pilgrimage(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_Pilgrimage Where Status = 2 and Mobile = @Mobile";

            if (name != "")
            {
                sql += " and Name = @Name ";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if (name != "")
            {
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            }
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得繞境商品購買資料
        /// <param name="ApplicantID">ApplicantID=編號</param>
        /// <param name="AdminID">AdminID=廠商編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_Pilgrimage(int ApplicantID, int AdminID)
        {
            string sql = "Select * from view_Product_PilgrimagewithAPPCharge Where AppStatus = 2 and ApplicantID = @ApplicantID and AdminID = @AdminID and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", ApplicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Supplies_wu(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_wu_Supplies Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="Num2String">Num2String=訂單編號</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="type">type=補財庫活動 1-下元補庫 2-呈疏補庫 3-企業補財庫</param>
        /// </summary>
        public DataTable Getapplicantinfo_Supplies_wufromNum2String(string Num2String, int adminID, int type, string Year)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 4:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 6:
                    switch (type)
                    {
                        case 1:
                            sql = "Select * from view_Supplies_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                            break;
                        case 2:
                            sql = "Select * from view_Supplies_wu_info2 Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                            break;
                        case 3:
                            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_info3 Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and Num2String = @Num2String";
                            break;
                    }

                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Num2String", Num2String);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="phone">phone=購買人手機or祈福人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="type">type=補財庫活動 1-下元補庫 2-呈疏補庫</param>
        /// </summary>
        public DataTable Getapplicantinfo_Supplies_wu(string phone, int adminID, int type)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 4:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 6:
                    switch (type)
                    {
                        case 1:
                            sql = "Select * from view_Supplies_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                            break;
                        case 2:
                            sql = "Select * from view_Supplies_wu_info2 Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                            break;
                    }

                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=購買人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="type">type=補財庫活動 1-下元補庫 2-呈疏補庫</param>
        /// </summary>
        public DataTable Getapplicantinfo_Supplies_wu(string name, string phone, int adminID, int type)
        {
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (adminID)
            {
                case 3:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 4:
                    //sql = "Select * from view_LightsInfo Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (Mobile = @Mobile or AppMobile = @Mobile)";
                    break;
                case 6:
                    switch (type)
                    {
                        case 1:
                            sql = "Select * from view_Supplies_wu_info Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                            break;
                        case 2:
                            sql = "Select * from view_Supplies_wu_info2 Where AppStatus = 2 and Status = 0 and AdminID = @AdminID and (AppName = @Name and AppMobile = @Mobile)";
                            break;
                    }

                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
                objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Supplies_wu2(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_wu_Supplies2 Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮點燈購買人資料
        /// <param name="name">name=購買人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo_Lights_wu(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_wu_Lights Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得付款資料
        /// </summary>
        public DataTable Getappcharge()
        {
            string sql = "Select * from APPCharge Where Status = 1";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得付款資料
        /// <param name="mobile">mobile=電話</param>
        /// </summary>
        public DataTable Getappcharge(string mobile)
        {
            string sql = "Select * from view_APPCharge Where Status = 1 and Mobile = @mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("mobile", mobile);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得付款資料
        /// <param name="UniqueID">UniqueID=付款編號</param>
        /// </summary>
        public DataTable Getappcharge(int UniqueID)
        {
            string sql = "Select * from view_APPCharge Where Status = 1 and UniqueID = @UniqueID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("UniqueID", UniqueID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得廠商編號
        /// <param name="UniqueID">UniqueID=付款編號</param>
        /// </summary>
        public int GetAdminID(int applicantID)
        {
            int result = 0;
            string sql = "Select * from ApplicantInfo_Purdue Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = int.Parse(dtGetData.Rows[0]["AdminID"].ToString());
            }

            return result;
        }

        /// <summary>
        /// 使用折扣碼
        /// <param name="Code">Code=折扣碼</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public int UseDiscountCode(string Code, int adminID, int applicantID)
        {
            int result = 0;
            string sql = "Select * from DiscountCode Where Status = 0 and Code = @Code";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("Code", Code);
            DataTable dtGetData = new DataTable();
            AdapterObj.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = int.Parse(dtGetData.Rows[0]["DiscountID"].ToString());
                dtGetData.Rows[0]["AdminID"] = adminID;
                dtGetData.Rows[0]["ApplicantID"] = applicantID;
                AdapterObj.Update(dtGetData);

            }

            return result;
        }

        /// <summary>
        /// 使用折扣碼
        /// <param name="Code">Code=折扣碼</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public int UseCAPTCHACode(string Code, int adminID, int applicantID, int kind, string Year)
        {
            int result = 0;
            string sql = "Select * from Temple_" + Year + "..CAPTCHACode Where Status = 0 and Code = @Code";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("Code", Code);
            DataTable dtGetData = new DataTable();
            AdapterObj.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = int.Parse(dtGetData.Rows[0]["CodeID"].ToString());
                dtGetData.Rows[0]["AdminID"] = adminID;
                dtGetData.Rows[0]["ApplicantID"] = applicantID;
                AdapterObj.Update(dtGetData);

            }

            return result;
        }

        /// <summary>
        /// 取得宮廟列表
        /// <param name="Permission">Permission=權限 0-超級管理員 1-管理員 9-宮廟會員 8-商品販賣會員</param>
        /// </summary>
        public DataTable GetAdminList(int Permission)
        {
            string sql = "Select * from Admin Where Status = 0 and Permission = @Permission";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Permission", Permission);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable GetLights_info(int applicantID, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_LightsInfo Where Status = 0 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID and AdminID = @AdminID Order by CreateDate";
                    break;
                case 4:
                    sql = "Select * from view_LightsInfo Where Status = 0 and AppStatus = 2 and Num > 0  and ApplicantID = @ApplicantID and AdminID = @AdminID Order by CreateDate";
                    break;
                case 6:
                    sql = "Select * from view_Lights_wu_info Where Status = 0 and AppStatus = 2 and Num > 0  and ApplicantID = @ApplicantID Order by CreateDate";
                    break;
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈資料
        /// <param name="mobile">mobile=購買手機or祈福人手機</param>
        /// <param name="adminID">adminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 6-北港武德宮</param>
        /// </summary>
        public DataTable GetLights_info(string mobile, int adminID)
        {
            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    sql = "Select * from view_LightsInfo Where Status = 0 and Mobile = @Mobile & AdminID = @AdminID";
                    break;
                case 4:
                    sql = "Select * from view_LightsInfo Where Status = 0 and Mobile = @Mobile & AdminID = @AdminID";
                    break;
                case 6:
                    sql = "Select * from view_Lights_wu_info Where Status = 0 and Mobile = @Mobile";
                    break;
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", mobile);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈資料(大甲鎮瀾宮、新港奉天宮)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetLightsinfo(int applicantID, int adminID)
        {
            string sql = "Select * from view_LightsInfo Where Status = 0 and ApplicantID = @ApplicantID & AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得贊普資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getzampinfo(int applicantID)
        {
            string sql = "Select * from ZampInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得贊普資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetzampinfoandReceiveInfo(int applicantID)
        {
            string sql = "Select * from view_ZampInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_da_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_da_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_h_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_h_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_wu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_Fu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_Luer_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_Luer_info(int applicantID)
        {
            string sql = "Select * from view_Lights_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮孝親祈福燈點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_ty_mom_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ty_mom_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得斗六五路財神宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_Fw_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Fw_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_dh_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_dh_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_Lk_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Lk_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_ma_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ma_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得進寶財神廟點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_jb_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_jb_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台灣道教總廟無極三清總道院點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_wjsan_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_wjsan_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園龍德宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlights_ld_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ld_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_da_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_da_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_h_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_h_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_wu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Fu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園大廟景福宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Jing_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Jing_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Luer_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得斗六五路財神宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Fw_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Fw_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_dh_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_dh_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Lk_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Lk_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_ma_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_ma_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鎮瀾買足普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_mazu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_mazu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_da_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_da_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_h_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_h_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_wu_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Fu_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得景福宮普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Jing_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_Jing_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟普度資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getpurdue_Luer_info(int applicantID)
        {
            string sql = "Select * from view_Purdue_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮關聖帝君聖誕資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetemperorGuansheng_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_EmperorGuansheng_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮靈寶禮斗資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlingbaolidou_ma_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lingbaolidou_ma_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮七朝清醮資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GettaoistJiaoCeremony_da_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_TaoistJiaoCeremony_da_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮護國息災梁皇大法會資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlybc_dh_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lybc_dh_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮天赦日補運資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮九九重陽天赦日補運資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies2_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies2_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮天公生招財補運資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies3_ty_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies3_ty_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮天貺納福添運法會資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_dh_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_dh_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得神霄玉府赦罪補庫資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_sx_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_sx_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟補財庫資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_Lk_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_Lk_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_wu_info(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_wu_info2(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_info2 Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_wu_info(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(呈疏補庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_wu_info2(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_wu_info2 Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(企業補財庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsupplies_wu_info3(int applicantID, string Year)
        {
            string sql = "Select * from Temple_"+ Year + "..view_Supplies_wu_info3 Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮企業補財庫訂閱日期
        /// </summary>
        public string GetSupplies_wu_ListSubDate(string Year, string startdate)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            if (startdate == "")
            {
                startdate = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string result = string.Empty;
            string sql = "Select Top 1 AppCreateDate, AppCreateDateString from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where Num > 0 and Status = 0 and AppStatus = 2 and AppcStatus = 1  and NewUser = 0 and ChargeCycle = 1 and AppCreateDate < '" + startdate + "' Order by AppCreateDate Desc";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = dtGetData.Rows[0]["AppCreateDateString"].ToString();
            }

            return result;
        }

        /// <summary>
        /// 檢查此購買人電話是否在舊用戶訂閱過
        /// </summary>
        /// <param name="oldUserDate">oldUserDate=上個月舊用戶訂閱日期</param>
        /// <param name="AdminID">AdminID=宮廟編號</param>
        /// <param name="AppMobile">AppMobile=購買人電話</param>
        /// <returns></returns>
        public bool CheckedSupplies_wu_ListOldDate(string oldUserDate, string AdminID, string AppMobile, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string startDate = oldUserDate + " 00:00:00";
            string endDate = oldUserDate + " 23:59:59";

            //企業補財庫
            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where Num > 0 and Status = 0 and AppStatus = 2 and AppcStatus = 1 and NewUser = 0 and ChargeCycle = 1 and AdminID = @AdminID and AppMobile = @AppMobile and AppCreateDate BETWEEN '" + startDate + "' And '" + endDate + "'";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            objDatabaseAdapter.AddParameterToSelectCommand("@AppMobile", AppMobile);
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查此購買人電話是否在新用戶訂閱過
        /// </summary>
        /// <param name="UserDate_start"></param>
        /// <param name="UserDate_end"></param>
        /// <param name="AdminID"></param>
        /// <param name="AppMobile"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool CheckedSupplies_wu_ListNewDate(string UserDate_start, string UserDate_end, string AdminID, string AppMobile, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string NewDate_start = UserDate_start + " 00:00:00";
            string NewDate_end = UserDate_end + " 23:59:59";

            //企業補財庫
            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where Num > 0 and Status = 0 and AppStatus = 2 and AppcStatus = 1 and NewUser = 1 and ChargeCycle = 1 and AdminID = @AdminID and AppMobile = @AppMobile and AppCreateDate between '" + NewDate_start + "' and '" + NewDate_end + "'";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            objDatabaseAdapter.AddParameterToSelectCommand("@AppMobile", AppMobile);
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 取得錢母商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetProduct_Moneymother(int applicantID)
        {
            string sql = "Select * from view_Product_Moneymother Where Status = 0 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="type">type=商品種類：1-鎮宅、開運錢母擺件 2-開運隨身御守</param>
        /// </summary>
        public DataTable GetProduct_Moneymother(int applicantID, int type)
        {
            string sql = "Select * from view_Product_Moneymother Where Status = 0 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID and Type = @Type";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得錢母商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="type">type=商品種類：1-鎮宅、開運錢母擺件 3-開運隨身御守 4-2024新港奉天宮黃金符令手鍊 5-招財大嘴貓(白色) 6-招財大嘴貓(藍色) 7-招財大嘴貓(粉色) 8-招財大嘴貓(橘色)</param>
        /// </summary>
        public DataTable GetProduct_Moneymother(int applicantID, int type, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and " +
                "ApplicantID = @ApplicantID and Type = @Type";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮繞競商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetProduct_Pilgrimage(int applicantID)
        {
            string sql = "Select * from view_Product_Pilgrimage Where Status = 0 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮繞競商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="type">type=商品種類：1-鎮宅、開運錢母擺件 2-開運隨身御守</param>
        /// </summary>
        public DataTable GetProduct_Pilgrimage(int applicantID, int type)
        {
            string sql = "Select * from view_Product_Pilgrimage Where Status = 0 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID and Type = @Type";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetProduct_info(int applicantID)
        {
            string sql = "Select * from view_ProductInfolist Where Status = 0 and appStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得文創商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// <param name="type">type=商品種類：1-鎮宅、開運錢母擺件 2-開運隨身御守</param>
        /// </summary>
        public DataTable GetProduct_info(int applicantID, int type)
        {
            string sql = "Select * from view_ProductInfolist Where Status = 0 and appStatus = 2 and Num > 0 and ApplicantID = @ApplicantID and Type = @Type";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮錢母付款資料
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Moneymother(string StartDate, string EndDate)
        {
            string sql = "Select * from APPCharge_Moneymother Where Status = 1 and (CreateDate between @StartDate and @EndDate)";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate", StartDate);
            objDatabaseAdapter.AddParameterToSelectCommand("@EndDate", EndDate);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮繞境付款資料
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Pilgrimage(string StartDate, string EndDate)
        {
            string sql = "Select * from APPCharge_Pilgrimage Where Status = 1 and (CreateDate between @StartDate and @EndDate)";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate", StartDate);
            objDatabaseAdapter.AddParameterToSelectCommand("@EndDate", EndDate);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_da_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_da_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_h_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_h_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_wu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Fu_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_Fu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園大廟景福宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Jing_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_Jing_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Luer_Purdue(int applicantID)
        {
            string sql = "Select * from view_Purdue_Luer_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies2(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_InfowithAPPCharge2 Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫付款資料
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies2(string StartDate, string EndDate)
        {
            string sql = "Select * from APPCharge_wu_Supplies2 Where Status = 1 and (CreateDate between @StartDate and @EndDate)";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate", StartDate);
            objDatabaseAdapter.AddParameterToSelectCommand("@EndDate", EndDate);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_da_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_da_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_h_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_h_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_wu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Fu_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Fu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Luer_Lights(int applicantID)
        {
            string sql = "Select * from view_Lights_Luer_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Luer_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Luer_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮孝親祈福燈點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_mom_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ty_mom_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得斗六五路財神宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Fw_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Fw_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_dh_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_dh_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Hs_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Hs_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Jt_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Jt_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Am_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Am_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Lk_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_Lk_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ma_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ma_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得進寶財神廟點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_jb_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_jb_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台灣道教總廟無極三清總道院點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wjsan_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_wjsan_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園龍德宮點燈付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ld_Lights(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lights_ld_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_da_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_da_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得新港奉天宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_h_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_h_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_wu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得西螺福興宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Fu_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Fu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台南正統鹿耳門聖母廟普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Luer_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Luer_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得斗六五路財神宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Fw_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Fw_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_dh_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_dh_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Lk_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_Lk_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ma_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_ma_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鎮瀾買足普度付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_mazu_Purdue(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Purdue_mazu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮關聖帝君聖誕付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_EmperorGuansheng(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_EmperorGuansheng_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得玉敕大樹朝天宮靈寶禮斗付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ma_Lingbaolidou(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lingbaolidou_ma_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得大甲鎮瀾宮七朝清醮付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_da_TaoistJiaoCeremony(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_TaoistJiaoCeremony_da_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮護國息災梁皇大法會付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_dh_Lybc(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Lybc_dh_infowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮天赦日補運付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_Supplies(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮九九重陽天赦日補運付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_Supplies2(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies2_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = " +
                "@ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得桃園威天宮天公生招財補運付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_ty_Supplies3(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies3_ty_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = " +
                "@ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得台東東海龍門天聖宮天貺納福添運法會付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_dh_Supplies(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_dh_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得鹿港城隍廟補財庫付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_Lk_Supplies(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_Lk_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得神霄玉府財神會館赦罪補庫付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_sx_Supplies(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_sx_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(呈疏補庫)付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies2(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge2 Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(企業補財庫)付款資料
        /// </summary>
        /// <param name="applicantID"></param>
        /// <returns></returns>
        public DataTable GetAPPCharge_wu_Supplies3(int applicantID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where AppcStatus = 1 and AppStatus = 2 and Num > 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得付款資料
        /// <param name="Transaction_id">Transaction_id</param>
        /// <param name="adminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗 
        /// 13-七朝清醮 14-九九重陽天赦日補運 15-護國息災梁皇大法會 16-補財庫 17-赦罪補庫</param>
        /// </summary>
        public DataTable Getappcharge(string Transaction_id, string adminID, string kind, string Year)
        {
            string sql = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "4":
                            //新港奉天宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_h_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "6":
                            //北港武德宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "8":
                            //西螺福興宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_Fu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            sql = "Select * from Temple_" + Year + "..view_Lights_Luer_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "15":
                            //斗六五路財神宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_Fw_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "21":
                            //鹿港城隍廟
                            sql = "Select * from Temple_" + Year + "..view_Lights_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "29":
                            //進寶財神廟
                            sql = "Select * from Temple_" + Year + "..view_Lights_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "31":
                            //台灣道教總廟無極三清總道院
                            sql = "Select * from Temple_" + Year + "..view_Lights_wjsan_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "32":
                            //桃園龍德宮
                            sql = "Select * from Temple_" + Year + "..view_Lights_ld_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "4":
                            //新港奉天宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_h_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "6":
                            //北港武德宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "8":
                            //西螺福興宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_Fu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        //case "9":
                        //    //景福宮
                        //    sql = "Select * from Temple_" + Year + "..view_Purdue_Jing_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                        //    break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            sql = "Select * from Temple_" + Year + "..view_Purdue_Luer_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "15":
                            //斗六五路財神宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_Fw_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "21":
                            //鹿港城隍廟
                            sql = "Select * from Temple_" + Year + "..view_Purdue_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select * from Temple_" + Year + "..view_Purdue_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "30":
                            //鎮瀾買足
                            sql = "Select * from Temple_" + Year + "..view_Purdue_mazu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    switch (adminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "7":
                            //繞境商品小舖
                            sql = "Select * from Temple_" + Year + "..view_Product_PilgrimagewithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "11":
                            //錢母商品小舖
                            sql = "Select * from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "20":
                            //文創商品-西螺福興宮
                            sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "22":
                            //流金富貴商品小舖
                            sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "28":
                            //財神小舖商品小舖
                            sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "4":
                    //下元補庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "5":
                    //呈疏補庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge2 Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "6":
                    //企業補財庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "7":
                    //天赦日補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "8":
                    //天赦日祭改
                    switch (adminID)
                    {
                        case "29":
                            //進寶財神廟
                            sql = "Select * from Temple_" + Year + "..view_Supplies_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "9":
                    //關聖帝君聖誕
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_EmperorGuansheng_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "10":
                    //代燒金紙
                    switch (adminID)
                    {
                        case "29":
                            //進寶財神廟
                            sql = "Select * from Temple_" + Year + "..view_BPO_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "11":
                    //天貺納福添運法會
                    switch (adminID)
                    {
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "12":
                    //靈寶禮斗
                    switch (adminID)
                    {
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select * from Temple_" + Year + "..view_Lingbaolidou_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "13":
                    //七朝清醮
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select * from Temple_" + Year + "..view_TaoistJiaoCeremony_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "14":
                    //九九重陽天赦日補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies2_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "16":
                    //補財庫
                    switch (adminID)
                    {
                        case "21":
                            //鹿港城隍廟
                            sql = "Select * from Temple_" + Year + "..view_Supplies_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "17":
                    //赦罪補庫
                    switch (adminID)
                    {
                        case "33":
                            //神霄玉府財神會館
                            sql = "Select * from Temple_" + Year + "..view_Supplies_sx_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "18":
                    //天公生招財補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select * from Temple_" + Year + "..view_Supplies3_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
            }

            DataTable dtGetData = new DataTable();
            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                //objDatabaseAdapter.AddParameterToSelectCommand("Transaction_id", Transaction_id);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得付款資料
        /// <param name="Transaction_id">Transaction_id</param>
        /// <param name="adminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗 
        /// 13-七朝清醮 14-九九重陽天赦日補運 15-護國息災梁皇大法會 16-補財庫 17-赦罪補庫</param>
        /// </summary>
        public DataTable GetappchargeNum2String(string Transaction_id, string adminID, string kind, string Year)
        {
            string sql = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "4":
                            //新港奉天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_h_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "6":
                            //北港武德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "8":
                            //西螺福興宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_Fu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_Luer_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "15":
                            //斗六五路財神宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_Fw_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "21":
                            //鹿港城隍廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "29":
                            //進寶財神廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "31":
                            //台灣道教總廟無極三清總道院
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_wjsan_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "32":
                            //桃園龍德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lights_ld_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "4":
                            //新港奉天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_h_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "6":
                            //北港武德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "8":
                            //西螺福興宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_Fu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        //case "9":
                        //    //景福宮
                        //    sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_Jing_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                        //    break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_Luer_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "15":
                            //斗六五路財神宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_Fw_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "21":
                            //鹿港城隍廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "30":
                            //鎮瀾買足
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Purdue_mazu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    switch (adminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "7":
                            //繞境商品小舖
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Product_PilgrimagewithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "11":
                            //錢母商品小舖
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "20":
                            //文創商品-西螺福興宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "22":
                            //流金富貴商品小舖
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "28":
                            //財神小舖商品小舖
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "4":
                    //下元補庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "5":
                    //呈疏補庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge2 Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "6":
                    //企業補財庫
                    switch (adminID)
                    {
                        case "6":
                            //北港武德宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_wu_InfowithAPPCharge3 Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "7":
                    //天赦日補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                        case "23":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "8":
                    //天赦日祭改
                    switch (adminID)
                    {
                        case "29":
                            //進寶財神廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "9":
                    //關聖帝君聖誕
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_EmperorGuansheng_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "10":
                    //代燒金紙
                    switch (adminID)
                    {
                        case "29":
                            //進寶財神廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_BPO_jb_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "11":
                    //天貺納福添運法會
                    switch (adminID)
                    {
                        case "16":
                            //台東東海龍門天聖宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_dh_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "12":
                    //靈寶禮斗
                    switch (adminID)
                    {
                        case "23":
                            //玉敕大樹朝天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Lingbaolidou_ma_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "13":
                    //七朝清醮
                    switch (adminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_TaoistJiaoCeremony_da_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "14":
                    //九九重陽天赦日補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies2_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "16":
                    //補財庫
                    switch (adminID)
                    {
                        case "21":
                            //鹿港城隍廟
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_Lk_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "17":
                    //赦罪補庫
                    switch (adminID)
                    {
                        case "33":
                            //神霄玉府財神會館
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies_sx_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
                case "18":
                    //天公生招財補運
                    switch (adminID)
                    {
                        case "14":
                            //桃園威天宮
                            sql = "Select Num2String, AdminID, OrderID, [Description] from Temple_" + Year + "..view_Supplies3_ty_InfowithAPPCharge Where AppcStatus = 1 and Transaction_id = N'" + Transaction_id + "'";
                            break;
                    }
                    break;
            }

            DataTable dtGetData = new DataTable();
            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                //objDatabaseAdapter.AddParameterToSelectCommand("Transaction_id", Transaction_id);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(下元補庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetSupplies_wu_info(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetSupplies_wu_info2(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_info2 Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得補財庫(企業補財庫)資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetSupplies_wu_info3(int applicantID)
        {
            string sql = "Select * from view_Supplies_wu_info3 Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得北港武德宮點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable GetLights_wu_info(int applicantID)
        {
            string sql = "Select * from view_Lights_wu_info Where Status = 0 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得超拔資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getsalvationinfo(int applicantID)
        {
            string sql = "Select * from SalvationInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得收件資料
        /// <param name="zampID">zampID=贊普編號</param>
        /// </summary>
        public DataTable Getreceiveinfo(int zampID)
        {
            string sql = "Select * from ReceiveInfo Where Status = 0 and ZampID = @ZampID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ZampID", zampID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getlightsinfo(int applicantID)
        {
            string sql = "Select * from LightsInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得付費狀態
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public int GetappchargeStatus(int applicantID)
        {
            int status = -1;
            string sql = "Select * from view_APPCharge_Purdue Where ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                status = int.Parse(dtGetData.Rows[0]["Status"].ToString());
            }

            return status;
        }

        /// <summary>
        /// 取得序號資料
        /// <param name="code">code=序號</param>
        /// </summary>
        public int GetCodeInfo(string code)
        {
            int status = -1;
            string sql = "Select * from CodeInfo Where Code = @code";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("code", code);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                status = int.Parse(dtGetData.Rows[0]["Status"].ToString());
            }

            return status;
        }

        /// <summary>
        /// 取得資料最後時間
        /// <param name="aid">aid=資料表編號</param>
        /// <param name="adminID">adminID=宮廟編號</param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗 
        /// 13-七朝清醮 14-九九重陽天赦日補運 15-護國息災梁皇大法會 16-補財庫 17-赦罪補庫 18-天公生招財補運</param>
        /// <returns></returns>
        public DateTime GetInfoLastDate(int aid, int adminID, int kind, int type, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 13:
                            //七朝清醮
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_TaoistJiaoCeremony Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_h_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_h_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 5:
                    //商品販賣小舖
                    sql = "Select * from ApplicantInfo_Product Where ApplicantID = @aid and AdminID = " + adminID;
                    break;
                case 6:
                    //北港武德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 4:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 5:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies2 Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 6:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies3 Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fu_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fu_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 9:
                    //景福宮
                    switch (kind)
                    {
                        case 1:
                            //sql = "Select * from ApplicantInfo_Jing_Lights Where ApplicantID = @aid and AdminID = 3";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Jing_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Luer_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Luer_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (kind)
                    {
                        case 1:
                            switch (type)
                            {
                                case 1:
                                    //一般點燈
                                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                                    break;
                                case 2:
                                    //活動-孝親祈福燈
                                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                                    break;
                            }
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 7:
                            //天赦日補運
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 9:
                            //關聖帝君聖誕
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_EmperorGuansheng Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 14:
                            //九九重陽天赦日
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Supplies2 Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 18:
                            //天公生招財補運
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Supplies3 Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fw_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fw_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 11:
                            //天貺納福添運法會
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 15:
                            //護國息災梁皇大法會
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Lybc Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Lk_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Lk_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 16:
                            //補財庫
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Lk_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ma_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ma_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 12:
                            //靈寶禮斗
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ma_Lingbaolidou Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_jb_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 8:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_jb_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 30:
                    //鎮瀾買足
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_mazu_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_mazu_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wjsan_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..ApplicantInfo_wjsan_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 32:
                    //桃園龍德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ld_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..ApplicantInfo_ld_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
                case 33:
                    //神霄玉府財神會館
                    switch (kind)
                    {
                        //case 1:
                        //    sql = "Select * from Temple_" + Year + "..ApplicantInfo_sx_Lights Where ApplicantID = @aid and AdminID = " + adminID;
                        //    break;
                        //case 2:
                        //    sql = "Select * from Temple_" + Year + "..ApplicantInfo_sx_Purdue Where ApplicantID = @aid and AdminID = " + adminID;
                        //    break;
                        case 17:
                            //赦罪補庫
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_sx_Supplies Where ApplicantID = @aid and AdminID = " + adminID;
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@aid", aid);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                if (dtGetData.Rows.Count > 0)
                {
                    dtNow = DateTime.Parse(dtGetData.Rows[0]["CreateDate"].ToString());
                }
            }

            return dtNow;
        }

        /// <summary>
        /// 取得FET對應商品編號資料
        /// <param name="id">id=編號</param>
        /// </summary>
        public DataTable GetTempleCodeInfo(string id)
        {
            string sql = string.Empty; ;
            DataTable dtGetData = new DataTable();

            sql = "Select * from view_TempleCode where ProductCode = @Id";

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@Id", id);
                objDatabaseAdapter.Fill(dtGetData);
            }

            return dtGetData;
        }

        /// <summary>
        /// 檢查數量
        /// <param name="count">count=數量</param>
        /// <param name="type">type=商品種類 1-擺件(有刻名) 2-擺件(無刻名) 3-香火袋</param>
        /// <param name="overStatus">overStatus=超過數量的狀態 -1-已額滿 -2-數量不足</param>
        /// </summary>
        public bool CheckedProductstock(int count, int type, out int overStatus)
        {
            bool result = false;
            string sql = string.Empty;
            overStatus = 0;

            if (count == 0)
            { return false; }

            sql = "Select SUM(Count) as TotalCount from view_Product_PilgrimagewithAPPCharge Where Type = @type and AppStatus = 2 and AppcStatus = 1";

            if (type == 1)
            {
                sql += " and SculptureName != N'無'";
            }
            else
            {
                sql += " and SculptureName = N'無'";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if (type == 1 || type == 2)
            {
                objDatabaseAdapter.AddParameterToSelectCommand("@type", 1);
            }
            else
            {
                objDatabaseAdapter.AddParameterToSelectCommand("@type", 2);
            }
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                int totalcount = 0;
                int.TryParse(dtGetData.Rows[0]["TotalCount"].ToString(), out totalcount);

                if (type == 1)
                {
                    //擺件(有刻名)
                    if (totalcount >= 500)
                    {
                        result = true;
                        overStatus = -1;
                    }
                    else
                    {
                        overStatus = totalcount + count > 500 ? -2 : overStatus;
                        if (overStatus == -2)
                        {
                            result = true;
                        }
                    }

                }
                else if (type == 2)
                {
                    //擺件(無刻名)
                    if (totalcount >= 500)
                    {
                        result = true;
                        overStatus = -1;
                    }
                    else
                    {
                        overStatus = totalcount + count > 500 ? -2 : overStatus;
                        if (overStatus == -2)
                        {
                            result = true;
                        }
                    }
                }
                else if (type == 3)
                {
                    //香火袋
                    if (totalcount >= 300)
                    {
                        result = true;
                        overStatus = -1;
                    }
                    else
                    {
                        overStatus = totalcount + count > 300 ? -2 : overStatus;
                        if (overStatus == -2)
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查數量-錢母擺件
        /// <param name="count">count=數量</param>
        /// <param name="type">type=商品種類 1-擺件 2- 3-</param>
        /// <param name="overStatus">overStatus=超過數量的狀態 -1-已額滿 -2-數量不足</param>
        /// </summary>
        public bool CheckedProductstock_Moneymother(int count, int type, out int overStatus)
        {
            bool result = false;
            string sql = string.Empty;
            overStatus = 0;

            if (count == 0)
            { return false; }

            sql = "Select SUM(Count) as TotalCount from view_Product_MoneymotherwithAPPCharge Where Type = @type and AppStatus = 2 and AppcStatus = 1";

            if (type == 1)
            {
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if (type == 1)
            {
                objDatabaseAdapter.AddParameterToSelectCommand("@type", 1);
            }
            else
            {
                //objDatabaseAdapter.AddParameterToSelectCommand("@type", 2);
            }
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                int totalcount = 0;
                int.TryParse(dtGetData.Rows[0]["TotalCount"].ToString(), out totalcount);

                if (type == 1)
                {
                    //擺件
                    if (totalcount >= 500)
                    {
                        result = true;
                        overStatus = -1;
                    }
                    else
                    {
                        overStatus = totalcount + count > 500 ? -2 : overStatus;
                        if (overStatus == -2)
                        {
                            result = true;
                        }
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// 檢查數量-文創商品
        /// <param name="count">count=數量</param>
        /// <param name="type">type=商品種類 1-擺件 2- 3-香火袋 4-黃金符令手鍊</param>
        /// <param name="overStatus">overStatus=超過數量的狀態 -1-已額滿 -2-數量不足</param>
        /// </summary>
        public bool CheckedProductstock(int count, string type, out int overStatus, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            overStatus = 0;

            if (count == 0)
            { return false; }

            sql = "Select Num, UseCount from Product Where ProductID = @type and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@type", type);

            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                int UseCount = 0, Num = 0;
                if (int.TryParse(dtGetData.Rows[0]["UseCount"].ToString(), out UseCount) &&
                int.TryParse(dtGetData.Rows[0]["Num"].ToString(), out Num))
                {
                    if (UseCount >= Num)
                    {
                        result = true;
                        overStatus = -1;
                    }
                    else
                    {
                        overStatus = UseCount + count > Num ? -2 : overStatus;
                        if (overStatus == -2)
                        {
                            result = true;
                        }
                    }
                }
                else
                { return false; }
            }

            return result;
        }

        /// <summary>
        /// 檢查數量-錢母擺件
        /// <param name="count">count=數量</param>
        /// <param name="type">type=商品種類 1-擺件 2- 3-香火袋 4-黃金符令手鍊</param>
        /// <param name="overStatus">overStatus=超過數量的狀態 -1-已額滿 -2-數量不足</param>
        /// </summary>
        public bool CheckedProductstock_Moneymother(int count, string type, out int overStatus, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            overStatus = 0;

            if (count == 0)
            { return false; }

            sql = "Select SUM(Count) as TotalCount from Temple_" + Year + "..view_Product_MoneymotherwithAPPCharge Where Type = @type and AppStatus = 2 " +
                "and AppcStatus = 1 and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@type", type);

            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                int totalcount = 0;
                int.TryParse(dtGetData.Rows[0]["TotalCount"].ToString(), out totalcount);

                switch (type)
                {
                    case "1":
                        //鎮宅、開運錢母擺件
                        if (totalcount >= 500)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 500 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                    case "3":
                        //香火袋
                        if (totalcount >= 300)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 300 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                    case "5":
                        //招財大嘴貓(白色)
                        if (totalcount >= 10)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 10 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                    case "6":
                        //招財大嘴貓(藍色)
                        if (totalcount >= 10)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 10 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                    case "7":
                        //招財大嘴貓(粉色)
                        if (totalcount >= 15)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 15 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                    case "8":
                        //招財大嘴貓(橘色)
                        if (totalcount >= 20)
                        {
                            result = true;
                            overStatus = -1;
                        }
                        else
                        {
                            overStatus = totalcount + count > 20 ? -2 : overStatus;
                            if (overStatus == -2)
                            {
                                result = true;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查是否使用過QrCode
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool CheckedCode(int adminID, int applicantID, int kind, string Year, ref string AppTag)
        {
            bool result = true;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Purdue Where ApplicantID = @aid";
                            break;
                        case 13:
                            //七朝清醮
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_TaoistJiaoCeremony Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 5:
                    //商品販賣小舖
                    sql = "Select * from APPCharge_Product Where ApplicantID = @aid";
                    break;
                case 6:
                    //北港武德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Purdue Where ApplicantID = @aid";
                            break;
                        case 4:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies Where ApplicantID = @aid";
                            break;
                        case 5:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 6:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 9:
                    //景福宮
                    switch (kind)
                    {
                        case 1:
                            //sql = "Select * from APPCharge_Jing_Lights Where ApplicantID = @aid and AdminID = 3";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Jing_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Purdue Where ApplicantID = @aid";
                            break;
                        case 7:
                            //天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies Where ApplicantID = @aid";
                            break;
                        case 9:
                            //關聖帝君聖誕
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_EmperorGuansheng Where ApplicantID = @aid";
                            break;
                        case 14:
                            //九九重陽天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 18:
                            //天公生招財補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Purdue Where ApplicantID = @aid";
                            break;
                        case 11:
                            //天貺納福添運法會
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Purdue Where ApplicantID = @aid";
                            break;
                        case 12:
                            //靈寶禮斗
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lingbaolidou Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Lights Where ApplicantID = @aid";
                            break;
                        case 8:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 32:
                    //桃園龍德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ld_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_ld_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@aid", applicantID);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                if (dtGetData.Rows.Count > 0)
                {
                    string[] AppTagArr = dtGetData.Rows[0]["APPTag"].ToString().Split(',');
                    AppTag = AppTagArr[0];
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查是否使用過QrCode
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool CheckedCode(int adminID, int applicantID, int kind, string Year)
        {
            bool result = false;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = string.Empty;

            switch (adminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Purdue Where ApplicantID = @aid";
                            break;
                        case 13:
                            //七朝清醮
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_TaoistJiaoCeremony Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 5:
                    //商品販賣小舖
                    sql = "Select * from APPCharge_Product Where ApplicantID = @aid";
                    break;
                case 6:
                    //北港武德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Purdue Where ApplicantID = @aid";
                            break;
                        case 4:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies Where ApplicantID = @aid";
                            break;
                        case 5:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 6:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 8:
                    //西螺福興宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 9:
                    //景福宮
                    switch (kind)
                    {
                        case 1:
                            //sql = "Select * from APPCharge_Jing_Lights Where ApplicantID = @aid and APPTag = @id and AdminID = 3";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Jing_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 10:
                    //台南正統鹿耳門聖母廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 14:
                    //桃園威天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Purdue Where ApplicantID = @aid";
                            break;
                        case 7:
                            //天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies Where ApplicantID = @aid";
                            break;
                        case 9:
                            //關聖帝君聖誕
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_EmperorGuansheng Where ApplicantID = @aid";
                            break;
                        case 14:
                            //九九重陽天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 18:
                            //天公生招財補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 15:
                    //斗六五路財神宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 16:
                    //台東東海龍門天聖宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Purdue Where ApplicantID = @aid";
                            break;
                        case 11:
                            //天貺納福添運法會
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Purdue Where ApplicantID = @aid";
                            break;
                        case 12:
                            //靈寶禮斗
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lingbaolidou Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Lights Where ApplicantID = @aid";
                            break;
                        case 8:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 32:
                    //桃園龍德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ld_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_ld_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@aid", applicantID);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                if (dtGetData.Rows.Count > 0)
                {
                    string[] AppTagArr = dtGetData.Rows[0]["AppTag"].ToString().Split(',');
                    if (AppTagArr.Length > 1)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查折扣碼
        /// <param name="discountCode">discountCode=折扣碼</param>
        /// </summary>
        public DataTable CheckeddiscountCode(string discountCode)
        {
            string sql = string.Empty;

            sql = "Select * from DiscountCode Where Code = @discountCode";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@discountCode", discountCode);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 檢查折扣碼
        /// <param name="discountCode">discountCode=折扣碼</param>
        /// </summary>
        public bool CheckeddiscountCode(string discountCode, ref int cost)
        {
            bool result = false;
            string sql = string.Empty;

            sql = "Select * from DiscountCode Where Code = @discountCode and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@discountCode", discountCode);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                cost = int.Parse(dtGetData.Rows[0]["Cost"].ToString());
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 檢查折扣碼
        /// <param name="discountCode">discountCode=折扣碼</param>
        /// <param name="cost">cost=折扣金額</param>
        /// <param name="discountType">discountType=折扣類別 0-折扣部分金額 1-免費</param>
        /// </summary>
        public bool CheckeddiscountCode(string discountCode, ref int cost, ref int discountType)
        {
            bool result = false;
            string sql = string.Empty;

            sql = "Select * from DiscountCode Where Code = @discountCode and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@discountCode", discountCode);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                cost = int.Parse(dtGetData.Rows[0]["Cost"].ToString());
                discountType = int.Parse(dtGetData.Rows[0]["DiscountType"].ToString());
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 檢查驗證碼
        /// <param name="Code">Code=驗證碼</param>
        /// </summary>
        public bool CheckedCAPTCHACode(string Code, int ApplicantID, int AdminID, int Kind, string Year, ref string Codeerror)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            bool result = false;
            string sql = string.Empty;

            sql = "Select * from Temple_" + Year + "..CAPTCHACode Where ApplicantID = @ApplicantID and AdminID = @AdminID and  Kind = @Kind and ExpirationDate > @ExpirationDate" +
                " Order by CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            objDatabaseAdapter.AddParameterToSelectCommand("@Kind", Kind);
            objDatabaseAdapter.AddParameterToSelectCommand("@ExpirationDate", dtNow);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                if (Code == dtGetData.Rows[0]["Code"].ToString())
                {
                    result = true;
                }
                else
                {
                    Codeerror = "-3";
                }
            }
            else
            {
                Codeerror = "-2";
            }

            return result;
        }

        /// <summary>
        /// 檢查驗證碼次數
        /// <param name="Code">Code=驗證碼</param>
        /// </summary>
        public bool CheckedCAPTCHACodeCount(int ApplicantID, int AdminID, int Kind, string AppMobile, string Year, ref string Codeerror)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            bool result = false;
            string sql = string.Empty;

            sql = "Select * from Temple_" + Year + "..CAPTCHACode Where ApplicantID = @ApplicantID and AdminID = @AdminID and Kind = @Kind and AppMobile = @AppMobile " +
                "and CreateDate between @StartDate and @EndDate Order by CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            objDatabaseAdapter.AddParameterToSelectCommand("@Kind", Kind);
            objDatabaseAdapter.AddParameterToSelectCommand("@AppMobile", AppMobile);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate", dtNow.ToString("yyyy-MM-dd 00:00:00"));
            objDatabaseAdapter.AddParameterToSelectCommand("@EndDate", dtNow.ToString("yyyy-MM-dd 23:59:59"));
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                if (dtGetData.Rows.Count >= 3)
                {
                    Codeerror = "-5";
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有贊普資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedzampinfo(int applicantID)
        {
            bool result = false;
            string sql = "Select * from ZampInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有超拔資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedsalvationinfo(int applicantID)
        {
            bool result = false;
            string sql = "Select * from SalvationInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有普度資料-新港奉天宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedpurdue_h_info(int applicantID)
        {
            bool result = false;
            string sql = "Select * from Purdue_h_info Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有普度資料-北港武德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedpurdue_wu_info(int applicantID)
        {
            bool result = false;
            string sql = "Select * from Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人電話是否有補財庫(企業補財庫)資料-北港武德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool CheckedSupplies_wu_info3(string AdminID, string AppMobile, string Year)
        {
            bool result = true;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            DataTable dtlist = new DataTable();
            string startdate = GetSupplies_wu_ListSubDate(Year, "");
            if (startdate != "")
            {
                //檢查此購買人電話是否在舊用戶訂閱過
                if (CheckedSupplies_wu_ListOldDate(startdate, AdminID, AppMobile, Year))
                {
                    result = false;
                }
                else
                {
                    string startdate_last = GetSupplies_wu_ListSubDate(Year, startdate);
                    if (startdate != "" && startdate_last != "")
                    {
                        //檢查此購買人電話是否在上個月新用戶訂閱過
                        if (CheckedSupplies_wu_ListNewDate(startdate_last, startdate, AdminID, AppMobile, Year))
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有補財庫(下元補庫)資料-北港武德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedSupplies_wu_info(int applicantID)
        {
            bool result = false;
            string sql = "Select * from Supplies_wu_info Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有補財庫資料-北港武德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedSupplies_wu_info2(int applicantID)
        {
            bool result = false;
            string sql = "Select * from Supplies_wu_info2 Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有點燈資料-北港武德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedLights_wu_info(int applicantID)
        {
            bool result = false;
            string sql = "Select * from Lights_wu_info Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }


        /// <summary>
        /// 檢查購買人是否有點燈資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedlightsinfo(int applicantID)
        {
            bool result = false;
            string sql = "Select * from LightsInfo Where Status = 0 and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 檢查點燈購買人是否有已付款(大甲鎮瀾宮、新港奉天宮)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights(int applicantID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Lights Where Status = 1 and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_da(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_da_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-新港奉天宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_h(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_h_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-北港武德宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_wu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_wu_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款西螺福興宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Fu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Fu_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台南正統鹿耳門聖母廟點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Luer(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Luer_Lights Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台南正統鹿耳門聖母廟點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Luer(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_da_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮孝親祈福燈點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_ty_mom(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_mom_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-斗六五路財神宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Fw(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Fw_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台東東海龍門天聖宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_dh(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_dh_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-五股賀聖宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Hs(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Hs_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-外澳接天宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Jt(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Jt_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-安平開台天后宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Am(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Am_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-鹿港城隍廟點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_Lk(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Lk_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-玉敕大樹朝天宮點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_ma(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ma_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-進寶財神廟點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_jb(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_jb_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台灣道教總廟無極三清總道院點燈
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_wjsan(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_wjsan_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園龍德宮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lights_ld(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ld_Lights Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_da(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_da_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-新港奉天宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_h(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_h_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-北港武德宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_wu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_wu_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-西螺福興宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Fu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_Fu_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園大廟景福宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Jing(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_Jing_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台南正統鹿耳門聖母廟普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Luer(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_Luer_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_ty_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-斗六五路財神宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Fw(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_Fw_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台東東海龍門天聖宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_dh(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_dh_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-鹿港城隍廟普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Lk(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_"+ Year + "..view_APPCharge_Lk_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-玉敕大樹朝天宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_ma(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ma_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-鎮瀾買足普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_mazu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_mazu_Purdue Where AppStatus = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_da(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_da_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-新港奉天宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_h(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_h_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-北港武德宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_wu(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_wu_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-西螺福興宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Fu(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Fu_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-景福宮普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Jing(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Jing_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台南正統鹿耳門聖母廟普度
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Purdue_Luer(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Luer_Purdue Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-新港奉天宮錢母
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Moneymother(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Moneymother Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮繞境
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Pilgrimage(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_Pilgrimage Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮關聖帝君聖誕
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_EmperorGuansheng_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_EmperorGuansheng Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-玉敕大樹朝天宮靈寶禮斗
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lingbaolidou_ma(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ma_Lingbaolidou Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-大甲鎮瀾宮七朝清醮
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_TaoistJiaoCeremony_da(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_da_TaoistJiaoCeremony Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-台東東海龍門天聖宮護國息災梁皇大法會
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Lybc_dh(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_dh_Lybc Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮天赦日補運
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_Supplies Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮九九重陽天赦日補運
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies2_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_Supplies2 Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and " +
                "AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-桃園威天宮天公生招財補運
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies3_ty(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_ty_Supplies3 Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and " +
                "AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-補財庫(下元補庫)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_wu(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_wu_Supplies Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-補財庫
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_wu2(int applicantID, int adminID)
        {
            bool result = true;
            string sql = "Select * from view_APPCharge_wu_Supplies2 Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-補財庫(下元補庫)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_wu(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_wu_Supplies Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-補財庫(呈疏補庫)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_wu2(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_wu_Supplies2 Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-補財庫(企業補財庫)
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_wu3(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_wu_Supplies3 Where Status = 1 and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-鹿港城隍廟補財庫
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_Lk(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_Lk_Supplies Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查購買人是否有已付款-神霄玉府財神會館赦罪補庫
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public bool checkedappcharge_Supplies_sx(int applicantID, int adminID, string Year)
        {
            bool result = true;
            string sql = "Select * from Temple_" + Year + "..view_APPCharge_sx_Supplies Where (AppStatus = 1 or AppStatus = 2) and ApplicantID = @ApplicantID and AdminID = @AdminID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 檢查燈種餘額
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈(智慧燈) 34-元辰斗燈</param>
        /// <param name="adminID">adminID=廟方編號</param>
        /// </summary>
        public bool checkedLightsNum(string LightsType, string AdminID, int Count, int type, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            DataTable dtGetData = new DataTable();

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    sql = "Select * from Temple_" + Year + "..view_Lights_da_infowithAPPCharge Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 and Num > 0 and AppcStatus = 1";
                    break;
                //case "4":
                //    //新港奉天宮
                //    sql = "Select * from Temple_" + Year + "..view_Lights_h_info Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 and Num > 0 and AppcStatus = 1";
                //    break;
                case "6":
                    //北港武德宮
                    sql = "Select * from Temple_" + Year + "..view_Lights_wu_infowithAPPCharge Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 and Num > 0 and AppcStatus = 1";
                    break;
                case "14":
                    //桃園威天宮
                    switch (type)
                    {
                        case 1:
                            //一般點燈
                            sql = "Select * from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 and Num > 0 and AppcStatus = 1";
                            break;
                        case 2:
                            //活動-孝親祈福燈
                            sql = "Select * from Temple_" + Year + "..view_Lights_ty_InfowithAPPCharge Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 and Num > 0 and AppcStatus = 1";
                            break;
                    }
                    break;
                case "23":
                    //玉敕大樹朝天宮
                    sql = "Select * from Temple_" + Year + "..view_Lights_ma_infowithAPPCharge Where AdminID = @AdminID and LightsType = @LightsType and AppStatus = 2 " +
                        "and Num > 0 and AppcStatus = 1";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("AdminID", AdminID);
                objDatabaseAdapter.AddParameterToSelectCommand("LightsType", LightsType);
                objDatabaseAdapter.Fill(dtGetData);
            }

            BasePage basePage = new BasePage();
            AdminDAC objAdminDAc = new AdminDAC(basePage);
            bool checkedcount = true;
            string lightsStr = string.Empty;
            int adminid = 0;
            int lcount = 0;
            string msg = string.Empty;

            lightsStr = dtGetData.Rows.Count > 0 ? dtGetData.Rows[0]["LightsString"].ToString() : "服務項目";

            if (int.TryParse(AdminID, out adminid))
            {
                DataTable dtadminInfo = objAdminDAc.GetAdminInfo(adminid);
                if (dtadminInfo.Rows.Count > 0)
                    msg = dtadminInfo.Rows[0]["Nickname"].ToString() + " " + lightsStr + " 快額滿了。";
            }

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count >= 4000 || dtGetData.Rows.Count + Count > 4000)
                            {
                                result = true;
                            }

                            lcount = 4000 - dtGetData.Rows.Count;
                            break;
                        case "4":
                            //安太歲
                            if (dtGetData.Rows.Count >= 5000 || dtGetData.Rows.Count + Count > 5000)
                            {
                                result = true;
                            }

                            lcount = 4000 - dtGetData.Rows.Count;
                            break;
                        case "5":
                            //文昌燈
                            if (dtGetData.Rows.Count >= 500 || dtGetData.Rows.Count + Count > 500)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 500 - dtGetData.Rows.Count;
                            break;
                    }
                    break;
                //case "4":
                //    //新港奉天宮
                //    switch (LightsType)
                //    {
                //        case "3":
                //            //光明燈
                //            if (dtGetData.Rows.Count > 1000)
                //            {
                //                result = true;
                //            }
                //            break;
                //        case "4":
                //            //安太歲
                //            if (dtGetData.Rows.Count > 1000)
                //            {
                //                result = true;
                //            }
                //            break;
                //    }
                //    break;
                case "6":
                    //北港武德宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count >= 500 || dtGetData.Rows.Count + Count > 500)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 500 - dtGetData.Rows.Count;
                            break;
                        case "4":
                            //安太歲
                            if (dtGetData.Rows.Count >= 500 || dtGetData.Rows.Count + Count > 500)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 500 - dtGetData.Rows.Count;
                            break;
                        case "6":
                            //財神燈
                            if (dtGetData.Rows.Count >= 1000 || dtGetData.Rows.Count + Count > 1000)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 1000 - dtGetData.Rows.Count;
                            break;
                    }
                    break;
                case "14":
                    //桃園威天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count >= 6000 || dtGetData.Rows.Count + Count > 6000)
                            {
                                result = true;
                            }

                            lcount = 6000 - dtGetData.Rows.Count;
                            break;
                        case "4":
                            //安太歲
                            if (dtGetData.Rows.Count >= 3000 || dtGetData.Rows.Count + Count > 3000)
                            {
                                result = true;
                            }

                            lcount = 3000 - dtGetData.Rows.Count;
                            break;
                        case "33":
                            //智慧燈
                            if (dtGetData.Rows.Count >= 3000 || dtGetData.Rows.Count + Count > 3000)
                            {
                                result = true;
                            }

                            lcount = 3000 - dtGetData.Rows.Count;
                            break;
                        case "6":
                            //財神燈
                            if (dtGetData.Rows.Count >= 3000 || dtGetData.Rows.Count + Count > 3000)
                            {
                                result = true;
                            }

                            lcount = 3000 - dtGetData.Rows.Count;
                            break;
                        case "8":
                            //藥師燈
                            if (dtGetData.Rows.Count >= 1000 || dtGetData.Rows.Count + Count > 1000)
                            {
                                result = true;
                            }

                            lcount = 1000 - dtGetData.Rows.Count;
                            break;
                        case "10":
                            //貴人燈
                            if (dtGetData.Rows.Count >= 1000 || dtGetData.Rows.Count + Count > 1000)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 1000 - dtGetData.Rows.Count;
                            break;
                        case "11":
                            //福祿燈
                            if (dtGetData.Rows.Count >= 300 || dtGetData.Rows.Count + Count > 300)
                            {
                                result = true;
                            }

                            lcount = 300 - dtGetData.Rows.Count;
                            break;
                        case "21":
                            //孝親祈福燈
                            if (dtGetData.Rows.Count >= 3000 || dtGetData.Rows.Count + Count > 3000)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 3000 - dtGetData.Rows.Count;
                            break;
                    }
                    break;
                case "23":
                    //玉敕大樹朝天宮
                    switch (LightsType)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count >= 350 || dtGetData.Rows.Count + Count > 350)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 350 - dtGetData.Rows.Count;
                            break;
                        case "4":
                            //太歲燈
                            if (dtGetData.Rows.Count >= 250 || dtGetData.Rows.Count + Count > 250)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 250 - dtGetData.Rows.Count;
                            break;
                        case "5":
                            //五文昌燈
                            if (dtGetData.Rows.Count >= 300 || dtGetData.Rows.Count + Count > 300)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 300 - dtGetData.Rows.Count;
                            break;
                        case "6":
                            //福財燈
                            if (dtGetData.Rows.Count >= 250 || dtGetData.Rows.Count + Count > 250)
                            {
                                result = true;
                            }

                            checkedcount = false;
                            lcount = 500 - dtGetData.Rows.Count;
                            break;
                    }
                    break;
            }


            if (lcount > 0 && lcount <= 50 && checkedcount)
            {
                SMSHepler objSMSHepler = new SMSHepler();

                objSMSHepler.SendMsg_SL("0934315020", msg);
            }

            return result;
        }


        /// <summary>
        /// 檢查燈種餘額
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="adminID">adminID=廟方編號</param>
        /// </summary>
        public bool checkedlightsnum(string type, string adminID)
        {
            bool result = false;
            string sql = "Select * from view_LightsInfo Where AdminID = @AdminID and Type = @Type and AppStatus = 2 and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            switch (adminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    switch (type)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count > 2000)
                            {
                                result = true;
                            }
                            break;
                        case "4":
                            //安太歲
                            if (dtGetData.Rows.Count > 1200)
                            {
                                result = true;
                            }
                            break;
                        case "5":
                            //文昌燈
                            if (dtGetData.Rows.Count > 300)
                            {
                                result = true;
                            }
                            break;
                    }
                    break;
                case "4":
                    //新港奉天宮
                    switch (type)
                    {
                        case "3":
                            //光明燈
                            if (dtGetData.Rows.Count > 1000)
                            {
                                result = true;
                            }
                            break;
                        case "4":
                            //安太歲
                            if (dtGetData.Rows.Count > 1000)
                            {
                                result = true;
                            }
                            break;
                    }
                    break;
            }



            return result;
        }

        /// <summary>
        /// 檢查燈種餘額
        /// <param name="type">type=燈種 3-光明燈 4-安太歲 5-文昌燈 6-財神燈 7-姻緣燈 8-藥師燈 9-財利燈 10-貴人燈 11-福祿(壽)燈 12-寵物平安燈 13-龍王燈 14-虎爺燈 15-轉運納福燈 
        /// 16-光明燈上層 17-偏財旺旺燈 18-廣進安財庫 19-財庫燈 20-月老姻緣燈 21-孝親祈福燈 22-事業燈 23-全家光明燈 24-觀音佛祖燈 25-財神斗 26-事業斗 27-平安斗 28-文昌斗 29-藥師斗 
        /// 30-元神斗 31-福祿壽斗 32-觀音斗 33-明心智慧燈 34-元辰斗燈</param>
        /// <param name="adminID">adminID=廟方編號</param>
        /// </summary>
        public bool checkedlightsnum_wude(string type, string adminID)
        {
            bool result = false;
            string sql = "Select * from view_Lights_wu_info Where AdminID = @AdminID and Type = @Type and AppStatus = 2 and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            objDatabaseAdapter.AddParameterToSelectCommand("Type", type);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            switch (type)
            {
                case "3":
                    //光明燈
                    if (dtGetData.Rows.Count >= 500)
                    {
                        result = true;
                    }
                    break;
                case "4":
                    //安太歲
                    if (dtGetData.Rows.Count >= 500)
                    {
                        result = true;
                    }
                    break;
                case "5":
                    //財神燈
                    if (dtGetData.Rows.Count >= 1000)
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }









        /// <summary>
        /// 刪除普度功德人資料-新港奉天宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeletePurdue_h_info(int applicantID)
        {
            string sql = "Select * From Purdue_h_info Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除普度功德人資料-北港武德宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeletePurdue_wu_info(int applicantID)
        {
            string sql = "Select * From Purdue_wu_info Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除補財庫(下元補庫)功德人資料-北港武德宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteSupplies_wu_info(int applicantID)
        {
            string sql = "Select * From Supplies_wu_info Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除補財庫功德人資料-北港武德宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteSupplies_wu_info2(int applicantID)
        {
            string sql = "Select * From Supplies_wu_info2 Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除點燈功德人資料-北港武德宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteLights_wu_info(int applicantID)
        {
            string sql = "Select * From Lights_wu_info Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除超拔功德人資料-大甲鎮瀾宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteSlvation(int applicantID)
        {
            string sql = "Select * From SalvationInfo Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除點燈資料-大甲鎮瀾宮、新港奉天宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteLights(int applicantID)
        {
            string sql = "Select * From LightsInfo Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除贊普功德人資料-大甲鎮瀾宮
        /// <param name="applicantID">applicantID=購買人編號</param>
        /// </summary>
        public void DeleteZamp(int applicantID)
        {
            string sql = "Select * From ZampInfo Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }




        public bool Updatestatus2appcharge(int UniqueID, int Status)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From APPCharge Where UniqueID=@UniqueID and Status = 1";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@UniqueID", UniqueID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 1)
            {
                dtDataList.Rows[0]["Status"] = Status;
                AdapterObj.Update(dtDataList);

                if (Updatestatus2applicantinfo(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), Status))
                {
                    bResult = true;
                }

            }


            return bResult;
        }

        public bool Updatestatus2applicantinfo(int ApplicantID, int Status)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo Where ApplicantID=@ApplicantID and Status = 2";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 2)
            {
                dtDataList.Rows[0]["Status"] = Status;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecount2login(string url)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Aspxlogin Where URL=@URL and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@URL", url);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0)
            {
                dtDataList.Rows[0]["Count"] = int.Parse(dtDataList.Rows[0]["Count"].ToString()) + 1;
                dtDataList.Rows[0]["LastDate"] = dtNow;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo(int ApplicantID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo Where ApplicantID=@ApplicantID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights(int ApplicantID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Lights Where ApplicantID=@ApplicantID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool UpdateTime2DiscountCode(int AdminID, int ApplicantID, int CodeID, string resp)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From DiscountCode Where AdminID=@AdminID and ApplicantID=@ApplicantID and DiscountID=@DiscountID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@DiscountID", CodeID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["UseDate"] = dtNow;
                dtDataList.Rows[0]["UseDateString"] = dtNow.ToString("yyyy-MM-dd");
                dtDataList.Rows[0]["CallbackLog"] = resp;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_da(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_da_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_h(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_h_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_wu(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_wu_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Fu(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Fu_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Jing(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Jing_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Luer(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Luer_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool UpdateCAPTCHACodeStatus(int adminID, int applicantID, int kind, string Year)
        {
            bool result = false;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = string.Empty;
            sql = "Select * from Temple_" + Year + "..CAPTCHACode Where ApplicantID = @ApplicantID and AdminID = @AdminID and Kind = @Kind";

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
                objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", adminID);
                objDatabaseAdapter.AddParameterToSelectCommand("@Kind", kind);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                if (dtGetData.Rows.Count > 0)
                {
                    BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                    DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);
                    int cid = 0;
                    if (int.TryParse(dtGetData.Rows[0]["CodeID"].ToString(), out cid))
                    {
                        if (objDatabaseHelper.UpdateCAPTCHACodeStatus(cid, Year))
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        public bool UpdateAppTagStatus(int adminID, int applicantID, int kind, string Year)
        {
            bool result = false;
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = string.Empty;
            string TempleString = string.Empty;

            switch (adminID)
            {
                case 3:
                    //大甲鎮瀾宮
                    TempleString = "da";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Purdue Where ApplicantID = @aid";
                            break;
                        case 13:
                            //七朝清醮
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_TaoistJiaoCeremony Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 4:
                    //新港奉天宮
                    TempleString = "h";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 5:
                    //商品販賣小舖
                    sql = "Select * from APPCharge_Product Where ApplicantID = @aid";
                    break;
                case 6:
                    TempleString = "wu";
                    //北港武德宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Purdue Where ApplicantID = @aid";
                            break;
                        case 4:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies Where ApplicantID = @aid";
                            break;
                        case 5:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 6:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 8:
                    TempleString = "Fu";
                    //西螺福興宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 9:
                    //景福宮
                    switch (kind)
                    {
                        case 1:
                            //sql = "Select * from APPCharge_Jing_Lights Where ApplicantID = @aid and APPTag = @id and AdminID = 3";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Jing_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 10:
                    TempleString = "Luer";
                    //台南正統鹿耳門聖母廟
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            TempleString = "Luer";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 14:
                    TempleString = "ty";
                    //桃園威天宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            TempleString = "ty";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Purdue Where ApplicantID = @aid";
                            break;
                        case 7:
                            //天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies Where ApplicantID = @aid";
                            break;
                        case 9:
                            //關聖帝君聖誕
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_EmperorGuansheng Where ApplicantID = @aid";
                            break;
                        case 14:
                            //九九重陽天赦日補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies2 Where ApplicantID = @aid";
                            break;
                        case 18:
                            //桃園威天宮天公生招財補運
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies3 Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 15:
                    TempleString = "Fw";
                    //斗六五路財神宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            TempleString = "Fw";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 16:
                    TempleString = "dh";
                    //台東東海龍門天聖宮
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            TempleString = "dh";
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Purdue Where ApplicantID = @aid";
                            break;
                        case 11:
                            //天貺納福添運法會
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 21:
                    //鹿港城隍廟
                    TempleString = "Lk";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            TempleString = "Lk";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 23:
                    //玉敕大樹朝天宮
                    TempleString = "ma";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Purdue Where ApplicantID = @aid";
                            break;
                        case 12:
                            //靈寶禮斗
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lingbaolidou Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 29:
                    //進寶財神廟
                    TempleString = "jb";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Lights Where ApplicantID = @aid";
                            break;
                        case 8:
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Supplies Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 31:
                    //台灣道教總廟無極三清總道院
                    TempleString = "wjsan";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_wjsan_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
                case 32:
                    //桃園龍德宮
                    TempleString = "ld";
                    switch (kind)
                    {
                        case 1:
                            sql = "Select * from Temple_" + Year + "..APPCharge_ld_Lights Where ApplicantID = @aid";
                            break;
                        case 2:
                            //sql = "Select * from Temple_" + Year + "..APPCharge_ld_Purdue Where ApplicantID = @aid";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
                objDatabaseAdapter.AddParameterToSelectCommand("@aid", applicantID);
                DataTable dtGetData = new DataTable();
                objDatabaseAdapter.Fill(dtGetData);

                if (dtGetData.Rows.Count > 0)
                {
                    BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                    DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);
                    int uid = 0;
                    int.TryParse(dtGetData.Rows[0]["UniqueID"].ToString(), out uid);

                    if (uid > 0 && TempleString != "")
                    {
                        if (objDatabaseHelper.UpdateAppChargeAppTag(uid, TempleString, dtGetData.Rows[0]["APPTag"].ToString(), Year))
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        public bool Updatecost2applicantinfo_Purdue_da(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_da_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_da(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_h(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_h_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_h(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_wu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wu_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_wu(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Fu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Fu_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_Fu(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Luer(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Luer_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_Luer(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_ty(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Fw(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Fw_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_Fw(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_dh(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_dh_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_dh(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_Lk(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Lk_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_Lk(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_ma(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ma_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_ma(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Purdue_mazu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_mazu_Purdue Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Purdue_mazu(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Product(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Product Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Product(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Moneymother(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Moneymother Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Moneymother(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_"+ Year + "..ApplicantInfo_Moneymother Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Moneymother(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Pilgrimage(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Pilgrimage Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_wu(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_wu_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_wu2(int ApplicantID, int AdminID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_wu_Supplies2 Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                dtDataList.Rows[0]["Status"] = 1;
                dtDataList.Rows[0]["Cost"] = Cost;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_EmperorGuansheng_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_EmperorGuansheng Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_EmperorGuansheng_ty(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lingbaolidou_ma(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ma_Lingbaolidou Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lingbaolidou_ma(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_TaoistJiaoCeremony_da(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_da_TaoistJiaoCeremony Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_TaoistJiaoCeremony_da(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lybc_dh(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_dh_Lybc Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lybc_dh(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_sx(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_sx_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_sx(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_Lk(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Lk_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_Lk(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_ty(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies2_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_Supplies2 Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies2_ty(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies3_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_Supplies3 Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies3_ty(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_dh(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_dh_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_dh(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_wu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wu_Supplies Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_wu(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_wu2(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wu_Supplies2 Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_wu2(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Supplies_wu3(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wu_Supplies3 Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Supplies_wu3(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updateapplicantinfo_Lights_da(int applicantID, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select * From Temple_" + Year + "..ApplicantInfo_da_Lights Where ApplicantID=@ApplicantID";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", applicantID);
            AdapterObj.Fill(dtDataList);

            if ((int)dtDataList.Rows[0]["Status"] == 1)
            {
                dtDataList.Rows[0]["Status"] = 2;
                AdapterObj.Update(dtDataList);

                bResult = true;
            }

            return bResult;
        }

        //public bool Updatecost2applicantinfo_Lights_Luer(int ApplicantID, int AdminID, int Cost)
        //{
        //    TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
        //    DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
        //    bool bResult = false;
        //    DataTable dtDataList = new DataTable();
        //    string sql = "Select Top 1 * From ApplicantInfo_Luer_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

        //    DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
        //    AdapterObj.SetSqlCommandBuilder();
        //    AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
        //    AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
        //    AdapterObj.Fill(dtDataList);

        //    if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
        //    {
        //        dtDataList.Rows[0]["Status"] = 1;
        //        dtDataList.Rows[0]["Cost"] = Cost;
        //        AdapterObj.Update(dtDataList);

        //        bResult = true;
        //    }


        //    return bResult;
        //}

        public bool Updatecost2applicantinfo_Lights_da(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_da_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_da(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_h(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_h_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_h(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_wu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wu_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_wu(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Fu(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Fu_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Fu(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Luer(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Luer_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Luer(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_ty(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_ty(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_ty_mom(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ty_mom_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_ty_mom(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Fw(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Fw_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Fw(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_dh(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_dh_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_dh(ApplicantID, Cost, 1, Year);
            }

            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Hs(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Hs_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Hs(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Jt(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Jt_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Jt(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Am(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Am_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Am(ApplicantID, Cost, 1, Year);
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_Lk(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Lk_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_Lk(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_ma(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ma_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_ma(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_jb(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_jb_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_jb(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_wjsan(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_wjsan_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_wjsan(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }

        public bool Updatecost2applicantinfo_Lights_ld(int ApplicantID, int AdminID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_ld_Lights Where ApplicantID=@ApplicantID and AdminID=@AdminID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                BCFBaseLibrary.Web.BasePage basePage = new BCFBaseLibrary.Web.BasePage();
                DatabaseHelper objDatabaseHelper = new DatabaseHelper(basePage);

                bResult = objDatabaseHelper.Updateapplicantinfo_Lights_ld(ApplicantID, Cost, 1, Year);
                //dtDataList.Rows[0]["Status"] = 1;
                //dtDataList.Rows[0]["Cost"] = Cost;
                //AdapterObj.Update(dtDataList);

                //bResult = true;
            }


            return bResult;
        }







        ///<summary>
        /// 取得農曆日期
        ///</summary>
        public static string GetLeapDate(DateTime Date, ref int Lm, ref string year, ref string month, ref string day, bool bShowYear = true, bool bShowMonth = true)
        {
            int L_ly, nLeapYear, nLeapMonth;

            ChineseLunisolarCalendar MyChineseLunisolarCalendar = new ChineseLunisolarCalendar();

            nLeapYear = MyChineseLunisolarCalendar.GetYear(Date);
            nLeapMonth = MyChineseLunisolarCalendar.GetMonth(Date);
            if (MyChineseLunisolarCalendar.IsLeapYear(nLeapYear)) //判斷此農曆年是否為閏年
            {
                L_ly = MyChineseLunisolarCalendar.GetLeapMonth(nLeapYear); //抓出此閏年閏何月

                if (nLeapMonth == L_ly)
                {
                    Lm = 1;
                }
                if (nLeapMonth >= L_ly)
                {
                    nLeapMonth--;
                }
            }
            else
            {
                nLeapMonth = MyChineseLunisolarCalendar.GetMonth(Date);
            }

            if (bShowYear)
            {
                year = (MyChineseLunisolarCalendar.GetYear(Date) - 1911).ToString();
                month = nLeapMonth.ToString();
                day = MyChineseLunisolarCalendar.GetDayOfMonth(Date).ToString();
                return "" + MyChineseLunisolarCalendar.GetYear(Date) + "/" + nLeapMonth + "/" + MyChineseLunisolarCalendar.GetDayOfMonth(Date);
            }
            else if (bShowMonth)
            {
                month = nLeapMonth.ToString();
                day = MyChineseLunisolarCalendar.GetDayOfMonth(Date).ToString();
                return "" + nLeapMonth + "/" + MyChineseLunisolarCalendar.GetDayOfMonth(Date);
            }
            else
            {
                return "" + MyChineseLunisolarCalendar.GetDayOfMonth(Date);
            }
        }

        ///<summary>
        /// 取得某一日期的該農曆月份的總天數
        ///</summary>
        public static string GetDaysInLeapMonth(DateTime date)
        {
            ChineseLunisolarCalendar MyChineseLunisolarCalendar = new ChineseLunisolarCalendar();

            return "" + MyChineseLunisolarCalendar.GetDaysInMonth(MyChineseLunisolarCalendar.GetYear(date), date.Month);
        }

        /// <summary>
        /// To the full taiwan date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ToFullTaiwanDate(DateTime datetime)
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("民國 {0} 年 {1} 月 {2} 日",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }

        /// <summary>
        /// To the simple taiwan date.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static string ToSimpleTaiwanDate(DateTime datetime)
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("{0}/{1}/{2}",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }

        /// <summary>
        /// 閏年的判斷 0-平年 1-閏年
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        int GetLeap(int year)
        {
            if (year % 400 == 0)
                return 1;
            else if (year % 100 == 0)
                return 0;
            else if (year % 4 == 0)
                return 1;
            else
                return 0;
        }
    }
}