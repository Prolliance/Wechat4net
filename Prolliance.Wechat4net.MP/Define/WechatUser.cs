using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Wechat4net.MP.Define
{
    /// <summary>
    /// 微信用户信息类
    /// </summary>
    [Serializable]
    public class WechatUser
    {
        [JsonProperty("errcode")]
        private int errorCode = 0;
        /// <summary>
        /// 错误代码 0成功
        /// </summary>
        internal int ErrorCode { get { return errorCode; } }

        [JsonProperty("errmsg")]
        private string errorMessage = null;
        /// <summary>
        /// 错误信息
        /// </summary>
        internal string ErrorMessage { get { return errorMessage; } }

        /// <summary>
        /// OpenID 用户的唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenID { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [JsonProperty("sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        [JsonProperty("headimgurl")]
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        [JsonProperty("privilege")]
        public object Privilege { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：
        /// <para>https://open.weixin.qq.com/cgi-bin/frame?t=resource/res_main_tmpl&lang=zh_CN&target=res/app_wx_login</para>
        /// </summary>
        [JsonProperty("unionid")]
        public string Unionid { get; set; }
    }
}