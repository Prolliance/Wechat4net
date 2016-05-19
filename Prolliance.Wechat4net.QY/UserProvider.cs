using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

using Wechat4net.QY.Common;
using Wechat4net.QY.Define;
using Wechat4net.QY.Define.ReceiveMessage;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    /// <summary>
    /// 用户信息提供类
    /// </summary>
    public static class UserProvider
    {
        private class RetValue
        {
            [JsonProperty("errcode")]
            public int ErrorCode { set; get; }
            [JsonProperty("errmsg")]
            public string ErrorMessage { set; get; }
            [JsonProperty("UserId")]
            public string UserID { set; get; }
            [JsonProperty("DeviceId")]
            public string DeviceID { set; get; }
        }

        /// <summary>
        /// 通过微信重定向跳转后获取到的身份标识，获取当前用户在企业通讯录中的信息
        /// 微信接口：https://open.weixin.qq.com/connect/oauth2/authorize?appid=CORPID&amp;redirect_uri=REDIRECT_URI&amp;response_type=code&amp;scope=SCOPE&amp;state=STATE#wechat_redirect
        /// </summary>
        /// <param name="code">通过微信提供的重定向跳转获取到的用户身份标识，code参数。</param>
        /// <returns>用户企业通讯录信息</returns>
        public static WechatUser GetUserInfo(string code)
        {
            if (!string.IsNullOrWhiteSpace(AccessToken.Value))
            {
                //用access_token、code获取UserId
                string url = string.Format("{0}?access_token={1}&code={2}", ServiceUrl.GetUserID, AccessToken.Value, code);
                RetValue rs = HttpHelper.Get<RetValue>(url);

                if (rs.ErrorCode != 0)
                {
                    throw new Exception("根据code获取成员信息失败,ErrorCode:" + rs.ErrorCode.ToString() + "; ErrorMessage:" + rs.ErrorMessage);
                }

                if (string.IsNullOrEmpty(rs.UserID))
                {
                    throw new Exception("获取userId失败");
                }


                return Wechat4net.QY.ContactManager.GetUserInfo(rs.UserID);

                /*
                //用UserId获取人员信息
                //url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=" + AccessToken.Value + "&userid=" + rs.UserID;
                var data2 = new
                {
                    access_token = AccessToken.Value,
                    userid = rs.UserID
                };
                WechatUser user = Create().Get<WechatUser>(ServiceUrl.GetUserInfo, data2);

                if (user.ErrorCode != 0)
                {
                    throw new Exception("获取微信用户信息错误,ErrorCode:" + user.ErrorCode.ToString() + "; ErrorMessage:" + user.ErrorMessage);
                }
                return user;
                */
            }
            else
            {
                throw new Exception("获取access_token失败");
            }
        }

        /// <summary>
        /// 根据UserID获取该用户当前位置信息。
        /// 调用该方法需要 1、在微信管理平台开启回调模式上报地理位置功能；2、将CallbackManager.enablePositionRecord设为True。
        /// </summary>
        /// <param name="userId">用户ID（用户在企业通讯录中的唯一ID）</param>
        /// <returns>上报地理位置事件所接受的信息</returns>
        public static EventLocation GetUserLocation(string userId)
        {
            return HttpContext.Current.Cache.Get(CacheKey.UserLocation + userId) as EventLocation;
        }

    }
}
