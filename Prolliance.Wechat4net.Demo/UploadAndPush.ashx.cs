using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wechat4net.Define;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    /// <summary>
    /// UploadAndPush 的摘要说明
    /// </summary>
    public class UploadAndPush : IHttpHandler
    {
        private Logger _Logger = null;
        public Logger Logger
        {
            get
            {
                if (_Logger == null)
                {
                    _Logger = new Logger(HttpContext.Current.Server.MapPath(".") + "\\log");
                }
                return _Logger;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            Logger.Log("URL = " + HttpContext.Current.Server.MapPath(".") + "\\logo.png");

            Wechat4net.QY.Define.PushMessage.Base message = null;
            string type = context.Request.QueryString["type"];
            bool istemp = context.Request.QueryString["istemp"] == "1";
            string mediaID = string.Empty;
            List<string> userList = new List<string>();
            userList.Add("sunzhen");
            //Wechat4net.QY.Define.MediaFile.UploadTempReturnValue uploadRet = new QY.Define.MediaFile.UploadTempReturnValue();
            ReturnValue uploadRet = new ReturnValue();

            switch (type.ToLower())
            {
                case "text":
                    message = new Wechat4net.QY.Define.PushMessage.Text("6", userList, null, null, "This is a test message!\r\n                          by<a href='http://www.tiancaisz.com'>5Kong</a>'Wechat4net", false);
                    break;
                case "image":
                    if (istemp)
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadTempFile(HttpContext.Current.Server.MapPath(".") + "\\logo.png", QY.Define.MediaFile.FileType.Image);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadTempReturnValue).MediaID;
                    }
                    else
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadForeverFile(HttpContext.Current.Server.MapPath(".") + "\\logo.png", QY.Define.MediaFile.FileType.Image, 6);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue).MediaID;
                    }
                    if (uploadRet.ErrorCode != 0)
                    {
                        throw new Exception("上传图片失败!ErrorCode = " + uploadRet.ErrorCode + ";ErrorMessage = " + uploadRet.ErrorMessage);
                    }
                    //mediaID = uploadRet.MediaID;
                    message = new Wechat4net.QY.Define.PushMessage.Image("6", userList, null, null, mediaID, false);
                    break;
                case "voice":
                    if (istemp)
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadTempFile(HttpContext.Current.Server.MapPath(".") + "\\voice.mp3", QY.Define.MediaFile.FileType.Voice);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadTempReturnValue).MediaID;
                    }
                    else
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadForeverFile(HttpContext.Current.Server.MapPath(".") + "\\voice.mp3", QY.Define.MediaFile.FileType.Voice, 6);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue).MediaID;
                    }

                    if (uploadRet.ErrorCode != 0)
                    {
                        throw new Exception("上传语音失败!ErrorCode = " + uploadRet.ErrorCode + ";ErrorMessage = " + uploadRet.ErrorMessage);
                    }
                    //mediaID = uploadRet.MediaID;
                    message = new Wechat4net.QY.Define.PushMessage.Voice("6", userList, null, null, mediaID, false);
                    break;
                case "video":
                    if (istemp)
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadTempFile(HttpContext.Current.Server.MapPath(".") + "\\video.mp4", QY.Define.MediaFile.FileType.Video);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadTempReturnValue).MediaID;
                    }
                    else
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadForeverFile(HttpContext.Current.Server.MapPath(".") + "\\video.mp4", QY.Define.MediaFile.FileType.Video, 6);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue).MediaID;
                    }

                    if (uploadRet.ErrorCode != 0)
                    {
                        throw new Exception("上传视频失败!ErrorCode = " + uploadRet.ErrorCode + ";ErrorMessage = " + uploadRet.ErrorMessage);
                    }
                    //mediaID = uploadRet.MediaID;
                    message = new Wechat4net.QY.Define.PushMessage.Video("6", userList, null, null, mediaID, "标题", "描述信息", false);
                    break;
                case "file":
                    if (istemp)
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadTempFile(HttpContext.Current.Server.MapPath(".") + "\\file.docx", QY.Define.MediaFile.FileType.File);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadTempReturnValue).MediaID;
                    }
                    else
                    {
                        uploadRet = Wechat4net.QY.MediaFileManager.UploadForeverFile(HttpContext.Current.Server.MapPath(".") + "\\file.docx", QY.Define.MediaFile.FileType.File, 6);
                        mediaID = (uploadRet as Wechat4net.QY.Define.MediaFile.UploadForeverReturnValue).MediaID;
                    }
                    if (uploadRet.ErrorCode != 0)
                    {
                        throw new Exception("上传文件失败!ErrorCode = " + uploadRet.ErrorCode + ";ErrorMessage = " + uploadRet.ErrorMessage);
                    }
                    //mediaID = uploadRet.MediaID;
                    message = new Wechat4net.QY.Define.PushMessage.File("6", userList, null, null, mediaID, false);
                    break;
                //case "QYnews":
                //    var list = new List<Wechat4net.QY.Define.MediaFile.Type.QYNews.Article>();
                //    list.Add(new Wechat4net.QY.Define.MediaFile.Type.QYNews.Article()
                //    {
                //        Author = "我是作者",
                //        ContentSourceUrl = "www.tiancaisz.com",
                //        Title = "这儿是标题！",
                //        ShowCoverPic = true,
                //        Digest = "没啥可描述的...",
                //        ThumbMediaID = "3YzxmSGoJidDHs1cElUmOEE27vyimGU4RjfFcNNU719IwCAALTpnTfV41LJAEKWc",
                //        Content = "This is a test QYNews message!</ Br>                          by<a href='http://www.tiancaisz.com'>5Kong</a>'Wechat4net"
                //    });
                //    var QYnews = Wechat4net.QY.MediaFileManager.UploadNews(new Wechat4net.QY.Define.MediaFile.Type.QYNews()
                //    {
                //        Articles = list
                //    }, isteQY);
                //    if (QYnews.ErrorCode != 0)
                //    {
                //        throw new Exception("上传图文消息失败!ErrorCode = " + QYnews.ErrorCode + ";ErrorMessage = " + QYnews.ErrorMessage);
                //    }
                //    mediaID = QYnews.MediaID;
                //    message = new Wechat4net.QY.Define.PushMessage.QYNews(QYnews.MediaID);
                //    break;
                default:
                    break;
            }

            var ret = Wechat4net.QY.PushManager.Push(message);
            //var ret = Wechat4net.QY.PushManager.PushMessageByOpenID(message, new List<string>() { "okAHOvjsIav__n72k38ktxE8Y9Po", "okAHOvjsIav__n72k38ktxE8Y9Po" });


            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Format("MediaID = {0}; ErrorCode = {1}; ErrorMessage = {2}; InvalidUser = {3}", mediaID, ret.ErrorCode, ret.ErrorMessage, ret.InvalidUser));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}