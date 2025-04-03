namespace MeetingRooms.Domain.DTOs.Room.Response;

public class GetRoomsResponseDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Capacity { get; set; }

    public DateTime? CreatedAt { get; set; }
}
