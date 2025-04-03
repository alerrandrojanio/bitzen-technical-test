using FluentValidation;
using MeetingRooms.API.Models.Reserve;
using MeetingRooms.API.Resources;
using MeetingRooms.Domain.Entities;

namespace MeetingRooms.API.Validators.Reserve;

public class GetReservesByParamsModelValidator : AbstractValidator<GetReservesByParamsModel>
{
    public GetReservesByParamsModelValidator()
    {
        #region UserId/RoomId
        RuleFor(reserve => reserve)
            .Must(reserve => !string.IsNullOrEmpty(reserve.UserId) || !string.IsNullOrEmpty(reserve.RoomId))
            .WithMessage(APIMessage.Property_Empty_UserId_RoomId);
        #endregion UserId/RoomId

        #region UserId
        RuleFor(reserve => reserve.UserId)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(reserve => reserve.UserId is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.UserId)));
        #endregion UserId

        #region RoomId
        RuleFor(reserve => reserve.RoomId)
            .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
            .When(reserve => reserve.UserId is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.RoomId)));
        #endregion RoomId

        #region Status
        RuleFor(reserve => reserve.Status)
            .IsInEnum()
            .When(reserve => reserve.Status is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Value, nameof(reserve.Status)));
        #endregion Status

        #region InitialDate/FinalDate
        RuleFor(reserve => reserve)
            .Must(reserve => (string.IsNullOrEmpty(reserve.InitialDate) && string.IsNullOrEmpty(reserve.FinalDate)) ||
                       (!string.IsNullOrEmpty(reserve.InitialDate) && !string.IsNullOrEmpty(reserve.FinalDate)))
            .WithMessage(APIMessage.Property_Empty_Dates);
        #endregion InitialDate/FinalDate

        #region InitialDate
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
            .Must(finalDate => DateTime.TryParse(finalDate, out _))
            .When(reserve => reserve.FinalDate is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_Invalid_Format, nameof(reserve.FinalDate)));

        RuleFor(reserve => reserve)
            .Must((_, reserve) => DateTime.TryParse(reserve.InitialDate, out var initialDate) &&
                                  DateTime.TryParse(reserve.FinalDate, out var finalDate) &&
            finalDate > initialDate)
            .When(reserve => reserve.InitialDate is not null && reserve.FinalDate is not null)
            .WithMessage(reserve => string.Format(APIMessage.Property_GreaterThan, nameof(reserve.FinalDate), nameof(reserve.InitialDate)));
        #endregion FinalDate
    }
}
