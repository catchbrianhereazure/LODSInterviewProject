using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace LODSInterviewProject.Services
{
    public class EmailServiceProvider
    {
        public static async Task Execute(String toEmail, String toName)
        {
            var apiKey = "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"; // Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("catchazurehere@gmail.com", "B Buikema");
            var subject = "New Notification";
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = "You have been added to an organization.";
            var htmlContent = "<strong>You have been added to an organization.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
