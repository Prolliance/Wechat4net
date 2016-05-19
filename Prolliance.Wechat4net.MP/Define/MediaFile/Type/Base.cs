using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.MediaFile.Type
{
    public abstract class Base
    {
        internal abstract Utils.FileType FileType { get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string FilePath { set; get; }
        //internal abstract object GetData();

    }
}
