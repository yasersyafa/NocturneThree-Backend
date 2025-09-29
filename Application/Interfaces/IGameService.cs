using Backend.Application.DTOs.Game;

namespace Backend.Application.Interfaces;

public interface IGameService
{
    Task<GameResponseDto> CreateGameAsync(CreateGameDto dto);
    Task<GameResponseDto?> GetGameByIdAsync(Guid id);
    Task<List<GameResponseDto>> GetAllGamesAsync();
    Task<GameResponseDto?> UpdateGameAsync(Guid id, UpdateGameDto dto);
    Task<bool> DeleteGameAsync(Guid id);
}