using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

using Wechat4net.Define;
using Wechat4net.MP.Common;
using Wechat4net.MP.Define.MediaFile;
using Wechat4net.MP.Utils;
using Wechat4net.Utils;

namespace Wechat4net.MP
{
    /// <summary>
    /// 多媒体文件管理类
    /// </summary>
    public static class MediaFileManager
    {
        #region 辅助类
        public static Logger Logger
        {
            get
            {
                Logger _Logger = new Logger(AppSettings.LogPath);
                return _Logger;
            }
        }

        private static Wechat4net.Utils.Post _Post = null;
        private static Wechat4net.Utils.Post Post
        {
            get
            {
                if (_Post == null)
                {
                    _Post = new Wechat4net.Utils.Post();
                }
                return _Post;
            }
        }
        #endregion

        #region Upload
        /// <summary>
        /// 上传媒体文件
        /// <para>图片（image）: 1M，支持JPG格式</para>
        /// <para>语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式</para>
        /// <para>视频（video）：10MB，支持MP4格式</para>
        /// <para>缩略图（thumb）：64KB，支持JPG格式</para>
        /// </summary>
        /// <param name="mediaFile">媒体文件</param>
        /// <param name="isTemp">是否临时</param>
        /// <returns>上传结果</returns>
        public static UploadReturnValue UploadFile(Wechat4net.MP.Define.MediaFile.Type.Base mediaFile, bool isTemp)
        {
            if (!File.Exists(mediaFile.FilePath))
            {
                return new UploadReturnValue(-1, "文件 " + mediaFile.FilePath + " 不存在");
            }
            string url = string.Format("{0}?access_token={1}&type={2}", isTemp ? ServiceUrl.UploadMediaFile_Temp : ServiceUrl.UploadMediaFile,
                AccessToken.Value,
                Wechat4net.MP.Define.MediaFile.Utils.GetFileTypeString(mediaFile.FileType));
            FileInfo file = new FileInfo(mediaFile.FilePath);

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddFileParameter("media", file);
            if (mediaFile is Wechat4net.MP.Define.MediaFile.Type.Video && !isTemp)
            {
                string json = JsonConvert.SerializeObject((mediaFile as Wechat4net.MP.Define.MediaFile.Type.Video).GetData());
                Post.AddTextParameter("description", json);
            }
            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            return JsonConvert.DeserializeObject<Wechat4net.MP.Define.MediaFile.UploadReturnValue>(result);
        }

        /// <summary>
        /// 上传图文消息
        /// </summary>
        /// <param name="mpNews">图文消息</param>
        /// <param name="isTemp">是否临时</param>
        /// <returns>上传结果</returns>
        public static UploadReturnValue UploadNews(Wechat4net.MP.Define.MediaFile.Type.MpNews mpNews, bool isTemp)
        {
            string json = JsonConvert.SerializeObject(mpNews.GetData());
            string url = string.Format("{0}?access_token={1}", isTemp ? ServiceUrl.UploadNews_Temp : ServiceUrl.UploadNews, AccessToken.Value);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UploadNews] Json = " + json);
            }

