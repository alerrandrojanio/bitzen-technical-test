using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Models.User;

public class DeleteUserModel
{
    [FromRoute]
    public string Id { get; set; } = string.Empty;
}
