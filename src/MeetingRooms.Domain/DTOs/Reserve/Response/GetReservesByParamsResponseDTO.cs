namespace MeetingRooms.Domain.DTOs.Reserve.Response;

public class GetReservesByParamsResponseDTO
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }

    public DateTime InitialDate { get; set; }

    public DateTime FinalDate { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
