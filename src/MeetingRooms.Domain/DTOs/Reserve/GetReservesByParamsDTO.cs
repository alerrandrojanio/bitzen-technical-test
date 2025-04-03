using MeetingRooms.Domain.Enums;

namespace MeetingRooms.Domain.DTOs.Reserve;

public class GetReservesByParamsDTO
{
    public Guid? UserId { get; set; }

    public Guid? RoomId { get; set; }

    public DateTime? InitialDate { get; set; }

    public DateTime? FinalDate { get; set; }

    public ReserveStatus? Status { get; set; }
}
