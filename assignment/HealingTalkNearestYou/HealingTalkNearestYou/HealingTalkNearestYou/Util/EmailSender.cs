using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Web;

namespace HealingTalkNearestYou.Util
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, string fileName = null, string fileContent = null)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("gaoyurong923@gmail.com", "HTNY");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (fileName != null && fileContent != null)
            {
                msg.AddAttachment(fileName, fileContent);
            }
            var response = client.SendEmailAsync(msg);
        }
    }
}