using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 上报地理位置事件 消息类型
    /// </summary>
    public class EventLocation : Base
    {
        public EventLocation() { }
        
        /// <summary>
        /// 事件类型，LOCATION
        /// </summary>
        public string Event { set; get; }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { set; get; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { set; get; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { set; get; }
    }
}
