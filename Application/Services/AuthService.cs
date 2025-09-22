using AutoMapper;
using Backend.Application.DTOs.Auth;
using Backend.Application.DTOs.Player;
using Backend.Application.Interfaces;
using Backend.Application.Models;

namespace Backend.Application.Services;

public class AuthService(IPlayerRepository playerRepo, ISessionRepository sessionRepo, IOtpCodeService otpService, IMapper mapper) : IAuthService
{
    private readonly IPlayerRepository _playerRepo = playerRepo;
    private readonly ISessionRepository _sessionRepo = sessionRepo;
    private readonly IOtpCodeService _otpService = otpService;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> RegisterRequestOtpAsync(string email)
    {
        var existing = await _playerRepo.GetByEmailAsync(email);
        if (existing != null) return false;

        await _otpService.GenerateOtpAsync(email, "register");
        return true;
    }

    public async Task<bool> LoginRequestOtpAsync(string email)
    {
        var existing = await _playerRepo.GetByEmailAsync(email);
        if (existing == null) return false;

        await _otpService.GenerateOtpAsync(email, "login");
        return true;
    }

    public async Task<AuthResponseDto?> VerifyOtpForRegisterAsync(string email, string code, string username, string? deviceInfo, string? ipAddress)
    {
        var valid = await _otpService.ValidateOtpAsync(email, code, "register");
        if (!valid) return null;

        var player = new Player
        {
            Email = email,
            Username = username,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        };
        await _playerRepo.AddAsync(player);
        await _playerRepo.SaveChangesAsync();

        var token = Guid.NewGuid().ToString("N");
        var session = new Session
        {
            PlayerId = player.Id,
            Token = token,
            DeviceInfo = deviceInfo,
            IpAddress = ipAddress
        };
        await _sessionRepo.AddAsync(session);
        await _sessionRepo.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = token,
            Player = _mapper.Map<PlayerDto>(player)
        };
    }

    public async Task<AuthResponseDto?> VerifyOtpForLoginAsync(string email, string code, string? deviceInfo, string? ipAddress)
    {
        var valid = await _otpService.ValidateOtpAsync(email, code, "login");
        if (!valid) return null;

        var player = await _playerRepo.GetByEmailAsync(email);
        if (player == null) return null;

        player.LastLoginAt = DateTime.UtcNow;
        await _playerRepo.UpdateAsync(player);
        await _playerRepo.SaveChangesAsync();

        var token = Guid.NewGuid().ToString("N");
        var session = new Session
        {
            PlayerId = player.Id,
            Token = token,
            DeviceInfo = deviceInfo,
            IpAddress = ipAddress
        };
        await _sessionRepo.AddAsync(session);
        await _sessionRepo.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = token,
            Player = _mapper.Map<PlayerDto>(player)
        };
    }
}