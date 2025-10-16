using maERP.Application.Contracts.Infrastructure;

namespace maERP.Infrastructure.EmailService;

public class EmailTemplateService : IEmailTemplateService
{
    public Task<string> GeneratePasswordResetEmailAsync(string recipientName, string resetToken, string resetUrl)
    {
        var html = $@"
<!DOCTYPE html>
<html lang='de'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Passwort zurücksetzen</title>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background-color: #f8f9fa; border-radius: 10px; padding: 30px;'>
        <h1 style='color: #0066cc; margin-top: 0;'>Passwort zurücksetzen</h1>

        <p>Hallo {recipientName},</p>

        <p>Sie haben eine Anfrage zum Zurücksetzen Ihres Passworts gestellt. Klicken Sie auf die Schaltfläche unten, um Ihr Passwort zurückzusetzen:</p>

        <div style='text-align: center; margin: 30px 0;'>
            <a href='{resetUrl}?token={Uri.EscapeDataString(resetToken)}'
               style='background-color: #0066cc; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                Passwort zurücksetzen
            </a>
        </div>

        <p style='color: #666; font-size: 14px;'>Falls die Schaltfläche nicht funktioniert, kopieren Sie bitte den folgenden Link in Ihren Browser:</p>
        <p style='background-color: #f1f1f1; padding: 10px; border-radius: 5px; word-break: break-all; font-size: 12px;'>
            {resetUrl}?token={Uri.EscapeDataString(resetToken)}
        </p>

        <p style='margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 14px;'>
            <strong>Wichtig:</strong> Wenn Sie diese E-Mail nicht angefordert haben, können Sie sie einfach ignorieren.
            Ihr Passwort wird nicht geändert.
        </p>

        <p style='color: #666; font-size: 14px;'>
            Dieser Link ist aus Sicherheitsgründen nur für eine begrenzte Zeit gültig.
        </p>

        <p style='margin-top: 30px; color: #999; font-size: 12px;'>
            Mit freundlichen Grüßen,<br>
            Ihr maERP Team
        </p>
    </div>
</body>
</html>";

        return Task.FromResult(html);
    }

    public Task<string> GenerateWelcomeEmailAsync(string recipientName)
    {
        var html = $@"
<!DOCTYPE html>
<html lang='de'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Willkommen bei maERP</title>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background-color: #f8f9fa; border-radius: 10px; padding: 30px;'>
        <h1 style='color: #0066cc; margin-top: 0;'>Willkommen bei maERP!</h1>

        <p>Hallo {recipientName},</p>

        <p>Vielen Dank für Ihre Registrierung bei maERP. Wir freuen uns, Sie als neuen Benutzer begrüßen zu dürfen!</p>

        <p>Mit maERP haben Sie Zugriff auf eine umfassende ERP-Lösung für Ihr Unternehmen.</p>

        <h2 style='color: #0066cc; font-size: 18px; margin-top: 30px;'>Nächste Schritte:</h2>
        <ul style='padding-left: 20px;'>
            <li>Richten Sie Ihr Unternehmensprofil ein</li>
            <li>Laden Sie Ihre Teammitglieder ein</li>
            <li>Konfigurieren Sie Ihre Einstellungen</li>
            <li>Beginnen Sie mit der Nutzung von maERP</li>
        </ul>

        <p style='margin-top: 30px; color: #666; font-size: 14px;'>
            Bei Fragen stehen wir Ihnen gerne zur Verfügung.
        </p>

        <p style='margin-top: 30px; color: #999; font-size: 12px;'>
            Mit freundlichen Grüßen,<br>
            Ihr maERP Team
        </p>
    </div>
</body>
</html>";

        return Task.FromResult(html);
    }

    public Task<string> GenerateEmailConfirmationAsync(string recipientName, string confirmationToken, string confirmationUrl)
    {
        var html = $@"
<!DOCTYPE html>
<html lang='de'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>E-Mail-Adresse bestätigen</title>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background-color: #f8f9fa; border-radius: 10px; padding: 30px;'>
        <h1 style='color: #0066cc; margin-top: 0;'>E-Mail-Adresse bestätigen</h1>

        <p>Hallo {recipientName},</p>

        <p>Bitte bestätigen Sie Ihre E-Mail-Adresse, indem Sie auf die Schaltfläche unten klicken:</p>

        <div style='text-align: center; margin: 30px 0;'>
            <a href='{confirmationUrl}?token={Uri.EscapeDataString(confirmationToken)}'
               style='background-color: #28a745; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                E-Mail bestätigen
            </a>
        </div>

        <p style='color: #666; font-size: 14px;'>Falls die Schaltfläche nicht funktioniert, kopieren Sie bitte den folgenden Link in Ihren Browser:</p>
        <p style='background-color: #f1f1f1; padding: 10px; border-radius: 5px; word-break: break-all; font-size: 12px;'>
            {confirmationUrl}?token={Uri.EscapeDataString(confirmationToken)}
        </p>

        <p style='margin-top: 30px; color: #999; font-size: 12px;'>
            Mit freundlichen Grüßen,<br>
            Ihr maERP Team
        </p>
    </div>
</body>
</html>";

        return Task.FromResult(html);
    }
}
