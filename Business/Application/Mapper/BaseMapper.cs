using Application.Dto;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapper
{
    public class BaseMapper : Profile
    {
        #region Constructors
        public BaseMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<BlockedEmail, BlockedEmailDto>().ReverseMap();
        }
        #endregion
    }
}