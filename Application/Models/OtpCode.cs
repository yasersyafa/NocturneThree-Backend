namespace Backend.Application.Models;

public class OtpCode
{
    public long Id { get; set; }  // bisa pakai bigint auto-increment
    public Guid? PlayerId { get; set; }  // nullable, karena register mungkin belum ada Player
    public Player? Player { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // 6 digit kode OTP
    public string Purpose { get; set; } = string.Empty; // "login" | "register"
    public bool Consumed { get; set; } = false;
    public DateTime ExpiredAt { get; set; } // waktu kadaluarsa
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}