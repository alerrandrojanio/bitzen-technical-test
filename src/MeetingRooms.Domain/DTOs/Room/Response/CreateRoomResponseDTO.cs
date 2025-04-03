namespace MeetingRooms.Domain.DTOs.Room.Response;

public class CreateRoomResponseDTO
{
    public string? Id { get; set; }
    
    public string? Name { get; set; }
    
    public int? Capacity { get; set; }
    
    public DateTime? CreatedAt { get; set; }
}
