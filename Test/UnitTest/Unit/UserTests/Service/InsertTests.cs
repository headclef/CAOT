using Domain.Repository.Abstract;
using Domain.Wrapper;
using Application.Service.Concrete;
using Moq;
using UnitTest.Helper;
using UnitTest.Mock;
using Xunit;
using Domain.Entity;
using Application.Dto;
using UnitTest.Unit.UserTests.Service.Common;

namespace UnitTest.Unit.UserTests.Service
{
    public class InsertTests : UserTestBase
    {
        #region Properties
        private readonly UserService _userService;                      // The service being tested
        private readonly Mock<IUserRepository> _mockUserRepository;     // Mocked repository for user data access
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for the InsertTests class.
        /// </summary>
        public InsertTests()
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
                    // Simulate newly created user
                    user.Id = 1;
                    return new ModelResponse<User>().Success(user);
                });

            // Define service
            _userService = CreateUserService();
        }
        #endregion
        #region Methods
        [Theory]
        [MemberData(nameof(FakeUserData.InvalidInsertUsers), MemberType = typeof(FakeUserData))]
        public async Task InsertAsync_ShouldReturnFail_WhenUserIsInvalid(UserDto model)
        {
            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenUsernameIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingUsernameInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserUsernameIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenFirstNameIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingFirstNameInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserFirstNameIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenLastNameIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingLastNameInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserLastNameIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenEmailIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingEmailInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.EmailIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenEmailIsInvalid()
        {
            // Arrange
            var model = FakeUserData.InvalidEmailInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.EmailIsInvalid, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenPasswordIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingPasswordInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.PasswordIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenInsertDateIsInvalid()
        {
            // Arrange
            var model = FakeUserData.InvalidInsertDateInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserInsertDateIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenUserAlreadyDeleted()
        {
            // Arrange
            var model = FakeUserData.InvalidActivityInsert();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserIsNotActive, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnFail_WhenUserHasId()
        {
            // Arrange
            var model = FakeUserData.ValidUserModel();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.IdIsNotRequired, result.Error.Message);
        }

        [Fact]
        public async Task InsertAsync_ShouldReturnSuccess_WhenUserIsValid()
        {
            // Arrange
            var model = FakeUserData.ValidUserInsertModel();

            // Act
            var result = await _userService.InsertAsync(model);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }
        #endregion
    }
}