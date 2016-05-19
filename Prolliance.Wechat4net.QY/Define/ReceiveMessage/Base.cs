using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Wechat4net.Define;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// 微信企业号接收消息类型基础类
    /// </summary>
    public class Base : ReceiveMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        [XmlProperty("MsgType")]
        public string MsgType { set; get; }
        /// <summary>
        /// 企业应用的id
        /// </summary>
        [XmlProperty("AgentID")]
        public string AppID { set; get; }

    }
}
