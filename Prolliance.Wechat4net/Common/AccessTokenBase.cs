using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using AjaxEngine.AjaxHandlers;
using Newtonsoft.Json;
using System.Net;
using Wechat4net.Utils;

namespace Wechat4net.Common
{
    /// <summary>
    /// 服务号和企业号调用接口凭证的基类
    /// </summary>
    public class AccessTokenBase
    {
        private class RetValue
        {
            [JsonProperty("errcode")]
            public int ErrorCode { set; get; }

            [JsonProperty("errmsg")]
            public string ErrorMessage { set; get; }

            [JsonProperty("access_token")]
            public string AccessToken { set; get; }
            /// <summary>
            /// access_token过期时间(秒)
            /// </summary>
            [JsonProperty("expires_in")]
            public int ExpiresIn { set; get; }
        }

        /// <summary>
        /// 获取调用接口凭证
        /// </summary>
        /// <param name="url">微信接口URL</param>
        /// <param name="cacheKey">服务器缓存Key</param>
        /// <returns></returns>
        protected static string GetValue(string url, string cacheKey)
        {
            DateTime now = DateTime.UtcNow;
            string _value = HttpContext.Current.Cache.Get(cacheKey) as string;
            if (string.IsNullOrWhiteSpace(_value))
            {
                var rs = HttpHelper.Get<RetValue>(url);
                if (!string.IsNullOrEmpty(rs.AccessToken))
                {
                    _value = rs.AccessToken;
                    HttpContext.Current.Cache.Insert(cacheKey, _value, null, now.AddSeconds(rs.ExpiresIn - 300), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    throw new Exception("获取微信AccessToken失败,ErrorCode:" + rs.ErrorCode.ToString() + "; ErrorMessage:" + rs.ErrorMessage);
                }
            }
            return _value;
        }
    }
}
