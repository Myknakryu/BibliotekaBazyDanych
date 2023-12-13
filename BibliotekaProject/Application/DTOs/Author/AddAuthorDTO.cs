using System.ComponentModel.DataAnnotations;

namespace BibliotekaProject.Application.DTOs.Author;

public class AddAuthorDTO
{
    [Required]
    public string Name {get; set;}
}