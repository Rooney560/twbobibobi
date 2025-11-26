using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// File: ITempleOrderProcessorFactory.cs
    /// Namespace: twbobibobi.FET.Processors
    /// 定義工廠介面，用於根據宮廟代碼 (AdminID) 取得對應的訂單處理器。
    /// 並支援跨年度切換邏輯。
    /// </summary>
    public interface ITempleOrderProcessorFactory
    {
        /// <summary>
        /// 目前工廠所使用的年度。
        /// 可由外部指定或由內部方法 <see cref="UpdateYearByService"/> 動態調整。
        /// </summary>
        string Year { get; set; }

        /// <summary>
        /// 根據宮廟編號 AdminID 取得對應的訂單處理策略實例。
        /// </summary>
        /// <param name="adminId">宮廟編號例如：3=大甲鎮瀾宮、14=桃園威天宮）</param>
        /// <returns>對應的 <see cref="ITempleOrderProcessor"/> 實例。</returns>
        ITempleOrderProcessor GetProcessor(int adminId);

        /// <summary>
        /// 根據服務種類 (ServiceID / Kind) 自動判斷是否需要切換年度。
        /// 例如：點燈服務 (Kind=1) 在 2025/11/01 之後應自動轉為 2026 年度。
        /// </summary>
        /// <param name="serviceId">服務代碼 (Kind)。</param>
        void UpdateYearByService(int serviceId);
    }
}