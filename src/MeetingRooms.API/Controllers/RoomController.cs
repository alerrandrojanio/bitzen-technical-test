using Mapster;
using MeetingRooms.API.Models.Room;
using MeetingRooms.Domain.DTOs.Room;
using MeetingRooms.Domain.DTOs.Room.Response;
using MeetingRooms.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRooms.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/room")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateRoom(CreateRoomModel createRoomModel)
    {
        CreateRoomDTO createRoomDTO = createRoomModel.Adapt<CreateRoomDTO>();

        CreateRoomResponseDTO? createRoomResponseDTO = await _roomService.CreateRoom(createRoomDTO);

        return Ok(createRoomResponseDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRoom(UpdateRoomModel updateRoomModel)
    {
        UpdateRoomDTO updateRoomDTO = updateRoomModel.Adapt<UpdateRoomDTO>();

        UpdateRoomResponseDTO? updateRoomResponseDTO = await _roomService.UpdateRoom(updateRoomDTO);

        return Ok(updateRoomResponseDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRoom(DeleteRoomModel deleteRoomModel)
    {
        DeleteRoomDTO deleteRoomDTO = deleteRoomModel.Adapt<DeleteRoomDTO>();

        await _roomService.DeleteRoom(deleteRoomDTO);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetRooms()
    {
        List<GetRoomsResponseDTO> getRoomsResponseDTO = await _roomService.GetRooms();

        return Ok(getRoomsResponseDTO);
    }
}
