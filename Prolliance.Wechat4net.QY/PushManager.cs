using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

using Wechat4net.QY.Business;
using Wechat4net.QY.Common;
using Wechat4net.QY.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    /// <summary>
    /// 推送消息管理类
    /// </summary>
    public static class PushManager
    {
        #region 辅助类
        #endregion

        #region Push 推送消息
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="message">消息内容 推送不同类型消息 传入相应类型对象</param>
        /// <returns>返回值</returns>
        public static PushMessageReturnValue Push(Wechat4net.QY.Define.PushMessage.Base message)
        {
            string json = PushMessageBuilder.BuildJson(message);
            string url = ServiceUrl.PushMessage + "?access_token=" + AccessToken.Value;
            //string result = Create().WebClient.UploadString(url, json);
            //return AjaxEngine.Gloabl.Serializer.Deserialize<PushMessageReturnValue>(result);
            return HttpHelper.Post<PushMessageReturnValue>(url, json);
        }
        #endregion
    }
}
