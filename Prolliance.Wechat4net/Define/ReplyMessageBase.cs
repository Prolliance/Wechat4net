using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Wechat4net.Utils;

namespace Wechat4net.Define
{
    public interface IReplyMessageBase : IMessageBase
    {
        //long MsgId { get; set; }
        //ReceiveMessageBase Parser(XmlNode root);
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public abstract class ReplyMessageBase : MessageBase, IReplyMessageBase
    {
        public ReplyMessageBase()
        {

        }

        //public long MsgId { get; set; }
        //public abstract ReceiveMessageBase Parser(XmlNode root);
    }
}
