using FluentValidation;
using MeetingRooms.API.Models.Reserve;
using MeetingRooms.API.Resources;

namespace MeetingRooms.API.Validators.Reserve;

public class CreateReserveModelValidator : AbstractValidator<CreateReserveModel>
{
    public CreateReserveModelValidator()
    {
        #region UserId
        RuleFor(reserve => reserve.UserId)
            .NotEmpty()
            .WithMessage(reserve => string.Format(APIMessage.Property_Empty, nameof(reserve.UserId)));

        RuleFor(reserve => reserve.UserId)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(reserve => reserve.UserId is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.UserId)));
        #endregion UserId

        #region RoomId
        RuleFor(reserve => reserve.RoomId)
            .NotEmpty()
            .WithMessage(reserve => string.Format(APIMessage.Property_Empty, nameof(reserve.RoomId)));

        RuleFor(reserve => reserve.UserId)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(reserve => reserve.RoomId is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.RoomId)));
        #endregion RoomId

        #region InitialDate
        RuleFor(reserve => reserve.InitialDate)
            .NotEmpty()
            .WithMessage(reserve => string.Format(APIMessage.Property_Empty, nameof(reserve.InitialDate)));

        RuleFor(person => person.InitialDate)
            .Must(initialDate => DateTime.TryParse(initialDate, out _))
            .When(person => person.InitialDate is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.InitialDate)));

        RuleFor(person => person.InitialDate)
            .Must(initialDate => DateTime.TryParse(initialDate, out var date) && date > DateTime.UtcNow)
            .When(reserve => reserve.InitialDate is not null && DateTime.TryParse(reserve.InitialDate, out _))
            .WithMessage(reserve => string.Format(APIMessage.Property_GreaterThan, nameof(reserve.InitialDate), DateTime.UtcNow));
        #endregion InitialDate

        #region FinalDate
        RuleFor(reserve => reserve.FinalDate)
            .NotEmpty()
            .WithMessage(reserve => string.Format(APIMessage.Property_Empty, nameof(reserve.FinalDate)));

        RuleFor(reserve => reserve.FinalDate)
            .Must(finalDate => DateTime.TryParse(finalDate, out _))
            .When(reserve => reserve.FinalDate is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.FinalDate)));

        RuleFor(reserve => reserve)
            .Must((_, reserve) => DateTime.TryParse(reserve.InitialDate, out var initialDate) &&
                                  DateTime.TryParse(reserve.FinalDate, out var finalDate) &&
                                  finalDate > initialDate)
            .WithMessage(reserve => string.Format(APIMessage.Property_GreaterThan, nameof(reserve.FinalDate), nameof(reserve.InitialDate)));


        RuleFor(reserve => reserve)
            .Must((_, reserve) => DateTime.TryParse(reserve.InitialDate, out var initialDate) &&
                                  DateTime.TryParse(reserve.FinalDate, out var finalDate) &&
                                  initialDate.Date == finalDate.Date)
            .WithMessage(reserve => string.Format(APIMessage.Property_Date_SameDay, nameof(reserve.InitialDate), nameof(reserve.FinalDate)));
        #endregion FinalDate
    }
}
