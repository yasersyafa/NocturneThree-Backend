using AutoMapper;
using Backend.Application.DTOs.GameData;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Newtonsoft.Json;

namespace Backend.Application.Services;

public class PlayerGameDataService(IPlayerGameDataRepository repo, IMapper mapper) : IPlayerGameDataService
{
    private readonly IPlayerGameDataRepository _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<GameDataResponseDto> SaveAsync(Guid userId, SaveGameDataRequestDto dto)
    {
        var existing = await _repo.GetByUserAndGameAsync(userId, dto.GameId);

        if (existing == null)
        {
            var newData = new PlayerGameData
            {
                UserId = userId,
                GameId = dto.GameId,
                Data = JsonConvert.SerializeObject(dto.Data),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(newData);
            await _repo.SaveChangesAsync();

            return _mapper.Map<GameDataResponseDto>(newData);
        }
        else
        {
            existing.Data = JsonConvert.SerializeObject(dto.Data);
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();

            return _mapper.Map<GameDataResponseDto>(existing);
        }
    }

    public async Task<GameDataResponseDto?> GetAsync(Guid userId, Guid gameId)
    {
        var data = await _repo.GetByUserAndGameAsync(userId, gameId);
        return data == null ? null : _mapper.Map<GameDataResponseDto>(data);
    }

    public async Task<List<PlayerGameListResponseDto>> GetAllAsync(Guid userId)
    {
        var list = await _repo.GetAllByUserAsync(userId);
        return _mapper.Map<List<PlayerGameListResponseDto>>(list);
    }

    public async Task<bool> DeleteAsync(Guid userId, Guid gameId)
    {
        var data = await _repo.GetByUserAndGameAsync(userId, gameId);
        if (data == null) return false;

        await _repo.DeleteAsync(data);
        await _repo.SaveChangesAsync();
        return true;
    }
}
