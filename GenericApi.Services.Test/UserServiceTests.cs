using AutoMapper;
using GenericApi.Bl.Dto;
using GenericApi.Bl.Mapper;
using GenericApi.Bl.Validations;
using GenericApi.Core.Settings;
using GenericApi.Model.Contexts;
using GenericApi.Model.Repositories;
using GenericApi.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GenericApi.Services.Test
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        public UserServiceTests()
        {
            #region Autommaper

            var mapper = new MapperConfiguration(x => x.AddProfile<MainProfile>())
               .CreateMapper();

            #endregion

            #region Repository

            var optionsBuilder = new DbContextOptionsBuilder<WorkShopContext>();
            optionsBuilder.UseInMemoryDatabase("WorkShop");
            var context = new WorkShopContext(optionsBuilder.Options);

            IUserRepository respository = new UserRepository(context);

            #endregion

            #region Validator

            var validator = new UserValidator();

            #endregion

            #region Option Settings

            var settings = Options.Create(new JwtSettings
            {
                ExpiresInMinutes = 10,
                Secret = "0263875b-b775-4426-938c-ab7c04c74b22"
            });

            #endregion

            _userService = new UserService(respository, mapper, validator, settings);
        }

        [Fact]
        public async Task ShouldSaveUserAsync()
        {
            //Arrange
            var requestDto = new UserDto
            {
                Name = "Emmanuel",
                MiddleName = "Enrique",
                LastName = "Jimenez",
                SecondLastName = "Pimentel",
                Dob = new System.DateTime(1996, 06, 16),
                DocumentType = Core.Enums.DocumentType.ID,
                DocumentTypeValue = "22500851658",
                Gender = Core.Enums.Gender.MALE,
                UserName = "emmanuel",
                Password = "Hola1234,"
            };

            //Act
            var result = await _userService.AddAsync(requestDto);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ShouldGetAllUserAsync()
        {
            //Arrange

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);

        }
    }
}
