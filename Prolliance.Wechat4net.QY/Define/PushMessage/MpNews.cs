using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.PushMessage
{
    /// <summary>
    /// 图文消息类型
    /// </summary>
    public class MpNews : Base
    {
        /// <summary>
        /// 图文素材ID
        /// <para>可以调用上传图文素材接口获取，如果未上传图文素材，可将此参数置空，并给MpNewsInfo赋值</para>
        /// <para>MediaID和MpNewsInfo同时存在时，优先MediaID</para>
        /// </summary>
        public string MediaID { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "mpnews"; } }

        /// <summary>
        /// 是否是保密消息
        /// </summary>
        public bool IsSafe { set; get; }

        /// <summary>
        /// 图文消息内容
        /// </summary>
        public Wechat4net.QY.Define.MediaFile.MpNewsInfo MpNewsInfo { set; get; }

        /// <summary>
        /// 实例化一个图片类型推送信息对象
        /// </summary>
        /// <param name="mediaId">图文媒体文件id，可以调用上传图文素材接口获取</param>
        public MpNews(string appId, List<string> toUser, List<string> toParty, List<string> toTag, string mediaId, bool isSafe)
        {
            this.messageType = Enums.QY.PushMessageEnum.MpNews;
            //this.MsgType = "mpnews";
            this.AppID = appId;
            this.ToUser = toUser ?? new List<string>();
            this.ToParty = toParty ?? new List<string>();
            this.ToTag = toTag ?? new List<string>();
            this.MediaID = mediaId;
            this.IsSafe = isSafe;
        }

        internal override object GetData()
        {
            string touser, toparty, totag;
            this.GetPushObject(out touser, out toparty, out totag);

            if (!string.IsNullOrWhiteSpace(this.MediaID))
            {
                return new
                {
                    touser = touser,
                    toparty = toparty,
                    totag = totag,
                    msgtype = MsgType,
                    agentid = AppID,
                    mpnews = new
                    {
                        media_id = MediaID
                    },
                    safe = IsSafe ? "1" : "0"
                };
            }
            else
            {
                //List<object> newsList = new List<object>();
                //foreach (var item in this.MpNewsInfo.Articles)
                //{
                //    var newsItem = new
                //    {
                //        title = item.Title,
                //        thumb_media_id = item.ThumbMediaID,
                //        author = item.Author,
                //        content_source_url = item.ContentSourceUrl,
                //        content = item.Content,
                //        digest = item.Digest,
                //        show_cover_pic = item.showCoverPic
                //    };
                //    newsList.Add(newsItem);
                //}

                return new
                {
                    touser = touser,
                    toparty = toparty,
                    totag = totag,
                    msgtype = MsgType,
                    agentid = AppID,
                    mpnews = MpNewsInfo.GetData(),
                    safe = IsSafe ? "1" : "0"
                };
            }
        }

    }
}
