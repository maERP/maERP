# E-Mail Testing Guide für maERP

Diese Anleitung zeigt, wie Sie die Passwort-Zurücksetzen-Funktionalität und andere E-Mail-Features in maERP testen können.

## 📧 SMTP Test-Server starten (Mailpit)

### Mit Docker Compose:

```bash
# Im Projektverzeichnis ausführen
docker-compose -f docker-compose.mail.yml up -d

# Status überprüfen
docker-compose -f docker-compose.mail.yml ps

# Logs ansehen
docker-compose -f docker-compose.mail.yml logs -f mailpit
```

### Oder direkt mit Docker:

```bash
docker run -d \
  --name mailpit \
  -p 1025:1025 \
  -p 8025:8025 \
  axllent/mailpit:latest
```

### Mailpit Web-UI öffnen:

Öffnen Sie im Browser: **http://localhost:8025**

Hier sehen Sie alle E-Mails, die von Ihrer Anwendung versendet werden.

---

## 🚀 Server starten

```bash
cd src/maERP.Server
dotnet run
```

Der Server läuft standardmäßig auf `https://localhost:5001` und `http://localhost:5000`.

---

## 🧪 Passwort-Reset testen

### 1. Benutzer registrieren (falls noch nicht vorhanden)

```bash
curl -X POST https://localhost:5001/api/v1/auth/register \
  -H "Content-Type: application/json" \
  -k \
  -d '{
    "email": "test@example.com",
    "firstname": "Max",
    "lastname": "Mustermann",
    "username": "maxmustermann",
    "password": "Test123!"
  }'
```

### 2. Passwort-Reset anfordern

```bash
curl -X POST https://localhost:5001/api/v1/auth/forgot-password \
  -H "Content-Type: application/json" \
  -k \
  -d '{
    "email": "test@example.com"
  }'
```

**Erwartete Antwort:**
```json
{
  "succeeded": true,
  "message": "Falls ein Konto mit dieser E-Mail-Adresse existiert, wurde eine E-Mail mit Anweisungen zum Zurücksetzen des Passworts gesendet.",
  "statusCode": 200,
  "messages": [],
  "data": {
    "succeeded": true,
    "message": "Falls ein Konto mit dieser E-Mail-Adresse existiert, wurde eine E-Mail mit Anweisungen zum Zurücksetzen des Passworts gesendet."
  }
}
```

### 3. E-Mail in Mailpit überprüfen

1. Öffnen Sie http://localhost:8025
2. Sie sollten eine neue E-Mail sehen
3. Öffnen Sie die E-Mail und kopieren Sie den Reset-Token aus dem Link

**Der Link sieht etwa so aus:**
```
http://localhost:5173/reset-password?token=CfDJ8...sehr-langer-token...
```

### 4. Passwort zurücksetzen

Verwenden Sie den Token aus der E-Mail:

```bash
curl -X POST https://localhost:5001/api/v1/auth/reset-password \
  -H "Content-Type: application/json" \
  -k \
  -d '{
    "email": "test@example.com",
    "token": "HIER_DEN_TOKEN_AUS_DER_EMAIL_EINFÜGEN",
    "newPassword": "NeuesPasswort123!",
    "confirmPassword": "NeuesPasswort123!"
  }'
```

**Erwartete Antwort:**
```json
{
  "succeeded": true,
  "message": "Ihr Passwort wurde erfolgreich zurückgesetzt.",
  "statusCode": 200,
  "messages": [],
  "data": {
    "succeeded": true,
    "message": "Ihr Passwort wurde erfolgreich zurückgesetzt. Sie können sich jetzt mit Ihrem neuen Passwort anmelden."
  }
}
```

### 5. Mit neuem Passwort einloggen

```bash
curl -X POST https://localhost:5001/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -k \
  -d '{
    "email": "test@example.com",
    "password": "NeuesPasswort123!"
  }'
```

---

## ⚙️ E-Mail-Einstellungen konfigurieren

### Über appsettings.Development.json (Standard für Entwicklung)

Die Datei enthält bereits die Mailpit-Konfiguration:

```json
{
  "EmailSettings": {
    "ProviderType": "Smtp",
    "SmtpHost": "localhost",
    "SmtpPort": 1025,
    "SmtpUsername": "",
    "SmtpPassword": "",
    "SmtpEnableSsl": false,
    "FromAddress": "noreply@maerp.local",
    "FromName": "maERP Development",
    "PasswordResetUrl": "http://localhost:5173/reset-password"
  }
}
```

### Über Datenbank (System-weite Einstellungen)

Die E-Mail-Einstellungen werden beim ersten Start automatisch in die `Setting`-Tabelle eingefügt.

Um sie zu ändern:

