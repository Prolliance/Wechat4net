using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.MediaFile
{
    /// <summary>
    /// 查询永久素材个数返回类型
    /// </summary>
    public class QuiryCountReturnValue : ReturnValue
    {
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
        /// 永久图文素材个数
        /// </summary>
        [JsonProperty("news_count")]
        public int NewsCount { set; get; }

    }
}
