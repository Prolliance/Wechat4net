using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using Wechat4net.QY.Define;

namespace Wechat4net.QY.Utils
{
    /// <summary>
    /// CSV文件处理类
    /// </summary>
    public static class CsvHelper
    {

        private static readonly string deptCsvHeader = "部门名称,部门ID,父部门ID,排序";
        private static string GetDeptCsvHeader()
        {
            return deptCsvHeader;
        }

        private static readonly string userCsvHeader = "姓名,帐号,微信号,手机号,邮箱,所在部门,职位";
        private static string GetUserCsvHeader(List<WechatUser> userList)
        {
            //if (userList == null || userList.Count < 1
            //    || userList[0].extattr == null || userList[0].extattr.attrs == null
            //    || userList[0].extattr.attrs.Count < 1)
            //{
            //    return _userCsvHeader;
            //}
            string header = userCsvHeader;
            try
            {
                foreach (var item in userList[0].ExtAttr.Attrs)
                {
                    header += "," + item.Name;
                }
            }
            catch
            {
            }
            return header;
        }

        private static class FilePath
        {
            public static string Dept
            {
                get
                {
                    return AppSettings.TempPath + "\\dept" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                }
            }
            public static string User
            {
                get
                {
                    return AppSettings.TempPath + "\\user" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                }
            }
        }

        public static string CrateDeptCsv(List<WechatDept> deptList)
        {
            //try
            //{
                StringBuilder strBufferLine = null;
                string path = CsvHelper.FilePath.Dept;
                StreamWriter strmWriterObj = new StreamWriter(path, false, System.Text.Encoding.UTF8);
                //strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(CsvHelper.GetDeptCsvHeader());
                foreach (var dept in deptList)
                {
                    strBufferLine = new StringBuilder();
                    strBufferLine.Append(dept.Name + ",");
                    strBufferLine.Append(dept.ID + ",");
                    strBufferLine.Append(dept.ParentID + ",");
                    strBufferLine.Append(dept.Order);
                    strmWriterObj.WriteLine(strBufferLine.ToString());
                }
                strmWriterObj.Close();
                return path;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public static string CrateUserCsv(List<WechatUser> userList)
        {
            //try
            //{
                StringBuilder strBufferLine = null;
                string path = CsvHelper.FilePath.User;
                StreamWriter strmWriterObj = new StreamWriter(path, false, System.Text.Encoding.UTF8);
                //strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(CsvHelper.GetUserCsvHeader(userList));
                foreach (var user in userList)
                {
                    strBufferLine = new StringBuilder();
                    strBufferLine.Append(user.Name + ",");
                    strBufferLine.Append(user.UserID + ",");
                    strBufferLine.Append(user.WeixinID + ",");
                    strBufferLine.Append(user.Mobile + ",");
                    strBufferLine.Append(user.Email + ",");

                    string dept = string.Empty;
                    foreach (int d in user.Department)
                    {
                        dept += d.ToString();
                        dept += ";";
                    }
                    strBufferLine.Append(dept.TrimEnd(';') + ",");

                    strBufferLine.Append(user.Position);
                    try
                    {
                        foreach (var item in user.ExtAttr.Attrs)
                        {
                            strBufferLine.Append("," + item.Value);
                        }
                    }
                    catch
                    {
                    }

                    strmWriterObj.WriteLine(strBufferLine.ToString());
                }
                strmWriterObj.Close();
                return path;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /*
        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static bool dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable csv2dt(string filePath, int n, DataTable dt)
        {
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr = split;
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
         */
    }
}
