using Domain.Repository.Abstract;
using Domain.Wrapper;
using Application.Service.Concrete;
using Moq;
using UnitTest.Helper;
using Xunit;
using Application.Dto;
using Domain.Entity;
using UnitTest.Unit.UserTests.Service.Common;

namespace UnitTest.Unit.UserTests.Service
{
    public class GetByEmailTests : UserTestBase
    {
        #region Properties
        private readonly UserService _userService;                      // The service being tested
        private readonly Mock<IUserRepository> _mockUserRepository;     // Mocked repository for user data access
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for GetByEmailTests
        /// </summary>
        public GetByEmailTests()
        {
            // Define mock repository
            _mockUserRepository = new Mock<IUserRepository>();

            // Setup UnitOfWork mock to return the UserRepository mock
            _unitOfWork.Setup(uow => uow.Users)
                .Returns(_mockUserRepository.Object);

            // Define mock mapper
            _mapper.Setup(mapper => mapper.Map<User>(It.IsAny<UserDto>()))
                .Returns<UserDto>(model => new User
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    InsertDate = model.InsertDate,
                    UpdateDate = model.UpdateDate,
                    DeleteDate = model.DeleteDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Id = model.Id > 0 ? model.Id : 0 // Id yoksa 0 veriyoruz
                });

            // Map User entity back to UserModel
            _mapper.Setup(mapper => mapper.Map<UserDto>(It.IsAny<User>()))
                .Returns<User>(entity => new UserDto
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    Password = entity.Password,
                    InsertDate = entity.InsertDate,
                    UpdateDate = entity.UpdateDate,
                    DeleteDate = entity.DeleteDate,
                    IsActive = entity.IsActive,
                    IsDeleted = entity.IsDeleted
                });

            // Setup AddAsync mock
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User user, CancellationToken token) =>
                {
                    // Check if user has Id - for test InsertAsync_ShouldReturnFail_WhenUserHasId
                    if (user.Id > 0)
                    {
                        return new ModelResponse<User>().Fail(ErrorMessages.User.IdIsNotRequired);
                    }

                    // Set Id for newly created user
                    user.Id = 1;
                    return new ModelResponse<User>().Success(user);
                });

            // Define service
            _userService = CreateUserService();
        }
        #endregion
        #region Methods
        [Fact]
        public async Task GetByEmailAsync_ShouldReturnFail_WhenEmailIsMissing()
        {
            // Arrange
            var email = string.Empty;

            // Act
            var result = await _userService.GetByEmailAsync(email);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.EmailIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnFail_WhenEmailIsInvalid()
        {
            // Arrange
            var email = "invalidemail";

            // Act
            var result = await _userService.GetByEmailAsync(email);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.EmailIsInvalid, result.Error.Message);
        }
        #endregion
    }
}