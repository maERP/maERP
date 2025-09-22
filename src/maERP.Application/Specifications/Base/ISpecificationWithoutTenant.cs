using System.Linq.Expressions;
using maERP.Domain.Entities.Common;

namespace maERP.Application.Specifications.Base;

public interface ISpecificationWithoutTenant<T> where T : class, IBaseEntityWithoutTenant
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, bool>> And(Expression<Func<T, bool>> query);
    Expression<Func<T, bool>> Or(Expression<Func<T, bool>> query);
}