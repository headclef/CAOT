using Application.Service.Concrete;
using UnitTest.Helper;

namespace UnitTest.Unit.UserTests.Service.Common
{
    public class UserTestBase : TestBase
    {
        #region Methods
        protected UserService CreateUserService()
        {
            return new UserService(
                _unitOfWork.Object,
                _mapper.Object,
                _logService.Object,
                _mailService.Object
            );
        }
        #endregion
    }
}