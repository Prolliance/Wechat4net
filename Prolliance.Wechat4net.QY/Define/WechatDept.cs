using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 微信通讯录部门定义类
    /// </summary>
    public class WechatDept
    {
        /// <summary>
        /// 部门名称
        /// 必填
        /// </summary>
        [JsonProperty("name")]
        public string Name { set; get; }
        /// <summary>
        /// 部门ID
        /// （创建时，指定时必须大于1，不指定时则自动生成）
        /// </summary>
        [JsonProperty("id")]
        public int ID { set; get; }
        /// <summary>
        /// 父部门ID
        /// 必填
        /// </summary>
        [JsonProperty("parentid")]
        public int ParentID { set; get; }
        /// <summary>
        /// 排序
        /// 选填 填0不修改排序
        /// </summary>
        [JsonProperty("order")]
        public int Order { set; get; }

        internal WechatDept()
        {
            this.ID = 0;
            this.Name = "";
            this.ParentID = 0;
            this.Order = 0;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <param name="id">部门ID</param>
        /// <param name="parentID">父部门ID</param>
        public WechatDept(string name, int id, int parentID)
        {
            this.ID = id;
            this.Name = name;
            this.ParentID = parentID;
            this.Order = 0;
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <param name="id">部门ID</param>
        /// <param name="parentID">父部门ID</param>
        /// <param name="order">排序</param>
        public WechatDept(string name, int id, int parentID, int order)
        {
            this.ID = id;
            this.Name = name;
            this.ParentID = parentID;
            this.Order = order;
        }

    }
}
