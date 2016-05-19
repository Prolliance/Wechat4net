using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.MP.Define
{
    internal class WebAccessTokenReturnValue : ReturnValue
    {
        //[JsonProperty("errcode")]
        //public int ErrorCode { set; get; }

        //[JsonProperty("errmsg")]
        //public string ErrorMessage { set; get; }

        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        [JsonProperty("access_token")]
        public string WebAccessToken { set; get; }

        /// <summary>
        /// WebAccessToken接口调用凭证超时时间，单位（秒）
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { set; get; }

        /// <summary>
        /// 用户刷新WebAccessToken
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { set; get; }

        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        [JsonProperty("openid")]
        public string OpenID { set; get; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { set; get; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：
        /// <para>https://open.weixin.qq.com/cgi-bin/frame?t=resource/res_main_tmpl&lang=zh_CN&target=res/app_wx_login</para>
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionID { set; get; }

    }
}
