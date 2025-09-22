using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController(IPlayerRepository player) : ControllerBase
{
    private readonly IPlayerRepository _player = player;

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        if (!HttpContext.Items.ContainsKey("PlayerId"))
            return Unauthorized(new { message = "Not authenticated." });

        var playerId = (Guid)HttpContext.Items["PlayerId"]!;
        var player = await _player.GetByIdAsync(playerId);

        if (player == null) return NotFound(new { message = "Player not found." });

        return Ok(player);
    }
}