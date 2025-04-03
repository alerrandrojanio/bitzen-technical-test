namespace MeetingRooms.Domain.DTOs.Reserve.Response;

public class CancelReserveResponseDTO
{
    public Guid? Id { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
