using Backend.Application.Database;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Repositories;

public class PlayerRepository(AppDbContext dbContext) : IPlayerRepository
{
    private readonly AppDbContext _db = dbContext;

    public async Task<Player?> GetByIdAsync(Guid id)
            => await _db.Players.FindAsync(id);

    public async Task<Player?> GetByEmailAsync(string email)
        => await _db.Players.FirstOrDefaultAsync(p => p.Email == email);

    public async Task<Player?> GetByUsernameAsync(string username)
        => await _db.Players.FirstOrDefaultAsync(p => p.Username == username);

    public async Task AddAsync(Player player)
        => await _db.Players.AddAsync(player);

    public Task UpdateAsync(Player player)
    {
        _db.Players.Update(player);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();
}