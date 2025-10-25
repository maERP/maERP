using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;
using FluentValidation.Results;

namespace maERP.UI.Shared.Validation;

/// <summary>
/// Basis-ViewModel für FluentValidation-Integration mit Avalonia.
/// Implementiert INotifyDataErrorInfo für Avalonia's Data Binding.
///
/// Verwendung:
/// 1. Von dieser Klasse erben statt von ViewModelBase
/// 2. GetValidator() implementieren und eigenen Validator zurückgeben
/// 3. In OnPropertyChanged-Methoden ValidateProperty() aufrufen für Echtzeit-Validierung
/// 4. Vor Save-Operationen ValidateAllProperties() aufrufen
/// </summary>
public abstract class FluentValidationViewModelBase : ObservableObject, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();

    /// <summary>
    /// Muss von abgeleiteten Klassen implementiert werden, um den FluentValidator bereitzustellen.
    /// </summary>
    protected abstract IValidator GetValidator();

    /// <summary>
    /// Gibt an, ob das ViewModel Validierungsfehler enthält.
    /// </summary>
    public bool HasErrors => _errorsByPropertyName.Any();

    /// <summary>
    /// Ereignis, das ausgelöst wird, wenn sich die Validierungsfehler für eine Property ändern.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <summary>
    /// Gibt die Validierungsfehler für eine bestimmte Property zurück.
    /// </summary>
    /// <param name="propertyName">Name der Property oder null für alle Fehler</param>
    /// <returns>Enumerable von Fehlermeldungen</returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return _errorsByPropertyName.Values.SelectMany(x => x);

        if (_errorsByPropertyName.TryGetValue(propertyName, out var errors))
            return errors;

        return Enumerable.Empty<string>();
    }

    /// <summary>
    /// Validiert alle Properties mit FluentValidation.
    /// Sollte vor Save-Operationen aufgerufen werden.
    /// </summary>
    /// <returns>True wenn alle Properties valid sind, sonst false.</returns>
    protected bool ValidateAllProperties()
    {
        var validator = GetValidator();
        var validationResult = validator.Validate(new ValidationContext<object>(this));

        UpdateErrors(validationResult);

        return validationResult.IsValid;
    }

    /// <summary>
    /// Validiert eine einzelne Property.
    /// Wird typischerweise in OnPropertyChanged-Methoden aufgerufen für Echtzeit-Feedback.
    /// </summary>
    /// <param name="propertyName">Name der zu validierenden Property</param>
    protected void ValidateProperty(string propertyName)
    {
        var validator = GetValidator();

        // Validate all properties, then filter to the requested property
        var validationResult = validator.Validate(new ValidationContext<object>(this));

        // Extract only the errors for this property
        var propertyValidationResult = new ValidationResult(
            validationResult.Errors.Where(e => e.PropertyName == propertyName));

        UpdateErrorsForProperty(propertyName, propertyValidationResult);
    }

    /// <summary>
    /// Gibt die erste Fehlermeldung für eine Property zurück, oder null wenn keine Fehler vorhanden sind.
    /// Nützlich für XAML-Binding: public string? NameError => GetFirstErrorMessage(nameof(Name));
    /// </summary>
    /// <param name="propertyName">Name der Property</param>
    /// <returns>Erste Fehlermeldung oder null</returns>
    protected string? GetFirstErrorMessage(string propertyName)
    {
        if (_errorsByPropertyName.TryGetValue(propertyName, out var errors) && errors.Any())
        {
            return errors.First();
        }
        return null;
    }

    /// <summary>
    /// Löscht alle Validierungsfehler.
    /// Nützlich beim Laden neuer Daten oder Zurücksetzen des Formulars.
    /// </summary>
    protected void ClearErrors()
    {
        var propertyNames = _errorsByPropertyName.Keys.ToList();
        _errorsByPropertyName.Clear();

        foreach (var propertyName in propertyNames)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged($"{propertyName}Error"); // Notify error property changed
        }

        OnPropertyChanged(nameof(HasErrors));
    }

    /// <summary>
    /// Aktualisiert alle Fehler basierend auf dem Validierungsergebnis.
    /// </summary>
    private void UpdateErrors(ValidationResult validationResult)
    {
        // Alle bestehenden Fehler merken für Benachrichtigung
        var allPropertyNames = _errorsByPropertyName.Keys.ToList();
        _errorsByPropertyName.Clear();

        // Neue Fehler hinzufügen
        foreach (var error in validationResult.Errors)
        {
            if (!_errorsByPropertyName.TryGetValue(error.PropertyName, out var errors))
            {
                errors = new List<string>();
                _errorsByPropertyName[error.PropertyName] = errors;
            }

            errors.Add(error.ErrorMessage);
        }

        // Alle betroffenen Properties benachrichtigen (sowohl die mit neuen Fehlern als auch die, deren Fehler gelöscht wurden)
        var affectedProperties = allPropertyNames
            .Union(_errorsByPropertyName.Keys)
            .Distinct();

        foreach (var propertyName in affectedProperties)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged($"{propertyName}Error"); // Notify error property changed
        }

        OnPropertyChanged(nameof(HasErrors));
    }

    /// <summary>
    /// Aktualisiert die Fehler für eine einzelne Property.
    /// </summary>
    private void UpdateErrorsForProperty(string propertyName, ValidationResult validationResult)
    {
        // Fehler für diese Property entfernen
        _errorsByPropertyName.Remove(propertyName);

        // Neue Fehler für diese Property hinzufügen
        var propertyErrors = validationResult.Errors
            .Where(e => e.PropertyName == propertyName)
            .Select(e => e.ErrorMessage)
            .ToList();

        if (propertyErrors.Any())
        {
            _errorsByPropertyName[propertyName] = propertyErrors;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged($"{propertyName}Error"); // Notify error property changed
        OnPropertyChanged(nameof(HasErrors));
    }
}
