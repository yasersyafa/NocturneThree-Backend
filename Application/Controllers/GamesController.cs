using Backend.Application.DTOs.Game;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IGameService service) : ControllerBase
{
    private readonly IGameService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<GameResponseDto>>> GetAll()
    {
        var result = await _service.GetAllGamesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameResponseDto>> GetById(Guid id)
    {
        var result = await _service.GetGameByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GameResponseDto>> Create([FromBody] CreateGameDto dto)
    {
        var result = await _service.CreateGameAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GameResponseDto>> Update(Guid id, [FromBody] UpdateGameDto dto)
    {
        var result = await _service.UpdateGameAsync(id, dto);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteGameAsync(id);
        return success ? NoContent() : NotFound();
    }
}