/*
 * 版本: 1.0
 * 描述: 支持多线程，并发的无阻塞日志类
 * 创建: Houfeng
 * 邮件: Houzf@prolliance.cn
 * 
 * 修改记录:
 * 2012-1-4 , Houfeng , 添加文件信息
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wechat4net.Utils
{
    /// <summary>
    /// 提供日志功能
    /// </summary>
    public class Logger
    {
        private ConcurrentQueue<string> LogBuffer = new ConcurrentQueue<string>();
        public string LogFolder { get; set; }
        private bool IsProcessing { get; set; }

        public Logger(string logFolder)
        {
            this.LogFolder = logFolder;
        }

        private void BufferToFile()
        {
            if (!this.IsProcessing)
            {
                this.IsProcessing = true;
                while (this.LogBuffer.Count > 0)
                {
                    string text;
                    if (this.LogBuffer.TryDequeue(out text))
                    {
                        this.WriteFile(text);
                    }
                }
                this.IsProcessing = false;
            }
        }
        private void Write(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            this.LogBuffer.Enqueue(text);

            if (!this.IsProcessing)
            {
                this.BufferToFile();
            }
        }
        private void WriteLine(string text)
        {
            this.Write(string.Format("{0}\r\n", text));
        }
        private void WriteLogItem(string type, string text)
        {
            string item = string.Format("[{0}][{1}]:{2}", type, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text);
            this.WriteLine(item);
        }

        public void Log(string text)
        {
            this.WriteLogItem("L", text);
        }
        public void Info(string text)
        {
            this.WriteLogItem("I", text);
        }
        public void Wran(string text)
        {
            this.WriteLogItem("W", text);
        }
        public void Error(string text)
        {
            this.WriteLogItem("E", text);
        }
        public void Error(Exception ex)
        {
            var stackTrace = new StringBuilder();
            var message = "";
            while (ex != null)
            {
                stackTrace.Insert(0, string.Format("{0}", ex.StackTrace));
                if (ex.InnerException != null)
                    stackTrace.Insert(0, string.Format("\r\n   {0}\r\n", ex.Message));
                else
                    message = ex.Message;
                ex = ex.InnerException;
            }
            this.Error(string.Format("{0}\r\n{1}", message.Trim(), stackTrace.ToString()));
        }

        #region 有关写文件
        private string GenerateFile()
        {
            string path = string.Format("{0}\\{1}", this.LogFolder, DateTime.Now.ToString("yyyy-MM").Replace("-", "\\"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string file = string.Format("{0}\\{1}.log", path, DateTime.Now.ToString("yyyy-MM-dd"));
            return file;
        }
        private static object FileLocker = new Object();
        private void WriteFile(string text)
        {
            lock (FileLocker)
            {
                string file = this.GenerateFile();
                File.AppendAllText(file, text, Encoding.Unicode);
            }
        }
        #endregion
    }
}
