using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace LODSInterviewProject.Services
{
    public class EmailServiceProvider : IEmailServiceProvider
    {
        protected String _apiKey;
        protected String _subject;
        protected String _from;
        protected String _fromName;
        protected String _plainText;
        protected String _htmlContent;

        public EmailServiceProvider(String apiKey, String subject, String from, String fromName, String plainText, String htmlContent)
        {
            _apiKey = apiKey;
            _subject = subject;
            _from = from;
            _fromName = fromName;
            _plainText = plainText;
            _htmlContent = htmlContent;
        }

        public async Task Execute(String toEmail, String toName)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_from, _fromName);
            var subject = _subject;
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = _plainText;
            var htmlContent = _htmlContent;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
