using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 微信服务号接收消息类型基础类
    /// </summary>
    public class Base : ReceiveMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        [XmlProperty("MsgType")]
        public string MsgType { set; get; }
    }
}
