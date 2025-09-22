using Backend.Application.Models;

namespace Backend.Application.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(Guid id);
    Task<Player?> GetByEmailAsync(string email);
    Task<Player?> GetByUsernameAsync(string username);
    Task AddAsync(Player player);
    Task UpdateAsync(Player player);
    Task SaveChangesAsync();
}