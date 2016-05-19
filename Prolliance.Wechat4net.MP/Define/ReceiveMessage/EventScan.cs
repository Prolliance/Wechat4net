using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 扫描带参数二维码事件(用户已关注)
    /// </summary>
    public class EventScan:Base
    {
        public EventScan() { }

        /// <summary>
        /// 事件类型，SCAN
        /// </summary>
        public string Event { set; get; }
        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { set; get; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { set; get; }

    }
}
