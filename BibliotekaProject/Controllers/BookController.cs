using System.Data.Common;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using Microsoft.AspNetCore.Mvc;
using BibliotekaProject.Application.DTOs.Book;
using Microsoft.EntityFrameworkCore;
using BibliotekaProject.Entities.Enums;
using System.Drawing;


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
            WearoutStatus = dto.WearoutStatus,
            CurrentStatus = StatusEnum.InLibrary
        };
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
        return Ok(book.Id);
    }

    [HttpPost]
    [Route("{id:int}/borrow")]
    public async Task<IActionResult> BorrowBook(int id, BorrowBookDTO dto)
    {
        using(var transaction = _dbContext.Database.BeginTransaction()){
            try
            {
                var book = _dbContext.Books.Where(b => b.Id == id ).FirstOrDefault();
                if(book is not null)
                {
                    book.CurrentStatus = StatusEnum.Borrowed;
                    _dbContext.SaveChanges();
                    var client = _dbContext.Clients.Where(u => u.Id == dto.IdClient).FirstOrDefault();
                    if(client is not null)
                    {
                        if(client.HasBorrowedBook == true)
                        {
                            throw new Exception("User has already borrowed book");
                        }
                        client.HasBorrowedBook = true;
                        _dbContext.SaveChanges();
                        StatusHistory statusHistory = new StatusHistory()
                        {
                            IdBook = book.Id,
                            IdClient = client.Id,
                            StatusChange = StatusEnum.Borrowed
                        };
                        _dbContext.StatusHistories.Add(statusHistory);
                        _dbContext.SaveChanges();

                    }
                    else
                    {
                            throw new Exception("No such user");
                    }

                }
                else
                {
                    throw new Exception("Error when fetching Book ");
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest();
            }
        }
        return Ok();
    }

    [HttpPost]
    [Route("{id:int}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        using(var transaction = _dbContext.Database.BeginTransaction()){
            try
            {
                var book = _dbContext.Books.Where(b => b.Id == id ).FirstOrDefault();
                if(book is not null)
                {
                    book.CurrentStatus = StatusEnum.InLibrary;
                    _dbContext.SaveChanges();
                    var lastBookStatus = _dbContext.StatusHistories.Where(b=>b.IdBook == book.Id).OrderByDescending(b=>b.Id).FirstOrDefault();
                    if(lastBookStatus is null)
                    {
                        throw new Exception("No book history found");
                    }

                    var client = _dbContext.Clients.Where(u => u.Id == lastBookStatus.IdClient).FirstOrDefault();
                    if(client is not null)
                    {
                        client.HasBorrowedBook = false;
                        _dbContext.SaveChanges();
                        StatusHistory statusHistory = new StatusHistory()
                        {
                            IdBook = book.Id,
                            IdClient = client.Id,
                            StatusChange = StatusEnum.InLibrary
                        };
                        _dbContext.StatusHistories.Add(statusHistory);
                        _dbContext.SaveChanges();

                    }
                    else
                    {
                            throw new Exception("No such user");
                    }

                }
                else
                {
                    throw new Exception("Error when fetching Book ");
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest();
            }
        }
        return Ok();
    }

    [HttpGet]
    [Route("LastUsers")]
    public IActionResult GetAllBooksLastUser()
{
    var query = @"
        SELECT b.Id, b.CurrentStatus, b.WearoutStatus, p.Name, p.ISBN, 
        sh.IdClient, CONCAT(c.Name, ' ', c.LastName) AS 'FullName' FROM Books b
        INNER JOIN Positions p ON b.IdPosition = p.Id
        LEFT OUTER JOIN (
            SELECT IdBook, MAX(Id) as MaxId
            FROM StatusHistories GROUP BY IdBook
        ) as LatestSH ON LatestSH.IdBook = b.Id
        LEFT OUTER JOIN StatusHistories sh on sh.Id = LatestSH.MaxId
        LEFT OUTER JOIN Clients c on sh.IdClient = c.Id";

    var booksLastUsers = _dbContext.Database.SqlQueryRaw<BookLastUserDTO>(query).ToList();

    return Ok(booksLastUsers);
    }


    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(_dbContext.Books
                        .Include(b => b.Position)
                        .ThenInclude(p => p.Authors)
                        .Include(b => b.Position.Genre)
                        .Select(b => new
                        {
                            b.Id,
                            Position = new
                            {
                                b.Position.Id,
                                b.Position.Name,
                                Genre = b.Position.Genre != null ? new { b.Position.Genre.Id, b.Position.Genre.Name } : null,
                                Authors = b.Position.Authors.Select(a => new { a.Id, a.Name })
                            },
                            b.CurrentStatus
                        })
                        .ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var book = await _dbContext.Books
                            .Where(b => b.Id == id)
                            .Include(b => b.Position)
                                .ThenInclude(p => p.Authors)
                            .Include(b => b.Position.Genre)
                            .Select(b => new
                            {
                                b.Id,
                                Position = new
                                {
                                    b.Position.Id,
                                    b.Position.Name,
                                    Genre = b.Position.Genre != null ? new { b.Position.Genre.Id, b.Position.Genre.Name } : null,
                                    Authors = b.Position.Authors.Select(a => new { a.Id, a.Name })
                                },
                                b.CurrentStatus
                            })
                            .FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id, UpdateBookDTO dto)
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
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book is not null)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
}