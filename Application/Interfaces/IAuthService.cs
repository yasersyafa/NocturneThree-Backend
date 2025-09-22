using Backend.Application.DTOs.Auth;

namespace Backend.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterRequestOtpAsync(string email);
    Task<bool> LoginRequestOtpAsync(string email);
    Task<AuthResponseDto?> VerifyOtpForRegisterAsync(string email, string code, string username, string? deviceInfo, string? ipAddress);
    Task<AuthResponseDto?> VerifyOtpForLoginAsync(string email, string code, string? deviceInfo, string? ipAddress);
}