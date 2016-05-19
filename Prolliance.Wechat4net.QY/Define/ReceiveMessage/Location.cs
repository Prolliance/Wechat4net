using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// Location 消息类型
    /// </summary>
    public class Location : Base
    {
        public Location() { }
        
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Location_X { set; get; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { set; get; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { set; get; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { set; get; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [XmlProperty("MsgId")]
        public Int64 MsgID { set; get; }

    }
}
