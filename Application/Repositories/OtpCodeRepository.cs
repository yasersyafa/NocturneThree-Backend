using Backend.Application.Database;
using Backend.Application.Interfaces;
using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Repositories;

public class OtpCodeRepository(AppDbContext dbContext) : IOtpCodeRepository
{
    private readonly AppDbContext _db = dbContext;

    public async Task<OtpCode?> GetValidOtpAsync(string email, string code, string purpose)
    {
        return await _db.OtpCodes.FirstOrDefaultAsync(o =>
            o.Email == email &&
            o.Code == code &&
            o.Purpose == purpose &&
            !o.Consumed &&
            o.ExpiredAt > DateTime.UtcNow
        );
    }

    public async Task AddAsync(OtpCode otp)
        => await _db.OtpCodes.AddAsync(otp);

    public Task MarkAsConsumedAsync(OtpCode otp)
    {
        otp.Consumed = true;
        _db.OtpCodes.Update(otp);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();

    public async Task InvalidatePreviousOtpAsync(string email, string purpose)
    {
        var otps = await _db.OtpCodes
        .Where(o => o.Email == email && o.Purpose == purpose && !o.Consumed && o.ExpiredAt > DateTime.UtcNow)
        .ToListAsync();

        if (otps.Count != 0)
        {
            foreach (var otp in otps)
            {
                otp.Consumed = true;
            }

            _db.OtpCodes.UpdateRange(otps);
        }
    }
}