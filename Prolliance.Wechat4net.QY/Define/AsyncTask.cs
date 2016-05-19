using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.QY.Define
{
    /// <summary>
    /// 异步任务相关类型定义
    /// </summary>
    public class AsyncTask
    {
        /// <summary>
        /// 异步任务请求返回结果
        /// </summary>
        public class ReturnValue
        {
            /// <summary>
            /// 返回码
            /// </summary>
            [JsonProperty("errcode")]
            public int ErrorCode { set; get; }

            /// <summary>
            /// 对返回码的文本描述内容
            /// </summary>
            [JsonProperty("errmsg")]
            public string ErrorMessage { set; get; }

            /// <summary>
            /// 异步任务id
            /// </summary>
            [JsonProperty("jobid")]
            public string JobID { set; get; }

            public ReturnValue() { }

            internal ReturnValue(int errorCode, string errorMessage, string jobId)
            {
                this.ErrorCode = errorCode;
                this.ErrorMessage = errorMessage;
                this.JobID = jobId;
            }
        }
    }
}
