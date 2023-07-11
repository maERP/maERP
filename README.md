# maERP

 maERP ist ein Client-Server, Cross-Plattform, Open-Source ERP System entwickelt mit .NET 7, MAUI und Entity Framework. 

* :house: **GitHub:** [https://github.com/maERP/maERP](https://github.com/maERP/maERP)
* :speech_balloon: **Docker:** [https://hub.docker.com/u/maerp](https://hub.docker.com/u/maerp)

## Technologien

* Moderne Architektur mittels `ASP.NET Core 7`, `Entity Framework Core 7` und Dotnet MAUI
* Cross-Platform: Windows, MacOS, Linux (Server), iOS, Android
* Offene API zur Erweiterung und Anbindung von Anwendungen Dritter
* Unterstützt `Docker` out of the box

## maERP installieren

maERP besteht aus drei Teilprojekten:

|maERP.Server |Das eigentliche CMS. Headless und ohne eigenes Frontend.                   |         
|maERP.Web    |Das Web-Frontend zur Anbindung an maERP.Server                             |
|maERP.Client |Die Client-App (iOS, Android, Windows, MacOS) zur Anbindung an maERP.Server|

maERP.Server per Docker mit externem MySQL oder MariaDB-Server starten:

´´´
docker run -d --name maerp-server -p 8080 maerp/server -e DB_TYPE=mysql -e DB_HOST=localhost -e DB_PORT=3306 -e DB_NAME=maerp_01 -e DB_USER=maerp -e DB_PASS=YourHiddenPassword
´´´

maERP.Server per Docker mit externem PostgreSQL-Server starten:

´´´
docker run -d --name maerp-server -p 8080 maerp/server -e DB_TYPE=pgsql -e DB_HOST=localhost -e DB_PORT=5432 -e DB_NAME=maerp_01 -e DB_USER=maerp -e DB_PASS=YourHiddenPassword
´´´