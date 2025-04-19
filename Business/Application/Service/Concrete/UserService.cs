using Application.Enum;
using Application.Service.Abstract;
using Domain.Wrapper;
using AutoMapper;
using Domain.Entity;
using Domain.Repository.Abstract;
using Domain.Parameter.Concrete;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Application.Dto;

namespace Application.Service.Concrete
{
    public class UserService : IUserService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IMailService _mailService;
        #endregion
        #region Constructors
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logService = logService;
            _mailService = mailService;
        }
        #endregion
        #region Methods
        public async Task<ModelResponse<UserDto>> InsertAsync(UserDto dto)
        {
            try
            {
                // Validate the model
                var control = await ValidateForUpdateAndInsert(dto, false, true);
                if (!control.IsSuccess)
                {
                    return control;
                }

                // Map the model to the entity
                var userEntity = _mapper.Map<User>(dto);

                // Insert the user
                var user = await _unitOfWork.Users.AddAsync(userEntity, CancellationToken.None);

                // Check if the user adding is successful
                if (!user.IsSuccess)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User insert is unsuccessful. Reason: {user.Error.Message}");
                    return new ModelResponse<UserDto>().Fail("User insert is unsuccessful");
                }

                // Log information
                await _logService.WriteLog(LogLevel.Information, "User added successfully");

                // Map the entity to the model
                var userModelResult = _mapper.Map<UserDto>(user.Data);

                try
                {
                    // Send email
                    await _mailService.SendEmailAsync(userModelResult.Email, EmailType.Welcome, new Dictionary<string, string>
                    {
                        { "FirstName", userModelResult.FirstName },
                        { "LastName", userModelResult.LastName }
                    });
                }
                catch (Exception innerException)
                {
                    // Log the error
                    await _logService.WriteLog(LogLevel.Warning, $"User inserted successfully but email could not be sent. Reason: {innerException.Message}");
                }

                // Return the result
                return new ModelResponse<UserDto>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<UserDto>().Fail("An error occured while adding user.");
            }
        }

        public async Task<ModelResponse<UserDto>> UpdateAsync(UserDto dto)
        {
            try
            {
                // Validate the model
                var validationResult = await ValidateForUpdateAndInsert(dto, true, false);
                if (!validationResult.IsSuccess)
                {
                    return validationResult;
                }

                // Check if the user exists
                var userExists = await _unitOfWork.Users.GetByIdAsync(dto.Id, CancellationToken.None);
                if (!userExists.IsSuccess || userExists.Data == null)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User with Id {dto.Id} could not be found.");
                    return new ModelResponse<UserDto>().Fail("User not found");
                }

                // Update the user
                var user = await _unitOfWork.Users.UpdateAsync(_mapper.Map<User>(dto), CancellationToken.None);
                if (!user.IsSuccess)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User update is unsuccessful. Reason: {user.Error.Message}");
                    return new ModelResponse<UserDto>().Fail("User update is unsuccessful");
                }

                // Log information
                await _logService.WriteLog(LogLevel.Information, "User updated successfully");

                try
                {
                    // Send email
                    await _mailService.SendEmailAsync(userExists.Data.Email, EmailType.AccountUpdate, new Dictionary<string, string>
                    {
                        { "FirstName", dto.FirstName },
                        { "LastName", dto.LastName }
                    });
                }
                catch (Exception innerException)
                {
                    // Log the error
                    await _logService.WriteLog(LogLevel.Warning, $"User updated successfully but email could not be sent. Reason: {innerException.Message}");
                }

                // Map the entity to the model
                var userModelResult = _mapper.Map<UserDto>(user.Data);

                // Return the result
                return new ModelResponse<UserDto>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<UserDto>().Fail("An error occured while updating user.");
            }
        }

        public async Task<ModelResponse<UserDto>> DeleteAsync(UserDto model)
        {
            try
            {
                // Check if the model is null
                if (model == null)
                {
                    await _logService.WriteLog(LogLevel.Error, "User delete failed: Model is null.");
                    return new ModelResponse<UserDto>().Fail("Id is required");
                }

                // Check if the id is valid
                if (model.Id <= 0)
                {
                    await _logService.WriteLog(LogLevel.Error, "User delete failed: Invalid Id.");
                    return new ModelResponse<UserDto>().Fail("Id is required");
                }

                // Check model validation for IsDeleted and IsActive
                if (model.IsDeleted || !model.IsActive)
                {
                    await _logService.WriteLog(LogLevel.Error, "User delete failed: User is not active.");
                    return new ModelResponse<UserDto>().Fail("User is not active");
                }

                // Check if the user exists
                var userExists = await _unitOfWork.Users.GetByIdAsync(model.Id, CancellationToken.None);
                if (!userExists.IsSuccess || userExists.Data == null)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User with Id {model.Id} could not be found.");
                    return new ModelResponse<UserDto>().Fail("User not found");
                }

                // Delete the user
                var user = await _unitOfWork.Users.DeleteAsync(model.Id, CancellationToken.None);

                // Check if the user delete is successful
                if (!user.IsSuccess)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User with Id {model.Id} could not be deleted.");
                    return new ModelResponse<UserDto>().Fail("User could not be deleted");
                }

                // Log information
                await _logService.WriteLog(LogLevel.Information, "User deleted successfully");

                // Email the user
                try
                {
                    await _mailService.SendEmailAsync(userExists.Data.Email, EmailType.AccountDelete, new Dictionary<string, string>
                    {
                        { "FirstName", userExists.Data.FirstName },
                        { "LastName", userExists.Data.LastName }
                    });
                }
                catch (Exception innerException)
                {
                    // Log the error
                    await _logService.WriteLog(LogLevel.Warning, $"User deleted successfully but email could not be sent. Reason: {innerException.Message}");
                }

                // Map the entity to the model
                var userModelResult = _mapper.Map<UserDto>(userExists.Data);

                // Return the result
                return new ModelResponse<UserDto>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<UserDto>().Fail("An error occured while deleting user.");
            }
        }

        public async Task<ModelResponse<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                // Check if the id is valid
                var validationResult = await ValidateForGetById(id);
                if (!validationResult.IsSuccess)
                {
                    return validationResult;
                }

                // Get the user
                var user = await _unitOfWork.Users.GetByIdAsync(id, CancellationToken.None);

                // Check if the user getting is successful
                if (!user.IsSuccess || user.Data == null)
                {
                    await _logService.WriteLog(LogLevel.Error, $"User with Id {id} could not be found.");
                    return new ModelResponse<UserDto>().Fail("User not found");
                }

                // Map the entity to the model
                var userModelResult = _mapper.Map<UserDto>(user.Data);

                // Return the result
                return new ModelResponse<UserDto>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<UserDto>().Fail("An error occured while getting user.");
            }
        }

        public async Task<ModelResponse<UserDto>> GetByEmailAsync(string email)
        {
            try
            {
                // Check if the email is valid
                if (string.IsNullOrWhiteSpace(email))
                {
                    await _logService.WriteLog(LogLevel.Error, "User get failed: Invalid Email.");
                    return new ModelResponse<UserDto>().Fail("Email is required");
                }

                // Validate email format
                if (!IsValidEmail(email))
                {
                    await _logService.WriteLog(LogLevel.Error, "User get failed: Email format is invalid.");
                    return new ModelResponse<UserDto>().Fail("Email is invalid");
                }

                // Get the user
                var user = await _unitOfWork.Users.GetByEmailAsync(email, CancellationToken.None);

                // Check if the user is empty
                if (user.Data is null)
                {
                    await _logService.WriteLog(LogLevel.Information, $"User with Email {email} could not be found.");
                    return new ModelResponse<UserDto>().Fail("User not found");
                }

                // Map the entity to the model
                var userModelResult = _mapper.Map<UserDto>(user.Data);

                // Return the result
                return new ModelResponse<UserDto>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<UserDto>().Fail("An error occured while getting user.");
            }
        }

        public async Task<ModelResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
            try
            {
                // Check if there are any users
                var users = await _unitOfWork.Users.GetAllAsync(CancellationToken.None);
                if (!users.IsSuccess)
                {
                    await _logService.WriteLog(LogLevel.Information, $"An error occured while getting users. Reason: {users.Error.Message}");
                    return new ModelResponse<IEnumerable<UserDto>>().Fail("An error occured while getting users.");
                }
                else if (users.Data == null || !users.Data.Any())
                {
                    await _logService.WriteLog(LogLevel.Information, "No users found");
                    return new ModelResponse<IEnumerable<UserDto>>().Success();
                }

                // Map the entities to the models
                var userModelResult = _mapper.Map<IEnumerable<UserDto>>(users.Data);

                // Return the result
                return new ModelResponse<IEnumerable<UserDto>>().Success(userModelResult);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new ModelResponse<IEnumerable<UserDto>>().Fail("An error occured while getting users.");
            }
        }

        public async Task<PagedModelResponse<IEnumerable<UserDto>>> GetAllByParametersAsync(ListRequestParameter parameter)
        {
            try
            {
                // Define
                Expression<Func<User, bool>>? expression = null;

                // Check if the parameter is null
                if (!string.IsNullOrWhiteSpace(parameter.Search))
                {
                    expression = x => x.FirstName.Contains(parameter.Search) || x.LastName.Contains(parameter.Search) || x.Email.Contains(parameter.Search);
                }

                // Order by
                Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null;

                // Check if the parameter is null
                if (!string.IsNullOrWhiteSpace(parameter.SortBy))
                {
                    // Define
                    bool ascending = parameter.SortOrder?.ToLower() == "asc";

                    // Order by
                    orderBy = parameter.SortBy switch
                    {
                        "FirstName" => x => ascending ? x.OrderBy(y => y.FirstName) : x.OrderByDescending(y => y.FirstName),
                        "LastName" => x => ascending ? x.OrderBy(y => y.LastName) : x.OrderByDescending(y => y.LastName),
                        "Email" => x => ascending ? x.OrderBy(y => y.Email) : x.OrderByDescending(y => y.Email),
                        _ => x => x.OrderBy(y => y.Id)
                    };
                }

                // Date filter
                Func<IQueryable<User>, IQueryable<User>>? filter = null;

                // Get the users
                var users = await _unitOfWork.Users.GetAllByParametersAsync(
                    expression,
                    orderBy,
                    filter,
                    parameter.PageNumber,
                    parameter.PageSize,
                    false,
                    true,
                    false,
                    CancellationToken.None
                );

                // Check if the users getting is successful
                if (!users.IsSuccess)
                {
                    await _logService.WriteLog(LogLevel.Information, $"An error occured while getting users. Reason: {users.Error.Message}");
                    return new PagedModelResponse<IEnumerable<UserDto>>().Fail("An error occured while getting users.");
                }
                else if (users.Data == null || !users.Data.Any())
                {
                    await _logService.WriteLog(LogLevel.Information, "No users found.");
                    return new PagedModelResponse<IEnumerable<UserDto>>().Fail("No users found.");
                }

                // Map the entities to the models
                var userModelResult = _mapper.Map<IEnumerable<UserDto>>(users.Data);

                // Return the result
                return new PagedModelResponse<IEnumerable<UserDto>>().Success(userModelResult, users.PageNumber, users.PageSize, users.TotalPages, users.TotalItems);
            }
            catch (Exception exception)
            {
                // Log the error
                await _logService.WriteLog(LogLevel.Error, exception.Message);

                // Return the result
                return new PagedModelResponse<IEnumerable<UserDto>>().Fail("An error occured while getting users.");
            }
        }

        /// <summary>
        /// Validate the model for update and insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isUpdate"></param>
        /// <param name="isInsert"></param>
        /// <returns></returns>
        private async Task<ModelResponse<UserDto>> ValidateForUpdateAndInsert(UserDto model, bool isUpdate, bool isInsert)
        {
            if (model == null)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Model is null.");
                return new ModelResponse<UserDto>().Fail("User model is required");
            }

            if (isUpdate && model.Id <= 0)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Id is required for update.");
                return new ModelResponse<UserDto>().Fail("Id is required");
            }

            if (isInsert && model.Id > 0)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Id should not be provided for insert.");
                return new ModelResponse<UserDto>().Fail("Id is not required");
            }

            if (string.IsNullOrEmpty(model.Username))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Username is missing.");
                return new ModelResponse<UserDto>().Fail("Username is required");
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: First name is missing.");
                return new ModelResponse<UserDto>().Fail("First name is required");
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Last name is missing.");
                return new ModelResponse<UserDto>().Fail("Last name is required");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Email is missing.");
                return new ModelResponse<UserDto>().Fail("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(model.Email))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Email format is invalid.");
                return new ModelResponse<UserDto>().Fail("Email is invalid");
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Password is missing.");
                return new ModelResponse<UserDto>().Fail("Password is required");
            }

            // Validate dates
            if (model.InsertDate == DateTime.MinValue)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Insert date is invalid.");
                return new ModelResponse<UserDto>().Fail("Insert date is required");
            }

            if (isUpdate && model.UpdateDate == DateTime.MinValue)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Update date is invalid.");
                return new ModelResponse<UserDto>().Fail("Update date is required");
            }

            // Check if the user is active
            if (model.IsDeleted || !model.IsActive)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: User is not active.");
                return new ModelResponse<UserDto>().Fail("User is not active");
            }

            return new ModelResponse<UserDto>().Success(model);
        }

        /// <summary>
        /// Validate the model for GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<ModelResponse<UserDto>> ValidateForGetById(int id)
        {
            if (id < 0)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Id is required for update.");
                return new ModelResponse<UserDto>().Fail("Id is invalid");
            }

            if (id == 0)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Id is required for update.");
                return new ModelResponse<UserDto>().Fail("Id is required");
            }

            if (id == 0)
            {
                await _logService.WriteLog(LogLevel.Error, "User validation failed: Id is required for update.");
                return new ModelResponse<UserDto>().Fail("User not found");
            }

            return new ModelResponse<UserDto>().Success();
        }

        /// <summary>
        /// Validate the email format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Use regex pattern for basic email validation
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}