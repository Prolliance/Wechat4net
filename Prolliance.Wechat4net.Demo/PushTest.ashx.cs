using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Wechat4net.QY;
using Wechat4net.QY.Define.PushMessage;

namespace Wechat4net.Demo
{
    /// <summary>
    /// PushTest 的摘要说明
    /// </summary>
    public class PushTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string userIDs = context.Request.QueryString["UserID"] ?? string.Empty;
            string partIDs = context.Request.QueryString["PartID"] ?? string.Empty;
            string tagIDs = context.Request.QueryString["TagID"] ?? string.Empty;

            List<string> toUser = new List<string>();//要推送的用户列表
            List<string> toPart = new List<string>();//要推送的部门列表
            List<string> toTag = new List<string>();//要推送的标签列表

            if (!string.IsNullOrWhiteSpace(userIDs)) toUser = userIDs.Split('|').ToList();
            if (!string.IsNullOrWhiteSpace(partIDs)) toPart = userIDs.Split('|').ToList();
            if (!string.IsNullOrWhiteSpace(tagIDs)) toTag = userIDs.Split('|').ToList();

            Text msg = new Text(ConfigurationManager.AppSettings["AppID"], toUser, toPart, toTag, "推送一条文字信息", false);
            var ret = PushManager.Push(msg);

            context.Response.ContentType = "text/html";
            context.Response.Write("ErrorCode = " + ret.ErrorCode + "<br/>");
            context.Response.Write("ErrorMessage = " + ret.ErrorMessage + "<br/>");
            context.Response.Write("InvalidUser = " + ret.InvalidUser + "<br/>");
            context.Response.Write("InvalidParty = " + ret.InvalidParty + "<br/>");
            context.Response.Write("InvalidTag = " + ret.InvalidTag + "<br/>");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}