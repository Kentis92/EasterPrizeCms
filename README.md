# Påskeharens Premiesentral

Et lite REST API laget i C# og ASP.NET Core for å holde styr på deltakere, premier og utlevering av premier.

## Teknologi

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- PostgreSQL
- Docker Compose
- xUnit
- Swagger

## Prosjektstruktur

- `EasterPrizeCms.Domain` – modeller og domeneregler
- `EasterPrizeCms.Application` – services, DTOer og repository interfaces
- `EasterPrizeCms.Api` – API, controllers og database/repositories
- `EasterPrizeCms.Tests` – tester

## Kjøre prosjektet

Start API og PostgreSQL med:

```powershell
docker compose up --build
```

Swagger finnes på:

**http://localhost:8080/swagger**

For å stoppe containerne:

```powershell
docker compose down
```

## Tester

Kjør alle tester med:

```powershell
dotnet test
```

Prosjektet har tester for domeneregler, validering og API-endepunkter.

## API

### Participants

| Method | Endpoint                        | Beskrivelse                  |
| ------ | ------------------------------- | ---------------------------- |
| GET    | `/api/participants`             | Hent alle deltakere          |
| GET    | `/api/participants/{id}`        | Hent én deltaker             |
| POST   | `/api/participants`             | Opprett deltaker             |
| PUT    | `/api/participants/{id}`        | Oppdater deltaker            |
| DELETE | `/api/participants/{id}`        | Slett deltaker               |
| GET    | `/api/participants/{id}/prizes` | Hent premier til en deltaker |

### Prizes

| Method | Endpoint                   | Beskrivelse                |
| ------ | -------------------------- | -------------------------- |
| GET    | `/api/prizes`              | Hent alle premier          |
| GET    | `/api/prizes/{id}`         | Hent én premie             |
| POST   | `/api/prizes`              | Opprett premie             |
| PUT    | `/api/prizes/{id}`         | Oppdater premie            |
| DELETE | `/api/prizes/{id}`         | Slett premie               |
| POST   | `/api/prizes/{id}/assign`  | Tildel premie til deltaker |
| POST   | `/api/prizes/{id}/collect` | Marker premie som hentet   |
| GET    | `/api/prizes/statistics`   | Hent statistikk            |

## Regler

En premie starter som `InStock`.

- En premie kan bare tildeles når den er `InStock`.
- En premie kan bare hentes når den er `Assigned`.
- En `Collected` premie kan ikke slettes.
- En deltaker kan ikke slettes hvis de har en `Assigned` premie.

## Validering

### Deltakere

- Navn: 2–80 tegn
- By: 2–80 tegn
- Alder: 0–120

### Premier

- Navn: 2–80 tegn
- Verdi: 0 eller høyere

## Statuskoder

API-et bruker blant annet:

- `201 Created` ved opprettelse
- `200 OK` ved vellykkede operasjoner
- `204 No Content` ved sletting
- `400 Bad Request` ved ugyldig input
- `404 Not Found` når noe ikke finnes
- `409 Conflict` når en domeneregel blir brutt
