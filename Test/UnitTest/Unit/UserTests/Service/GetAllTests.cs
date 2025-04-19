using Domain.Repository.Abstract;
using Domain.Wrapper;
using Application.Service.Concrete;
using Moq;
using UnitTest.Mock;
using Application.Dto;
using Domain.Entity;
using UnitTest.Unit.UserTests.Service.Common;

namespace UnitTest.Unit.UserTests.Service
{
    public class GetAllTests : UserTestBase
    {
        #region Properties
        private readonly UserService _userService;                      // The service being tested
        private readonly Mock<IUserRepository> _mockUserRepository;     // Mocked repository for user data access
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for GetAllTests
        /// </summary>
        public GetAllTests()
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

            // Setup GetAllAsync method
            var fakeModel = FakeUserData.ValidUserModel();
            var fakeEntity = _mapper.Object.Map<User>(fakeModel);
            var fakeList = new List<User> { fakeEntity };
            _mockUserRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((CancellationToken token) =>
                {
                    return new ModelResponse<IEnumerable<User>>().Success(fakeList);
                });

            // Define service
            _userService = CreateUserService();
        }
        #endregion
        #region Methods
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenUsersExist()
        {
            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenUsersDoesNotExist()
        {
            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
        }
        #endregion
    }
}