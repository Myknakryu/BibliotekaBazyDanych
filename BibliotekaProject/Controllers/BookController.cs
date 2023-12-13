using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Book;


namespace BibliotekaProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : Controller
{
    private readonly DatabaseContext _dbContext;

    public BookController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddBookDTO dto)
    {
        var book = new Book()
        {
            IdPosition = dto.IdPosition,
            WearoutStatus = dto.WearoutStatus
        };
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
        return Ok(book.Id);
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(_dbContext.Books.ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetGenre(int id)
    {
        return Ok(_dbContext.Books.Find(id));
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id, UpdateBookDTO dto)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if(book is not null)
        {
            book.Update(dto.WearoutStatus);
            await _dbContext.SaveChangesAsync();
            return Ok(book);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if(book is not null)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}