using MeetingRooms.Domain.Entities;

namespace MeetingRooms.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> CreateUser(User user);

    Task<User?> UpdateUser(User user);

    Task DeleteUser(User user);

    Task<User?> GetUserById(Guid id);

    Task<User?> GetUserByEmail(string email);

    Task<List<User>> GetUsers();
}
