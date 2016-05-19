using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.MP.Define.MediaFile
{
    public class QuiryVideoReturnValue : ReturnValue
    {
        [JsonProperty("title")]
        public string Title { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("down_url")]
        public string DownloadUrl { set; get; }

    }
}
