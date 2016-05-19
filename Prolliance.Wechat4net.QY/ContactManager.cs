using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.QY.Common;
using Wechat4net.QY.Define;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;
using Wechat4net.Define;
using Newtonsoft.Json;

namespace Wechat4net.QY
{
    /// <summary>
    /// 通讯录相关管理类
    /// </summary>
    public static class ContactManager
    {
        #region 辅助类
        private static Logger Logger
        {
            get
            {
                Logger _Logger = new Logger(AppSettings.LogPath);
                return _Logger;
            }
        }

        #endregion

        #region Dept

        #region CrateDept
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="dept">部门对象</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.Contact.CrateDeptReturnValue CrateDept(WechatDept dept)
        {
            //https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=ACCESS_TOKEN
            string url = string.Format("{0}?access_token={1}", ServiceUrl.CrateDept, AccessToken.Value);

            //{
            //  "name": "广州研发中心",
            //  "parentid": "1",
            //  "order": "1",
            //  "id": "1"
            //}
            var data = new
            {
                name = dept.Name,
                parentid = dept.ParentID,
                order = dept.Order,
                id = dept.ID
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[CrateDept] Json = " + json);
            }

            string result = HttpHelper.Post(url, json);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[CrateDept] result = " + result);
            }

            return JsonConvert.DeserializeObject<Contact.CrateDeptReturnValue>(result);
        }
        #endregion

        #region GetDeptList
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="deptId">部门id。获取指定部门及其下的子部门</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.Contact.QuiryDeptListReturnValue GetDeptList(int deptId)
        {
            string url = string.Format(
                "{0}?access_token={1}&id={2}",
                ServiceUrl.GetDeptList,
                AccessToken.Value,
                deptId
                );
            //return Create().Get<Wechat4net.QY.Define.Contact.QuiryDeptListReturnValue>(url);
            return HttpHelper.Get<Wechat4net.QY.Define.Contact.QuiryDeptListReturnValue>(url);
        }
        #endregion

        #region UpdateDeptInfo
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dept">部门对象</param>
        /// <returns></returns>
        public static ReturnValue UpdateDeptInfo(WechatDept dept)
        {
            //https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token=ACCESS_TOKEN
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UpdateDeptInfo, AccessToken.Value);

