using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReplyMessage
{
    /// <summary>
    /// 图片消息类型
    /// </summary>
    public class Image : Base
    {
        /// <summary>
        /// 图片文件id，可以调用上传媒体文件接口获取
        /// </summary>
        [XmlProperty("Image.MediaId", 5)]
        public string MediaID { set; get; }

        /// <summary>
        /// 实例化一个图片消息类型对象
        /// </summary>
        public Image()
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Image;
            this.MsgType = "image";
        }

        /// <summary>
        /// 实例化一个图片消息类型对象
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="mediaId">图片文件id，可以调用上传媒体文件接口获取</param>
        public Image(string toUserName, string fromUserName, string mediaId)
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Image;
            this.MsgType = "image";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.MediaID = mediaId;
        }

    }
}
