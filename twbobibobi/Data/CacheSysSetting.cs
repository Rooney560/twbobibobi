using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;

namespace twbobibobi.Data
{
    public class CacheSysSetting
    {
        private const string _keyPrefix = "SysSetting";
        private static int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        protected static readonly object _locker2 = new object();
        protected static readonly object _locker3 = new object();

        public static int GetCacheMinutes()
        {
            lock (_locker2)
            {
                //try
                //{
                string item = "cache_minutes";
                string key = $"{_keyPrefix}-{item}";
                ObjectCache cache = MemoryCache.Default;
                object obj = cache[key];

                if (obj == null)
                {
                    int res;
                    var sysSetting = new SysSettingDAC().SelectByPK(item);
                    if (sysSetting == null)
                        res = 10; //default value
                    else
                    {
                        if (!int.TryParse(sysSetting.Value, out res))
                            res = 10; //default value
                    }
                    _cacheMinutes = res;
                    cache.Set(key, res, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(res) });
                }

                return _cacheMinutes;
                //}
                //catch (Exception ex)
                //{
                //    return 10; //default vaue
                //}
            }
        }

        public static List<SysSetting> GetList(string groupName)
        {
            int cacheMinutes = GetCacheMinutes();

            lock (_locker)
            {
                var result = new List<SysSetting>();
                try
                {
                    string key = $"{_keyPrefix}|{groupName}";
                    ObjectCache cache = MemoryCache.Default;
                    List<SysSetting> obj = cache[key] as List<SysSetting>;

                    if (obj == null)
                    {
                        obj = new SysSettingDAC().SelectActive(groupName);
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

        public static string GetValue(string item)
        {
            int cacheMinutes = GetCacheMinutes();
            lock (_locker3)
            {
                string result = null;
                try
                {
                    string key = $"{_keyPrefix}-{item}";
                    ObjectCache cache = MemoryCache.Default;
                    object obj = cache[key];

                    if (obj == null)
                    {
                        var sysSetting = new SysSettingDAC().SelectByPK(item);
                        if (sysSetting == null) return null;

                        obj = sysSetting.Value;
                        cache.Set(key, obj, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(cacheMinutes) });
                    }

                    result = (string)obj;
                }
                catch (Exception ex)
                {
                }
                return result;
            }
        }

        //public static SysSetting Find(string groupName, string item)
        //{
        //    var list = GetList(groupName);
        //    foreach(var it in list)
        //    {
        //        if (it.Item.Equals(item, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return it;
        //        }
        //    }
        //    return null;
        //}
    }
}