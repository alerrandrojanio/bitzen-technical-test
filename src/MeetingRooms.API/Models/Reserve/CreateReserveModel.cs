namespace MeetingRooms.API.Models.Reserve;

public class CreateReserveModel
{
    public string? UserId { get; set; }

    public string? RoomId { get; set; }

    public string? InitialDate { get; set; }

    public string? FinalDate { get; set; }
}
