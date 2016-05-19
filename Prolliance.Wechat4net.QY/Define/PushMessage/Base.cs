using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wechat4net.Utils;

namespace Wechat4net.QY.Define.PushMessage
{
    /// <summary>
    /// 推送消息类型基类
    /// </summary>
    public abstract class Base
    {
        /// <summary>
        /// 消息类型字段 SDK内部判断使用
        /// </summary>
        internal Enums.QY.PushMessageEnum messageType = Enums.QY.PushMessageEnum.Unknow;

        /// <summary>
        /// 是否发送全部员工
        /// </summary>
        internal bool ToAll
        {
            get
            {
                return (this.ToUser == null || this.ToUser.Count == 0) && (this.ToParty == null || this.ToParty.Count == 0) && (this.ToTag == null || this.ToTag.Count == 0);
            }
        }
        /// <summary>
        /// 员工ID列表 最多支持1000个（当ToUser、ToParty、ToTag都为空时，给全部员工发送）
        /// </summary>
        public List<string> ToUser { set; get; }
        /// <summary>
        /// 部门ID列表 最多支持100个（当ToUser、ToParty、ToTag都为空时，给全部员工发送）
        /// </summary>
        public List<string> ToParty { set; get; }
        /// <summary>
        /// 标签ID列表 （当ToUser、ToParty、ToTag都为空时，给全部员工发送）
        /// </summary>
        public List<string> ToTag { set; get; }
        /// <summary>
        /// 消息类型
        /// </summary>
        internal abstract string MsgType { get; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppID { set; get; }

        internal abstract object GetData();

        #region GetPushObject 获取消息推送对象信息
        /// <summary>
        /// 获取消息推送对象信息
        /// </summary>
        /// <param name="toUser">人员对象列表 |分隔</param>
        /// <param name="toParty">部门对象列表 |分隔</param>
        /// <param name="toTag">标签对象列表 |分隔</param>
        protected void GetPushObject(out string toUser, out string toParty, out string toTag)
        {
            if (this.ToAll)
            {
                toUser = "@all";
                toParty = "";
                toTag = "";
            }
            else
            {
                StringBuilder sb_user = new StringBuilder();
                foreach (string u in this.ToUser)
                {
                    if (!string.IsNullOrEmpty(u))
                    {
                        sb_user.Append(u).Append("|");
                    }
                }
                toUser = sb_user.ToString();
                if (toUser.Length > 0)
                {
                    toUser = toUser.Remove(toUser.Length - 1);
                }

                StringBuilder sb_party = new StringBuilder();
                foreach (string p in this.ToParty)
                {
                    if (!string.IsNullOrEmpty(p))
                    {
                        sb_party.Append(p).Append("|");
                    }
                }
                toParty = sb_party.ToString();
                if (toParty.Length > 0)
                {
                    toParty = toParty.Remove(toParty.Length - 1);
                }

                StringBuilder sb_tag = new StringBuilder();
                foreach (string t in this.ToTag)
                {
                    if (!string.IsNullOrEmpty(t))
                    {
                        sb_tag.Append(t).Append("|");
                    }
                }
                toTag = sb_tag.ToString();
                if (toTag.Length > 0)
                {
                    toTag = toTag.Remove(toTag.Length - 1);
                }
            }
            return;
        }
        #endregion

    }
}
