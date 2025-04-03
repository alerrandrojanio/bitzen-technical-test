using FluentValidation;
using MeetingRooms.API.Models.Room;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.Room;

public class CreateRoomModelValidator : AbstractValidator<CreateRoomModel>
{
    public CreateRoomModelValidator()
    {
        #region Name
        RuleFor(room => room.Name)
            .NotEmpty()
            .WithMessage(room => string.Format(APIMessage.Property_Empty, nameof(room.Name)));

        RuleFor(room => room.Name)
            .MinimumLength(3)
            .WithMessage(room => string.Format(APIMessage.Property_MinimumLength, nameof(room.Name), 3));
        #endregion Name

        #region Capacity
        RuleFor(room => room.Capacity)
            .NotEmpty()
            .WithMessage(room => string.Format(APIMessage.Property_Empty, nameof(room.Capacity)));

        RuleFor(room => room.Capacity)
            .GreaterThan(0)
            .WithMessage(room => string.Format(APIMessage.Property_GreaterThan, nameof(room.Capacity), 0));
        #endregion Capacity
    }
}
