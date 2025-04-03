namespace MeetingRooms.Domain.DTOs.Reserve.Response;

public class CreateReserveResponseDTO
{
    public Guid? Id { get; set; }

    public DateTime? InitialDate { get; set; }

    public DateTime? FinalDate { get; set; }

    public string? User { get; set; }

    public string? Room { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
}
