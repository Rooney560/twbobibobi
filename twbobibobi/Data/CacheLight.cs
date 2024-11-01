using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using twbobibobi.Entities;
using Temple.data;

using MotoSystem.Data;

namespace twbobibobi.Data
{
    public class CacheLight
    {
        private const string _key = "Light";
        private const int _cacheMinutes = 10; //緩存有效時長（分鐘）
        protected static readonly object _locker = new object();
        public static List<Light> GetList()
        {
            lock (_locker)
            {
                var result = new List<Light>();
                try
                {
                    ObjectCache cache = MemoryCache.Default;
                    List<Light> obj = cache[_key] as List<Light>;

                    if (obj == null)
                    {
                        BasePage basePage = new BasePage();
                        obj = new LightDAC(basePage).SelectActive();
                        cache.Set(_key, obj, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(_cacheMinutes) });
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