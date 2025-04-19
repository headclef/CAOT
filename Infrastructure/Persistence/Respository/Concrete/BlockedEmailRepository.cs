using Domain.Repository.Abstract;
using Domain.Wrapper;
using Domain.Entity;
using Persistence.Context;
using Persistence.Respository.Concrete.Common;

namespace Persistence.Respository.Concrete
{
    public class BlockedEmailRepository : BaseRepository<BlockedEmail>, IBlockedEmailRepository
    {
        #region Constructors
        /// <summary>
        /// Constructor for BlockedEmailRepository that takes a DbContext as a parameter.
        /// </summary>
        /// <param name="dbContext"></param>
        public BlockedEmailRepository(BaseDbContext dbContext) : base(dbContext) { }
        #endregion
        #region Methods
        public async Task<ModelResponse<BlockedEmail>> GetByEmailAsync(string email)
        {
            // Get the blocked email by email
            var emails = await GetAllAsync();

            // If the email is not found, return a fail response
            if (!emails.IsSuccess)
                return new ModelResponse<BlockedEmail>().Fail("Belirtilen e-posta adresi bulunamadı.");

            // Return the blocked email
            var blockedEmail = emails.Data.FirstOrDefault(x => x.Email == email);

            // If the blocked email is not found, return a fail response
            if (blockedEmail == null)
                return new ModelResponse<BlockedEmail>().Fail("Belirtilen e-posta adresi bulunamadı.");

            // Return the blocked email
            return new ModelResponse<BlockedEmail>().Success(blockedEmail);
        }
        #endregion
    }
}