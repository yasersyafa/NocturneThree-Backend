using Backend.Application.DTOs.Player;

namespace Backend.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public PlayerDto Player { get; set; } = new();
}