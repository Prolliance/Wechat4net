using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Wechat4net.QY;
using Wechat4net.QY.Define;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    public class Global : System.Web.HttpApplication
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

        protected void Application_Start(object sender, EventArgs e)
        {
            //微信框架初始化
            //Wechat.Init(new WechatOption
            //{
            //    CorpID = ConfigurationManager.AppSettings["CorpID"],
            //    Secret = ConfigurationManager.AppSettings["Secret"],
            //    Token = ConfigurationManager.AppSettings["Token"],
            //    EncodingAESKey = ConfigurationManager.AppSettings["EncodingAESKey"]
            //});
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Logger.Error(ex);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}