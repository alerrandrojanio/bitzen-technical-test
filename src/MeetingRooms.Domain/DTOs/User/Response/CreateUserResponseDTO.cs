namespace MeetingRooms.Domain.DTOs.User.Response;

public class CreateUserResponseDTO
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }
}
