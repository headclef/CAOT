using Domain.Wrapper;
using Domain.Parameter.Concrete;
using Application.Dto;

namespace UnitTest.Mock
{
    public static class FakeUserData
    {
        #region Methods
        public static IEnumerable<object[]> InvalidInsertUsers =>
            new List<object[]>
            {
                new object[] { MissingUsernameInsert() },
                new object[] { MissingFirstNameInsert() },
                new object[] { MissingLastNameInsert() },
                new object[] { MissingEmailInsert() },
                new object[] { MissingPasswordInsert() },
                new object[] { InvalidEmailInsert() },
                new object[] { InvalidInsertDateInsert() },
                new object[] { InvalidActivityInsert() }
            };

        public static IEnumerable<object[]> InvalidUpdateUsers =>
            new List<object[]>
            {
                new object[] { MissingId() },
                new object[] { MissingFirstNameUpdate() },
                new object[] { MissingLastNameUpdate() },
                new object[] { MissingEmailUpdate() },
                new object[] { MissingPasswordUpdate() },
                new object[] { InvalidEmailUpdate() },
                new object[] { InvalidInsertDateUpdate() },
                new object[] { InvalidActivityUpdate() },
                new object[] { InvalidUpdateDateUpdate() }
            };

        public static IEnumerable<object[]> InvalidDeleteUsers =>
            new List<object[]>
            {
                new object[] { MissingId() },
                new object[] { InvalidDeleteDateDelete() },
                new object[] { InvalidActivityDelete() }
            };

        public static UserDto ValidUserModel() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = null,
            DeleteDate = null,
            IsActive = true,
            IsDeleted = false
        };

        public static UserDto ValidUserInsertModel() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = null,
            DeleteDate = null,
            IsActive = true,
            IsDeleted = false
        };

        public static ListRequestParameter ValidListRequestParameter() => new ListRequestParameter
        {
            PageNumber = 1,
            PageSize = 10,
            Search = null,
            SortBy = null,
            SortOrder = null,
            StartDate = null,
            EndDate = null
        };

        public static PagedModelResponse<IEnumerable<UserDto>> ValidPagedUserModelResponse()
        {
            var model = ValidUserModel();
            var list = new[] { model };
            return new PagedModelResponse<IEnumerable<UserDto>>()
                .Success(
                    data: list,
                    pageNumber: 1,
                    pageSize: 10,
                    totalPages: 1,
                    totalItems: list.Length
                );
        }

        public static UserDto MissingUser() => new UserDto
        {
            Id = 99,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = null,
            DeleteDate = null,
            IsActive = true,
            IsDeleted = false
        };

        public static UserDto MissingId() => new UserDto
        {
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingUsernameInsert() => new UserDto
        {
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = null,
            DeleteDate = null,
            IsActive = true,
            IsDeleted = false
        };

        public static UserDto MissingFirstNameInsert() => new UserDto
        {
            Username = "camilletural",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingLastNameInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingEmailInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingPasswordInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidEmailInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camillefurkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidInsertDateInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.MinValue,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidUpdateDateInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = DateTime.MinValue,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidDeleteDateInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            DeleteDate = DateTime.MinValue,
            IsDeleted = true,
            IsActive = false
        };

        public static UserDto InvalidActivityInsert() => new UserDto
        {
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = true,
            IsActive = false
        };

        public static UserDto MissingUsernameUpdate() => new UserDto
        {
            Id = 1,
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = null,
            DeleteDate = null,
            IsActive = true,
            IsDeleted = false
        };

        public static UserDto MissingFirstNameUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingLastNameUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingEmailUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto MissingPasswordUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidEmailUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camillefurkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidInsertDateUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.MinValue,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidUpdateDateUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            UpdateDate = DateTime.MinValue,
            IsDeleted = false,
            IsActive = true
        };

        public static UserDto InvalidDeleteDateUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            DeleteDate = DateTime.MinValue,
            IsDeleted = true,
            IsActive = false
        };

        public static UserDto InvalidActivityUpdate() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = true,
            IsActive = false
        };

        public static UserDto InvalidDeleteDateDelete() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            DeleteDate = DateTime.MinValue,
            IsDeleted = true,
            IsActive = false
        };

        public static UserDto InvalidActivityDelete() => new UserDto
        {
            Id = 1,
            Username = "camilletural",
            FirstName = "Camille",
            LastName = "Tural",
            Email = "camille@furkantural.com",
            Password = "camillesPassword",
            InsertDate = DateTime.Now,
            IsDeleted = true,
            IsActive = false
        };
        #endregion
    }
}