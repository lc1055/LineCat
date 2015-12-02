using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LineCat.Web
{
    public class TxtLog
    {
        /// <summary>
        /// 写一行日志
        /// </summary>
        /// <param name="content"></param>
        public static void WriteLine(string content)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = date.Year + "-" + date.Month + "-" + date.Day;
            try
            {
                FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/") + FileName + ".log";

                #region 检测日志目录是否存在
                if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                sw.WriteLine("[" + date + "]" + content);
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="user"></param>
        public static void SaveLog(string title, string content, string user)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = date.Year + "-" + date.Month + "-" + date.Day;
            try
            {
                FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/") + FileName + ".log";

                #region 检测日志目录是否存在
                if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                sw.WriteLine("时间:  " + System.DateTime.Now);
                sw.WriteLine("地址:  " + Utils.GetClientIP() + "\r");
                sw.WriteLine("用户:  " + user + "\r");
                sw.WriteLine("操作:  " + title + "\r");
                sw.WriteLine("详细:  " + content);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="errorContent"></param>
        /// <param name="userName"></param>
        public static void SaveErrorLog(string title, string errorContent, string userName)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = "error";
            try
            {
                FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/Error/" + FileName + "_" + date.ToString("yyyyMMdd") + ".log");

                #region 检测日志目录是否存在
                if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/Error")))
                {
                    Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/Error"));
                }

                if (!File.Exists(FileName))
                {
                    sw = File.CreateText(FileName);
                }
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion
                sw.WriteLine("↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓  [时间:" + System.DateTime.Now + "]  ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓\r");
                sw.WriteLine("[IP      : ]" + Utils.GetClientIP() + "\r");
                sw.WriteLine("[操作者  ：]" + userName);
                sw.WriteLine("[错误名  ：]" + title);
                sw.WriteLine("[详细信息：]" + errorContent);
                sw.WriteLine("\r\n");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        public static void SaveErrorLog(string errorContent)
        {
            SaveErrorLog("", errorContent, "");
        }
    }
}