namespace Backend.Application.Interfaces;

public interface IEmailService
{
    Task SendOtpAsync(string email, string otpCode);
}