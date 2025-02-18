using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheEventTitbits
    {
        private const string _keyPrefix = "EventTitbits-";
        protected static readonly object _locker = new object();
        protected static readonly object _locker2 = new object();

        public static List<EventTitbits> GetList(int topRows)
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();
            lock (_locker)
            {
                var result = new List<EventTitbits>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    string key = _keyPrefix + "HOME";
                    List<EventTitbits> obj = cache[key] as List<EventTitbits>;

                    if (obj == null)
                    {
                        obj = new EventTitbitsDAC().SelectActive(topRows);
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
        public static List<EventTitbits> GetListByEveintId(int eventId)
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();

            lock (_locker2)
            {
                var result = new List<EventTitbits>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    string key = _keyPrefix + eventId.ToString();
                    List<EventTitbits> obj = cache[key] as List<EventTitbits>;

                    if (obj == null)
                    {
                        obj = new EventTitbitsDAC().SelectByEventId(eventId);
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