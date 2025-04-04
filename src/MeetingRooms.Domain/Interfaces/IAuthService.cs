using MeetingRooms.Domain.DTOs.Auth;
using MeetingRooms.Domain.DTOs.Auth.Response;

namespace MeetingRooms.Domain.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO> Login(LoginDTO loginDTO);
}
