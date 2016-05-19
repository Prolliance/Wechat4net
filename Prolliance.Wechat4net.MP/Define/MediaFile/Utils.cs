using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.MediaFile
{
    public class Utils
    {
        /// <summary>
        /// 媒体文件类型枚举
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 图片（image）
            /// </summary>
            Image = 1,
            /// <summary>
            /// 语音（voice）
            /// </summary>
            Voice = 2,
            /// <summary>
            /// 视频（video）
            /// </summary>
            Video = 3,
            /// <summary>
            /// 缩略图(thumb)
            /// </summary>
            Thumb = 4,
            /// <summary>
            /// 图文消息(news)
            /// </summary>
            News = 5
        }

        internal static FileType GetFileTypeEnum(string type)
        {
            FileType fte = FileType.Unknow;

            string[] enumList = Enum.GetNames(typeof(FileType));
            for (int i = 0; i < enumList.Length; i++)
            {
                if (enumList[i].ToLower() == type.ToLower())
                {
                    fte = (FileType)Enum.ToObject(typeof(FileType), i);
                    break;
                }
            }

            return fte;
        }

        internal static string GetFileTypeString(FileType type)
        {
            return Enum.GetName(typeof(FileType), type);
        }
    }
}