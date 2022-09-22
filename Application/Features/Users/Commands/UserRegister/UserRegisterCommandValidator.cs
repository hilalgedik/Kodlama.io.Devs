using Application.Features.Users.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UserRegister
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage(UserMessages.FirstNameIsRequired);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage(UserMessages.LastNameIsRequired);

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage(UserMessages.EmailAddressIsRequired)
                .EmailAddress().WithMessage(UserMessages.EmailAddressIsNotValid);

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage(UserMessages.PasswordIsRequired);
        }
    }
}
