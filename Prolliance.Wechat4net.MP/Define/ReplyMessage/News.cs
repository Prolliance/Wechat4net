using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.ReplyMessage
{
    /// <summary>
    /// 图文类型
    /// </summary>
    public class News : Base
    {
        /// <summary>
        /// 图文条数，默认第一条为大图。最多10条。根据newsList自动生成
        /// </summary>
        [XmlProperty("ArticleCount", 5)]
        public int ArticleCount
        {
            get
            {
                if (NewsList == null) return 0;
                return NewsList.Count;
            }
        }

        private List<NewsItem> newsList = null;
        /// <summary>
        /// 新闻列表 最多10条，多余部分将不会返回
        /// </summary>
        [XmlProperty("Articles", 6)]
        public List<NewsItem> NewsList
        {
            set
            {
                if (value != null && value.Count > 10)
                {
                    value.RemoveRange(9, value.Count - 10);
                }
                newsList = value;
            }
            get
            {
                return newsList;
            }
        }

        /// <summary>
        /// 图文详情类型
        /// </summary>
        public class NewsItem
        {
            /// <summary>
            /// 实例化一条图文详情
            /// </summary>
            public NewsItem() { }

            /// <summary>
            /// 图文消息标题
            /// </summary>
            [XmlProperty(1)]
            public string Title { set; get; }
            /// <summary>
            /// 图文消息描述
            /// </summary>
            [XmlProperty(2)]
            public string Description { set; get; }
            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
            /// </summary>
            [XmlProperty(3)]
            public string PicUrl { set; get; }
            /// <summary>
            /// 点击图文消息跳转链接
            /// </summary>
            [XmlProperty(4)]
            public string Url { set; get; }
        }

        /// <summary>
        /// 实例化一个图文消息类型对象
        /// </summary>
        public News()
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.News;
            this.MsgType = "news";
        }

        /// <summary>
        /// 实例化一个图文消息类型对象
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="newsList">新闻列表</param>
        public News(string toUserName, string fromUserName, List<NewsItem> newsList)
        {
            this.messageType = Wechat4net.Utils.Enums.MP.ReplyMessageEnum.News;
            this.MsgType = "news";
            this.ToUserName = toUserName;
            this.FromUserName = fromUserName;
            //if (newsList != null && newsList.Count > 10)
            //{
            //    newsList.RemoveRange(9, newsList.Count - 10);
            //}
            //this.ArticleCount = newsList == null ? 0 : newsList.Count;
            this.NewsList = newsList;
        }

    }
}
