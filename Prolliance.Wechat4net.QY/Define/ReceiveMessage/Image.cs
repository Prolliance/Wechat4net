using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// Image 消息类型
    /// </summary>
    public class Image : Base
    {
        public Image() { }
        
        /// <summary>
        /// 图片URL链接地址
        /// </summary>
        public string PicUrl { set; get; }
        /// <summary>
        /// 图片媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        [XmlProperty("MediaId")]
        public string MediaID { set; get; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [XmlProperty("MsgId")]
        public Int64 MsgID { set; get; }

    }
}
