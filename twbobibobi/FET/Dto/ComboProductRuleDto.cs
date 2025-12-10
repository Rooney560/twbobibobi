/**************************************************************************************************
 * 專案名稱：twbobibobi
 * 檔案名稱：ComboProductRuleDto.cs
 * 類別說明：組合商品規則 DTO，對應 ComboProductRules 表，用於組合商品展開時的資料傳遞
 * 建立日期：2025-12-09
 * 建立人員：Rooney
 * 修改記錄：2025-12-09 - 建立 ComboProductRuleDto 資料模型
 *
 * 目前維護人員：Rooney
 **************************************************************************************************/


namespace twbobibobi.FET.Dto
{
    /// <summary>
    /// DTO：組合商品規則，用於描述組合商品展開的每一個子項目。
    /// 一筆 ComboProductRuleDto 代表拆分後的一項商品定義。
    /// </summary>
    public class ComboProductRuleDto
    {
        /// <summary>
        /// 規則主鍵 ID（對應資料表 ComboProductRules.RuleID）。
        /// </summary>
        public int RuleID { get; set; }

        /// <summary>
        /// 遠傳產品表代碼（CodeID）。決定展開商品屬於哪一個產品。
        /// </summary>
        public int CodeID { get; set; }

        /// <summary>
        /// 宮廟代碼（AdminID）。決定展開商品屬於哪一個宮廟。
        /// </summary>
        public int AdminID { get; set; }

        /// <summary>
        /// 主商品或文創商品的 ServiceID(Kind)。
        /// 例如：1 = 點燈、3 = 文創小販部。
        /// </summary>
        public int ServiceID { get; set; }

        /// <summary>
        /// 類型（TypeID），用於對應 TempleCode.TypeID，例如服務項目分類 (LightsType、PurdueType、SuppliesType)等。
        /// </summary>
        public string TypeID { get; set; }

        /// <summary>
        /// 組合商品的 ProductCode（例：appsspcombo01）。
        /// 系統會依此識別是哪一種組合商品。
        /// </summary>
        public string ComboProductCode { get; set; }

        /// <summary>
        /// 價格來源：
        /// Fixed   = 使用固定價格（需填 FixedPrice）
        /// LightsCost = 依照點燈價格邏輯動態計算
        /// PurdueCost = 依照普度價格邏輯動態計算
        /// Formula = 計算剩餘金額（Total - 已分配金額）
        /// </summary>
        public string PriceSource { get; set; }

        /// <summary>
        /// 固定價格金額，當 PriceSource = Fixed 時有效。
        /// 若 PriceSource 非 Fixed，則本欄位可為 NULL。
        /// </summary>
        public int? FixedPrice { get; set; } = 0;

        /// <summary>
        /// 展開順序。決定組合商品各子項目的排列順序（1 為第一個）。
        /// </summary>
        public int SortOrder { get; set; } = 1;

        /// <summary>
        /// 備註，可用於描述此規則的用途或其他資訊。
        /// </summary>
        public string Remark { get; set; }
    }
}