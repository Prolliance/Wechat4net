using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxEngine.AjaxHandlers;
using AjaxEngine.Utils;

namespace Wechat4net.Demo
{
    /// <summary>
    /// TestService 的摘要说明
    /// </summary>
    [Summary(Name = "Wechat4net测试服务", Description = "对API进行测试")]
    public class TestService : AjaxHandlerBase
    {
        [AjaxMethod]
        [Summary(Description = "", Parameters = "", Return = "")]
        public object GetForeverMediaFileCount()
        {
            return Wechat4net.QY.MediaFileManager.GetForeverMediaFileCount(6);
        }

        [AjaxMethod]
        [Summary(Description = "", Parameters = "", Return = "")]
        public object GetForeverMediaFileList(string fileType, int offset, int count)
        {
            return Wechat4net.QY.MediaFileManager.GetForeverMediaFileList(fileType, offset, count, 6);
        }

        [AjaxMethod]
        [Summary(Description = "", Parameters = "", Return = "")]
        public object GetUserList(int deptId, bool isIncludeChildDept, int status, bool isDetail)
        {
            return Wechat4net.QY.ContactManager.GetUserList(deptId, isIncludeChildDept, status, isDetail);
        }

        [AjaxMethod]
        [Summary(Description = "", Parameters = "", Return = "")]
        public object GetUserInfo(string userId)
        {
            return Wechat4net.QY.ContactManager.GetUserInfo(userId);
        }

        [AjaxMethod]
        [Summary(Description = "", Parameters = "", Return = "")]
        public object GetAppInfo(int appId)
        {
            return Wechat4net.QY.AppManager.GetAppInfo(appId);
        }
    }
}