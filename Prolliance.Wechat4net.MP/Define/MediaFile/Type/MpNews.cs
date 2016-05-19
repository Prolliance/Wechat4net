using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Wechat4net.MP.Define.MediaFile.Type
{
    /// <summary>
    /// 图文消息类型
    /// </summary>
    public class MpNews
    {
        internal Utils.FileType FileType
        {
            get { return Utils.FileType.News; }
        }

        /// <summary>
        /// 图文消息列表，一个图文消息支持1到10条图文
        /// </summary>
        public List<Article> Articles { set; get; }

        /// <summary>
        /// 图文消息内容类型
        /// </summary>
        public class Article
        {
            /// <summary>
            /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
            /// </summary>
            [JsonProperty("thumb_media_id")]
            public string ThumbMediaID { set; get; }

            /// <summary>
            /// 图文消息的作者
            /// </summary>
            [JsonProperty("author")]
            public string Author { set; get; }

            /// <summary>
            /// 图文消息的标题
            /// </summary>
            [JsonProperty("title")]
            public string Title { set; get; }

            /// <summary>
            /// 在图文消息页面点击“阅读原文”后的页面
            /// </summary>
            [JsonProperty("content_source_url")]
            public string ContentSourceUrl { set; get; }

            /// <summary>
            /// 图文消息页面的内容，支持HTML标签
            /// </summary>
            [JsonProperty("content")]
            public string Content { set; get; }

            /// <summary>
            /// 图文消息的描述
            /// </summary>
            [JsonProperty("digest")]
            public string Digest { set; get; }

            /// <summary>
            /// 是否显示封面，1为显示，0为不显示
            /// </summary>
            [JsonProperty("show_cover_pic")]
            internal int showCoverPic
            {
                get { return ShowCoverPic ? 1 : 0; }
            }

            /// <summary>
            /// 是否显示封面
            /// </summary>
            public bool ShowCoverPic { set; get; }

            internal object GetData()
            {
                return new
                {
                    thumb_media_id = this.ThumbMediaID,
                    author = this.Author,
                    title = this.Title,
                    content_source_url = this.ContentSourceUrl,
                    content = this.Content,
                    digest = this.Digest,
                    show_cover_pic = this.showCoverPic
                };
            }
        }

        internal object GetData()
        {
            List<object> temp = new List<object>();
            if (this.Articles != null)
            {
                foreach (var article in this.Articles)
                {
                    temp.Add(article.GetData());
                }
            }
            return new { articles = temp };
        }
    }
}
