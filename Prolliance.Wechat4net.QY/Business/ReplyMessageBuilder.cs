using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wechat4net.Utils;

namespace Wechat4net.QY.Business
{
    /// <summary>
    /// 回复消息构建类
    /// </summary>
    internal class ReplyMessageBuilder
    {
        #region BuildXml
        /// <summary>
        /// 构建回复XML字符串
        /// </summary>
        /// <param name="returnMessage">回复消息对象</param>
        /// <returns>未加密的回复消息XML数据</returns>
        public static string BuildXml(Wechat4net.QY.Define.ReplyMessage.Base returnMessage)
        {
            if (returnMessage == null)
            {
                return "";
            }
            if (returnMessage.messageType == Wechat4net.Utils.Enums.QY.ReplyMessageEnum.Unknow)
            {
                throw new Exception("未知的返回消息类型");
            }

            return returnMessage.BuidReplyMessageXmlString();

            //switch (returnMessage.messageType)
            //{
            //    case ReplyMessageEnum.Unknow:
            //        throw new Exception("未知的返回消息类型");

            //    case ReplyMessageEnum.Text:
            //        Text text = returnMessage as Text;
            //        return BuildTextXml(text);

            //    case ReplyMessageEnum.Image:
            //        Image image = returnMessage as Image;
            //        return BuildImageXml(image);

            //    case ReplyMessageEnum.Voice:
            //        Voice voice = returnMessage as Voice;
            //        return BuildVoiceXml(voice);

            //    case ReplyMessageEnum.Video:
            //        Video video = returnMessage as Video;
            //        return BuildVideoXml(video);

            //    case ReplyMessageEnum.News:
            //        News news = returnMessage as News;
            //        return BuildNewsXml(news);

            //    default:
            //        throw new Exception("未知的返回消息类型");
            //}
        }
        #endregion

        //#region 内部方法

        //#region BuildTextXml
        //private static string BuildTextXml(Text text)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<xml><ToUserName><![CDATA[").Append(text.ToUserName).Append("]]></ToUserName>");
        //    sb.Append("<FromUserName><![CDATA[").Append(text.FromUserName).Append("]]></FromUserName>");
        //    sb.Append("<CreateTime>").Append(DateTime.Now.ToString("yyyyMMdd")).Append("</CreateTime>");
        //    sb.Append("<MsgType><![CDATA[text]]></MsgType>");
        //    sb.Append("<Content><![CDATA[").Append(text.Content).Append("]]></Content></xml>");

        //    return sb.ToString();
        //}
        //#endregion

        //#region BuildImageXml
        //private static string BuildImageXml(Image image)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<xml><ToUserName><![CDATA[").Append(image.ToUserName).Append("]]></ToUserName>");
        //    sb.Append("<FromUserName><![CDATA[").Append(image.FromUserName).Append("]]></FromUserName>");
        //    sb.Append("<CreateTime>").Append(DateTime.Now.ToString("yyyyMMdd")).Append("</CreateTime>");
        //    sb.Append("<MsgType><![CDATA[image]]></MsgType><Image>");
        //    sb.Append("<MediaId><![CDATA[").Append(image.MediaID).Append("]]></MediaId></Image></xml>");

        //    return sb.ToString();
        //}
        //#endregion

        //#region BuildVoiceXml
        //private static string BuildVoiceXml(Voice voice)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<xml><ToUserName><![CDATA[").Append(voice.ToUserName).Append("]]></ToUserName>");
        //    sb.Append("<FromUserName><![CDATA[").Append(voice.FromUserName).Append("]]></FromUserName>");
        //    sb.Append("<CreateTime>").Append(DateTime.Now.ToString("yyyyMMdd")).Append("</CreateTime>");
        //    sb.Append("<MsgType><![CDATA[voice]]></MsgType><Voice>");
        //    sb.Append("<MediaId><![CDATA[").Append(voice.MediaID).Append("]]></MediaId></Voice></xml>");

        //    return sb.ToString();
        //}
        //#endregion

        //#region BuildVideoXml
        //private static string BuildVideoXml(Video video)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<xml><ToUserName><![CDATA[").Append(video.ToUserName).Append("]]></ToUserName>");
        //    sb.Append("<FromUserName><![CDATA[").Append(video.FromUserName).Append("]]></FromUserName>");
        //    sb.Append("<CreateTime>").Append(DateTime.Now.ToString("yyyyMMdd")).Append("</CreateTime>");
        //    sb.Append("<MsgType><![CDATA[video]]></MsgType>");
        //    sb.Append("<Video>");
        //    sb.Append("<MediaId><![CDATA[").Append(video.MediaID).Append("]]></MediaId>");
        //    sb.Append("<Title><![CDATA[").Append(video.Title).Append("]]></Title>");
        //    sb.Append("<Description><![CDATA[").Append(video.Description).Append("]]></Description>");
        //    sb.Append("</Video></xml>");

        //    return sb.ToString();
        //}
        //#endregion

        //#region BuildNewsXml
        ///// <summary>
        ///// 构造新闻类型返回消息XML
        ///// </summary>
        ///// <param name="news">新闻对象</param>
        ///// <returns>未加密的XML字符串</returns>
        //private static string BuildNewsXml(News news)//string toUserName, string fromUserName, List<Define.News> newsList)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<xml><ToUserName><![CDATA[").Append(news.ToUserName).Append("]]></ToUserName>");
        //    sb.Append("<FromUserName><![CDATA[").Append(news.FromUserName).Append("]]></FromUserName>");
        //    sb.Append("<CreateTime>").Append(DateTime.Now.ToString("yyyyMMdd")).Append("</CreateTime>");
        //    sb.Append("<MsgType><![CDATA[").Append(news.MsgType).Append("]]></MsgType>");
        //    sb.Append("<ArticleCount>").Append(news.ArticleCount).Append("</ArticleCount>");
        //    sb.Append("<Articles>");
        //    foreach (News.NewsItem item in news.NewsList)
        //    {
        //        sb.Append("<item><Title><![CDATA[").Append(item.Title).Append("]]></Title>");
        //        sb.Append("<Description><![CDATA[").Append(item.Description).Append("]]></Description>");
        //        sb.Append("<PicUrl><![CDATA[").Append(item.PicUrl).Append("]]></PicUrl>");
        //        sb.Append("<Url><![CDATA[").Append(item.Url).Append("]]></Url></item>");
        //    }
        //    sb.Append("</Articles></xml>");

        //    return sb.ToString();
        //}
        //#endregion

        //#endregion
    }
}
