using MeetingRooms.Domain.Entities;

namespace MeetingRooms.Domain.Interfaces;

public interface IRoomRepository
{
    Task<Room?> CreateRoom(Room room);

    Task<Room?> UpdateRoom(Room room);

    Task DeleteRoom(Room room);

    Task<Room?> GetRoomById(Guid id);

    Task<Room?> GetRoomByName(string name);

    Task<List<Room>> GetRooms();
}
