using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Temple.data;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheLight
    {
        private const string _key = "Light";
        //private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<Light> GetList()
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();
            lock (_locker)
            {
                var result = new List<Light>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    List<Light> obj = cache[_key] as List<Light>;

                    if (obj == null)
                    {
                        obj = new LightsDAC().SelectActive();
                        cache.Set(_key, obj, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(cacheMinutes) });
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