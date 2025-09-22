namespace Backend.Application.Interfaces;

public interface IOtpCodeService
{
    Task<string> GenerateOtpAsync(string email, string purpose);
    Task<bool> ValidateOtpAsync(string email, string code, string purpose);
}