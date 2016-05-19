using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;
using Wechat4net.MP.Define.MediaFile;
namespace Wechat4net.MP.Define.MediaFile
{
    /// <summary>
    /// 上传媒体文件返回类型
    /// </summary>
    public class UploadReturnValue : ReturnValue
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
        public Utils.FileType Type
        {
            get { return Utils.GetFileTypeEnum(this.TypeString); }
            set { this.TypeString = Utils.GetFileTypeString(value); }
        }
        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaID { set; get; }

        /// <summary>
        /// 上传的图片素材的图片URL（仅上传图片永久素材时会返回该字段）
        /// </summary>
        [JsonProperty("url")]
        public string Url { set; get; }


        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { set; get; }

        public UploadReturnValue() { }

        internal UploadReturnValue(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
