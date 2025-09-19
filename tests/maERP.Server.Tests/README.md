# maERP.Server.Tests

## Übersicht

Dieses Test-Projekt enthält Integration Tests für die maERP.Server API mit besonderem Fokus auf Multi-Tenancy.

## Teststruktur

### Infrastructure/
- **TestWebApplicationFactory.cs**: Konfiguriert eine Test-Instanz der API mit InMemory-Datenbank
- **BaseIntegrationTest.cs**: Basis-Klasse für alle Integration Tests mit nützlichen Hilfsmethoden
- **TestDataSeeder.cs**: Erstellt Test-Daten für verschiedene Tenants
- **TestAssertions.cs**: Ersatz für FluentAssertions mit einfachen Assert-Methoden

### Features/
- **AiModel/AiModelListQueryTests.cs**: Tests für AiModel-Liste mit Tenant-Isolation
- **TenantIsolation/TenantIsolationTests.cs**: Allgemeine Tests zur Tenant-Isolation

## Multi-Tenancy Testing Strategy

### 1. Tenant-Header Testing
Alle Tests verwenden den `X-Tenant-Id` Header um den aktuellen Tenant zu identifizieren:
```csharp
SetTenantHeader(1); // Setzt Tenant auf ID 1
```

### 2. Daten-Isolation Testing
- Jeder Test prüft, dass nur Daten des aktuellen Tenants zurückgegeben werden
- Tests für verschiedene Tenants laufen isoliert voneinander
- Ungültige oder fehlende Tenant-IDs führen zu leeren Resultaten

### 3. Test-Daten Management
- Verwendung von InMemory-Database pro Test
- Seeding von Test-Daten für verschiedene Tenants
- Automatische Cleanup nach jedem Test

## Ausführung der Tests

```bash
# Alle Tests
dotnet test tests/maERP.Server.Tests/

# Spezifische Test-Klasse
dotnet test tests/maERP.Server.Tests/ --filter "FullyQualifiedName~AiModelListQueryTests"

# Spezifischer Test
dotnet test tests/maERP.Server.Tests/ --filter "FullyQualifiedName~GetAiModels_WithValidTenant_ShouldReturnTenantSpecificModels"
```

## Erweiterte Test-Entwicklung

### Neue Feature-Tests hinzufügen:
1. Erstelle eine neue Test-Klasse unter `Features/[FeatureName]/`
2. Erbe von `BaseIntegrationTest`
3. Nutze `TestDataSeeder` für Test-Daten
4. Verwende `TestAssertions` für Assertions
5. Teste immer die Tenant-Isolation

### Test-driven Development:
1. Schreibe zuerst den Test (Red)
2. Implementiere das Feature (Green)
3. Refaktoriere den Code (Refactor)
4. Prüfe Tenant-Isolation

## Best Practices

- **Immer Tenant-Header setzen** in Tests die API-Endpoints aufrufen
- **Tenant-Isolation prüfen** durch Vergleich von Ergebnissen verschiedener Tenants
- **InMemory-Database verwenden** für schnelle, isolierte Tests
- **Test-Daten über Seeder** erstellen, nicht inline
- **Aussagekräftige Test-Namen** verwenden die das erwartete Verhalten beschreiben