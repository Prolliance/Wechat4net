using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define
{
    /// <summary>
    /// 微信SDK设置
    /// </summary>
    public class WechatOption
    {
        /// <summary>
        /// 公众号AppID
        /// </summary>
        public string AppID { set; get; }
        /// <summary>
        /// 公众号Secret
        /// </summary>
        public string Secret { set; get; }
        /// <summary>
        /// 回调模式Token
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// 回调模式EncodingAESKey
        /// </summary>
        public string EncodingAESKey { set; get; }
        //public int AppID { set; get; }

        /// <summary>
        /// 语言
        /// </summary>
        public Wechat4net.Utils.Enums.MP.Language Language { set; get; }

        /// <summary>
        /// 创建一个新WechatOption实例
        /// </summary>
        /// <param name="appID">公众号AppID</param>
        /// <param name="secret">公众号Secret</param>
        /// <param name="token">回调模式Token</param>
        /// <param name="encodingAESKey">回调模式EncodingAESKey</param>
        public WechatOption(string appID, string secret, string token, string encodingAESKey, Wechat4net.Utils.Enums.MP.Language language)
        {
            this.Token = token;
            this.AppID = appID;
            this.EncodingAESKey = encodingAESKey;
            this.Secret = secret;
            //this.AppID = appId;
            this.Language = language;
        }

        /// <summary>
        /// 创建一个新WechatOption实例
        /// </summary>
        public WechatOption() { }

    }
}
