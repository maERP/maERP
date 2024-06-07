using maERP.Domain.Models.Common;
using System.Linq.Expressions;

namespace maERP.Application.Specifications.Base
{
    public interface ISpecification<T> where T : class, IBaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, bool>> And(Expression<Func<T, bool>> query);
        Expression<Func<T, bool>> Or(Expression<Func<T, bool>> query);
    }
}