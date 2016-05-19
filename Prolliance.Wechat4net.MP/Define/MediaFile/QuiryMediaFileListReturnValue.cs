using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.MP.Define.MediaFile
{
    public class QuiryMediaFileListReturnValue : ReturnValue
    {
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

        /// <summary>
        /// 素材列表
        /// </summary>
        [JsonProperty("item")]
        public List<QuiryResultItem> ItemList { set; get; }


        public class QuiryResultItem
        {
            /// <summary>
            /// 这篇图文消息素材的MediaID
            /// </summary>
            [JsonProperty("media_id")]
            public string MediaID { set; get; }

            /// <summary>
            /// 文件名称
            /// </summary>
            [JsonProperty("name")]
            public string Name { set; get; }

            /// <summary>
            /// 当获取的列表是图片素材列表时，该字段是图片的URL
            /// </summary>
            [JsonProperty("url")]
            public string Url { set; get; }


            [JsonProperty("update_time")]
            private long updateTime;
            /// <summary>
            /// 这篇图文消息素材的最后更新时间
            /// </summary>
            public DateTime UpdateTime { get { return DateTimeConverter.GetDateTimeFromXml(updateTime); } }
        }

    }
}
