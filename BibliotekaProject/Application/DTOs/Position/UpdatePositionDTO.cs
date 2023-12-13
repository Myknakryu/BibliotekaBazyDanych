using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaProject.Application.DTOs.Position;

public class UpdatePositionDTO
{
    public string Name { get; set; }

    [Unicode(false)]
    [MaxLength(13)]
    public string ISBN { get; set; }
    
    public int GenreId { get; set; }

    public List<int> AuthorIds { get; set; }
}