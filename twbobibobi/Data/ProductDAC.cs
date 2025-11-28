using twbobibobi.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Temple.data
{
    public class ProductDAC : SqlClientBase
    {
        public ProductDAC(BasePage basePage) : base(basePage)
        {

        }

        public string GetConfigValue(string paramName)
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

        private string SubstringTitle(string OldTitle, string NewTitle, int SubstringNum)
        {
            if (OldTitle != null)
            {
                string title = OldTitle.ToString();
                if (title.Length > SubstringNum)
                {
                    NewTitle = title.Substring(0, SubstringNum) + "...";
                }
                else
                {
                    NewTitle = title;
                }
            }

            return NewTitle;
        }

        /// <summary>
        /// 建立申請人資料
        /// </summary>
        /// <param name="Name">Name=購買人姓名</param>
        /// <param name="Phone">Phone=購買人電話</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="Zipcode">Zipcode=郵遞區號</param>
        /// <returns></returns>
        public int addapplicantinfo(string Name, string Phone, string Country, string Dist, string Addr, string Address, string Zipcode)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Product(Name, Mobile, County, Dist, Address, Addr, Zipcode, CreateDate) values(@Name, @Mobile, @County, @Dist, @Address, @Addr, @Zipcode, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@County", Country);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Zipcode", Zipcode);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立申請人資料
        /// </summary>
        /// <param name="Name">Name=購買人姓名</param>
        /// <param name="Phone">Phone=購買人電話</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="Zipcode">Zipcode=郵遞區號</param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo(string Name, string Phone, string Country, string Dist, string Addr, string Address, string Zipcode, string postURL)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ApplicantInfo_Product(Name, Mobile, County, dist, Address, Addr, Zipcode, PostURL, CreateDate) values(@Name, @Mobile, @County, @Dist, @Address, @Addr, @Zipcode, @postURL, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@County", Country);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Zipcode", Zipcode);
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.AddParameterToSelectCommand("@Cost", 0);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立申請人資料-商品小舖-測試
        /// </summary>
        /// <param name="Name">Name=購買人姓名</param>
        /// <param name="Phone">Phone=購買人電話</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="Zipcode">Zipcode=郵遞區號</param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Product_test(string AdminID, string Name, string Phone, string Country, string Dist, string Addr, string Zipcode, string Sendback, string ReceiptName, int Status, string Cost, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_TEST..ApplicantInfo_Product(AdminID, Name, Mobile, County, dist, Address, Addr, Zipcode, Sendback, ReceiptName, Cost, Status, CreateDate, CreateDateString, PostURL) " +
                "values(@AdminID, @Name, @Mobile, @County, @Dist, @Address, @Addr, @Zipcode, @Sendback, @ReceiptName, @Cost, @Status, @CreateDate, @CreateDateString, @postURL)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@County", Country);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", Country + Dist + Addr);
            Adapter.AddParameterToSelectCommand("@Zipcode", Zipcode);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dt.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立申請人資料-商品小舖
        /// </summary>
        /// <param name="Name">Name=購買人姓名</param>
        /// <param name="Phone">Phone=購買人電話</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="Zipcode">Zipcode=郵遞區號</param>
        /// <param name="postURL">postURL=訪問的網址</param>
        /// <returns></returns>
        public int addapplicantinfo_Product(string AdminID, string Name, string Phone, string Country, string Dist, string Addr, string Zipcode, string Sendback, string ReceiptName, string ReceiptMobile, int Status, string Cost, string postURL, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ApplicantInfo_Product(AdminID, Name, Mobile, County, dist, Address, Addr, Zipcode, Sendback, ReceiptName, ReceiptMobile, Cost, Status, CreateDate, CreateDateString, PostURL) " +
                "values(@AdminID, @Name, @Mobile, @County, @Dist, @Address, @Addr, @Zipcode, @Sendback, @ReceiptName, @ReceiptMobile, @Cost, @Status, @CreateDate, @CreateDateString, @postURL)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable(); ;
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@County", Country);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@Address", Country + Dist + Addr);
            Adapter.AddParameterToSelectCommand("@Zipcode", Zipcode);
            Adapter.AddParameterToSelectCommand("@Cost", Cost);
            Adapter.AddParameterToSelectCommand("@Sendback", Sendback);
            Adapter.AddParameterToSelectCommand("@ReceiptName", ReceiptName);
            Adapter.AddParameterToSelectCommand("@ReceiptMobile", ReceiptMobile);
            Adapter.AddParameterToSelectCommand("@Status", Status);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.AddParameterToSelectCommand("@CreateDateString", dt.ToString("yyyy-MM-dd"));
            Adapter.AddParameterToSelectCommand("@postURL", postURL);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立單一品項資料
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="UserID">UserID=會員編號</param>
        /// <param name="ProductID">ProductID=商品編號</param>
        /// <param name="TypeID">TypeID=商品類別編號</param>
        /// <param name="Cost">Cost=金額</param>
        /// <param name="Count">Count=數量</param>
        /// </summary>
        public int addproductinfo(int ApplicantID, int tAID, int UserID, int TypeID, string ProductID, string Cost, string Count)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ProductInfo (ApplicantID, tAID, UserID, TypeID, ProductID, Cost, Count, Status, CreateDate) values(@ApplicantID, @tAID, @UserID, @TypeID, @ProductID, @Cost, @Count, @Status, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@tAID", tAID);
            Adapter.AddParameterToSelectCommand("@UserID", UserID);
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.AddParameterToSelectCommand("@Cost", int.Parse(Cost));
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Status", 0);                 
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立單一品項資料
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="UserID">UserID=會員編號</param>
        /// <param name="ProductID">ProductID=商品編號</param>
        /// <param name="TypeID">TypeID=商品類別編號</param>
        /// <param name="Cost">Cost=金額</param>
        /// <param name="Count">Count=數量</param>
        /// </summary>
        public int addproductinfo(string AdminID, int ApplicantID, int tAID, int UserID, int TypeID, string ProductID, string Cost, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ProductInfo (AdminID, ApplicantID, tAID, UserID, TypeID, ProductID, Cost, Count, Status, CreateDate) values(@AdminID, @ApplicantID, @tAID, @UserID, @TypeID, @ProductID, @Cost, @Count, @Status, @CreateDate)";

            int amount = 0, cost = 0, count = 0;

            int.TryParse(Cost, out cost);
            int.TryParse(Count, out count);
            amount = cost * count;

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@tAID", tAID);
            Adapter.AddParameterToSelectCommand("@UserID", UserID);
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.AddParameterToSelectCommand("@Cost", amount);
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立單一品項資料
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="UserID">UserID=會員編號</param>
        /// <param name="ProductID">ProductID=商品編號</param>
        /// <param name="TypeID">TypeID=商品類別編號</param>
        /// <param name="Cost">Cost=金額</param>
        /// <param name="Count">Count=數量</param>
        /// </summary>
        public int addproductinfo_test(string AdminID ,int ApplicantID, int tAID, int UserID, int TypeID, string ProductID, string Cost, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_TEST..ProductInfo (AdminID, ApplicantID, tAID, UserID, TypeID, ProductID, Cost, Count, Status, CreateDate) values(@AdminID, @ApplicantID, @tAID, @UserID, @TypeID, @ProductID, @Cost, @Count, @Status, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@tAID", tAID);
            Adapter.AddParameterToSelectCommand("@UserID", UserID);
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.AddParameterToSelectCommand("@Cost", int.Parse(Cost));
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立單一品項資料
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="UserID">UserID=會員編號</param>
        /// <param name="ProductID">ProductID=商品編號</param>
        /// <param name="TypeID">TypeID=商品類別編號</param>
        /// <param name="Cost">Cost=金額</param>
        /// <param name="Count">Count=數量</param>
        /// </summary>
        public int addproductinfo(int ApplicantID, int tAID, int UserID, int TypeID, string ProductID, string Cost, string Count, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into Temple_" + Year + "..ProductInfo (ApplicantID, tAID, UserID, TypeID, ProductID, Cost, Count, Status, CreateDate) values(@ApplicantID, @tAID, @UserID, @TypeID, @ProductID, @Cost, @Count, @Status, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            Adapter.AddParameterToSelectCommand("@tAID", tAID);
            Adapter.AddParameterToSelectCommand("@UserID", UserID);
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.AddParameterToSelectCommand("@Cost", int.Parse(Cost));
            Adapter.AddParameterToSelectCommand("@Count", Count);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }

        /// <summary>
        /// 建立收件人資料
        /// <param name="BuyID">BuyID=商品編號</param>
        /// <param name="Name">Name=姓名</param>
        /// <param name="Phone">Phone=手機號碼</param>
        /// <param name="Address">Address=地址(全)</param>
        /// <param name="Addr">Addr=地址(部分)</param>
        /// <param name="County">County=縣市</param>
        /// <param name="Dist">Dist=區域</param>
        /// <param name="Zipcode">Zipcode=郵遞區號</param>
        /// </summary>
        public int addreceiveInfo(int BuyID, string Name, string Phone, string Address, string Addr, string County, string Dist, string Zipcode)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);

            string sql = "Insert into ReceiveInfo_Product(BuyID, Name, Mobile, Address, Addr, County, Dist, Zipcode, CreateDate) values(@BuyID, @Name, @Mobile, @Address, @Addr, @County, @Dist, @Zipcode, @CreateDate)";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtdata = new DataTable();
            Adapter.AddParameterToSelectCommand("@BuyID", BuyID);
            Adapter.AddParameterToSelectCommand("@Name", Name);
            Adapter.AddParameterToSelectCommand("@Mobile", Phone);
            Adapter.AddParameterToSelectCommand("@Address", Address);
            Adapter.AddParameterToSelectCommand("@Addr", Addr);
            Adapter.AddParameterToSelectCommand("@County", County);
            Adapter.AddParameterToSelectCommand("@Dist", Dist);
            Adapter.AddParameterToSelectCommand("@Zipcode", Zipcode);
            Adapter.AddParameterToSelectCommand("@Status", 0);
            Adapter.AddParameterToSelectCommand("@CreateDate", dt);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtdata);
            Adapter.Update(dtdata);

            return this.GetIdentity();
        }


        /// <summary>
        /// 更新購買人編號到購買列表
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="Total">Total=總金額</param>
        /// </summary>
        public bool Updateaid2productinfo(int AdminID, int oApplicantID, int tAID, int applicantID, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            if (oApplicantID != 0)
            {
                sql = "Select * From Temple_" + Year + "..ProductInfo Where AdminID = " + AdminID + " and ApplicantID=" + oApplicantID + " and Status = 0";
            }
            if (tAID != 0)
            {
                sql = "Select * From Temple_" + Year + "..ProductInfo Where AdminID = " + AdminID + " and tAID = " + tAID + " and Status = 0";
            }

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0)
            {
                for (int i = 0; i < dtDataList.Rows.Count; i++)
                {
                    string bid = dtDataList.Rows[i]["BuyID"].ToString();

                    int res = ExecuteSql("Update Temple_" + Year + "..ProductInfo Set ApplicantID= " + applicantID + ", tAID= " + 0 + " Where BuyID=" + bid);

                    if (res > 0)
                    {
                        bResult = true;
                    }
                }

                bResult = true;

            }


            return bResult;
        }

        /// <summary>
        /// 更新購買人總金額及狀態(付款中)
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="Total">Total=總金額</param>
        /// </summary>
        public bool Updatecost2applicantinfo(int AdminID, int ApplicantID, int Cost, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From Temple_" + Year + "..ApplicantInfo_Product Where AdminID=@AdminID and ApplicantID=@ApplicantID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.AddParameterToSelectCommand("@AdminID", AdminID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0 && (int)dtDataList.Rows[0]["Status"] == 0)
            {
                int res = ExecuteSql("Update Temple_" + Year + "..ApplicantInfo_Product Set Status=1 , Cost= " + Cost + " Where ApplicantID=" + ApplicantID);

                if (res > 0)
                {
                    bResult = true;
                }
            }


            return bResult;
        }

        /// <summary>
        /// 更新購買人編號到購買列表
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="Total">Total=總金額</param>
        /// </summary>
        public bool Updateaid2productinfo(int oApplicantID, int tAID, int applicantID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = string.Empty;
            if (oApplicantID != 0)
            {
                sql = "Select * From ProductInfo Where ApplicantID=" + oApplicantID + " and Status = 0";
            }
            if (tAID != 0)
            {
                sql = "Select * From ProductInfo Where tAID = " + tAID + " and Status = 0";
            }

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0)
            {
                for (int i = 0; i < dtDataList.Rows.Count; i++)
                {
                    dtDataList.Rows[i]["ApplicantID"] = applicantID;
                    dtDataList.Rows[i]["tAID"] = 0;
                    AdapterObj.Update(dtDataList);
                }

                bResult = true;

            }


            return bResult;
        }

        /// <summary>
        /// 更新購買人總金額
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="Total">Total=總金額</param>
        /// </summary>
        public bool Updatecost2applicanInfo(int ApplicantID, int Total)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Product Where ApplicantID=@ApplicantID and Status = 0";

            DatabaseAdapter AdapterObj = new DatabaseAdapter(sql, this.DBSource);
            AdapterObj.SetSqlCommandBuilder();
            AdapterObj.AddParameterToSelectCommand("@ApplicantID", ApplicantID);
            AdapterObj.Fill(dtDataList);

            if (dtDataList.Rows.Count > 0)
            {
                dtDataList.Rows[0]["Cost"] = Total;
                AdapterObj.Update(dtDataList);

                bResult = true;

            }


            return bResult;
        }

        /// <summary>
        /// 更新購買人總金額及狀態(付款中)
        /// <param name="ApplicantID">ApplicantID=購買人編號</param>
        /// <param name="Total">Total=總金額</param>
        /// </summary>
        public bool Updatecost2applicantinfo(int ApplicantID, int Cost)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            bool bResult = false;
            DataTable dtDataList = new DataTable();
            string sql = "Select Top 1 * From ApplicantInfo_Product Where ApplicantID=@ApplicantID and Status = 0";

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

        /// <summary>
        /// 檢查產品餘額
        /// <param name="productID">productID=產品編號</param>
        /// <param name="typeID">typeID=產品細項編號</param>
        /// <param name="adminID">adminID=廟方編號</param>
        /// <param name="Num">Num=購買數量</param>
        /// </summary>
        public bool checkedproductnum(string productID, string typeID, string adminID, int Num)
        {
            bool result = false;
            string sql = string.Empty;

            if (typeID != "0")
            {
                sql = "Select * from Item_Product Where AdminID = @AdminID and TypeID = " + typeID + " and Status = 0";
            }
            else
            {
                sql = "Select * from Product Where AdminID = @AdminID and ProductID = " + productID + " and Status = 0";
            }

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("AdminID", adminID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            int count = int.Parse(dtGetData.Rows[0]["Num"].ToString()) - int.Parse(dtGetData.Rows[0]["Usecount"].ToString());
            if (count >= Num)
            {
                result = true;
            }

            return result;
        }


        /// <summary>
        /// 取得申請人資料
        /// <param name="applecantID">applecantID=申請人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo(int applicantID, int Status, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得申請人資料
        /// <param name="applecantID">applecantID=申請人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo(int applicantID, int Status)
        {
            string sql = "Select * from ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得購買人資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getapplicantinfo_product(int applicantID, int Status)
        {
            string sql = "Select * from ApplicantInfo_Product Where Status = @Status and ApplicantID = @ApplicantID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            objDatabaseAdapter.AddParameterToSelectCommand("Status", Status);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得申請人資料
        /// <param name="name">name=申請人姓名</param>
        /// <param name="phone">phone=手機號碼</param>
        /// </summary>
        public DataTable Getapplicantinfo(string name, string phone)
        {
            string sql = "Select * from ApplicantInfo_Product Where Status = 2 and Name = @Name and Mobile = @Mobile";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("Name", name);
            objDatabaseAdapter.AddParameterToSelectCommand("Mobile", phone);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得購買資料
        /// <param name="applecantID">applecantID=申請人編號</param>
        /// </summary>
        public DataTable Getproductinfofaid(int applicantID, int tAID)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            string sql = string.Empty;

            if (applicantID != 0)
            {
                sql = "Select * from view_ProductInfolist Where Status = 0 and ApplicantID = " + applicantID;
            }
            else
            {
                sql = "Select * from view_ProductInfolist Where Status = 0 and tAID = " + tAID;
            }

            sql += " and CreateDate between '" + dt.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dt.AddDays(1).ToString("yyyy-MM-dd 23:59:59") + "'"; 

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得購買資料
        /// <param name="applecantID">applecantID=申請人編號</param>
        /// </summary>
        public DataTable Getproductinfofaid(int AdminID, int applicantID, int tAID, string Year)
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, info);
            string sql = string.Empty;

            if (applicantID != 0)
            {
                sql = "Select Temple_" + Year + "..ProductInfo.* from Temple_" + Year + "..ProductInfo Where Status = 0 and AdminID = " + AdminID + " and ApplicantID = " + applicantID;
            }
            else
            {
                sql = "Select Temple_" + Year + "..ProductInfo.*, Product.Title as ProductName  from Temple_" + Year + "..ProductInfo, Product Where Temple_" + Year + "..ProductInfo.Status = 0 and Temple_" + Year + "..ProductInfo.AdminID = " + AdminID + " and Temple_" + Year + "..ProductInfo.tAID = " + tAID + " and Temple_" + Year + "..ProductInfo.ProductID = Product.ProductID";
            }

            sql += " and Temple_" + Year + "..ProductInfo.CreateDate between '" + dtNow.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtNow.AddDays(1).ToString("yyyy-MM-dd 23:59:59") + "'";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得商品資料
        /// <param name="productID">productID=商品編號</param>
        /// </summary>
        public DataTable Getproductfpid(int productID)
        {
            string sql = "Select * from view_Productlist Where Status = 0 and ImageType = 0 and ProductID = @ProductID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ProductID", productID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得商品資訊
        /// <param name="AdminID">AdminID=商家編號</param>
        /// </summary>
        public DataTable GetProductInfo(int ProductID, int TypeID)
        {
            string sql = "Select * from view_ProductInfo Where ProductID = @ProductID";

            if (TypeID > 0)
            {
                sql += " and TypeID = " + TypeID;
            }

            sql += " and Status = 0 Order By Sort";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ProductID", ProductID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);
            return dtGetData;
        }

        /// <summary>
        /// 取得商品資訊
        /// <param name="AdminID">AdminID=商家編號</param>
        /// </summary>
        public DataTable GetProductInfo(int ProductID, int TypeID, string Year)
        {
            string sql = "Select * from Temple_" + Year + "..view_ProductInfolistwithAPPCharge Where ProductID = @ProductID";

            if (TypeID > 0)
            {
                sql += " and TypeID = " + TypeID;
            }

            sql += " and Status = 0 and AppStatus = 2 and AppcStatus = 1 and Num > 0";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ProductID", ProductID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);
            return dtGetData;
        }

        /// <summary>
        /// 取得商品列表
        /// <param name="AdminID">AdminID=商家編號</param>
        /// </summary>
        public DataTable GetProductList(int pageindex, int pagesize, int AdminID)
        {
            string sql = "Select * from view_Productlist Where ImageType = 0 and AdminID = @AdminID and Status = 0 Order By Sort";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtData");
            DataTable dtGetData = dsVideo.Tables["dtData"];
            //將Date的時間部分去除
            DataColumn column = dtGetData.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetData.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetData.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetData.Rows[i]["CreateDate"].ToString());
                dtGetData.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetData.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetData.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetData.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetData.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetData.Rows[i]["OriginalImageAddress"] = dtGetData.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetData.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetData.Rows[i]["Title"].ToString(), dtGetData.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetData;
        }

        /// <summary>
        /// 取得商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getproductinfo(int applicantID, int appStatus, string Year)
        {
            
            string sql = "Select Temple_" + Year + "..view_ProductInfolist.AdminID, Temple_" + Year + "..view_ProductInfolist.ProductID, Temple_" + Year + "..view_ProductInfolist.TypeID, Temple_" + Year + "..view_ProductInfolist.Num2String, Temple_" + Year + "..view_ProductInfolist.Count, Temple_" + Year + "..view_ProductInfolist.Cost, Product.Title as productName from Temple_" + Year + "..view_ProductInfolist, Product Where Temple_" + Year + "..view_ProductInfolist.Status = 0 and Temple_" + Year + "..view_ProductInfolist.AppStatus = " + appStatus + " and Temple_" + Year + "..view_ProductInfolist.ApplicantID = @ApplicantID and Temple_" + Year + "..view_ProductInfolist.ProductID = Product.ProductID";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得商品資料
        /// <param name="applecantID">applecantID=購買人編號</param>
        /// </summary>
        public DataTable Getproductinfo(int applicantID, int appStatus)
        {
            string sql = "Select * from view_ProductInfolist Where Status = 0 and appStatus = "+ appStatus +" and ApplicantID = @ApplicantID Order by Num Desc";

            DatabaseAdapter objDatabaseAdapter = new DatabaseAdapter(sql, this.DBSource);
            objDatabaseAdapter.AddParameterToSelectCommand("ApplicantID", applicantID);
            DataTable dtGetData = new DataTable();
            objDatabaseAdapter.Fill(dtGetData);

            return dtGetData;
        }

        /// <summary>
        /// 取得商品項目列表
        /// <param name="AdminID">AdminID=商家編號</param>
        /// </summary>
        public DataTable GetItemlist(int ProductID)
        {
            string sql = "Select * from Item_Product Where ProductID = @ProductID and Status = 0 Order By Sort";

            DataTable dtData = new DataTable();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.Fill(dtData);
            return dtData;
        }

        public int GetItemcost(int TypeID)
        {
            string sql = "Select * From Item_Product Where TypeID = @TypeID and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            DataTable dtData = new DataTable();
            Adapter.Fill(dtData);
            return (int)dtData.Rows[0]["Cost"];
        }

        public DataTable GetItemInfo(int TypeID)
        {
            string sql = "Select * From Item_Product Where TypeID = @TypeID and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            DataTable dtData = new DataTable();
            Adapter.Fill(dtData);
            return dtData;
        }

        public DataTable GetItemInfo(string ProductID, string TypeID)
        {
            string sql = "Select * From Item_Product Where TypeID = @TypeID and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.AddParameterToSelectCommand("@TypeID", TypeID);
            DataTable dtData = new DataTable();
            Adapter.Fill(dtData);
            return dtData;
        }

        public int GetProductListCount(int AdminID)
        {
            string sql = "Select Count(*) as count From view_Productlist Where ImageType = 0 and AdminID = @AdminID and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            DataTable dtData = new DataTable();
            Adapter.Fill(dtData);
            return (int)dtData.Rows[0]["count"];
        }

        /// <summary>
        /// 取得商品圖片列表
        /// <param name="ProductID">ProductID=商品編號</param>
        /// </summary>
        public DataTable GetImgageslist(int ProductID)
        {
            string sql = "Select * from Productimages Where ProductID = @ProductID and Status = 0 Order By Sort";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtGetData);

            for (int i = 0; i < dtGetData.Rows.Count; i++)
            {
                //判斷圖片位址開頭是否為http
                if (dtGetData.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetData.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetData.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetData.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetData.Rows[i]["OriginalImageAddress"] = dtGetData.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

            }

            return dtGetData;
        }

        /// <summary>
        /// 取得商品圖片
        /// <param name="ProductID">ProductID=商品編號</param>
        /// </summary>
        public string GetProductImgages(int ProductID)
        {
            string result = string.Empty;

            string sql = "Select * from Productimages Where ProductID = @ProductID and Status = 0 Order By Sort";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtGetData);

            for (int i = 0; i < dtGetData.Rows.Count; i++)
            {
                //判斷圖片位址開頭是否為http
                if (dtGetData.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetData.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetData.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetData.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetData.Rows[i]["OriginalImageAddress"] = dtGetData.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }
            }

            if (dtGetData.Rows.Count > 0)
            {
                result = dtGetData.Rows[0]["OriginalImageAddress"].ToString();
            }

            return result;
        }

        /// <summary>
        /// 取得商品圖片
        /// <param name="ProductID">ProductID=商品編號</param>
        /// </summary>
        public string GetProductImgages(int ProductID, string Year)
        {
            string result = string.Empty;

            string sql = "Select * from Temple_" + Year + "..Productimages Where ProductID = @ProductID and Status = 0 Order By Sort";

            DataTable dtGetData = new DataTable();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.AddParameterToSelectCommand("@ProductID", ProductID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtGetData);

            for (int i = 0; i < dtGetData.Rows.Count; i++)
            {
                //判斷圖片位址開頭是否為http
                if (dtGetData.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetData.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetData.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetData.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetData.Rows[i]["OriginalImageAddress"] = dtGetData.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }
            }

            if (dtGetData.Rows.Count > 0)
            {
                result = dtGetData.Rows[0]["OriginalImageAddress"].ToString();
            }

            return result;
        }





        /// <summary>
        /// 刪除購買資料
        /// <param name="applicantID">applicantID=申請人編號</param>
        /// </summary>
        public void DeleteProductInfo(int applicantID)
        {
            string sql = "Select * From ProductInfo Where ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    dtUpdateStatus.Rows[i]["Status"] = -1;
                    Adapter.Update(dtUpdateStatus);
                }
            }
        }

        /// <summary>
        /// 刪除購買資料
        /// <param name="applicantID">applicantID=申請人編號</param>
        /// </summary>
        public void DeleteProductInfo(int AdminID, int applicantID, string Year)
        {
            string sql = "Select * From Temple_" + Year + "..ProductInfo Where AdminID = @AdminID and ApplicantID = @ApplicantID and Status = 0";
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            DataTable dtUpdateStatus = new DataTable();
            Adapter.AddParameterToSelectCommand("@ApplicantID", applicantID);
            Adapter.AddParameterToSelectCommand("@AdminID", AdminID);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dtUpdateStatus);
            if (dtUpdateStatus.Rows.Count > 0)
            {
                for (int i = 0; i < dtUpdateStatus.Rows.Count; i++)
                {
                    string bid = dtUpdateStatus.Rows[i]["BuyID"].ToString();

                    int res = ExecuteSql("Update Temple_" + Year + "..ProductInfo Set Status= -1 Where BuyID=" + bid);

                    if (res > 0)
                    {
                        //bResult = true;
                    }
                }
            }
        }




















        public DataTable GetFreeVideoList(int pageindex, int pagesize)
        {
            string sql = "Select * from view_NewVideo Where VideoUnit = 0 and ImageType = 1 and Status = 0 and videotype = 5 Order By CreateDate desc";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtVideo");
            DataTable dtGetVideoList = dsVideo.Tables["dtVideo"];
            //將Date的時間部分去除
            DataColumn column = dtGetVideoList.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetVideoList.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetVideoList.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetVideoList.Rows[i]["CreateDate"].ToString());
                dtGetVideoList.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetVideoList.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetVideoList.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetVideoList.Rows[i]["OriginalImageAddress"] = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetVideoList.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetVideoList.Rows[i]["VideoTitle"].ToString(), dtGetVideoList.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetVideoList;
        }
        public int GetFreeVideoListCount()
        {
            string sql = "Select Count(*) as count From AVVideo Where VideoType = 5 and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            DataTable dtGetVideoList = new DataTable();
            Adapter.Fill(dtGetVideoList);
            return (int)dtGetVideoList.Rows[0]["count"];
        }

        public DataTable GetNewVideoList(int pageindex, int pagesize)
        {
            string sql = "Select * from view_NewVideo Where VideoUnit = 0 and ImageType = 1 and Status = 0 Order By CreateDate desc";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtVideo");
            DataTable dtGetVideoList = dsVideo.Tables["dtVideo"];
            //將Date的時間部分去除
            DataColumn column = dtGetVideoList.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetVideoList.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetVideoList.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetVideoList.Rows[i]["CreateDate"].ToString());
                dtGetVideoList.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetVideoList.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetVideoList.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetVideoList.Rows[i]["OriginalImageAddress"] = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetVideoList.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetVideoList.Rows[i]["VideoTitle"].ToString(), dtGetVideoList.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetVideoList;
        }
        public int GetNewVideoListCount()
        {
            string sql = "Select Count(*) as count From AVVideo Where Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            DataTable dtGetVideoList = new DataTable();
            Adapter.Fill(dtGetVideoList);
            return (int)dtGetVideoList.Rows[0]["count"];
        }

        public DataTable GetLongVideoList(int pageindex, int pagesize)
        {
            string sql = "Select * from view_NewVideo Where VideoUnit = 0 and ImageType = 1 and Status = 0 and videotype <= 2 Order By Visit desc";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtVideo");
            DataTable dtGetVideoList = dsVideo.Tables["dtVideo"];
            //將Date的時間部分去除
            DataColumn column = dtGetVideoList.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetVideoList.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetVideoList.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetVideoList.Rows[i]["CreateDate"].ToString());
                dtGetVideoList.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetVideoList.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetVideoList.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetVideoList.Rows[i]["OriginalImageAddress"] = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetVideoList.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetVideoList.Rows[i]["VideoTitle"].ToString(), dtGetVideoList.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetVideoList;
        }
        public int GetLongVideoListCount()
        {
            string sql = "Select Count(*) as count From AVVideo Where VideoType <= 2 and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            DataTable dtGetVideoList = new DataTable();
            Adapter.Fill(dtGetVideoList);
            return (int)dtGetVideoList.Rows[0]["count"];
        }

        public DataTable GetShortVideoList(int pageindex, int pagesize)
        {
            string sql = "Select * from view_NewVideo Where VideoUnit = 0 and ImageType = 1 and Status = 0 and videotype = 6 Order By Visit desc";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtVideo");
            DataTable dtGetVideoList = dsVideo.Tables["dtVideo"];
            //將Date的時間部分去除
            DataColumn column = dtGetVideoList.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetVideoList.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetVideoList.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetVideoList.Rows[i]["CreateDate"].ToString());
                dtGetVideoList.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetVideoList.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetVideoList.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetVideoList.Rows[i]["OriginalImageAddress"] = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetVideoList.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetVideoList.Rows[i]["VideoTitle"].ToString(), dtGetVideoList.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetVideoList;
        }
        public int GetShortVideoListCount()
        {
            string sql = "Select Count(*) as count From AVVideo Where VideoType = 6 and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            DataTable dtGetVideoList = new DataTable();
            Adapter.Fill(dtGetVideoList);
            return (int)dtGetVideoList.Rows[0]["count"];
        }

        public DataTable GetNopixVideoList(int pageindex, int pagesize)
        {
            string sql = "Select * from view_NewVideo Where VideoUnit = 0 and ImageType = 1 and Status = 0 and videotype = 4 Order By Visit desc";

            DataSet dsVideo = new DataSet();
            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            Adapter.Fill(dsVideo, ((pageindex - 1) * pagesize), pagesize, "dtVideo");
            DataTable dtGetVideoList = dsVideo.Tables["dtVideo"];
            //將Date的時間部分去除
            DataColumn column = dtGetVideoList.Columns.Add("Date");
            column.ReadOnly = false;
            column.DataType = Type.GetType("System.String");
            column.Unique = false;

            DataColumn columnTitle = dtGetVideoList.Columns.Add("ShortenTitle");
            columnTitle.ReadOnly = false;
            columnTitle.DataType = Type.GetType("System.String");
            columnTitle.Unique = false;

            for (int i = 0; i < dtGetVideoList.Rows.Count; i++)
            {
                DateTime DateChang = Convert.ToDateTime(dtGetVideoList.Rows[i]["CreateDate"].ToString());
                dtGetVideoList.Rows[i]["Date"] = DateChang.ToString("yyyy-MM-dd");

                //判斷圖片位址開頭是否為http
                if (dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString() != "")
                {
                    int checkedHTTP = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().IndexOf("http");

                    //如果ImageAddress開頭不是http
                    if (checkedHTTP == -1)
                    {
                        dtGetVideoList.Rows[i]["OriginalImageAddress"] = GetConfigValue("ImageWebHome") + dtGetVideoList.Rows[i]["OriginalImageAddress"];
                    }
                    dtGetVideoList.Rows[i]["OriginalImageAddress"] = dtGetVideoList.Rows[i]["OriginalImageAddress"].ToString().Replace(@"\", "/");
                }

                //縮減Title剩16字,放入ShortenTitle
                dtGetVideoList.Rows[i]["ShortenTitle"] = SubstringTitle(dtGetVideoList.Rows[i]["VideoTitle"].ToString(), dtGetVideoList.Rows[i]["ShortenTitle"].ToString(), 16);
            }
            return dtGetVideoList;
        }
        public int GetNopixVideoListCount()
        {
            string sql = "Select Count(*) as count From AVVideo Where VideoType = 4 and Status = 0";

            DatabaseAdapter Adapter = new DatabaseAdapter(sql, this.DBSource);
            Adapter.SetSqlCommandBuilder();
            DataTable dtGetVideoList = new DataTable();
            Adapter.Fill(dtGetVideoList);
            return (int)dtGetVideoList.Rows[0]["count"];
        }
    }
}