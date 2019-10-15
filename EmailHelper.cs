
using MailKit.Net.Smtp;
using MimeKit;

namespace RatesMarkettingMailAutomation
{
    public static class EmailHelper
    {
        public static void SendEmail(EmailModel message) {
            using (SmtpClient client = new SmtpClient()) {
                client.Send(message);
            }
        }
    }
}
