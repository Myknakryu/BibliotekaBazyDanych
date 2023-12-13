using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Author;


namespace BibliotekaProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : Controller
{
    private readonly DatabaseContext _dbContext;

    public AuthorController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(AddAuthorDTO dto)
    {
        var author = new Author()
        {
            Name = dto.Name
        };
        await _dbContext.Authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();
        return Ok(author.Id);
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        return Ok(_dbContext.Authors.ToList());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if(author is not null)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}