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
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The Name's field is required")
                .MinimumLength(3)
                .WithMessage("The minimum length for this field is 3");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("The LastName's field is required")
                .MinimumLength(3)
                .WithMessage("The minimum length for this field is 3");

            RuleFor(x => x.MiddleName)
                .MinimumLength(3)
                .WithMessage("The minimum length for this field is 3");

            RuleFor(x => x.SecondLastName)
                .MinimumLength(3)
                .WithMessage("The minimum length for this field is 3");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("The Gender's field is required")
                .IsInEnum()
                .WithMessage("Write a correct value for this field");

            RuleFor(x => x.DocumentType)
                .NotEmpty()
                .WithMessage("The DocumentType's field is required")
                .IsInEnum()
                .WithMessage("Write a correct value for this field");

            RuleFor(x => x.DocumentTypeValue)
                .NotEmpty()
                .WithMessage("The DocumentTypeValue's field is required");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("The UserName's field is required")
                .MinimumLength(4)
                .WithMessage("The minimum length for the username is 4");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The Password's field is required")
                .MinimumLength(4)
                .WithMessage("The minimum length for the username is 8 characters");

        }
	}
}
