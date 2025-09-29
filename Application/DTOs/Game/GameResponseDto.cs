namespace Backend.Application.DTOs.Game;

public class GameResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}