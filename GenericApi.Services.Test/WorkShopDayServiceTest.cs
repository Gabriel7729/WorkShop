using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GenericApi.Model.Contexts;
using GenericApi.Model.Entities;
using GenericApi.Model.Repositories;
using GenericApi.Bl.Dto;
using GenericApi.Bl.Mapper;
using GenericApi.Bl.Validations;
using GenericApi.Core.Settings;
using GenericApi.Services.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GenericApi.Services.Test
{
    public class WorkShopDayServiceTest
    {
        private readonly IWorkShopDayService _workShopDayService;
        private readonly WorkShopDay _newWorkShopDay = new WorkShopDay
        {
            Day = Core.Enums.WeekDay.MONDAY,
            Mode = Core.Enums.WorkShopDayMode.ON_SITE,
            ModeLocation = "Company SOLVEX",
            StartHour = new System.TimeSpan(10,00,00),
            EndHour = new System.TimeSpan(1, 00, 00),
            WorkShopId = 1
        };

        private readonly WorkShopDay _oldWorkShopDay = new WorkShopDay
        {
            Day = Core.Enums.WeekDay.FRIDAY,
            Mode = Core.Enums.WorkShopDayMode.VIRTUAL,
            ModeLocation = "HOME",
            StartHour = new System.TimeSpan(9, 00, 00),
            EndHour = new System.TimeSpan(12, 00, 00),
            WorkShopId = 1
        };

        public WorkShopDayServiceTest()
        {
            #region Autommaper

            var mapper = new MapperConfiguration(x => x.AddProfile<MainProfile>())
               .CreateMapper();

            #endregion

            #region Repository

            var optionsBuilder = new DbContextOptionsBuilder<WorkShopContext>();
            optionsBuilder.UseInMemoryDatabase("WorkShop");
            var context = new WorkShopContext(optionsBuilder.Options);
            context.AddRange(_newWorkShopDay, _oldWorkShopDay);
            context.SaveChanges();

            IWorkShopDayRepository respository = new WorkShopDayRepository(context);

            #endregion

            #region Validator

            var validator = new WorkShopDayValidator();

            #endregion

            _workShopDayService = new WorkShopDayService(respository, mapper, validator);
        }

        [Fact]
        public async Task ShouldSaveWorkShopDayAsync()
        {
            //Arrange
            var requestDto = new WorkShopDayDto
            {
                Day = Core.Enums.WeekDay.TUESDAY,
                Mode = Core.Enums.WorkShopDayMode.ON_SITE,
                ModeLocation = "Company SOLVEX",
                StartHour = new System.TimeSpan(8, 00, 00),
                EndHour = new System.TimeSpan(5, 00, 00),
                WorkShopId = 1
            };

            //Act
            var result = await _workShopDayService.AddAsync(requestDto);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ShouldGetWorkShopDayGeAllAsync()
        {
            //Act
            var result = await _workShopDayService.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldDeleteWorkShopDayAsync()
        {
            //Arrange
            var id = 1;

            //Act
            var result = await _workShopDayService.DeleteByIdAsync(id);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.True(result.Entity.Deleted);
        }

        [Fact]
        public async Task ShouldUpdateWorkShopDayAsync()
        {
            //Arrange
            var id = 2;

            var requestDto = new WorkShopDayDto
            {
                Id = 2,
                Day = Core.Enums.WeekDay.MONDAY,
                Mode = Core.Enums.WorkShopDayMode.ON_SITE,
                ModeLocation = "HOME",
                StartHour = new System.TimeSpan(10, 00, 00),
                EndHour = new System.TimeSpan(1, 30, 00),
                WorkShopId = 1
            };

            var result = await _workShopDayService.UpdateAsync(id, requestDto);

            //Assert

            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Equal(requestDto.Day, result.Entity.Day);

        }
    }
}
