using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 微信SDK设置
    /// </summary>
    public class WechatOption
    {
        /// <summary>
        /// 企业号CorpID
        /// </summary>
        public string CorpID { set; get; }
        /// <summary>
        /// 企业号Secret
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
        /// 创建一个新WechatOption实例
        /// </summary>
        /// <param name="corpId">企业号CorpID</param>
        /// <param name="secret">企业号Secret</param>
        /// <param name="token">回调模式Token</param>
        /// <param name="encodingAESKey">回调模式EncodingAESKey</param>
        public WechatOption(string corpId, string secret, string token, string encodingAESKey)
        {
            this.Token = token;
            this.CorpID = corpId;
            this.EncodingAESKey = encodingAESKey;
            this.Secret = secret;
            //this.AppID = appId;
        }
        
        /// <summary>
        /// 创建一个新WechatOption实例
        /// </summary>
        public WechatOption() { }

    }
}
