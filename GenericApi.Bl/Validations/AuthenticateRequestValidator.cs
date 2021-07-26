using FluentValidation;
using GenericApi.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Validations
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequestDto>
    {
		public AuthenticateRequestValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Username is required");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required")
				.MinimumLength(8)
				.WithMessage("Password's length must be at least 8 characters");
		}
	}
}
