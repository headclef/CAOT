using AutoMapper;
using Application.Service.Abstract;
using Moq;
using Domain.Repository.Abstract;

namespace UnitTest.Helper
{
    public class TestBase
    {
        #region Properties
        protected readonly Mock<ILogService> _logService = new();   // Log service
        protected readonly Mock<IMapper> _mapper = new();           // AutoMapper
        protected readonly Mock<IUnitOfWork> _unitOfWork = new();   // Unit of work
        protected readonly Mock<IMailService> _mailService = new(); // Mail service
        #endregion
    }
}