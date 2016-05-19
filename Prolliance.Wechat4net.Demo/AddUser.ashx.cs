using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechat4net.QY.Define;

namespace Wechat4net.Demo
{
    /// <summary>
    /// AddUser 的摘要说明
    /// </summary>
    public class AddUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            WechatUser wachatuser = new WechatUser();
            wachatuser.Name = "test";
            wachatuser.UserID = "test001";
            wachatuser.Mobile = "13800138001";
            wachatuser.Email = "test001@prolliance.cn";
            wachatuser.Department = new List<int> { 1 };
            wachatuser.Gender = "0";
            wachatuser.Position = "测试账号";
            wachatuser.WeixinID = "";

            var ret = Wechat4net.QY.ContactManager.CreateUser(wachatuser);

            context.Response.ContentType = "text/plain";
            context.Response.Write(ret.ErrorCode + " || " + ret.ErrorMessage);
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