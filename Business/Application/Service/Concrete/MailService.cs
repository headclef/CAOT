using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Application.Service.Abstract;
using Application.Enum;
using Application.Model;

namespace Application.Service.Concrete
{
    public class MailService : IMailService
    {
        #region Properties
        private readonly SmtpModel _smtpModel;                                                          // The SMTP model
        private readonly EmailSettingsModel _emailSettings;                                             // The email settings model
        private readonly ILogService _logService;                                                       // The log service
        private readonly string _templatePath = Path.Combine(AppContext.BaseDirectory, "Templates");    // The path to the email templates
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for the MailService that initializes the SMTP model, email settings, and log service.
        /// </summary>
        /// <param name="smtpModel"></param>
        /// <param name="emailSettings"></param>
        /// <param name="logService"></param>
        public MailService(IOptions<SmtpModel> smtpModel, IOptions<EmailSettingsModel> emailSettings, ILogService logService)
        {
            _smtpModel = smtpModel.Value;
            _emailSettings = emailSettings.Value;
            _logService = logService;
        }
        #endregion
        #region Methods
        public async Task SendEmailAsync(string toEmail, EmailType emailType, Dictionary<string, string> placeholders)
        {
            try
            {
                // Get parameters
                string subject = GetSubject(emailType);
                string body = GetHtmlBody(emailType, placeholders);
                var message = new MimeMessage();
                var builder = new BodyBuilder { HtmlBody = body };

                // Set from
                message.From.Add(new MailboxAddress(_smtpModel.FromName, _smtpModel.FromEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                message.Body = builder.ToMessageBody();

                // Check if email settings are enabled
                if (!_emailSettings.SendEmails || string.IsNullOrWhiteSpace(toEmail))
                {
                    await _logService.WriteLog(LogLevel.Information, "Email settings are disabled or email is not correct");
                    return;
                }

                // Send email
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpModel.Host, _smtpModel.Port, _smtpModel.EnableSsl);
                await client.AuthenticateAsync(_smtpModel.UserName, _smtpModel.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception exception)
            {
                await _logService.WriteLog(LogLevel.Error, exception.Message);
                return;
            }
        }

        /// <summary>
        /// Gets mail body from template
        /// </summary>
        /// <param name="emailType"></param>
        /// <param name="placeholders"></param>
        /// <returns></returns>
        private string GetHtmlBody(EmailType emailType, Dictionary<string, string> placeholders)
        {
            // Get html from template
            string templatePath = Path.Combine(_templatePath, $"{emailType}.html");

            // Check if template exists
            if (!File.Exists(templatePath))
            {
                _logService.WriteLog(LogLevel.Error, "Template not found");
                throw new FileNotFoundException($"Email template not found: {templatePath}");
            }

            // Read html from template
            string html = File.ReadAllText(templatePath);

            // Replace placeholders
            foreach (var placeholder in placeholders)
            {
                html = html.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            // Return html
            return html;
        }

        /// <summary>
        /// Gets mail subject based on email type
        /// </summary>
        /// <param name="emailType"></param>
        /// <returns></returns>
        private string GetSubject(EmailType emailType) => emailType switch
        {
            EmailType.Welcome => "Welcome!",
            EmailType.PasswordReset => "Password Reset",
            EmailType.AccountActivation => "Account Activation",
            EmailType.Information => "Information",
            EmailType.Notification => "Notification",
            _ => string.Empty
        };
        #endregion
    }
}