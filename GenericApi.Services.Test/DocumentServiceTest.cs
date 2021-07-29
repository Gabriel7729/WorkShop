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
    public class DocumentServiceTest
    {
        private readonly IDocumentService _documentService;
        private readonly Document _newDocument = new Document
        {
            FileName = "Video de Emmanuel",
            OriginalName = "Video",
            ContentType = ".mp4"
        };

        private readonly Document _oldDocument = new Document
        {
            FileName = "Video de Solvex",
            OriginalName = "Video",
            ContentType = ".mp4"
        };

        public DocumentServiceTest()
        {
            #region Autommaper

            var mapper = new MapperConfiguration(x => x.AddProfile<MainProfile>())
               .CreateMapper();

            #endregion

            #region Repository

            var optionsBuilder = new DbContextOptionsBuilder<WorkShopContext>();
            optionsBuilder.UseInMemoryDatabase("WorkShop");
            var context = new WorkShopContext(optionsBuilder.Options);
            context.AddRange(_newDocument, _oldDocument);
            context.SaveChanges();

            IDocumentRepository respository = new DocumentRepository(context);

            #endregion

            #region Validator

            var validator = new DocumentValidator();

            #endregion

            _documentService = new DocumentService(respository, mapper, validator);
        }

        [Fact]
        public async Task ShouldSaveDocumentAsync()
        {
            //Arrange
            var requestDto = new DocumentDto
            {
                FileName = "Video de Emmanuel",
                OriginalName = "Video",
                ContentType = ".mp4"
            };

            //Act
            var result = await _documentService.AddAsync(requestDto);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ShouldGetDocumentGeAllAsync()
        {
            //Act
            var result = await _documentService.GetAllAsync();

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ShouldDeleteDocumentAsync()
        {
            //Arrange
            var id = 1;

            //Act
            var result = await _documentService.DeleteByIdAsync(id);

            //Assert
            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.True(result.Entity.Deleted);
        }

        [Fact]
        public async Task ShouldUpdateDocumentAsync()
        {
            //Arrange
            var id = 2;

            var requestDto = new DocumentDto
            {
                Id = 2,
                FileName = "Canción del ITLA",
                OriginalName = "Canción",
                ContentType = ".mp3"
            };

            var result = await _documentService.UpdateAsync(id, requestDto);

            //Assert

            Assert.True(result.IsSuccess, result.Errors.FirstOrDefault());
            Assert.NotNull(result.Entity);
            Assert.Equal(requestDto.FileName, result.Entity.FileName);

        }
    }
}
