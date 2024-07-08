# maERP

 maERP ist ein Client-Server, Cross-Plattform, Open-Source ERP System entwickelt mit .NET 8, MAUI und Entity Framework. 

* :house: **GitHub:** [https://github.com/maERP/maERP](https://github.com/maERP/maERP)
* :speech_balloon: **Docker:** [https://hub.docker.com/u/maerp](https://hub.docker.com/u/maerp)

## Technologien

* Moderne Architektur mittels `ASP.NET Core 8`, `Entity Framework Core 8` und Dotnet MAUI
* Cross-Platform: Windows, MacOS, Linux (Server), iOS, Android
* Offene API zur Erweiterung und Anbindung von Anwendungen Dritter
* Unterstützt `Docker` out of the box
* OpenAI und Claude AI Integration

## Projektübersicht

maERP besteht aus drei Teilprojekten:

| Projekt  | Funktion |
| ------------ | ------------ |
| maERP.Server | Das CMS. Headless und ohne eigenes Frontend.                            |
| maERP.Web    | Web-Frontend zur Anbindung an maERP.Server                              |
| maERP.Client | Client-App (iOS, Android, Windows, MacOS) zur Anbindung an maERP.Server |

## maERP Live-Demo

Web: [https://www.maERP.de](https://www.maerp.de)


## maERP installieren

maERP.Server per Docker mit externem MySQL-Server starten:

```
docker run -d --name maerp-server -p 8082:80 maerp/server -e ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=maerp_01;Uid=maerp;Password=maerp;"
```