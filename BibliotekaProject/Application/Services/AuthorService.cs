using BibliotekaProject.Application.DTOs.Author;
using BibliotekaProject.Application.Services.Interfaces;
using BibliotekaProject.Context;
using BibliotekaProject.Entities;
using AutoMapper;

namespace BibliotekaProject.Application.Services;
class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Author> _authorRepository;
    public async Task<List<AuthorDetailsDTO>> GetAllAuthors()
    {

    }
    public async Task<AuthorDetailsDTO> GetSingleAuthor(int id)
    {
        
    }

    public async Task<int> Create(AuthorCreateDTO dto)
    {
        return 1;
    }
    public async Task Update(int id, AuthorUpdateDTO dto)
    {
        
    }
    public async Task Delete(int id)
    {
        
    }
}