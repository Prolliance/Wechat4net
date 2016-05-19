using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.QY.Define.ReplyMessage
{
    /// <summary>
    /// 视频消息类型
    /// </summary>
    public class Video : Base
    {
        /// <summary>
        /// 视频文件id，可以调用上传媒体文件接口获取
        /// </summary>
        [XmlProperty("Video.MediaId", 5)]
        public string MediaID { set; get; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        [XmlProperty("Video.Title", 6)]
        public string Title { set; get; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        [XmlProperty("Video.Description", 7)]
        public string Description { set; get; }

        /// <summary>
        /// 实例化一个视频消息类型对象
        /// </summary>
        public Video()
        {
            this.messageType = Wechat4net.Utils.Enums.QY.ReplyMessageEnum.Video;
            this.MsgType = "video";
        }

        /// <summary>
        /// 实例化一个视频消息类型对象
        /// </summary>
        /// <param name="toUserName">成员UserID</param>
        /// <param name="fromUserName">企业号CorpID</param>
        /// <param name="mediaId">视频文件id，可以调用上传媒体文件接口获取</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        public Video(string toUserName, string fromUserName, string mediaId, string title, string description)
        {
            this.messageType = Wechat4net.Utils.Enums.QY.ReplyMessageEnum.Video;
            this.MsgType = "video";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.MediaID = mediaId;
            this.Title = title;
            this.Description = description;
        }

    }
}
