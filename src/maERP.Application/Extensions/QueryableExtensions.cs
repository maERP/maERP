using maERP.Application.Exceptions;
using maERP.Application.Specifications.Base;
using maERP.Domain.Entities.Common;
using maERP.Domain.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {
        if (source == null) throw new SourceNullException("source is null - pagination is aborted");
        pageNumber = pageNumber < 0 ? 0 : pageNumber;
        pageSize = pageSize == 0 ? 10 : pageSize;
        int count = await source.CountAsync();
        List<T> items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        return PaginatedResult<T>.Success(items, count, pageNumber + 1, pageSize);
    }

    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IBaseEntity
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(query,
                (current, include) => current.Include(include));
        var secondaryResult = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));
        return secondaryResult.Where(spec.Criteria);
    }
}
