using Backend.Application.Database;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Repositories;

public class SessionRepository(AppDbContext dbContext) : ISessionRepository
{
    private readonly AppDbContext _db = dbContext;

    public async Task<Session?> GetByTokenAsync(string token)
        => await _db.Sessions.FirstOrDefaultAsync(s => s.Token == token && !s.Revoked);

    public async Task<List<Session>> GetByPlayerIdAsync(Guid playerId)
        => await _db.Sessions.Where(s => s.PlayerId == playerId && !s.Revoked).ToListAsync();

    public async Task AddAsync(Session session)
        => await _db.Sessions.AddAsync(session);

    public Task RevokeAsync(Session session)
    {
        session.Revoked = true;
        _db.Sessions.Update(session);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();
}