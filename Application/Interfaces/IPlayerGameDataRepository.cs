using Backend.Application.Models;

namespace Backend.Application.Interfaces;

public interface IPlayerGameDataRepository
{
    Task<PlayerGameData?> GetByUserAndGameAsync(Guid userId, Guid gameId);
    Task<List<PlayerGameData>> GetAllByUserAsync(Guid userId);
    Task AddAsync(PlayerGameData data);
    Task UpdateAsync(PlayerGameData data);
    Task DeleteAsync(PlayerGameData data);
    Task SaveChangesAsync();
}
