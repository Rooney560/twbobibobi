using System;
using System.Collections.Generic;
using System.Runtime.Caching;

using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheCarousel
    {
        private const string _key = "Carousel-";
        //private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<Carousel> GetList(string groupName)
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();
           
            lock (_locker)
            {
                var result = new List<Carousel>();
                try
                {
                    string key = _key + groupName;
                    ObjectCache cache = MemoryCache.Default;
                    List<Carousel> obj = cache[key] as List<Carousel>;

                    if (obj == null)
                    {
                        obj = new CarouselDAC().SelectActive(groupName);
                        cache.Set(key, obj, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(cacheMinutes) });
                    }

                    result = obj;
                }
                catch (Exception ex)
                {
                }
                return result;
            }
        }
    }
}