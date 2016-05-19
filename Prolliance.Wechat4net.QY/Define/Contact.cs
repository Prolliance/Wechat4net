using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Wechat4net.Define;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 通讯录相关接口返回数据类型定义
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// 创建部门返回类型
        /// </summary>
        public class CrateDeptReturnValue : ReturnValue
        {
            /// <summary>
            /// 创建的部门ID
            /// </summary>
            [JsonProperty("id")]
            public int ID { get; set; }
        }

        /// <summary>
        /// 查询部门列表返回类型
        /// </summary>
        public class QuiryDeptListReturnValue : ReturnValue
        {
            /// <summary>
            /// 部门列表数据
            /// <para>以部门的order字段从小到大排列</para>
            /// </summary>
            [JsonProperty("department")]
            public List<WechatDept> DeptList { set; get; }
        }

        /// <summary>
        /// 通讯录查询部门人员列表返回类
        /// </summary>
        public class QuiryUserListByDeptReturnValue : ReturnValue
        {
            /// <summary>
            /// 用户列表
            /// </summary>
            [JsonProperty("userlist")]
            public List<WechatUser> UserList { set; get; }
        }

        /// <summary>
        /// 通讯录查询标签下人员列表返回类
        /// </summary>
        public class QuiryUserListByTagReturnValue : ReturnValue
        {
            /// <summary>
            /// 用户列表
            /// </summary>
            [JsonProperty("userlist")]
            public List<WechatUser> UserList { set; get; }

            /// <summary>
            /// 部门列表
            /// </summary>
            [JsonProperty("partylist")]
            public List<int> DeptList { set; get; }
        }
    }
}
