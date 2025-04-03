using Mapster;
using MeetingRooms.Application.Resources;
using MeetingRooms.Domain.DTOs.User;
using MeetingRooms.Domain.DTOs.User.Response;
using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Exceptions;
using MeetingRooms.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MeetingRooms.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateUserResponseDTO> CreateUser(CreateUserDTO createUserDTO)
    {
        User? user = await _userRepository.GetUserByEmail(createUserDTO.Email!);

        if (user is not null)
            throw new ServiceException(ApplicationMessage.User_AlreadyRegistered, HttpStatusCode.BadRequest);

        string passwordHash = _passwordHasher.HashPassword(null!, createUserDTO.Password!);

        user = ValueTuple.Create(createUserDTO, passwordHash).Adapt<User>();

        user = await _userRepository.CreateUser(user);

        CreateUserResponseDTO createUserResponseDTO = user.Adapt<CreateUserResponseDTO>();

        return createUserResponseDTO;
    }

    public async Task<UpdateUserResponseDTO> UpdateUser(UpdateUserDTO updateUserDTO)
    {
        User? user = await _userRepository.GetUserById(updateUserDTO.Id);

        if (user is null)
            throw new ServiceException(ApplicationMessage.User_NotFound, HttpStatusCode.BadRequest);

        string passwordHash = _passwordHasher.HashPassword(null!, updateUserDTO.Password!);

        user = ValueTuple.Create(updateUserDTO, passwordHash).Adapt(user);

        user = await _userRepository.UpdateUser(user);

        UpdateUserResponseDTO updateUserResponseDTO = user.Adapt<UpdateUserResponseDTO>();

        return updateUserResponseDTO;
    }

    public async Task DeleteUser(DeleteUserDTO deleteUserDTO)
    {
        User? user = await _userRepository.GetUserById(deleteUserDTO.Id);

        if (user is null)
            throw new ServiceException(ApplicationMessage.User_NotFound, HttpStatusCode.BadRequest);

        await _userRepository.DeleteUser(user);
    }

    public async Task<List<GetUsersResponseDTO>> GetUsers()
    {
        List<User>? users = await _userRepository.GetUsers();

        if (users is null)
            throw new ServiceException(ApplicationMessage.GetAllUsers_NoContent, HttpStatusCode.NoContent);

        List<GetUsersResponseDTO> getUsersResponseDTO = users.Adapt<List<GetUsersResponseDTO>>();

        return getUsersResponseDTO;
    }
}
