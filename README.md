# MiniHittegods

Et lite REST API for håndtering av hittegods.

Prosjektet lar brukere registrere gjenstander som er funnet, hente ut oversikt over registrerte ting, markere gjenstander som hentet, og slette gjenstander som kan fjernes.

Dette prosjektet er laget som en del av opplæringen min i backend-utvikling, med fokus på TDD, API-utvikling og lagdeling av kode.

## Teknologi

* C# / .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Docker Compose
* xUnit
* Swagger

## Prosjektstruktur

Prosjektet er delt opp i flere lag:

```
MiniHittegods
│
├── MiniHittegods.Api
│   └── API, Controllers, Database og Repositories
│
├── MiniHittegods.Application
│   └── Services og logikk mellom API og Domain
│
├── MiniHittegods.Domain
│   └── Entities og regler for domenet
│
└── MiniHittegods.Tests
    └── Tester for funksjonalitet
```

## Starte prosjektet

### 1. Start PostgreSQL med Docker

Fra rotmappen:

```bash
docker compose up -d
```

### 2. Oppdater databasen

Kjør:

```bash
dotnet ef database update --project MiniHittegods.Api
```

### 3. Start API-et

```bash
dotnet run --project MiniHittegods.Api
```

Swagger blir tilgjengelig via adressen som vises i terminalen, for eksempel:

```
http://localhost:5111/swagger
```

## Kjøre tester

For å kjøre alle tester:

```bash
dotnet test
```

Prosjektet har tester for blant annet:

* Oppretting av nye funn
* Statusendringer (Available, Claimed, Returned)
* Validering av regler
* Repository og service-logikk

## Database

Prosjektet bruker PostgreSQL som database.

Docker Compose setter opp en lokal PostgreSQL-container med:

* Database: `minihittegods`
* Bruker: `postgres`
* Port: `5432`

## Litt om prosjektet

Dette var mitt første prosjekt hvor jeg jobbet mer strukturert med:

* Test Driven Development (TDD)
* Separasjon mellom Domain, Application og API
* Entity Framework og ekte database
* REST API-design

Målet var ikke å lage det største systemet mulig, men å bygge en fungerende backend med god struktur og forståelse for hvordan delene henger sammen.
