using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 链接消息
    /// </summary>
    public class Link : Base
    {
        public Link() { }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { set; get; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [XmlProperty("MsgId")]
        public Int64 MsgID { set; get; }

    }
}
