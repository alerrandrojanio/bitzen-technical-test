using MeetingRooms.Domain.DTOs.User;
using MeetingRooms.Domain.DTOs.User.Response;

namespace MeetingRooms.Domain.Interfaces;

public interface IUserService
{
    Task<CreateUserResponseDTO> CreateUser(CreateUserDTO createUserDTO);

    Task<UpdateUserResponseDTO> UpdateUser(UpdateUserDTO updateUserDTO);

    Task DeleteUser(DeleteUserDTO deleteUserDTO);

    Task<List<GetUsersResponseDTO>> GetUsers();
}
