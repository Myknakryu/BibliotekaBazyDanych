using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class ReturnClientBookDTO
{
    [Required]
    public int IdBook { get; set; }
}