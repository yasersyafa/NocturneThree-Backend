namespace Backend.Application.DTOs.Auth;

public class RegisterRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}