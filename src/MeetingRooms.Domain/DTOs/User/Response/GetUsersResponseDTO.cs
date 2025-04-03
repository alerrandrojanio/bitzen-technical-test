namespace MeetingRooms.Domain.DTOs.User.Response;

public class GetUsersResponseDTO
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
