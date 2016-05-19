using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.PushMessage
{
    /// <summary>
    /// 图文消息类型
    /// </summary>
    public class MpNews : Base
    {
        /// <summary>
        /// 实例化一个图片类型推送信息对象
        /// </summary>
        /// <param name="mediaId">图文媒体文件id，可以调用上传图文素材接口获取</param>
        public MpNews(string mediaId)
        {
            this.messageType = Enums.MP.PushMessageEnum.MpNews;
            this.MediaID = mediaId;
        }

        /// <summary>
        /// 图文媒体文件id，可以调用上传图文素材接口获取
        /// </summary>
        public string MediaID { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "mpnews"; } }

        internal override object GetData()
        {
            return new { media_id = this.MediaID };
        }

    }
}
