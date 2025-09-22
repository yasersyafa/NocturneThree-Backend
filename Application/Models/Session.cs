namespace Backend.Application.Models;

public class Session
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }  // FK ke Player
    public Player? Player { get; set; }  // Navigation property
    public string Token { get; set; } = string.Empty; // UUID / random string
    public string? DeviceInfo { get; set; } // Optional: model/OS device
    public string? IpAddress { get; set; } // IP login
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Revoked { get; set; } = false;
}