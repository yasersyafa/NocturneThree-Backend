using Backend.Application.Interfaces;
using Backend.Application.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Backend.Application.Services;

public class EmailService(IOptions<EmailSettings> options, IWebHostEnvironment environment) : IEmailService
{
    private readonly EmailSettings _settings = options.Value;
    private readonly IWebHostEnvironment _env = environment;

    public async Task SendOtpAsync(string email, string otpCode)
    {
        var templatePath = Path.Combine(_env.ContentRootPath, "Application/Templates", "EmailVerification.html");
        var htmlBody = await File.ReadAllTextAsync(templatePath);

        // Replace placeholder OTP
        htmlBody = htmlBody.Replace("{{OTP_CODE}}", otpCode);

        // Buat message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Nocturne Three ID", _settings.From));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Your Verification Code";

        // Logo path
        var logoPath = Path.Combine(_env.ContentRootPath, "Application/wwwroot", "NTID.JPG");

        var builder = new BodyBuilder();

        // Embed logo inline
        var logo = builder.LinkedResources.Add(logoPath);
        logo.ContentId = "logoImage";

        builder.HtmlBody = htmlBody;

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServer, _settings.Port, false);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }


}