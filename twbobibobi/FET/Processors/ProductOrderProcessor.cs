using twbobibobi.Data;
using Newtonsoft.Json;
using Read.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Temple.data;
using Temple.FET.APITEST;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: ProductOrderProcessor
    /// 實作 ITempleOrderProcessor，商品小舖建單邏輯
    /// </summary>
    public class ProductOrderProcessor : TempleOrderProcessorBase
    {
        private readonly int _adminID;
        private string PostUrl = "Product_{0}_Index_FETAPI";

        /// <summary>
        /// 建構式：建立商品小舖處理器實例。
        /// </summary>
        /// <param name="page">目前頁面 BasePage 實例，用於記錄與取得 HttpContext。</param>
        /// <param name="adminID">宮廟編號。</param>
        /// <param name="year">當前年度字串。</param>
        /// <exception cref="NotSupportedException"></exception>
        public ProductOrderProcessor(
            BasePage page,
            int adminID,
            string year)
            : base(page, year)
        {
            _adminID = adminID;

            switch (adminID)
            {
                //新港奉天宮文創小販部
                case 5:
                    PostUrl = string.Format(PostUrl, "h");
                    break;
                //西螺福興宮文創小販部
                case 20:
                    PostUrl = string.Format(PostUrl, "Fu");
                    break;
                //流金富貴文創小販部
                case 22:
                    PostUrl = string.Format(PostUrl, "Fg");
                    break;
                //財神小舖文創小販部
                case 28:
                    PostUrl = string.Format(PostUrl, "god");
                    break;
                default:
                    throw new NotSupportedException($"不支援的宮廟編號 a={adminID}");
            }
        }

        /// <summary>
        /// 取得宮廟編號
        /// </summary>
        /// <returns></returns>
        protected override int GetAdminId() => _adminID;

        /// <summary>
        /// 處理文創小販部訂單
        /// </summary>
        /// <param name="applicant">購買人資料</param>
        /// <param name="prayedPersons">祈福人列表</param>
        /// <param name="Tid">交易代碼</param>
        /// <param name="fetOrderNumber">合作方訂單號</param>
        /// <param name="kind">服務種類 (e.g. "1"=點燈)</param>
        /// <param name="totalAmount">取得總金額</param>
        /// <param name="itemsInfo">取得祈福人資料</param>
        /// <param name="OrderId">取得訂單序號</param>
        protected override Task<List<string>> HandleOrderAsync(
            ApplicantDto applicant,
            List<PrayedPersonDto> prayedPersons,
            string Tid,
            string fetOrderNumber,
            string kind,
            string totalAmount,
            string itemsInfo,
            string OrderId)
        {
            var resultOrderNumbers = new List<string>();

            // 根據 kind 選擇服務邏輯
            switch (kind)
            {
                // 文創小販部
                case "3":

                    switch (_adminID)
                    {
                        //新港奉天宮文創小販部
                        case 5:
                            resultOrderNumbers = Product_ApplicantInfo(
                                applicant: applicant,
                                prayedPersons: prayedPersons,
                                Tid: Tid,
                                fetOrderNumber: fetOrderNumber,
                                kind: kind,
                                totalAmount: totalAmount,
                                itemsInfo: itemsInfo,
                                OrderId: OrderId);
                            break;
                        //西螺福興宮文創小販部
                        case 20:
                            resultOrderNumbers = Product_ApplicantInfo(
                                applicant: applicant,
                                prayedPersons: prayedPersons,
                                Tid: Tid,
                                fetOrderNumber: fetOrderNumber,
                                kind: kind,
                                totalAmount: totalAmount,
                                itemsInfo: itemsInfo,
                                OrderId: OrderId);
                            break;
                        //流金富貴文創小販部
                        case 22:
                            resultOrderNumbers = Product_ApplicantInfo(
                                applicant: applicant,
                                prayedPersons: prayedPersons,
                                Tid: Tid,
                                fetOrderNumber: fetOrderNumber,
                                kind: kind,
                                totalAmount: totalAmount,
                                itemsInfo: itemsInfo,
                                OrderId: OrderId);
                            break;
                        //財神小舖文創小販部
                        case 28:
                            resultOrderNumbers = Product_ApplicantInfo(
                                applicant: applicant,
                                prayedPersons: prayedPersons,
                                Tid: Tid,
                                fetOrderNumber: fetOrderNumber,
                                kind: kind,
                                totalAmount: totalAmount,
                                itemsInfo: itemsInfo,
                                OrderId: OrderId);
                            break;
                        default:
                            throw new NotSupportedException($"不支援的宮廟編號 a={_adminID}");
                    }

                    break;

                default:
                    throw new NotSupportedException($"不支援的服務種類 kind={kind}");
            }

            return Task.FromResult(resultOrderNumbers);
        }

        //文創小販部
        private List<string> Product_ApplicantInfo(
            ApplicantDto applicant,
            List<PrayedPersonDto> prayedPersons,
            string Tid,
            string fetOrderNumber,
            string kind,
            string totalAmount,
            string itemsInfo,
            string OrderId)
        {
            var resultOrderNumbers = new List<string>();

            // 台北時區時間
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            DateTime dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, tz);

            // 處理 Receipt 欄位預設
            string receiptName = string.IsNullOrEmpty(applicant.reName) ? applicant.appName : applicant.reName;
            string receiptMobile = string.IsNullOrEmpty(applicant.reMobile) ? applicant.appMobile : applicant.reMobile;

            int.TryParse(totalAmount, out int cost);

            // 新增購買人資料
            int applicantId = _lightDAC.Addapplicantinfo_Product(
                AdminID: _adminID.ToString(),
                Name: applicant.appName,
                Mobile:applicant.appMobile,
                Cost: cost,
                County: applicant.appCity,
                Dist: applicant.appRegion,
                Addr: applicant.appAddr,
                ZipCode: applicant.appzipCode,
                Sendback: applicant.sendback,
                ReceiptName: receiptName,
                ReceiptMobile: receiptMobile,
                Email: applicant.appEmail,
                Status: 2,
                PostURL: PostUrl,
                Year: _year
            );

            if (applicantId <= 0)
                return resultOrderNumbers;

            // 做 JSON 備份
            string itemsJson = JsonConvert.SerializeObject(prayedPersons);

            int amount = 0;
            int.TryParse(totalAmount, out amount);

            resultOrderNumbers.AddRange(ProcessPrducting(applicantId, applicant, prayedPersons, Tid, fetOrderNumber, amount, itemsInfo, OrderId, dtNow));

            return resultOrderNumbers;
        }

        // 文創商品小販部服務
        private List<string> ProcessPrducting(int applicantId,
                                             ApplicantDto applicant,
                                             List<PrayedPersonDto> prayedPersons,
                                             string Tid,
                                             string fetOrderNumber,
                                             int totalAmount,
                                             string itemsJson,
                                             string OrderId,
                                             DateTime dtNow)
        {
            var orderNumbers = new List<string>();
            string[] productList = new string[0];

            // 逐筆插入燈明細
            foreach (var p in prayedPersons)
            {
                int.TryParse(p.ItemTypeID, out int typeID);
                int.TryParse(p.TypeID, out int productID);

                // 呼叫 LightDAC
                int productId = _lightDAC.Addproductinfo(
                    AdminID: _adminID,
                    ApplicantID: applicantId,
                    tAID: 0,
                    UserID: 0,
                    TypeID: typeID,
                    ProductID: productID,
                    Cost: p.Cost,
                    Count: p.Qty,
                    Year: _year);
            }

            // 建立交易流水
            string callbackLog = $"{Tid},{fetOrderNumber}";
            _lightDAC.AddChargeLog_Product(
                OrderID: OrderId,
                AdminID: _adminID,
                ApplicantID: applicantId,
                Amount: totalAmount,
                ChargeType: "FETAPI",
                Status: 0,
                Description: itemsJson,
                Comment: string.Empty,
                PayChannelLog: fetOrderNumber,
                IP: _page.Request.UserHostAddress,
                Year: _year);

            // 更新並獲取燈號
            DataTable dtCharge = _lightDAC.GetChargeLog_Product(OrderId, _year);
            if (dtCharge != null && dtCharge.Rows.Count > 0 && Convert.ToInt32(dtCharge.Rows[0]["Status"]) == 0)
            {
                string msg = "【保必保庇】線上宮廟服務平臺，感謝購買，已成功付款" + totalAmount + "元，您的訂單編號 ";

                var productArr = new string[productList.Length];
                _lightDAC.UpdateProductInfo(
                    applicantID: applicantId,
                    adminID: _adminID,
                    Year: _year,
                    ref msg,
                    ref productList,
                    ref productArr);
                orderNumbers.AddRange(productArr);

                //更新購買數量至商品表or商品類別表
                if (!_lightDAC.UpdateCount2Product(
                    ApplicantID: applicantId,
                    Year: _year.ToString()))
                {
                    // 沒有更新到數量表，都當作失敗
                    throw new InvalidOperationException("更新數量狀態異常");
                }

                string ChargeType = string.Empty;
                int uStatus = 0;
                //更新流水付費表資訊(付費成功)
                if (!_lightDAC.UpdateChargeLog_Product(
                    OrderId,
                    Tid,
                    msg,
                    _page.Request.UserHostAddress,
                    callbackLog, _year, ref ChargeType, ref uStatus))
                {
                    // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                    throw new InvalidOperationException("更新交易流水或狀態異常");
                }
            }
            else
            {
                // 沒有查到任何 ChargeLog，或 Status != 0，都當作失敗
                throw new InvalidOperationException("查無交易流水或狀態異常");
            }

            return orderNumbers;
        }

    }
}