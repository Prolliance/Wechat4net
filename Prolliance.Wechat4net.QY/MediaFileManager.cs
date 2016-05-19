using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Wechat4net.Define;
using Wechat4net.QY.Common;
using Wechat4net.QY.Define;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;
using Newtonsoft.Json;

namespace Wechat4net.QY
{
    /// <summary>
    /// 多媒体文件管理类
    /// </summary>
    public static class MediaFileManager
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

        #region Upload

        #region File
        /// <summary>
        /// 临时媒体文件上传
        /// <para>用于上传图片、语音、视频等媒体资源文件以及普通文件（如doc，ppt）</para>
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileType">文件类型</param>
        /// <returns>上传结果</returns>
        [Obsolete("该方法已过时，如果上传临时文件请使用UploadTempFile方法，如果上传永久文件请使用UploadForeverFile方法。")]
        public static MediaFile.UploadTempReturnValue Upload(string filePath, MediaFile.FileType fileType)
        {
            return UploadTempFile(filePath, fileType);
            //string url = string.Format("{0}?access_token={1}&type={2}", ServiceUrl.UploadMediaFile_Temp, AccessToken.Value, MediaFile.GetFileTypeString(fileType));
            //return UploadFile(url, filePath);
        }

        /// <summary>
        /// 临时媒体文件上传（有效期3天）
        /// <para>用于上传图片、语音、视频等媒体资源文件以及普通文件（如doc，ppt）</para>
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileType">文件类型</param>
        /// <returns>上传结果</returns>
        public static MediaFile.UploadTempReturnValue UploadTempFile(string filePath, MediaFile.FileType fileType)
        {
            if (!File.Exists(filePath))
            {
                return new MediaFile.UploadTempReturnValue(-1, "文件 " + filePath + " 不存在");
            }

            string url = string.Format("{0}?access_token={1}&type={2}", ServiceUrl.UploadMediaFile_Temp, AccessToken.Value, MediaFile.GetFileTypeString(fileType));
            return JsonConvert.DeserializeObject<MediaFile.UploadTempReturnValue>(UploadFile(url, filePath));
        }

        /// <summary>
        /// 永久媒体文件上传
        /// <para>用于上传图片、语音、视频等媒体资源文件以及普通文件（如doc，ppt）</para>
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="appId">应用ID</param>
        /// <returns>上传结果</returns>
        public static MediaFile.UploadForeverReturnValue UploadForeverFile(string filePath, MediaFile.FileType fileType, int appId)
        {
            if (!File.Exists(filePath))
            {
                return new MediaFile.UploadForeverReturnValue(-1, "文件 " + filePath + " 不存在");
            }
            string url = string.Format("{0}?agentid={1}&type={2}&access_token={3}", ServiceUrl.UploadMediaFile_Forever, appId, MediaFile.GetFileTypeString(fileType), AccessToken.Value);
            return JsonConvert.DeserializeObject<MediaFile.UploadForeverReturnValue>(UploadFile(url, filePath));
        }

        private static string UploadFile(string url, string filePath)
        {
            return HttpHelper.UploadFile(url, @filePath);

            //byte[] rs = Create().WebClient.UploadFile(url, @filePath);
            //return System.Text.Encoding.UTF8.GetString(rs);

            //FileInfo file = new FileInfo(filePath);
            //var Post = CreatePost(url);
            //Post.AddFileParameter("media", file);
            //byte[] rs = Post.Send();
            //string ret = System.Text.Encoding.UTF8.GetString(rs);
            //return AjaxEngine.Gloabl.Serializer.Deserialize<MediaFile.UploadTempReturnValue>(ret);
        }
        #endregion

