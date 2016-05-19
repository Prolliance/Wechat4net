using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.PushMessage
{
    /// <summary>
    /// 卡券消息类型
    /// </summary>
    public class WxCard : Base
    {
        /// <summary>
        /// 实例化一个卡券类型推送信息对象
        /// </summary>
        /// <param name="cardId">卡券ID</param>
        public WxCard(string cardId)
        {
            this.messageType = Enums.MP.PushMessageEnum.WxCard;
            this.CardID = cardId;
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardID { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "wxcard"; } }

        internal override object GetData()
        {
            return new { card_id = this.CardID };
        }

    }
}
