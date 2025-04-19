namespace Application.Service.Abstract
{
    public interface IBlockedEmailService
    {
        #region Signatures
        /// <summary>
        /// Checks if the email is blocked
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsBlockedEmailConfirmedAsync(string email);

        /// <summary>
        /// Blocks the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task BlockEmailAsync(string email);
        #endregion
    }
}