namespace Domain.Repository.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        #region Properties
        IUserRepository Users { get; }                  // User repository
        IBlockedEmailRepository BlockedEmails { get; }  // Blocked email repository
        #endregion
        #region Signatures
        /// <summary>
        /// Save the changes to the database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}