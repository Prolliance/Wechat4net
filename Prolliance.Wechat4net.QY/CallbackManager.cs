using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Wechat4net.Define;
using Wechat4net.QY.Business;
using Wechat4net.QY.Define;
using ReceiveMessage = Wechat4net.QY.Define.ReceiveMessage;
using ReplyMessage = Wechat4net.QY.Define.ReplyMessage;
using ReceiveMessageEnum = Wechat4net.Utils.Enums.QY.ReceiveMessageEnum;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    /// <summary>
    /// 微信回调模式管理类
    /// </summary>
    public class CallbackManager
    {
        #region 辅助类
        private Logger _Logger = null;
        public Logger Logger
        {
            get
            {
                if (_Logger == null)
                {
                    _Logger = new Logger(AppSettings.LogPath);
                }
                return _Logger;
            }
        }

        private Tencent.WXBizMsgCrypt _Wxcpt = null;
        private Tencent.WXBizMsgCrypt Wxcpt
        {
            get
            {
                if (_Wxcpt == null)
                {
                    _Wxcpt = new Tencent.WXBizMsgCrypt(WechatConfig.Token, WechatConfig.EncodingAESKey, WechatConfig.CorpID);//(Wechat.Options.Token, Wechat.Options.EncodingAESKey, Wechat.Options.CorpID);
                }
                return _Wxcpt;
            }
        }

        #endregion

        #region 实例化
        /// <summary>
        /// 实例化CallbackManager
        /// </summary>
        public CallbackManager()
        {
            this.EnablePositionRecord = false;
            this.PositionRecordExpiresHours = 24;
        }
        #endregion

        #region 属性定义
        /// <summary>
        /// 是否启用位置记录功能，默认为false。
        /// 如果启用 则需要在微信管理平台回调模式设置中开启上报地理位置功能
        /// </summary>
        public bool EnablePositionRecord { set; get; }

        /// <summary>
        /// 位置记录有效期(小时)，默认为24小时。
        /// </summary>
        public int PositionRecordExpiresHours { set; get; }

        #endregion

        #region 委托事件定义

        public class ProcessingEventArgs : EventArgs
        {
            /// <summary>
            /// 接收信息储存字段
            /// </summary>
            protected ReceiveMessage.Base messageData;
            /// <summary>
            /// 返回信息 如果无需返回信息可不赋值或设为null
            /// </summary>
            public Wechat4net.QY.Define.ReplyMessage.Base ReplyMessage { set; get; }
        }

        #region 处理文字消息事件
        //声明委托
        public delegate void ProcessingTextEventHandler(Object sender, ProcessingTextEventArgs e);
        /// <summary>
        /// 处理文字消息事件
        /// </summary>
        public event ProcessingTextEventHandler ProcessingText;

        /// <summary>
        /// 处理文字消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingTextEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.Text MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.Text; }
            }

            /// <summary>
            /// 实例化ProcessingTextEventArgs
            /// </summary>
            /// <param name="messageData">消息内容</param>
            public ProcessingTextEventArgs(ReceiveMessage.Text messageData)
            {
                this.MessageData = messageData;
            }
        }

        protected virtual void OnProcessingText(ProcessingTextEventArgs e)
        {
            if (ProcessingText != null)
            { // 如果有对象注册
                ProcessingText(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理图片消息事件
        public delegate void ProcessingImageEventHandler(Object sender, ProcessingImageEventArgs e);
        /// <summary>
        /// 处理图片消息事件
        /// </summary>
        public event ProcessingImageEventHandler ProcessingImage;

        /// <summary>
        /// 处理图片消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingImageEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.Image MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.Image; }
            }

            /// <summary>
            /// 实例化ProcessingImageEventArgs
            /// </summary>
            /// <param name="messageData">消息内容</param>
            public ProcessingImageEventArgs(ReceiveMessage.Image messageData)
            {
                this.MessageData = messageData;
            }
        }

        protected virtual void OnProcessingImage(ProcessingImageEventArgs e)
        {
            if (ProcessingImage != null)
            { // 如果有对象注册
                ProcessingImage(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理语音消息事件
        public delegate void ProcessingVoiceEventHandler(Object sender, ProcessingVoiceEventArgs e);
        /// <summary>
        /// 处理语音消息事件
        /// </summary>
        public event ProcessingVoiceEventHandler ProcessingVoice;

        /// <summary>
        /// 处理语音消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingVoiceEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.Voice MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.Voice; }
            }

            /// <summary>
            /// 实例化ProcessingVoiceEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingVoiceEventArgs(ReceiveMessage.Voice massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingVoice(ProcessingVoiceEventArgs e)
        {
            if (ProcessingVoice != null)
            { // 如果有对象注册
                ProcessingVoice(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理视频消息事件
        public delegate void ProcessingVideoEventHandler(Object sender, ProcessingVideoEventArgs e);
        /// <summary>
        /// 处理视频消息事件
        /// </summary>
        public event ProcessingVideoEventHandler ProcessingVideo;

        /// <summary>
        /// 处理视频消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingVideoEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.Video MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.Video; }
            }

            /// <summary>
            /// 实例化ProcessingVideoEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingVideoEventArgs(ReceiveMessage.Video massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingVideo(ProcessingVideoEventArgs e)
        {
            if (ProcessingVideo != null)
            { // 如果有对象注册
                ProcessingVideo(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理位置消息事件
        public delegate void ProcessingLocationEventHandler(Object sender, ProcessingLocationEventArgs e);
        /// <summary>
        /// 处理位置消息事件
        /// </summary>
        public event ProcessingLocationEventHandler ProcessingLocation;

        /// <summary>
        /// 处理位置消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingLocationEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.Location MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.Location; }
            }

            /// <summary>
            /// 实例化ProcessingLocationEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingLocationEventArgs(ReceiveMessage.Location massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingLocation(ProcessingLocationEventArgs e)
        {
            if (ProcessingLocation != null)
            { // 如果有对象注册
                ProcessingLocation(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion


        #region 处理成员关注/取消关注事件
        public delegate void ProcessingEventSubscribeEventHandler(Object sender, ProcessingEventSubscribeEventArgs e);
        /// <summary>
        /// 处理成员关注/取消关注事件
        /// </summary>
        public event ProcessingEventSubscribeEventHandler ProcessingEventSubscribe;

        /// <summary>
        /// 处理成员关注/取消关注事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventSubscribeEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventSubscribe MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventSubscribe; }
            }

            /// <summary>
            /// 实例化ProcessingEventSubscribeEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventSubscribeEventArgs(ReceiveMessage.EventSubscribe massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventSubscribe(ProcessingEventSubscribeEventArgs e)
        {
            if (ProcessingEventSubscribe != null)
            { // 如果有对象注册
                ProcessingEventSubscribe(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理点击菜单拉取消息事件
        public delegate void ProcessingEventClickEventHandler(Object sender, ProcessingEventClickEventArgs e);
        /// <summary>
        /// 处理点击菜单拉取消息事件
        /// </summary>
        public event ProcessingEventClickEventHandler ProcessingEventClick;

        /// <summary>
        /// 处理点击菜单拉取消息事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventClickEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventClick MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventClick; }
            }

            /// <summary>
            /// 实例化ProcessingEventClickEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventClickEventArgs(ReceiveMessage.EventClick massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventClick(ProcessingEventClickEventArgs e)
        {
            if (ProcessingEventClick != null)
            { // 如果有对象注册
                ProcessingEventClick(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理成员进入应用事件
        public delegate void ProcessingEventEnterAgentEventHandler(Object sender, ProcessingEventEnterAgentEventArgs e);
        /// <summary>
        /// 处理成员进入应用事件
        /// </summary>
        public event ProcessingEventEnterAgentEventHandler ProcessingEventEnterAgent;

        /// <summary>
        /// 处理成员进入应用事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventEnterAgentEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventEnterAgent MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventEnterAgent; }
            }

            /// <summary>
            /// 实例化ProcessingEventEnterAgentEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventEnterAgentEventArgs(ReceiveMessage.EventEnterAgent massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventEnterAgent(ProcessingEventEnterAgentEventArgs e)
        {
            if (ProcessingEventEnterAgent != null)
            { // 如果有对象注册
                ProcessingEventEnterAgent(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 处理上报地理位置事件
        public delegate void ProcessingEventLocationEventHandler(Object sender, ProcessingEventLocationEventArgs e);
        /// <summary>
        /// 处理上报地理位置事件
        /// </summary>
        public event ProcessingEventLocationEventHandler ProcessingEventLocation;

        /// <summary>
        /// 处理成员进入应用事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventLocationEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventLocation MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventLocation; }
            }

            /// <summary>
            /// 实例化ProcessingEventLocationEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventLocationEventArgs(ReceiveMessage.EventLocation massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventLocation(ProcessingEventLocationEventArgs e)
        {
            if (ProcessingEventLocation != null)
            { // 如果有对象注册
                ProcessingEventLocation(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #region 异步任务完成事件
        public delegate void ProcessingEventBatchJobResultEventHandler(Object sender, ProcessingEventBatchJobResultEventArgs e);
        /// <summary>
        /// 异步任务完成事件
        /// </summary>
        public event ProcessingEventBatchJobResultEventHandler ProcessingEventBatchJobResult;

        /// <summary>
        /// 处理异步任务完成事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventBatchJobResultEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventBatchJobResult MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventBatchJobResult; }
            }

            /// <summary>
            /// 实例化ProcessingEventBatchJobResultEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventBatchJobResultEventArgs(ReceiveMessage.EventBatchJobResult massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventBatchJobResult(ProcessingEventBatchJobResultEventArgs e)
        {
            if (ProcessingEventBatchJobResult != null)
            { // 如果有对象注册
                ProcessingEventBatchJobResult(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion


        #endregion

        #region 接收消息校验、加解密所需参数定义
        /// <summary>
        /// 微信加密签名，msg_signature结合了企业填写的token、请求中的timestamp、nonce参数、加密的消息体
        /// </summary>
        private string MsgSig { set; get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        private string TimeStamp { set; get; }

        /// <summary>
        /// 随机数
        /// </summary>
        private string Nonce { set; get; }

        /// <summary>
        /// 加密的随机字符串，以msg_encrypt格式提供。需要解密并返回echostr明文，解密后有random、msg_len、msg、$CorpID四个字段，其中msg即为echostr明文。
        /// 首次校验时必带。正常消息请求不带。
        /// </summary>
        private string Echostr { set; get; }

        #endregion

        #region Processing 开始处理
        /// <summary>
        /// 开始处理
        /// </summary>
        public void Processing()
        {
            HttpContext context = HttpContext.Current;
            if (context == null || context.Request == null)
            {
                throw new Exception("HttpContext信息错误");
            }
            //this.MsgSig = HttpUtility.UrlDecode(context.Request.QueryString["msg_signature"], Encoding.UTF8);
            //this.TimeStamp = HttpUtility.UrlDecode(context.Request.QueryString["timestamp"], Encoding.UTF8);
            //this.Nonce = HttpUtility.UrlDecode(context.Request.QueryString["nonce"], Encoding.UTF8);
            //this.Echostr = HttpUtility.UrlDecode(context.Request.QueryString["echostr"], Encoding.UTF8);
            this.MsgSig = context.Request.QueryString["msg_signature"];
            this.TimeStamp = context.Request.QueryString["timestamp"];
            this.Nonce = context.Request.QueryString["nonce"];
            this.Echostr = context.Request.QueryString["echostr"];

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("MsgSig = " + MsgSig);
                Logger.Info("TimeStamp = " + TimeStamp);
                Logger.Info("Nonce = " + Nonce);
                Logger.Info("Echostr = " + Echostr);
            }

            if (string.IsNullOrEmpty(this.MsgSig) ||
                string.IsNullOrEmpty(this.TimeStamp) ||
                string.IsNullOrEmpty(this.Nonce))
            {
                throw new Exception("HttpContext信息错误");
            }
            int ret = 0;
            #region 微信平台开启回调模式，首次配置服务器信息时，校验服务器URL
            if (!string.IsNullOrEmpty(this.Echostr))
            {
                /*
                 ------------使用示例一：验证回调URL---------------
                 *企业开启回调模式时，企业号会向验证url发送一个get请求 
                 假设点击验证时，企业收到类似请求：
                 * GET /cgi-bin/wxpush?msg_signature=5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3&timestamp=1409659589&nonce=263014780&echostr=P9nAzCzyDtyTWESHep1vC5X9xho%2FqYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp%2B4RPcs8TgAE7OaBO%2BFZXvnaqQ%3D%3D 
                 * HTTP/1.1 Host: qy.weixin.qq.com

                 * 接收到该请求时，企业应
                 1.解析出Get请求的参数，包括消息体签名(msg_signature)，时间戳(timestamp)，随机数字串(nonce)以及公众平台推送过来的随机加密字符串(echostr),
                   这一步注意作URL解码。
                 2.验证消息体签名的正确性 
                 3.解密出echostr原文，将原文当作Get请求的response，返回给公众平台
                 第2，3步可以用公众平台提供的库函数VerifyURL来实现。
                 */

                string sEchoStr = "";
                ret = Wxcpt.VerifyURL(this.MsgSig, this.TimeStamp, this.Nonce, this.Echostr, ref sEchoStr);
                if (ret != 0)
                {
                    throw new Exception("ERR: VerifyURL Fail, errorCode: " + ret + "。");
                }
                //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。

                context.Response.ContentType = "text/plain";
                context.Response.Write(sEchoStr);
                return;
            }
            #endregion

            //获取post请求内容
            StreamReader postData = new StreamReader(context.Request.InputStream);
            string postDataString = postData.ReadToEnd();

            if (AppSettings.IsDebug) Logger.Info("postDataString = " + postDataString);

            string sMsg = "";  // 解析之后的明文
            //解密
            ret = Wxcpt.DecryptMsg(MsgSig, TimeStamp, Nonce, postDataString, ref sMsg);
            if (ret != 0)
            {
                throw new Exception("ERR: Decrypt Fail, errorCode: " + ret);
            }

            if (AppSettings.IsDebug) Logger.Info("sMsg = " + sMsg);

            //解析XML
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sMsg);

            ReceiveMessageEnum messageType = ReceiveMessageEnum.Unknow;
            ReceiveMessage.Base messageEntity = ReceiveMessageParser.Parse(doc, ref messageType);
            ReplyMessage.Base retMessage = null;

            #region 根据接收消息类型 执行相应事件
            switch (messageType)
            {
                case ReceiveMessageEnum.Unknow:
                    throw new Exception("未知的接收消息类型。");
                case ReceiveMessageEnum.Text:
                    if (ProcessingText != null)
                    {
                        ProcessingTextEventArgs e = new ProcessingTextEventArgs(messageEntity as ReceiveMessage.Text);
                        ProcessingText(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Image:
                    if (ProcessingImage != null)
                    {
                        ProcessingImageEventArgs e = new ProcessingImageEventArgs(messageEntity as ReceiveMessage.Image);
                        ProcessingImage(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Voice:
                    if (ProcessingVoice != null)
                    {
                        ProcessingVoiceEventArgs e = new ProcessingVoiceEventArgs(messageEntity as ReceiveMessage.Voice);
                        ProcessingVoice(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Video:
                    if (ProcessingVideo != null)
                    {
                        ProcessingVideoEventArgs e = new ProcessingVideoEventArgs(messageEntity as ReceiveMessage.Video);
                        ProcessingVideo(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Location:
                    if (ProcessingLocation != null)
                    {
                        ProcessingLocationEventArgs e = new ProcessingLocationEventArgs(messageEntity as ReceiveMessage.Location);
                        ProcessingLocation(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Event_Location:
                    //如果开启位置记录，则缓存
                    if (this.EnablePositionRecord)
                    {
                        if (AppSettings.IsDebug) Logger.Info("enablePositionRecord! key = " + CacheKey.UserLocation + messageEntity.FromUserName);
                        HttpContext.Current.Cache.Insert(CacheKey.UserLocation + messageEntity.FromUserName,
                            messageEntity,
                            null,
                            DateTime.UtcNow.AddHours(this.PositionRecordExpiresHours),
                            System.Web.Caching.Cache.NoSlidingExpiration);
                    }
                    if (ProcessingEventLocation != null)
                    {
                        ProcessingEventLocationEventArgs e = new ProcessingEventLocationEventArgs(messageEntity as ReceiveMessage.EventLocation);
                        ProcessingEventLocation(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Event_Click:
                    if (ProcessingEventClick != null)
                    {
                        ProcessingEventClickEventArgs e = new ProcessingEventClickEventArgs(messageEntity as ReceiveMessage.EventClick);
                        ProcessingEventClick(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Event_View:
                    break;
                case ReceiveMessageEnum.Event_Scancode_Push:
                    break;
                case ReceiveMessageEnum.Event_Scancode_Waitmsg:
                    break;
                case ReceiveMessageEnum.Event_Pic_Sysphoto:
                    break;
                case ReceiveMessageEnum.Event_Pic_Photo_Or_Album:
                    break;
                case ReceiveMessageEnum.Event_Pic_Weixin:
                    break;
                case ReceiveMessageEnum.Event_Location_Select:
                    break;
                case ReceiveMessageEnum.Event_Enter_Agent:
                    if (ProcessingEventEnterAgent != null)
                    {
                        ProcessingEventEnterAgentEventArgs e = new ProcessingEventEnterAgentEventArgs(messageEntity as ReceiveMessage.EventEnterAgent);
                        ProcessingEventEnterAgent(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Event_Subscribe:
                    if (ProcessingEventSubscribe != null)
                    {
                        ProcessingEventSubscribeEventArgs e = new ProcessingEventSubscribeEventArgs(messageEntity as ReceiveMessage.EventSubscribe);
                        ProcessingEventSubscribe(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                case ReceiveMessageEnum.Event_Batch_Job_Result:

                    if (ProcessingEventBatchJobResult != null)
                    {
                        ProcessingEventBatchJobResultEventArgs e = new ProcessingEventBatchJobResultEventArgs(messageEntity as ReceiveMessage.EventBatchJobResult);
                        ProcessingEventBatchJobResult(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;
                default:
                    throw new Exception("未知的接收消息类型。");
            }
            #endregion


            if (AppSettings.IsDebug)
            {
                if (retMessage == null) Logger.Info("retMessage = null");
                else Logger.Log("retMessage = " + retMessage.ToString());
            }

            string sEncryptMsg = ""; //xml格式的密文

            if (retMessage != null)
            {
                //未加密明文字符串
                string replyMsg = ReplyMessageBuilder.BuildXml(retMessage);

                if (AppSettings.IsDebug) Logger.Info("replyMsg = " + replyMsg);

                //加密
                //string sEncryptMsg = ""; //xml格式的密文
                ret = Wxcpt.EncryptMsg(replyMsg, TimeStamp, Nonce, ref sEncryptMsg);
                if (ret != 0)
                {
                    throw new Exception("ERR: Encrypt Fail, errorCode: " + ret);
                }

                if (AppSettings.IsDebug) Logger.Info("sEncryptMsg = " + sEncryptMsg);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(sEncryptMsg);
        }
        #endregion

    }
}
