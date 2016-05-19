using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 成员关注/取消关注事件 消息类型
    /// 成员关注、取消关注企业号的事件，会推送到每个应用在管理端设置的URL；特别的，默认企业小助手可以用于获取整个企业号的关注状况。
    /// </summary>
    public class EventSubscribe : Base
    {
        public EventSubscribe() { }
        
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        public string Event { set; get; }
    }
}
