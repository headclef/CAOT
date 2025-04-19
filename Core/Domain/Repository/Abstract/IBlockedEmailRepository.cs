using Domain.Entity;
using Domain.Repository.Abstract.Common;
using Domain.Wrapper;

namespace Domain.Repository.Abstract
{
    // IBlockedEmailRepository inherits from IBaseRepository<BlockedEmail>
    public interface IBlockedEmailRepository : IBaseRepository<BlockedEmail>
    {
        /// <summary>
        /// This method is used to get a blocked email by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ModelResponse<BlockedEmail>> GetByEmailAsync(string email);
    }
}