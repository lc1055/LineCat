﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LineCat.Web.Service;

namespace LineCat.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //定时任务
            int min = 1000 * 60 * 1;
            System.Timers.Timer myTimer = new System.Timers.Timer(min);
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //myTimer.Interval = 60000;
            myTimer.Enabled = true;
        }

        protected void Application_Error()
        {
            string currentUrl = HttpContext.Current.Request.Url.ToString();

            HttpServerUtility server = HttpContext.Current.Server;
            Exception lastError = server.GetLastError();
            if (lastError != null)
            {
                TxtLog.SaveErrorLog("应用程序捕获异常，出错链接：[[[ " + currentUrl + " ]]]", lastError.ToString(), "应用程序异常处理程序");

                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "base");
                routeData.Values.Add("action", "exception");
                routeData.Values.Add("error", lastError);

                Server.ClearError();
                IController errorController = new Controllers.BaseController();
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            }
        }

        void Session_End(object sender, EventArgs e)
        {
            //下面的代码是关键，可解决IIS应用程序池自动回收的问题
            System.Threading.Thread.Sleep(1000);

            //触发事件, 写入提示信息
            TxtLog.WriteLine("iis休眠");

            //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
            //使用您自己的URL
            string url = "http://localhost:1358";
            System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流

            // 在会话结束时运行的代码。
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为 InProc 时，才会引发 Session_End 事件。
            // 如果会话模式设置为 StateServer 或 SQLServer，则不会引发该事件。
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            TxtLog.WriteLine("执行定时事件");
            Common.GetPrice();
        }



    }
}
