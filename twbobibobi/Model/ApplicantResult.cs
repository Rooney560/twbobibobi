using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace twbobibobi.Model
{
    /// <summary>
    /// 表示用戶查詢的申請人資料結果模型。
    /// 用於整合查詢回傳的年度、宮廟、服務類別及訂單資訊。
    /// </summary>
    public class ApplicantResult
    {
        /// <summary>
        /// 資料所屬年度，例如 2024 或 2025。
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 宮廟編號 AdminID。
        /// </summary>
        public int AdminID { get; set; }

        /// <summary>
        /// 購買人姓名。
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 購買人電話。
        /// </summary>
        public string AppMobile { get; set; }

        /// <summary>
        /// 購買人信箱。
        /// </summary>
        public string AppEmail { get; set; }

        /// <summary>
        /// 購買人農曆生日。
        /// </summary>
        public string AppBirth { get; set; }

        /// <summary>
        /// 購買人農曆生日是否閏月。Y-閏月；N-非閏月
        /// </summary>
        public string AppLeapMonth { get; set; }

        /// <summary>
        /// 購買人農曆時辰。
        /// </summary>
        public string AppBirthTime { get; set; }

        /// <summary>
        /// 購買人國曆生日。
        /// </summary>
        public string AppsBirth { get; set; }

        /// <summary>
        /// 購買人地址。
        /// </summary>
        public string AppAddress { get; set; }

        /// <summary>
        /// 購買人是否寄回。Y-寄回 N-不計回
        /// </summary>
        public string AppSendback { get; set; } = "N";

        /// <summary>
        /// 宮廟名稱。
        /// </summary>
        public string TempleName { get; set; }

        /// <summary>
        /// 訂單編號。
        /// </summary>
        public string Num2String { get; set; }

        /// <summary>
        /// 祈福人姓名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 祈福人姓名2。
        /// </summary>
        public string Name2 { get; set; }

        /// <summary>
        /// 祈福人姓名3。
        /// </summary>
        public string Name3 { get; set; }

        /// <summary>
        /// 祈福人姓名4。
        /// </summary>
        public string Name4 { get; set; }

        /// <summary>
        /// 祈福人姓名5。
        /// </summary>
        public string Name5 { get; set; }

        /// <summary>
        /// 祈福人姓名6。
        /// </summary>
        public string Name6 { get; set; }

        /// <summary>
        /// 祈福人電話。
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 祈福人性別。
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 祈福人農曆生日。
        /// </summary>
        public string Birth { get; set; }

        /// <summary>
        /// 祈福人農曆生日是否閏月。Y-閏月；N-非閏月
        /// </summary>
        public string LeapMonth { get; set; } = "N";

        /// <summary>
        /// 祈福人農曆時辰。
        /// </summary>
        public string BirthTime { get; set; }

        /// <summary>
        /// 祈福人國曆生日。
        /// </summary>
        public string sBirth { get; set; }

        /// <summary>
        /// 祈福人市話。
        /// </summary>
        public string Homenum { get; set; }

        /// <summary>
        /// 祈福人信箱。
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 祈福人地址。
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 是否寄回。Y-寄回 N-不計回
        /// </summary>
        public string Sendback { get; set; } = "N";

        /// <summary>
        /// 服務類型（點燈、祈福、普度、補庫）。
        /// </summary>
        public ServiceKind Kind { get; set; }

        /// <summary>
        /// 服務項目細項。
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 服務項目字串。
        /// </summary>
        public string ServiceString { get; set; }

        /// <summary>
        /// 訂單金額。
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// 訂單數量。
        /// </summary>
        public int Count { get; set; } = 1;

        /// <summary>
        /// 訂單狀態。
        /// </summary>
        public string StatusString { get; set; }

        /// <summary>
        /// 訂單受理日期。
        /// </summary>
        public string ChargeDate { get; set; }

        /// <summary>
        /// 備註。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 收件人姓名。
        /// </summary>
        public string rName { get; set; }

        /// <summary>
        /// 收件人電話。
        /// </summary>
        public string rMobile { get; set; }

        /// <summary>
        /// 收件人地址。
        /// </summary>
        public string rAddress { get; set; }

        /// <summary>
        /// 寵物姓名。
        /// </summary>
        public string PetName { get; set; }

        /// <summary>
        /// 寵物品種。
        /// </summary>
        public string PetType { get; set; }

        /// <summary>
        /// 祈福小語。
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 文創小舖-商品名稱。
        /// </summary>
        public string Product_Title { get; set; }

        /// <summary>
        /// 文創小舖-商品細項名稱。
        /// </summary>
        public string Product_ItemName { get; set; }

        /// <summary>
        /// 訂單金額。
        /// </summary>
        public string Product_Cost { get; set; }

        /// <summary>
        /// 文創小舖-數量。
        /// </summary>
        public int Product_Count { get; set; } = 0;


        /// <summary>
        /// 文創小舖-商品圖片。
        /// </summary>
        public string Product_Img { get; set; }
    }


    /// <summary>
    /// DataTable 擴充方法，用於快速轉換查詢結果為 ApplicantResult 清單。
    /// </summary>
    public static class ApplicantResultExtensions
    {
        /// <summary>
        /// 將 DataTable 轉換為 ApplicantResult 集合。
        /// 資料表須包含欄位：Name、Mobile、OrderNo、Code、AddDate。
        /// </summary>
        /// <param name="dt">來源資料表。</param>
        /// <returns>ApplicantResult 集合。</returns>
        public static List<ApplicantResult> ToApplicantResults(this DataTable dt)
        {
            var list = new List<ApplicantResult>();

            if (dt == null || dt.Rows.Count == 0)
                return list;

            foreach (DataRow row in dt.Rows)
            {
                var result = new ApplicantResult
                {
                    AppName = row.Table.Columns.Contains("AppName") ? row["AppName"].ToString() : string.Empty,
                    AppMobile = row.Table.Columns.Contains("AppMobile") ? row["AppMobile"].ToString() : string.Empty,
                    Name = row.Table.Columns.Contains("Name") ? row["Name"].ToString() : string.Empty,
                    Mobile = row.Table.Columns.Contains("Mobile") ? row["Mobile"].ToString() : string.Empty,
                    Num2String = row.Table.Columns.Contains("Num2String") ? row["Num2String"].ToString() : string.Empty,
                    ChargeDate = row.Table.Columns.Contains("ChargeDate") ? row["ChargeDate"].ToString() : string.Empty
                };

                list.Add(result);
            }

            return list;
        }

        /// <summary>
        /// 嘗試將日期字串安全轉換為 DateTime，若轉換失敗則回傳最小值。
        /// </summary>
        private static DateTime TryParseDate(string value)
        {
            if (DateTime.TryParse(value, out DateTime dt))
                return dt;
            return DateTime.MinValue;
        }
    }
}