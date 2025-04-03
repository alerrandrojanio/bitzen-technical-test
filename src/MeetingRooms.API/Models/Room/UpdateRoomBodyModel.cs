namespace MeetingRooms.API.Models.Room;

public class UpdateRoomBodyModel
{
    public string Name { get; set; } = string.Empty;

    public int Capacity { get; set; }
}
