using Mapster;
using MeetingRooms.Domain.DTOs.User.Response;
using MeetingRooms.Domain.DTOs.User;
using MeetingRooms.Domain.Entities;
using MeetingRooms.API.Models.User;

namespace MeetingRooms.API.Mapping;

public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        #region CreateUser
        TypeAdapterConfig<(CreateUserDTO createUserDTO, string passwordHash), User>.NewConfig()
            .Map(dest => dest.Name, src => src.createUserDTO.Name)
            .Map(dest => dest.Email, src => src.createUserDTO.Email)
            .Map(dest => dest.PasswordHash, src => src.passwordHash)
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

        TypeAdapterConfig<User, CreateUserResponseDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        #endregion CreateUser

        #region UpdateUser
        TypeAdapterConfig<UpdateUserModel, UpdateUserDTO>.NewConfig()
            .Map(dest => dest.Name, src => src.Body!.Name)
            .Map(dest => dest.Email, src => src.Body!.Email)
            .Map(dest => dest.Password, src => src.Body!.Password);

        TypeAdapterConfig<(UpdateUserDTO updateUserDTO, string passwordHash), User>.NewConfig()
            .Map(dest => dest.Id, src => src.updateUserDTO.Id)
            .Map(dest => dest.Name, src => src.updateUserDTO.Name)
            .Map(dest => dest.Email, src => src.updateUserDTO.Email)
            .Map(dest => dest.PasswordHash, src => src.passwordHash)
            .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

        TypeAdapterConfig<User, UpdateUserResponseDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
        #endregion UpdateUser

        #region GetUsers
        TypeAdapterConfig<User, GetUsersResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        #endregion GetUsers
    }
}
