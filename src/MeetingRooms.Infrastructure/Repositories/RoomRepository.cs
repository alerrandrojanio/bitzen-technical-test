using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Interfaces;
using MeetingRooms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingRooms.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Room?> CreateRoom(Room room)
    {
        await _context.Rooms.AddAsync(room);

        await _context.SaveChangesAsync();

        return room;
    }

    public async Task<Room?> UpdateRoom(Room room)
    {
        _context.Entry(room).Property(room => room.CreatedAt).IsModified = false;

        _context.Rooms.Update(room);

        await _context.SaveChangesAsync();

        return room;
    }

    public async Task DeleteRoom(Room room)
    {
        _context.Rooms.Remove(room);

        await _context.SaveChangesAsync();
    }

    public async Task<Room?> GetRoomById(Guid id)
    {
        Room? user = await _context.Rooms.AsNoTracking()
                                         .Where(room => room.Id == id)
                                         .FirstOrDefaultAsync();

        return user;
    }

    public async Task<Room?> GetRoomByName(string name)
    {
        Room? room = await _context.Rooms.AsNoTracking()
                                         .Where(room => room.Name == name)
                                         .FirstOrDefaultAsync();

        return room;
    }

    public async Task<List<Room>> GetRooms()
    {
        List<Room>? rooms = await _context.Rooms.ToListAsync();

        return rooms;
    }
}
