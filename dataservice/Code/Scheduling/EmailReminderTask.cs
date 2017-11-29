using System.Threading.Tasks;
using System.Threading;
using dataservice.Code.Scheduling;
using System;
using System.Net.Mail;
using System.Net;
using dataservice.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// Source: https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
namespace dataservice.Code
{
    public class EmailReminderTask : IScheduledTask
    {
        public string Schedule => "*/1 * * * *";

        public async Task Invoke(_17SP2G4Context context, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            List<Followingtraining> followingtrainings = await context.Followingtraining
                .Include(f => f.TrainingSession).Include(f => f.User).Include(f => f.TrainingSession.Training).Include(f => f.TrainingSession.Address)
                .Where(f => !String.IsNullOrWhiteSpace(f.User.Email) && f.IsApproved && !f.IsCancelled && !f.TrainingSession.Cancelled && (f.TrainingSession.Date - DateTime.Now).TotalHours <=24 && (f.TrainingSession.Date - DateTime.Now).TotalHours >= 0)
                .ToListAsync();
            
            foreach (Followingtraining ft in followingtrainings)
            {
                SendMail(ft.User, ft.TrainingSession);
            }
        }

        private void SendMail(User user, Trainingsession trainingsession)
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
                mailMessage.To.Add(user.Email);
                mailMessage.Body = "Beste " + user.Username 
                    + "\nOp " + trainingsession.Date.Date.ToShortDateString() + " heeft u een training, " + trainingsession.Training.Name + "."
                    + " Deze training zal doorgaan in: " + trainingsession.Address.Locality;
                mailMessage.Subject = "Training reminder";
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
            }
        }
    }
}
