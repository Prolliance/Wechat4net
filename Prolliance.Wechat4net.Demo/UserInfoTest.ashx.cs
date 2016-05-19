using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Wechat4net.Demo
{
    /// <summary>
    /// UserInfoTest 的摘要说明
    /// </summary>
    public class UserInfoTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string code = context.Request.QueryString["code"] ?? string.Empty;
            string state = context.Request.QueryString["state"] ?? string.Empty;
            context.Response.Write("code = " + code + "<br/>");
            context.Response.Write("state = " + state + "<br/> <br/>");

            if (!string.IsNullOrWhiteSpace(code))
            {
                var userInfo = Wechat4net.QY.UserProvider.GetUserInfo(code);//ConfigurationManager.AppSettings["AppID"]);
                if (userInfo != null)
                {
                    context.Response.Write("Name = " + userInfo.Name + "<br/>");
                    context.Response.Write("UserID = " + userInfo.UserID + "<br/>");
                    context.Response.Write("WeixinID = " + userInfo.WeixinID + "<br/>");
                    context.Response.Write("Gender = " + userInfo.Gender + " (0表示未定义，1表示男性，2表示女性)" + "<br/>" + "<br/>");

                    if (!string.IsNullOrWhiteSpace(userInfo.UserID))
                    {
                        var loactionInfo = Wechat4net.QY.UserProvider.GetUserLocation(userInfo.UserID);

                        context.Response.Write("Longitude = " + loactionInfo.Longitude + "<br/>");
                        context.Response.Write("Latitude = " + loactionInfo.Latitude + "<br/>");
                        context.Response.Write("Precision = " + loactionInfo.Precision + "<br/>");
                    }
                }
            }
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