        #region MpNews
        /// <summary>
        /// 上传永久图文素材
        /// </summary>
        /// <param name="mpNews">图文素材信息</param>
        /// <param name="appId">应用ID</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue UploadMpNews(Wechat4net.QY.Define.MediaFile.MpNewsInfo mpNews, int appId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UploadMpNews_Forever, AccessToken.Value);
            var data = new
            {
                agentid = appId,
                mpnews = mpNews.GetData()
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UploadMpNews] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UploadMpNews] result = " + result);
            }

            return JsonConvert.DeserializeObject<Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue>(result);
        }

        /// <summary>
        /// 修改永久图文信息
        /// </summary>
        /// <param name="mediaId">图文MediaID</param>
        /// <param name="mpNews">图文素材信息</param>
        /// <param name="appId">应用ID</param>
        /// <returns></returns>
        public static ReturnValue UpdataMpNews(string mediaId, Wechat4net.QY.Define.MediaFile.MpNewsInfo mpNews, int appId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UpdataMpNews, AccessToken.Value);
            var data = new
            {
                agentid = appId,
                media_id = mediaId,
                mpnews = mpNews.GetData()
            };

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdataMpNews] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdataMpNews] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }

        #endregion

        #endregion

        #region Download
        /// <summary>
        /// 下载临时媒体文件
        /// <para>该方法将直接在页面输出文件，所以该方法后面的代码将无法继续执行</para>
        /// </summary>
        /// <param name="mediaId">媒体文件上传后获取的唯一标识</param>
        [Obsolete("该方法已过时，如果下载临时文件请使用DownloadTempFile方法，如果下载永久文件请使用DownloadForeverFile方法。")]
        public static void Download(string mediaId)
        {
            string url = ServiceUrl.DownloadMediaFile_Temp + "?access_token=" + AccessToken.Value + "&media_id=" + mediaId;
            DownloadFile(url);
        }

        /// <summary>
        /// 下载临时媒体文件
        /// <para>该方法将直接在页面输出文件，所以该方法后面的代码将无法继续执行</para>
        /// </summary>
        /// <param name="mediaId">媒体文件上传后获取的唯一标识</param>
        public static void DownloadTempFile(string mediaId)
        {
            string url = string.Format("{0}?access_token={1}&media_id={2}", ServiceUrl.DownloadMediaFile_Temp, AccessToken.Value, mediaId);
            DownloadFile(url);
        }

        /// <summary>
        /// 下载永久媒体文件
        /// <para>该方法将直接在页面输出文件，所以该方法后面的代码将无法继续执行</para>
        /// </summary>
        /// <param name="mediaId">媒体文件上传后获取的唯一标识</param>
        /// <param name="appId">应用ID</param>
        public static void DownloadForeverFile(string mediaId, int appId)
        {
            string url = string.Format("{0}?access_token={1}&media_id={2}&agentid={3}", ServiceUrl.DownloadMediaFile_Forever, AccessToken.Value, mediaId, appId);
            DownloadFile(url);
        }

        private static void DownloadFile(string url)
        {
            WebRequest WebRequestObject = HttpWebRequest.Create(url);
            WebResponse ResponseObject = WebRequestObject.GetResponse();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Headers.Add(ResponseObject.Headers);

            Stream stream = ResponseObject.GetResponseStream();
            MemoryStream stmMemory = new MemoryStream();
            byte[] buffer = new byte[64 * 1024];
            int i;
            while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                stmMemory.Write(buffer, 0, i);
            }
            byte[] arraryByte = stmMemory.ToArray();
            stmMemory.Close();

            if (ResponseObject.ContentType.Contains("application/json"))
            {
                MediaFile.DownloadReturnValue retValue = JsonConvert.DeserializeObject<MediaFile.DownloadReturnValue>(System.Text.Encoding.UTF8.GetString(arraryByte));
                if (retValue.ErrorCode != 0)
                {
                    throw new Exception("获取媒体文件异常。ErrorCode=" + retValue.ErrorCode + ",ErrorMessage=" + retValue.ErrorMessage);
                }
            }

            HttpContext.Current.Response.OutputStream.Write(arraryByte, 0, arraryByte.Length);
        }

        /// <summary>
        /// 下载媒体文件数据
        /// </summary>
        /// <param name="mediaId">媒体文件上传后获取的唯一标识</param>
        /// <returns>下载结果</returns>
        [Obsolete("该方法已过时，如果下载临时文件请使用DownloadTempFile方法，如果下载永久文件请使用DownloadForeverFile方法。")]
        public static MediaFile.DownloadReturnValue DownloadData(string mediaId)
        {
            string url = ServiceUrl.DownloadMediaFile_Temp + "?access_token=" + AccessToken.Value + "&media_id=" + mediaId;
            WebRequest WebRequestObject = HttpWebRequest.Create(url);
            WebResponse ResponseObject = WebRequestObject.GetResponse();

            MediaFile.DownloadReturnValue retValue = new MediaFile.DownloadReturnValue();

            retValue.FileSize = ResponseObject.ContentLength;
            foreach (var key in ResponseObject.Headers.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "content-disposition":
                        int index = ResponseObject.Headers[key].IndexOf("filename=");
                        string[] a = ResponseObject.Headers[key].Remove(0, index + 9).Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
                        if (a.Length > 0)
                        {
                            retValue.FileName = a[0];
                        }
                        break;
                    case "content-type":
                        retValue.ContentType = ResponseObject.Headers[key];
                        break;
                    default:
                        break;
                }
            }

            retValue.FileStream = ResponseObject.GetResponseStream();
            MemoryStream stmMemory = new MemoryStream();
            byte[] buffer = new byte[64 * 1024];
            int i;
            while ((i = retValue.FileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                stmMemory.Write(buffer, 0, i);
            }
            retValue.FileData = stmMemory.ToArray();
            stmMemory.Close();

            if (ResponseObject.ContentType.Contains("application/json"))
            {
                MediaFile.DownloadReturnValue retValue2 = JsonConvert.DeserializeObject<MediaFile.DownloadReturnValue>(System.Text.Encoding.UTF8.GetString(retValue.FileData));
                if (retValue2.ErrorCode != 0)
                {
                    retValue = retValue2;
                }
            }

            return retValue;
        }
        #endregion

        #region QuiryCount
        /// <summary>
        /// 查询永久素材数量
        /// </summary>
        /// <returns></returns>
        public static Wechat4net.QY.Define.MediaFile.QuiryCountReturnValue GetForeverMediaFileCount(int appId)
        {
            string url = string.Format("{0}?access_token={1}&agentid={2}", ServiceUrl.GetAllMediaFilesCount, AccessToken.Value, appId);
            return HttpHelper.Get<Wechat4net.QY.Define.MediaFile.QuiryCountReturnValue>(url);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="mediaId">素材MediaID</param>
        /// <returns></returns>
        public static ReturnValue DeleteForeverMedia(int appId, string mediaId)
        {
            string url = string.Format("{0}?access_token={1}&agentid={2}&media_id={3}", ServiceUrl.DeleteMedia, AccessToken.Value, appId, mediaId);
            return HttpHelper.Get<ReturnValue>(url);
        }
        #endregion

        /// <summary>
        /// 获取永久素材列表(除图文素材)
        /// </summary>
        /// <param name="fileType">素材类型，支持：图片（Image）、视频（Video）、语音 （Voice）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="appId">企业号应用ID</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.MediaFile.QuiryFileListReturnValue GetForeverMediaFileList(Wechat4net.QY.Define.MediaFile.FileType fileType, int offset, int count, int appId)
        {
            string type = Wechat4net.QY.Define.MediaFile.GetFileTypeString(fileType);
            return GetForeverMediaFileList(type, offset, count, appId);
        }

        /// <summary>
        /// 获取永久素材列表(除图文素材)
        /// </summary>
        /// <param name="fileType">素材类型字符串，支持：图片（image）、视频（video）、语音 （voice）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="appId">企业号应用ID</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.MediaFile.QuiryFileListReturnValue GetForeverMediaFileList(string fileType, int offset, int count, int appId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.GetMediaList, AccessToken.Value);
            var data = new
            {
                type = fileType,
                agentid = appId,
                offset = offset,
                count = count
            };
            //return Create().Post<QuiryNewsListReturnValue>(url, date);

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverMediaFileList] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverMediaFileList] result = " + result);
            }

            return JsonConvert.DeserializeObject<Wechat4net.QY.Define.MediaFile.QuiryFileListReturnValue>(result);
        }

        /// <summary>
        /// 获取永久图文素材列表
        /// </summary>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static Wechat4net.QY.Define.MediaFile.QuiryNewsListReturnValue GetForeverNewsList(int appId, int offset, int count)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.GetMediaList, AccessToken.Value);
            var data = new
            {
                type = "mpnews",
                agentid = appId,
                offset = offset,
                count = count
            };
            //return Create().Post<QuiryNewsListReturnValue>(url, date);

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverNewsList] Json = " + json);
            }

            //var Post = CreatePost(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();
            //string result = System.Text.Encoding.UTF8.GetString(rs);

            string result = HttpHelper.Post(url, json);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverNewsList] result = " + result);
            }

            return JsonConvert.DeserializeObject<Wechat4net.QY.Define.MediaFile.QuiryNewsListReturnValue>(result);
        }

    }
}
