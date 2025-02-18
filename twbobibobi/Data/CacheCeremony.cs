using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheCeremony
    {
        private const string _key = "Ceremony";
        //private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<Ceremony> GetList()
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();
            lock (_locker)
            {
                var result = new List<Ceremony>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    List<Ceremony> obj = cache[_key] as List<Ceremony>;

                    if (obj == null)
                    {
                        obj = new CeremonyDAC().SelectActive();
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