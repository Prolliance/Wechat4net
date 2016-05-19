using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Wechat4net.QY.Common;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    public class JsApi
    {
        public static object GetJsConfig(string url)
        {
            //HttpContext context = HttpContext.Current;

            //Logger logger = new Logger(AppSettings.LogPath);
            //logger.Info("Url = " + context.Request.Url);
            //logger.Info("UrlReferrer = " + context.Request.UrlReferrer);

            //logger.Info("ticket = " + JsApiTicket.Value);

            string noncestr = "wechat4net";
            long timestamp = DateTimeConverter.GetWeixinDateTime(DateTime.Now);
            //string url = "";
            //if (context.Request.UrlReferrer != null)
            //{
            //    url = context.Request.UrlReferrer.ToString().Split('#')[0];
            //}
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            else
            {
                url = url.Split('#')[0];
            }

            string unencrypted = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", JsApiTicket.Value, noncestr, timestamp, url);

            SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";
            try
            {
                sha = new SHA1CryptoServiceProvider();
                enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(unencrypted);
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                hash = BitConverter.ToString(dataHashed).Replace("-", "");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                return null;
            }

            //{
            //debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            //appId: '', // 必填，企业号的唯一标识，此处填写企业号corpid
            //timestamp: , // 必填，生成签名的时间戳
            //nonceStr: '', // 必填，生成签名的随机串
            //signature: '',// 必填，签名，见附录1
            //jsApiList: [] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            //}

            var config = new
            {
                debug = false,
                appId = WechatConfig.CorpID,
                timestamp = timestamp,
                nonceStr = noncestr,
                signature = hash,
                jsApiList = new List<string>()
                {
                    "checkJsApi",
                    "onMenuShareTimeline",
                    "onMenuShareAppMessage",
                    "onMenuShareQQ",
                    "onMenuShareWeibo",
                    "onMenuShareQZone",
                    "hideMenuItems",
                    "showMenuItems",
                    "hideAllNonBaseMenuItem",
                    "showAllNonBaseMenuItem",
                    "translateVoice",
                    "startRecord",
                    "stopRecord",
                    "onVoiceRecordEnd",
                    "playVoice",
                    "onVoicePlayEnd",
                    "pauseVoice",
                    "stopVoice",
                    "uploadVoice",
                    "downloadVoice",
                    "chooseImage",
                    "previewImage",
                    "uploadImage",
                    "downloadImage",
                    "getNetworkType",
                    "openLocation",
                    "getLocation",
                    "hideOptionMenu",
                    "showOptionMenu",
                    "closeWindow",
                    "scanQRCode",
                    "chooseWXPay",
                    "openProductSpecificView",
                    "addCard",
                    "chooseCard",
                    "openCard",
                    "openEnterpriseChat"
                }
            };

            return config;
        }

    }
}
