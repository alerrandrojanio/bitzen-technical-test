namespace MeetingRooms.Domain.DTOs.Reserve;

public class CreateReserveDTO
{
    public Guid UserId { get; set; }
    
    public Guid RoomId { get; set; }
    
    public DateTime InitialDate { get; set; }
    
    public DateTime FinalDate { get; set; }
}
