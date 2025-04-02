using MeetingRooms.Domain.Entities.Base;

namespace MeetingRooms.Domain.Entities;

public class Room : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int Capacity { get; set; }
}
