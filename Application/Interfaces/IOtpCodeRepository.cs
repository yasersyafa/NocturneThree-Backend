using Backend.Application.Models;

namespace Backend.Application.Interfaces;

public interface IOtpCodeRepository
{
    Task<OtpCode?> GetValidOtpAsync(string email, string code, string purpose);
    Task AddAsync(OtpCode otp);
    Task MarkAsConsumedAsync(OtpCode otp);
    Task InvalidatePreviousOtpAsync(string email, string purpose);
    Task SaveChangesAsync();
}