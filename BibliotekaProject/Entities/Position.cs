namespace BibliotekaProject.Entities;

using System.ComponentModel.DataAnnotations;
using BibliotekaProject.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

public class Position : BaseEntity
{
    public required string Name { get; set; }

    [Unicode(false)]
    [MaxLength(13)]
    public string? ISBN {get; set;}

    public int IdGenre {get; set;}
    public Genre Genre {get; set;}

    public List<Author> Authors {get; set;}
    public List<Book> Books { get; set; }

    public static class Factory
    {
        public static Position Create(string name, string? isbn, int idGerne, List<Author> authors)
        {
            Position position = new()
            {
                Name = name,
                ISBN = isbn,
                IdGenre = idGerne,
                Authors = authors
            };
            return position;
        }
    }
    public void Update(string? name, string? isbn, int? idGenre, List<Author>? authors)
    {
        if(!string.IsNullOrEmpty(name))
        {
            Name = name;
        }
        ISBN = isbn;
        if(authors is not null)
        {
            Authors = authors;
        }
        if(idGenre is not null)
        {
            IdGenre = (int)idGenre;
        }
    }
}