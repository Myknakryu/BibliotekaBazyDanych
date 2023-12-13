namespace BibliotekaProject.Entities;
using BibliotekaProject.Entities.Base;

public class Author : BaseEntity
{
    public required string Name { get; set; }
    public List<Position> Positions {get; set;}
    
    public static class Factory
    {
        public static Author Create(string name)
        {
            Author author = new()
            {
                Name = name
            };
            return author;
        }
    }
    public void Update(string? name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            Name = name;
        }
    }
}