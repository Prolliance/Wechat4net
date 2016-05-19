using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechat4net.QY;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    /// <summary>
    /// ImHandler 的摘要说明
    /// </summary>
    public class ImHandler : IHttpHandler
    {
        private Logger _Logger = null;
        public Logger Logger
        {
            get
            {
                if (_Logger == null)
                {
                    _Logger = new Logger(HttpContext.Current.Server.MapPath(".") + "\\log");
                }
                return _Logger;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            Logger.Log("【IM】" + context.Request.Url);

            CallbackManager cm = new CallbackManager();
            cm.Processing();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
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