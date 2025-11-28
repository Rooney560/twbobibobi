using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twbobibobi.Model;

namespace twbobibobi.Helpers
{
    /// <summary>
    /// 負責產生不同宮廟、不同服務類型的 HTML 輸出模板。
    /// </summary>
    public class ServiceTemplateHelper
    {
        #region —— 點燈 —— 

        /// <summary>
        /// 點燈模板
        /// </summary>
        public static string Lights_defult(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 北港武德宮
        /// </summary>
        public static string Lights_wu(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人市話", item.Homenum)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 台南正統鹿耳門聖母廟
        /// </summary>
        public static string Lights_Luer(ApplicantResult item)
        {
            // 判斷是否有寵物資料
            bool hasPet = !string.IsNullOrWhiteSpace(item.PetName);

            // 動態欄位名稱
            string ownerTitle = hasPet ? "飼主姓名" : "祈福人姓名";
            string mobileTitle = hasPet ? "飼主電話" : "祈福人電話";
            string sexTitle = hasPet ? "飼主性別" : "祈福人性別";
            string birthTitle = hasPet ? "飼主農曆生日" : "農曆生日";
            string birthtimeTitle = hasPet ? "飼主農曆時辰" : "農曆時辰";
            string sbirthTitle = hasPet ? "飼主國曆生日" : "國曆生日";
            string addressTitle = hasPet ? "飼主居住地址" : "居住地址";

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content(ownerTitle, item.Name)}
                        {addData_Content(mobileTitle, item.Mobile)}
                        {addData_Content(sexTitle, item.Sex)}
                        {addData_Content(birthTitle, item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content(birthtimeTitle, item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content(addressTitle, item.Address)}
                        {addData_Content("祈福小語", item.Msg)}
                        {addData_Content("寵物姓名", item.PetName)}
                        {addData_Content("寵物品種", item.PetType)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 斗六五路財神宮
        /// </summary>
        public static string Lights_Fw(ApplicantResult item)
        {
            // 判斷是否有寵物資料
            bool hasPet = !string.IsNullOrWhiteSpace(item.PetName);

            // 動態欄位名稱
            string ownerTitle = hasPet ? "飼主姓名" : "祈福人姓名";
            string mobileTitle = hasPet ? "飼主電話" : "祈福人電話";
            string sexTitle = hasPet ? "飼主性別" : "祈福人性別";
            string birthTitle = hasPet ? "飼主農曆生日" : "農曆生日";
            string birthtimeTitle = hasPet ? "飼主農曆時辰" : "農曆時辰";
            string sbirthTitle = hasPet ? "飼主國曆生日" : "國曆生日";
            string addressTitle = hasPet ? "飼主居住地址" : "居住地址";

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content(ownerTitle, item.Name)}
                        {addData_Content(mobileTitle, item.Mobile)}
                        {addData_Content(sexTitle, item.Sex)}
                        {addData_Content(birthTitle, item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content(birthtimeTitle, item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content(addressTitle, item.Address)}
                        {addData_Content("寵物姓名", item.PetName)}
                        {addData_Content("寵物品種", item.PetType)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 鹿港城隍廟
        /// </summary>
        public static string Lights_Lk(ApplicantResult item)
        {
            // 判斷寄回方式
            bool hasSendback = item.Sendback == "Y" || item.AppSendback == "Y";

            // 收件人資訊
            string rBlock = hasSendback
                ? $@"
                    {addData_Content("收件人姓名", item.rName)}
                    {addData_Content("收件人電話", item.rMobile)}
                    {addData_Content("收件人地址", item.rAddress)}"
                : string.Empty;

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人市話", item.Homenum)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("贈品處理方式", hasSendback ? "Y" : "N")}
                        {rBlock}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 玉敕大樹朝天宮
        /// </summary>
        public static string Lights_ma(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 進寶財神廟
        /// </summary>
        public static string Lights_jb(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 台灣道教總廟無極三清總道院
        /// </summary>
        public static string Lights_wjsan(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 桃園龍德宮
        /// </summary>
        public static string Lights_ld(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 基隆悟玄宮
        /// </summary>
        public static string Lights_wh(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 點燈模板 - 松柏嶺受天宮
        /// </summary>
        public static string Lights_st(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region －－普度－－

        #endregion

        #region －－文創小舖－－

        /// <summary>
        /// 文創模板
        /// </summary>
        public static string Product_defult(ApplicantResult item)
        {
            string productlist = string.Empty;

            if (item.Product_Count > 0)
            {
                productlist = $@"
                            {addData_Content("商品名稱", item.Product_Title)}
                            {addData_Content("商品類別", item.Product_ItemName)}
                            {addData_Content("商品金額", item.Product_Cost)}
                            {addData_Content("商品數量", item.Product_Count.ToString())}
                            {addData_Content("商品圖片", "<img src=\"" + item.Product_Img + "\" style=\"width: 50%\" alt=\"" + item.Product_Title + "\" title=\"" + item.Product_Title + "\">")}";
            }

            return $@"
                    <li>
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("購買人", item.AppName)}
                        {addData_Content("連絡電話", item.AppMobile)}
                        {addData_Content("收件地址", item.Address)}
                        {productlist}
                    </li>";
        }

        /// <summary>
        /// 文創模板 - 繞境商品
        /// </summary>
        public static string Product_Pilgrimage(ApplicantResult item)
        {
            string productlist = string.Empty;

            if (item.Product_Count > 0)
            {
                productlist = $@"
                            {addData_Content("商品名稱", item.Product_Title)}
                            {addData_Content("商品類別", item.Product_ItemName)}
                            {addData_Content("商品金額", item.Product_Cost)}
                            {addData_Content("商品數量", item.Product_Count.ToString())}
                            {addData_Content("商品圖片", "<img src=\"" + item.Product_Img + "\" style=\"width: 50%\" alt=\"" + item.Product_Title + "\" title=\"" + item.Product_Title + "\">")}";
            }

            return $@"
                    <li>
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("購買人", item.AppName)}
                        {addData_Content("連絡電話", item.AppMobile)}
                        {addData_Content("收件地址", item.Address)}
                        {productlist}
                    </li>";
        }

        /// <summary>
        /// 文創模板 - 錢母擺件小舖
        /// </summary>
        public static string Product_MoneyMother(ApplicantResult item, List<ProductMoneymotherItem> productList)
        {
            // === 商品區塊 ===
            string productBlock = string.Empty;

            foreach (var p in productList)
            {
                if (p.Count <= 0) continue;

                productBlock += $@"
                                    {addData_Content("商品代碼", p.Code)}
                                    {addData_Content("商品名稱", p.Name)}
                                    {addData_Content("商品金額", p.Cost.ToString("N0"))}
                                    {addData_Content("商品數量", p.Count.ToString())}
                                    {addData_Content("商品小計", p.SubTotal.ToString("N0"))}
                                    {addData_Content("商品圖片", $"<img src=\"{p.ImageUrl}\" style=\"width:50%;\" alt=\"{p.Name}\" />")}
                                ";
            }

            //if (item.Product_Count > 0)
            //{
            //    productlist = $@"
            //                {addData_Content("商品名稱", item.Product_Title)}
            //                {addData_Content("商品類別", item.Product_ItemName)}
            //                {addData_Content("商品金額", item.Product_Cost)}
            //                {addData_Content("商品數量", item.Product_Count.ToString())}
            //                {addData_Content("商品圖片", "<img src=\"" + item.Product_Img + "\" style=\"width: 50%\" alt=\"" + item.Product_Title + "\" title=\"" + item.Product_Title + "\">")}";
            //}

            return $@"
                    <li>
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("購買人", item.AppName)}
                        {addData_Content("連絡電話", item.AppMobile)}
                        {addData_Content("收件地址", item.Address)}
                    </li>";
        }

        #endregion

        #region －－法會－－

        /// <summary>
        /// 法會模板
        /// </summary>
        public static string Supplies_defult(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 法會模板 - 補財庫 - 鹿港城隍廟
        /// </summary>
        public static string Supplies_Lk(ApplicantResult item)
        {
            // 判斷寄回方式
            bool hasSendback = item.Sendback == "Y" || item.AppSendback == "Y";

            // 收件人資訊
            string rBlock = hasSendback
                ? $@"
            {addData_Content("收件人姓名", item.rName)}
            {addData_Content("收件人電話", item.rMobile)}
            {addData_Content("收件人地址", item.rAddress)}"
                : string.Empty;

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人市話", item.Homenum)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("贈品處理方式", hasSendback ? "Y" : "N")}
                        {rBlock}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 法會模板 - 補財庫 - 斗六五路財神宮
        /// </summary>
        public static string Supplies_Fw(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 法會模板 - 天赦日祭改、財神賜福-消災補庫法會 - 進寶財神廟
        /// </summary>
        public static string Supplies_jb(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("收件人姓名", item.rName)}
                        {addData_Content("收件人地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 法會模板 - 天赦日招財補運 - 北港武德宮
        /// </summary>
        public static string Supplies_ma(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人市話", item.Homenum)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 法會模板 - 桃園威天宮
        /// 4-天赦日招財補運
        /// 7-招財補運
        /// 8-招財補運九九重陽升級版
        /// 20-天公生招財補運
        /// </summary>
        public static string Supplies_ty(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人國曆生日", item.AppsBirth)}
                        {addData_Content("購買人居住地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region －－關聖帝君聖誕－－

        /// <summary>
        /// 關聖帝君聖誕模板 - 桃園威天宮
        /// </summary>
        public static string EmperorGuansheng_ty(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人國曆生日", item.AppsBirth)}
                        {addData_Content("購買人居住地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region －－靈寶禮斗－－

        /// <summary>
        /// 靈寶禮斗模板 - 玉敕大樹朝天宮
        /// </summary>
        public static string Lingbaolidou_ma(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人居住地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region －－七朝清醮－－

        /// <summary>
        /// 七朝清醮模板 - 大甲鎮瀾宮
        /// </summary>
        public static string TaoistJiaoCeremony_da(ApplicantResult item)
        {
            string Infolist = string.Empty;
            switch (item.ServiceType)
            {
                case "1":
                    Infolist = $@"
                                {addData_Content("祈福人姓名2", item.Name2)}
                                {addData_Content("祈福人電話", item.Mobile)}
                                {addData_Content("居住地址", item.Address)}
                                {addData_Content("寄送方式", item.Sendback == "Y" ? "寄回(加收運費250元)" : "不寄回(會轉送給弱勢團體)")}";

                    if (item.Sendback == "Y")
                    {
                        Infolist += $@"
                                    {addData_Content("收件人姓名", item.rName)}
                                    {addData_Content("收件人電話", item.rMobile)}
                                    {addData_Content("收件人地址", item.rAddress)}";
                    }
                    break;
                case "4":
                    Infolist = $@"  
                                {addData_Content("祈福人姓名2", item.Name2)}
                                {addData_Content("祈福人姓名3", item.Name3)}
                                {addData_Content("祈福人姓名4", item.Name4)}
                                {addData_Content("祈福人姓名5", item.Name5)}
                                {addData_Content("祈福人姓名6", item.Name6)}";
                    break;
                case "5":
                    Infolist = $@"
                                {addData_Content("祈福人姓名2", item.Name2)}
                                {addData_Content("祈福人電話", item.Mobile)}
                                {addData_Content("居住地址", item.Address)}";
                    break;
                case "6":
                    Infolist = $@"
                                {addData_Content("祈福人姓名2", item.Name2)}
                                {addData_Content("祈福人電話", item.Mobile)}
                                {addData_Content("居住地址", item.Address)}";
                    break;
                case "7":
                    Infolist = $@"
                                {addData_Content("祈福人姓名2", item.Name2)}
                                {addData_Content("祈福人電話", item.Mobile)}
                                {addData_Content("居住地址", item.Address)}";
                    break;
                default:
                    Infolist = $@"
                                {addData_Content("祈福人電話", item.Mobile)}
                                {addData_Content("居住地址", item.Address)}";
                    break;
            }

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人居住地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {Infolist}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region －－護國息災梁皇大法會－－

        /// <summary>
        /// 護國息災梁皇大法會模板
        /// </summary>
        public static string Lybc_dh(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region —— 安斗 —— 

        /// <summary>
        /// 安斗模板 - 斗六五路財神宮
        /// </summary>
        public static string AnDou_Fw(ApplicantResult item)
        {
            // 判斷是否有寵物資料
            bool hasPet = !string.IsNullOrWhiteSpace(item.PetName);

            // 動態欄位名稱
            string ownerTitle = hasPet ? "飼主姓名" : "祈福人姓名";
            string mobileTitle = hasPet ? "飼主電話" : "祈福人電話";
            string sexTitle = hasPet ? "飼主性別" : "祈福人性別";
            string birthTitle = hasPet ? "飼主農曆生日" : "農曆生日";
            string birthtimeTitle = hasPet ? "飼主農曆時辰" : "農曆時辰";
            string sbirthTitle = hasPet ? "飼主國曆生日" : "國曆生日";
            string addressTitle = hasPet ? "飼主居住地址" : "居住地址";

            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content(ownerTitle, item.Name)}
                        {addData_Content(mobileTitle, item.Mobile)}
                        {addData_Content(sexTitle, item.Sex)}
                        {addData_Content(birthTitle, item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content(birthtimeTitle, item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content(addressTitle, item.Address)}
                        {addData_Content("寵物姓名", item.PetName)}
                        {addData_Content("寵物品種", item.PetType)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        /// <summary>
        /// 安斗模板 - 台灣道教總廟無極三清總道院
        /// </summary>
        public static string AnDou_wjsan(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region —— 供花供果 —— 

        /// <summary>
        /// 供花供果模板 - 台灣道教總廟無極三清總道院
        /// </summary>
        public static string Huaguo_wjsan(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("祈福人信箱", item.Email)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region —— 孝親祈福燈 —— 

        /// <summary>
        /// 孝親祈福燈模板 - 桃園威天宮
        /// </summary>
        public static string Lights_ty_mom(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("購買人國曆生日", item.AppsBirth)}
                        {addData_Content("購買人居住地址", item.AppAddress)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        #region —— 祈安植福 —— 

        /// <summary>
        /// 祈安植福模板 - 松柏嶺受天宮
        /// </summary>
        public static string Blessing_st(ApplicantResult item)
        {
            return $@"
                    <li>
                        {addData_Content("購買人姓名", item.AppName)}
                        {addData_Content("購買人電話", item.AppMobile)}
                        {addData_Content("購買人信箱", item.AppEmail)}
                        {addData_Content("宮廟名稱", item.TempleName)}
                        {addData_Content("訂單編號", item.Num2String)}
                        {addData_Content("祈福人姓名", item.Name)}
                        {addData_Content("祈福人電話", item.Mobile)}
                        {addData_Content("祈福人性別", item.Sex)}
                        {addData_Content("農曆生日", item.Birth + (item.LeapMonth == "Y" ? " 閏月" : ""))}
                        {addData_Content("農曆時辰", item.BirthTime)}
                        {addData_Content("國曆生日", item.sBirth)}
                        {addData_Content("居住地址", item.Address)}
                        {addData_Content("服務項目", item.ServiceString)}
                        {addData_Content("金額", item.Cost)}
                        {addData_Content("狀態", item.StatusString)}
                        {addData_Content("受理日期", item.ChargeDate)}
                        {addData_Content("備註", item.Remark)}
                    </li>";
        }

        #endregion

        /// <summary>
        /// 組合資料欄位顯示 HTML（若 Value 為空、null 或僅空白則不輸出）
        /// </summary>
        /// <param name="Name">欄位標題</param>
        /// <param name="Value">欄位內容</param>
        /// <returns>HTML 片段字串</returns>
        public static string addData_Content(string Name, string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
                return string.Empty;

            return $"<div><div>{Name}</div><div>{Value}</div></div>";
        }

    }
}