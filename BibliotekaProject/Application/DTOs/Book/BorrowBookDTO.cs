using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class BorrowBookDTO
{
    [Required]
    public int IdClient {get; set;}

}