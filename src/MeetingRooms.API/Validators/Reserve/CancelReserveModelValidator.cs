using FluentValidation;
using MeetingRooms.API.Models.Reserve;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.Reserve;

public class CancelReserveModelValidator : AbstractValidator<CancelReserveModel>
{
    public CancelReserveModelValidator()
    {
        #region Id
        RuleFor(reserve => reserve.Id)
            .NotEmpty()
            .WithMessage(reserve => string.Format(APIMessage.Property_Empty, nameof(reserve.Id)));

        RuleFor(reserve => reserve.Id)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(reserve => reserve.Id is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.Id)));
        #endregion Id
    }
}
