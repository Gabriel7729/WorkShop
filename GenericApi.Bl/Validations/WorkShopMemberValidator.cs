using FluentValidation;
using GenericApi.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Validations
{
    public class WorkShopMemberValidator : AbstractValidator<WorkShopMemberDto>
    {
		public WorkShopMemberValidator()
		{
            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage("This fields is required")
                .IsInEnum()
                .WithMessage("Write a correct value for this field");

            RuleFor(x => x.WorkShopId)
                .NotEmpty()
                .WithMessage("This fields is required");

            RuleFor(x => x.MemberId)
                .NotEmpty()
                .WithMessage("This fields is required");
        }
	}
}
