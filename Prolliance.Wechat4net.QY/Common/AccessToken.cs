using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Wechat4net.Common;
using Wechat4net.QY.Utils;

namespace Wechat4net.QY.Common
{
    /// <summary>
    /// 企业号调用微信接口凭证
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
                return string.Format("{0}?corpid={1}&corpsecret={2}", ServiceUrl.GetToken, WechatConfig.CorpID, WechatConfig.Secret); //Wechat.Options.CorpID, Wechat.Options.Secret);
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
