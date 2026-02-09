# Real Estate project

This project is a modern Real Estate Management system built using Clean Architecture principles on .NET 8.

## Project Structure

The solution is divided into several layers to ensure separation of concerns and maintainability:

### 1. Core
- **Realestate.Domain**: Contains the core domain entities (`Country`, `Estate`, `Staff`, etc.) and the `BaseEntity`.
- **Realestate.Application**: Contains the business logic contracts, DTOs, service interfaces, repository interfaces, mappings, and validation logic.

### 2. Business
- **Realestate.Business**: Contains the service implementations for the logic defined in the Application layer.

### 3. Infrastructure
- **Realestate.persistence**: Implements the data access layer.
    - **Hybrid Repository**: Uses **Entity Framework Core** for write operations (Add, Update, Delete) and **Dapper** for high-performance read operations (GetById, GetAll).
    - **Soft Delete**: All entities inherit from `BaseEntity` and deletions are handled by setting `IsDeleted = true`. Global query filters exclude deleted records by default.
    - **Contexts**: `ApplicationDbContext` handles EF configuration and audit fields.

### 4. Web / Presentation
- **Realestate.API**: A Web API entry point that exposes the application functionality to external consumers.

## Features
- **Clean Architecture**: Decoupled layers and dependency inversion.
- **Hybrid Data Access**: Combines the productivity of EF Core with the performance of Dapper.
- **Soft Deletes**: Automatic filtering of deleted records via the persistence layer.
- **Audit Logs**: Automated `CreatedAt` and `UpdatedAt` tracking.
- **Validation**: Per-entity validation using a consistent `Response` wrapper.

## Technological Stack
- **.NET 8.0**
- **Entity Framework Core 8.0** (SQL Server)
- **Dapper**
- **Dependency Injection**

## Getting Started
1. Configure the connection string in `Web/Realestate.API/appsettings.json`.
2. Run migrations (Planned).
3. Start the API.
