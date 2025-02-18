using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace twbobibobi.Data
{
    public class CacheSeoTag
    {
        private const string _key = "SeoTag-";
        //private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<string> GetList(string page)
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();
            lock (_locker)
            {
                var result = new List<string>();
                try
                {
                    string key = _key + page;
                    ObjectCache cache = MemoryCache.Default;
                    List<string> obj = cache[key] as List<string>;

                    if (obj == null)
                    {
                        obj = new SeoTagDAC().SelectActive(page);
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