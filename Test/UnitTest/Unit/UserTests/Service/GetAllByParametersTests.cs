using Domain.Repository.Abstract;
using Application.Service.Concrete;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using UnitTest.Mock;
using Domain.Entity;
using Application.Dto;
using UnitTest.Unit.UserTests.Service.Common;

namespace UnitTest.Unit.UserTests.Service
{
    public class GetAllByParametersTests : UserTestBase
    {
        #region Properties
        private readonly UserService _userService;                      // The service being tested
        private readonly Mock<IUserRepository> _mockUserRepository;     // Mocked repository for user data access
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for GetAllByParametersTests
        /// </summary>
        public GetAllByParametersTests()
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

            //// Setup GetAllByParameters method
            //var param = FakeUserData.ValidListRequestParameter();
            //var fakeRepoResponse = FakeUserData.ValidPagedUserModelResponse();
            //_mockUserRepository
            //    .Setup(r => r.GetAllByParametersAsync(
            //        It.IsAny<Expression<Func<Domain.Entities.User, bool>>>(),
            //        It.IsAny<Func<IQueryable<Domain.Entities.User>, IOrderedQueryable<Domain.Entities.User>>>(),
            //        It.IsAny<Func<IQueryable<Domain.Entities.User>, IQueryable<Domain.Entities.User>>>(),
            //        param.PageNumber,
            //        param.PageSize,
            //        false, true, false,
            //        It.IsAny<CancellationToken>()
            //    ))
            //    .ReturnsAsync(fakeRepoResponse);

            // Define service
            _userService = CreateUserService();
        }
        #endregion
        #region Methods

        #endregion
    }
}