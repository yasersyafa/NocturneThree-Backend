using Backend.Application.Interfaces;
using Backend.Application.Models;

namespace Backend.Application.Services;

public class OtpCodeService(IOtpCodeRepository otpRepo, IEmailService emailService) : IOtpCodeService
{
    private readonly IOtpCodeRepository _otpRepo = otpRepo;
    private readonly IEmailService _emailService = emailService;

    public async Task<string> GenerateOtpAsync(string email, string purpose)
    {
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