using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 点击菜单跳转链接时的事件推送
    /// </summary>
    public class EventView:Base
    {
        public EventView() { }

        /// <summary>
        /// 事件类型，VIEW
        /// </summary>
        public string Event { set; get; }
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { set; get; }

    }
}
