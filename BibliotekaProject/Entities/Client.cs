namespace BibliotekaProject.Entities;
using BibliotekaProject.Entities.Base;

public class Client : BaseEntity
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required bool HasBorrowedBook {get; set;} 
    public List<StatusHistory> StatusHistories { get; set; }

    public static class Factory
    {
        public static Client Create(string name, string lastName)
        {
            Client client = new()
            {
                Name = name,
                LastName = lastName,
                HasBorrowedBook = false
            };
            return client;
        }
    }
    public void Update(string? name, string? lastName, bool? hasBorrowedBook )
    {
        if(!string.IsNullOrEmpty(name))
        {
            Name = name;
        }
        if(!string.IsNullOrEmpty(lastName))
        {
            LastName = lastName;
        }
        if(hasBorrowedBook is not null)
        {
            HasBorrowedBook = (bool) hasBorrowedBook;
        }
    }
}