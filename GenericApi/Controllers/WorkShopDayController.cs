using FluentValidation;
using GenericApi.Bl.Dto;
using GenericApi.Bl.Validations;
using GenericApi.Model.Entities;
using GenericApi.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApi.Controllers
{
    public class WorkShopDayController : BaseController<WorkShopDay, WorkShopDayDto>
    {
        public WorkShopDayController(IWorkShopDayService service) : base(service)
        {
        }
    }
}
