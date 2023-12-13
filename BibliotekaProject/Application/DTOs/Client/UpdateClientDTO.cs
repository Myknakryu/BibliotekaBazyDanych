using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Application.DTOs.Book;

public class UpdateClientDTO
{
    public string Name {get; set;}
    public string LastName {get;set;}
}