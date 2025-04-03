using Mapster;
using MeetingRooms.API.Models.Reserve;
using MeetingRooms.Domain.DTOs.Reserve;
using MeetingRooms.Domain.DTOs.Reserve.Response;
using MeetingRooms.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reserve")]
public class ReserveController : ControllerBase
{
    private readonly IReserveService _reserveService;

    public ReserveController(IReserveService reserveService)
    {
        _reserveService = reserveService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateReserve(CreateReserveModel createReserveModel)
    {
        CreateReserveDTO createReserveDTO = createReserveModel.Adapt<CreateReserveDTO>();

        CreateReserveResponseDTO? createReserveResponseDTO = await _reserveService.CreateReserve(createReserveDTO);

        return Ok(createReserveResponseDTO);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelReserve(CancelReserveModel cancelReserveModel)
    {
        CancelReserveDTO cancelReserveDTO = cancelReserveModel.Adapt<CancelReserveDTO>();

        CancelReserveResponseDTO? cancelReserveResponseDTO = await _reserveService.CancelReserve(cancelReserveDTO);

        return Ok(cancelReserveResponseDTO);
    }

    [HttpGet]
    public async Task<ActionResult> GetReservesByParams([FromQuery] GetReservesByParamsModel getReservesByParamsModel)
    {
        GetReservesByParamsDTO getReservesByParamsDTO = getReservesByParamsModel.Adapt<GetReservesByParamsDTO>();

        List<GetReservesByParamsResponseDTO>? getReservesByParamsResponseDTO = await _reserveService.GetReservesByParams(getReservesByParamsDTO);

        return Ok(getReservesByParamsResponseDTO);
    }
}
