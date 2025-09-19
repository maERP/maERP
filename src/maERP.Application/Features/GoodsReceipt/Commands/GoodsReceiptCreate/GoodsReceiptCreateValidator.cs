using FluentValidation;
using maERP.Application.Contracts.Persistence;

namespace maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;

public class GoodsReceiptCreateValidator : AbstractValidator<GoodsReceiptCreateCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public GoodsReceiptCreateValidator(IProductRepository productRepository, IWarehouseRepository warehouseRepository)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;

        RuleFor(p => p.ReceiptDate)
            .NotEmpty().WithMessage("Receipt date is required.")
            .LessThanOrEqualTo(DateTime.Today.AddDays(1)).WithMessage("Receipt date cannot be in the future.");

        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("Product is required.")
            .MustAsync(ProductExists).WithMessage("Selected product does not exist.");

        RuleFor(p => p.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(999999).WithMessage("Quantity cannot exceed 999,999.");

        RuleFor(p => p.WarehouseId)
            .NotEmpty().WithMessage("Warehouse is required.")
            .MustAsync(WarehouseExists).WithMessage("Selected warehouse does not exist.");

        RuleFor(p => p.Supplier)
            .MaximumLength(255).WithMessage("Supplier cannot exceed 255 characters.");

        RuleFor(p => p.Notes)
            .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");
    }

    private async Task<bool> ProductExists(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return product != null;
    }

    private async Task<bool> WarehouseExists(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
        return warehouse != null;
    }
}