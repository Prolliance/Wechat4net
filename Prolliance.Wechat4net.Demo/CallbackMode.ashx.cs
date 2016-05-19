using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechat4net.QY;
using Wechat4net.QY.Define;
using ReceiveMessage = Wechat4net.QY.Define.ReceiveMessage;
using ReplyMessage = Wechat4net.QY.Define.ReplyMessage;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    /// <summary>
    /// CallbackMode 的摘要说明
    /// </summary>
    public class CallbackMode : IHttpHandler
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
            try
            {
                //实例化CallbackManager
                CallbackManager cm = new CallbackManager();
                cm.EnablePositionRecord = true; //开启位置记录功能
                cm.PositionRecordExpiresHours = 24; //记录缓存有效时间（可不设置，默认为24小时）
                //注册事件
                cm.ProcessingEventClick += cm_ProcessingEventClick;
                cm.ProcessingText += cm_ProcessingText;
                cm.ProcessingImage += cm_ProcessingImage;
                cm.ProcessingLocation += cm_ProcessingLocation;
                cm.ProcessingEventBatchJobResult += cm_ProcessingEventBatchJobResult;
                //.....
                //其他类型注册和处理方法相同 省略
                //只需注册需要处理的事件
                //最后执行Processing方法开始处理
                cm.Processing();
            }
            catch (Exception ex)//ex.Message 异常信息
            {
                Logger.Error(ex);
                //todo...
                //...
            }

        }

        void cm_ProcessingEventBatchJobResult(object sender, CallbackManager.ProcessingEventBatchJobResultEventArgs e)
        {
            ReceiveMessage.EventBatchJobResult rec = e.MessageData;
            Logger.Log("异步任务完成！errorCode=" + rec.ErrorCode + ",errorMessage=" + rec.ErrorMessage + ",jobID=" + rec.JobID);
        }

        void cm_ProcessingLocation(object sender, CallbackManager.ProcessingLocationEventArgs e)
        {
            ReceiveMessage.Location rec = e.MessageData;
            ReplyMessage.Text ret = new ReplyMessage.Text(rec.FromUserName, rec.ToUserName, rec.Label + ":/r/nX = " + rec.Location_X.ToString() + "/r/nY = " + rec.Location_Y + "/r/n缩放等级=" + rec.Scale.ToString());
            e.ReplyMessage = ret;
        }

        /// <summary>
        /// 菜单点击处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cm_ProcessingEventClick(object sender, CallbackManager.ProcessingEventClickEventArgs e)
        {
            //接收到的信息对象
            ReceiveMessage.EventClick rec = e.MessageData;
            //应用id。多应用公用同一个server时，可以根据此参数判断是哪个应用，从而进行相应处理
            string appID = rec.AppID;
            //按钮key值
            string key = rec.EventKey;
            //todo...
            //...
            //如果需要返回信息，则实例化相应类型的ReplyMessage对象，赋值给e.replyMessage
            //如果无需返回信息，则不用处理e.replyMessage或给其复制null

            ReceiveMessage.EventLocation location = UserProvider.GetUserLocation(rec.FromUserName);
            if (location != null)
            {
                e.ReplyMessage = new ReplyMessage.Text(rec.FromUserName, rec.ToUserName, "x:" + location.Latitude + ",Y:" + location.Longitude + ",Pre" + location.Precision);
            }
            else
            {
                e.ReplyMessage = new ReplyMessage.Text(rec.FromUserName, rec.ToUserName, "null");
            }
        }

        /// <summary>
        /// 文字信息处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cm_ProcessingText(object sender, CallbackManager.ProcessingTextEventArgs e)
        {
            //接收到的信息对象
            ReceiveMessage.Text rec = e.MessageData;
            //声明返回信息类型对象
            ReplyMessage.Text ret = new ReplyMessage.Text(rec.FromUserName, rec.ToUserName, "接收到一条文字信息：" + rec.Content);
            //将返回信息对象赋值给e.replyMessage
            e.ReplyMessage = ret;
        }

        /// <summary>
        /// 图片信息处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cm_ProcessingImage(object sender, CallbackManager.ProcessingImageEventArgs e)
        {
            //接收到的信息对象
            ReceiveMessage.Image rec = e.MessageData;
            //返回图片链接
            ReplyMessage.Text ret = new ReplyMessage.Text(rec.FromUserName, rec.ToUserName, "图片链接：" + rec.PicUrl);
            //或者返回接收的图片
            //ReplyMessage.Image ret = new ReplyMessage.Image(rec.FromUserName, rec.ToUserName, rec.MediaId);
            //将返回信息对象赋值给e.replyMessage
            e.ReplyMessage = ret;
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