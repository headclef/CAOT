using Domain.Wrapper;
using Domain.Parameter.Concrete;
using Application.Dto;

namespace Application.Service.Abstract
{
    public interface IUserService
    {
        #region Signatures
        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ModelResponse<UserDto>> InsertAsync(UserDto model);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ModelResponse<UserDto>> UpdateAsync(UserDto model);

        /// <summary>
        /// Delete an existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ModelResponse<UserDto>> DeleteAsync(UserDto model);

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ModelResponse<UserDto>> GetByIdAsync(int id);

        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<ModelResponse<UserDto>> GetByEmailAsync(string email);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        Task<ModelResponse<IEnumerable<UserDto>>> GetAllAsync();

        /// <summary>
        /// Get all users by parameters
        /// </summary>
        /// <returns></returns>
        Task<PagedModelResponse<IEnumerable<UserDto>>> GetAllByParametersAsync(ListRequestParameter parameter);
        #endregion
    }
}