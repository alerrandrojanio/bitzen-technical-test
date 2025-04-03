using FluentValidation;
using MeetingRooms.API.Models.Room;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.Room;

public class UpdateRoomModelValidator : AbstractValidator<UpdateRoomModel>
{
    public UpdateRoomModelValidator()
    {
        #region Id
        RuleFor(room => room.Id)
            .NotEmpty()
            .WithMessage(room => string.Format(APIMessage.Property_Empty, nameof(room.Id)));

        RuleFor(room => room.Id)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(room => room.Id is not null)
            .WithMessage(room => string.Format(APIMessage.Property_Invalid_Format, nameof(room.Id)));
        #endregion Id

        #region Body
        RuleFor(room => room.Body)
            .SetValidator(new UpdateRoomBodyModelValidator()!);
        #endregion Body
    }
}

