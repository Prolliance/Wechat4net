using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.MediaFile
{
    public class QuiryNewsReturnValue : ReturnValue
    {
        /// <summary>
        /// 这篇图文消息素材的图文页列表
        /// </summary>
        [JsonProperty("news_item")]
        public List<NewsItemInfo> NewsItemList { set; get; }

        public class NewsItemInfo : MediaFile.Type.MpNews.Article
        {
            /// <summary>
            /// 图文页的Url
            /// </summary>
            [JsonProperty("url")]
            public string Url { set; get; }
        }
    }
}
