using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Models.Room;

public class DeleteRoomModel
{
    [FromRoute]
    public string Id { get; set; } = string.Empty;
}
