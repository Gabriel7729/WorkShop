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
    public interface IWorkShopService : IBaseService<WorkShop, WorkShopDto> {}
    public class WorkShopService : BaseService<WorkShop, WorkShopDto>, IWorkShopService
    {
        public WorkShopService(
            IWorkShopRepository repository, 
            IMapper mapper, 
            IValidator<WorkShopDto> validator) : base(repository,mapper, validator)
        {
        }
    }
}
