using Mapster;
using MeetingRooms.Application.Resources;
using MeetingRooms.Domain.DTOs.Room;
using MeetingRooms.Domain.DTOs.Room.Response;
using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Exceptions;
using MeetingRooms.Domain.Interfaces;
using System.Net;

namespace MeetingRooms.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<CreateRoomResponseDTO> CreateRoom(CreateRoomDTO createRoomDTO)
    {
        Room? room = await _roomRepository.GetRoomByName(createRoomDTO.Name!);

        if (room is not null)
            throw new ServiceException(ApplicationMessage.Room_AlreadyRegistered, HttpStatusCode.BadRequest);

        room = createRoomDTO.Adapt<Room>();

        room = await _roomRepository.CreateRoom(room);

        CreateRoomResponseDTO createRoomResponseDTO = room.Adapt<CreateRoomResponseDTO>();

        return createRoomResponseDTO;
    }

    public async Task<UpdateRoomResponseDTO> UpdateRoom(UpdateRoomDTO updateRoomDTO)
    {
        Room? room = await _roomRepository.GetRoomById(updateRoomDTO.Id);

        if (room is null)
            throw new ServiceException(ApplicationMessage.Room_NotFound, HttpStatusCode.BadRequest);

        Room updatedRoom = ValueTuple.Create(updateRoomDTO, room).Adapt<Room>();

        room = await _roomRepository.UpdateRoom(updatedRoom);

        UpdateRoomResponseDTO updateRoomResponseDTO = updatedRoom.Adapt<UpdateRoomResponseDTO>();

        return updateRoomResponseDTO;
    }

    public async Task DeleteRoom(DeleteRoomDTO deleteRoomDTO)
    {
        Room? room = await _roomRepository.GetRoomById(deleteRoomDTO.Id);

        if (room is null)
            throw new ServiceException(ApplicationMessage.Room_NotFound, HttpStatusCode.BadRequest);

        await _roomRepository.DeleteRoom(room);
    }

    public async Task<List<GetRoomsResponseDTO>> GetRooms()
    {
        List<Room>? rooms = await _roomRepository.GetRooms();

        if (rooms is null)
            throw new ServiceException(ApplicationMessage.GetRooms_NoContent, HttpStatusCode.NoContent);

        List<GetRoomsResponseDTO> getRoomsResponseDTO = rooms.Adapt<List<GetRoomsResponseDTO>>();

        return getRoomsResponseDTO;
    }
}
