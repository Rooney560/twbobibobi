using twbobibobi.Data;
using Read.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twbobibobi.FET.Data;

namespace twbobibobi.FET.Processors
{
    /// <summary>
    /// Class: TempleOrderProcessorFactory
    /// 負責根據宮廟編號 AdminID 回傳對應的 ITempleOrderProcessor 實例
    /// 支援動態調整年度 (Year)，可依據服務種類 (Kind) 與跨年時間自動切換。
    /// </summary>
    public class TempleOrderProcessorFactory : ITempleOrderProcessorFactory
    {
        private readonly BasePage _basePage;

        /// <summary>
        /// 當前使用的年度 (可由外部或內部動態更新)
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 定義各服務 (Kind) 的跨年切換日期，例如：
        /// 點燈 (Kind=1) 在 2025/11/01 起視為下一年度。
        /// </summary>
        private readonly Dictionary<int, DateTime> _yearChangeDate = new Dictionary<int, DateTime>
        {
            { 1, new DateTime(2025, 11, 1) } // 點燈
            ,{ 20, new DateTime(2025, 11, 1) } // 安斗
        };

        /// <summary>
        /// 用於快取已建立的 Processor 實例（避免重複建立）。
        /// </summary>
        private readonly Dictionary<int, ITempleOrderProcessor> _processorCache = new Dictionary<int, ITempleOrderProcessor>();


        private readonly Dictionary<int, ITempleOrderProcessor> _processors;

        /// <summary>
        /// 初始化 TempleOrderProcessorFactory。
        /// </summary>
        /// <param name="basePage">Web BasePage 實例。</param>
        /// <param name="year">初始年度。</param>
        public TempleOrderProcessorFactory(BasePage basePage, string year)
        {
            _basePage = basePage;
            Year = year;
        }

        /// <summary>
        /// 根據傳入的 ServiceID (Kind) 自動更新年度。
        /// 若跨越指定的切換日期，則自動進入下一年度。
        /// </summary>
        /// <param name="serviceId">服務代碼 (Kind)</param>
        public void UpdateYearByService(int serviceId)
        {
            if (Year != "TEST")
            {
                string currentYear = DateTime.Now.Year.ToString();

                if (_yearChangeDate.ContainsKey(serviceId))
                {
                    DateTime switchDate = _yearChangeDate[serviceId];
                    Year = (DateTime.Now >= switchDate)
                        ? (DateTime.Now.Year + 1).ToString()
                        : currentYear;
                }
                else
                {
                    Year = currentYear;
                }
            }
        }

        /// <summary>
        /// 根據 AdminID 取得對應的 Processor 實例。
        /// 若尚未建立，則動態建立並快取。
        /// </summary>
        /// <param name="adminId">宮廟代碼。</param>
        /// <returns>對應的 ITempleOrderProcessor。</returns>
        /// <exception cref="NotSupportedException">當 AdminID 不在定義清單內時拋出。</exception>
        public ITempleOrderProcessor GetProcessor(int adminId)
        {
            if (_processorCache.ContainsKey(adminId))
            {
                return _processorCache[adminId];
            }

            ITempleOrderProcessor processor;

            switch (adminId)
            {
                case 3:
                    // 大甲鎮瀾宮
                    processor = new DajiaOrderProcessor(_basePage, Year); break;
                case 4:
                    // 新港奉天宮
                    processor = new HsinkangOrderProcessor(_basePage, Year); break;
                case 5:
                    // 新港奉天宮商品販賣小舖
                    processor = new ProductOrderProcessor(_basePage, 5, Year); break;
                case 6:
                    // 北港武德宮
                    processor = new WudeOrderProcessor(_basePage, Year); break;
                case 8:
                    // 西螺福興宮
                    processor = new FusingOrderProcessor(_basePage, Year); break;
                case 10:
                    // 台南正統鹿耳門聖母廟
                    processor = new LuermenOrderProcessor(_basePage, Year); break;
                case 14:
                    // 桃園威天宮
                    processor = new TywtgOrderProcessor(_basePage, Year); break;
                case 15:
                    // 斗六五路財神宮
                    processor = new FivewayOrderProcessor(_basePage, Year); break;
                case 16:
                    // 台東東海龍門天聖宮
                    processor = new DonghaimazuOrderProcessor(_basePage, Year); break;
                case 20:
                    // 西螺福興宮商品販賣小舖
                    processor = new ProductOrderProcessor(_basePage, 20, Year); break;
                case 21:
                    // 鹿港城隍廟
                    processor = new LukanggodOrderProcessor(_basePage, Year); break;
                case 22:
                    // 流金富貴商品販賣小舖
                    processor = new ProductOrderProcessor(_basePage, 22, Year); break;
                case 23:
                    // 玉敕大樹朝天宮
                    processor = new DsctgOrderProcessor(_basePage, Year); break;
                case 28:
                    // 財神小舖商品販賣小舖
                    processor = new ProductOrderProcessor(_basePage, 28, Year); break;
                case 29:
                    // 進寶財神廟
                    processor = new JinbaoOrderProcessor(_basePage, Year); break;
                case 31:
                    // 台灣道教總廟無極三清總道院
                    processor = new WjsanOrderProcessor(_basePage, Year); break;
                case 32:
                    // 桃園龍德宮
                    processor = new Ld4mOrderProcessor(_basePage, Year); break;
                case 33:
                    // 神霄玉府財神會館
                    processor = new ShenxiaoOrderProcessor(_basePage, Year); break;
                case 34:
                    // 基隆悟玄宮
                    processor = new WuxuanOrderProcessor(_basePage, Year); break;
                case 35:
                    // 松柏嶺受天宮
                    processor = new ShoutianOrderProcessor(_basePage, Year); break;
                case 36:
                    // 中寮石龍宮
                    processor = new ShilongOrderProcessor(_basePage, Year); break;
                case 38:
                    // 北極玄天宮
                    processor = new BeijitaitungOrderProcessor(_basePage, Year); break;
                case 39:
                    // 慈惠石壁部堂
                    processor = new ShibibutangOrderProcessor(_basePage, Year); break;
                case 40:
                    // 真武山受玄宮
                    processor = new BopeeyouOrderProcessor(_basePage, Year); break;
                case 41:
                    // 壽山巖觀音寺
                    processor = new ShoushanyanOrderProcessor(_basePage, Year); break;
                default:
                    throw new NotSupportedException($"不支援的宮廟 AdminID={adminId}");
            }

            _processorCache[adminId] = processor;
            return processor;
        }
    }
}