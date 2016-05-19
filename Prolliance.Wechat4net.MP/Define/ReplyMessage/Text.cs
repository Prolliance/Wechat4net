using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReplyMessage
{
    /// <summary>
    /// 文字消息类型
    /// </summary>
    public class Text : Base
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [XmlProperty(5)]
        public string Content { set; get; }

        /// <summary>
        /// 实例化一个文本消息类型对象
        /// </summary>
        public Text()
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Text;
            this.MsgType = "text";
        }

        /// <summary>
        /// 实例化一个文本消息类型对象
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="content">文本消息内容</param>
        public Text(string toUserName, string fromUserName, string content)
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Text;
            this.MsgType = "text";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.Content = content;
        }

    }
}
