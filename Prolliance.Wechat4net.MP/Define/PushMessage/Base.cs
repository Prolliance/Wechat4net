using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.PushMessage
{
    /// <summary>
    /// 推送消息类型基类
    /// </summary>
    public abstract class Base
    {
        /// <summary>
        /// 消息类型字段 SDK内部判断使用
        /// </summary>
        internal Enums.MP.PushMessageEnum messageType = Enums.MP.PushMessageEnum.Unknow;

        /// <summary>
        /// 消息类型
        /// </summary>
        internal abstract string MsgType { get; }

        internal abstract object GetData();
    }

}
