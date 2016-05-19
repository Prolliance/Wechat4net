using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReplyMessage
{
    /// <summary>
    /// 音乐消息
    /// </summary>
    public class Music : Base
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        [XmlProperty("Music.Title", 5)]
        public string Title { set; get; }
        /// <summary>
        /// 音乐描述
        /// </summary>
        [XmlProperty("Music.Description", 6)]
        public string Description { set; get; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        [XmlProperty("Music.MusicUrl", 7)]
        public string MusicUrl { set; get; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        [XmlProperty("Music.HQMusicUrl", 8)]
        public string HQMusicUrl { set; get; }
        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        [XmlProperty("Music.ThumbMediaId", 9)]
        public string ThumbMediaID { set; get; }


        /// <summary>
        /// 实例化一个音乐消息类型对象
        /// </summary>
        public Music()
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Music;
            this.MsgType = "music";
        }

        /// <summary>
        /// 实例化一个音乐消息类型对象
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaID">缩略图的媒体id，通过上传多媒体文件，得到的id</param>
        public Music(string toUserName, string fromUserName, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaID)
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.Music;
            this.MsgType = "music";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            this.Title = title;
            this.Description = description;
            this.MusicUrl = musicUrl;
            this.HQMusicUrl = hqMusicUrl;
            this.ThumbMediaID = thumbMediaID;
        }

    }
}
