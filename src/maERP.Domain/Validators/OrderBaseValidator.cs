using System;
using FluentValidation;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;

namespace maERP.Domain.Validators;

public class OrderBaseValidator<T> : AbstractValidator<T> where T : IOrderInputModel
{
    public OrderBaseValidator()
    {
        // Basic order validation
        RuleFor(x => x.CustomerId)
            .NotEqual(Guid.Empty).WithMessage("Bitte wählen Sie einen Kunden aus.");

        RuleFor(x => x.SalesChannelId)
            .NotEqual(Guid.Empty).WithMessage("Bitte wählen Sie einen Vertriebskanal aus.");

        RuleFor(x => x.Status)
            .NotEqual(OrderStatus.Unknown).When(x => x.Status != OrderStatus.Pending)
            .WithMessage("Bitte wählen Sie einen gültigen Bestellstatus aus.")
            .Must(BeAValidOrderStatus)
            .WithMessage("Bitte wählen Sie einen gültigen Bestellstatus aus.");

        RuleFor(x => x.DateOrdered)
            .NotEmpty().WithMessage("Das Bestelldatum darf nicht leer sein.");

        // Payment information validation
        RuleFor(x => x.PaymentStatus)
            .Must(BeAValidPaymentStatus)
            .WithMessage("Bitte wählen Sie einen gültigen Zahlungsstatus aus.");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().When(x => x.PaymentStatus != PaymentStatus.Unknown)
            .WithMessage("Bitte geben Sie eine Zahlungsmethode an, wenn ein Zahlungsstatus angegeben ist.");

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

        // Shipping information validation
        //RuleFor(x => x.ShippingMethod)
        //    .NotEmpty().When(x => x.Status == OrderStatus.Processing || x.Status == OrderStatus.ReadyForDelivery || x.Status == OrderStatus.PartiallyDelivered)
        //    .WithMessage("Eine Versandmethode ist erforderlich, wenn die Bestellung in Bearbeitung ist oder versandt wurde.");
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

    private bool BeAValidOrderStatus(OrderStatus status)
    {
        return Enum.IsDefined(typeof(OrderStatus), status);
    }

    private bool BeAValidPaymentStatus(PaymentStatus status)
    {
        return Enum.IsDefined(typeof(PaymentStatus), status);
    }
}