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
    public interface IWorkShopDayService : IBaseService<WorkShopDay, WorkShopDayDto>  {}
    public class WorkShopDayService : BaseService<WorkShopDay, WorkShopDayDto>, IWorkShopDayService
    {
        public WorkShopDayService(
            IWorkShopDayRepository repository, 
            IMapper mapper, 
            IValidator<WorkShopDayDto> validator) : base(repository,mapper, validator)
        {
        }
    }
}
