/* ===================================================================================================
   專案名稱：twbobibobi
   檔案名稱：AllowanceUsageContext.cs
   類別說明：折讓使用狀態 Context，用於追蹤「已折讓的項目數」，並判斷本次折讓是否為最後一次折讓（避免作廢、跳號造成誤判）。
   建立日期：2025-12-23
   建立人員：Rooney

   目前維護人員：Rooney
   =================================================================================================== */

using System;

namespace twbobibobi.Model.Allowance
{
    /// <summary>
    /// 折讓使用狀態 Context。
    /// <para>
    /// 本 Context 專責處理「折讓項目數」的累積與判斷，
    /// 用於解決以下實務問題：
    /// <list type="bullet">
    /// <item>折讓單編號可能因作廢而跳號（1,2,4,7...）</item>
    /// <item>不可再依賴折讓單編號 index 判斷是否為最後一次</item>
    /// <item>廠商 API 僅能依「折讓單編號」逐筆查詢</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <remarks>
    /// 設計原則：
    /// <para>
    /// 1. 只統計「已成功開立的折讓單（D0401 / 99）」  
    /// 2. 只累加該折讓單中的 ProductItem 數量  
    /// 3. 不關心金額、不呼叫 API、不知道折讓單編號  
    /// </para>
    /// </remarks>
    public sealed class AllowanceUsageContext
    {
        /// <summary>
        /// 原始訂單可折讓的總項目數（通常為訂單明細筆數）。
        /// </summary>
        public int TotalItemCount { get; }

        /// <summary>
        /// 歷史已折讓的項目數（僅統計成功折讓 D0401 / 99）。
        /// </summary>
        public int UsedItemCount { get; private set; }

        /// <summary>
        /// 建立折讓使用狀態 Context。
        /// </summary>
        /// <param name="totalItemCount">
        /// 原始訂單可折讓的總項目數。
        /// </param>
        public AllowanceUsageContext(int totalItemCount)
        {
            if (totalItemCount <= 0)
                throw new ArgumentException("TotalItemCount 必須大於 0", nameof(totalItemCount));

            TotalItemCount = totalItemCount;
            UsedItemCount = 0;
        }

        /// <summary>
        /// 累加歷史折讓單所使用的項目數。
        /// <para>
        /// 僅在折讓單狀態為 D0401 / 99 時呼叫。
        /// </para>
        /// </summary>
        /// <param name="itemCount">
        /// 該折讓單中的 ProductItem 數量。
        /// </param>
        public void ApplyHistoricalItems(int itemCount)
        {
            if (itemCount <= 0)
                return;

            UsedItemCount += itemCount;
        }

        /// <summary>
        /// 判斷「在套用本次折讓項目數後」是否已達或超過可折讓總數，
        /// 以此判定本次是否為最後一次折讓。
        /// </summary>
        /// <param name="currentItemCount">
        /// 本次折讓欲使用的項目數。
        /// </param>
        /// <returns>
        /// true：本次為最後一次折讓  
        /// false：仍有剩餘可折讓項目
        /// </returns>
        public bool IsLastAllowanceAfter(int currentItemCount)
        {
            if (currentItemCount <= 0)
                return false;

            return UsedItemCount + currentItemCount >= TotalItemCount;
        }
    }
}
