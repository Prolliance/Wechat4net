using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using PushMessageEnum = Wechat4net.Utils.Enums.QY.PushMessageEnum;
using PushMessage = Wechat4net.QY.Define.PushMessage;

namespace Wechat4net.QY.Business
{
    /// <summary>
    /// 推送消息构建类
    /// </summary>
    internal static class PushMessageBuilder
    {
        #region BuildJson 构建推送信息Json字符串
        /// <summary>
        /// 构建推送信息Json字符串
        /// </summary>
        /// <param name="pushMessage">推送信息对象</param>
        /// <returns>未加密的Json字符串</returns>
        public static string BuildJson(PushMessage.Base pushMessage)
        {
            if (pushMessage == null)
            {
                return "";
            }
            if (pushMessage.messageType == PushMessageEnum.Unknow)
            {
                throw new Exception("未知的推送消息类型");
            }

            return JsonConvert.SerializeObject(pushMessage.GetData());

            #region 注释代码
            //switch (pushMessage.messageType)
            //{
            //    case PushMessageEnum.Unknow:
            //        throw new Exception("未知的推送消息类型");
            //    case PushMessageEnum.Text:
            //        PushMessage.Text text = pushMessage as PushMessage.Text;
            //        return BuildTextJson(text);
            //    case PushMessageEnum.Image:
            //        PushMessage.Image image = pushMessage as PushMessage.Image;
            //        return BuildImageJson(image);
            //    case PushMessageEnum.Voice:
            //        PushMessage.Voice voice = pushMessage as PushMessage.Voice;
            //        return BuildVoiceJson(voice);
            //    case PushMessageEnum.Video:
            //        PushMessage.Video video = pushMessage as PushMessage.Video;
            //        return BuildVideoJson(video);
            //    case PushMessageEnum.File:
            //        PushMessage.File file = pushMessage as PushMessage.File;
            //        return BuildFileJson(file);
            //    case PushMessageEnum.News:
            //        PushMessage.News news = pushMessage as PushMessage.News;
            //        return BuildNewsJson(news);
            //    case PushMessageEnum.MpNews:
            //        break;
            //    default:
            //        throw new Exception("未知的推送消息类型");
            //}
            //return "";
        }

        //#region 内部方法

        //#region GetPushObject 获取消息推送对象信息
        ///// <summary>
        ///// 获取消息推送对象信息
        ///// </summary>
        ///// <param name="pushMessage">推送信息</param>
        ///// <param name="toUser">人员对象列表 |分隔</param>
        ///// <param name="toParty">部门对象列表 |分隔</param>
        ///// <param name="toTag">标签对象列表 |分隔</param>
        //private static void GetPushObject(PushMessage.Base pushMessage, out string toUser, out string toParty, out string toTag)
        //{
        //    if (pushMessage.ToAll)
        //    {
        //        toUser = "@all";
        //        toParty = "";
        //        toTag = "";
        //    }
        //    else
        //    {
        //        StringBuilder sb_user = new StringBuilder();
        //        foreach (string u in pushMessage.ToUser)
        //        {
        //            if (!string.IsNullOrEmpty(u))
        //            {
        //                sb_user.Append(u).Append("|");
        //            }
        //        }
        //        toUser = sb_user.ToString();
        //        if (toUser.Length > 0)
        //        {
        //            toUser = toUser.Remove(toUser.Length - 1);
        //        }

        //        StringBuilder sb_party = new StringBuilder();
        //        foreach (string p in pushMessage.ToParty)
        //        {
        //            if (!string.IsNullOrEmpty(p))
        //            {
        //                sb_party.Append(p).Append("|");
        //            }
        //        }
        //        toParty = sb_party.ToString();
        //        if (toParty.Length > 0)
        //        {
        //            toParty = toParty.Remove(toParty.Length - 1);
        //        }

        //        StringBuilder sb_tag = new StringBuilder();
        //        foreach (string t in pushMessage.ToTag)
        //        {
        //            if (!string.IsNullOrEmpty(t))
        //            {
        //                sb_tag.Append(t).Append("|");
        //            }
        //        }
        //        toTag = sb_tag.ToString();
        //        if (toTag.Length > 0)
        //        {
        //            toTag = toTag.Remove(toTag.Length - 1);
        //        }
        //    }
        //    return;
        //}
        //#endregion

        //#region BuildTextJson
        ///// <summary>
        ///// 构造文本类型推送消息Json
        ///// </summary>
        ///// <param name="text">文本对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildTextJson(PushMessage.Text text)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "totag": " TagID1 | TagID2 ",
        //       "msgtype": "text",
        //       "agentid": "1",
        //       "text": {
        //           "content": "Holiday Request For Pony(http://xxxxx)"
        //       },
        //       "safe":"0"
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(text, out touser, out toparty, out totag);

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "text",
        //        agentid = text.AppID,
        //        text = new
        //        {
        //            content = text.Content
        //        },
        //        safe = text.IsSafe ? "1" : "0"
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion

