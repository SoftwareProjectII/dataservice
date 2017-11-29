using System.Threading.Tasks;
using System.Threading;
using dataservice.Code.Scheduling;
using System;
using System.Net.Mail;
using System.Net;

// Source: https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
namespace dataservice.Code
{
    public class EmailReminderTask : IScheduledTask
    {
        public string Schedule => "*/1 * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com")
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("vandermerwe.joshua@hotmail.com", "Ikbendom14@pri"),
                    Port = 587,
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("vandermerwe.joshua@hotmail.com");
                mailMessage.To.Add("joskevdm@hotmail.com");
                mailMessage.Body = "Testing: " + DateTime.Now.ToString();
                mailMessage.Subject = ".Net core email test";
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
            }
        }
    }
}
