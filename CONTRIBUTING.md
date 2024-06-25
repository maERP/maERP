# Contributing

Danke, dass du daran interessiert bist, zu unserem Projekt beizutragen! Diese Anleitung soll dir helfen, wie du am besten deinen Beitrag leisten kannst.

## Inhaltsverzeichnis
1. [Code of Conduct](#code-of-conduct)
2. [Wie kann ich beitragen?](#wie-kann-ich-beitragen)
    - [Fehler melden](#fehler-melden)
    - [Feature-Anfragen](#feature-anfragen)
    - [Code beitragen](#code-beitragen)
3. [Einrichtung der Entwicklungsumgebung](#einrichtung-der-entwicklungsumgebung)
4. [Pull Requests](#pull-requests)
5. [Kontakt](#kontakt)

## Code of Conduct

Dieses Projekt und alle Beteiligten verpflichten sich zu einem respektvollen und höflichen Umgang miteinander. Bitte lies unseren [Code of Conduct](CODE_OF_CONDUCT.md), um mehr zu erfahren.

## Wie kann ich beitragen?

### Fehler melden
Wenn du einen Fehler findest, melde ihn bitte im [Issue-Tracker](https://github.com/maERP/maERP/issues). Bevor du einen neuen Fehler meldest, überprüfe bitte, ob der Fehler nicht bereits gemeldet wurde.

#### Fehlerbericht Checkliste:
- **Titel**: Ein kurzer und prägnanter Titel.
- **Beschreibung**: Eine detaillierte Beschreibung des Problems.
- **Reproduktionsschritte**: Eine Liste der Schritte, um den Fehler zu reproduzieren.
- **Erwartetes Verhalten**: Was sollte deiner Meinung nach passieren?
- **Tatsächliches Verhalten**: Was ist tatsächlich passiert?
- **Screenshots/Logs**: Füge, wenn möglich, Screenshots oder Logs hinzu.
- **Umgebung**: Details zur Umgebung (Betriebssystem, Browser, usw.)

### Feature-Anfragen
Wir sind immer offen für neue Ideen! Wenn du eine Funktion vorschlagen möchtest, erstelle bitte ein neues Issue im [Issue-Tracker](https://github.com/maERP/maERP/issues).

#### Feature-Anfrage Checkliste:
- **Titel**: Ein kurzer und prägnanter Titel.
- **Beschreibung**: Eine detaillierte Beschreibung des Features und dessen Nutzen.
- **Beispiele**: Wenn möglich, füge Beispiele hinzu, wie das Feature verwendet werden könnte.

### Code beitragen
Wir freuen uns über jeden Beitrag! Hier sind einige Schritte, die du befolgen solltest, um sicherzustellen, dass dein Beitrag reibungslos integriert wird.

## Einrichtung der Entwicklungsumgebung
Um mit der Entwicklung zu beginnen, folge bitte diesen Schritten:

1. Forke das Repository.
2. Klone dein geforktes Repository:
   ```bash
   git clone https://github.com/maERP/maERP.git
   ```
3. Herunterladen der Abhängigkeiten und starten des Server-Projektes:
   ```bash
   dotnet restore
   cd maERP.Server && dotnet run
   ```

## Pull Requests
Bevor du einen Pull Request erstellst, stelle bitte sicher, dass:

- Du alle oben genannten Schritte befolgt hast.
- Dein Branch aktuell ist und keine Konflikte mit dem `main`-Branch aufweist.
- Alle Tests erfolgreich durchgelaufen sind:
  ```bash
  dotnet test
  ```

### Pull Request Prozess:
1. Erstelle einen neuen Branch für deine Arbeit:
   ```bash
   git checkout -b feature/mein-feature
   ```
2. Führe deine Änderungen durch und committe sie:
   ```bash
   git add .
   git commit -m "Beschreibung der Änderungen"
   ```
3. Pushe deinen Branch zu deinem Fork:
   ```bash
   git push origin feature/mein-feature
   ```
4. Erstelle einen Pull Request über die GitHub-Weboberfläche.

## Kontakt
Wenn du Fragen hast oder weitere Hilfe benötigst, zögere nicht, uns zu kontaktieren:

- [GitHub Issues](https://github.com/maERP/maERP/issues)
- E-Mail: [info@maERP.de](mailto:info@maERP.de)

Vielen Dank für deinen Beitrag!

---
