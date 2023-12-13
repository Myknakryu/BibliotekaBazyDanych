using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Position;
using Microsoft.EntityFrameworkCore;


namespace BibliotekaProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionController : Controller
{
    private readonly DatabaseContext _dbContext;

    public PositionController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> AddPosition(AddPositionDTO dto)
    {
        var authors = await _dbContext.Authors.Where(x => dto.AuthorIds.Contains(x.Id)).ToListAsync();
        var position = new Position()
        {
            Name = dto.Name,
            ISBN = dto.ISBN,
            IdGenre = dto.GenreId,
            Authors = authors,

        };
        await _dbContext.Positions.AddAsync(position);
        await _dbContext.SaveChangesAsync();
        return Ok(position.Id);
    }

    [HttpGet]
    public IActionResult GetPositions()
    {
        return Ok(_dbContext.Positions
                            .Include(p=>p.Genre)
                            .Include(p=>p.Authors)
                            .Select(p => new 
                                  {
                                      p.Id,
                                      p.Name,
                                      p.ISBN,
                                      Genre = p.Genre.Name,
                                      Authors = p.Authors.Select(a => new { a.Id, a.Name })
                                  })
                            .ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetPosition(int id)
    {
        return Ok(_dbContext.Positions.Find(id));
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdatePosition(int id, UpdatePositionDTO dto)
    {

        var position = await _dbContext.Positions.FindAsync(id);
        if(position is not null)
        {
            var authors = await _dbContext.Authors.Where(x => dto.AuthorIds.Contains(x.Id)).ToListAsync();
            position.Update(dto.Name,dto.ISBN, dto.GenreId, authors);
            await _dbContext.SaveChangesAsync();
            return Ok(position);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeletePosition(int id)
    {
        var genre = await _dbContext.Positions.FindAsync(id);
        if(genre is not null)
        {
            _dbContext.Positions.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}