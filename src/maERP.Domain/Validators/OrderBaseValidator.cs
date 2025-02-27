using FluentValidation;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class OrderBaseValidator<T> : AbstractValidator<T> where T : IOrderInputModel
{
}