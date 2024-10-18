# Employment System

A C# .NET Core-based Employment System for managing job vacancies and applications. The system allows employers to create and manage vacancies, and applicants can apply to available job vacancies. The system also includes logic to handle limits on applications, vacancy activation status, and user restrictions on applying to too many vacancies.

## Features

- **Vacancies Management**: Employers can create, update, delete, and archive vacancies.
- **Job Applications**: Applicants can apply to vacancies while ensuring:
  - The vacancy is active.
  - The number of applications does not exceed 100.
  - The applicant has not applied to more than 24 vacancies.
- **Role-based Authorization**: The system uses role-based authentication and authorization for employers and applicants.
- **CQRS and DDD Patterns**: The project structure follows Command Query Responsibility Segregation (CQRS) and Domain-Driven Design (DDD) principles for better separation of concerns.
- **JWT Authentication**: Secure authentication using JWT tokens.

## Project Structure

The project is organized using Domain-Driven Design principles, separating concerns into layers:

- **Domain**: Contains core business logic and entities.
- **Application**: Handles application-specific logic, DTOs, and services.
- **Infrastructure**: Data access and persistence using Entity Framework Core.
- **Presentation**: Contains controllers and API endpoints.

## Prerequisites

- .NET Core SDK 6.0 or higher
- MS SQL Server
- Entity Framework Core
- JWT Authentication

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-repo/employment-system.git
cd employment-system
```

### 2. Set Up the Database

Ensure that MS SQL Server is installed and running, then create a new database for the application.

### 3. Update the Connection String

Modify the `appsettings.json` file to include your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
```

### 3. Apply Migrations

Use Entity Framework to apply the database migrations and create the necessary tables:

```bash
dotnet ef database update
```

### 5. Run the Application

Execute the following command to run the application:

```bash
dotnet run
```

The API will be available at `http://localhost:5000`.

## License

This project is open-source and available under the [MIT License](LICENSE).