        //#region BuildImageJson
        ///// <summary>
        ///// 构造图片类型推送消息Json
        ///// </summary>
        ///// <param name="image">图片对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildImageJson(PushMessage.Image image)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "msgtype": "image",
        //       "agentid": "1",
        //       "image": {
        //           "media_id": "MEDIA_ID"
        //       },
        //       "safe":"0"
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(image, out touser, out toparty, out totag);

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "image",
        //        agentid = image.AppID,
        //        image = new
        //        {
        //            media_id = image.MediaID
        //        },
        //        safe = image.IsSafe ? "1" : "0"
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion

        //#region BuildVoiceJson
        ///// <summary>
        ///// 构造语音类型推送消息Json
        ///// </summary>
        ///// <param name="voice">语音对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildVoiceJson(PushMessage.Voice voice)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "totag": " TagID1 | TagID2 ",
        //       "msgtype": "voice",
        //       "agentid": "1",
        //       "voice": {
        //           "media_id": "MEDIA_ID"
        //       },
        //       "safe":"0"
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(voice, out touser, out toparty, out totag);

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "voice",
        //        agentid = voice.AppID,
        //        voice = new
        //        {
        //            media_id = voice.MediaID
        //        },
        //        safe = voice.IsSafe ? "1" : "0"
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion

        //#region BuildVideoJson
        ///// <summary>
        ///// 构造视频类型推送消息Json
        ///// </summary>
        ///// <param name="video">视频对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildVideoJson(PushMessage.Video video)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "totag": " TagID1 | TagID2 ",
        //       "msgtype": "video",
        //       "agentid": "1",
        //       "video": {
        //           "media_id": "MEDIA_ID",
        //           "title": "Title",
        //           "description": "Description"
        //       },
        //       "safe":"0"
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(video, out touser, out toparty, out totag);

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "video",
        //        agentid = video.AppID,
        //        video = new
        //        {
        //            media_id = video.MediaID,
        //            title = video.Title,
        //            description = video.Description
        //        },
        //        safe = video.IsSafe ? "1" : "0"
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion

        //#region BuildFileJson
        ///// <summary>
        ///// 构造文件类型推送消息Json
        ///// </summary>
        ///// <param name="file">文件对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildFileJson(PushMessage.File file)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "totag": " TagID1 | TagID2 ",
        //       "msgtype": "file",
        //       "agentid": "1",
        //       "file": {
        //           "media_id": "MEDIA_ID"
        //       },
        //       "safe":"0"
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(file, out touser, out toparty, out totag);

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "file",
        //        agentid = file.AppID,
        //        file = new
        //        {
        //            media_id = file.MediaID
        //        },
        //        safe = file.IsSafe ? "1" : "0"
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion

        //#region BuildNewsJson
        ///// <summary>
        ///// 构造新闻类型推送消息Json
        ///// </summary>
        ///// <param name="news">新闻对象</param>
        ///// <returns>未加密的Json字符串</returns>
        //private static string BuildNewsJson(PushMessage.News news)
        //{
        //    #region _______示例_______
        //    /*
        //    {
        //       "touser": "UserID1|UserID2|UserID3",
        //       "toparty": " PartyID1 | PartyID2 ",
        //       "totag": " TagID1 | TagID2 ",
        //       "msgtype": "news",
        //       "agentid": "1",
        //       "news": {
        //           "articles":[
        //               {
        //                   "title": "Title",
        //                   "description": "Description",
        //                   "url": "URL",
        //                   "picurl": "PIC_URL"
        //               },
        //               {
        //                   "title": "Title",
        //                   "description": "Description",
        //                   "url": "URL",
        //                   "picurl": "PIC_URL"
        //               }    
        //           ]
        //       }
        //    }
        //    */
        //    #endregion

        //    string touser, toparty, totag;
        //    GetPushObject(news, out touser, out toparty, out totag);

        //    List<object> newsList = new List<object>();
        //    foreach (var item in news.NewsList)
        //    {
        //        var newsItem = new
        //        {
        //            title = item.Title,
        //            description = item.Description,
        //            url = item.Url,
        //            picurl = item.PicUrl
        //        };
        //        newsList.Add(newsItem);
        //    }

        //    var data = new
        //    {
        //        touser = touser,
        //        toparty = toparty,
        //        totag = totag,
        //        msgtype = "news",
        //        agentid = news.AppID,
        //        news = new
        //        {
        //            articles = newsList
        //        }
        //    };

        //    return JsonSerializer.Serialize(data);
        //}
        //#endregion


        //#endregion

        #endregion
        #endregion
    }
}
