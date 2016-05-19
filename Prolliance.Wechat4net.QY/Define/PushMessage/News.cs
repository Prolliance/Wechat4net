using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.PushMessage
{
    /// <summary>
    /// 新闻消息类型
    /// </summary>
    public class News : Base
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "news"; } }

        private List<NewsItem> newsList = null;
        /// <summary>
        /// 新闻列表 最多10条，多余部分将不会发送
        /// </summary>
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
        /// 新闻详情类型
        /// </summary>
        public class NewsItem
        {
            /// <summary>
            /// 图文消息标题
            /// </summary>
            public string Title { set; get; }
            /// <summary>
            /// 图文消息描述
            /// </summary>
            public string Description { set; get; }
            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
            /// </summary>
            public string PicUrl { set; get; }
            /// <summary>
            /// 点击图文消息跳转链接
            /// </summary>
            public string Url { set; get; }
        }

        /// <summary>
        /// 实例化一个 新闻类型推送信息对象
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="toUser">员工ID列表 最多支持1000个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toParty">部门ID列表 最多支持100个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toTag">标签ID列表 （当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="newsList">新闻列表 最多10条，多余部分将不会发送</param>
        public News(string appId, List<string> toUser, List<string> toParty, List<string> toTag, List<NewsItem> newsList)
        {
            this.messageType = Enums.QY.PushMessageEnum.News;
            //this.MsgType = "news";
            this.AppID = appId;
            this.ToUser = toUser ?? new List<string>();
            this.ToParty = toParty ?? new List<string>();
            this.ToTag = toTag ?? new List<string>();
            this.NewsList = newsList;
        }

        internal override object GetData()
        {
            string touser, toparty, totag;
            this.GetPushObject(out touser, out toparty, out totag);

            List<object> newsList = new List<object>();
            foreach (var item in this.NewsList)
            {
                var newsItem = new
                {
                    title = item.Title,
                    description = item.Description,
                    url = item.Url,
                    picurl = item.PicUrl
                };
                newsList.Add(newsItem);
            }

            var data = new
            {
                touser = touser,
                toparty = toparty,
                totag = totag,
                msgtype = MsgType,
                agentid = AppID,
                news = new
                {
                    articles = newsList
                }
            };
            return data;
        }

    }
}
