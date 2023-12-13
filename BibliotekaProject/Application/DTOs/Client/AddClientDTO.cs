using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class AddClientDTO
{
    [Required]
    public string Name {get; set;}
    [Required]
    public string LastName {get;set;}
}