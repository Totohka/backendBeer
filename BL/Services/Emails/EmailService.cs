using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BL.Services.Emails
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправка писем
        /// </summary>
        /// <param name="email">Email куда нужно отправить сообщение</param>
        /// <param name="subject">Заголовок письма</param>
        /// <param name="message">Письмо</param>
        /// <returns></returns>
        public Task<bool> SendEmailAsync(string email, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation("Вызван метод EmailService.SendEmailAsync()");

            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Encryptarium", "Encryptarium@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                string error = null;
                try
                {
                    await client.ConnectAsync("smtp.yandex.ru", 465, true);
                    await client.AuthenticateAsync("Encryptarium@yandex.ru", "cnviitklvhqwxgob");
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    error = "Произошла ошибка в работе с почтой";
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }

                if (error is null)
                {
                    return true;
                }
                _logger.LogError("Метод EmailService.SendEmailAsync(). Произошла ошибка в работе с почтой");
                return false;
            }
        }
    }
}
