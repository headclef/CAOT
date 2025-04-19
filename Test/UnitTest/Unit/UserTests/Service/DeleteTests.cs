using Domain.Repository.Abstract;
using Domain.Wrapper;
using Application.Service.Concrete;
using Moq;
using UnitTest.Helper;
using UnitTest.Mock;
using Domain.Entity;
using Application.Dto;
using UnitTest.Unit.UserTests.Service.Common;

namespace UnitTest.Unit.UserTests.Service
{
    public class DeleteTests : UserTestBase
    {
        #region Properties
        private readonly UserService _userService;                      // The service being tested
        private readonly Mock<IUserRepository> _mockUserRepository;     // Mocked repository for user data access
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for DeleteTests
        /// </summary>
        public DeleteTests()
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

            // Setup GetByIdAsync mock
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken token) =>
                {
                    // Simulate a user not found scenario
                    if (id < 0)
                        return new ModelResponse<User>().Fail(ErrorMessages.User.IdIsInvalid);
                    if (id == 0)
                        return new ModelResponse<User>().Fail(ErrorMessages.User.IdIsRequired);
                    if (id == 99)
                        return new ModelResponse<User>().Fail(ErrorMessages.User.UserNotFound);

                    // Simulate a user found scenario
                    return new ModelResponse<User>().Success(new User
                    {
                        Id = id,
                        Username = "camilletural",
                        FirstName = "Camille",
                        LastName = "Tural",
                        Email = "camilletural@furkantural.com",
                        Password = "camillesPassword",
                        InsertDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        DeleteDate = null,
                        IsActive = true,
                        IsDeleted = false
                    });
                });

            // Setup DeleteAsync mock
            _mockUserRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int id, CancellationToken token) =>
                {
                    // Simulate a user not found scenario
                    if (id <= 0)
                        return new ModelResponse<User>().Fail("");

                    // Simulate a user found scenario
                    return new ModelResponse<User>().Success();
                });

            // Define service
            _userService = CreateUserService();
        }
        #endregion
        #region Methods
        [Theory]
        [MemberData(nameof(FakeUserData.InvalidDeleteUsers), MemberType = typeof(FakeUserData))]
        public async Task DeleteAsync_ShouldReturnFail_WhenUserIsInvalid(UserDto model)
        {
            // Act
            var result = await _userService.DeleteAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFail_WhenIdIsMissing()
        {
            // Arrange
            var model = FakeUserData.MissingId();

            // Act
            var result = await _userService.DeleteAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.IdIsRequired, result.Error.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFail_WhenUserDoesNotExist()
        {
            // Arrange
            var model = FakeUserData.MissingUser();

            // Act
            var result = await _userService.DeleteAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserNotFound, result.Error.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFail_WhenUserIsDeleted()
        {
            // Arrange
            var model = FakeUserData.ValidUserModel();
            model.IsDeleted = true;

            // Act
            var result = await _userService.DeleteAsync(model);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessages.User.UserIsNotActive, result.Error.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccess_WhenUserIsValid()
        {
            // Arrange
            var model = FakeUserData.ValidUserModel();

            // Act
            var result = await _userService.DeleteAsync(model);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }
        #endregion
    }
}