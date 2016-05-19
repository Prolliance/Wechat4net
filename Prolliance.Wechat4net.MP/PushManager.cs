using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wechat4net.Define;
using Wechat4net.Utils;
using Wechat4net.MP.Business;
using Wechat4net.MP.Common;
using Wechat4net.MP.Define;
using PushMessage = Wechat4net.MP.Define.PushMessage;

namespace Wechat4net.MP
{
    public class PushManager
    {
        #region 辅助类
        //private static Client Create()
        //{
        //    Client client = new Client();
        //    client.WebClient.Encoding = Encoding.UTF8;
        //    client.WebClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
        //    return client;
        //}
        #endregion

        /// <summary>
        /// 根据分组进行群发【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <param name="groupId">群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id</param>
        /// <param name="isToAll">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <returns></returns>
        public static PushMessageReturnValue PushMessageByGroupID(PushMessage.Base message, string groupId, bool isToAll)
        {
            string json = PushMessageBuilder.BuildPushJsonByGroupID(message, groupId, isToAll);
            string url = ServiceUrl.PushMessageByGroupID + "?access_token=" + AccessToken.Value;
            return HttpHelper.Post<PushMessageReturnValue>(url, json);
        }

        /// <summary>
        /// 根据OpenID列表群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <param name="openIdList">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static PushMessageReturnValue PushMessageByOpenID(PushMessage.Base message, List<string> openIdList)
        {
            string json = PushMessageBuilder.BuildPushJsonByOpenID(message, openIdList);
            string url = ServiceUrl.PushMessageByOpenID + "?access_token=" + AccessToken.Value;
            return HttpHelper.Post<PushMessageReturnValue>(url, json);
        }

        /// <summary>
        /// 删除群发【订阅号与服务号认证后均可用】
        /// <para>请注意，只有已经发送成功的消息才能删除删除消息只是将消息的图文详情页失效，已经收到的用户，还是能在其本地看到消息卡片。</para>
        /// <para>另外，删除群发消息只能删除图文消息和视频消息，其他类型的消息一经发送，无法删除。</para>
        /// </summary>
        /// <param name="messageID">发送出去的消息ID</param>
        /// <returns>删除结果</returns>
        public static ReturnValue DeleteMessage(string messageID)
        {
            string json = PushMessageBuilder.BuildDeleteJson(messageID);
            string url = ServiceUrl.DeleteMessage + "?access_token=" + AccessToken.Value;
            return HttpHelper.Post<ReturnValue>(url, json);
        }

        /// <summary>
        /// 预览消息
        /// <para>toWxName和toOpenId同时赋值时，以toWxName优先</para>
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <param name="toOpenId">接收消息用户对应该公众号的openid，也可以使用toWxName，以实现对微信号的预览</param>
        /// <param name="toWxName">接收消息用户的微信号</param>
        /// <returns></returns>
        public static PushMessageReturnValue PreviewMessage(PushMessage.Base message, string toOpenId, string toWxName)
        {
            string json = PushMessageBuilder.BuildPreviewJson(message, toOpenId, toWxName);
            string url = ServiceUrl.PreviewMessage + "?access_token=" + AccessToken.Value;
            return HttpHelper.Post<PushMessageReturnValue>(url, json);
        }

    }
}
