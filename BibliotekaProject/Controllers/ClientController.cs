using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Book;
using Microsoft.EntityFrameworkCore;
using BibliotekaProject.Entities.Enums;


namespace BibliotekaProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : Controller
{
    private readonly DatabaseContext _dbContext;

    public ClientController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddClientDTO dto)
    {
        var client = new Client()
        {
            Name = dto.Name,
            LastName = dto.LastName,
            HasBorrowedBook = false
        };
        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
        return Ok(client.Id);
    }

    [HttpGet]
    public IActionResult GetClients()
    {
        return Ok(_dbContext.Clients
                        .Select(b => new
                        {
                            b.Id,
                            b.Name,
                            b.LastName,
                            b.HasBorrowedBook   
                        })
                        .ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var client = await _dbContext.Clients
                            .Where(b => b.Id == id)
                            .Select(b => new
                            {
                                b.Id,
                                b.Name,
                                b.LastName,
                                b.HasBorrowedBook   
                            })
                            .FirstOrDefaultAsync();

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateClient(int id, UpdateBookDTO dto)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book is not null)
        {
            book.Update(dto.WearoutStatus, dto.CurrentStatus);
            await _dbContext.SaveChangesAsync();
            return Ok(book);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _dbContext.Clients.FindAsync(id);
        if (client is not null)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}