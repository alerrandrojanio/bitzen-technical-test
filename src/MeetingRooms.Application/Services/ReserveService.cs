using Mapster;
using MeetingRooms.Application.Resources;
using MeetingRooms.Domain.DTOs.Reserve;
using MeetingRooms.Domain.DTOs.Reserve.Response;
using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Enums;
using MeetingRooms.Domain.Exceptions;
using MeetingRooms.Domain.Interfaces;
using System.Net;

namespace MeetingRooms.Application.Services;

public class ReserveService : IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;

    public ReserveService(IReserveRepository reserveRepository, IUserRepository userRepository, IRoomRepository roomRepository)
    {
        _reserveRepository = reserveRepository;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
    }

    public async Task<CreateReserveResponseDTO> CreateReserve(CreateReserveDTO createReserveDTO)
    {
        User? user = await _userRepository.GetUserById(createReserveDTO.UserId);

        if (user is null)
            throw new ServiceException(ApplicationMessage.User_NotFound, HttpStatusCode.NotFound);

        Room? room = await _roomRepository.GetRoomById(createReserveDTO.RoomId);

        if (room is null)
            throw new ServiceException(ApplicationMessage.Room_NotFound, HttpStatusCode.NotFound);

        bool isRoomAlreadyReserved = await _reserveRepository.RoomHasReserved(createReserveDTO);

        if (isRoomAlreadyReserved)
            throw new ServiceException(ApplicationMessage.Reserve_RoomReserved, HttpStatusCode.BadGateway);

        Reserve? reserve = createReserveDTO.Adapt<Reserve>();

        reserve = await _reserveRepository.CreateReserve(reserve);

        CreateReserveResponseDTO createReserveResponseDTO = ValueTuple.Create(reserve, user, room).Adapt<CreateReserveResponseDTO>();

        return createReserveResponseDTO;
    }

    public async Task<CancelReserveResponseDTO> CancelReserve(CancelReserveDTO cancelReserveDTO)
    {
        Reserve? reserve = await _reserveRepository.GetReserveById(cancelReserveDTO.Id);

        if (reserve is null || reserve.Status == ReserveStatus.CANCELED)
            throw new ServiceException(ApplicationMessage.Reserve_NotFound, HttpStatusCode.BadRequest);

        reserve = await _reserveRepository.CancelReserve(reserve);

        CancelReserveResponseDTO cancelReserveResponseDTO = reserve.Adapt<CancelReserveResponseDTO>();

        return cancelReserveResponseDTO;
    }

    public async Task<List<GetReservesByParamsResponseDTO>?> GetReservesByParams(GetReservesByParamsDTO getReservesByParamsDTO)
    {
        List<Reserve>? reserves = await _reserveRepository.GetReservesByParams(getReservesByParamsDTO);

        if (reserves is null || reserves.Count == 0)
            throw new ServiceException(ApplicationMessage.Reserve_NotFound, HttpStatusCode.NotFound);

        List<GetReservesByParamsResponseDTO> getReservesByParamsResponseDTO = reserves.Adapt<List<GetReservesByParamsResponseDTO>>();

        return getReservesByParamsResponseDTO;
    }
}
