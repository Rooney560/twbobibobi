using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MotoSystem.Data
{
    public class AdminDAC : SqlClientBase
    {
        protected bool result = false;
        public AdminDAC(BasePage basePage) : base(basePage)
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

        public bool UpdateAdmin(string AdminID, string Username, string Password, string Nickname, string Permission, int status)
        {
            int rs;

            rs = this.ExecuteSql("  update Admin set Username='" + Username  + "'  , Password='" + Password + "'  , Nickname=N'" + Nickname + "'  , Permission='" + Permission + "'  , Status='" + status + "'  where AdminID='" + AdminID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateMotoInfo(string MotoID, int BrandID, string Name, int DepartmentType, string Department, string LicenseNum, string Model, string CCNum, string Cost, int PromissoryNote, int Level)
        {
            int rs;

            rs = this.ExecuteSql("  update MotoInfo set Name=N'" + Name + "'  , DepartmentType='" + DepartmentType + "'  , Department=N'" + Department + "'  , LicenseNum='" + LicenseNum + "'  , Model='" + Model + "'  , CCNum='" + CCNum + "'  , Cost='" + Cost + "'  , PromissoryNote='" + PromissoryNote + "'  , BrandID='" + BrandID + "'  , Level='" + Level + "'  where MotoID='" + MotoID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateBrandInfo(string BrandID, string Name)
        {
            int rs;

            rs = this.ExecuteSql("  update BrandInfo set Name=N'" + Name + "'  where BrandID='" + BrandID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateDiscountInfo(string DiscountID, string Cost)
        {
            int rs;

            rs = this.ExecuteSql("  update DiscountInfo set Cost='" + Cost + "'  where DiscountID='" + DiscountID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateVendorInfo(string VendorID, int AdminID, string Name)
        {
            int rs;

            rs = this.ExecuteSql("  update VendorInfo set Name=N'" + Name + "', AdminID = '" + AdminID +"'  where VendorID='" + VendorID + "'");

            if(rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateBookkeepingInfo(int BookID, string AdminID, int MotoID, string CustomerID, string RentTime, string Money, string PayType, int Share, string ContractAddress)
        {
            int rs;

            rs = this.ExecuteSql("  update BookkeepingInfo set AdminID='" + AdminID + "'  , MotoID='" + MotoID + "'  , CustomerID='" + CustomerID + "'  , RentTime='" + RentTime + "'  , Money='" + Money + "'  , PayType='" + PayType + "'  , Share='" + Share + "'  , ContractAddress='" + ContractAddress + "'  where BookID='" + BookID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateCustomerInfo(string CustomerID, string Name, string MobileNum, string IDNum, string HomeNum, string Address, string Birthday, string Gender, string Permission, string EmergencyNum, string Relation, string Remark, string Email)
        {
            int rs;

            rs = this.ExecuteSql("  update Customer set Name=N'" + Name + "'  , MobileNum='" + MobileNum + "'  , Address=N'" + Address + "'  , IDNum='" + IDNum + "'  , HomeNum='" + HomeNum + "'  , Birthday='" + Birthday + "'  , Gender='" + Gender + "'  , Permission='" + Permission + "'  , EmergencyNum='" + EmergencyNum + "'  , Relation='" + Relation + "'  , Email='" + Email + "'  , Remark=N'" + Remark + "'  where CustomerID='" + CustomerID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdateReservation(string ReservationID, string StartDate, string EndDate)
        {
            int rs;

            rs = this.ExecuteSql("  update Reservation set StartDate='" + StartDate + "'  , EndDate='" + EndDate + "'  where ReservationID='" + ReservationID + "'");

            if (rs > 0)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 建立管理員資料
        /// <param name="UserName">UserName=帳號</param>
        /// <param name="NickName">NickName=暱稱</param>
        /// <param name="Permission">Permission=權限 0-超級管理員 1-廠商最高管理員 2-廠商一般管理員 3-打卡系統管理員</param>
        /// </summary>
        public int InsertAdmindata(string UserName, string Password, string NickName, string Permission)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            string sql = "Insert into Admin(UserName, Password, NickName, Permission, CreateDate) values(@UserName, @Password, @NickName, @Permission, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@UserName", UserName);
            Adapter.AddParameterToSelectCommand("@Password", Password);
            Adapter.AddParameterToSelectCommand("@NickName", NickName);
            Adapter.AddParameterToSelectCommand("@Permission", Permission);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立車籍資料
        /// <param name="BrandID">BrandID=廠牌編號</param>
        /// <param name="Name">Name=重機名稱</param>
        /// <param name="DepartmentType">DepartmentType=所屬店家類別: 0-大喵店家 1-合作店家</param>
        /// <param name="Department">Department=所屬店家 ex:大喵重機</param>
        /// <param name="LicenseNum">LicenseNum=牌照號碼</param>
        /// <param name="Model">Model=重車型號</param>
        /// <param name="CCNum">CCNum=CC數</param>
        /// <param name="Level">Level=權限 0-A級 1-B級 2-C級 3-D級 4-E級 5-特級</param>
        /// <param name="Cost">Cost=價錢</param>
        /// <param name="PromissoryNote">PromissoryNote=本票金額</param>
        /// </summary>
        public int InsertMotoInfo(int BrandID, string Name, int DepartmentType, string Department, string LicenseNum, string Model, string CCNum, int Level, string Cost, int PromissoryNote)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into MotoInfo(BrandID, Name, DepartmentType, Department, LicenseNum, Model, CCNum, Level, Cost, PromissoryNote, CreateDate) values(@BrandID, @Name, @DepartmentType, @Department, @LicenseNum, @Model, @CCNum, @Level, @Cost, @PromissoryNote, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@BrandID", BrandID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@DepartmentType", DepartmentType);
            Adapter.AddParameterToSelectCommand("@Department", Department);
            Adapter.AddParameterToSelectCommand("@LicenseNum", LicenseNum);
            Adapter.AddParameterToSelectCommand("@CCNum", CCNum);
            Adapter.AddParameterToSelectCommand("@Model", Model);
            Adapter.AddParameterToSelectCommand("@Level", Level);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@PromissoryNote", PromissoryNote);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立出車資料
        /// <param name="AdminID">AdminID=管理員編號</param>
        /// <param name="MotoID">MotoID=車籍編號</param>
        /// <param name="CustomerID">CustomerID=會員編號</param>
        /// <param name="RentTime">RentTime=租車時間</param>
        /// <param name="Money">Money=價錢</param>
        /// <param name="PayType">PayType=付款方式: 0-現金 1-刷卡</param>
        /// <param name="Share">Share=他店拆帳</param>
        /// <param name="ContractAddress">ContractAddress=合約書路徑</param>
        /// </summary>
        public int InsertBookkeepingInfo(string AdminID, int MotoID, string CustomerID, string RentTime, string Money, string PayType, int Share, string ContractAddress)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into BookkeepingInfo(AdminID, MotoID, CustomerID, RentTime, Money, PayType, Share, ContractAddress, CreateDate) values(@AdminID, @MotoID, @CustomerID, @RentTime, @Money, @PayType, @Share, @ContractAddress, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@MotoID", MotoID);
            Adapter.AddParameterToSelectCommand("@CustomerID", CustomerID);
            Adapter.AddParameterToSelectCommand("@RentTime", RentTime);
            Adapter.AddParameterToSelectCommand("@Money", Money);
            Adapter.AddParameterToSelectCommand("@PayType", PayType);
            Adapter.AddParameterToSelectCommand("@Share", Share);
            Adapter.AddParameterToSelectCommand("@ContractAddress", ContractAddress);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立會員資料
        /// </summary>
        public int InsertCustomerInfo(string Name, string MobileNum, string IDNum, string HomeNum, string Address, string Birthday, string Gender, string Permission, string EmergencyNum, string Relation, string Remark, string Email)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Customer(Name, MobileNum, IDNum, HomeNum, Address, Birthday, Gender, Permission, EmergencyNum, Relation, Remark, Email, CreateDate) values(@Name, @MobileNum, @IDNum, @HomeNum, @Address, @Birthday, @Gender, @Permission, @EmergencyNum, @Relation, @Remark, @Email, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@MobileNum", MobileNum);
            Adapter.AddParameterToSelectCommand("@IDNum", IDNum);
            Adapter.AddParameterToSelectCommand("@HomeNum", HomeNum);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Birthday", Birthday);
            Adapter.AddParameterToSelectCommand("@Gender", Gender);
            Adapter.AddParameterToSelectCommand("@Permission", Permission);
            Adapter.AddParameterToSelectCommand("@EmergencyNum", EmergencyNum);
            Adapter.AddParameterToSelectCommand("@Relation", Relation);
            Adapter.AddParameterToSelectCommand("@Remark", Remark);
            Adapter.AddParameterToSelectCommand("@Email", Email);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立廠商資料
        /// <param name="Name">Name=廠商名稱</param>
        /// </summary>
        public int InsertBrandInfo(string Name)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into BrandInfo(Name, CreateDate) values(@Name, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立預約資料
        /// </summary>
        /// <param name="MotoID">MotoID=車籍編號</param>
        /// <param name="StartDate">StartDate=開始日期</param>
        /// <param name="EndDate">EndDate=結束日期</param>
        /// <returns></returns>
        public int InsertReservation(string MotoID, string StartDate, string EndDate)
        {
            string sql = "Insert into Reservation(MotoID, StartDate, EndDate) values(@MotoID, @StartDate, @EndDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@MotoID", MotoID);
            Adapter.AddParameterToSelectCommand("@StartDate", StartDate);
            Adapter.AddParameterToSelectCommand("@EndDate", EndDate);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }
        /// <summary>
        /// 建立折扣資料
        /// <param name="Cost">Cost=折扣價錢</param>
        /// </summary>
        public int InsertDiscountInfo(string Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into DiscountInfo(Cost, CreateDate) values(@Cost, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立廠商資料
        /// <param name="AdminID">AdminID=管理員編號</param>
        /// <param name="Name">Name=廠商名稱</param>
        /// </summary>
        public int InsertVendorInfo(int AdminID, string Name)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into VendorInfo(AdminID, Name, CreateDate) values(@AdminID, @Name, @CreateDate)";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 確認帳號重複性
        /// <param name="UserName">UserName=帳號</param>
        /// </summary>
        public bool CheckedUserName(string UserName)
        {
            bool username = true;
            string sql = string.Empty;

            sql = "Select * from Admin Where UserName = @UserName";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@UserName", UserName);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                username = false;
            }
            return username;
        }

        /// <summary>
        /// 確認預約重複性
        /// </summary>
        /// <param name="ReservationID">預約編號</param>
        /// <returns></returns>
        public bool CheckedReservation(string ReservationID)
        {
            bool result = false;
            string sql = string.Empty;

            sql = "Select * from Reservation Where ReservationID = @ReservationID and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@ReservationID", ReservationID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            if (dtGetData.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 取得管理員資訊
        /// <param name="UserName">UserName=帳號</param>
        /// <param name="password">password=密碼</param>
        /// </summary>
        public DataTable GetAdminInfo(string UserName, string password)
        {
            string sql = string.Empty;

            sql = "Select * from Admin Where UserName = @UserName and Password = @Password and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@UserName", UserName);
            objDatabaseAdapter.AddParameterToSelectCommand("@Password", password);

            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得管理員資訊
        /// <param name="AdminID">AdminID=管理員編號</param>
        /// </summary>
        public DataTable GetAdminInfo(int AdminID)
        {
            string sql = string.Empty;

            sql = "Select * from Admin Where AdminID = @AdminID and Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);

            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得管理員列表
        /// <param name="permission">permission=權限</param>
        /// <param name="adminID">adminID=管理員編號</param>
        /// </summary>
        public DataTable GetAdminList(int PageIndex, int PageSize, int permission, int adminID)
        {
            string sql = string.Empty;

            sql = "Select * from Admin";
            if(permission != 0)
            {
                sql += " Where AdminID = @AdminID or UpperAdminID = @UpperAdminID";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if(permission != 0)
            {
                objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", adminID);
                objDatabaseAdapter.AddParameterToSelectCommand("@UpperAdminID", adminID);
            }
            DataSet dsGetData = new DataSet();
            objDatabaseAdapter.Fill(dsGetData, ((PageIndex - 1) * PageSize), PageSize, "dtGetData");
            DataTable dtGetData = dsGetData.Tables["dtGetData"];

            return dtGetData;
        }

        /// <summary>
        /// 取得管理員列表
        /// </summary>
        public DataTable GetAdminList(int permission)
        {
            string sql = string.Empty;

            sql = "Select * from Admin Where Permission = @Permission";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@Permission", permission);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得管理員列表
        /// </summary>
        public DataTable GetAdminList()
        {
            string sql = string.Empty;

            sql = "Select * from Admin Where Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得人員名稱列表
        /// </summary>
        public DataTable GetCustomerList(int Permission)
        {
            string sql = string.Empty;

            sql = "Select UserName from Customer";
            if (Permission > 0)
            {
                sql += " where Permission = @Permission";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            if (Permission > 0)
                objDatabaseAdapter.AddParameterToSelectCommand("@Permission", Permission);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得重機列表
        /// <param name="AdminID">AdminID=店家編號</param>
        /// </summary>
        public DataTable GetMotoList(int AdminID)
        {
            string sql = "Select * From MotoInfo Where Status = 0 and AdminID = @AdminID Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得重機列表
        /// </summary>
        public DataTable GetMotoList()
        {
            string sql = "Select * From view_MotoInfo Where Status = 0 Order By Level";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得重機資訊
        /// <param name="MotoID">MotoID=重機編號</param>
        /// </summary>
        public DataTable GetMotoInfo()
        {
            string sql = "Select * from view_MotoInfo Where Status = 0 Order by CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }
        /// <summary>
        /// 取得重機資訊
        /// <param name="MotoID">MotoID=重機編號</param>
        /// </summary>
        public DataTable GetMotoInfo(int MotoID)
        {
            string sql = "Select * from view_MotoInfo Where Status = 0 and MotoID = @MotoID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@MotoID", MotoID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得廠牌列表
        /// <param name="AdminID">AdminID=店家編號</param>
        /// </summary>
        public DataTable GetBrandList(int AdminID)
        {
            string sql = "Select * From BrandInfo Where Status = 0 and AdminID = @AdminID Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得廠牌列表
        /// </summary>
        public DataTable GetBrandList()
        {
            string sql = "Select * From BrandInfo Where Status = 0 Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得折扣列表
        /// <param name="AdminID">AdminID=店家編號</param>
        /// </summary>
        public DataTable GetDiscountList(int AdminID)
        {
            string sql = "Select * From DiscountInfo Where Status = 0 and AdminID = @AdminID Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@AdminID", AdminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得折扣列表
        /// </summary>
        public DataTable GetDiscountList()
        {
            string sql = "Select * From DiscountInfo Where Status = 0 Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得廠商列表
        /// <param name="AdminID">AdminID=店家編號</param>
        /// <param name="Permission">Permission=管理員權限</param>
        /// </summary>
        public DataTable GetVendorList(int AdminID, int Permission)
        {
            string sql = "Select * From view_VendorInfo Where Status = 0";
            if (Permission != 0)
            {
                sql += " and AdminID = '" + AdminID + "'";
            }
            sql += " Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得宮廟內容
        /// </summary>
        /// <param name="AdminID"></param>
        /// <returns></returns>
        public DataTable GetTempleInfo(string AdminID)
        {
            DataTable dtData = new DataTable();
            string sql = "Select * From view_TempleInfo Where AdminID = @AdminID and Status = 0";
            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtData);
            if (dtData.Rows.Count > 0)
            {
                dtData.Columns.Add("Content1", typeof(string));
                if (dtData.Rows[0]["Content"] != DBNull.Value)
                {
                    string content;
                    content = System.Text.UnicodeEncoding.Unicode.GetString((byte[])dtData.Rows[0]["Content"]);
                    content = System.Text.RegularExpressions.Regex.Replace(content, "<table", "<table style=\"max-width:100%; height:auto\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    string valEx = @"width=""\d+""";
                    content = System.Text.RegularExpressions.Regex.Replace(content, valEx, "width=\"640px\" height=\"auto\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    dtData.Rows[0]["Content1"] = content;
                }
            }
            return dtData;
        }

        /// <summary>
        /// 取得廠商列表
        /// </summary>
        public DataTable GetVendorList()
        {
            string sql = "Select * From view_VendorInfo Where Status = 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }
        
        /// <summary>
        /// 取得出車列表
        /// </summary>
        public DataTable GetBookList(string StartDate, string EndDate, string MobileNum, string MotoID, string Department, ref DataSet dsPages)
        {
            string sql = "Select * From view_BookkeepingInfo Where Status = 0";
            if (MobileNum != "")
            {
                sql += " and MobileNum ='" + MobileNum + "'";
            }
            if (MotoID != "")
            {
                sql += " and MotoID ='" + MotoID + "'";
            }
            if (Department != "")
            {
                sql += " and Department ='" + Department + "'";
            }
            if (StartDate != "" && EndDate != "")
            {
                sql += " and CreateDate between '" + StartDate + "' and '" + EndDate + "'";
            }
            else if (StartDate != "")
            {
                sql += " and CreateDate >= '" + StartDate + "'";
            }
            else if (EndDate != "")
            {
                sql += " and CreateDate <= '" + EndDate + "'";
            }

            sql += " Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dsPages, "Pages");
            dtGetData = dsPages.Tables["Pages"];

            return dtGetData;
        }

        /// <summary>
        /// 取得出車列表
        /// </summary>
        public DataTable GetBookList(string StartDate, string EndDate, string MobileNum, string MotoID, string Department, int AdminID, ref DataSet dsPages)
        {
            string sql = "Select * From view_BookkeepingInfo Where Status = 0";
            if(AdminID != 0)
            {
                sql += " and AdminID = '" + AdminID + "'";
            }
            if (MobileNum != "")
            {
                sql += " and MobileNum ='" + MobileNum + "'";
            }
            if (MotoID != "")
            {
                sql += " and MotoID ='" + MotoID + "'";
            }
            if (Department != "")
            {
                sql += " and Department ='" + Department + "'";
            }
            if (StartDate != "" && EndDate != "")
            {
                sql += " and CreateDate between '" + StartDate + "' and '" + EndDate + "'";
            }
            else if (StartDate != "")
            {
                sql += " and CreateDate >= '" + StartDate + "'";
            }
            else if (EndDate != "")
            {
                sql += " and CreateDate <= '" + EndDate + "'";
            }

            sql += " Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dsPages, "Pages");
            dtGetData = dsPages.Tables["Pages"];

            return dtGetData;
        }

        /// <summary>
        /// 取得出車資訊
        /// <param name="BookID">BookID=出車編號</param>
        /// </summary>
        public DataTable GetBookInfo(int BookID)
        {
            string sql = "Select * from view_BookkeepingInfo Where Status = 0 and BookID = @BookID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@BookID", BookID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得預約資訊
        /// <param name="MotoID">MotoID=車籍編號</param>
        /// <param name="Date">Date=日期</param>
        /// </summary>
        public DataTable GetReservationInfo(int MotoID, string Date)
        {
            DateTime StartDate = DateTime.Parse(Date);
            DateTime EndDate = StartDate.AddDays(1);
            string StartDate_half = StartDate.ToString("yyyy-MM-dd 12:00:00");
            string sql = "Select * from Reservation Where MotoID = @MotoID and Status = 0 and ((StartDate BETWEEN @StartDate and @EndDate or @StartDate_half BETWEEN StartDate and EndDate) or (EndDate BETWEEN @StartDate_half and @EndDate))";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@MotoID", MotoID);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate", StartDate);
            objDatabaseAdapter.AddParameterToSelectCommand("@EndDate", EndDate);
            objDatabaseAdapter.AddParameterToSelectCommand("@StartDate_half", StartDate_half);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }
        ///// <summary>
        ///// 取得出車資訊
        ///// <param name="MotoID">MotoID=車籍編號</param>
        ///// <param name="Date">Date=日期</param>
        ///// </summary>
        //public DataTable GetBookInfo(int MotoID, DateTime Date)
        //{
        //    string sql = "Select * from view_BookkeepingInfo Where Status = 0 and MotoID = @MotoID and EndDate > @Date";

        //    DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
        //    objDatabaseAdapter.AddParameterToSelectCommand("@MotoID", MotoID);
        //    objDatabaseAdapter.AddParameterToSelectCommand("@Date", Date);
        //    DataTable dtGetData = new DataTable();
        //    objDatabaseAdapter.Fill(dtGetData);

        //    return dtGetData;
        //}

        /// <summary>
        /// 取得會員列表
        /// </summary>
        public DataTable GetCustomerList()
        {
            string sql = "Select * From Customer Where Status = 0 Order By CreateDate Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得會員資訊
        /// <param name="CustomerID">CustomerID=會員編號</param>
        /// </summary>
        public DataTable GetCustomerInfo(int CustomerID)
        {
            string sql = "Select * from Customer Where Status = 0 and CustomerID = @CustomerID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@CustomerID", CustomerID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得會員資訊
        /// <param name="MobileNum">MobileNum=電話號碼</param>
        /// </summary>
        public DataTable GetCustomerInfo(string MobileNum)
        {
            string sql = "Select * from Customer Where Status = 0 and MobileNum = @MobileNum";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("@MobileNum", MobileNum);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得會員資訊
        /// <param name="MobileNum">MobileNum=電話號碼</param>
        /// <param name="Name">Name=姓名</param>
        /// </summary>
        public DataTable GetCustomerInfo(string MobileNum, string Name, ref DataSet dsPages)
        {
            string sql = "Select * from Customer Where Status = 0";
            
            if (MobileNum != "")
            {
                sql += " and MobileNum ='" + MobileNum + "'";
            }
            if (Name != "")
            {
                sql += " and Name = N'" + Name + "'";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dsPages, "Pages");
            dtGetData = dsPages.Tables["Pages"];

            return dtGetData;
        }

        /// <summary>
        /// 刪除管理者
        /// <param name="AdminID">AdminID=管理員編號</param>
        /// </summary>
        public void DeleteAdmin(int AdminID)
        {
            string sql = "Select * From Admin Where AdminID = @AdminID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除車籍資料
        /// <param name="MotoID">MotoID=車籍編號</param>
        /// </summary>
        public void DeleteMotoData(int MotoID)
        {
            string sql = "Select * From MotoInfo Where MotoID = @MotoID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@MotoID", MotoID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除廠牌資料
        /// <param name="BrandID">BrandID=廠牌編號</param>
        /// </summary>
        public void DeleteBrandData(int BrandID)
        {
            string sql = "Select * From BrandInfo Where BrandID = @BrandID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@BrandID", BrandID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除折扣資料
        /// <param name="DiscountID">DiscountID=廠牌編號</param>
        /// </summary>
        public void DeleteDiscountData(int DiscountID)
        {
            string sql = "Select * From DiscountInfo Where DiscountID = @DiscountID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@DiscountID", DiscountID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除廠商資料
        /// <param name="VendorID">VendorID=廠商編號</param>
        /// </summary>
        public void DeleteVendorData(int VendorID)
        {
            string sql = "Select * From VendorInfo Where VendorID = @VendorID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@VendorID", VendorID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除出車資料
        /// <param name="BookID">BookID=出車編號</param>
        /// </summary>
        public void DeleteBookData(int BookID)
        {
            string sql = "Select * From BookkeepingInfo Where BookID = @BookID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@BookID", BookID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除會員資料
        /// <param name="CustomerID">CustomerID=會員編號</param>
        /// </summary>
        public void DeleteCustomerData(int CustomerID)
        {
            string sql = "Select * From Customer Where CustomerID = @CustomerID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@CustomerID", CustomerID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
            }
        }

        /// <summary>
        /// 刪除預約資料
        /// <param name="ReservationID">ReservationID=預約編號</param>
        /// </summary>
        public bool DeleteReservation(string ReservationID)
        {
            bool result = false;
            string sql = "Select * From Reservation Where ReservationID = @ReservationID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ReservationID", ReservationID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                dtUpdateStatus.Rows[0]["Status"] = -1;
                Adapter.Update(dtUpdateStatus);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <param name="Status"></param>
        /// <param name="AdminID"></param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool Updatestatus2appcharge_test(int UniqueID, int Status, string AdminID, string kind)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            string view = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_TEST..APPCharge_da_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_da_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_TEST..APPCharge_h_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_h_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_TEST..APPCharge_wu_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_wu_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_TEST..APPCharge_Fu_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_Fu_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_TEST..APPCharge_Luer_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_Luer_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_TEST..APPCharge_ty_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_ty_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_TEST..APPCharge_Fw_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_Fw_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_TEST..APPCharge_dh_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_dh_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_TEST..APPCharge_Lk_Lights";
                            sql = "Select * from Temple_TEST..APPCharge_Lk_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_TEST..APPCharge_da_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_da_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_TEST..APPCharge_h_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_h_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_TEST..APPCharge_wu_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_wu_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_TEST..APPCharge_Fu_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_Fu_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "9":
                            //桃園大廟景福宮
                            view = "Temple_TEST..APPCharge_Jing_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_Jing_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_TEST..APPCharge_Luer_Purdue";
                            sql = "Select * from Temple_TEST..APPCharge_Luer_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    switch (AdminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            view = "Temple_TEST..APPCharge_Product";
                            sql = "Select * from Temple_TEST..APPCharge_Product Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "7":
                            //大甲鎮瀾宮繞境商品小舖
                            view = "Temple_TEST..APPCharge_Pilgrimage";
                            sql = "Select * from Temple_TEST..APPCharge_Pilgrimage Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "11":
                            //新港奉天宮錢母商品小舖
                            view = "Temple_TEST..APPCharge_Moneymother";
                            sql = "Select * from Temple_TEST..APPCharge_Moneymother Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
                AdapterObj.SetSqlCommandBuilder();
                AdapterObj.AddParameterToSelectCommand("@UniqueID", UniqueID);
                AdapterObj.Fill(dtDataList);

                if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 1)
                {
                    //dtDataList.Rows[0]["Status"] = Status;
                    //AdapterObj.Update(dtDataList);
                    if (view != "")
                    {
                        int res = ExecuteSql("Update " + view + " Set Status = " + Status + " Where UniqueID=" + UniqueID);

                        if (res > 0)
                        {
                            if (Updatestatus2applicantinfo_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), Status, AdminID, kind))
                            {
                                switch (kind)
                                {
                                    case "1":
                                        //點燈
                                        if (DeleteLightsinfo_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "2":
                                        //普度
                                        if (DeletePurdueNum_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "3":
                                        //商品小舖
                                        if (DeleteProductNum_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }

            }

            return bResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicantID"></param>
        /// <param name="Status"></param>
        /// <param name="AdminID"></param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool Updatestatus2applicantinfo_test(int ApplicantID, int Status, string AdminID, string kind)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            string view = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_TEST..ApplicantInfo_da_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_da_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_TEST..ApplicantInfo_h_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_h_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_TEST..ApplicantInfo_wu_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_wu_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_TEST..ApplicantInfo_Fu_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Fu_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_TEST..ApplicantInfo_Luer_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Luer_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_TEST..ApplicantInfo_ty_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_ty_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_TEST..ApplicantInfo_Fw_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Fw_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_TEST..ApplicantInfo_dh_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_dh_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_TEST..ApplicantInfo_Lk_Lights";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Lk_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_TEST..ApplicantInfo_da_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_da_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_TEST..ApplicantInfo_h_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_h_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_TEST..ApplicantInfo_wu_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_wu_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_TEST..ApplicantInfo_Fu_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Fu_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "9":
                            //桃園大廟景福宮
                            view = "Temple_TEST..ApplicantInfo_Jing_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Jing_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_TEST..ApplicantInfo_Luer_Purdue";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Luer_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    switch (AdminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            view = "Temple_TEST..ApplicantInfo_Product";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Product Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "7":
                            //大甲鎮瀾宮繞境商品小舖
                            view = "Temple_TEST..ApplicantInfo_Pilgrimage";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Pilgrimage Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "11":
                            //新港奉天宮錢母商品小舖
                            view = "Temple_TEST..ApplicantInfo_Moneymother";
                            sql = "Select * from Temple_TEST..ApplicantInfo_Moneymother Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
                AdapterObj.SetSqlCommandBuilder();
                AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                AdapterObj.Fill(dtDataList);

                if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 2)
                {
                    //dtDataList.Rows[0]["Status"] = Status;
                    //dtDataList.Rows[0]["UpdateinfoDate"] = dt;
                    //dtDataList.Rows[0]["UpdateinfoDateString"] = dt.ToString("yyyy-MM-dd");
                    //AdapterObj.Update(dtDataList);
                    if (view != "")
                    {
                        int res = ExecuteSql("Update " + view + " Set Status = " + Status + ", UpdateinfoDate = '" + dt + "', UpdateinfoDateString = '" + dt.ToString("yyyy-MM-dd") + "' Where ApplicantID=" + ApplicantID);

                        if (res > 0)
                        {
                            bResult = true;
                        }
                    }
                }
            }

            return bResult;
        }

        /// <summary>
        /// 刪除點燈訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteLightsinfo_test(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    view = "Temple_TEST..Lights_da_info";
                    sql = "Select * from Temple_TEST..Lights_da_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "4":
                    //新港奉天宮
                    view = "Temple_TEST..Lights_h_info";
                    sql = "Select * from Temple_TEST..Lights_h_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "6":
                    //北港武德宮
                    view = "Temple_TEST..Lights_wu_info";
                    sql = "Select * from Temple_TEST..Lights_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "8":
                    //西螺福興宮
                    view = "Temple_TEST..Lights_Fu_info";
                    sql = "Select * from Temple_TEST..Lights_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "10":
                    //台南正統鹿耳門聖母廟
                    view = "Temple_TEST..Lights_Luer_info";
                    sql = "Select * from Temple_TEST..Lights_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "14":
                    //桃園威天宮
                    view = "Temple_TEST..Lights_ty_info";
                    sql = "Select * from Temple_TEST..Lights_ty_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "15":
                    //斗六五路財神宮
                    view = "Temple_TEST..Lights_Fw_info";
                    sql = "Select * from Temple_TEST..Lights_Fw_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "16":
                    //台東東海龍門天聖宮
                    view = "Temple_TEST..Lights_dh_info";
                    sql = "Select * from Temple_TEST..Lights_dh_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int LightsID = int.Parse(dtUpdateStatus.Rows[i]["LightsID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);
                        int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where LightsID=" + LightsID);

                        if (res > 0)
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除普度訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 8-西螺福興宮 9-桃園大廟景福宮 10-台南正統鹿耳門聖母廟</param>
        /// </summary>
        public bool DeletePurdueNum_test(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    sql = "Select * from Purdue_da_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "4":
                    //新港奉天宮
                    sql = "Select * from Purdue_h_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "5":
                    //文創商品-新港奉天宮
                    sql = "Select * from ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "6":
                    //北港武德宮
                    sql = "Select * from Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "8":
                    //西螺福興宮
                    sql = "Select * from Purdue_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "9":
                    //桃園大廟景福宮
                    sql = "Select * from Purdue_Jing_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "10":
                    //台南正統鹿耳門聖母廟
                    sql = "Select * from Purdue_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        dtUpdateStatus.Rows[i]["Num2String"] = "";
                        dtUpdateStatus.Rows[i]["Num"] = 0;
                        Adapter.Update(dtUpdateStatus);
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除商品小舖訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeleteProductNum_test(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = string.Empty;

            switch (AdminID)
            {
                case "5":
                    //文創商品-新港奉天宮
                    sql = "Select * from ApplicantInfo_ProductInfo Where Status = -2 and ApplicantID = @ApplicantID";
                    break;
                case "7":
                    //繞境商品小舖
                    sql = "Select * from ApplicantInfo_Pilgrimage Where Status = -2 and ApplicantID = @ApplicantID";
                    break;
                case "11":
                    //錢母商品小舖
                    sql = "Select * from ApplicantInfo_Moneymother Where Status = -2 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        dtUpdateStatus.Rows[i]["Num2String"] = "";
                        dtUpdateStatus.Rows[i]["Num"] = 0;
                        Adapter.Update(dtUpdateStatus);
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <param name="Status"></param>
        /// <param name="AdminID"></param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool Updatestatus2appcharge(int UniqueID, int Status, string AdminID, string kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            string view = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..APPCharge_da_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_" + Year + "..APPCharge_h_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..APPCharge_wu_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_" + Year + "..APPCharge_Fu_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_" + Year + "..APPCharge_Luer_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..APPCharge_ty_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_" + Year + "..APPCharge_Fw_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_" + Year + "..APPCharge_dh_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_" + Year + "..APPCharge_Lk_Lights";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Lights Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..APPCharge_da_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_" + Year + "..APPCharge_h_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_h_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..APPCharge_wu_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_" + Year + "..APPCharge_Fu_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fu_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "9":
                            //桃園大廟景福宮
                            view = "Temple_" + Year + "..APPCharge_Jing_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Jing_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_" + Year + "..APPCharge_Luer_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Luer_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..APPCharge_ty_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_" + Year + "..APPCharge_Fw_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Fw_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_" + Year + "..APPCharge_dh_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_dh_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_" + Year + "..APPCharge_Lk_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Lk_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            view = "Temple_" + Year + "..APPCharge_ma_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "30":
                            //鎮瀾買足
                            view = "Temple_" + Year + "..APPCharge_mazu_Purdue";
                            sql = "Select * from Temple_" + Year + "..APPCharge_mazu_Purdue Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    Year = dt.Year.ToString();
                    switch (AdminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            view = "Temple_" + Year + "..APPCharge_Product";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Product Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "7":
                            //大甲鎮瀾宮繞境商品小舖
                            view = "Temple_" + Year + "..APPCharge_Pilgrimage";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Pilgrimage Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "11":
                            //新港奉天宮錢母商品小舖
                            view = "Temple_" + Year + "..APPCharge_Moneymother";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Moneymother Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "20":
                            //文創商品-西螺福興宮
                            view = "Temple_" + Year + "..APPCharge_Product";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Product Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "22":
                            //流金富貴商品小舖
                            view = "Temple_" + Year + "..APPCharge_Product";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Product Where Status = 1 and UniqueID = @UniqueID";
                            break;
                        case "28":
                            //財神小舖商品小舖
                            view = "Temple_" + Year + "..APPCharge_Product";
                            sql = "Select * from Temple_" + Year + "..APPCharge_Product Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "4":
                    //下元補庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..APPCharge_wu_Supplies";
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "5":
                    //呈疏補庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..APPCharge_wu_Supplies2";
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies2 Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "6":
                    //企業補財庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..APPCharge_wu_Supplies3";
                            sql = "Select * from Temple_" + Year + "..APPCharge_wu_Supplies3 Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "7":
                    //天赦日補運
                    switch (AdminID)
                    {
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..APPCharge_ty_Supplies";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ty_Supplies Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "8":
                    //天赦日祭改
                    switch (AdminID)
                    {
                        case "29":
                            //進寶財神廟
                            view = "Temple_" + Year + "..APPCharge_jb_Supplies";
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_Supplies Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "10":
                    //代燒金紙
                    switch (AdminID)
                    {
                        case "29":
                            //進寶財神廟
                            view = "Temple_" + Year + "..APPCharge_jb_BPO";
                            sql = "Select * from Temple_" + Year + "..APPCharge_jb_BPO Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "12":
                    //靈寶禮斗
                    switch (AdminID)
                    {
                        case "23":
                            //桃園威天宮
                            view = "Temple_" + Year + "..APPCharge_ma_Lingbaolidou";
                            sql = "Select * from Temple_" + Year + "..APPCharge_ma_Lingbaolidou Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
                case "13":
                    //七朝清醮
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..APPCharge_da_TaoistJiaoCeremony";
                            sql = "Select * from Temple_" + Year + "..APPCharge_da_TaoistJiaoCeremony Where Status = 1 and UniqueID = @UniqueID";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
                AdapterObj.SetSqlCommandBuilder();
                AdapterObj.AddParameterToSelectCommand("@UniqueID", UniqueID);
                AdapterObj.Fill(dtDataList);

                if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 1)
                {
                    //dtDataList.Rows[0]["Status"] = Status;
                    //AdapterObj.Update(dtDataList);
                    if (view != "")
                    {
                        int res = ExecuteSql("Update " + view + " Set Status = " + Status + " Where UniqueID=" + UniqueID);

                        if (res > 0)
                        {
                            if (Updatestatus2applicantinfo(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), Status, AdminID, kind, Year))
                            {
                                switch (kind)
                                {
                                    case "1":
                                        //點燈
                                        if (DeleteLightsinfo(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "2":
                                        //普度
                                        if (DeletePurdueinfo(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "3":
                                        //商品小舖
                                        if (DeleteProductInfo(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "4":
                                        //下元補庫
                                        if (DeleteSuppliesNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, 1, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "5":
                                        //呈疏補庫
                                        if (DeleteSuppliesNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, 2, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "6":
                                        //企業補財庫
                                        if (DeleteSuppliesNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, 3, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "7":
                                        //天赦日補運
                                        if (DeleteSuppliesNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, 4, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "8":
                                        //天赦日祭改
                                        if (DeleteSuppliesNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, 5, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "10":
                                        //代燒金紙
                                        if (DeleteBPONum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "12":
                                        //靈寶禮斗
                                        if (DeleteLingbaolidouNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                    case "13":
                                        //七朝清醮
                                        if (DeleteTaoistJiaoCeremonyNum(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID, Year))
                                        {
                                            bResult = true;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }


            return bResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicantID"></param>
        /// <param name="Status"></param>
        /// <param name="AdminID"></param>
        /// <param name="kind">kind=活動名稱: 1-點燈 2-普度 4-下元補庫 5-呈疏補庫 6-企業補財庫 7-天赦日補運 8-天赦日祭改 9-關聖帝君聖誕 10-代燒金紙 11-天貺納福添運法會 12-靈寶禮斗</param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public bool Updatestatus2applicantinfo(int ApplicantID, int Status, string AdminID, string kind, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            string view = string.Empty;

            switch (kind)
            {
                case "1":
                    //點燈
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..ApplicantInfo_da_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_" + Year + "..ApplicantInfo_h_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_h_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..ApplicantInfo_wu_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_" + Year + "..ApplicantInfo_Fu_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fu_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_" + Year + "..ApplicantInfo_Luer_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Luer_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..ApplicantInfo_ty_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_" + Year + "..ApplicantInfo_Fw_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fw_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_" + Year + "..ApplicantInfo_dh_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "17":
                            //五股賀聖宮
                            view = "Temple_" + Year + "..ApplicantInfo_Hs_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Hs_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "18":
                            //外澳接天宮
                            view = "Temple_" + Year + "..ApplicantInfo_Jt_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Jt_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "19":
                            //安平開台天后宮
                            view = "Temple_" + Year + "..ApplicantInfo_Am_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Am_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_" + Year + "..ApplicantInfo_Lk_Lights";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Lk_Lights Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "2":
                    //普度
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..ApplicantInfo_da_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "4":
                            //新港奉天宮
                            view = "Temple_" + Year + "..ApplicantInfo_h_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_h_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..ApplicantInfo_wu_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "8":
                            //西螺福興宮
                            view = "Temple_" + Year + "..ApplicantInfo_Fu_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fu_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "9":
                            //桃園大廟景福宮
                            view = "Temple_" + Year + "..ApplicantInfo_Jing_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Jing_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "10":
                            //台南正統鹿耳門聖母廟
                            view = "Temple_" + Year + "..ApplicantInfo_Luer_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Luer_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..ApplicantInfo_ty_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "15":
                            //斗六五路財神宮
                            view = "Temple_" + Year + "..ApplicantInfo_Fw_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Fw_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "16":
                            //台東東海龍門天聖宮
                            view = "Temple_" + Year + "..ApplicantInfo_dh_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_dh_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "18":
                            //外澳接天宮
                            view = "Temple_" + Year + "..ApplicantInfo_Jt_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Jt_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "21":
                            //鹿港城隍廟
                            view = "Temple_" + Year + "..ApplicantInfo_Lk_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Lk_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "23":
                            //玉敕大樹朝天宮
                            view = "Temple_" + Year + "..ApplicantInfo_ma_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ma_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "30":
                            //鎮瀾買足
                            view = "Temple_" + Year + "..ApplicantInfo_mazu_Purdue";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_mazu_Purdue Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "3":
                    //商品小舖
                    switch (AdminID)
                    {
                        case "5":
                            //文創商品-新港奉天宮
                            view = "Temple_" + Year + "..ApplicantInfo_Product";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "7":
                            //大甲鎮瀾宮繞境商品小舖
                            view = "Temple_" + Year + "..ApplicantInfo_Pilgrimage";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Pilgrimage Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "11":
                            //新港奉天宮錢母商品小舖
                            view = "Temple_" + Year + "..ApplicantInfo_Moneymother";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Moneymother Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "20":
                            //文創商品-西螺福興宮
                            view = "Temple_" + Year + "..ApplicantInfo_Product";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "22":
                            //流金富貴商品小舖
                            view = "Temple_" + Year + "..ApplicantInfo_Product";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                        case "28":
                            //財神小舖商品小舖
                            view = "Temple_" + Year + "..ApplicantInfo_Product";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "4":
                    //下元補庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..ApplicantInfo_wu_Supplies";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "5":
                    //呈疏補庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..ApplicantInfo_wu_Supplies2";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies2 Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "6":
                    //企業補財庫
                    switch (AdminID)
                    {
                        case "6":
                            //北港武德宮
                            view = "Temple_" + Year + "..ApplicantInfo_wu_Supplies3";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_wu_Supplies3 Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "7":
                    //天赦日補運
                    switch (AdminID)
                    {
                        case "14":
                            //桃園威天宮
                            view = "Temple_" + Year + "..ApplicantInfo_ty_Supplies";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ty_Supplies Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "8":
                    //天赦日祭改
                    switch (AdminID)
                    {
                        case "29":
                            //進寶財神廟
                            view = "Temple_" + Year + "..ApplicantInfo_jb_Supplies";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_jb_Supplies Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "10":
                    //代燒金紙
                    switch (AdminID)
                    {
                        case "29":
                            //進寶財神廟
                            view = "Temple_" + Year + "..ApplicantInfo_jb_BPO";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_jb_BPO Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "12":
                    //靈寶禮斗
                    switch (AdminID)
                    {
                        case "23":
                            //玉敕大樹朝天宮
                            view = "Temple_" + Year + "..ApplicantInfo_ma_Lingbaolidou";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_ma_Lingbaolidou Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
                case "13":
                    //七朝清醮
                    switch (AdminID)
                    {
                        case "3":
                            //大甲鎮瀾宮
                            view = "Temple_" + Year + "..ApplicantInfo_da_TaoistJiaoCeremony";
                            sql = "Select * from Temple_" + Year + "..ApplicantInfo_da_TaoistJiaoCeremony Where Status = 2 and ApplicantID = @ApplicantID";
                            break;
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
                AdapterObj.SetSqlCommandBuilder();
                AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                AdapterObj.Fill(dtDataList);

                if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 2)
                {
                    //dtDataList.Rows[0]["Status"] = Status;
                    //dtDataList.Rows[0]["UpdateinfoDate"] = dt;
                    //dtDataList.Rows[0]["UpdateinfoDateString"] = dt.ToString("yyyy-MM-dd");
                    //AdapterObj.Update(dtDataList);
                    if (view != "")
                    {
                        int res = ExecuteSql("Update " + view + " Set Status = " + Status + ", UpdateinfoDate = '" + dt + "', UpdateinfoDateString = '" + dt.ToString("yyyy-MM-dd") + "' Where ApplicantID=" + ApplicantID);

                        if (res > 0)
                        {
                            bResult = true;
                        }
                    }
                }
            }

            return bResult;
        }

        /// <summary>
        /// 刪除點燈訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteLightsinfo(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    view = "Temple_" + Year + "..Lights_da_info";
                    sql = "Select * from Temple_" + Year + "..Lights_da_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "4":
                    //新港奉天宮
                    view = "Temple_" + Year + "..Lights_h_info";
                    sql = "Select * from Temple_" + Year + "..Lights_h_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "6":
                    //北港武德宮
                    view = "Temple_" + Year + "..Lights_wu_info";
                    sql = "Select * from Temple_" + Year + "..Lights_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "8":
                    //西螺福興宮
                    view = "Temple_" + Year + "..Lights_Fu_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "10":
                    //台南正統鹿耳門聖母廟
                    view = "Temple_" + Year + "..Lights_Luer_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "14":
                    //桃園威天宮
                    view = "Temple_" + Year + "..Lights_ty_info";
                    sql = "Select * from Temple_" + Year + "..Lights_ty_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "15":
                    //斗六五路財神宮
                    view = "Temple_" + Year + "..Lights_Fw_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Fw_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "16":
                    //台東東海龍門天聖宮
                    view = "Temple_" + Year + "..Lights_dh_info";
                    sql = "Select * from Temple_" + Year + "..Lights_dh_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "17":
                    //五股賀聖宮
                    view = "Temple_" + Year + "..Lights_Hs_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Hs_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "18":
                    //外澳接天宮
                    view = "Temple_" + Year + "..Lights_Jt_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Jt_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "19":
                    //安平開台天后宮
                    view = "Temple_" + Year + "..Lights_Am_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Am_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "21":
                    //鹿港城隍廟
                    view = "Temple_" + Year + "..Lights_Lk_info";
                    sql = "Select * from Temple_" + Year + "..Lights_Lk_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int LightsID = int.Parse(dtUpdateStatus.Rows[i]["LightsID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);

                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where LightsID=" + LightsID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除普度訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeletePurdueinfo(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    view = "Temple_" + Year + "..Purdue_da_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_da_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "4":
                    //新港奉天宮
                    view = "Temple_" + Year + "..Purdue_h_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_h_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "6":
                    //北港武德宮
                    view = "Temple_" + Year + "..Purdue_wu_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "8":
                    //西螺福興宮
                    view = "Temple_" + Year + "..Purdue_Fu_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "10":
                    //台南正統鹿耳門聖母廟
                    view = "Temple_" + Year + "..Purdue_Luer_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "14":
                    //桃園威天宮
                    view = "Temple_" + Year + "..Purdue_ty_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_ty_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "15":
                    //斗六五路財神宮
                    view = "Temple_" + Year + "..Purdue_Fw_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_Fw_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "16":
                    //台東東海龍門天聖宮
                    view = "Temple_" + Year + "..Purdue_dh_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_dh_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "18":
                    //外澳接天宮
                    view = "Temple_" + Year + "..Purdue_Jt_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_Jt_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "21":
                    //鹿港城隍廟
                    view = "Temple_" + Year + "..Purdue_Lk_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_Lk_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "23":
                    //玉敕大樹朝天宮
                    view = "Temple_" + Year + "..Purdue_ma_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_ma_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
                case "30":
                    //鎮瀾買足
                    view = "Temple_" + Year + "..Purdue_mazu_info";
                    sql = "Select * from Temple_" + Year + "..Purdue_mazu_info Where ApplicantID = @ApplicantID and Status = 0";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int PurdueID = int.Parse(dtUpdateStatus.Rows[i]["PurdueID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);

                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where PurdueID=" + PurdueID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除普度訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 8-西螺福興宮 9-桃園大廟景福宮 10-台南正統鹿耳門聖母廟</param>
        /// </summary>
        public bool DeletePurdueNum(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    sql = "Select * from Purdue_da_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "4":
                    //新港奉天宮
                    sql = "Select * from Purdue_h_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "5":
                    //文創商品-新港奉天宮
                    sql = "Select * from ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "6":
                    //北港武德宮
                    sql = "Select * from Purdue_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "8":
                    //西螺福興宮
                    sql = "Select * from Purdue_Fu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "9":
                    //桃園大廟景福宮
                    sql = "Select * from Purdue_Jing_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "10":
                    //台南正統鹿耳門聖母廟
                    sql = "Select * from Purdue_Luer_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        dtUpdateStatus.Rows[i]["Num2String"] = "";
                        dtUpdateStatus.Rows[i]["Num"] = 0;
                        Adapter.Update(dtUpdateStatus);
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除商品小舖訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeleteProductNum(int ApplicantID, string AdminID, string Status, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "5":
                    //文創商品-新港奉天宮
                    view = "Temple_" + Year + "..ApplicantInfo_Product";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case "7":
                    //繞境商品小舖
                    view = "Temple_" + Year + "..ApplicantInfo_Pilgrimage";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Pilgrimage Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case "11":
                    //錢母商品小舖
                    view = "Temple_" + Year + "..ApplicantInfo_Moneymother";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Moneymother Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case "20":
                    //文創商品-西螺福興宮
                    view = "Temple_" + Year + "..ApplicantInfo_Product";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case "22":
                    //文創商品-琉金富貴
                    view = "Temple_" + Year + "..ApplicantInfo_Product";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
                case "29":
                    //文創商品-財神小舖
                    view = "Temple_" + Year + "..ApplicantInfo_Product";
                    sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID and AdminID = @AdminID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
                Adapter.AddParameterToSelectCommand("@Status", Status);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where ApplicantID=" + ApplicantID);

                        if (res > 0)
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除繞境商品訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeletePilgrimageNum(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = string.Empty;

            switch (AdminID)
            {
                case "5":
                    //文創商品-新港奉天宮
                    sql = "Select * from ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "7":
                    //繞境商品小舖
                    sql = "Select * from ApplicantInfo_Pilgrimage Where Status = -2 and ApplicantID = @ApplicantID";
                    break;
            }

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    dtUpdateStatus.Rows[i]["Num2String"] = "";
                    dtUpdateStatus.Rows[i]["Num"] = 0;
                    Adapter.Update(dtUpdateStatus);
                }
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 刪除錢母商品訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeleteMoneymotherNum(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string view = "Temple_" + Year + "..ApplicantInfo_Moneymother";
            string sql = "Select * from Temple_" + Year + "..ApplicantInfo_Moneymother Where Status = -2 and ApplicantID = @ApplicantID";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);

            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where ApplicantID=" + ApplicantID);

                    if (res > 0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除錢母商品訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeleteMoneymotherNum(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = "Select * from ApplicantInfo_Moneymother Where Status = -2 and ApplicantID = @ApplicantID";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    dtUpdateStatus.Rows[i]["Num2String"] = "";
                    dtUpdateStatus.Rows[i]["Num"] = 0;
                    Adapter.Update(dtUpdateStatus);
                }
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 刪除商品小舖訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮 7-繞境商品小舖</param>
        /// </summary>
        public bool DeleteProductInfo(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "5":
                    //文創商品-新港奉天宮
                    view = "Temple_" + Year + "..ProductInfo";
                    sql = "Select * from Temple_" + Year + "..ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "7":
                    //繞境商品小舖
                    view = "Temple_" + Year + "..Lights_Am_info";
                    sql = "Select * from ApplicantInfo_Pilgrimage Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "11":
                    //錢母商品小舖
                    view = "Temple_" + Year + "..Lights_Am_info";
                    sql = "Select * from ApplicantInfo_Moneymother Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "20":
                    //文創商品-西螺福興宮
                    view = "Temple_" + Year + "..ProductInfo";
                    sql = "Select * from Temple_" + Year + "..ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "22":
                    //流金富貴商品小舖
                    view = "Temple_" + Year + "..ProductInfo";
                    sql = "Select * from Temple_" + Year + "..ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
                case "28":
                    //財神小舖商品小舖
                    view = "Temple_" + Year + "..ProductInfo";
                    sql = "Select * from Temple_" + Year + "..ProductInfo Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int BuyID = int.Parse(dtUpdateStatus.Rows[i]["BuyID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);

                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where BuyID=" + BuyID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除補財庫訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteSuppliesNum(int ApplicantID, string AdminID, int Suppliestype, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "6":
                    //北港武德宮
                    if (Suppliestype == 1)
                    {
                        //下元補庫
                        view = "Temple_" + Year + "..Supplies_wu_info";
                        sql = "Select * from Temple_" + Year + "..Supplies_wu_info Where Status = 0 and ApplicantID = @ApplicantID";
                    }
                    else if (Suppliestype == 2)
                    {
                        //呈疏補庫
                        view = "Temple_" + Year + "..Supplies_wu_info2";
                        sql = "Select * from Temple_" + Year + "..Supplies_wu_info2 Where Status = 0 and ApplicantID = @ApplicantID";
                    }
                    else if (Suppliestype == 3)
                    {
                        //企業補財庫
                        view = "Temple_" + Year + "..Supplies_wu_info3";
                        sql = "Select * from Temple_" + Year + "..Supplies_wu_info3 Where Status = 0 and ApplicantID = @ApplicantID";
                    }
                    break;
                case "14":
                    //桃園威天宮
                    if (Suppliestype == 4)
                    {
                        //天赦日補運
                        view = "Temple_" + Year + "..Supplies_ty_info";
                        sql = "Select * from Temple_" + Year + "..Supplies_ty_info Where Status = 0 and ApplicantID = @ApplicantID";
                    }
                    break;
                case "29":
                    //進寶財神廟
                    if (Suppliestype == 5)
                    {
                        //天赦日祭改
                        view = "Temple_" + Year + "..Supplies_jb_info";
                        sql = "Select * from Temple_" + Year + "..Supplies_jb_info Where Status = 0 and ApplicantID = @ApplicantID";
                    }
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int SuppliesID = int.Parse(dtUpdateStatus.Rows[i]["SuppliesID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);
                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where SuppliesID=" + SuppliesID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除代燒金紙訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteBPONum(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "29":
                    //進寶財神廟
                    view = "Temple_" + Year + "..BPO_jb_info";
                    sql = "Select * from Temple_" + Year + "..BPO_jb_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int BPOID = int.Parse(dtUpdateStatus.Rows[i]["BPOID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);
                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where BPOID=" + BPOID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除靈寶禮斗訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteLingbaolidouNum(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "23":
                    //玉敕大樹朝天宮
                    view = "Temple_" + Year + "..Lingbaolidou_ma_info";
                    sql = "Select * from Temple_" + Year + "..Lingbaolidou_ma_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int LingbaolidouID = int.Parse(dtUpdateStatus.Rows[i]["LingbaolidouID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);
                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where LingbaolidouID=" + LingbaolidouID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 刪除七朝清醮訂單編號
        /// <param name="ApplicantID">ApplicantID=申請人編號</param>
        /// <param name="AdminID">AdminID=廟宇編號 3-大甲鎮瀾宮 4-新港奉天宮 5-文創商品(新港奉天宮) 6-北港武德宮</param>
        /// </summary>
        public bool DeleteTaoistJiaoCeremonyNum(int ApplicantID, string AdminID, string Year)
        {
            bool result = false;
            string sql = string.Empty;
            string view = string.Empty;

            switch (AdminID)
            {
                case "3":
                    //大甲鎮瀾宮
                    view = "Temple_" + Year + "..TaoistJiaoCeremony_da_info";
                    sql = "Select * from Temple_" + Year + "..TaoistJiaoCeremony_da_info Where Status = 0 and ApplicantID = @ApplicantID";
                    break;
            }

            if (sql != "")
            {
                DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
                DataTable dtUpdateStatus = new DataTable();
                Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
                Adapter.SetSqlCommandBuilder();
                Adapter.Fill(dtUpdateStatus);
                if (dtUpdateStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                    {
                        int TaoistJiaoCeremonyID = int.Parse(dtUpdateStatus.Rows[i]["TaoistJiaoCeremonyID"].ToString());
                        //dtUpdateStatus.Rows[i]["Num2String"] = "";
                        //dtUpdateStatus.Rows[i]["Num"] = 0;
                        //Adapter.Update(dtUpdateStatus);
                        if (view != "")
                        {
                            int res = ExecuteSql("Update " + view + " Set Num2String = '', Num = 0 Where TaoistJiaoCeremonyID=" + TaoistJiaoCeremonyID);

                            if (res > 0)
                            {
                                result = true;
                            }
                        }
                    }
                    result = true;
                }
            }

            return result;
        }

        public bool Updatestatus2appcharge_fet_test(string ClientOrderNumber, int Status, string AdminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select * from APPCharge_fet_test Where Status = 1 and ClientOrderNumber = @ClientOrderNumber";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ClientOrderNumber", ClientOrderNumber);
            AdapterObj.Fill(dtDataList);
            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 1)
            {
                dtDataList.Rows[0]["Status"] = Status;
                AdapterObj.Update(dtDataList);

                if (Updatestatus2applicantinfo_fet_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), Status, AdminID))
                {
                    if (DeletePurdue_fet_test(int.Parse(dtDataList.Rows[0]["ApplicantID"].ToString()), AdminID))
                    {
                        bResult = true;
                    }
                }
            }

            return bResult;
        }

        public bool Updatestatus2applicantinfo_fet_test(int ApplicantID, int Status, string AdminID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select * from ApplicantInfo_fet_test Where Status = 2 and ApplicantID = @ApplicantID and AdminID = 3";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 2)
            {
                dtDataList.Rows[0]["Status"] = Status;
                dtDataList.Rows[0]["UpdateinfoDate"] = dt;
                dtDataList.Rows[0]["UpdateinfoDateString"] = dt.ToString("yyyy-MM-dd");
                AdapterObj.Update(dtDataList);

                bResult = true;
            }


            return bResult;
        }

        public bool DeletePurdue_fet_test(int ApplicantID, string AdminID)
        {
            bool result = false;
            string sql = "Select * From Purdue_fet_test Where ApplicantID = @ApplicantID and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    dtUpdateStatus.Rows[i]["Num2String"] = "";
                    dtUpdateStatus.Rows[i]["Num"] = 0;
                    Adapter.Update(dtUpdateStatus);
                }
                result = true;
            }

            return result;
        }

    }
}