using FluentValidation;
using GenericApi.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
		public UserValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("User's Name is required");
		}
	}
}
