using System;
using FluentValidation;
using PaintStore.Models.DTOs;

namespace PaintStore.API.Validators;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name can not be empty")
                          .MaximumLength(50).WithMessage("Name Length can not be more than 50")
                          .Must(IsValidName).WithMessage("Name can not contain @");
        
        RuleFor(x=>x.Email).EmailAddress().WithMessage("Email format is wrong");
    }

    private bool IsValidName(string name)
    {
        return !name.Contains("@");
    }
}
