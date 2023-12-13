using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities;

namespace BibliotekaProject.Application.DTOs.Author;
public class AuthorDetailsDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Position> Positions { get; set; } 
}
