using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wechat4net.Demo
{
    /// <summary>
    /// JsApiTest 的摘要说明
    /// </summary>
    public class JsApiTest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var ret = Wechat4net.QY.JsApi.GetJsConfig(context.Request.Url.ToString());
            //context.Response.ContentType = "text/json";
            context.Response.Write(JsonConvert.SerializeObject(ret));
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