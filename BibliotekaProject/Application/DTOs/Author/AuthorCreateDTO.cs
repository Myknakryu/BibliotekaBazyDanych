using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities;

namespace BibliotekaProject.Application.DTOs.Author;
public class AuthorCreateDTO
{
    [Required]
    public string Name { get; set; }
}
