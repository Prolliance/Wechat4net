using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wechat4net.Define;

namespace Wechat4net.QY.Define.PushMessage
{
    public class AppInfo : ReturnValue
    {
        //{
        //   "errcode":"0",
        //   "errmsg":"ok" ,
        //   "agentid":"1" ,
        //   "name":"NAME" ,
        //   "square_logo_url":"xxxxxxxx" ,
        //   "round_logo_url":"yyyyyyyy" ,
        //   "description":"desc" ,
        //   "allow_userinfos":{
        //       "user":[
        //             {
        //                 "userid":"id1",
        //                 "status":"1"
        //             },
        //             {
        //                 "userid":"id2",
        //                 "status":"1"
        //             },
        //             {
        //                 "userid":"id3",
        //                 "status":"1"
        //             }
        //              ]
        //    },
        //   "allow_partys":{
        //       "partyid": [1]
        //    },
        //   "allow_tags":{
        //       "tagid": [1,2,3]
        //    }
        //   "close":0 ,
        //   "redirect_domain":"www.qq.com",
        //   "report_location_flag":0,
        //   "isreportuser":0,
        //   "isreportenter":0
        //}

        /// <summary>
        /// 企业应用ID
        /// </summary>
        [JsonProperty("agentid")]
        public int AppID { set; get; }

        /// <summary>
        /// 企业应用名称
        /// </summary>
        [JsonProperty("name")]
        public string AppName { set; get; }

        /// <summary>
        /// 企业应用方形头像
        /// </summary>
        [JsonProperty("square_logo_url")]
        public string SquareLogoUrl { set; get; }

        /// <summary>
        /// 企业应用圆形头像
        /// </summary>
        [JsonProperty("round_logo_url")]
        public string RoundLogoUrl { set; get; }

        /// <summary>
        /// 企业应用详情
        /// </summary>
        [JsonProperty("description")]
        public string Description { set; get; }

        /// <summary>
        /// 企业应用可见范围（人员），其中包括userid和关注状态state
        /// </summary>
        [JsonProperty("allow_userinfos")]
        public AllowUserInfo AllowUserInfos { set; get; }

        /// <summary>
        /// 企业应用可见范围（部门）
        /// </summary>
        [JsonProperty("allow_partys")]
        public AllowDept AllowDepts { set; get; }

        /// <summary>
        /// 企业应用可见范围（标签）
        /// </summary>
        [JsonProperty("allow_tags")]
        public AllowTag AllowTags { set; get; }

        [JsonProperty("close")]
        public int Close;

        /// <summary>
        /// 企业应用是否被禁用
        /// </summary>
        public bool IsClosed { set; get; }

        /// <summary>
        /// 企业应用可信域名
        /// </summary>
        [JsonProperty("redirect_domain")]
        public string RedirectDomain { set; get; }

        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
        /// </summary>
        [JsonProperty("report_location_flag")]
        public int ReportLocationFlag { set; get; }

        [JsonProperty("isreportuser")]
        public int isReportUser;
        /// <summary>
        /// 是否接收用户变更通知
        /// </summary>
        public bool IsReportUser
        {
            set
            { isReportUser = value ? 1 : 0; }
            get
            { return isReportUser == 1; }
        }

        [JsonProperty("isreportenter")]
        public int isReportEnter;
        /// <summary>
        /// 是否上报用户进入应用事件
        /// </summary>
        public bool IsReportEnter
        {
            set
            { isReportEnter = value ? 1 : 0; }
            get
            { return isReportEnter == 1; }
        }


        public class AllowUserInfo
        {
            [JsonProperty("user")]
            public List<AllowUserInfoItem> users { set; get; }

            public class AllowUserInfoItem
            {
                [JsonProperty("userid")]
                public string UserID { set; get; }

                [JsonProperty("status")]
                public string Status { set; get; }
            }
        }

        public class AllowDept
        {
            [JsonProperty("partyid")]
            public List<int> DeptIDs { set; get; }
        }

        public class AllowTag
        {
            [JsonProperty("tagid")]
            public List<int> TagIDs { set; get; }
        }
    }
}
