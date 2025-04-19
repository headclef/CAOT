namespace Application.Model
{
    public class SmtpModel
    {
        #region Properties
        public string Host { get; set; } = string.Empty;        // SMTP server address
        public int Port { get; set; }                           // SMTP server port
        public bool EnableSsl { get; set; }                     // Indicates if SSL is enabled
        public string UserName { get; set; } = string.Empty;    // SMTP username
        public string Password { get; set; } = string.Empty;    // SMTP password
        public string FromName { get; set; } = string.Empty;    // Sender name
        public string FromEmail { get; set; } = string.Empty;   // Sender email address
        #endregion
    }
}