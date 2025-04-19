using Application.Enum;

namespace Application.Model
{
    public class QueuedMailModel
    {
        #region Properties
        public string To { get; set; }                                  // The recipient's email address
        public EmailType Type { get; set; }                             // The type of email to be sent
        public Dictionary<string, string> Placeholders { get; set; }    // A dictionary of placeholders and their values for email templates
        #endregion
    }
}