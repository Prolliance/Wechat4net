using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat4net.Demo
{
    /// <summary>
    /// Download 的摘要说明
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string mediaId = context.Request.QueryString["mediaid"];
            bool istemp = context.Request.QueryString["istemp"] == "1";
            int appId = 6;


            if (istemp)
            {
                Wechat4net.QY.MediaFileManager.DownloadTempFile(mediaId);
            }
            else
            {
                Wechat4net.QY.MediaFileManager.DownloadForeverFile(mediaId, appId);
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