namespace Backend.Application.DTOs.Otp;

public class OtpVerifyDto
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
}