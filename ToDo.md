## Utviklingsplan

Målet er å bygge en fungerende MVP først, og heller legge til ekstra funksjoner dersom det er tid igjen.

Fokus:
- Ryddig struktur
- Domenelogikk
- Tester
- Fungerende REST API


# Mandag - Domenelag og grunnstruktur

## Før lunsj

Fokus:
- Lage grunnmodellene
- Implementere domeneregler
- Starte med tester (TDD)

Arbeid:
- Prize
- Participant
- PrizeStatus
- Regler for tildeling og utlevering

Mål:
- Domenelogikken fungerer
- Viktigste regler er testet
- Første tester kjører grønt


## Etter lunsj

Fokus:
- Bygge Application-laget

Arbeid:
- Services
- Repository interfaces
- DTO-struktur
- Mapping mellom modeller

Mål:
- Logikken er tilgjengelig gjennom services
- Klargjort for API-laget


# Tirsdag - API og funksjonalitet

Fokus:
- Bygge REST API
- Koble sammen lagene

Arbeid:
- Controllers
- CRUD for deltakere og premier
- Assign/Collect-endepunkter
- Validering
- HTTP-statuskoder
- Testing via Swagger

Mål:
- API fungerer
- Deltakere og premier kan opprettes, endres og hentes
- Domenereglene håndheves


# Onsdag - Testing og forbedringer

Fokus:
- Gjøre prosjektet ferdig og stabilt

Arbeid:
- Flere tester
- Feilhåndtering
- Rydding i kode
- Oppdatere README
- Kontrollere at build og tester fungerer

Mål:

dotnet test

- Alle tester kjører grønt
- Swagger viser fungerende endepunkter
- Prosjektet er klart for levering


# Prioriteringsrekkefølge

1. Domenemodeller og regler
2. Unit-tester
3. Application-lag
4. REST API
5. Validering og feilhåndtering
6. Ekstra funksjoner


# Stretch (kun hvis MVP er ferdig)

- EF Core + SQLite
- PostgreSQL + Docker Compose
- JWT/autentisering
- Enkel frontend
- Ekstra statistikk
- Audit logging