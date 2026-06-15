# Reservations System API

## Description

Reservations System API is a RESTful web application built with ASP.NET Core for managing reservations of sports facilities.

The system allows users to create and manage reservations, manage sports facilities, and enforce business rules related to reservation scheduling and facility availability.

## Application Features

### Users

- Create users
- Retrieve users
- Update users
- Deactivate users

### Facilities

- Create facilities
- Retrieve facilities
- Update facilities
- Deactivate facilities

### Reservations

- Create reservations
- Retrieve reservations
- Update reservations
- Cancel reservations

### Additional Features

- CSV export of reservations
- Standardized API responses using Response Wrapper
- Swagger/OpenAPI documentation
- Data persistence using Entity Framework Core and SQL Server

## Business Rules

#### The application enforces the following rules:

- A facility cannot be reserved for overlapping time periods
- A user cannot have more than five reservations
- Reservations can only be created by active users and active facilities
- Facility name and location combination must be unique

## Structure

The application follows Clean Architecture and is organised into four layers:

- API - responsible for presentation, requests management and returning responses. Contains controllers and middleware.
- Application - responsible for business logic. Contains services, DTOs, validators and interfaces for repositories and services.
- Domain - contains core business entities and domain models.
- Infra - responsible for infrastructure that supports the application - persistence, database migrations, files management. Contains AppDbContext and its configuration, migration files, file export classes and repositories implementing interfaces from application layer.

### Implemented patterns and architectural solutions

The application uses several commonly adopted software design patterns:

- Repository Pattern - abstracts data access logic from business logic.
- Service Layer Pattern - encapsulates business rules and application workflows.
- Dependency Injection - manages object creation and dependencies through the ASP.NET Core built-in DI container.
- Data Transfer Object (DTO) Pattern - separates API contracts from domain entities.
- Middleware Pattern - provides centralized exception handling and request processing.
- Unit of Work Pattern - implemented by Entity Framework Core DbContext, which tracks changes and saves them through `SaveChangesAsync()` method.

## Technologies

Following technologies were used:

- C# - main programming language
- ASP.NET Core Web API (.NET 8) - web framework
- Microsoft SQL Server LocalDB - database
- Entity Framework Core - Object Relational Mapper (ORM) for handling database requests
- LINQ - provides a concise and readable way to query and manipulate collections and database data
- Swagger / OpenAPI - tool for API documentation and initial endpoint testing
- FluentValidation - library for verification of incoming requests data

## Requirements

Before running the application, ensure that the following software is installed:

- .NET 8 SDK
- SQL Server LocalDB
- Visual Studio 2022 (recommended)

## Configuration

Connection string is configured in:

`appsettings.json`

Example:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ReservationsSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

## Running the Application

Restore packages:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update --project ReservationsSystem.Infra --startup-project ReservationsSystem
```

```bash
dotnet run --project ReservationsSystem
```

## API Documentation

Swagger UI is available after application startup:

`https://localhost:<port>/swagger`

## Author

Sławomir Wąs
