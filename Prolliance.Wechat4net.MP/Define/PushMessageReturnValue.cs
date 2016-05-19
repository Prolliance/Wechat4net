using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.MP.Define
{
    /// <summary>
    /// 推送微信消息返回结果类型
    /// </summary>
    public class PushMessageReturnValue : ReturnValue
    {
        ///// <summary>
        ///// 错误代码，0成功
        ///// </summary>
        //[JsonProperty("errcode")]
        //public int ErrorCode { set; get; }

        ///// <summary>
        ///// 错误消息
        ///// </summary>
        //[JsonProperty("errmsg")]
        //public string ErrorMessage { set; get; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty("msg_id")]
        public string MessageID { set; get; }

        /// <summary>
        /// 实例化推送微信消息返回结果类型实体
        /// </summary>
        public PushMessageReturnValue()
        {
            this.ErrorCode = 0;
            this.ErrorMessage = "ok";
        }
        /// <summary>
        /// 实例化推送微信消息返回结果类型实体
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误消息</param>
        public PushMessageReturnValue(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
