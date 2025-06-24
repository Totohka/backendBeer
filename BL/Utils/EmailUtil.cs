using AuthSSO.Common.Constants;
using AuthSSO.Common.Enums;
using AuthSSO.Common.Resourses.Emails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Localization;
using MimeKit;

namespace BL.Utils
{
    public static class EmailUtil
    {
        public static async Task<bool> SendEmailAsync(string email, string data, EnumEmailMessages typeEmail, IStringLocalizer<Email> localizer)
        {
            using var emailMessage = new MimeMessage();
            string type = typeEmail.ToString();
            var title = localizer[$"{EmailConstants.Title}{type}"];
            var message = localizer[$"{EmailConstants.Message}{type}"];
            emailMessage.From.Add(new MailboxAddress(localizer[Constants.NameApplication], "Encryptarium@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = title;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message + "<br/><br/><br/>" + data
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

                return false;
            }
        }
    }
}
