using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.QY.Define.ReceiveMessage
{
    /// <summary>
    /// 异步任务完成事件 消息类型
    /// </summary>
    public class EventBatchJobResult : Base
    {
        public EventBatchJobResult() { }

        /// <summary>
        /// 事件类型，batch_job_result
        /// </summary>
        public string Event { set; get; }

        /// <summary>
        /// 异步任务id，最大长度为64字符
        /// </summary>
        [XmlProperty("BatchJob.JobId")]
        public string JobID { set; get; }

        /// <summary>
        /// 操作类型，字符串，目前分别有：
        /// 1. sync_user(增量更新成员) 
        /// 2. replace_user(全量覆盖成员) 
        /// 3. invite_user(邀请成员关注) 
        /// 4. replace_party(全量覆盖部门) 
        /// </summary>
        [XmlProperty("BatchJob.JobType")]
        public string JobType { set; get; }

        /// <summary>
        /// 返回码 0成功
        /// </summary>
        [XmlProperty("BatchJob.ErrCode")]
        public int ErrorCode { set; get; }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        [XmlProperty("BatchJob.ErrMsg")]
        public string ErrorMessage { set; get; }

        /// <summary>
        /// 该事件有系统生成 AppID无效！
        /// </summary>
        public new string AppID { set { base.AppID = value; } }

        /// <summary>
        /// 此事件该值固定为sys，表示该消息由系统生成
        /// </summary>
        public new string FromUserName { get { return "sys"; } }
    }
}
