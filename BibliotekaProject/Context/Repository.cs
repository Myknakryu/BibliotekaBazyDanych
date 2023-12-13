using Microsoft.EntityFrameworkCore;

namespace BibliotekaProject.Context;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DatabaseContext _dbContext;
    private Repository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> ListAsync(bool asNoTracking = false)
    {
        if (asNoTracking)
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, bool asNoTracking = false)
    {
        IQueryable<T> queryableResultWithIncludes = spec.Includes
            .Aggregate(_dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

        IQueryable<T> query = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));

        if (spec.IgnoreQueryFilter)
            query = query.IgnoreQueryFilters();

        query = query.Where(spec.Criteria);

        if (spec.OrderBy is not null)
            query = query.OrderBy(spec.OrderBy);
        if (spec.OrderByDescending is not null)
            query = query.OrderByDescending(spec.OrderByDescending);
        if (spec.Skip is not null)
            query = query.Skip(spec.Skip.Value);
        if (spec.Take is not null)
            query = query.Take(spec.Take.Value);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        _dbContext.SaveChanges();
    }

    public async Task AddRangeAsync(IEnumerable<T> list)
    {
        await _dbContext.Set<T>().AddRangeAsync(list);
        _dbContext.SaveChanges();
    }

    public async Task EditAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditRangeAsync(IEnumerable<T> list)
    {
        foreach (T entity in list)
            _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(ISpecification<T> spec)
    {
        IQueryable<T> queryableResultWithIncludes = spec.Includes
            .Aggregate(_dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

        IQueryable<T> query = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));

        if (spec.IgnoreQueryFilter)
        {
            return await query
                        .IgnoreQueryFilters()
                        .Where(spec.Criteria)
                        .SingleOrDefaultAsync();
        }
        return await query
                        .Where(spec.Criteria)
                        .SingleOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(ISpecification<T> spec)
    {
        IQueryable<T> queryableResultWithIncludes = spec.Includes
            .Aggregate(_dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

        IQueryable<T> query = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));

        if (spec.IgnoreQueryFilter)
        {
            return await query
                        .IgnoreQueryFilters()
                        .AnyAsync(spec.Criteria);
        }
        return await query.AnyAsync(spec.Criteria);
    }

    public async Task<bool> AnyAsync()
    {
        return await _dbContext.Set<T>().AnyAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        IQueryable<T> queryableResultWithIncludes = spec.Includes
          .Aggregate(_dbContext.Set<T>().AsQueryable(),
              (current, include) => current.Include(include));

        IQueryable<T> query = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));

        if (spec.IgnoreQueryFilter)
            query = query.IgnoreQueryFilters();

        query = query.Where(spec.Criteria);

        return await query.CountAsync();
    }
}
