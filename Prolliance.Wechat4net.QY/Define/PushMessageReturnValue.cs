using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 推送微信消息返回结果类型
    /// </summary>
    public class PushMessageReturnValue
    {
        /// <summary>
        /// 错误代码，0成功
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrorCode { set; get; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMessage { set; get; }
        
        /// <summary>
        /// 无效的人员名单 |分隔
        /// </summary>
        [JsonProperty("invaliduser")]
        public string InvalidUser { set; get; }
        
        /// <summary>
        /// 无效的部门名单 |分隔
        /// </summary>
        [JsonProperty("invalidparty")]
        public string InvalidParty { set; get; }

        /// <summary>
        /// 无效的标签名单 |分隔
        /// </summary>
        [JsonProperty("invalidtag")]
        public string InvalidTag { set; get; }

        public PushMessageReturnValue()
        {
            this.ErrorCode = 0;
            this.ErrorMessage = "ok";
        }
        public PushMessageReturnValue(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
