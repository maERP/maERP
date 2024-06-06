using maERP.Application.Extensions;
using maERP.Domain.Models.Common;
using System.Linq.Expressions;

namespace maERP.Application.Specifications.Base;

public abstract class FilterSpecification<T> : ISpecification<T> where T : class, IBaseEntity
{
#nullable disable
    public Expression<Func<T, bool>> Criteria { get; set; }
#nullable enable
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    
    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> query)
    {
        return Criteria = Criteria == null ? query : Criteria.And(query);
    }

    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> query)
    {
        return Criteria = Criteria == null ? query : Criteria.Or(query);
    }
}
