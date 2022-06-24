using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Hosting;
using System.Web;
using System.Net.Mime;
using System.IO;

namespace HPIMS_Console_API.Email
{
    public class AutomatedEmailService
    {
        DatabaseCode ML = new DatabaseCode();

        List<NotificationMappingModel> list_notification = new List<NotificationMappingModel>();
        List<ServerUpdateModel> list_updates = new List<ServerUpdateModel>();
        public void Command()
        {
            list_notification = ML.NotificationMappingList();
            list_updates = ML.ServerUpdateList();
            for (int i = 0; i < list_updates.Count; i++)
            {
                if (list_updates[i].IsServerUp == false)
                {
                    string x = Convert.ToString(list_notification[0].ToAddress);
                    string y = Convert.ToString(list_updates[i].ServerName);
                    string z = Convert.ToString(list_updates[i].ServerId);
                    string k = Convert.ToString(list_notification[0].ProjectId);
                    Mailer.SendMail(x, y, z, k);
                }
                ML.ServerUpdateDelete(list_updates[i].ID);
            }
            ML.ServerUpdatesReset();

        }
    }
    public static class Mailer
    {
        const string SMTPServer = "smtp3.hp.com";
        const string FromEmail = "roboticsupport@hp.com";

        public static void SendMail(string ToAddress, string serverName, string serverId, string projectId)
        {
            string bodyAlternateView; 
            string ExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string WorkPath = System.IO.Path.GetDirectoryName(ExeFilePath);
            string TemplateFilePath = System.IO.Path.Combine(WorkPath, "..\\..\\Email\\EmailHTMLTemplate.html");
            string HeaderImagePath = System.IO.Path.Combine(WorkPath, "..\\..\\Images\\Email\\EmailHeader.png");
            string FooterImagePath = System.IO.Path.Combine(WorkPath, "..\\..\\Images\\Email\\EmailFooter.png");
            
            using (StreamReader reader = new StreamReader(@TemplateFilePath))
            {
                bodyAlternateView = reader.ReadToEnd();
            }

            SmtpClient client = new SmtpClient(SMTPServer);
            client.EnableSsl = true;
            client.Timeout = 300000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            message.To.Add(ToAddress);
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            

            bodyAlternateView = bodyAlternateView.Replace("#serverName", serverName);
            bodyAlternateView = bodyAlternateView.Replace("#serverId", serverId);
            bodyAlternateView = bodyAlternateView.Replace("#projectId", projectId);
            message.Body = bodyAlternateView;

            AlternateView av = AlternateView.CreateAlternateViewFromString(bodyAlternateView, null, MediaTypeNames.Text.Html);
            LinkedResource mailImages = new LinkedResource(@HeaderImagePath);
            mailImages.ContentId = "mailHeader";
            av.LinkedResources.Add(mailImages);
            mailImages = new LinkedResource(@FooterImagePath);
            mailImages.ContentId = "mailFooter";
            av.LinkedResources.Add(mailImages);
            message.AlternateViews.Add(av);

            message.Subject = "Action Required | " + serverName + " Down";
            try
            {
                client.Send(message);
                message.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
