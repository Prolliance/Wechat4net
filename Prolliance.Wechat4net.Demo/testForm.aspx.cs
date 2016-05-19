using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Wechat4net.QY.Define.ReceiveMessage;
using Wechat4net.QY.Utils;
using Wechat4net.Utils;

namespace Wechat4net.Demo
{
    public partial class testForm : System.Web.UI.Page
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Wechat4net.MP.Define.ReplyMessage.News.NewsItem> items = new List<Wechat4net.MP.Define.ReplyMessage.News.NewsItem>();
            items.Add(new Wechat4net.MP.Define.ReplyMessage.News.NewsItem()
            {
                Title = "Title1",
                Description = "Description1",
                PicUrl = "PicUrl1",
                Url = "Url1"
            });
            items.Add(new Wechat4net.MP.Define.ReplyMessage.News.NewsItem()
            {
                Title = "Title2",
                Description = "Description2",
                PicUrl = "PicUrl2",
                Url = "Url2"
            });
            Wechat4net.MP.Define.ReplyMessage.News text = new Wechat4net.MP.Define.ReplyMessage.News("to", "from", items);

            var ret = EntityHelper.BuidReplyMessageXml(text);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}