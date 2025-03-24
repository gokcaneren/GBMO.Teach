using FluentValidation;
using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;

namespace GBMO.Teach.Application.Validations.Auth.User
{
    public class UserRegisterInputValidator : AbstractValidator<UserRegisterInput>
    {
        public UserRegisterInputValidator(IStringLocalizer<SharedResources> _localizer)
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage(_localizer["Vldn.EmailCannotBeNull"])
                .NotEmpty().WithMessage(_localizer["Vldn.EmailCannotBeEmpty"])
                .EmailAddress().WithMessage(_localizer["Vldn.EmailFormatError"]);

            RuleFor(c=>c.Password)
                .NotNull().WithMessage(_localizer["Vldn.PasswordError"])
                .NotEmpty().WithMessage(_localizer["Vldn.PasswordError"])
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[.!?\-_&%/(]).{6,}$")
            .WithMessage(_localizer["Vldn.PasswordError"]);


            RuleFor(c => c.FirstName)
                .NotNull().WithMessage(_localizer["Vldn.FirstNameCannetBeNullOrEmpty"])
                .NotEmpty().WithMessage(_localizer["Vldn.FirstNameCannetBeNullOrEmpty"]);

            RuleFor(c => c.LastName)
                .NotNull().WithMessage(_localizer["Vldn.LastNameCannotBeNullOrEmpty"])
                .NotEmpty().WithMessage(_localizer["Vldn.LastNameCannotBeNullOrEmpty"]);
        }
    }
}
