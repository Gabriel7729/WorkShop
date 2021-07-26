using AutoMapper;
using FluentValidation;
using GenericApi.Bl.Dto;
using GenericApi.Model.Entities;
using GenericApi.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApi.Services.Services
{ 
    public interface IDocumentService : IBaseService<Document, DocumentDto> {}
    public class DocumentService : BaseService<Document, DocumentDto>, IDocumentService
    {
        public DocumentService(
            IDocumentRepository repository, 
            IMapper mapper, 
            IValidator<DocumentDto> validator) : base(repository,mapper, validator)
        {
        }
    }
}
