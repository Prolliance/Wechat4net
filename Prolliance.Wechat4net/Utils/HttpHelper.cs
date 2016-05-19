using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace Wechat4net.Utils
{
    public class HttpHelper
    {
        private static WebClient _WebClient = null;
        private static WebClient WebClient
        {
            get
            {
                if (HttpHelper._WebClient==null)
                {
                    HttpHelper._WebClient = new WebClient();
                    HttpHelper._WebClient.Encoding = Encoding.UTF8;
                    HttpHelper._WebClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                }
                return HttpHelper._WebClient;
            }
        }

        public static T Get<T>(string uri, Dictionary<string, object> data)
        {
            WebClient.QueryString.Clear();
            if (data != null)
            {
                foreach (var key in data.Keys)
                {
                    string val = (data[key] is string) ? (string)data[key] : JsonConvert.SerializeObject(data[key]);
                    WebClient.QueryString.Add(key, val);
                }
            }
            var rs = WebClient.DownloadString(uri);
            return JsonConvert.DeserializeObject<T>(rs);
        }

        //public T Get<T>(string uri, object data)
        //{
        //    WebClient.QueryString.Clear();
        //    if (data != null)
        //    {
        //        foreach (var key in data.GetPropertyInfos())
        //        {
        //            var propVal = data.GetPropertyValue(key.Name);
        //            string val = (propVal is string) ? (string)propVal : Gloabl.Serializer.Serialize(propVal);
        //            this.WebClient.QueryString.Add(key.Name, val);
        //        }
        //    }
        //    var rs = this.WebClient.DownloadString(uri);
        //    return Gloabl.Serializer.Deserialize<T>(rs);
        //}

        public static string Get(string url)
        {
            return WebClient.DownloadString(url);
        }

        public static T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(Get(url));
        }




        public static string Post(string url, string data)
        {
            return WebClient.UploadString(url, data);
        }

        public static string Post(string url, object data)
        {
            string data_str = JsonConvert.SerializeObject(data);
            return Post(url, data_str);
        }

        public static T Post<T>(string url, string data)
        {
            return JsonConvert.DeserializeObject<T>(Post(url, data));
        }

        public static T Post<T>(string url, object data)
        {
            return JsonConvert.DeserializeObject<T>(Post(url, data));
        }



        public static string UploadFile(string url, string fileName)
        {
            return Encoding.UTF8.GetString(WebClient.UploadFile(url, fileName));
        }
    }
}
