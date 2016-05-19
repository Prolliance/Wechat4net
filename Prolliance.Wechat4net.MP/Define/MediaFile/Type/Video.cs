using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wechat4net.MP.Define.MediaFile.Type
{
    public class Video : Base
    {
        internal override Utils.FileType FileType
        {
            get { return Utils.FileType.Video; }
        }

        public string Title { set; get; }
        public string Introduction { set; get; }

        public object GetData()
        {
            return new { title = this.Title, introduction = this.Introduction };
        }
    }
}
