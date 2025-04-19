using Domain.Entity;
using Domain.Repository.Abstract.Common;
using Domain.Wrapper;

namespace Domain.Repository.Abstract
{
    // IUserRepository inherits from IBaseRepository<User>
    public interface IUserRepository : IBaseRepository<User>
    {
        #region Signatures
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ModelResponse<User>> GetByEmailAsync(string email, CancellationToken cancellationToken);
        #endregion
    }
}