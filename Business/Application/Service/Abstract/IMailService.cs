using Application.Enum;

namespace Application.Service.Abstract
{
    public interface IMailService
    {
        #region Signatures
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="emailType"></param>
        /// <param name="placeholders"></param>
        /// <returns></returns>
        Task SendEmailAsync(string toEmail, EmailType emailType, Dictionary<string, string> placeholders);
        #endregion
    }
}