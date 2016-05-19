using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// Voice 消息类型
    /// </summary>
    public class Voice : Base
    {
        public Voice() { }
        
        /// <summary>
        /// 语音媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        [XmlProperty("MediaId")]
        public string MediaID { set; get; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { set; get; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        [XmlProperty("MsgId")]
        public Int64 MsgID { set; get; }
    }
}
