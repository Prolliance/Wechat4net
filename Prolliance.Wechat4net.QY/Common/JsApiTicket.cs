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
    /// 企业号调用JsApi凭证的基类
    /// </summary>
    internal class JsApiTicket : JsApiTicketBase
    {
        /// <summary>
        /// 微信接口URL
        /// </summary>
        private static string Url
        {
            get
            {
                return string.Format("{0}?access_token={1}", ServiceUrl.GetJsApiTicket, AccessToken.Value);
            }
        }

        /// <summary>
        /// 服务器缓存Key
        /// </summary>
        private static string Key
        {
            get { return CacheKey.JsApiTicket; }
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
