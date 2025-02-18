using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheEvent
    {
        private const string _keyPrefix = "Event";
        protected static readonly object _locker = new object();
        public static List<Event> GetList()
        {
            int cacheMinutes = CacheSysSetting.GetCacheMinutes();

            lock (_locker)
            {
                var result = new List<Event>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    string key = _keyPrefix;
                    List<Event> obj = cache[key] as List<Event>;

                    if (obj == null)
                    {
                        obj = new EventDAC().SelectActive();
                        var eventTitbitsDAC = new EventTitbitsDAC();
                        EventTitbits et;
                        foreach (var item in obj)
                        {
                            et = eventTitbitsDAC.SelectFirst(item.Id);
                            if(et != null)
                            {
                                item.FirstResourceId = et.Id;
                                item.FirstResourceImageType = et.ImageFileType;
                            }
                        }
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