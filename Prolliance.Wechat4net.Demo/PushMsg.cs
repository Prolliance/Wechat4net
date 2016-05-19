using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Wechat4net.QY;
using Wechat4net.QY.Define;
using PushMessage = Wechat4net.QY.Define.PushMessage;

namespace Wechat4net.Demo
{
    public class PushMsg
    {
        public void push()
        {
            List<string> toUser = new List<string>();//要推送的用户列表
            List<string> toPart = new List<string>();//要推送的部门列表
            List<string> toTag = new List<string>();//要推送的标签列表

            //实例化 相应类型的新闻对象

            //文字类型
            PushMessage.Text msg = new PushMessage.Text(ConfigurationManager.AppSettings["AppID"], toUser, toPart, toTag, "推送一条文字信息", false);

            //图片类型
            //PushMessage.Image msg = new PushMessage.Image(ConfigurationManager.AppSettings["AppID"], toUser, toPart, toTag, "mediaId", true);

            //新闻类型
            //List<PushMessage.News.NewsItem> newsList = new List<PushMessage.News.NewsItem>();
            //newsList.Add(new PushMessage.News.NewsItem() { title = "标题", description = "摘要", picUrl = "图片url", url = "详情链接url" });
            //PushMessage.News msg = new PushMessage.News(ConfigurationManager.AppSettings["AppID"], toUser, toPart, toTag, newsList);

            //其他类型相同
            //......

            //调用PushManager.Push()静态方法进行消息推送
            try
            {
                PushManager.Push(msg);
            }
            catch (Exception ex)//ex.Message 异常信息
            {
                //todo...
                //...
            }

        }
    }
}