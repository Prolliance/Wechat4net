using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Common;
using Wechat4net.MP.Utils;

namespace Wechat4net.MP.Common
{
    /// <summary>
    /// 服务号调用微信接口凭证
    /// </summary>
    internal class AccessToken : AccessTokenBase
    {
        /// <summary>
        /// 微信接口URL
        /// </summary>
        private static string Url
        {
            get
            {
                return string.Format("{0}?grant_type={1}&appid={2}&secret={3}", ServiceUrl.GetToken, "client_credential", WechatConfig.AppID, WechatConfig.Secret);
            }
        }

        /// <summary>
        /// 服务器缓存Key
        /// </summary>
        private static string Key
        {
            get { return CacheKey.AccessToken; }
        }

        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public static string Value
        {
            get { return GetValue(Url, Key); }
        }

    }
}
