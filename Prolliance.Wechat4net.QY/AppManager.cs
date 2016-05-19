using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.QY.Common;
using Wechat4net.QY.Define.PushMessage;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    /// <summary>
    /// 企业号应用管理类
    /// </summary>
    public class AppManager
    {
        #region 辅助类
        private static Logger Logger
        {
            get
            {
                Logger _Logger = new Logger(AppSettings.LogPath);
                return _Logger;
            }
        }

        #endregion

        public static AppInfo GetAppInfo(int appId)
        {
            string url = string.Format("{0}?access_token={1}&agentid={2}", ServiceUrl.GetAppInfo, AccessToken.Value, appId);
            return HttpHelper.Get<AppInfo>(url); //Create().Get<AppInfo>(url);
        }
    }
}
