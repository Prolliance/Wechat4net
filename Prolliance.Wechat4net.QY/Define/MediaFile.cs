using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 多媒体文件相关定义
    /// </summary>
    public class MediaFile
    {
        /// <summary>
        /// 媒体文件类型枚举
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// 普通文件(file)
            /// </summary>
            File = 0,
            /// <summary>
            /// 图片（image）
            /// </summary>
            Image = 1,
            /// <summary>
            /// 语音（voice）
            /// </summary>
            Voice = 2,
            /// <summary>
            /// 视频（video）
            /// </summary>
            Video = 3
        }

        internal static FileType GetFileTypeEnum(string type)
        {
            FileType fte = FileType.File;

            string[] enumList = Enum.GetNames(typeof(FileType));
            for (int i = 0; i < enumList.Length; i++)
            {
                if (enumList[i].ToLower() == type.ToLower())
                {
                    fte = (FileType)Enum.ToObject(typeof(FileType), i);
                    break;
                }
            }

            return fte;
        }

        internal static string GetFileTypeString(FileType type)
        {
            return Enum.GetName(typeof(FileType), type);
        }


        /// <summary>
        /// 上传临时媒体文件返回类型
        /// </summary>
        public class UploadTempReturnValue : ReturnValue
        {
            /// <summary>
            /// 媒体文件类型字符串
            /// 仅限SDK内部使用
            /// </summary>
            [JsonProperty("type")]
            internal string TypeString { set; get; }
            /// <summary>
            /// 媒体文件类型
            /// </summary>
            public FileType Type
            {
                get { return GetFileTypeEnum(this.TypeString); }
                set { this.TypeString = GetFileTypeString(value); }
            }
            /// <summary>
            /// 媒体文件上传后获取的唯一标识
            /// <para>所有管理组均可调用，MediaID可以共享。</para>
            /// </summary>
            [JsonProperty("media_id")]
            public string MediaID { set; get; }

            [JsonProperty("created_at")]
            private long createdAt { set; get; }

            /// <summary>
            /// 媒体文件上传时间
            /// </summary>
            public DateTime CreatedAt
            {
                set
                {
                    this.createdAt = DateTimeConverter.GetWeixinDateTime(value);
                }
                get
                {
                    return DateTimeConverter.GetDateTimeFromXml(this.createdAt);
                }
            }

            public UploadTempReturnValue() { }

            internal UploadTempReturnValue(int errorCode, string errorMessage)
            {
                this.ErrorCode = errorCode;
                this.ErrorMessage = errorMessage;
            }
        }

        /// <summary>
        /// 上传永久媒体文件返回类型
        /// </summary>
        public class UploadForeverReturnValue : ReturnValue
        {
            /// <summary>
            /// 媒体文件上传后获取的唯一标识
            /// <para>MediaID可以在同一个应用的不同管理组共享</para>
            /// </summary>
            [JsonProperty("media_id")]
            public string MediaID { set; get; }

            public UploadForeverReturnValue() { }

            internal UploadForeverReturnValue(int errorCode, string errorMessage)
            {
                this.ErrorCode = errorCode;
                this.ErrorMessage = errorMessage;
            }
        }

        /// <summary>
        /// 下载媒体文件返回类型
        /// </summary>
        public class DownloadReturnValue
        {
            [JsonProperty("errcode")]
            private int errorCode = 0;
            /// <summary>
            /// 错误代码 0成功
            /// </summary>
            public int ErrorCode { get { return this.errorCode; } }

            [JsonProperty("errmsg")]
            private string errorMessage = "";
            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMessage { get { return this.errorMessage; } }

            /// <summary>
            /// 文件名（带扩展）
            /// </summary>
            public string FileName { set; get; }
            /// <summary>
            /// 文件大小（字节）
            /// </summary>
            public long FileSize { set; get; }
            /// <summary>
            /// Content-Type
            /// </summary>
            public string ContentType { set; get; }
            /// <summary>
            /// byte[]类型文件数据
            /// </summary>
            public byte[] FileData { set; get; }
            /// <summary>
            /// 文件流
            /// </summary>
            public Stream FileStream { set; get; }
        }


        public class MpNewsInfo
        {
            /// <summary>
            /// 图文消息列表，一个图文消息支持1到10条图文
            /// </summary>
            [JsonProperty("articles")]
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
                List<object> newsList = new List<object>();
                foreach (var item in Articles)
                {
                    var newsItem = new
                    {
                        title = item.Title,
                        thumb_media_id = item.ThumbMediaID,
                        author = item.Author,
                        content_source_url = item.ContentSourceUrl,
                        content = item.Content,
                        digest = item.Digest,
                        show_cover_pic = item.showCoverPic
                    };
                    newsList.Add(newsItem);
                }
                return new
                {
                    articles = newsList
                };
            }
        }


        public class QuiryCountReturnValue : ReturnValue
        {
            /// <summary>
            /// 应用素材总数目
            /// </summary>
            [JsonProperty("total_count")]
            public int TotalCount { set; get; }

            /// <summary>
            /// 永久图片素材个数
            /// </summary>
            [JsonProperty("image_count")]
            public int ImageCount { set; get; }

            /// <summary>
            /// 永久语音素材个数
            /// </summary>
            [JsonProperty("voice_count")]
            public int VoiceCount { set; get; }

            /// <summary>
            /// 永久视频素材个数
            /// </summary>
            [JsonProperty("video_count")]
            public int VideoCount { set; get; }

            /// <summary>
            /// 永久文件素材个数
            /// </summary>
            [JsonProperty("file_count")]
            public int FileCount { set; get; }

            /// <summary>
            /// 永久图文素材个数
            /// </summary>
            [JsonProperty("mpnews_count")]
            public int MpNewsCount { set; get; }

        }

        public class QuiryMediaListReturnValueBase : ReturnValue
        {
            /// <summary>
            /// 素材类型
            /// </summary>
            [JsonProperty("type")]
            public string Type { set; get; }

            /// <summary>
            /// 该类型的素材的总数
            /// </summary>
            [JsonProperty("total_count")]
            public int TotalCount { set; get; }

            /// <summary>
            /// 本次调用获取的素材的数量
            /// </summary>
            [JsonProperty("item_count")]
            public int ItemCount { set; get; }
        }

        public class QuiryFileListReturnValue : QuiryMediaListReturnValueBase
        {
            /// <summary>
            /// 素材列表
            /// </summary>
            [JsonProperty("itemlist")]
            public List<QuiryResultFileItem> ItemList { set; get; }


            public class QuiryResultFileItem
            {
                /// <summary>
                /// 素材MediaID
                /// </summary>
                [JsonProperty("media_id")]
                public string MediaID { set; get; }

                /// <summary>
                /// 素材文件名称
                /// </summary>
                [JsonProperty("filename")]
                public string FileName { set; get; }

                [JsonProperty("update_time")]
                private long updateTime;
                /// <summary>
                /// 素材的最后更新时间
                /// </summary>
                public DateTime UpdateTime { get { return DateTimeConverter.GetDateTimeFromXml(updateTime); } }
            }

        }

        public class QuiryNewsListReturnValue : QuiryMediaListReturnValueBase
        {
            /// <summary>
            /// 素材列表
            /// </summary>
            [JsonProperty("itemlist")]
            public List<QuiryResultItem> ItemList { set; get; }


            public class QuiryResultItem : MpNewsInfo
            {
                /// <summary>
                /// 这篇图文消息素材的MediaID
                /// </summary>
                [JsonProperty("media_id")]
                public string MediaID { set; get; }

                [JsonProperty("update_time")]
                private long updateTime { set; get; }
                /// <summary>
                /// 这篇图文消息素材的最后更新时间
                /// </summary>
                public DateTime UpdateTime { get { return DateTimeConverter.GetDateTimeFromXml(updateTime); } }

            }

        }

    }
}
