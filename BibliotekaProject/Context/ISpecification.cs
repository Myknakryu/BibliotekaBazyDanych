using System.Linq.Expressions;

namespace BibliotekaProject.Context;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    bool IgnoreQueryFilter { get; set; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    public int? Skip { get; }
    public int? Take { get; }
}
