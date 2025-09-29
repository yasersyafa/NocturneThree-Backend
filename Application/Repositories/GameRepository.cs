using Backend.Application.Database;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Repositories;

public class GameRepository(AppDbContext db) : IGameRepository
{
    private readonly AppDbContext _db = db;
    public async Task AddAsync(Game game)
    {
        await _db.Games.AddAsync(game);
    }

    public Task DeleteAsync(Game game)
    {
        _db.Games.Remove(game);
        return Task.CompletedTask;
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _db.Games.ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await _db.Games.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public Task UpdateAsync(Game game)
    {
        _db.Games.Update(game);
        return Task.CompletedTask;
    }
}