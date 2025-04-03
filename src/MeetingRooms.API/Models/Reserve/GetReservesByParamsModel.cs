using MeetingRooms.Domain.Enums;

namespace MeetingRooms.API.Models.Reserve;

public class GetReservesByParamsModel
{
    public string? UserId { get; set; }

    public string? RoomId { get; set; }

    public string? InitialDate { get; set; }

    public string? FinalDate { get; set; }

    public ReserveStatus? Status { get; set; }
}
