using System;
using FluentValidation;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

/// <summary>
/// Base validator for invoice input models.
/// Defines common validation rules for invoice data.
/// </summary>
public class InvoiceBaseValidator<T> : AbstractValidator<T> where T : IInvoiceInputModel
{
    public InvoiceBaseValidator()
    {
        // Basic invoice validation
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage("Die Rechnungsnummer darf nicht leer sein.")
            .MaximumLength(128).WithMessage("Die Rechnungsnummer darf maximal 128 Zeichen lang sein.");

        RuleFor(x => x.InvoiceDate)
            .NotEmpty().WithMessage("Das Rechnungsdatum darf nicht leer sein.")
            .LessThanOrEqualTo(_ => DateTime.UtcNow.AddDays(1)).WithMessage("Das Rechnungsdatum darf nicht in der Zukunft liegen.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Bitte wählen Sie einen Kunden aus.");

        // Payment information validation
        RuleFor(x => x.PaymentMethod)
            .NotEmpty().When(x => x.PaymentStatus != PaymentStatus.Unknown)
            .WithMessage("Bitte geben Sie eine Zahlungsmethode an, wenn ein Zahlungsstatus angegeben ist.");

        RuleFor(x => x.InvoiceStatus)
            .NotEqual(InvoiceStatus.Unknown).WithMessage("Bitte wählen Sie einen gültigen Rechnungsstatus aus.");

        // Invoice address validation
        RuleFor(x => x.InvoiceAddressFirstName)
            .NotEmpty().WithMessage("Der Vorname für die Rechnungsadresse ist erforderlich.");

        RuleFor(x => x.InvoiceAddressLastName)
            .NotEmpty().WithMessage("Der Nachname für die Rechnungsadresse ist erforderlich.");

        RuleFor(x => x.InvoiceAddressStreet)
            .NotEmpty().WithMessage("Die Straße für die Rechnungsadresse ist erforderlich.");

        RuleFor(x => x.InvoiceAddressCity)
            .NotEmpty().WithMessage("Die Stadt für die Rechnungsadresse ist erforderlich.");

        RuleFor(x => x.InvoiceAddressZip)
            .NotEmpty().WithMessage("Die Postleitzahl für die Rechnungsadresse ist erforderlich.");

        RuleFor(x => x.InvoiceAddressCountry)
            .NotEmpty().WithMessage("Das Land für die Rechnungsadresse ist erforderlich.");

        // Delivery address validation - only required if different from invoice address
        When(x => DeliveryAddressDiffersFromInvoice(x), () =>
        {
            RuleFor(x => x.DeliveryAddressFirstName)
                .NotEmpty().WithMessage("Der Vorname für die Lieferadresse ist erforderlich.");

            RuleFor(x => x.DeliveryAddressLastName)
                .NotEmpty().WithMessage("Der Nachname für die Lieferadresse ist erforderlich.");

            RuleFor(x => x.DeliveryAddressStreet)
                .NotEmpty().WithMessage("Die Straße für die Lieferadresse ist erforderlich.");

            RuleFor(x => x.DeliveryAddressCity)
                .NotEmpty().WithMessage("Die Stadt für die Lieferadresse ist erforderlich.");

            RuleFor(x => x.DeliveryAddressZip)
                .NotEmpty().WithMessage("Die Postleitzahl für die Lieferadresse ist erforderlich.");

            RuleFor(x => x.DeliveryAddressCountry)
                .NotEmpty().WithMessage("Das Land für die Lieferadresse ist erforderlich.");
        });

        // Totals validation
        RuleFor(x => x.Subtotal)
            .GreaterThanOrEqualTo(0).WithMessage("Die Zwischensumme muss größer oder gleich 0 sein.");

        RuleFor(x => x.ShippingCost)
            .GreaterThanOrEqualTo(0).WithMessage("Die Versandkosten müssen größer oder gleich 0 sein.");

        RuleFor(x => x.TotalTax)
            .GreaterThanOrEqualTo(0).WithMessage("Die Mehrwertsteuer muss größer oder gleich 0 sein.");

        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0).WithMessage("Die Gesamtsumme muss größer oder gleich 0 sein.");

        // Validate that total equals subtotal + shipping cost + tax
        RuleFor(x => x.Total)
            .Equal(x => x.Subtotal + x.ShippingCost + x.TotalTax)
            .WithMessage("Die Gesamtsumme entspricht nicht der Summe aus Zwischensumme, Versandkosten und Mehrwertsteuer.");
    }

    private bool DeliveryAddressDiffersFromInvoice(T model)
    {
        return model.DeliveryAddressFirstName != model.InvoiceAddressFirstName ||
               model.DeliveryAddressLastName != model.InvoiceAddressLastName ||
               model.DeliveryAddressStreet != model.InvoiceAddressStreet ||
               model.DeliveryAddressCity != model.InvoiceAddressCity ||
               model.DeliveryAddressZip != model.InvoiceAddressZip ||
               model.DeliveryAddressCountry != model.InvoiceAddressCountry;
    }
}
