namespace BibliotekaProject.Entities;
using BibliotekaProject.Entities.Base;

public class Genre : BaseEntity
{
    public required string Name { get; set; }
    public List<Position> Positions {get; set;}

    public static class Factory
    {
        public static Genre Create(string name)
        {
            Genre genre = new()
            {
                Name = name
            };
            return genre;
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