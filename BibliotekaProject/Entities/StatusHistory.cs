namespace BibliotekaProject.Entities;

using BibliotekaProject.Entities.Base;
using BibliotekaProject.Entities.Enums;
using Microsoft.Identity.Client;

public class StatusHistory : BaseEntity
{

    public required int IdBook {get; set;}
    public Book Book { get; set; }
    public required int IdClient { get; set;}
    public Client Client { get; set; }
    public required StatusEnum StatusChange { get; set; }
    public static class Factory
    {
        public static StatusHistory Create(int idBook, int idClient, StatusEnum statusChange)
        {
            StatusHistory status = new()
            {
                IdBook = idBook,
                IdClient = idClient,
                StatusChange = statusChange,
            };
            return status;
        }
    }
}