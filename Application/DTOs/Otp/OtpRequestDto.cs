namespace Backend.Application.DTOs.Otp;

public class OtpRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty; // "login" or "register"
}