            //{
            //  "name": "广州研发中心",
            //  "parentid": "1",
            //  "order": "1",
            //  "id": "1"
            //}
            var data = new
            {
                name = dept.Name,
                parentid = dept.ParentID,
                order = dept.Order,
                id = dept.ID
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateDeptInfo] Json = " + json);
            }

            string result = HttpHelper.Post(url, json);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateDeptInfo] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }
        #endregion

        #region DeleteDept
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        public static ReturnValue DeleteDept(int deptId)
        {
            string url = string.Format(
                "{0}?access_token={1}&id={2}",
                ServiceUrl.DeleteDept,
                AccessToken.Value,
                deptId
                );
            return HttpHelper.Get<ReturnValue>(url);
        }
        #endregion

        #endregion

        #region User

        #region GetUserList
        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="deptId">获取的部门ID</param>
        /// <param name="isIncludeChildDept">是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <param name="isDetail">是否获取人员详细信息，如果为true则获取全部信息，如果为false则值获取UserID和Name</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.Contact.QuiryUserListByDeptReturnValue GetUserList(int deptId, bool isIncludeChildDept, int status, bool isDetail)
        {
            string url = string.Format(
                "{0}?access_token={1}&department_id={2}&fetch_child={3}&status={4}",
                isDetail ? ServiceUrl.GetUserInfoList : ServiceUrl.GetUserList,
                AccessToken.Value,
                deptId,
                isIncludeChildDept ? 1 : 0,
                status
                );
            //return Create().Get<Wechat4net.QY.Define.Contact.QuiryUserListByDeptReturnValue>(url);
            return HttpHelper.Get<Wechat4net.QY.Define.Contact.QuiryUserListByDeptReturnValue>(url);
        }

        /// <summary>
        /// 获取标签成员及部门
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.Contact.QuiryUserListByTagReturnValue GetUserList(int tagId)
        {
            string url = string.Format(
                "{0}?access_token={1}&tagid={2}", ServiceUrl.GetUserListByTag, AccessToken.Value, tagId
                );
            //return Create().Get<Wechat4net.QY.Define.Contact.QuiryUserListByTagReturnValue>(url);
            return HttpHelper.Get<Wechat4net.QY.Define.Contact.QuiryUserListByTagReturnValue>(url);
        }
        #endregion

        #region CreateUser
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="user">成员信息</param>
        /// <returns></returns>
        public static ReturnValue CreateUser(WechatUser user)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.CreateUser, AccessToken.Value);

            //{
            //   "userid": "zhangsan",
            //   "name": "张三",
            //   "department": [1, 2],
            //   "position": "产品经理",
            //   "mobile": "15913215421",
            //   "gender": "1",
            //   "email": "zhangsan@gzdev.com",
            //   "weixinid": "zhangsan4dev",
            //   "avatar_mediaid": "2-G6nrLmr5EC3MNb_-zL1dDdzkd0p7cNliYu9V5w7o8K0",
            //   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]}
            //}

            //List<Dictionary<string, string>> attrs = new List<Dictionary<string, string>>();
            //if (user.ExtAttr != null && user.ExtAttr.Attrs != null)
            //{
            //    foreach (var item in user.ExtAttr.Attrs)
            //    {
            //        Dictionary<string, string> attrsItem = new Dictionary<string, string>();
            //        attrsItem.Add("name", item.Name);
            //        attrsItem.Add("value", item.Value);
            //        attrs.Add(attrsItem);
            //    }
            //}

            var data = new
            {
                userid = user.UserID,
                name = user.Name,
                department = user.Department.ToArray(),
                position = user.Position,
                mobile = user.Mobile,
                gender = user.Gender,
                email = user.Email,
                weixinid = user.WeixinID,
                avatar_mediaid = user.AvatarMediaID,
                extattr = new
                {
                    attrs = user.ExtAttr.Attrs
                }
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[CreateUser] Json = " + json);
            }

            string result = HttpHelper.Post(url, json);


            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[CreateUser] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="userId">成员UserID</param>
        /// <returns></returns>
        public static WechatUser GetUserInfo(string userId)
        {
            string url = string.Format("{0}?access_token={1}&userid={2}", ServiceUrl.GetUserInfo, AccessToken.Value, userId);
            WechatUser user = HttpHelper.Get<WechatUser>(url); //Create().Get<WechatUser>(url);

            if (user.ErrorCode != 0)
            {
                throw new Exception("获取微信用户信息错误,ErrorCode:" + user.ErrorCode.ToString() + "; ErrorMessage:" + user.ErrorMessage);
            }
            return user;
        }
        #endregion

        #region UpdateUserInfo
        /// <summary>
        /// 更新成员信息
        /// </summary>
        /// <param name="user">成员对象</param>
        /// <returns></returns>
        public static ReturnValue UpdateUserInfo(WechatUser user)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UpdateUserInfo, AccessToken.Value);

            //{
            //   "userid": "zhangsan",
            //   "name": "李四",
            //   "department": [1],
            //   "position": "后台工程师",
            //   "mobile": "15913215421",
            //   "gender": "1",
            //   "email": "zhangsan@gzdev.com",
            //   "weixinid": "lisifordev",
            //   "enable": 1,
            //   "avatar_mediaid": "2-G6nrLmr5EC3MNb_-zL1dDdzkd0p7cNliYu9V5w7o8K0",
            //   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]}
            //}

            List<Dictionary<string, string>> attrs = new List<Dictionary<string, string>>();
            if (user.ExtAttr != null && user.ExtAttr.Attrs != null)
            {
                foreach (var item in user.ExtAttr.Attrs)
                {
                    Dictionary<string, string> attrsItem = new Dictionary<string, string>();
                    attrsItem.Add("name", item.Name);
                    attrsItem.Add("value", item.Value);
                    attrs.Add(attrsItem);
                }
            }

            var data = new
            {
                userid = user.UserID,
                name = user.Name,
                department = user.Department.ToArray(),
                position = user.Position,
                mobile = user.Mobile,
                gender = user.Gender,
                email = user.Email,
                weixinid = user.WeixinID,
                avatar_mediaid = user.AvatarMediaID,
                extattr = new
                {
                    attrs = attrs
                }
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateUserInfo] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateUserInfo] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }
        #endregion

        #region SetUserWhetherEnable
        /// <summary>
        /// 设置成员是否启用
        /// </summary>
        /// <param name="userID">成员ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public static ReturnValue SetUserWhetherEnable(string userID, bool isEnable)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UpdateUserInfo, AccessToken.Value);

            var data = new
            {
                userid = userID,
                enable = isEnable ? 1 : 0
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[SetUserWhetherEnable] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[SetUserWhetherEnable] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="userId">成员UserID</param>
        /// <returns></returns>
        public static ReturnValue DeleteUser(string userId)
        {
            string url = string.Format("{0}?access_token={1}&userid={2}", ServiceUrl.DeleteUser, AccessToken.Value, userId);
            ReturnValue result = HttpHelper.Get<ReturnValue>(url);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[DeleteUser] result : ErrorCode = " + result.ErrorCode + " ; ErrorMessage = " + result.ErrorMessage);
            }
            return result;
        }
        #endregion

        #endregion

    }
}
