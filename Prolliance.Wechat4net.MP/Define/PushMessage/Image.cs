using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.PushMessage
{
    /// <summary>
    /// 图片消息类型
    /// </summary>
    public class Image : Base
    {
        /// <summary>
        /// 实例化一个图片类型推送信息对象
        /// </summary>
        /// <param name="mediaId">图片媒体文件id，可以调用上传媒体文件接口获取</param>
        public Image(string mediaId)
        {
            this.messageType = Enums.MP.PushMessageEnum.Image;
            this.MediaID = mediaId;
        }

        /// <summary>
        /// 图片媒体文件id，可以调用上传媒体文件接口获取
        /// </summary>
        public string MediaID { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "image"; } }

        internal override object GetData()
        {
            return new { media_id = this.MediaID };
        }

    }
}
