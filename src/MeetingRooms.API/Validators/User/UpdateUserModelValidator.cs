using FluentValidation;
using MeetingRooms.API.Models.User;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.User;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        #region Id
        RuleFor(user => user.Id)
            .NotEmpty()
            .WithMessage(user => string.Format(APIMessage.Property_Empty, nameof(user.Id)));

        RuleFor(user => user.Id)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(user => user.Id is not null)
            .WithMessage(user => string.Format(APIMessage.Property_Invalid_Format, nameof(user.Id)));
        #endregion Id

        #region Body
        RuleFor(user => user.Body)
            .SetValidator(new UpdateUserBodyModelValidator()!);
        #endregion Body
    }
}
