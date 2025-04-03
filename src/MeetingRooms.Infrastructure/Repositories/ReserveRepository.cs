using MeetingRooms.Domain.DTOs.Reserve;
using MeetingRooms.Domain.Entities;
using MeetingRooms.Domain.Enums;
using MeetingRooms.Domain.Interfaces;
using MeetingRooms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MeetingRooms.Infrastructure.Repositories;

public class ReserveRepository : IReserveRepository
{
    private readonly AppDbContext _context;

    public ReserveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Reserve?> CreateReserve(Reserve reserve)
    {
        await _context.Reserves.AddAsync(reserve);

        await _context.SaveChangesAsync();

        return reserve;
    }

    public async Task<Reserve?> CancelReserve(Reserve reserve)
    {
        reserve.Status = ReserveStatus.CANCELED;
        reserve.UpdatedAt = DateTime.UtcNow;

        _context.Reserves.Update(reserve);

        await _context.SaveChangesAsync();

        return reserve;
    }

    public async Task<Reserve?> GetReserveById(Guid id)
    {
        Reserve? reserve = await _context.Reserves.AsNoTracking()
                                         .Where(reserve => reserve.Id == id)
                                         .FirstOrDefaultAsync();

        return reserve;
    }

    public Task<Reserve?> GetReserveByRoom(GetReservesByParamsDTO getReserveByRoomDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RoomHasReserved(CreateReserveDTO createReserveDTO)
    {
        bool hasReserved = await _context.Reserves.AnyAsync(room => room.RoomId == createReserveDTO.RoomId &&
                                                                 room.Status != ReserveStatus.CANCELED &&
                                                                 (room.InitialDate < createReserveDTO.FinalDate && room.FinalDate > createReserveDTO.InitialDate));

        return hasReserved;
    }

    public async Task<List<Reserve>?> GetReservesByParams(GetReservesByParamsDTO getReservesByParamsDTO)
    {
        var query = _context.Reserves.AsQueryable();

        if (getReservesByParamsDTO.UserId.HasValue)
            query = query.Where(reserve => reserve.UserId == getReservesByParamsDTO.UserId.Value);

        if (getReservesByParamsDTO.RoomId.HasValue)
            query = query.Where(reserve => reserve.RoomId == getReservesByParamsDTO.RoomId.Value);

        if (getReservesByParamsDTO.InitialDate.HasValue)
            query = query.Where(reserve => reserve.InitialDate >= getReservesByParamsDTO.InitialDate.Value);

        if (getReservesByParamsDTO.FinalDate.HasValue)
            query = query.Where(reserve => reserve.FinalDate <= getReservesByParamsDTO.FinalDate.Value);

        if (getReservesByParamsDTO.Status.HasValue)
            query = query.Where(reserve => reserve.Status == getReservesByParamsDTO.Status.Value);

        List<Reserve>? reserves = await query.ToListAsync();

        return reserves;
    }
}
