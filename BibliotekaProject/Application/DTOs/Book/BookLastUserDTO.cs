using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class BookLastUserDTO
{
    public int Id { get; set; }
    public StatusEnum CurrentStatus { get; set; }
    public WearoutStatusEnum WearoutStatus { get; set; }
    public string Name { get; set; } 
    public string ISBN { get; set; }
    public int? IdClient { get; set; }
    public string? FullName { get; set; }
}
