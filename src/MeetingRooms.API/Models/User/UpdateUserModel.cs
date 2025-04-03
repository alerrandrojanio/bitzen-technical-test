using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Models.User;

public class UpdateUserModel
{
    [FromRoute]
    public string Id { get; set; } = string.Empty;

    [FromBody]
    public UpdateUserBodyModel? Body { get; set; }
}
