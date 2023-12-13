using System.ComponentModel.DataAnnotations;

namespace BibliotekaProject.Application.DTOs.Genre;

public class AddGenreDTO
{
    [Required]
    public string Name {get; set;}
}