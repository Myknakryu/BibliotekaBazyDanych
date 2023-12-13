namespace BibliotekaProject.Context;
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(ISpecification<T> spec);
    Task<IEnumerable<T>> ListAsync(bool asNoTracking = false);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> list);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
    Task EditAsync(T entity);
    Task EditRangeAsync(IEnumerable<T> list);
    Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, bool asNoTracking = false);
    Task<bool> AnyAsync(ISpecification<T> spec);
    Task<bool> AnyAsync();

    Task<int> CountAsync();
    Task<int> CountAsync(ISpecification<T> spec);
}
