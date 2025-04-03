using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Models.Reserve;

public class CancelReserveModel
{
    [FromRoute]
    public string Id { get; set; } = string.Empty;
}
