using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

/// <summary>
/// Command for creating a new product in the system.
/// Inherits from ProductInputDto to get all product properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created product wrapped in a Result.
/// </summary>
public class ProductCreateCommand : ProductInputDto, IRequest<Result<int>>
{
}