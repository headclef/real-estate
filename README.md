# Real Estate

This repository contains the Real Estate project domain and services.

Status: Initial scaffold — update later with setup and usage details.

Planned updates:
- Project description
- Development setup and prerequisites
- Build and run instructions
- Configuration (appsettings, secrets)

## Entity Mapping

- **BaseEntity**: `Id` (int), `IsDeleted` (bool), `CreatedAt` (DateTime), `UpdatedAt` (DateTime?) — base for all entities. See Core/Realestate.Domain/Entities/Commons/BaseEntity.cs

- **Estate**: `Name`, `Description`, `Price` (decimal), `DivisionId`, `EstateTypeId`, `EstateStatusId`.
	- Navigation: `Division`, `EstateType`, `EstateStatus`.
	- FK: `DivisionId` -> `Division.Id`, `EstateTypeId` -> `EstateType.Id`, `EstateStatusId` -> `EstateStatus.Id`.

- **Division**: `Name`, `Code`, `CountryId`, `DivisionTypeId`, `ParentId?`.
	- Navigation: `Country`, `DivisionType`, `ParentDivision` (self-reference).
	- FK: `CountryId` -> `Country.Id`, `DivisionTypeId` -> `DivisionType.Id`, `ParentId` -> `Division.Id`.

- **Staff**: `Name`, `Surname`, computed `FullName`, `Code`, `Email`, `PhoneNumber`, `StaffRoleId`.
	- Navigation: `StaffRole`.
	- FK: `StaffRoleId` -> `StaffRole.Id`.

- **DivisionType**: `Name`.
- **Country**: `Name`, `IsoTwo`, `IsoThree`, `IsoNumber`, `Cctld`, `Plate`, `Currency`.
- **EstateType**: `Name`.
- **EstateStatus**: `Name`.
- **StaffRole**: `Name`.

If you'd like, I can add a PlantUML ERD or a graphical diagram and include it in this README.

