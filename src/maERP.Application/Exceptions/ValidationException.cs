using FluentValidation.Results;

namespace maERP.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {

    }

    public ValidationException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new();
        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }

    public List<string> ValidationErrors { get; set; } = new();
}