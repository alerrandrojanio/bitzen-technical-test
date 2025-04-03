namespace MeetingRooms.Domain.DTOs.Room;

public class UpdateRoomDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Capacity { get; set; }
}
