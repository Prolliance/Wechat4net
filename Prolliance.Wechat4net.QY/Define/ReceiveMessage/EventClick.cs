using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// 点击菜单拉取消息的事件 消息类型
    /// </summary>
    public class EventClick : Base
    {
        public EventClick() { }
        
        /// <summary>
        /// 事件类型，CLICK
        /// </summary>
        public string Event { set; get; }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { set; get; }
    }
}
