using Backend.Application.DTOs.GameData;

namespace Backend.Application.Interfaces;

public interface IPlayerGameDataService
{
    Task<GameDataResponseDto> SaveAsync(Guid userId, SaveGameDataRequestDto dto);
    Task<GameDataResponseDto?> GetAsync(Guid userId, Guid gameId);
    Task<List<PlayerGameListResponseDto>> GetAllAsync(Guid userId);
    Task<bool> DeleteAsync(Guid userId, Guid gameId);
}
