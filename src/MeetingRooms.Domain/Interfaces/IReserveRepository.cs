using MeetingRooms.Domain.DTOs.Reserve;
using MeetingRooms.Domain.Entities;

namespace MeetingRooms.Domain.Interfaces;

public interface IReserveRepository
{
    Task<Reserve?> CreateReserve(Reserve reserve);

    Task<Reserve?> CancelReserve(Reserve reserve);

    Task<Reserve?> GetReserveById(Guid id);

    Task<Reserve?> GetReserveByRoom(GetReservesByParamsDTO getReserveByRoomDTO);

    Task<bool> RoomHasReserved(CreateReserveDTO createReserveDTO);

    Task<List<Reserve>?> GetReservesByParams(GetReservesByParamsDTO getReservesByParamsDTO);
}
