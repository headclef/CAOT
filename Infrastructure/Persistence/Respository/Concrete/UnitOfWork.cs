using Domain.Repository.Abstract;
using Persistence.Context;

namespace Persistence.Respository.Concrete
{
    public sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        #region Properties
        private readonly BaseDbContext _dbContext;
        private IUserRepository? UserRepository;
        private IBlockedEmailRepository? BlockedEmailRepository;
        public IUserRepository Users => UserRepository ??= new UserRepository(_dbContext);
        public IBlockedEmailRepository BlockedEmails => BlockedEmailRepository ??= new BlockedEmailRepository(_dbContext);
        #endregion
        #region Constructors
        public UnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion
        #region Methods
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Dispose the DbContext
        /// </summary>
        public void Dispose() => _dbContext.Dispose();

        /// <summary>
        /// Dispose the DbContext asynchronously
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
        #endregion
    }
}