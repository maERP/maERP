﻿using MediatR;

namespace maERP.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}