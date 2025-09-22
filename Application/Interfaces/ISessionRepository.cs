using Backend.Application.Models;

namespace Backend.Application.Interfaces;

public interface ISessionRepository
{
    Task<Session?> GetByTokenAsync(string token);
    Task<List<Session>> GetByPlayerIdAsync(Guid playerId);
    Task AddAsync(Session session);
    Task RevokeAsync(Session session);
    Task SaveChangesAsync();
}