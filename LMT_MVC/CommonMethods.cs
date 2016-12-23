using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace LMT_MVC
{
    public class CommonMethods
    {
        public static int CurrentUserId
        {
            get
            {
                int userId = 0;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name.Split('|')[1]);
                }

                return userId;
            }
        }

        public static string CurrentUserName
        {
            get
            {
                string userName = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    userName = HttpContext.Current.User.Identity.Name.Split('|')[0];
                }

                return userName;
            }
        }
        public static string CurrentUserType
        {
            get
            {
                string userType = string.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    userType = HttpContext.Current.User.Identity.Name.Split('|')[2];
                }

                return userType;
            }
        }
        public static void SendMail(string Toemailaddress, string Fromemailaddress, string subject, string body, bool IsBodyHtml)
        {
            try
            {
                if (string.IsNullOrEmpty(Fromemailaddress))
                {
                    Fromemailaddress = ConfigurationManager.AppSettings["outgoingsmtpmailusername"].ToString();
                }
                if (string.IsNullOrEmpty(Toemailaddress))
                {
                    Toemailaddress = ConfigurationManager.AppSettings["outgoingsmtpmailusername"].ToString();
                }

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Fromemailaddress);
                mail.To.Add(Toemailaddress);
                mail.Subject = subject;
                mail.IsBodyHtml = IsBodyHtml;
                mail.Body = body;

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["outgoingsmtpmailserver"].ToString())
                {
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["outgoingsmtpmailusername"].ToString(), ConfigurationManager.AppSettings["outgoingsmtpmailpassword"].ToString())
                    //,
                    //EnableSsl = true
                };
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}