namespace MeetingRooms.Domain.DTOs.User.Response;

public class UpdateUserResponseDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
