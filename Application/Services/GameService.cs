using AutoMapper;
using Backend.Application.DTOs.Game;
using Backend.Application.Interfaces;
using Backend.Application.Models;

namespace Backend.Application.Services;

public class GameService(IGameRepository gameRepository, IMapper mapper) : IGameService
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<GameResponseDto> CreateGameAsync(CreateGameDto dto)
    {
        var game = _mapper.Map<Game>(dto);
        await _gameRepository.AddAsync(game);
        await _gameRepository.SaveChangesAsync();
        return _mapper.Map<GameResponseDto>(game);
    }

    public async Task<GameResponseDto?> GetGameByIdAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        return game == null ? null : _mapper.Map<GameResponseDto>(game);
    }

    public async Task<List<GameResponseDto>> GetAllGamesAsync()
    {
        var game = await _gameRepository.GetAllAsync();
        return _mapper.Map<List<GameResponseDto>>(game);
    }

    public async Task<GameResponseDto?> UpdateGameAsync(Guid id, UpdateGameDto dto)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null) return null;

        game.Title = dto.Title;
        game.Description = dto.Description;

        await _gameRepository.UpdateAsync(game);
        await _gameRepository.SaveChangesAsync();

        return _mapper.Map<GameResponseDto>(game);
    }

    public async Task<bool> DeleteGameAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null) return false;
        await _gameRepository.DeleteAsync(game);
        await _gameRepository.SaveChangesAsync();

        return true;
    }
}