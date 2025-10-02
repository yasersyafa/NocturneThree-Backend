namespace Backend.Application.Models;

public class PlayerGameData
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Data { get; set; } = "{}";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // navigations
    public Player? Player { get; set; }
    public Game? Game { get; set; }
}