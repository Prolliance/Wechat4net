using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PushMessage = Wechat4net.MP.Define.PushMessage;

namespace Wechat4net.MP.Business
{
    /// <summary>
    /// 推送消息构建类
    /// </summary>
    internal static class PushMessageBuilder
    {
        #region 辅助类
        #endregion

        /// <summary>
        /// 构建根据群组ID群发消息的Json内容
        /// </summary>
        /// <param name="message"></param>
        /// <param name="groupId"></param>
        /// <param name="isToAll"></param>
        /// <returns></returns>
        public static string BuildPushJsonByGroupID(PushMessage.Base message, string groupId, bool isToAll)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("filter", new { is_to_all = isToAll, group_id = groupId });
            dic.Add(message.MsgType, message.GetData());
            dic.Add("msgtype", message.MsgType);

            return JsonConvert.SerializeObject(dic);
        }

        /// <summary>
        /// 构建根据OpenID群发消息的Json内容
        /// </summary>
        /// <param name="message"></param>
        /// <param name="openIdList"></param>
        /// <returns></returns>
        public static string BuildPushJsonByOpenID(PushMessage.Base message, List<string> openIdList)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("touser", openIdList);
            dic.Add(message.MsgType, message.GetData());
            dic.Add("msgtype", message.MsgType);

            return JsonConvert.SerializeObject(dic);
        }

        /// <summary>
        /// 构建删除群发消息的Json内容
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static string BuildDeleteJson(string messageId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("msg_id", messageId);
            return JsonConvert.SerializeObject(dic);
        }

        /// <summary>
        /// 构建预览消息的Json内容
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toOpenId">接收消息用户对应该公众号的openid，也可以使用toWxName，以实现对微信号的预览</param>
        /// <param name="toWxName">接收消息用户的微信号</param>
        /// <returns></returns>
        public static string BuildPreviewJson(PushMessage.Base message, string toOpenId, string toWxName)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(toWxName))
            {
                dic.Add("towxname", toWxName);
            }
            string msgType = message.MsgType == "video" ? "mpvideo" : message.MsgType;

            dic.Add("touser", toOpenId);
            dic.Add(msgType, message.GetData());
            dic.Add("msgtype", msgType);

            return JsonConvert.SerializeObject(dic);
        }


    }
}
