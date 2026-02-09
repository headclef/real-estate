# Real Estate

This repository contains the Real Estate project's domain and an initial application layer.

Status: Scaffolded — domain entities and application-layer DTOs, interfaces, mappings, and validators have been added.

Planned updates:
- Project description
- Development setup and prerequisites
- Build and run instructions
- Configuration (appsettings, secrets)

**Entity Mapping**

- **BaseEntity**: Id, IsDeleted, CreatedAt, UpdatedAt — base for all entities. See [Core/Realestate.Domain/Entities/Commons/BaseEntity.cs](Core/Realestate.Domain/Entities/Commons/BaseEntity.cs#L1).
- **Estate**: Name, Description, Price, DivisionId, EstateTypeId, EstateStatusId — navigation to Division/EstateType/EstateStatus.
- **Division**: Name, Code, CountryId, DivisionTypeId, ParentId (self-reference) — navigation to Country/DivisionType/ParentDivision.
- **Staff**: Name, Surname, FullName, Code, Email, PhoneNumber, StaffRoleId — navigation to StaffRole.
- **DivisionType**, **Country**, **EstateType**, **EstateStatus**, **StaffRole**: simple lookup entities (Name and metadata).

**Application Layer (Core/Realestate.Application)**

The application layer is organized per-entity under `Core/Realestate.Application` with the following structure for each entity:

- `DTOs/<Entity>`: `Create<...>Dto`, `Update<...>Dto`, `<Entity>Dto`.
- `Interfaces/<Entity>`: service interface `I<Entity>Service` (CRUD methods returning `Response<T>`).
- `Mappings/<Entity>`: mapping extension methods to convert between entities and DTOs.
- `Validation/<Entity>`: simple create validators returning `Response<T>` with errors.

Shared items:
- `Wrappers/Response.cs`: generic `Response` and `Response<T>` wrapper used by service interfaces.

Example folders added:
- `DTOs/Estate`, `DTOs/Division`, `DTOs/Staff`, `DTOs/Country`, `DTOs/DivisionType`, `DTOs/EstateType`, `DTOs/EstateStatus`, `DTOs/StaffRole`
- `Interfaces/Estate`, `Interfaces/Division`, `Interfaces/Staff`, `Interfaces/Country`, `Interfaces/DivisionType`, `Interfaces/EstateType`, `Interfaces/EstateStatus`, `Interfaces/StaffRole`
- `Mappings/*`, `Validation/*` mirroring the above.

Next steps (options):
- Scaffold simple in-memory service implementations for each `I*Service`.
- Add persistence (EF Core) repositories and wiring.
- Generate a PlantUML ERD and place it in `docs/`.

If you want one of those now, tell me which and I will implement it.

