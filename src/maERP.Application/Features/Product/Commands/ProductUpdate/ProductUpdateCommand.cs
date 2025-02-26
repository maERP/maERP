using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}