```sql
-- SMTP Host ändern
UPDATE Setting SET Value = 'smtp.gmail.com' WHERE Key = 'Email.SmtpHost';

-- SMTP Port ändern
UPDATE Setting SET Value = '587' WHERE Key = 'Email.SmtpPort';

-- Von-Adresse ändern
UPDATE Setting SET Value = 'info@meinefirma.de' WHERE Key = 'Email.FromAddress';
```

### Tenant-spezifische Einstellungen

Jeder Tenant kann eigene E-Mail-Einstellungen in der `TenantEmailSettings`-Tabelle haben.

Beispiel-Insert:

```sql
INSERT INTO TenantEmailSettings (
    Id, TenantId, ProviderType, IsActive,
    SmtpHost, SmtpPort, SmtpUsername, SmtpPassword, SmtpEnableSsl,
    FromAddress, FromName, DateCreated, DateModified
) VALUES (
    NEWID(), -- oder gen_random_uuid() für PostgreSQL
    'TENANT_ID_HIER',
    0, -- 0 = Smtp, 1 = SendGrid
    1, -- IsActive = true
    'smtp.tenantserver.com',
    587,
    'user@tenantserver.com',
    'password123',
    1, -- EnableSsl = true
    'noreply@tenant.com',
    'Tenant Name',
    GETUTCDATE(),
    GETUTCDATE()
);
```

---

## 🔍 Debugging

### Logs überprüfen

Der Server gibt ausführliche Logs aus:

```
🔑 ForgotPassword called for email: test@example.com
✅ Password reset token generated for user: 123e4567-e89b-12d3-a456-426614174000
✅ Password reset email sent successfully to test@example.com
```

### Häufige Probleme

**Problem:** "Failed to send email"

**Lösung:**
- Prüfen Sie, ob Mailpit läuft: `docker ps | grep mailpit`
- Prüfen Sie die SMTP-Einstellungen in appsettings.json
- Prüfen Sie die Server-Logs auf Fehler

**Problem:** "No email received"

**Lösung:**
- Öffnen Sie http://localhost:8025 und aktualisieren Sie die Seite
- Prüfen Sie die E-Mail-Adresse des Benutzers
- Überprüfen Sie die Logs auf "email sent successfully"

**Problem:** "Invalid token"

**Lösung:**
- Kopieren Sie den kompletten Token aus der E-Mail
- Tokens sind zeitlich begrenzt (Standard: 1 Tag)
- Generieren Sie einen neuen Token mit `/forgot-password`

---

## 🌐 Produktions-Setup

### SendGrid verwenden

1. Erstellen Sie einen SendGrid-Account
2. Erstellen Sie einen API-Key
3. Konfigurieren Sie die Einstellungen:

```sql
UPDATE Setting SET Value = 'SendGrid' WHERE Key = 'Email.ProviderType';
UPDATE Setting SET Value = 'IHR_SENDGRID_API_KEY' WHERE Key = 'Email.ApiKey';
UPDATE Setting SET Value = 'verified@ihredomain.com' WHERE Key = 'Email.FromAddress';
UPDATE Setting SET Value = 'Ihre Firma' WHERE Key = 'Email.FromName';
```

### Echter SMTP-Server

```sql
UPDATE Setting SET Value = 'Smtp' WHERE Key = 'Email.ProviderType';
UPDATE Setting SET Value = 'smtp.gmail.com' WHERE Key = 'Email.SmtpHost';
UPDATE Setting SET Value = '587' WHERE Key = 'Email.SmtpPort';
UPDATE Setting SET Value = 'ihr-email@gmail.com' WHERE Key = 'Email.SmtpUsername';
UPDATE Setting SET Value = 'ihr-app-passwort' WHERE Key = 'Email.SmtpPassword';
UPDATE Setting SET Value = 'True' WHERE Key = 'Email.SmtpEnableSsl';
UPDATE Setting SET Value = 'noreply@ihredomain.com' WHERE Key = 'Email.FromAddress';
```

---

## 📊 E-Mail-Fallback-Hierarchie

Das System verwendet folgende Priorität:

1. **Tenant-spezifische Einstellungen** (aus `TenantEmailSettings` Tabelle)
2. **System-weite Einstellungen** (aus `Setting` Tabelle)
3. **appsettings.json** (als letzter Fallback)

---

## 🛑 Mailpit stoppen

```bash
# Mit Docker Compose:
docker-compose -f docker-compose.mail.yml down

# Oder direkt:
docker stop mailpit
docker rm mailpit
```

---

## 📝 Weitere Informationen

- **Mailpit GitHub**: https://github.com/axllent/mailpit
- **Mailpit Dokumentation**: https://mailpit.axllent.org/
- **ASP.NET Core Identity**: https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity
