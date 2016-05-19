using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReplyMessage
{
    /// <summary>
    /// 语音消息类型
    /// </summary>
    public class Voice : Base
    {
        /// <summary>
        /// 语音文件id，可以调用上传媒体文件接口获取
        /// </summary>
        [XmlProperty("Voice.MediaId", 5)]
        public string MediaID { set; get; }

        /// <summary>
        /// 实例化一个语音消息类型对象
        /// </summary>
        public Voice()
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Voice;
            this.MsgType = "voice";
        }

        /// <summary>
        /// 实例化一个语音消息类型对象
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="mediaId">语音文件id，可以调用上传媒体文件接口获取</param>
        public Voice(string toUserName, string fromUserName, string mediaId)
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Voice;
            this.MsgType = "voice";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.MediaID = mediaId;
        }

    }
}
