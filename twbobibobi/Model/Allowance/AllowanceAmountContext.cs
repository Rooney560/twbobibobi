/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceAmountContext.cs
   類別說明：
       折讓金額計算上下文物件（Allowance Amount Context）。
       用於計算「第 N 次折讓」時，剩餘可折讓的未稅金額與營業稅額，
       並確保所有折讓累積總額不超過原始發票金額。

   建立日期：2025-12-22
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System;

namespace twbobibobi.Model.Allowance
{
    /// <summary>
    /// 折讓金額計算上下文。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 本類別用於「單一原始發票」的折讓計算流程，負責：
    /// </para>
    /// <list type="number">
    /// <item>保存原始發票的未稅金額與營業稅額</item>
    /// <item>累積已折讓的未稅金額與營業稅額</item>
    /// <item>計算目前剩餘可折讓金額</item>
    /// <item>驗證第 N 次折讓不會使折讓總金額超過原發票</item>
    /// </list>
    ///
    /// <para>
    /// 注意：
    /// </para>
    /// <para>
    /// 此 Context 僅負責「金額邏輯與驗證」，不進行：
    /// </para>
    /// <list type="bullet">
    /// <item>資料庫存取</item>
    /// <item>廠商 API 呼叫</item>
    /// <item>折讓單號碼產生</item>
    /// </list>
    ///
    /// <para>
    /// 適用情境：
    /// </para>
    /// <list type="bullet">
    /// <item>全額折讓（一次完成）</item>
    /// <item>部分折讓（多次逐筆折讓）</item>
    /// </list>
    /// </remarks>
    public sealed class AllowanceAmountContext
    {
        /// <summary> 原始發票未稅金額。 </summary>
        public decimal OriginalSalesAmount { get; }

        /// <summary> 原始發票營業稅額。 </summary>
        public decimal OriginalTaxAmount { get; }

        /// <summary> 原始發票含稅總金額。 </summary>
        public decimal OriginalTotalAmount => OriginalSalesAmount + OriginalTaxAmount;

        /// <summary> 已折讓的未稅金額累計。 </summary>
        public decimal RefundedSalesAmount { get; private set; }

        /// <summary> 已折讓的營業稅額累計。 </summary>
        public decimal RefundedTaxAmount { get; private set; }

        /// <summary> 原始發票含稅總金額。 </summary>
        public decimal RefundedTotalAmount => RefundedSalesAmount + RefundedTaxAmount;

        /// <summary> 目前剩餘可折讓的未稅金額。 </summary>
        public decimal RemainingSalesAmount => OriginalSalesAmount - RefundedSalesAmount;

        /// <summary> 目前剩餘可折讓的營業稅額。 </summary>
        public decimal RemainingTaxAmount => OriginalTaxAmount - RefundedTaxAmount;

        /// <summary>
        /// 建立折讓金額計算上下文。
        /// </summary>
        /// <param name="originalSalesAmount">原始發票未稅金額</param>
        /// <param name="originalTaxAmount">原始發票營業稅額</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// 當金額為負數時拋出
        /// </exception>
        public AllowanceAmountContext(decimal originalSalesAmount, decimal originalTaxAmount)
        {
            if (originalSalesAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(originalSalesAmount));
            if (originalTaxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(originalTaxAmount));

            OriginalSalesAmount = originalSalesAmount;
            OriginalTaxAmount = originalTaxAmount;
            RefundedSalesAmount = 0m;
            RefundedTaxAmount = 0m;
        }

