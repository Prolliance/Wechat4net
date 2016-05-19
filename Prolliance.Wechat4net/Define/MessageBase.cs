using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat4net.Define
{
    public interface IMessageBase
    {
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 所有Receive和Reply消息的基类
    /// </summary>
    public class MessageBase : IMessageBase
    {
        /// <summary>
        /// 消息发送者
        /// </summary>
        [XmlProperty(1)]
        public string ToUserName { get; set; }

        /// <summary>
        /// 消息接受者
        /// </summary>
        [XmlProperty(2)]
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        [XmlProperty(3)]
        public DateTime CreateTime { get; set; }
    }
}
