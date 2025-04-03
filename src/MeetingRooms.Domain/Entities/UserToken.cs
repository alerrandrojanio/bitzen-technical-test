using MeetingRooms.Domain.Entities.Base;

namespace MeetingRooms.Domain.Entities;

public class UserToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
}
