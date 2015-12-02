using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace LineCat.Web.Service
{
    public class General
    {
        public static void SendEmail()
        {
            SmtpSection cfg = ConfigurationManager.GetSection(@"system.net/mailSettings/smtp") as SmtpSection;
            SmtpNetworkElement smtElement = cfg.Network;
            string _FromEmail = "lc1055@163.com";
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
                //email.Sender = new MailAddress(_FromEmail, displayName, System.Text.Encoding.UTF8);  
            }
            email.IsBodyHtml = true;
            email.Body = "linecat提醒：您关注的商品达到了历史最低价格，请立即剁手！";
            email.Subject = "linecat提醒：您关注的商品达到了历史最低价格，请立即剁手！";
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