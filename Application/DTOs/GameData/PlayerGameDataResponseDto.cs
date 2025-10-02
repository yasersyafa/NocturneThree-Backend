namespace Backend.Application.DTOs.GameData;

public class SaveGameDataRequestDto
{
    public Guid GameId { get; set; }
    public object Data { get; set; } = new { }; // client kirim JSON
}

public class GameDataResponseDto
{
    public Guid GameId { get; set; }
    public object Data { get; set; } = new { };
    public DateTime UpdatedAt { get; set; }
}

public class PlayerGameListResponseDto
{
    public Guid GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}