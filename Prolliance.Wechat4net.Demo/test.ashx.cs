using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Wechat4net.MP.Business;
using Wechat4net.QY;
using Wechat4net.QY.Define;
using Wechat4net.QY.Define.ReceiveMessage;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class test : IHttpHandler
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
            context.Response.Write(context.Request.QueryString["code"]);
            return; 
            Wechat4net.MP.Define.ReplyMessage.Text text = new MP.Define.ReplyMessage.Text("to", "from", "hello");
            var ret = EntityHelper.BuidReplyMessageXml(text);
            context.Response.Write(ret.ToString());
            return;


            List<string> xmlList = new List<string>();
            xmlList.Add(@"<xml><ToUserName><![CDATA[wxd108e1a8668594b5]]></ToUserName>
            <FromUserName><![CDATA[sunzhen]]></FromUserName>
            <CreateTime>1433400237</CreateTime>
            <MsgType><![CDATA[location]]></MsgType>
            <Location_X>40.019886</Location_X>
            <Location_Y>116.350304</Location_Y>
            <Scale>15</Scale>
            <Label><![CDATA[北京市海淀区汇智大厦西南(学清路西)]]></Label>
            <MsgId>4445318561841283104</MsgId>
            <AgentID>6</AgentID>
            </xml>");

            xmlList.Add(@"<xml><ToUserName><![CDATA[wxd108e1a8668594b5]]></ToUserName>
            <FromUserName><![CDATA[sys]]></FromUserName>
            <CreateTime>1433403448</CreateTime>
            <MsgType><![CDATA[event]]></MsgType>
            <Event><![CDATA[batch_job_result]]></Event>
            <BatchJob><JobId><![CDATA[djC65ZT7wJ_FKd10fc01itC-6vjvUnl779h5yOBPKyU]]></JobId>
            <JobType><![CDATA[sync_user]]></JobType>
            <ErrCode>2</ErrCode>
            <ErrMsg><![CDATA[ok]]></ErrMsg>
            </BatchJob>
            </xml>");
            int index = 0;
            foreach (var xml in xmlList)
            {
                XmlDocument d = new XmlDocument();
                d.LoadXml(xml);

                switch (index)
                {
                    case 0:
                        Location location = new Location();
                        Wechat4net.Utils.EntityHelper.FillReceiveMessageFromXml(location, d);
                        break;
                    case 1:
                        EventBatchJobResult job = new EventBatchJobResult();
                        Wechat4net.Utils.EntityHelper.FillReceiveMessageFromXml(job, d);
                        Logger.Log(job.JobID);
                        break;
                    default:
                        break;
                }
                index++;
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