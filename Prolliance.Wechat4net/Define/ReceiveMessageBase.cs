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
    public interface IReceiveMessageBase : IMessageBase
    {
        //long MsgId { get; set; }
        //ReceiveMessageBase Parser(XmlDocument xml);
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public abstract class ReceiveMessageBase : MessageBase, IReceiveMessageBase
    {
        public ReceiveMessageBase()
        {

        }

        //public long MsgId { get; set; }
        //public abstract ReceiveMessageBase Parser(XmlDocument xml);
    }
}
