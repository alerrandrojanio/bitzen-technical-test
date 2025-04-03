using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Models.Room;

public class UpdateRoomModel
{
    [FromRoute]
    public string? Id { get; set; } = string.Empty;

    [FromBody]
    public UpdateRoomBodyModel? Body { get; set; }
}
