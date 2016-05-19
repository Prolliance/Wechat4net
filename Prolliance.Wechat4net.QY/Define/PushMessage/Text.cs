using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.PushMessage
{
    /// <summary>
    /// 文字消息类型
    /// </summary>
    public class Text : Base
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "text"; } }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 是否是保密消息
        /// </summary>
        public bool IsSafe { set; get; }

        /// <summary>
        /// 实例化一个文字类型推送信息对象
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="toUser">员工ID列表 最多支持1000个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toParty">部门ID列表 最多支持100个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toTag">标签ID列表 （当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="content">信息内容</param>
        /// <param name="isSafe">是否加密</param>
        public Text(string appId, List<string> toUser, List<string> toParty, List<string> toTag, string content, bool isSafe)
        {
            this.messageType = Enums.QY.PushMessageEnum.Text;
            //this.MsgType = "text";
            this.AppID = appId;
            this.ToUser = toUser ?? new List<string>();
            this.ToParty = toParty ?? new List<string>();
            this.ToTag = toTag ?? new List<string>();
            this.Content = content;
            this.IsSafe = isSafe;
        }

        internal override object GetData()
        {
            string touser, toparty, totag;
            this.GetPushObject(out touser, out toparty, out totag);

            var data = new
            {
                touser = touser,
                toparty = toparty,
                totag = totag,
                msgtype = MsgType,
                agentid = AppID,
                text = new
                {
                    content = Content
                },
                safe = IsSafe ? "1" : "0"
            };

            return data;
        }

    }
}
