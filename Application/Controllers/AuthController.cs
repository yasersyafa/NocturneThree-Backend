using Backend.Application.DTOs.Otp;
using Backend.Application.DTOs.Auth;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISessionRepository session, IAuthService auth) : ControllerBase
{
    private readonly ISessionRepository _sessionRepo = session;
    private readonly IAuthService _authService = auth;

    /// <summary>
    /// Request OTP untuk register
    /// </summary>
    [HttpPost("register/request")]
    public async Task<IActionResult> RegisterRequestOtp([FromBody] OtpRequestDto dto)
    {
        var success = await _authService.RegisterRequestOtpAsync(dto.Email);
        if (!success) return BadRequest(new { message = "Email already exist, please change your eamil." });
        return Ok(new { message = "OTP has sent to your email." });
    }

    /// <summary>
    /// Verifikasi OTP untuk register + buat akun player
    /// </summary>
    [HttpPost("register/verify")]
    public async Task<IActionResult> RegisterVerifyOtp([FromBody] RegisterVerifyDto dto)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var token = await _authService.VerifyOtpForRegisterAsync(dto.Email, dto.Code, dto.Username, dto.DeviceInfo, ip);

        if (token == null) return BadRequest(new { message = "OTP not valid or expired." });
        return Ok(new { token });
    }

    /// <summary>
    /// Request OTP untuk login
    /// </summary>
    [HttpPost("login/request")]
    public async Task<IActionResult> LoginRequestOtp([FromBody] OtpRequestDto dto)
    {
        var success = await _authService.LoginRequestOtpAsync(dto.Email);
        if (!success) return BadRequest(new { message = "Account not found." });
        return Ok(new { message = "OTP has sent to your email." });
    }

    /// <summary>
    /// Verifikasi OTP untuk login
    /// </summary>
    [HttpPost("login/verify")]
    public async Task<IActionResult> LoginVerifyOtp([FromBody] LoginVerifyDto dto)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var token = await _authService.VerifyOtpForLoginAsync(dto.Email, dto.Code, dto.DeviceInfo, ip);

        if (token == null) return BadRequest(new { message = "OTP not valid or expired." });
        return Ok(new { token });
    }

    /// <summary>
    /// Logout → revoke session
    /// </summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
            return Unauthorized(new { message = "Token not found." });

        var session = await _sessionRepo.GetByTokenAsync(token);
        if (session == null) return Unauthorized(new { message = "Session not valid." });

        await _sessionRepo.RevokeAsync(session);
        await _sessionRepo.SaveChangesAsync();

        return Ok(new { message = "Logout successful." });
    }
}