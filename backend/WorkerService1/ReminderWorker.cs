using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NovaLite.Todo.Shared.Data;
using System.Net.Mail;
using System.Net;

namespace NovaLite.Todo.Reminder
{
    public class ReminderWorker : BackgroundService
    {
        private readonly ILogger<ReminderWorker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public ReminderWorker(ILogger<ReminderWorker> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await PullReminders(dbContext);
                }
                var pullMinutes = _configuration.GetValue<int>("ReminderService:PullIntervalInMinutes");
                var pullSeconds = TimeSpan.FromMinutes(pullMinutes);
                await Task.Delay(pullSeconds, stoppingToken);
            }
        }

        private async Task PullReminders(ApplicationDbContext context)
        {
            var reminders = context.TodoReminders
                .Where(r => !r.Sent && r.Timestamp.AddDays(1) > DateTimeOffset.UtcNow)
                .ToList();

            foreach (var reminder in reminders)
            {
                
                
                if (reminder.Timestamp.AddDays(1) >= DateTimeOffset.UtcNow)
                {
                    SendEmail("aleksaignjatovic15@gmail.com", "Reminder", "Reminder to do your tasks :)");
                    reminder.Sent = true;
                }

            }

            await context.SaveChangesAsync();
        }

        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                string senderEmail = _configuration["SmtpSettings:Email"];
                string senderPassword = _configuration["SmtpSettings:Password"];

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(to);

                smtpClient.Send(mailMessage);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}