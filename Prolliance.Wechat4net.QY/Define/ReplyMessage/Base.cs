using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;
using ReplyMessageEnum = Wechat4net.Utils.Enums.QY.ReplyMessageEnum;

namespace Wechat4net.QY.Define.ReplyMessage
{
    /// <summary>
    /// 微信企业号回复消息类型基础类
    /// </summary>
    public class Base : ReplyMessageBase
    {
        /// <summary>
        /// 回复信息类型枚举
        /// </summary>
        internal ReplyMessageEnum messageType = ReplyMessageEnum.Unknow;

        /// <summary>
        /// 消息类型
        /// </summary>
        [XmlProperty("MsgType", 4)]
        public string MsgType { internal set; get; }
    }
}
