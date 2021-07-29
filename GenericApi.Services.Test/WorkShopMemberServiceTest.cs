using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GenericApi.Model.Contexts;
using GenericApi.Model.Entities;
using GenericApi.Model.Repositories;
using GenericApi.Bl.Dto;
using GenericApi.Bl.Mapper;
using GenericApi.Bl.Validations;
using GenericApi.Services.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GenericApi.Services.Test
{
    public class WorkShopMemberServiceTest
    {
        private readonly IWorkShopMemberService _workShopMemberService;
        private readonly WorkShopMember _newWorkShopMember = new WorkShopMember
        {
            Role = Core.Enums.WorkShopMemberRole.STUDENT,
            WorkShopId = 1,
            UserId = 1
        };

        private readonly WorkShopMember _oldWorkShopMember = new WorkShopMember
        {
            Role = Core.Enums.WorkShopMemberRole.TEACHER,
            WorkShopId = 1,
            UserId = 1
        };

        public WorkShopMemberServiceTest()
        {
            #region Autommaper

            var mapper = new MapperConfiguration(x => x.AddProfile<MainProfile>())
               .CreateMapper();

            #endregion

            #region Repository

            var optionsBuilder = new DbContextOptionsBuilder<WorkShopContext>();
            optionsBuilder.UseInMemoryDatabase("WorkShop");
            var context = new WorkShopContext(optionsBuilder.Options);
            context.AddRange(_newWorkShopMember, _oldWorkShopMember);
            context.SaveChanges();

            IWorkShopMemberRepository respository = new WorkShopMemberRepository(context);

            #endregion

            #region Validator

            var validator = new WorkShopMemberValidator();

            #endregion

            _workShopMemberService = new WorkShopMemberService(respository, mapper, validator);
        }

        [Fact]
        public async Task ShouldSaveWorkShopMemberAsync()
        {
            //Arrange
            var requestDto = new WorkShopMemberDto
            {
                Role = Core.Enums.WorkShopMemberRole.STUDENT,
                WorkShopId = 1,
                MemberId = 1
            };

            //Act
            var result = await _workShopMemberService.AddAsync(requestDto);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ShouldGetWorkShopMemberGeAllAsync()
        {
            //Act
            var result = await _workShopMemberService.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldDeleteWorkShopMemberAsync()
        {
            //Arrange
            var id = 1;

            //Act
            var result = await _workShopMemberService.DeleteByIdAsync(id);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.True(result.Entity.Deleted);
        }

        [Fact]
        public async Task ShouldUpdateWorkShopMemberAsync()
        {
            //Arrange
            var id = 2;

            var requestDto = new WorkShopMemberDto
            {
                Id = 2,
                Role = Core.Enums.WorkShopMemberRole.TEACHER,
                WorkShopId = 1,
                MemberId = 1
            };

            var result = await _workShopMemberService.UpdateAsync(id, requestDto);

            //Assert

            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Equal(requestDto.Role, result.Entity.Role);

        }
    }
}
