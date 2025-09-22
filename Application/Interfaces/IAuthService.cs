namespace Backend.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterRequestOtpAsync(string email);
    Task<bool> LoginRequestOtpAsync(string email);
    Task<string?> VerifyOtpForRegisterAsync(string email, string code, string username, string? deviceInfo, string? ipAddress);
    Task<string?> VerifyOtpForLoginAsync(string email, string code, string? deviceInfo, string? ipAddress);
}