using FluentValidation;
using GenericApi.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Validations
{
    public class WorkShopValidator : AbstractValidator<WorkShopDto>
    {
		public WorkShopValidator()
		{
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("This fields is required")
                .MinimumLength(2)
                .WithMessage("Your name can not have only one character")
                .MaximumLength(50)
                .WithMessage("Your name can not have 50 character");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("This fields is required")
                .MinimumLength(2)
                .WithMessage("The description can't have only one character");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("This fields is required");

            RuleFor(x => x.ContentSupport)
                .NotEmpty()
                .WithMessage("This fields is required");
        }
	}
}
