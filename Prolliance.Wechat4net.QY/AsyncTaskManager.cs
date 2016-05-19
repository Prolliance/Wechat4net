using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.QY.Common;
using Wechat4net.QY.Define;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.QY
{
    /// <summary>
    /// 异步任务管理类
    /// </summary>
    public class AsyncTaskManager
    {
        #region 辅助类

        private static Logger _Logger = null;
        private static Logger Logger
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

        #endregion

        #region ReplaceDept
        /// <summary>
        /// 本接口以部门ID为键，全量覆盖企业号通讯录组织架构，任务完成后企业号通讯录组织架构与提交的列表完全保持一致。
        /// <para>注意事项：</para>
        /// <para>1.列表中存在、通讯录中也存在的部门，执行修改操作</para>
        /// <para>2.列表中存在、通讯录中不存在的部门，执行添加操作</para>
        /// <para>3.列表中不存在、通讯录中存在的部门，当部门为空时，执行删除操作</para>
        /// <para>4.列表项中，部门名称、部门ID、父部门ID为必填字段；排序为可选字段</para>
        /// </summary>
        /// <param name="deptList">部门列表</param>
        /// <param name="callbackUrl">接收回调请求的URL</param>
        /// <returns>异步任务请求结果</returns>
        public static AsyncTask.ReturnValue ReplaceDept(List<WechatDept> deptList, string callbackUrl)
        {
            if (deptList == null || deptList.Count < 1)
            {
                return new AsyncTask.ReturnValue(0, "部门列表为空，未进行操作", null);
            }
            //生成csv文件
            string filePath = CsvHelper.CrateDeptCsv(deptList);

            return UploadAndBeginTask(filePath, callbackUrl, AsyncType.ReplaceDept);
        }
        #endregion

        #region ReplaceUser
        /// <summary>
        /// 本接口以UserID为主键，全量覆盖企业号通讯录成员，任务完成后企业号通讯录成员与提交的列表完全保持一致。
        /// <para>注意事项：</para>
        /// <para>1.列表中存在、通讯录中也存在的成员，完全以文件为准</para>
        /// <para>2.列表中存在、通讯录中不存在的成员，执行添加操作</para>
        /// <para>3.通讯录中存在、列表中不存在的成员，执行删除操作。出于安全考虑，如果需要删除的成员多于50人，且多于现有人数的20%以上，系统将中止导入并返回相应的错误码</para>
        /// </summary>
        /// <param name="userList">人员列表</param>
        /// <param name="callbackUrl">接收回调请求的URL</param>
        /// <returns>异步任务请求结果</returns>
        public static AsyncTask.ReturnValue ReplaceUser(List<WechatUser> userList, string callbackUrl)
        {
            if (userList == null || userList.Count < 1)
            {
                return new AsyncTask.ReturnValue(0, "人员列表为空，未进行操作", null);
            }
            //生成csv文件
            string filePath = CsvHelper.CrateUserCsv(userList);

            return UploadAndBeginTask(filePath, callbackUrl, AsyncType.ReplaceUser);
        }
        #endregion

        #region SyncUser
        /// <summary>
        /// 本接口以UserID为主键，增量更新企业号通讯录成员。
        /// <para>注意事项：</para>
        /// <para>1.文件中存在、通讯录中也存在的成员，更新成员在文件中指定的字段值</para>
        /// <para>2.文件中存在、通讯录中不存在的成员，执行添加操作</para>
        /// <para>3.通讯录中存在、文件中不存在的成员，保持不变</para>
        /// </summary>
        /// <param name="userList">人员列表</param>
        /// <param name="callbackUrl">接收回调请求的URL</param>
        /// <returns>异步任务请求结果</returns>
        public static AsyncTask.ReturnValue SyncUser(List<WechatUser> userList, string callbackUrl)
        {
            if (userList == null || userList.Count < 1)
            {
                return new AsyncTask.ReturnValue(0, "人员列表为空，未进行操作", null);
            }
            //生成csv文件
            string filePath = CsvHelper.CrateUserCsv(userList);

            return UploadAndBeginTask(filePath, callbackUrl, AsyncType.SyncUser);
        }
        #endregion

        private enum AsyncType
        {
            InviteUser = 0,
            SyncUser = 1,
            ReplaceUser = 2,
            ReplaceDept = 3
        }

        private static AsyncTask.ReturnValue UploadAndBeginTask(string filePath, string callbackUrl, AsyncType type)
        {
            string url = string.Empty;
            switch (type)
            {
                case AsyncType.InviteUser:
                    break;
                case AsyncType.SyncUser:
                    url = ServiceUrl.SyncUser + "?access_token=" + AccessToken.Value;
                    break;
                case AsyncType.ReplaceUser:
                    url = ServiceUrl.ReplaceUser + "?access_token=" + AccessToken.Value;
                    break;
                case AsyncType.ReplaceDept:
                    url = ServiceUrl.ReplaceDept + "?access_token=" + AccessToken.Value;
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(url))
            {

            }
            //上传csv文件
            var uploadRet = MediaFileManager.UploadTempFile(filePath, MediaFile.FileType.File);

            //请求覆盖操作
            var data = new
            {
                media_id = uploadRet.MediaID,
                callback = new
                {
                    url = callbackUrl,
                    token = WechatConfig.Token, //Wechat.Options.Token,
                    encodingaeskey = WechatConfig.EncodingAESKey //Wechat.Options.EncodingAESKey
                }
            };

            //string result = Create().WebClient.UploadString(url, AjaxEngine.Gloabl.Serializer.Serialize(data));
            //return AjaxEngine.Gloabl.Serializer.Deserialize<AsyncTask.ReturnValue>(result);

            return HttpHelper.Post<AsyncTask.ReturnValue>(url, data);
        }
    }
}
