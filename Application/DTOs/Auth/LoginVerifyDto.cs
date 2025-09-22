namespace Backend.Application.DTOs.Auth;

public class LoginVerifyDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public string? DeviceInfo { get; set; }
}