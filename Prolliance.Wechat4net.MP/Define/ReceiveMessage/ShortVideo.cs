using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReceiveMessage
{
    /// <summary>
    /// 小视频消息
    /// </summary>
    class ShortVideo:Base
    {
        public ShortVideo() { }
        
        /// <summary>
        /// 视频媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        [XmlProperty("MediaId")]
        public string MediaID { set; get; }
        
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        [XmlProperty("ThumbMediaId")]
        public string ThumbMediaID { set; get; }
        
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [XmlProperty("MsgId")]
        public Int64 MsgID { set; get; }

    }
}
