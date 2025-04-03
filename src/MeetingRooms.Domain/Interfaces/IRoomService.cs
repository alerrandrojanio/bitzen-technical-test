using MeetingRooms.Domain.DTOs.Room;
using MeetingRooms.Domain.DTOs.Room.Response;

namespace MeetingRooms.Domain.Interfaces;

public interface IRoomService
{
    Task<CreateRoomResponseDTO> CreateRoom(CreateRoomDTO createRoomDTO);

    Task<UpdateRoomResponseDTO> UpdateRoom(UpdateRoomDTO updateRoomDTO);

    Task DeleteRoom(DeleteRoomDTO deleteRoomDTO);

    Task<List<GetRoomsResponseDTO>> GetRooms();
}
