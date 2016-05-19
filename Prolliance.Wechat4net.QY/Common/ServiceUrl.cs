using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.QY.Common
{
    internal class ServiceUrl
    {
        public static readonly string GetToken = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
        public static readonly string PushMessage = "https://qyapi.weixin.qq.com/cgi-bin/message/send";
        public static readonly string GetUserID = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo";
        public static readonly string CreateUser = "https://qyapi.weixin.qq.com/cgi-bin/user/create";
        public static readonly string GetUserInfo = "https://qyapi.weixin.qq.com/cgi-bin/user/get";
        public static readonly string UpdateUserInfo = "https://qyapi.weixin.qq.com/cgi-bin/user/update";
        public static readonly string DeleteUser = "https://qyapi.weixin.qq.com/cgi-bin/user/delete";
        public static readonly string UploadMediaFile_Temp = "https://qyapi.weixin.qq.com/cgi-bin/media/upload";
        public static readonly string UploadMediaFile_Forever = "https://qyapi.weixin.qq.com/cgi-bin/material/add_material";
        public static readonly string UploadMpNews_Forever = "https://qyapi.weixin.qq.com/cgi-bin/material/add_mpnews";
        public static readonly string UpdataMpNews = "https://qyapi.weixin.qq.com/cgi-bin/material/update_mpnews";
        public static readonly string DeleteMedia = "https://qyapi.weixin.qq.com/cgi-bin/material/del";
        public static readonly string DownloadMediaFile_Temp = "https://qyapi.weixin.qq.com/cgi-bin/media/get";
        public static readonly string DownloadMediaFile_Forever = "https://qyapi.weixin.qq.com/cgi-bin/material/get";
        public static readonly string GetAllMediaFilesCount = "https://qyapi.weixin.qq.com/cgi-bin/material/get_count";
        public static readonly string GetMediaList = "https://qyapi.weixin.qq.com/cgi-bin/material/batchget";
        public static readonly string ReplaceDept = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceparty";
        public static readonly string ReplaceUser = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceuser";
        public static readonly string SyncUser = "https://qyapi.weixin.qq.com/cgi-bin/batch/syncuser";

        public static readonly string CrateDept = "https://qyapi.weixin.qq.com/cgi-bin/department/create";
        public static readonly string GetDeptList = "https://qyapi.weixin.qq.com/cgi-bin/department/list";
        public static readonly string UpdateDeptInfo = "https://qyapi.weixin.qq.com/cgi-bin/department/update";
        public static readonly string DeleteDept = "https://qyapi.weixin.qq.com/cgi-bin/department/delete";
        
        public static readonly string GetUserList = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist";
        public static readonly string GetUserInfoList = "https://qyapi.weixin.qq.com/cgi-bin/user/list";
        public static readonly string GetUserListByTag = "https://qyapi.weixin.qq.com/cgi-bin/tag/get";

        public static readonly string GetAppInfo = "https://qyapi.weixin.qq.com/cgi-bin/agent/get";

        public static readonly string GetJsApiTicket = "https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket";
    }
}
