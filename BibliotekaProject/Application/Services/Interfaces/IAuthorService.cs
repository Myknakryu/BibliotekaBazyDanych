using BibliotekaProject.Application.Services.Interfaces.Base;
using BibliotekaProject.Application.DTOs.Author;

namespace BibliotekaProject.Application.Services.Interfaces;

public interface IAuthorService : IService
{
    Task<List<AuthorDetailsDTO>> GetAllAuthors();
    Task<AuthorDetailsDTO> GetSingleAuthor(int id);

    Task<int> Create(AuthorCreateDTO dto);
    Task Update(int id, AuthorUpdateDTO dto);
    Task Delete(int id);
}