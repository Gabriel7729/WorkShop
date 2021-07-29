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
    public class WorkShopServiceTest
    {
        private readonly IWorkShopService _workShopService;
        private readonly WorkShop _workshop = new WorkShop
        {
            Name = "WorkShop Solvex",
            Description = "This is a workshop that serves for training programmers to work in our company",
            StartDate = new System.DateTime(2021,01,1),
            EndDate = new System.DateTime(2021, 03, 30),
            ContentSupport = "C#, CLEAN CODE and SOLID PRINCIPLES"
        };

        private readonly WorkShop _workshop2 = new WorkShop
        {
            Name = "WorkShop Solvex 2",
            Description = "This is a workshop that serves for training programmers to work in our company",
            StartDate = new System.DateTime(2021, 04, 1),
            EndDate = new System.DateTime(2021, 06, 30),
            ContentSupport = "CLEAN CODE and SOLID PRINCIPLES"
        };

        public WorkShopServiceTest()
        {
            #region Autommaper

            var mapper = new MapperConfiguration(x => x.AddProfile<MainProfile>())
               .CreateMapper();

            #endregion

            #region Repository

            var optionsBuilder = new DbContextOptionsBuilder<WorkShopContext>();
            optionsBuilder.UseInMemoryDatabase("WorkShop");
            var context = new WorkShopContext(optionsBuilder.Options);
            context.AddRange(_workshop, _workshop2);
            context.SaveChanges();

            IWorkShopRepository respository = new WorkShopRepository(context);

            #endregion

            #region Validator

            var validator = new WorkShopValidator();

            #endregion

            _workShopService = new WorkShopService(respository, mapper, validator);
        }

        [Fact]
        public async Task ShouldSaveWorkShopAsync()
        {
            //Arrange
            var requestDto = new WorkShopDto
            {
                Name = "WorkShop Solvex",
                Description = "This is a workshop that serves for training programmers to work in our company",
                StartDate = new System.DateTime(2021, 01, 1),
                EndDate = new System.DateTime(2021, 03, 30),
                ContentSupport = "C#, CLEAN CODE and SOLID PRINCIPLES"
            };

            //Act
            var result = await _workShopService.AddAsync(requestDto);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ShouldGetWorkShopGeAllAsync()
        {
            //Act
            var result = await _workShopService.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldDeleteWorkShopAsync()
        {
            //Arrange
            var id = 1;

            //Act
            var result = await _workShopService.DeleteByIdAsync(id);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.True(result.Entity.Deleted);
        }

        [Fact]
        public async Task ShouldUpdateWorkShopAsync()
        {
            //Arrange
            var id = 2;

            var requestDto = new WorkShopDto
            {
                Id = 2,
                Name = "WorkShop Solvex 2",
                Description = "This is a workshop that serves for training programmers to work in our company",
                StartDate = new System.DateTime(2021, 04, 1),
                EndDate = new System.DateTime(2021, 06, 30),
                ContentSupport = "C#, .NET CORE 3.1, CLEAN CODE and SOLID PRINCIPLES"
            };

            var result = await _workShopService.UpdateAsync(id, requestDto);

            //Assert

            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Equal(requestDto.Name, result.Entity.Name);

        }
    }
}
