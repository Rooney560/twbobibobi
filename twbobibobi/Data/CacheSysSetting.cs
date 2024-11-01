using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheSysSetting
    {
        private const string _key = "SysSetting-";
        private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<SysSetting> GetList(string groupName)
        {
            lock (_locker)
            {
                var result = new List<SysSetting>();
                try
                {
                    string key = _key + groupName;
                    ObjectCache cache = MemoryCache.Default;
                    List<SysSetting> obj = cache[key] as List<SysSetting>;

                    if (obj == null)
                    {
                        obj = new SysSettingDAC().SelectActive(groupName);
                        cache.Set(key, obj, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(_cacheMinutes) });
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