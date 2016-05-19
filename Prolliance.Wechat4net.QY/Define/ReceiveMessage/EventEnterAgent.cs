using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// 成员进入应用的事件 消息类型
    /// </summary>
    public class EventEnterAgent : Base
    {
        public EventEnterAgent() { }
        
        /// <summary>
        /// 事件类型，enter_agent
        /// </summary>
        public string Event { set; get; }
        /// <summary>
        /// 事件KEY值，此事件该值为空
        /// </summary>
        public string EventKey { set; get; }
    }
}
