namespace Backend.Application.Models;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}