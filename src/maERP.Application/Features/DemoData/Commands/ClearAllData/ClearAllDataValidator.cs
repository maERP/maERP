using FluentValidation;

namespace maERP.Application.Features.DemoData.Commands.ClearAllData;

public class ClearAllDataValidator : AbstractValidator<ClearAllDataCommand>
{
    public ClearAllDataValidator()
    {
    }
}