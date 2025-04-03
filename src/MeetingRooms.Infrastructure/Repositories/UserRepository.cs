using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Interfaces;
using MeetingRooms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingRooms.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> UpdateUser(User user)
    {
        _context.Entry(user).Property(room => room.CreatedAt).IsModified = false;

        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        User? user = await _context.Users.AsNoTracking()
                                         .Where(user => user.Id == id)
                                         .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        User? user = await _context.Users.AsNoTracking()
                                         .Where(user => user.Email == email)
                                         .FirstOrDefaultAsync();

        return user;
    }

    public async Task<List<User>> GetUsers()
    {
        List<User>? users = await _context.Users.ToListAsync();

        return users;
    }
}
