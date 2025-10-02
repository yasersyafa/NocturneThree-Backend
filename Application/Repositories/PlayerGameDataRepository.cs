using Backend.Application.Database;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Repositories;

public class PlayerGameDataRepository(AppDbContext db) : IPlayerGameDataRepository
{
    private readonly AppDbContext _db = db;

    public async Task<PlayerGameData?> GetByUserAndGameAsync(Guid userId, Guid gameId)
    {
        return await _db.PlayerGameData
            .Include(x => x.Game)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.GameId == gameId);
    }

    public async Task<List<PlayerGameData>> GetAllByUserAsync(Guid userId)
    {
        return await _db.PlayerGameData
            .Include(x => x.Game)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(PlayerGameData data)
    {
        await _db.PlayerGameData.AddAsync(data);
    }

    public Task UpdateAsync(PlayerGameData data)
    {
        _db.PlayerGameData.Update(data);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(PlayerGameData data)
    {
        _db.PlayerGameData.Remove(data);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}