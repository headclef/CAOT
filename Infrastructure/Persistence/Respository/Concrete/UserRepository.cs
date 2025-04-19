using Domain.Repository.Abstract;
using Domain.Wrapper;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Respository.Concrete.Common;

namespace Persistence.Respository.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Properties
        private readonly BaseDbContext _dbContext;  // DbContext instance
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for UserRepository that takes a DbContext as a parameter.
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(BaseDbContext dbContext) : base(dbContext) { _dbContext = dbContext; }
        #endregion
        #region Methods
        public async Task<ModelResponse<User>> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            // Get user by email
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            // Check if user is null
            if (user is null)
                return new ModelResponse<User>().Success();

            // Return success response
            return new ModelResponse<User>().Success(new User
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
                InsertDate = user.InsertDate,
                UpdateDate = user.UpdateDate,
                DeleteDate = user.DeleteDate
            });
        }
        #endregion
    }
}