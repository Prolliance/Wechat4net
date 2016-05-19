using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Newtonsoft.Json;
using Wechat4net.MP.Common;
using Wechat4net.MP.Define;
using Wechat4net.MP.Define.ReceiveMessage;
using Wechat4net.MP.Utils;
using Wechat4net.Utils;

namespace Wechat4net.MP
{
    /// <summary>
    /// 用户信息提供类
    /// </summary>
    public static class UserProvider
    {
        /// <summary>
        /// 首先请注意，这里通过code换取的是一个特殊的网页授权access_token,与基础支持中的access_token（该access_token用于调用其他接口）不同。
        /// <para>公众号可通过下述接口来获取网页授权access_token。如果网页授权的作用域为snsapi_base，则本步骤中获取到网页授权access_token的同时，也获取到了openid，snsapi_base式的网页授权流程即到此为止。</para>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static WebAccessTokenReturnValue GetWebAccessToken(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new Exception("code参数不能为空");

            //var data = new
            //{
            //    appid = WechatConfig.AppID, //Wechat4net.MP.Wechat.Options.AppID,
            //    secret = WechatConfig.Secret, //Wechat4net.MP.Wechat.Options.Secret,
            //    code = code,
            //    grant_type = "authorization_code"
            //};

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("appid", WechatConfig.AppID);
            data.Add("secret", WechatConfig.Secret);
            data.Add("code", code);
            data.Add("grant_type", "authorization_code");

            WebAccessTokenReturnValue rs = HttpHelper.Get<WebAccessTokenReturnValue>(ServiceUrl.GetWebAccessToken, data);

            if (rs == null) throw new Exception("获取WebAccessToken失败");
            if (rs.ErrorCode != 0) throw new Exception("获取WebAccessToken失败。ErrorCode = " + rs.ErrorCode + ", ErrorMessage = " + rs.ErrorMessage);

            return rs;
        }

        public static string GetUserOpenID(string code)
        {
            return GetWebAccessToken(code).OpenID;
        }

        public static Wechat4net.MP.Define.WechatUser GetUserInfo(string code)
        {
            WebAccessTokenReturnValue ret = GetWebAccessToken(code);
            //var data = new
            //{
            //    access_token = ret.WebAccessToken,
            //    openid = ret.OpenID,
            //    lang = WechatConfig.Language.ToString() //Wechat4net.MP.Wechat.Options.Language.ToString()
            //};

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("access_token", ret.WebAccessToken);
            data.Add("openid", ret.OpenID);
            data.Add("lang", WechatConfig.Language.ToString());

            WechatUser user = HttpHelper.Get<WechatUser>(ServiceUrl.GetWebUserInfo, data);
            if (user.ErrorCode != 0)
            {
                throw new Exception("获取微信用户信息错误,ErrorCode:" + user.ErrorCode.ToString() + "; ErrorMessage:" + user.ErrorMessage);
            }
            return user;
        }

        /// <summary>
        /// 根据UserID获取该用户当前位置信息。
        /// 调用该方法需要 1、在微信管理平台开启回调模式上报地理位置功能；2、将CallbackManager.enablePositionRecord设为True。
        /// </summary>
        /// <param name="openId">用户的OpenID</param>
        /// <returns>上报地理位置事件所接受的信息</returns>
        public static EventLocation GetUserLocation(string openId)
        {
            return HttpContext.Current.Cache.Get(CacheKey.UserLocation + openId) as EventLocation;
        }

    }
}
