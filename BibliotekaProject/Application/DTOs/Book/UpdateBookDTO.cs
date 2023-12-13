using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class UpdateBookDTO
{
    [Required]
    public int IdPosition {get; set;}
    [Required]
    public WearoutStatusEnum WearoutStatus {get;set;}
}