using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.MediaFile.Type
{
    public class Image : Base
    {
        internal override Utils.FileType FileType
        {
            get { return Utils.FileType.Image; }
        }

        //public string FilePath { set; get; }
    }
}
