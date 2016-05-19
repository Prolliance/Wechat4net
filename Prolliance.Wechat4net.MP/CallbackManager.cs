using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Wechat4net.MP.Business;
using Wechat4net.MP.Utils;
using Wechat4net.Utils;
using ReceiveMessage = Wechat4net.MP.Define.ReceiveMessage;
using ReceiveMessageEnum = Wechat4net.Utils.Enums.MP.ReceiveMessageEnum;
using ReplyMessage = Wechat4net.MP.Define.ReplyMessage;
using ReplyMessageEnum = Wechat4net.Utils.Enums.MP.ReplyMessageEnum;

namespace Wechat4net.MP
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
                    _Wxcpt = new Tencent.WXBizMsgCrypt(WechatConfig.Token, WechatConfig.EncodingAESKey, WechatConfig.AppID.ToString()); //(Wechat.Options.Token, Wechat.Options.EncodingAESKey, Wechat.Options.AppID);
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
            public ReplyMessage.Base ReplyMessage { set; get; }
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

        #region 处理推送消息完成
        public delegate void ProcessingEventMasssEndJobFinishEventHandler(Object sender, ProcessingEventMasssEndJobFinishEventArgs e);
        /// <summary>
        /// 处理推送消息完成
        /// </summary>
        public event ProcessingEventMasssEndJobFinishEventHandler ProcessingEventMasssEndJobFinish;

        /// <summary>
        /// 推送消息完成事件EventArgs。包含massageData
        /// </summary>
        public class ProcessingEventMasssEndJobFinishEventArgs : ProcessingEventArgs
        {
            /// <summary>
            /// 接收的消息内容
            /// </summary>
            public ReceiveMessage.EventMasssEndJobFinish MessageData
            {
                private set { this.messageData = value; }
                get { return this.messageData as ReceiveMessage.EventMasssEndJobFinish; }
            }

            /// <summary>
            /// 实例化ProcessingEventMasssEndJobFinishEventArgs
            /// </summary>
            /// <param name="massageData">消息内容</param>
            public ProcessingEventMasssEndJobFinishEventArgs(ReceiveMessage.EventMasssEndJobFinish massageData)
            {
                this.MessageData = massageData;
            }
        }

        protected virtual void OnProcessingEventMasssEndJobFinish(ProcessingEventMasssEndJobFinishEventArgs e)
        {
            if (ProcessingEventMasssEndJobFinish != null)
            { // 如果有对象注册
                ProcessingEventMasssEndJobFinish(this, e);  // 调用所有注册对象的方法
            }
        }
        #endregion

        #endregion


        #region 接收消息校验、加解密所需参数定义
        /// <summary>
        /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。用于校验token，证明请求来源于微信服务器
        /// </summary>
        private string Signature { set; get; }

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

        /// <summary>
        /// 消息体加密类型，在安全模式或兼容模式下存在
        /// <para>该参数为空或raw时表示为不加密；该参数为aes时，表示aes加密（暂时只有raw和aes两种值)</para>
        /// </summary>
        private string EncryptType { set; get; }

        /// <summary>
        /// 消息体签名，在安全模式或兼容模式下存在
        /// </summary>
        private string MsgSignature { set; get; }
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

            this.Signature = context.Request.QueryString["signature"];
            this.TimeStamp = context.Request.QueryString["timestamp"];
            this.Nonce = context.Request.QueryString["nonce"];
            this.Echostr = context.Request.QueryString["echostr"];

            this.EncryptType = context.Request.QueryString["encrypt_type"];
            this.MsgSignature = context.Request.QueryString["msg_signature"];

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("MsgSig = " + MsgSignature);
                Logger.Info("Signature = " + Signature);
                Logger.Info("TimeStamp = " + TimeStamp);
                Logger.Info("Nonce = " + Nonce);
                Logger.Info("Echostr = " + Echostr);
                Logger.Info("EncryptType = " + EncryptType);
                Logger.Info("MsgSignature = " + MsgSignature);
            }

            if (string.IsNullOrEmpty(this.Signature) ||
                string.IsNullOrEmpty(this.TimeStamp) ||
                string.IsNullOrEmpty(this.Nonce))
            {
                throw new Exception("HttpContext信息错误");
            }
            //校验Token签名(请求来源是否为微信服务器)
            if (!CheckSignature.Check(this.Signature, this.TimeStamp, this.Nonce, WechatConfig.Token))
            { throw new Exception("ERR: VerifyURL Fail, 签名校验失败。"); }

            int ret = 0;
            #region 首次配置服务器信息时，直接返回Echostr
            if (!string.IsNullOrEmpty(this.Echostr))
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(Echostr);
                return;
            }
            #endregion

            //获取post请求内容
            StreamReader postData = new StreamReader(context.Request.InputStream);
            string postDataString = postData.ReadToEnd();

            if (AppSettings.IsDebug) Logger.Info("postDataString = " + postDataString);


            if (EncryptType == "aes")
            {
                //解密
                ret = Wxcpt.DecryptMsg(MsgSignature, TimeStamp, Nonce, postDataString, ref postDataString);
                if (ret != 0)
                {
                    throw new Exception("ERR: Decrypt Fail, errorCode: " + ret);
                }
            }
            if (AppSettings.IsDebug) Logger.Info("sMsg = " + postDataString);


            //解析XML
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postDataString);

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

                case ReceiveMessageEnum.ShortVideo:
                    break;
                case ReceiveMessageEnum.Location:
                    if (ProcessingLocation != null)
                    {
                        ProcessingLocationEventArgs e = new ProcessingLocationEventArgs(messageEntity as ReceiveMessage.Location);
                        ProcessingLocation(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;

                case ReceiveMessageEnum.Link:
                    break;
                case ReceiveMessageEnum.Event_Subscribe:
                    if (ProcessingEventSubscribe != null)
                    {
                        ProcessingEventSubscribeEventArgs e = new ProcessingEventSubscribeEventArgs(messageEntity as ReceiveMessage.EventSubscribe);
                        ProcessingEventSubscribe(this, e);
                        retMessage = e.ReplyMessage;
                    }
                    break;

                case ReceiveMessageEnum.Event_Scan:
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
                case ReceiveMessageEnum.Event_MasssEndJobFinish:
                    if (ProcessingEventMasssEndJobFinish != null)
                    {
                        ProcessingEventMasssEndJobFinishEventArgs e = new ProcessingEventMasssEndJobFinishEventArgs(messageEntity as ReceiveMessage.EventMasssEndJobFinish);
                        ProcessingEventMasssEndJobFinish(this, e);
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

            string replyMsg = ""; //xml格式字符串（明文或加密）

            if (retMessage != null)
            {
                //未加密明文字符串
                replyMsg = ReplyMessageBuilder.BuildXml(retMessage);

                if (AppSettings.IsDebug) Logger.Info("replyMsg = " + replyMsg);

                if (EncryptType == "aes")
                {
                    //加密
                    //replyMsg变为xml格式的密文
                    ret = Wxcpt.EncryptMsg(replyMsg, TimeStamp, Nonce, ref replyMsg);
                    if (ret != 0)
                    {
                        throw new Exception("ERR: Encrypt Fail, errorCode: " + ret);
                    }

                    if (AppSettings.IsDebug) Logger.Info("sEncryptMsg = " + replyMsg);
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(replyMsg);

        }
        #endregion

    }
}
