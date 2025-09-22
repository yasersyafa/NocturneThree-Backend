namespace Backend.Application.DTOs.Session;

public class SessionDto
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}