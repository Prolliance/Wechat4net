using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.PushMessage
{
    /// <summary>
    /// 文本消息类型
    /// </summary>
    public class Text : Base
    {
        /// <summary>
        /// 实例化一个文本类型推送信息对象
        /// </summary>
        /// <param name="content">信息内容</param>
        public Text(string content)
        {
            this.messageType = Enums.MP.PushMessageEnum.Text;
            this.Content = content;
        }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "text"; } }

        internal override object GetData()
        {
            return new { content = this.Content };
        }

    }
}
