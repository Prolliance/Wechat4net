using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 微信用户信息类
    /// </summary>
    [Serializable]
    public class WechatUser
    {
        public WechatUser()
        {
            this.Department = new List<int>();
            this.ExtAttr = new Attrsex();
        }

        [JsonProperty("errcode")]
        private int errorCode = 0;
        /// <summary>
        /// 错误代码 0成功
        /// </summary>
        internal int ErrorCode { get { return errorCode; } }

        [JsonProperty("errmsg")]
        private string errorMessage = null;
        /// <summary>
        /// 错误信息
        /// </summary>
        internal string ErrorMessage { get { return errorMessage; } }

        /// <summary>
        /// 用户ID（用户在企业通讯录中的唯一ID）
        /// </summary>
        [JsonProperty("userid")]
        public string UserID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 部门（一个用户可属于多个部门）
        /// </summary>
        [JsonProperty("department")]
        public List<int> Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 性别 0表示未定义，1表示男性，2表示女性
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 用户微信ID
        /// </summary>
        [JsonProperty("weixinid")]
        public string WeixinID { get; set; }

        [JsonProperty("avatar")]
        private string avatar = null;
        /// <summary>
        /// 微信头像url。注：如果要获取小图将url最后的"/0"改成"/64"即可
        /// </summary>
        public string Avatar { get { return avatar; } }

        /// <summary>
        /// 成员头像的mediaid，通过多媒体接口上传图片获得的mediaid
        /// </summary>
        [JsonProperty("avatar_mediaid")]
        public string AvatarMediaID { get; set; }
        
        [JsonProperty("status")]
        private string status = null;
        /// <summary>
        /// 状态 1=已关注，2=已冻结，4=未关注
        /// </summary>
        public string Status { get { return status; } }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [JsonProperty("extattr")]
        public Attrsex ExtAttr { get; set; }
    }

    /// <summary>
    /// 用户扩展类型
    /// </summary>
    [Serializable]
    public class Attrsex
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public Attrsex()
        {
            this.Attrs = new List<Attr>();
        }

        /// <summary>
        /// 扩展属性集合
        /// </summary>
        [JsonProperty("attrs")]
        public List<Attr> Attrs { get; set; }
    }

    /// <summary>
    /// 属性类型
    /// </summary>
    [Serializable]
    public class Attr
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public Attr()
        {
            this.Name = "";
            this.Value = "";
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        public Attr(string name,string value)
        {
            this.Name = name;
            this.Value = value;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}