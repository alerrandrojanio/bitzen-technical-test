namespace MeetingRooms.API.Models.Room;

public class CreateRoomModel
{
    public string Name { get; set; } = string.Empty;

    public int? Capacity { get; set; }
}
