using FluentValidation;
using MeetingRooms.API.Models.Auth;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.Auth;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        #region Email
        RuleFor(login => login.Email)
            .NotEmpty()
            .WithMessage(login => string.Format(APIMessage.Property_Empty, nameof(login.Email)));

        RuleFor(login => login.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .When(login => login.Email is not null)
            .WithMessage(APIMessage.Property_Email_Format);
        #endregion Email

        #region Password
        RuleFor(login => login.Password)
            .NotEmpty()
            .WithMessage(login => string.Format(APIMessage.Property_Empty, nameof(login.Password)));

        RuleFor(login => login.Password)
            .MinimumLength(8)
            .WithMessage(login => string.Format(APIMessage.Property_MinimumLength, nameof(login.Password), 8));
        #endregion Password
    }
}
