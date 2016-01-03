using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace LineCat.Web.Common
{
    public class Utils
    {
        //获取客户端ip
        public static string GetClientIP()
        {
            string ip = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
                    {
                        //得到真实的客户端地址
                        ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
                    }
                    else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
                    {
                        //得到服务端的地址要判断  System.Web.HttpContext.Current 为空的情况
                        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
                    }
                }
            }
            catch (Exception ep)
            {
                ip = "没有正常获取IP，" + ep.Message;
            }
            return ip;
        }

        //
        public static void SendEmail(string title, double p1, double p2)
        {
            SmtpSection cfg = ConfigurationManager.GetSection(@"system.net/mailSettings/smtp") as SmtpSection;
            SmtpNetworkElement smtElement = cfg.Network;
            string _FromEmail = "lc1055@126.com";
            string _ToEmail = "lc1055@163.com";
            //实例化一个邮件消息对象       
            MailMessage email;
            string displayName = ""; //email中的显示名  
            if (string.IsNullOrEmpty(displayName))
            {
                email = new MailMessage(_FromEmail, _ToEmail);
            }
            else
            {
                MailAddress fromMail = new MailAddress(_FromEmail, displayName, System.Text.Encoding.UTF8);
                MailAddress toMail = new MailAddress(_ToEmail);
                email = new MailMessage(fromMail, toMail);
            }
            email.IsBodyHtml = true;
            email.Body = "最新低价：" + p1 + "，上次低价：" + p2;
            //email.Subject = "linecat提醒：您关注的商品【" + title + "】达到了历史最低价格，降价幅度：" + Math.Round((p2 - p1) / p2 * 100) + "%";
            email.Subject = "《" + title + "》新低价：" + p1 + "，上次低价：" + p2 + "，降幅：" + Math.Round((p2 - p1) / p2 * 100) + "%";
            email.BodyEncoding = System.Text.Encoding.UTF8;
            //实例化smtp客服端对象，用来发送电子邮件      
            System.Net.Mail.SmtpClient stmp = new SmtpClient(smtElement.Host);
            //设置是否需要发送是否需要身份验证，如果不需要下面的credentials是不需要的     
            if (smtElement.DefaultCredentials)
            {
                stmp.UseDefaultCredentials = true;
                stmp.Credentials = new System.Net.NetworkCredential(smtElement.UserName, smtElement.Password);
            }
            //发送邮件      
            stmp.Send(email);
        }
    }
}