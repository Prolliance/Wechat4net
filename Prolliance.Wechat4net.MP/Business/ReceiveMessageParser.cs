using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Wechat4net.Utils;
using ReceiveMessage = Wechat4net.MP.Define.ReceiveMessage;
using ReceiveMessageEnum = Wechat4net.Utils.Enums.MP.ReceiveMessageEnum;

namespace Wechat4net.MP.Business
{
    /// <summary>
    /// 接收消息解析类
    /// </summary>
    internal class ReceiveMessageParser
    {
        /// <summary>
        /// 根据xml获取消息类型实体
        /// </summary>
        /// <param name="xml">接收到消息解密后的xml</param>
        /// <param name="receiveMessageEnum">消息类型枚举</param>
        /// <returns>消息类型</returns>
        public static ReceiveMessage.Base Parse(XmlDocument xml, ref ReceiveMessageEnum receiveMessageEnum)
        {
            XmlNode root = xml.FirstChild;
            string msgType = root["MsgType"].InnerText.ToLower();
            string Event = string.Empty;
            if (root["Event"] != null)
            {
                Event = root["Event"].InnerText.ToLower();
            }

            receiveMessageEnum = Enums.MP.getEnum(msgType, Event);

            ReceiveMessage.Base rec = null;

            switch (receiveMessageEnum)
            {
                case ReceiveMessageEnum.Unknow:
                    break;
                case ReceiveMessageEnum.Text:
                    rec = new ReceiveMessage.Text();
                    break;
                case ReceiveMessageEnum.Image:
                    rec = new ReceiveMessage.Image();
                    break;
                case ReceiveMessageEnum.Voice:
                    rec = new ReceiveMessage.Voice();
                    break;
                case ReceiveMessageEnum.Video:
                    rec = new ReceiveMessage.Video();
                    break;
                case ReceiveMessageEnum.ShortVideo:
                    rec = new ReceiveMessage.ShortVideo();
                    break;
                case ReceiveMessageEnum.Location:
                    rec = new ReceiveMessage.Location();
                    break;
                case ReceiveMessageEnum.Link:
                    rec = new ReceiveMessage.Link();
                    break;
                case ReceiveMessageEnum.Event_Subscribe:
                    rec = new ReceiveMessage.EventSubscribe();
                    break;
                case ReceiveMessageEnum.Event_Scan:
                    rec = new ReceiveMessage.EventScan();
                    break;
                case ReceiveMessageEnum.Event_Location:
                    rec = new ReceiveMessage.EventLocation();
                    break;
                case ReceiveMessageEnum.Event_Click:
                    rec = new ReceiveMessage.EventClick();
                    break;
                case ReceiveMessageEnum.Event_View:
                    rec = new ReceiveMessage.EventView();
                    break;
                case ReceiveMessageEnum.Event_MasssEndJobFinish:
                    rec = new ReceiveMessage.EventMasssEndJobFinish();
                    break;
                default:
                    break;
            }

            rec.FillReceiveMessageFromXml(xml);
            return rec;
        }
    }
}
