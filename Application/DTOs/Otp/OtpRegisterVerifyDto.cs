namespace Backend.Application.DTOs.Otp;

public class RegisterVerifyDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? DeviceInfo { get; set; }
}