        /// <summary>
        /// 嘗試套用本次折讓金額，並驗證累積後是否超過原發票。
        /// </summary>
        /// <param name="salesAmount">本次折讓的未稅金額</param>
        /// <param name="taxAmount">本次折讓的營業稅額</param>
        /// <param name="errorMessage">
        /// 若失敗，回傳錯誤原因；
        /// 成功時為空字串
        /// </param>
        /// <returns>
        /// true：折讓金額合法，已成功累加  
        /// false：折讓金額超出原發票可折讓範圍
        /// </returns>
        /// <remarks>
        /// 驗證規則：
        /// <list type="bullet">
        /// <item>單次折讓金額不可為負數</item>
        /// <item>累積折讓未稅金額不可超過原始發票未稅金額</item>
        /// <item>累積折讓營業稅額不可超過原始發票營業稅額</item>
        /// </list>
        ///
        /// 本方法僅負責金額層級驗證，
        /// 是否符合廠商 API 規範（例如只驗證總金額）應由上層處理。
        /// </remarks>
        public bool TryApplyAllowance(
            decimal salesAmount,
            decimal taxAmount,
            out string errorMessage)
        {
            errorMessage = string.Empty;

            decimal nextSales = RefundedSalesAmount + salesAmount;
            decimal nextTax = RefundedTaxAmount + taxAmount;

            if (salesAmount < 0 || taxAmount < 0)
            {
                errorMessage = $"折讓金額不可為負數。折讓未稅金額：{salesAmount}，折讓營業稅額：{taxAmount}。";
                return false;
            }

            if (nextSales > OriginalSalesAmount)
            {
                errorMessage = $"累積折讓未稅金額已超過原始發票金額。累積折讓未稅金額：{nextSales}，原始發票未稅金額：{OriginalSalesAmount}";
                return false;
            }

            if (nextTax > OriginalTaxAmount)
            {
                errorMessage = $"累積折讓營業稅額已超過原始發票營業稅額。累積折讓未稅金額：{nextTax}，原始發票未稅金額：{OriginalTaxAmount}";
                return false;
            }

            RefundedSalesAmount = nextSales;
            RefundedTaxAmount = nextTax;
            return true;
        }

        /// <summary>
        /// 計算本次折讓可使用的金額（第 N 次折讓）。
        /// </summary>
        /// <param name="requestedSalesAmount"> 本次希望折讓的未稅金額（例如單筆商品金額）</param>
        /// <param name="requestedTaxAmount"> 本次希望折讓的營業稅額 </param>
        /// <param name="finalSalesAmount"> 實際可折讓的未稅金額（會自動扣除前次累積）</param>
        /// <param name="finalTaxAmount"> 實際可折讓的營業稅額 </param>
        /// <param name="errorMessage"> 若失敗，回傳錯誤原因 </param>
        /// <returns>
        /// true：成功計算本次折讓金額  
        /// false：本次折讓會導致總金額超過原始發票
        /// </returns>
        public bool TryResolveNextAllowanceAmount(
            decimal requestedSalesAmount,
            decimal requestedTaxAmount,
            out decimal finalSalesAmount,
            out decimal finalTaxAmount,
            out string errorMessage)
        {
            errorMessage = string.Empty;
            finalSalesAmount = 0m;
            finalTaxAmount = 0m;

            if (requestedSalesAmount < 0 || requestedTaxAmount < 0)
            {
                errorMessage = "折讓金額不可為負數。";
                return false;
            }

            // 剩餘可折讓金額
            var remainSales = RemainingSalesAmount; // 1140-495=591
            var remainTax = RemainingTaxAmount; // 54-25=29

            // 本次實際可折讓金額 = 取較小者
            finalSalesAmount = Math.Min(requestedSalesAmount, remainSales);
            finalTaxAmount = Math.Min(requestedTaxAmount, remainTax);

            // 再次驗證（防呆）
            if (RefundedSalesAmount + finalSalesAmount > OriginalSalesAmount ||
                RefundedTaxAmount + finalTaxAmount > OriginalTaxAmount)
            {
                errorMessage = "本次折讓將導致折讓總金額超過原始發票。";
                return false;
            }

            // 累積進 Context
            //RefundedSalesAmount += finalSalesAmount;
            //RefundedTaxAmount += finalTaxAmount;

            return true;
        }

        /// <summary>
        /// 取得「最後一次折讓」可用的剩餘金額（用於補齊）。
        /// </summary>
        public (decimal Sales, decimal Tax) GetRemaining()
        {
            return (
                OriginalSalesAmount - RefundedSalesAmount,
                OriginalTaxAmount - RefundedTaxAmount
            );
        }
    }
}