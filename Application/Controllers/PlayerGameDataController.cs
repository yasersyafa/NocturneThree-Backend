using Backend.Application.DTOs.GameData;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerGameDataController(IPlayerGameDataService service) : ControllerBase
{
    private readonly IPlayerGameDataService _service = service;

    [HttpPost("save")]
    public async Task<ActionResult<GameDataResponseDto>> Save([FromBody] SaveGameDataRequestDto dto)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var result = await _service.SaveAsync(userId, dto);
        return Ok(result);
    }

    [HttpGet("{gameId}")]
    public async Task<ActionResult<GameDataResponseDto>> Get(Guid gameId)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var result = await _service.GetAsync(userId, gameId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<PlayerGameListResponseDto>>> GetAll()
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var result = await _service.GetAllAsync(userId);
        return Ok(result);
    }

    [HttpDelete("{gameId}")]
    public async Task<IActionResult> Delete(Guid gameId)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var success = await _service.DeleteAsync(userId, gameId);
        return success ? NoContent() : NotFound();
    }
}