            Post.ClearData();
            Post.SetUrl(url);
            //Post.AddTextParameter("media", json);
            Post.AddText(json);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UploadNews] PostString = " + System.Text.Encoding.UTF8.GetString(Post.bs));
            }

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            return JsonConvert.DeserializeObject<UploadReturnValue>(result);
        }

        ///// <summary>
        ///// 上传媒体文件
        ///// </summary>
        ///// <param name="mediaFile"></param>
        ///// <param name="isTemp"></param>
        ///// <returns></returns>
        //public static UploadReturnValue Upload(Wechat4net.MP.Define.MediaFile.Base mediaFile, bool isTemp)
        //{
        //    string url = string.Format("{0}?access_token={1}&type={2}", ServiceUrl.UploadMediaFile_Temp, AccessToken.Value, Wechat4net.MP.Define.MediaFile.Utils.GetFileTypeString(mediaFile.FileType));
        //    string result = string.Empty;

        //    if (mediaFile is Wechat4net.MP.Define.MediaFile.MpNews)
        //    {
        //        url = string.Format("{0}?access_token={1}", isTemp ? ServiceUrl.UploadNews_Temp : ServiceUrl.UploadNews, AccessToken.Value);
        //        string json = JsonSerializer.Serialize((mediaFile as Wechat4net.MP.Define.MediaFile.MpNews).GetData());
        //        Post.SetUrl(url);
        //        Post.AddTextParameter("media", json);
        //        byte[] rs = Post.Send();
        //        result = System.Text.Encoding.UTF8.GetString(rs);
        //    }
        //    else
        //    {
        //        string filePath = mediaFile.FilePath;
        //        if (!File.Exists(filePath))
        //        {
        //            return new UploadReturnValue(-1, "文件 " + filePath + " 不存在");
        //        }
        //        FileInfo file = new FileInfo(filePath);
        //        Post.SetUrl(url);
        //        Post.AddFileParameter("media", file);
        //        if (mediaFile is Wechat4net.MP.Define.MediaFile.Video && !isTemp)
        //        {
        //            string json = JsonSerializer.Serialize((mediaFile as Wechat4net.MP.Define.MediaFile.Video).GetData());
        //            Post.AddTextParameter("description", json);
        //        }
        //        byte[] rs = Post.Send();
        //        result = System.Text.Encoding.UTF8.GetString(rs);

        //    }

        //    return AjaxEngine.Gloabl.Serializer.Deserialize<Wechat4net.MP.Define.MediaFile.UploadReturnValue>(result);
        //}

        #endregion

        #region Delete
        /// <summary>
        /// 删除永久素材接口
        /// <para>请谨慎操作本接口，因为它可以删除公众号在公众平台官网素材管理模块中新建的图文消息、语音、视频等素材（但需要先通过获取素材列表来获知素材的media_id）</para>
        /// <para>临时素材无法通过本接口删除</para>
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static ReturnValue DeleteForeverMediaFile(string mediaId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.DeleteMediaFile, AccessToken.Value);
            var data = new
            {
                media_id = mediaId
            };
            //return Create().Post<ReturnValue>(url, data);

            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[DeleteForeverMediaFile] Json = " + json);
            }

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddText(json);

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[DeleteForeverMediaFile] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);

        }
        #endregion

        #region QuiryCount
        /// <summary>
        /// 查询永久素材数量
        /// </summary>
        /// <returns></returns>
        public static QuiryCountReturnValue GetForeverMediaFileCount()
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.GetForeverMediaFileCount, AccessToken.Value);
            return HttpHelper.Get<QuiryCountReturnValue>(url);
        }
        #endregion

        #region QuiryList
        /// <summary>
        /// 获取永久图文素材列表
        /// </summary>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static QuiryNewsListReturnValue GetForeverNewsList(int offset, int count)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.GetForeverMediaFileList, AccessToken.Value);
            var data = new
            {
                type = "news",
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

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddText(json);

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverNewsList] result = " + result);
            }

            return JsonConvert.DeserializeObject<QuiryNewsListReturnValue>(result);
        }

        /// <summary>
        /// 获取永久素材列表(除图文素材)
        /// </summary>
        /// <param name="fileType">素材类型，支持：图片（Image）、视频（Video）、语音 （Voice）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static QuiryMediaFileListReturnValue GetForeverMediaFileList(Wechat4net.MP.Define.MediaFile.Utils.FileType fileType, int offset, int count)
        {
            string type = string.Empty;
            switch (fileType)
            {
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.Image:
                    type = "image";
                    break;
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.Voice:
                    type = "voice";
                    break;
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.Video:
                    type = "video";
                    break;
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.Thumb:
                    type = "image";
                    break;
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.News:
                case Wechat4net.MP.Define.MediaFile.Utils.FileType.Unknow:
                    goto default;
                default:
                    throw new Exception("素材类型错误");
            }
            string url = string.Format("{0}?access_token={1}", ServiceUrl.GetForeverMediaFileList, AccessToken.Value);
            var data = new
            {
                type = type,
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

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddText(json);

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverMediaFileList] result = " + result);
            }

            return JsonConvert.DeserializeObject<QuiryMediaFileListReturnValue>(result);
        }

        /// <summary>
        /// 获取永久素材列表(除图文素材)
        /// </summary>
        /// <param name="fileType">素材类型字符串，支持：图片（image）、视频（video）、语音 （voice）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static QuiryMediaFileListReturnValue GetForeverMediaFileList(string fileType, int offset, int count)
        {
            return GetForeverMediaFileList(Wechat4net.MP.Define.MediaFile.Utils.GetFileTypeEnum(fileType), offset, count);
        }
        #endregion

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId">要修改的永久图文素材的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="newsItem">图文页内容</param>
        /// <returns></returns>
        public static ReturnValue UpdateForeverNews(string mediaId, int index, Wechat4net.MP.Define.MediaFile.Type.MpNews.Article newsItem)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.UpdateForeverNews, AccessToken.Value);
            var data = new
            {
                media_id = mediaId,
                index = index,
                articles = newsItem.GetData()
            };
            //return Create().Post<ReturnValue>(url, data);
            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateForeverNews] Json = " + json);
            }

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddText(json);

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[UpdateForeverNews] result = " + result);
            }

            return JsonConvert.DeserializeObject<ReturnValue>(result);
        }

        /*
            {
                "news_item": [
                    {
                        "title": "这儿是标题！",
                        "author": "我是作者",
                        "digest": "没啥可描述的...",
                        "content": "This is a test MpNews message!",
                        "content_source_url": "http://www.baidu.com",
                        "thumb_media_id": "1108310245766",
                        "show_cover_pic": 1,
                        "url": "http://mp.weixin.qq.com/s?__biz=MzIwMTE3MTg1Ng==&mid=208683434&idx=1&sn=ac8530f47340c1c64d9c0ba54296c5de#rd"
                    }
                ]
            }
         */
        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="mediaId">永久图文素材的id</param>
        /// <returns></returns>
        public static QuiryNewsReturnValue GetForeverNews(string mediaId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.DownloadForeverMediaFile, AccessToken.Value);
            var data = new
            {
                media_id = mediaId
            };
            //return Create().Post<ReturnValue>(url, data);
            string json = JsonConvert.SerializeObject(data);
            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverNews] Json = " + json);
            }

            Post.ClearData();
            Post.SetUrl(url);
            Post.AddText(json);

            byte[] rs = Post.Send();
            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[GetForeverNews] result = " + result);
            }

            return JsonConvert.DeserializeObject<QuiryNewsReturnValue>(result);
        }

        /// <summary>
        /// 获取其他临时素材（除图文素材外）
        /// </summary>
        /// <param name="mediaId">临时素材id</param>
        public static void DownloadTempMediaFile(string mediaId)
        {
            string url = string.Format("{0}?access_token={1}&media_id={2}", ServiceUrl.DownloadTempMediaFile, AccessToken.Value, mediaId);

            //WebRequest WebRequestObject = HttpWebRequest.Create(url);
            //WebResponse ResponseObject = WebRequestObject.GetResponse();

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Headers.Add(ResponseObject.Headers);

            //Stream stream = ResponseObject.GetResponseStream();
            //MemoryStream stmMemory = new MemoryStream();
            //byte[] buffer = new byte[64 * 1024];
            //int i;
            //while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    stmMemory.Write(buffer, 0, i);
            //}
            //byte[] arraryByte = stmMemory.ToArray();
            //stmMemory.Close();

            //if (ResponseObject.ContentType.Contains("application/json"))
            //{
            //    ReturnValue retValue = AjaxEngine.Gloabl.Serializer.Deserialize<ReturnValue>(System.Text.Encoding.UTF8.GetString(arraryByte));
            //    if (retValue.ErrorCode != 0)
            //    {
            //        throw new Exception("获取媒体文件异常。ErrorCode=" + retValue.ErrorCode + ",ErrorMessage=" + retValue.ErrorMessage);
            //    }
            //}


            byte[] arraryByte = Download(url);

            HttpContext.Current.Response.OutputStream.Write(arraryByte, 0, arraryByte.Length);
            HttpContext.Current.Response.End();
        }

        public static void DownloadForeverMediaFile(string mediaId)
        {
            string url = string.Format("{0}?access_token={1}", ServiceUrl.DownloadForeverMediaFile, AccessToken.Value);
            var data = new
            {
                media_id = mediaId
            };
            //return Create().Post<ReturnValue>(url, data);
            string json = JsonConvert.SerializeObject(data);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[DownloadForeverMediaFile] Json = " + json);
            }

            //Post.ClearData();
            //Post.SetUrl(url);
            //Post.AddText(json);

            //byte[] rs = Post.Send();

            WebClient wc = new WebClient();
            var rs = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(json) ? "" : json));

            string result = System.Text.Encoding.UTF8.GetString(rs);

            if (AppSettings.IsDebug)
            {
                //DebugLog
                Logger.Info("[DownloadForeverMediaFile] result = " + result);
            }

            var videoRet = JsonConvert.DeserializeObject<QuiryVideoReturnValue>(result);
            if (videoRet.ErrorCode != 0)
            {
                throw new Exception("获取永久媒体文件异常。ErrorCode=" + videoRet.ErrorCode + ",ErrorMessage=" + videoRet.ErrorMessage);
            }

            if (!string.IsNullOrWhiteSpace(videoRet.DownloadUrl))
            {
                rs = Download(url);
            }

            HttpContext.Current.Response.OutputStream.Write(rs, 0, rs.Length);
            HttpContext.Current.Response.End();
        }

        private static byte[] Download(string url)
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

            //if (ResponseObject.ContentType.Contains("application/json"))
            //{
            ReturnValue retValue = JsonConvert.DeserializeObject<ReturnValue>(System.Text.Encoding.UTF8.GetString(arraryByte));
            if (retValue.ErrorCode != 0)
            {
                throw new Exception("获取媒体文件异常。ErrorCode=" + retValue.ErrorCode + ",ErrorMessage=" + retValue.ErrorMessage);
            }
            //}

            return arraryByte;
        }
    }
}
