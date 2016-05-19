using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Wechat4net.Utils
{
    public class Post
    {
        string boundary = "------Wechat4net by 5Kong";
        string url;
public byte[] bs = new byte[0];
        Encoding encoder = Encoding.UTF8;


        public Post() { }

        public Post(string url)
        {
            this.url = url;
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }

        public byte[] Send()
        {
            WebClient myWebClient = new WebClient();
            myWebClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            EndData();
            return myWebClient.UploadData(url, bs);
        }
        public void ClearData()
        {
            Array.Clear(bs, 0, bs.Length);
        }
        public void AddText(string value)
        {
            StringBuilder s = new StringBuilder();
            //s.Append("--").Append(boundary).Append("\r\n");
            //s.Append("Content-Disposition:  form-data; \"\r\n");
            s.Append("\r\n");
            s.Append(value).Append("\r\n");
            AppendString(s.ToString());
        }
        public void AddTextParameter(string name, string value)
        {
            StringBuilder s = new StringBuilder();
            s.Append("--").Append(boundary).Append("\r\n");
            s.Append("Content-Disposition:  form-data;  name=\"" + name + "\"\r\n");
            s.Append("\r\n");
            s.Append(value).Append("\r\n");
            AppendString(s.ToString());
        }
        public void AddFileParameter(string name, FileInfo file)
        {
            StringBuilder s = new StringBuilder();
            s.Append("--").Append(boundary).Append("\r\n");
            s.Append("Content-Disposition:  form-data;  name=\"" + name + "\";  filename=\"" + file.Name + "\"\r\n");
            s.Append("Content-Type: " + GetContentType(file) + "\r\n");
            s.Append("\r\n");
            AppendString(s.ToString());
            AppendBytes(GetFileBytes(file));
            AppendString("\r\n");
        }

        public void AddFileParameter(string name, string base64, string fileName, string ext)
        {
            StringBuilder s = new StringBuilder();
            s.Append("--").Append(boundary).Append("\r\n");
            s.Append("Content-Disposition:  form-data;  name=\"" + name + "\";  filename=\"" + fileName + "." + ext + "\"\r\n");
            s.Append("Content-Type: " + GetFormat(ext) + "\r\n");
            s.Append("\r\n");
            AppendString(s.ToString());
            AppendBytes(GetFileBytes(base64));
            AppendString("\r\n");
        }

        byte[] GetFileBytes(FileInfo f)
        {
            FileStream fs = new FileStream(f.FullName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] b = br.ReadBytes((int)f.Length);
            br.Close();
            fs.Close();
            return b;
        }
        byte[] GetFileBytes(string base64)
        {
            return Convert.FromBase64String(base64);
        }
        string GetContentType(FileInfo f)
        {
            // return "application/octet-stream";  //取消注释此行，则不再区分是否为图片
            System.Drawing.Image image = null;
            FileStream fs = new FileStream(f.FullName, FileMode.Open);
            try
            {
                image = System.Drawing.Image.FromStream(fs);
            }
            catch (ArgumentException)
            {
                // 文件无法被解析为一个图片
                return "application/octet-stream";
            }
            finally
            {
                fs.Close();
            }
            return GetFormat(image.RawFormat);
        }
        string GetFormat(ImageFormat imf)
        {
            if (ImageFormat.Bmp.Equals(imf))
            {
                return "image/bmp";
            }
            if (ImageFormat.Jpeg.Equals(imf))
            {
                return "image/jpeg";
            }
            if (ImageFormat.Png.Equals(imf))
            {
                return "image/png";
            }
            return "application/octet-stream";
        }
        string GetFormat(string ext)
        {
            if (!string.IsNullOrEmpty(ext))
            {
                if ("bmp".Equals(ext.ToLower()))
                {
                    return "image/bmp";
                }
                if ("jpg".Equals(ext.ToLower()))
                {
                    return "image/jpeg";
                }
                if ("png".Equals(ext.ToLower()))
                {
                    return "image/png";
                }
            }
            return "application/octet-stream";
        }
        void AppendBytes(byte[] bytes)
        {
            byte[] newByte = new byte[bs.Length + bytes.Length];
            Array.Copy(bs, 0, newByte, 0, bs.Length);
            Array.Copy(bytes, 0, newByte, bs.Length, bytes.Length);
            bs = newByte;
        }
        void AppendString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            AppendBytes(bytes);
        }
        void EndData()
        {
            StringBuilder s = new StringBuilder();
            s.Append("--").Append(boundary).Append("--\r\n");
            s.Append("\r\n");
            AppendString(s.ToString());
        }
    }

}
