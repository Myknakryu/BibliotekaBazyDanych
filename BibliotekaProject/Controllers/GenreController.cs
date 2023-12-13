using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Genre;


namespace BibliotekaProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : Controller
{
    private readonly DatabaseContext _dbContext;

    public GenreController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre(AddGenreDTO dto)
    {
        var genre = new Genre()
        {
            Name = dto.Name
        };
        await _dbContext.Genres.AddAsync(genre);
        await _dbContext.SaveChangesAsync();
        return Ok(genre.Id);
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        return Ok(_dbContext.Genres.ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetGenre(int id)
    {
        return Ok(_dbContext.Genres.Find(id));
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id, UpdateGenreDTO dto)
    {
        var genre = await _dbContext.Genres.FindAsync(id);
        if(genre is not null)
        {
            genre.Update(dto.Name);
            await _dbContext.SaveChangesAsync();
            return Ok(genre);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);
        if(genre is not null)
        {
            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}