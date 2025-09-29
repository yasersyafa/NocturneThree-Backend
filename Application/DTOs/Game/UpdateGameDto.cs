using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs.Game;

public class UpdateGameDto
{
    [MaxLength(50, ErrorMessage = "Title cannot exceed more than 50 characters")]
    public required string Title { get; set; }
    public string? Description { get; set; }
}