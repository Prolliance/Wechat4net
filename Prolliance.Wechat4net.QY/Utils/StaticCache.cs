using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.QY.Utils
{
    /// <summary>
    /// 全局静态缓存
    /// </summary>
    internal class StaticCache
    {
        private static IDictionary<string, object> _dic;
        private static object locker = new object();

        private static IDictionary<string, object> CachedDic
        {
            get
            {
                if (_dic == null)
                {
                    lock (locker)
                    {
                        if (_dic == null)
                        {
                            _dic = new Dictionary<string, object>();
                        }
                    }
                }

                return _dic;
            }
        }

        public static bool ContainsKey(string key)
        {
            return CachedDic.ContainsKey(key);
        }

        public static object GetObject(string key)
        {
            return CachedDic.ContainsKey(key) ? CachedDic[key] : null;
        }

        public static void SetObject(string key, object obj)
        {
            lock (locker)
            {
                if (CachedDic.ContainsKey(key))
                {
                    CachedDic[key] = obj;
                }
                else
                {
                    CachedDic.Add(key, obj);
                }
            }
        }
    }
}
