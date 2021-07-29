using FluentValidation;
using GenericApi.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Validations
{
    public class WorkShopDayValidator : AbstractValidator<WorkShopDayDto>
    {
		public WorkShopDayValidator()
		{
            RuleFor(x => x.Day)
                .NotNull()
                .WithMessage("This fields is required")
                .IsInEnum()
                .WithMessage("Write a correct value for this field");

            RuleFor(x => x.Mode)
                .NotNull()
                .WithMessage("This fields is required")
                .IsInEnum()
                .WithMessage("Write a correct value for this field");

            RuleFor(x => x.ModeLocation)
                .NotEmpty()
                .WithMessage("This fields is required");

            RuleFor(x => x.StartHour)
                .NotEmpty()
                .WithMessage("This fields is required");

            RuleFor(x => x.WorkShopId)
                .NotEmpty()
                .WithMessage("This fields is required");

        }
	}
}
