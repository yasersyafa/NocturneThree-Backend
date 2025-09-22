using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Application.Services;

public class OtpCodeService(IOtpCodeRepository otpRepo, IEmailService emailService, IMemoryCache memory) : IOtpCodeService
{
    private readonly IOtpCodeRepository _otpRepo = otpRepo;
    private readonly IEmailService _emailService = emailService;
    private readonly IMemoryCache _cache = memory;

    private const int OTP_LIMIT = 3;
    private static readonly TimeSpan OTP_WINDOW = TimeSpan.FromMinutes(1);

    public async Task<string> GenerateOtpAsync(string email, string purpose)
    {
        // create rate limiting
        var cacheKey = $"otp-req:{email.ToLower()}";
        var count = _cache.Get<int>(cacheKey);

        if (count >= OTP_LIMIT)
            throw new InvalidOperationException("Terlalu banyak request OTP. Coba lagi nanti.");

        // increment counter
        _cache.Set(cacheKey, count + 1, OTP_WINDOW);

        // invalidate old OTPs
        await _otpRepo.InvalidatePreviousOtpAsync(email, purpose);
        await _otpRepo.SaveChangesAsync();

        var code = new Random().Next(100000, 999999).ToString();

        var otp = new OtpCode
        {
            Email = email,
            Code = code,
            Purpose = purpose,
            Consumed = false,
            ExpiredAt = DateTime.UtcNow.AddMinutes(5),
            CreatedAt = DateTime.UtcNow
        };

        await _otpRepo.AddAsync(otp);
        await _otpRepo.SaveChangesAsync();

        await _emailService.SendOtpAsync(email, code);

        return code;
    }

    public async Task<bool> ValidateOtpAsync(string email, string code, string purpose)
    {
        var otp = await _otpRepo.GetValidOtpAsync(email, code, purpose);
        if (otp == null) return false;

        await _otpRepo.MarkAsConsumedAsync(otp);
        await _otpRepo.SaveChangesAsync();

        return true;
    }
}