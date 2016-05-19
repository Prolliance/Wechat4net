using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.PushMessage
{
    /// <summary>
    /// 图片消息类型
    /// </summary>
    public class Image : Base
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        internal override string MsgType { get { return "image"; } }

        /// <summary>
        /// 图片媒体文件id，可以调用上传媒体文件接口获取
        /// </summary>
        public string MediaID { set; get; }

        /// <summary>
        /// 是否是保密消息
        /// </summary>
        public bool IsSafe { set; get; }

        /// <summary>
        /// 实例化一个图片类型推送信息对象
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="toUser">员工ID列表 最多支持1000个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toParty">部门ID列表 最多支持100个（当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="toTag">标签ID列表 （当toUser、toParty、toTag都为空时，给全部员工发送）</param>
        /// <param name="mediaId">图片媒体文件id，可以调用上传媒体文件接口获取</param>
        /// <param name="isSafe">是否加密</param>
        public Image(string appId, List<string> toUser, List<string> toParty, List<string> toTag, string mediaId, bool isSafe)
        {
            this.messageType = Enums.QY.PushMessageEnum.Image;
            //this.MsgType = "image";
            this.AppID = appId;
            this.ToUser = toUser ?? new List<string>();
            this.ToParty = toParty ?? new List<string>();
            this.ToTag = toTag ?? new List<string>();
            this.MediaID = mediaId;
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
                image = new
                {
                    media_id = MediaID
                },
                safe = IsSafe ? "1" : "0"
            };

            return data;
        }

    }
}
