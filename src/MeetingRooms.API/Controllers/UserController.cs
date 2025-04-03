using MeetingRooms.API.Models.User;
using MeetingRooms.Domain.DTOs.User.Response;
using MeetingRooms.Domain.DTOs.User;
using MeetingRooms.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace MeetingRooms.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserModel createUserModel)
    {
        CreateUserDTO createUserDTO = createUserModel.Adapt<CreateUserDTO>();

        CreateUserResponseDTO? createUserResponseDTO = await _userService.CreateUser(createUserDTO);

        return Ok(createUserResponseDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(UpdateUserModel updateUserModel)
    {
        UpdateUserDTO updateUserDTO = updateUserModel.Adapt<UpdateUserDTO>();
        
        UpdateUserResponseDTO? updateUserResponseDTO = await _userService.UpdateUser(updateUserDTO);

        return Ok(updateUserResponseDTO);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(DeleteUserModel deleteUserModel)
    {
        DeleteUserDTO deleteUserDTO = deleteUserModel.Adapt<DeleteUserDTO>();

        await _userService.DeleteUser(deleteUserDTO);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        List<GetUsersResponseDTO> getUsersResponseDTO = await _userService.GetUsers();

        return Ok(getUsersResponseDTO);
    }
}
