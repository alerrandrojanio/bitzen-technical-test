using FluentValidation;
using MeetingRooms.API.Models.User;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.User;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        #region Name
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage(user => string.Format(APIMessage.Property_Empty, nameof(user.Name)));

        RuleFor(user => user.Name)
            .MinimumLength(3)
            .WithMessage(user => string.Format(APIMessage.Property_MinimumLength, nameof(user.Name), 3));
        #endregion Name

        #region Email
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(user => string.Format(APIMessage.Property_Empty, nameof(user.Email)));

        RuleFor(user => user.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .When(user => user.Email is not null)
            .WithMessage(APIMessage.Property_Email_Format);
        #endregion Email

        #region Password
        RuleFor(user => user.Password)
           .NotEmpty()
           .WithMessage(user => string.Format(APIMessage.Property_Empty, nameof(user.Password)));

        RuleFor(user => user.Password)
            .MinimumLength(8)
            .WithMessage(user => string.Format(APIMessage.Property_MinimumLength, nameof(user.Password), 8));
        #endregion Password
    }
}
