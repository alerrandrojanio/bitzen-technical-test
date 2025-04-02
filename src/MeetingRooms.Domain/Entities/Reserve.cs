using MeetingRooms.Domain.Entities.Base;

namespace MeetingRooms.Domain.Entities;

public class Reserve : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime InitialDate { get; set; }

    public DateTime FinalDate { get; set; }
}
