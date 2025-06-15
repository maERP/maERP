using System.Linq.Expressions;
using maERP.Application.Extensions;
using maERP.Domain.Entities.Common;

namespace maERP.Application.Specifications.Base;

public abstract class FilterSpecification<T> : ISpecification<T> where T : class, IBaseEntity
{
#nullable disable
    // ReSharper disable once NotNullOrRequiredMemberIsNotInitialized
    public Expression<Func<T, bool>> Criteria { get; set; }
#nullable enable
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();

    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public Expression<Func<T, bool>> And(Expression<Func<T, bool>> query)
    {
        // TODO: fix disable warning
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return Criteria = Criteria == null ? query : Criteria.And(query);
    }

    public Expression<Func<T, bool>> Or(Expression<Func<T, bool>> query)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return Criteria = Criteria == null ? query : Criteria.Or(query);
    }
}
