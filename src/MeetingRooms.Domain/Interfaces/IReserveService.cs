using MeetingRooms.Domain.DTOs.Reserve;
using MeetingRooms.Domain.DTOs.Reserve.Response;

namespace MeetingRooms.Domain.Interfaces;

public interface IReserveService
{
    Task<CreateReserveResponseDTO> CreateReserve(CreateReserveDTO createReserveDTO);

    Task<CancelReserveResponseDTO> CancelReserve(CancelReserveDTO cancelReserveDTO);

    Task<List<GetReservesByParamsResponseDTO>?> GetReservesByParams(GetReservesByParamsDTO getReservesByParamsDTO);
}
