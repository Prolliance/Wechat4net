using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Common
{
    internal class ServiceUrl
    {
        public static readonly string GetToken = "https://api.weixin.qq.com/cgi-bin/token";
        public static readonly string PushMessageByGroupID = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall";
        public static readonly string PushMessageByOpenID = "https://api.weixin.qq.com/cgi-bin/message/mass/send";
        public static readonly string DeleteMessage = "https://api.weixin.qq.com/cgi-bin/message/mass/delete";
        public static readonly string PreviewMessage = "https://api.weixin.qq.com/cgi-bin/message/mass/preview";
        public static readonly string UploadMediaFile = "https://api.weixin.qq.com/cgi-bin/material/add_material";
        public static readonly string UploadMediaFile_Temp = "https://api.weixin.qq.com/cgi-bin/media/upload";
        public static readonly string UploadNews = "https://api.weixin.qq.com/cgi-bin/material/add_news";
        public static readonly string UploadNews_Temp = "https://api.weixin.qq.com/cgi-bin/media/uploadnews";
        public static readonly string DeleteMediaFile = " https://api.weixin.qq.com/cgi-bin/material/del_material";
        public static readonly string GetForeverMediaFileCount = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount";
        public static readonly string GetForeverMediaFileList = "https://api.weixin.qq.com/cgi-bin/material/batchget_material";
        public static readonly string UpdateForeverNews = "https://api.weixin.qq.com/cgi-bin/material/update_news";
        public static readonly string DownloadTempMediaFile = "https://api.weixin.qq.com/cgi-bin/media/get";
        public static readonly string DownloadForeverMediaFile = "https://api.weixin.qq.com/cgi-bin/material/get_material";

        public static readonly string GetWebAccessToken = "https://api.weixin.qq.com/sns/oauth2/access_token";
        public static readonly string GetWebUserInfo = "https://api.weixin.qq.com/sns/userinfo";
    }
}
